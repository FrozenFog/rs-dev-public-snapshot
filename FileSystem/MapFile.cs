using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using relert_sharp.Common;
using relert_sharp.MapStructure;
using relert_sharp.Utils;

namespace relert_sharp.FileSystem
{
    public class MapFile : INIFile
    {
        public Map Map;
        public MapFile(string path) : base(path)
        {
            INIType = INIFileType.MapFile;
            Map = new Map(this);
            ClearAllIniEnt();
        }
        #region Public Methods - MapFile
        public void SaveMap()
        {
            IniDict = Map.IniResidue;
            DumpGeneralInfo();
            SaveIni(true);
        }
        #endregion
        #region Private Methods - MapFile
        private void DumpGeneralInfo()
        {
            IniDict["IsoMapPack5"] = new INIEntity("IsoMapPack5", Map.IsoMapPack5, 1);
            IniDict["OverlayDataPack"] = new INIEntity("OverlayDataPack", Map.OverlayDataPack, 1);
            IniDict["OverlayPack"] = new INIEntity("OverlayPack", Map.OverlayPack, 1);
            IniDict["PreviewPack"] = new INIEntity("PreviewPack", Map.PreviewPack, 1);
            INIEntity previewEnt = new INIEntity("Preview", "", "");
            if (!Map.PreviewSize.IsEmpty) previewEnt.AddPair(new INIPair("Size", Misc.FromRectangle(Map.PreviewSize), "", ""));
            IniDict["Preview"] = previewEnt;
            IniDict["Basic"] = Map.Info.Basic;
            IniDict["Map"] = Map.Info.Map;
            IniDict["SpecialFlags"] = Map.Info.SpecialFlags;
        }
        #endregion
    }
}
