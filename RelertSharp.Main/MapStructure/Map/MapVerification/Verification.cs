using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.Common;
using RelertSharp.MapStructure;
using RelertSharp.MapStructure.Points;
using RelertSharp.MapStructure.Objects;
using RelertSharp.MapStructure.Logic;
using RelertSharp.IniSystem;

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
            VerifyTriggerLogic();
            VerifyTagLogic();



            map = null;
            if (result.Count == 0) result.Add(new VerifyResultItem
            {
                Level = VerifyAlertLevel.Success,
                Message = string.Format("Your map {0} is clear for publish!", map.Info.MapName)
            });
            List<VerifyResultItem> rr = new List<VerifyResultItem>(result);
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
                        VerifyType = VerifyType.TaskforceEmpty,
                        IdNavigator = tf.ID,
                        LogicType = LogicType.Taskforce
                    });
                }
                else if (tf.Members.Count > 5)
                {
                    result.Add(new VerifyResultItem
                    {
                        Level = VerifyAlertLevel.Critical,
                        Message = string.Format("Taskforce {0}({1}) member num larger tham 5! This taskforce will surely crash the game.",
                            tf.ID, tf.Name),
                        IdNavigator = tf.ID,
                        LogicType = LogicType.Taskforce,
                        VerifyType = VerifyType.TaskforceOverflow
                    });
                }
                else if (tf.Members.Distinct().Count() != tf.Members.Count)
                {
                    result.Add(new VerifyResultItem
                    {
                        Level = VerifyAlertLevel.Critical,
                        Message = string.Format("Taskforce {0}({1}) has repeated member! This taskforce will surely crash the game.",
                            tf.ID, tf.Name),
                        VerifyType = VerifyType.TaskforceMemberRepeated,
                        LogicType = LogicType.Taskforce,
                        IdNavigator = tf.ID
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
                        Message = string.Format("Team {0}(1) has no available Taskforce and cannot operate properly.", team.ID, team.Name),
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
                        Message = string.Format("Team {0}(1) has no available Script and cannot operate properly.", team.ID, team.Name),
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
                    int maxHealth = GlobalVar.GlobalRules[comb.RegName].ParseInt("Strength", 1);
                    float current = maxHealth / 256f * comb.HealthPoint;
                    if (current <= 1f)
                    {
                        int lowest = Math.Min(512 / maxHealth, 256);
                        result.Add(new VerifyResultItem
                        {
                            Level = VerifyAlertLevel.Suggest,
                            Message = string.Format("{4} {0} at {1},{2} has health point lower than 1, will be destroyed upon game just begin. Suggested hp is {3}",
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
        private static void VerifyTriggerLogic()
        {
            DescriptCollection descs = new DescriptCollection();

            foreach (TriggerItem trg in map.Triggers)
            {
                // orphan trigger
                if (!map.Tags.Any(x => x.AssoTrigger == trg.ID))
                {
                    result.Add(new VerifyResultItem
                    {
                        Level = VerifyAlertLevel.Warning,
                        VerifyType = VerifyType.TriggerHasNoTagAsso,
                        IdNavigator = trg.ID,
                        LogicType = LogicType.Trigger,
                        Message = string.Format("Trigger {0}({1}) has no tag linked with, this trigger will never activate during the game.",
                            trg.ID, trg.Name)
                    });
                }

                // empty action
                if (trg.Actions.Count() == 0)
                {
                    result.Add(new VerifyResultItem
                    {
                        Level = VerifyAlertLevel.Suggest,
                        VerifyType = VerifyType.TriggerHasNoAction,
                        IdNavigator = trg.ID,
                        LogicType = LogicType.Trigger,
                        Message = string.Format("Trigger {0}({1}) has no action, consider removing it.",
                            trg.ID, trg.Name)
                    });
                }

                // check action
                else
                {
                    foreach (LogicItem lg in trg.Actions)
                    {
                        if (descs.Action(lg.ID) is TriggerDescription desc)
                        {
                            foreach (TriggerParam param in desc.Parameters)
                            {
                                if (param.Type == TriggerParam.ParamType.SelectableString)
                                {
                                    IEnumerable<TechnoPair> data = map.GetComboCollections(param);
                                    if (!data.Any(x => x.Index.ToLower() == param.GetParameter(lg.Parameters).ToString().ToLower()))
                                    {
                                        result.Add(new VerifyResultItem
                                        {
                                            Level = VerifyAlertLevel.Warning,
                                            VerifyType = VerifyType.TriggerParameterInvalid,
                                            IdNavigator = trg.ID,
                                            LogicType = LogicType.Trigger,
                                            Message = string.Format("Trigger {0}({1}) action{2}(Action-{3}) has unrecognizeable parameter value, it may cause severe crash to the game. It's adviced to double-check your parameter.",
                                                trg.ID, trg.Name, lg.idx, lg.ID)
                                        });
                                    }
                                }
                            }
                        }
                    }
                }

                // empty event
                if (trg.Events.Count() == 0)
                {
                    result.Add(new VerifyResultItem
                    {
                        Level = VerifyAlertLevel.Warning,
                        VerifyType = VerifyType.TriggerHasNoEvent,
                        IdNavigator = trg.ID,
                        LogicType = LogicType.Trigger,
                        Message = string.Format("Trigger {0}({1}) has no event, this trigger will never be fired.",
                            trg.ID, trg.Name)
                    });
                }
                // check evnt
                else
                {
                    foreach (LogicItem lg in trg.Events)
                    {
                        if (descs.Event(lg.ID) is TriggerDescription desc)
                        {
                            foreach (TriggerParam param in desc.Parameters)
                            {
                                if (param.Type == TriggerParam.ParamType.SelectableString)
                                {
                                    IEnumerable<TechnoPair> data = map.GetComboCollections(param);
                                    if (!data.Any(x => x.Index == param.GetParameter(lg.Parameters).ToString()))
                                    {
                                        result.Add(new VerifyResultItem
                                        {
                                            Level = VerifyAlertLevel.Warning,
                                            VerifyType = VerifyType.TriggerParameterInvalid,
                                            IdNavigator = trg.ID,
                                            LogicType = LogicType.Trigger,
                                            Message = string.Format("Trigger {0}({1}) event{2}(Event-{3}) has unrecognizeable parameter value, it may cause severe crash to the game. It's adviced to double-check your parameter.",
                                                trg.ID, trg.Name, lg.idx, lg.ID)
                                        });
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        private static void VerifyTagLogic()
        {
            foreach (TagItem tag in map.Tags)
            {
                // orphan tag
                if (!map.Triggers.HasId(tag.AssoTrigger))
                {
                    result.Add(new VerifyResultItem
                    {
                        Level = VerifyAlertLevel.Suggest,
                        VerifyType = VerifyType.TagHasNoTrigger,
                        IdNavigator = tag.ID,
                        LogicType = LogicType.Tag,
                        Message = string.Format("Tag {0}({1}) linked with an invalid trigger, consider removing it.",
                            tag.ID, tag.Name)
                    });
                }
            }
        }
    }
}
