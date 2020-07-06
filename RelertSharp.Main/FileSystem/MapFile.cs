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

        private void DumpObjects()
        {
            //buildings :
            using(var structureEntity=new INIEntity("Structures", "", "")) 
            {
                int idx = 0;
                foreach (var building in Map.Buildings)
                {
                    structureEntity.AddPair
                        (
                            new INIPair
                            (
                                idx.ToString(),
                                Misc.ParseObjectsToString
                                (
                                    building.OwnerHouse,
                                    building.ID,
                                    building.HealthPoint,
                                    building.X,
                                    building.Y,
                                    building.Rotation,
                                    building.TaggedTrigger,
                                    building.AISellable,
                                    building.UpgradeNum,
                                    building.SpotlightType,
                                    building.Upgrade1,
                                    building.Upgrade2,
                                    building.Upgrade3,
                                    building.AISellable,
                                "0"
                                )
                            )
                    );
                    ++idx;
                }
                IniDict["Structures"] = structureEntity;
            }

            //units
            using (var unitEntity = new INIEntity("Units", "", ""))
            {
                int idx = 0;
                foreach (var unit in Map.Units)
                {
                    unitEntity.AddPair
                        (
                            new INIPair
                                (
                                    idx.ToString(),
                                    Misc.ParseObjectsToString
                                    (
                                        unit.OwnerHouse,
                                        unit.RegName,
                                        unit.HealthPoint,
                                        unit.X,
                                        unit.Y,
                                        unit.Rotation,
                                        unit.Status,
                                        unit.TaggedTrigger,
                                        unit.VeterancyPercentage,
                                        unit.Group,
                                        unit.IsAboveGround,
                                        unit.FollowsIndex,
                                        unit.AutoNORecruitType,
                                        unit.AutoYESRecruitType
                                    )
                                )
                    );
                    ++idx;
                }
                IniDict["Units"] = unitEntity;
            }

            //infantries
            using (var infantryEntity = new INIEntity("Infantry", "", ""))
            {
                int idx = 0;
                foreach (var infantry in Map.Infantries)
                {
                    infantryEntity.AddPair
                        (
                            new INIPair
                                (
                                    idx.ToString(),
                                    Misc.ParseObjectsToString
                                    (
                                        infantry.OwnerHouse,
                                        infantry.RegName,
                                        infantry.HealthPoint,
                                        infantry.X,
                                        infantry.Y,
                                        infantry.SubCells,
                                        infantry.Status,
                                        infantry.Rotation,
                                        infantry.TaggedTrigger,
                                        infantry.VeterancyPercentage,
                                        infantry.Group,
                                        infantry.IsAboveGround,
                                        infantry.AutoNORecruitType,
                                        infantry.AutoYESRecruitType
                                    )
                                )
                    );
                    ++idx;
                }
                IniDict["Infantry"] = infantryEntity;
            }

            //aircrafts
            using (var aircraftEntity = new INIEntity("Aircraft", "", ""))
            {
                int idx = 0;
                foreach (var aircraft in Map.Aircrafts)
                {
                    aircraftEntity.AddPair
                        (
                            new INIPair
                                (
                                    idx.ToString(),
                                    Misc.ParseObjectsToString
                                    (
                                        aircraft.OwnerHouse,
                                        aircraft.RegName,
                                        aircraft.HealthPoint,
                                        aircraft.X,
                                        aircraft.Y,
                                        aircraft.Rotation,
                                        aircraft.Status,
                                        aircraft.TaggedTrigger,
                                        aircraft.VeterancyPercentage,
                                        aircraft.Group,
                                        aircraft.AutoNORecruitType,
                                        aircraft.AutoYESRecruitType
                                    )
                                )
                    );
                    ++idx;
                }
                IniDict["Aircraft"] = aircraftEntity;
            }
        }
        #endregion
    }
}
