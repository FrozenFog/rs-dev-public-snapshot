using RelertSharp.Common;
using RelertSharp.IniSystem;
using RelertSharp.MapStructure.Logic;
using RelertSharp.MapStructure.Objects;
using RelertSharp.MapStructure.Points;
using System;
using System.Collections.Generic;

namespace RelertSharp.MapStructure
{
    public partial class Map
    {
        public INIEntity DumpBuildingData()
        {
            INIEntity ent = new INIEntity("Structures");
            foreach (StructureItem bud in Buildings)
            {
                ent.AddPair(new INIPair(bud.Id, bud.SaveData.JoinBy()));
            }
            return ent;
        }
        public INIEntity DumpInfantryData()
        {
            INIEntity ent = new INIEntity("Infantry");
            foreach (InfantryItem inf in Infantries)
            {
                ent.AddPair(new INIPair(inf.Id, inf.SaveData.JoinBy()));
            }
            return ent;
        }
        public INIEntity DumpUnitData()
        {
            INIEntity ent = new INIEntity("Units");
            foreach (UnitItem u in Units)
            {
                ent.AddPair(new INIPair(u.Id, u.SaveData.JoinBy()));
            }
            return ent;
        }
        public INIEntity DumpAircraftData()
        {
            INIEntity ent = new INIEntity("Aircraft");
            foreach (AircraftItem a in Aircrafts)
            {
                ent.AddPair(new INIPair(a.Id, a.SaveData.JoinBy()));
            }
            return ent;
        }
        public INIEntity DumpTerrainData()
        {
            INIEntity ent = new INIEntity("Terrain");
            foreach (TerrainItem terr in Terrains)
            {
                ent.AddPair(new INIPair(terr.CoordString, terr.RegName));
            }
            return ent;
        }
        public INIEntity DumpLightSourceData()
        {
            INIEntity ent = new INIEntity(Constant.MapStructure.CustomComponents.LightsourceTitle);
            foreach (LightSource l in LightSources)
            {
                ent.AddPair(l.SaveToPair());
            }
            return ent;
        }
        public INIEntity DumpCelltagData()
        {
            INIEntity ent = new INIEntity("CellTags");
            foreach (CellTagItem cell in Celltags)
            {
                ent.AddPair(new INIPair(cell.CoordString, cell.TagID));
            }
            return ent;
        }
        public INIEntity DumpSmudgeData()
        {
            INIEntity ent = new INIEntity("Smudge");
            foreach (SmudgeItem smg in Smudges)
            {
                ent.AddPair(new INIPair(smg.Id, smg.SaveData.JoinBy()));
            }
            return ent;
        }
        public INIEntity DumpWaypointData()
        {
            INIEntity ent = new INIEntity("Waypoints");
            foreach (WaypointItem wp in Waypoints)
            {
                ent.AddPair(new INIPair(wp.Num, wp.CoordString));
            }
            return ent;
        }
        public INIEntity DumpLocalVar()
        {
            INIEntity ent = new INIEntity("VariableNames");
            foreach (LocalVarItem v in LocalVariables)
            {
                ent.AddPair(new INIPair(v.Id, v.Name + (v.InitState ? ",1" : ",0")));
            }
            return ent;
        }
        public void DumpTriggerData(out INIEntity trigger, out INIEntity events, out INIEntity actions, out INIEntity tags)
        {
            trigger = new INIEntity("Triggers");
            events = new INIEntity("Events");
            actions = new INIEntity("Actions");
            tags = new INIEntity("Tags");
            foreach (TriggerItem trg in Triggers)
            {
                trigger.AddPair(new INIPair(trg.Id, trg.SaveData.JoinBy()));
                events.AddPair(new INIPair(trg.Id, trg.Events.GetSaveData()));
                actions.AddPair(new INIPair(trg.Id, trg.Actions.GetSaveData()));
            }
            foreach (TagItem tag in Tags)
            {
                if (tag.Id == Constant.ITEM_NONE) continue;
                tags.AddPair(new INIPair(tag.Id, tag.SaveData.JoinBy()));
            }
        }
        public void DumpAiTriggerData(out INIEntity aitypes, out INIEntity aienable)
        {
            aitypes = new INIEntity("AITriggerTypes");
            aienable = new INIEntity("AITriggerTypesEnable");
            foreach (AITriggerItem t in AiTriggers)
            {
                aitypes.AddPair(new INIPair(t.Id, t.GetSaveData()));
            }
            foreach (string key in AiTriggers.GlobalEnables.Keys)
            {
                aienable.AddPair(new INIPair(key, AiTriggers.GlobalEnables[key] ? "yes" : "no"));
            }
        }
        private void DumpRegularTocItem<T>(string tocName, IEnumerable<T> srcReferance, Func<T, INIEntity> funcForT, Func<T, string> funcID, out INIEntity toc, out INIEntity[] result) where T : IIndexableItem
        {
            toc = new INIEntity(tocName);
            List<INIEntity> data = new List<INIEntity>();
            try
            {
                int i = 0;
                foreach (T item in srcReferance)
                {
                    if (item.Id == Constant.ITEM_NONE) continue;
                    toc.AddPair(i++.ToString(), funcID.Invoke(item));
                    data.Add(funcForT(item));
                }
                result = data.ToArray();
            }
            catch
            {
                Common.GlobalVar.Log.Critical("Save error in {0}, some item will not save!", tocName);
                result = data.ToArray();
            }
        }
        public void DumpScriptData(out INIEntity toc, out INIEntity[] allScripts)
        {
            DumpRegularTocItem("ScriptTypes", Scripts, x => x.GetSaveData(), x => x.Id, out toc, out allScripts);
        }
        public void DumpTaskforce(out INIEntity toc, out INIEntity[] allTaskforce)
        {
            DumpRegularTocItem("TaskForces", Taskforces, x => x.GetSaveData(), x => x.Id, out toc, out allTaskforce);
        }
        public void DumpTeam(out INIEntity toc, out INIEntity[] allTeams)
        {
            DumpRegularTocItem("TeamTypes", Teams, x => x.GetSaveData(), x => x.Id, out toc, out allTeams);
        }
        public void DumpHouse(out INIEntity toc, out INIEntity[] allHouses)
        {
            DumpRegularTocItem("Houses", Houses, x => x.GetSaveData(), x => x.Name, out toc, out allHouses);
        }
        public void DumpCountries(out INIEntity toc, out INIEntity[] allCountries)
        {
            DumpRegularTocItem("Countries", Countries, x => x.GetSaveData(), x => x.Name, out toc, out allCountries);
        }
    }
}
