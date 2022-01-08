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
        public void DumpTriggerData(out INIEntity trigger, out INIEntity events, out INIEntity actions, out INIEntity tags)
        {
            trigger = new INIEntity("Triggers");
            events = new INIEntity("Events");
            actions = new INIEntity("Actions");
            tags = Tags.SaveAsIni();
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
