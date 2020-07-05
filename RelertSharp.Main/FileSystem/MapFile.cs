using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.IniSystem;
using RelertSharp.Common;
using RelertSharp.MapStructure;
using RelertSharp.Utils;

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
        #endregion


        #region Public Methods - MapFile
        public void SaveMap(string savingPath)
        {
            IniDict = Map.IniResidue;
            Map.CompressTile();
            Map.CompressOverlay();
            DumpGeneralInfo();
            DumpObjects();
            SaveIni(savingPath, true);
        }

        private void DumpObjects() {
            var entity = new INIEntity("Structures", "", "");

            string handleSequencedList(List<string> list) {
                string items = list[0];
                list.RemoveAt(0);
                foreach (var item in list) {
                    items += "," + item;
                }
                return items;
            }

            //buildings :
            foreach (var building in Map.Buildings) {
                var idx = 0;
                List<string> values = new List<string>{building.OwnerHouse,
                              building.ID,
                              building.HealthPoint.ToString(),
                              building.X.ToString(),
                              building.Y.ToString(),
                              building.Rotation.ToString(),
                              building.TaggedTrigger,
                              building.AISellable.ToString(),
                              building.BuildingOnline.ToString(),
                              building.UpgradeNum.ToString(),
                              building.SpotlightType.ToString(),
                              building.Upgrade1,
                              building.Upgrade2,
                              building.Upgrade3,
                              building.AIRepairable.ToString(),
                              "0",//ShowRealName
                };
                
                entity.AddPair(new INIPair(idx.ToString(), handleSequencedList(values)));
                ++idx;
            }

            //units

            //infantries

            //aircrafts
            
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
