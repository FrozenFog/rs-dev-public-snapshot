using RelertSharp.Common;
using RelertSharp.IniSystem;
using RelertSharp.MapStructure;
using RelertSharp.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.IO;
using static RelertSharp.Common.Constant.MapStructure;

namespace RelertSharp.FileSystem
{
    public class MapFile : INIFile
    {
        public Map Map;


        #region Ctor - MapFile
        public MapFile(string path) : base(path)
        {
            INIType = INIFileType.MapFile;
            Map = new Map(this);
            ClearAllIniEnt();
        }
        public MapFile(IMapCreationConfig cfg)
        {
            INIType = INIFileType.MapFile;
            ClearAllIniEnt();
            Map = new Map(cfg);
        }
        #endregion


        #region Public Methods - MapFile
        //public void SaveMap(string savingPath)
        //{
        //    IniDict = Map.IniResidue;
        //    Map.DumpOverlayFromTile();
        //    Map.CompressTile();
        //    Map.CompressOverlay();
        //    DumpGeneralInfo(IniDict);
        //    DumpMapObjects(IniDict);
        //    DumpLogics(IniDict);
        //    DumpCustomComponents(IniDict);
        //    SaveIni(savingPath, true);
        //}
        public int GetMapChecksum()
        {
            unchecked
            {
                int hash = Constant.BASE_HASH;
                foreach (var obj in Map.AllChecksum) hash = hash * 486187739 + obj.GetChecksum();
                hash = hash * 486187739 + Map.IniResidue.GetHashCode();
                hash = hash * 486187739 + Map.Info.GetChecksum();
                hash = hash * 486187739 + Map.LightningCollection.GetChecksum();
                hash = hash * 486187739 + Map.Rank.GetChecksum();
                hash = hash * 486187739 + Map.Header.GetChecksum();
                return hash;
            }
        }
        public void SaveMapAs(string path, bool bypassPathSave = false)
        {
            INIFile f = new INIFile(path, true);
            f.AddEnt(Map.IniResidue.Values);
            Map.DumpOverlayFromTile();
            Map.CompressTile();
            Map.CompressOverlay();
            DumpSerializeable(f.IniDict);
            DumpGeneralInfo(f.IniDict);
            DumpLogics(f.IniDict);
            DumpCustomComponents(f.IniDict);
            f.SaveIni(path, true);
            if (GlobalVar.Log.HasCritical) GlobalVar.Log.ShowCritical();
            if (!bypassPathSave) FilePath = path;
            FileName = Path.GetFileName(path);
        }
        #endregion


        #region Private Methods - MapFile
        private void DumpCustomComponents(Dictionary<string, INIEntity> dest)
        {
            dest[ENT_RS_LIGHT] = Map.LightSources.SaveAsIni();
            if (!dest.TryGetValue("BuildingTypes", out INIEntity toc))
            {
                toc = new INIEntity("BuildingTypes");
                dest["BuildingTypes"] = toc;
            }
            Map.LightSources.Compile(out Dictionary<string, INIEntity> lightposts, toc, out var entities);
            dest.JoinWith(lightposts);
            int i = dest["Structures"].GetMaxIndex() + 1;
            foreach (var bud in entities)
            {
                dest["Structures"].AddPair(new INIPair(i++.ToString(), bud.SaveData.JoinBy()));
            }
        }
        private void DumpSerializeable(Dictionary<string, INIEntity> dest)
        {
            foreach (IIniEntitySerializable serializeable in Map.AllSerializeable)
            {
                string entName = serializeable.GetEntName();
                dest[entName] = serializeable.SaveAsIni();
            }
        }
        private void DumpLogics(Dictionary<string, INIEntity> dest)
        {
            Map.DumpTriggerData(out INIEntity trigger, out INIEntity events, out INIEntity actions, out INIEntity tags);
            INIEntity aiType = Map.AiTriggers.SaveAsIni();
            INIEntity aiEnable = Map.AiTriggers.DumpEnables();
            Map.DumpTeam(out INIEntity team, out INIEntity[] teams);
            Map.DumpScriptData(out INIEntity script, out INIEntity[] scripts);
            Map.DumpHouse(out INIEntity house, out INIEntity[] houses);
            Map.DumpCountries(out INIEntity con, out INIEntity[] cons);
            Map.DumpTaskforce(out INIEntity task, out INIEntity[] tasks);
            List<INIEntity> result = new List<INIEntity>
            {
                aiType, aiEnable, trigger, events, actions, tags,
                team, script, task, house, con
            };
            result.AddRange(teams.Concat(scripts).Concat(houses).Concat(cons).Concat(tasks));
            foreach (INIEntity ent in result) dest[ent.Name] = ent;
        }
        private void DumpGeneralInfo(Dictionary<string, INIEntity> dest)
        {
            dest["IsoMapPack5"] = new INIEntity("IsoMapPack5", Map.IsoMapPack5, 1);
            dest["OverlayDataPack"] = new INIEntity("OverlayDataPack", Map.OverlayDataPack, 1);
            dest["OverlayPack"] = new INIEntity("OverlayPack", Map.OverlayPack, 1);
            dest["PreviewPack"] = new INIEntity("PreviewPack", Map.PreviewPack, 1);
            INIEntity previewEnt = new INIEntity("Preview");
            if (!Map.PreviewSize.IsEmpty) previewEnt.AddPair("Size", Map.PreviewSize.ParseString());
            dest["Preview"] = previewEnt;
            dest["Basic"] = Map.Info.GetBasicEnt();
            dest["Map"] = Map.Info.GetMapEnt();
            dest["SpecialFlags"] = Map.Info.SpecialFlags;
            dest["Lighting"] = Map.LightningCollection.GetSaveData();
            dest[ENT_RANK] = Map.Rank?.GetSaveData();
        }
        #endregion
    }
}
