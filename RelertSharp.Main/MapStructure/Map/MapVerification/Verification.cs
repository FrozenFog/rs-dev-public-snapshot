﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.Common;
using RelertSharp.MapStructure;
using RelertSharp.MapStructure.Points;
using RelertSharp.MapStructure.Objects;
using RelertSharp.MapStructure.Logic;

namespace RelertSharp.MapStructure
{
    public static class Verification
    {
        private static readonly List<VerifyResultItem> result = new List<VerifyResultItem>();
        private static Map map { get; set; }


        public static List<VerifyResultItem> Verify(this Map _map)
        {
            result.Clear();
            map = _map;


            VerifyCelltag();
            VerifySingleCell();
            VerifyBuilding();
            VerifyForces();
            VerifyTaskforce();
            VerifyScript();
            VerifyTeam();
            VerifyCombatLogic();



            map = null;
            if (result.Count == 0) result.Add(new VerifyResultItem
            {
                Level = VerifyAlertLevel.Success,
                Message = string.Format("Your map {0} is clear for publish!", map.Info.MapName)
            });
            return result;
        }

        private static void VerifyCelltag()
        {
            foreach (CellTagItem cell in map.Celltags)
            {
                if (!map.Tags.HasId(cell.TagID))
                {
                    result.Add(new VerifyResultItem
                    {
                        Level = VerifyAlertLevel.Suggest,
                        VerifyType = VerifyType.CelltagIdNotFound,
                        Message = string.Format("Celltag at {0},{1} has ivalid tag id {2}", cell.X, cell.Y, cell.TagID),
                        Pos = cell
                    });
                }
            }
        }
        private static void VerifySingleCell()
        {
            HashSet<int> positions = new HashSet<int>();
            void func(IEnumerable<I2dLocateable> src, string type)
            {
                positions.Clear();
                foreach (I2dLocateable cell in src)
                {
                    if (positions.Contains(cell.Coord) && result.Any(x => x.VerifyType == VerifyType.SigleCellOverlap && x.Pos.Coord == cell.Coord))
                    {
                        result.Add(new VerifyResultItem
                        {
                            Level = VerifyAlertLevel.Warning,
                            Message = string.Format("{0},{1} has multiple {2} on it, only one will be saved!", cell.X, cell.Y, type),
                            Pos = cell,
                            VerifyType = VerifyType.SigleCellOverlap
                        });
                    }
                }
            }

            func(map.Celltags, "Celltags");
            func(map.Terrains, "Terrains");
            func(map.Waypoints, "Waypoints");
            func(map.Smudges, "Smudges");
        }
        private static void VerifyBuilding()
        {
            HashSet<int> positions = new HashSet<int>();
            foreach (StructureItem bud in map.Buildings)
            {
                foreach (I2dLocateable pos in new Foundation2D(bud))
                {
                    if (positions.Contains(pos.Coord) && result.Any(x => x.VerifyType == VerifyType.BuildingBasePosOverlap && x.Pos.Coord == pos.Coord))
                    {
                        result.Add(new VerifyResultItem
                        {
                            Level = VerifyAlertLevel.Critical,
                            Message = string.Format("Detect Multiple Structure {2} on {0},{1}! It may crash the game if not dealt properly.", 
                                bud.X, bud.Y, bud.RegName),
                            Pos = bud,
                            VerifyType = VerifyType.BuildingBasePosOverlap
                        });
                    }
                }
            }
        }
        private static void VerifyForces()
        {
            HashSet<I2dLocateable> positions = new HashSet<I2dLocateable>();
            void func(IEnumerable<I2dLocateable> src, string type)
            {
                positions.Clear();
                foreach (I2dLocateable item in src)
                {
                    if (positions.Contains(item))
                    {
                        result.Add(new VerifyResultItem
                        {
                            Level = VerifyAlertLevel.Suggest,
                            Message = string.Format("Multiple {0} on {1},{2}, suggest scattering if not intended.", type, item.X, item.Y),
                            Pos = item,
                            VerifyType = VerifyType.CombatObjectOverlap
                        });
                    }
                }
            }

            func(map.Units, "Units");
            func(map.Infantries, "Infantries");
            func(map.Aircrafts, "Aircrafts");
        }
        private static void VerifyTaskforce()
        {
            foreach (TaskforceItem tf in map.TaskForces)
            {
                if (tf.IsEmpty)
                {
                    result.Add(new VerifyResultItem
                    {
                        Level = VerifyAlertLevel.Suggest,
                        Message = string.Format("Empty Taskforce. Name: {0}, Id: {1}", tf.Name, tf.ID),
                        VerifyType = VerifyType.EmptyTaskforce,
                        IdNavigator = tf.ID,
                        LogicType = LogicType.Taskforce
                    });
                }
            }
        }
        private static void VerifyScript()
        {
            foreach (TeamScriptGroup sc in map.Scripts)
            {
                if (sc.IsEmpty)
                {
                    result.Add(new VerifyResultItem
                    {
                        Level = VerifyAlertLevel.Suggest,
                        Message = string.Format("Empty Script. Name: {0}, Id: {1}", sc.Name, sc.ID),
                        VerifyType = VerifyType.EmptyScript,
                        IdNavigator = sc.ID,
                        LogicType = LogicType.Script
                    });
                }
            }
        }
        private static void VerifyTeam()
        {
            foreach (TeamItem team in map.Teams)
            {
                if (!map.TaskForces.HasId(team.TaskforceID))
                {
                    result.Add(new VerifyResultItem
                    {
                        Level = VerifyAlertLevel.Warning,
                        Message = string.Format("Team {0}(1) has no available Taskforce and cannot operate properly.", team.Name, team.ID),
                        VerifyType = VerifyType.TeamInvalidTaskforce,
                        IdNavigator = team.ID,
                        LogicType = LogicType.Team
                    });
                }
                if (!map.Scripts.HasId(team.ScriptID))
                {
                    result.Add(new VerifyResultItem
                    {
                        Level = VerifyAlertLevel.Warning,
                        Message = string.Format("Team {0}(1) has no available Script and cannot operate properly.", team.Name, team.ID),
                        VerifyType = VerifyType.TeamInvalidScript,
                        IdNavigator = team.ID,
                        LogicType = LogicType.Team
                    });
                }
            }
        }
        private static void VerifyCombatLogic()
        {
            CombatObjectLowHealth();

        }
        #region Combat Logic
        private static void CombatObjectLowHealth()
        {
            void func(IEnumerable<ICombatObject> src, string type)
            {
                foreach (ICombatObject comb in src)
                {
                    int maxHealth = GlobalVar.GlobalRules[comb.RegName].ParseInt("Strength");
                    float current = maxHealth / 256f * comb.HealthPoint;
                    if (current <= 1f)
                    {
                        int lowest = Math.Min(512 / maxHealth, 256);
                        result.Add(new VerifyResultItem
                        {
                            Level = VerifyAlertLevel.Suggest,
                            Message = string.Format("{4} {0} at {1},{2} has health point lower than 1, will be destroy upon game begin. Suggested hp is {3}",
                                comb.RegName, comb.X, comb.Y, lowest, type),
                            Pos = comb,
                            VerifyType = VerifyType.CombatObjectLowHealth
                        });
                    }
                }
            }

            func(map.Buildings, "Building");
            func(map.Units, "Unit");
            func(map.Infantries, "Infantry");
            func(map.Aircrafts, "Aircraft");
        }
        #endregion
    }
}