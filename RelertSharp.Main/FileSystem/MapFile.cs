using RelertSharp.Common;
using RelertSharp.IniSystem;
using RelertSharp.MapStructure;
using RelertSharp.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

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
            Map.DumpOverlayFromTile();
            Map.CompressTile();
            Map.CompressOverlay();
            DumpGeneralInfo(IniDict);
            DumpMapObjects(IniDict);
            DumpLogics(IniDict);
            DumpCustomComponents(IniDict);
            SaveIni(savingPath, true);
        }
        public void SaveMapAs(string savePath, string filename)
        {
            INIFile f = new INIFile(filename, true);
            f.AddEnt(Map.IniResidue.Values);
            Map.DumpOverlayFromTile();
            Map.CompressTile();
            Map.CompressOverlay();
            DumpGeneralInfo(f.IniDict);
            DumpMapObjects(f.IniDict);
            DumpLogics(f.IniDict);
            DumpCustomComponents(f.IniDict);
            f.SaveIni(savePath + filename, true);
            if (GlobalVar.Log.HasCritical) GlobalVar.Log.ShowCritical();
        }
        #endregion


        #region Private Methods - MapFile
        private void DumpCustomComponents(Dictionary<string, INIEntity> dest)
        {
            dest[Constant.MapStructure.CustomComponents.LightsourceTitle] = Map.DumpLightSourceData();
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
        private void DumpMapObjects(Dictionary<string, INIEntity> dest)
        {
            dest["Structures"] = Map.DumpBuildingData();
            dest["Infantry"] = Map.DumpInfantryData();
            dest["Units"] = Map.DumpUnitData();
            dest["Aircraft"] = Map.DumpAircraftData();
            dest["Terrain"] = Map.DumpTerrainData();
            dest["Smudge"] = Map.DumpSmudgeData();
            dest["CellTags"] = Map.DumpCelltagData();
            dest["Waypoints"] = Map.DumpWaypointData();
            dest["VariableNames"] = Map.DumpLocalVar();
        }
        private void DumpLogics(Dictionary<string, INIEntity> dest)
        {
            Map.DumpTriggerData(out INIEntity trigger, out INIEntity events, out INIEntity actions, out INIEntity tags);
            Map.DumpAiTriggerData(out INIEntity type, out INIEntity enable);
            Map.DumpTeam(out INIEntity team, out INIEntity[] teams);
            Map.DumpScriptData(out INIEntity script, out INIEntity[] scripts);
            Map.DumpHouse(out INIEntity house, out INIEntity[] houses);
            Map.DumpCountries(out INIEntity con, out INIEntity[] cons);
            Map.DumpTaskforce(out INIEntity task, out INIEntity[] tasks);
            List<INIEntity> result = new List<INIEntity>
            {
                type, enable, trigger, events, actions, tags,
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
        }

        private void DumpObjects(Dictionary<string, INIEntity> dest)
        {
            INIEntity entBud = new INIEntity("Structures");
            //buildings :
            using (var structureEntity = new INIEntity("Structures", "", ""))
            {
                int idx = 0;
                foreach (var building in Map.Buildings)
                {
                    structureEntity.AddPair
                        (
                            new INIPair
                            (
                                idx++.ToString(),
                                Misc.ParseObjectsToString
                                (
                                    building.OwnerHouse,
                                    building.Id,
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
                                    idx++.ToString(),
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
