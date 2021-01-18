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
        private static Map Map { get; set; }


        public static List<VerifyResultItem> Verify(this Map _map)
        {
            result.Clear();
            Map = _map;


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



            Map = null;
            if (result.Count == 0) result.Add(new VerifyResultItem
            {
                Level = VerifyAlertLevel.Success,
                Message = string.Format("Your map {0} is clear for publish!", Map.Info.MapName)
            });
            return result;
        }

        private static void VerifyCelltag()
        {
            foreach (CellTagItem cell in Map.Celltags)
            {
                if (!Map.Tags.HasId(cell.TagID))
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

            func(Map.Celltags, "Celltags");
            func(Map.Terrains, "Terrains");
            func(Map.Waypoints, "Waypoints");
            func(Map.Smudges, "Smudges");
        }
        private static void VerifyBuilding()
        {
            HashSet<int> positions = new HashSet<int>();
            foreach (StructureItem bud in Map.Buildings)
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

            func(Map.Units, "Units");
            func(Map.Infantries, "Infantries");
            func(Map.Aircrafts, "Aircrafts");
        }
        private static void VerifyTaskforce()
        {
            foreach (TaskforceItem tf in Map.Taskforces)
            {
                if (tf.IsEmpty)
                {
                    result.Add(new VerifyResultItem
                    {
                        Level = VerifyAlertLevel.Suggest,
                        Message = string.Format("Empty Taskforce. Name: {0}, Id: {1}", tf.Name, tf.Id),
                        VerifyType = VerifyType.TaskforceEmpty,
                        IdNavigator = tf.Id,
                        LogicType = LogicType.Taskforce
                    });
                }
                else if (tf.Members.Count > 5)
                {
                    result.Add(new VerifyResultItem
                    {
                        Level = VerifyAlertLevel.Critical,
                        Message = string.Format("Taskforce {0}({1}) member num larger than 5! This taskforce will surely crash the game.",
                            tf.Id, tf.Name),
                        IdNavigator = tf.Id,
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
                            tf.Id, tf.Name),
                        VerifyType = VerifyType.TaskforceMemberRepeated,
                        LogicType = LogicType.Taskforce,
                        IdNavigator = tf.Id
                    });
                }
            }
        }
        private static void VerifyScript()
        {
            foreach (TeamScriptGroup sc in Map.Scripts)
            {
                EmptyScript(sc);
            }
        }
        private static void VerifyTeam()
        {
            foreach (TeamItem team in Map.Teams)
            {
                InvalidTeamComponent(team);
            }
        }
        private static void VerifyCombatLogic()
        {
            void func(IEnumerable<ICombatObject> src, string type)
            {
                foreach (ICombatObject comb in src)
                {
                    CombatObjectLowHealth(comb, type);
                    CombatObjectInvalid(comb, type);
                }
            }

            func(Map.Buildings, "Building");
            func(Map.Units, "Unit");
            func(Map.Infantries, "Infantry");
            func(Map.Aircrafts, "Aircraft");
        }
        private static void VerifyTriggerLogic()
        {
            DescriptCollection descs = new DescriptCollection();

            foreach (TriggerItem trg in Map.Triggers)
            {
                OrphanTrigger(trg);
                EmptyLogic(trg);
                InvalidParam(trg, descs);
                TriggerTooLong(trg);
            }
        }
        #region Team Logic
        private static void InvalidTeamComponent(TeamItem team)
        {
            if (!Map.Taskforces.HasId(team.TaskforceID))
            {
                result.Add(new VerifyResultItem
                {
                    Level = VerifyAlertLevel.Warning,
                    Message = string.Format("Team {0}(1) has no available Taskforce and cannot operate properly.", team.Id, team.Name),
                    VerifyType = VerifyType.TeamInvalidTaskforce,
                    IdNavigator = team.Id,
                    LogicType = LogicType.Team
                });
            }
            if (!Map.Scripts.HasId(team.ScriptID))
            {
                result.Add(new VerifyResultItem
                {
                    Level = VerifyAlertLevel.Warning,
                    Message = string.Format("Team {0}(1) has no available Script and cannot operate properly.", team.Id, team.Name),
                    VerifyType = VerifyType.TeamInvalidScript,
                    IdNavigator = team.Id,
                    LogicType = LogicType.Team
                });
            }
        }
        #endregion
        #region Combat Logic
        private static void CombatObjectLowHealth(ICombatObject comb, string type)
        {
            int maxHealth = GlobalVar.GlobalRules[comb.RegName].ParseInt("Strength");
            float current = maxHealth / 256f * comb.HealthPoint;
            if (current <= 1f && maxHealth > 0)
            {
                int lowest = (int)Math.Min(Math.Ceiling(512f / maxHealth), 256f);
                result.Add(new VerifyResultItem
                {
                    Level = VerifyAlertLevel.Suggest,
                    Message = string.Format("{4} {0} at {1},{2} has health point lower than 1, will be destroyed upon game just begin. Suggested hp is {3} (now {5})",
                        comb.RegName, comb.X, comb.Y, lowest, type, comb.HealthPoint),
                    Pos = comb,
                    VerifyType = VerifyType.CombatObjectLowHealth
                });
            }
        }
        private static void CombatObjectInvalid(ICombatObject comb, string type)
        {
            if (!GlobalVar.GlobalRules.HasIniEntAll(comb.RegName))
            {
                result.Add(new VerifyResultItem
                {
                    Level = VerifyAlertLevel.Warning,
                    Message = string.Format("{3} {0} at {1}, {2} is invalid / unregisted, please specify this {3} in map rules or global rules",
                        comb.RegName, comb.X, comb.Y, type),
                    Pos = comb,
                    VerifyType = VerifyType.CombatObjectInvalid
                });
            }
        }
        #endregion
        #region Trigger Logic
        private static void TriggerTooLong(TriggerItem trg)
        {
            int baseLength = trg.Id.Length + 1;
            if (trg.Events.GetSaveData().Length + baseLength > Constant.IniMaxLineLength)
            {
                result.Add(new VerifyResultItem
                {
                    Level = VerifyAlertLevel.Critical,
                    VerifyType = VerifyType.TriggerEventOverflow,
                    IdNavigator = trg.Id,
                    LogicType = LogicType.Trigger,
                    Message = string.Format("Trigger {0}({1})'s event has overflowed! Please split the trigger event into smaller group, otherwise it will surely crash the game!",
                        trg.Id, trg.Name)
                });
            }
            if (trg.Actions.GetSaveData().Length + baseLength > Constant.IniMaxLineLength)
            {
                result.Add(new VerifyResultItem
                {
                    Level = VerifyAlertLevel.Critical,
                    VerifyType = VerifyType.TriggerActionOverflow,
                    IdNavigator = trg.Id,
                    LogicType = LogicType.Trigger,
                    Message = string.Format("Trigger {0}({1})'s action has overflowed! Please split the trigger action into smaller group, otherwise it will surely crash the game!",
                        trg.Id, trg.Name)
                });
            }
        }
        private static void OrphanTrigger(TriggerItem trg)
        {
            if (!Map.Tags.Any(x => x.AssoTrigger == trg.Id))
            {
                result.Add(new VerifyResultItem
                {
                    Level = VerifyAlertLevel.Warning,
                    VerifyType = VerifyType.TriggerHasNoTagAsso,
                    IdNavigator = trg.Id,
                    LogicType = LogicType.Trigger,
                    Message = string.Format("Trigger {0}({1}) has no tag linked with, this trigger will never activate during the game.",
                        trg.Id, trg.Name)
                });
            }
        }
        private static void EmptyLogic(TriggerItem trg)
        {
            if (trg.Actions.Count() == 0)
            {
                result.Add(new VerifyResultItem
                {
                    Level = VerifyAlertLevel.Suggest,
                    VerifyType = VerifyType.TriggerHasNoAction,
                    IdNavigator = trg.Id,
                    LogicType = LogicType.Trigger,
                    Message = string.Format("Trigger {0}({1}) has no action, consider removing it.",
                        trg.Id, trg.Name)
                });
            }
            if (trg.Events.Count() == 0)
            {
                result.Add(new VerifyResultItem
                {
                    Level = VerifyAlertLevel.Warning,
                    VerifyType = VerifyType.TriggerHasNoEvent,
                    IdNavigator = trg.Id,
                    LogicType = LogicType.Trigger,
                    Message = string.Format("Trigger {0}({1}) has no event, this trigger will never be fired.",
                        trg.Id, trg.Name)
                });
            }
        }
        private static void InvalidParam(TriggerItem trg, DescriptCollection descs)
        {
            foreach (LogicItem lg in trg.Actions)
            {
                if (descs.Action(lg.ID) is TriggerDescription desc)
                {
                    foreach (TriggerParam param in desc.Parameters)
                    {
                        if (param.Type == TriggerParam.ParamType.SelectableString)
                        {
                            IEnumerable<TechnoPair> data = Map.GetComboCollections(param);
                            if (!data.Any(x => x.Index.ToLower() == param.GetParameter(lg.Parameters).ToString().ToLower()))
                            {
                                result.Add(new VerifyResultItem
                                {
                                    Level = VerifyAlertLevel.Warning,
                                    VerifyType = VerifyType.TriggerParameterInvalid,
                                    IdNavigator = trg.Id,
                                    LogicType = LogicType.Trigger,
                                    Message = string.Format("Trigger {0}({1}) action{2}(Action-{3}) has unrecognizeable parameter value, it may cause severe crash to the game. It's adviced to double-check your parameter.",
                                        trg.Id, trg.Name, lg.idx, lg.ID)
                                });
                            }
                        }
                    }
                }
            }
            foreach (LogicItem lg in trg.Events)
            {
                if (descs.Event(lg.ID) is TriggerDescription desc)
                {
                    foreach (TriggerParam param in desc.Parameters)
                    {
                        if (param.Type == TriggerParam.ParamType.SelectableString)
                        {
                            IEnumerable<TechnoPair> data = Map.GetComboCollections(param);
                            if (!data.Any(x => x.Index == param.GetParameter(lg.Parameters).ToString()))
                            {
                                result.Add(new VerifyResultItem
                                {
                                    Level = VerifyAlertLevel.Warning,
                                    VerifyType = VerifyType.TriggerParameterInvalid,
                                    IdNavigator = trg.Id,
                                    LogicType = LogicType.Trigger,
                                    Message = string.Format("Trigger {0}({1}) event{2}(Event-{3}) has unrecognizeable parameter value, it may cause severe crash to the game. It's adviced to double-check your parameter.",
                                        trg.Id, trg.Name, lg.idx, lg.ID)
                                });
                            }
                        }
                    }
                }
            }
        }
        #endregion
        #region Taskforce & Script
        private static void EmptyScript(TeamScriptGroup script)
        {
            if (script.IsEmpty)
            {
                result.Add(new VerifyResultItem
                {
                    Level = VerifyAlertLevel.Suggest,
                    Message = string.Format("Empty Script. Name: {0}, Id: {1}", script.Name, script.Id),
                    VerifyType = VerifyType.ScriptEmpty,
                    IdNavigator = script.Id,
                    LogicType = LogicType.Script
                });
            }
        }
        #endregion
        private static void VerifyTagLogic()
        {
            foreach (TagItem tag in Map.Tags)
            {
                // orphan tag
                if (!Map.Triggers.HasId(tag.AssoTrigger))
                {
                    result.Add(new VerifyResultItem
                    {
                        Level = VerifyAlertLevel.Suggest,
                        VerifyType = VerifyType.TagHasNoTrigger,
                        IdNavigator = tag.Id,
                        LogicType = LogicType.Tag,
                        Message = string.Format("Tag {0}({1}) linked with an invalid trigger, consider removing it.",
                            tag.Id, tag.Name)
                    });
                }
            }
        }
    }
}
