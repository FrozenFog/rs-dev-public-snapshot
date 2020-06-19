﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using RelertSharp.Common;
using RelertSharp.MapStructure.Objects;
using RelertSharp.FileSystem;
using static RelertSharp.Utils.Misc;
using static RelertSharp.Common.GlobalVar;

namespace RelertSharp.IniSystem
{
    public class Rules : INIFile
    {
        private char _suff
        {
            get
            {
                switch (CurrentTheater)
                {
                    case TheaterType.Desert:
                        return 'D';
                    case TheaterType.Snow:
                        return 'A';
                    case TheaterType.Lunar:
                        return 'L';
                    case TheaterType.NewUrban:
                        return 'N';
                    case TheaterType.Temprate:
                        return 'T';
                    case TheaterType.Urban:
                        return 'U';
                    default:
                        return 'G';
                }
            }
        }
        private Dictionary<string, Vec3> bufferedBuildingShape = new Dictionary<string, Vec3>();


        #region Ctor - Rules
        public Rules(string path, INIFileType itype = INIFileType.RulesINI) : base(path, INIFileType.RulesINI) { }
        public Rules(byte[] _data, string _filename) : base(_data, _filename, INIFileType.RulesINI)
        {
            this["OverlayTypes"].Reorganize();
        }
        #endregion


        #region Private Methods - Rules
        private void InitializePowerupDictionary()
        {
            foreach (INIEntity ent in IniData)
            {
                if (ent.HasPair("PowersUpBuilding"))
                {
                    INIPair p = ent.GetPair("PowersUpBuilding");
                    string host = p.Value as string;
                    if (!powerups.Keys.Contains(host))
                    {
                        powerups[host] = new List<TechnoPair>();
                    }
                    TechnoPair pwups = new TechnoPair(ent.Name, ent["Name"]);
                    powerups[host].Add(pwups);
                }
            }
            powerupInitialize = true;
        }
        private List<TechnoPair> GetTechnoPairs(string entName, TechnoPair.AbstractType type, TechnoPair.IndexType indexType = TechnoPair.IndexType.Index)
        {
            INIEntity entLs = this[entName];
            List<TechnoPair> result = new List<TechnoPair>();
            foreach (INIPair p in entLs)
            {
                INIEntity ent = this[p.Value.ToString()];
                result.Add(ent.ToTechno(p.Name, type, indexType));
            }
            return result;
        }
        private List<TechnoPair> GetTechnoPairs(INIEntity entlist)
        {
            List<TechnoPair> result = new List<TechnoPair>();
            foreach (INIPair p in entlist)
            {
                result.Add(new TechnoPair(p.Name, p.Value));
            }
            return result;
        }
        private string GuessStructureName(string id)
        {
            if (string.IsNullOrEmpty(id)) return "";
            string name = id + ".shp";
            if (!GlobalConfig.IgnoreBuildingTheaterArt) name = Replace(id + ".shp", 1, _suff);
            if (GlobalDir.HasFile(name)) return name;
            if (GlobalDir.HasFile(Replace(name, 1, 'G'))) return Replace(name, 1, 'G');
            if (GlobalDir.HasFile(id + ".shp")) return id + ".shp";
            return "";
        }
        private string GuessStructureName(INIEntity ent)
        {
            string img = ent["Image"];
            if (string.IsNullOrEmpty(img)) return GuessStructureName(ent.Name);
            else return GuessStructureName(img);
        }
        private string GetArtName(string regname)
        {
            string img = this[regname]["Image"];
            if (string.IsNullOrEmpty(img)) return regname;
            if (Art.HasIniEnt(img)) return img;
            else return regname;
        }
        private void VxlFormating(string id, bool vxl, ref string name, ref string tur, ref string barl)
        {
            if (vxl)
            {
                tur = id + "tur.vxl";
                barl = id + "barl.vxl";
                name = id + ".vxl";
            }
            else name = id + ".shp";
        }
        #endregion


        #region Public Methods - Rules
        public void FixWallOverlayName(ref string filename)
        {
            if (!GlobalConfig.IgnoreBuildingTheaterArt)
            {
                filename = Replace(filename, 1, _suff);
            }
        }
        private bool powerupInitialize = false;
        private Dictionary<string, List<TechnoPair>> powerups = new Dictionary<string, List<TechnoPair>>();
        public IEnumerable<TechnoPair> GetBuildingUpgradeList(string regid)
        {
            if (!powerupInitialize)
            {
                InitializePowerupDictionary();
            }
            if (powerups.Keys.Contains(regid))
            {
                return powerups[regid];
            }
            return null;
        }
        public string GetCsfUIName(string regid)
        {
            if (!HasIniEnt(regid)) return GlobalCsf[regid].ContentString;
            string uiname = this[regid]["UIName"];
            return GlobalCsf[uiname].ContentString;
        }
        public string GetPcxName(string regid)
        {
            string art = GetArtName(regid);
            string pcx = Art[art]["CameoPCX"];
            string shp = Art[art]["Cameo"].ToLower();
            if (string.IsNullOrEmpty(pcx))
            {
                if (string.IsNullOrEmpty(shp)) return "xxicon.shp";
                else return shp + ".shp";
            }
            return pcx;
        }
        public string GetCustomPaletteName(string nameid)
        {
            string artname = GetArtEntityName(nameid);
            INIEntity art = Art[artname];

            string pal = art["Palette"];
            if (string.IsNullOrEmpty(pal)) return pal;
            else return string.Format("{0}{1}.{2}", pal, TileDictionary.TheaterSub, "pal");
        }
        public void GetSmudgeSizeData(string nameid, out int foundx, out int foundy)
        {
            INIEntity ent = this[nameid];
            foundx = ent.GetPair("Width").ParseInt(1);
            foundy = ent.GetPair("Height").ParseInt(1);
        }
        public List<bool> GetBuildingCustomShape(string regname, int sizeX, int sizeY)
        {
            List<bool> shape = InitializeListWithCap<bool>(sizeX * sizeY);
            string artname = GetArtEntityName(regname);
            INIEntity art = Art[artname];
            string foundation = (string)art["Foundation"].ToLower();
            if (foundation == "custom")
            {
                for (int i = 0; i < sizeX * sizeY; i++)
                {
                    string found = string.Format("Foundation.{0}", i);
                    if (art.HasPair(found))
                    {
                        int[] tmp = art.GetPair(found).ParseIntList();
                        if (tmp.Length != 2)
                        {
                            Log.Critical("Building foundation error! Item {0} has unreadable foundation!", regname);
                            SetListValue<bool>(ref shape, true);
                            return shape;
                        }
                        try
                        {
                            shape[tmp[0] + tmp[1] * sizeY] = true;
                        }
                        catch
                        {
                            Log.Critical("Building foundation error! Item {0}: size {1},{2} has unreadable foundation!", regname, sizeX, sizeY);
                            SetListValue<bool>(ref shape, true);
                        }
                    }
                }
            }
            else SetListValue<bool>(ref shape, true);
            return shape;
        }
        public void GetBuildingShapeData(string nameid, out int height, out int foundX, out int foundY)
        {
            Vec3 sz;
            if (!bufferedBuildingShape.Keys.Contains(nameid))
            {
                sz = new Vec3();
                string artname = GetArtEntityName(nameid);
                INIEntity art = Art[artname];

                string foundation = (string)art["Foundation"].ToLower();
                if (!string.IsNullOrEmpty(foundation))
                {
                    if (foundation == "custom")
                    {
                        sz.X = art.GetPair("Foundation.X").ParseInt(1);
                        sz.Y = art.GetPair("Foundation.Y").ParseInt(1);
                    }
                    else
                    {
                        string[] tmp = foundation.Split('x');
                        sz.X = int.Parse(tmp[0]);
                        sz.Y = int.Parse(tmp[1]);
                    }
                }
                else
                {
                    sz.X = 1;
                    sz.Y = 1;
                }
                sz.Z = art.GetPair("Height").ParseInt(5) + 3;
                bufferedBuildingShape[nameid] = sz;
            }
            else sz = bufferedBuildingShape[nameid];
            foundX = (int)sz.X;
            foundY = (int)sz.Y;
            height = (int)sz.Z;
        }
        public Vec4 GetBuildingLampData(string nameid, out float intensity, out int visibility)
        {
            if (!HasIniEnt(nameid) || string.IsNullOrEmpty(this[nameid].Name))
            {
                intensity = 0;
                visibility = 0;
                return Vec4.Unit3(0);
            }
            else
            {
                INIEntity item = this[nameid];
                visibility = item.GetPair("LightVisibility").ParseInt(5000);
                intensity = item.GetPair("LightIntensity").ParseFloat();
                float r = item.GetPair("LightRedTint").ParseFloat() + 1;
                float g = item.GetPair("LightGreenTint").ParseFloat() + 1;
                float b = item.GetPair("LightBlueTint").ParseFloat() + 1;
                return new Vec4(r, g, b, 1);
            }
        }
        public bool IsVxl(string id)
        {
            return ParseBool(Art[id]["Voxel"]);
        }
        public int GetFrameFromDirection(int direction, string nameID)
        {
            string art = GetArtEntityName(nameID);
            INIEntity sequence = Art[Art[art]["Sequence"]];
            direction >>= 5;
            if (sequence.Name == "") return direction;
            int[] ready = sequence.GetPair("Ready").ParseIntList();
            int result = ready[0];
            return 7 - direction;
        }
        public string GetArtEntityName(string nameID, bool isinfantry = false)
        {
            string artname = this[nameID]["Image"];
            if (string.IsNullOrEmpty(artname)) artname = nameID;
            if (isinfantry && ParseBool(this[nameID]["AlternateTheaterArt"]))
            {
                if (GlobalDir.HasFile(artname + _suff + ".shp")) artname += _suff;
            }
            return artname;
        }
        public Pnt GetVoxTurOffset(string id)
        {
            string x = this[id]["TurretAnimX"];
            string y = this[id]["TurretAnimY"];
            return new Pnt()
            {
                X = string.IsNullOrEmpty(x) ? 0 : int.Parse(x),
                Y = string.IsNullOrEmpty(y) ? 0 : int.Parse(y)
            };
        }
        public string GetUnitImgName(string id, ref string tur, ref string barl, ref bool vxl)
        {
            string artname = GetArtEntityName(id);
            INIEntity art = Art[artname];
            vxl = IsVxl(artname);
            VxlFormating(artname, vxl, ref artname, ref tur, ref barl);
            return artname;
        }
        public INIEntity GetBuildingTurret(string nameid)
        {
            string turanim = this[nameid]["TurretAnim"];
            return Art[turanim];
        }
        public string GetObjectImgName(string id, ref string anim, ref string turret, ref string bib, ref bool isVox, ref string idle, ref string anim2, ref string anim3, ref string barl, ref string super,
            out short nSelf, out short nAnim, out short nTurret, out short nBib, out short nIdle, out short nAnim2, out short nAnim3, out short nSuper, out string alpha)
        {
            string artname = GetArtEntityName(id);
            INIEntity art = Art[artname];
            string img;
            if (string.IsNullOrEmpty(art.Name)) img = id + ".shp";
            else img = GuessStructureName(art);
            alpha = this[id]["AlphaImage"];
            if (!string.IsNullOrEmpty(alpha)) alpha += ".shp";
            if (!GlobalConfig.DeactiveAnimList.Contains(artname))
            {
                anim = GuessStructureName(Art[art["ActiveAnim"]]);
                anim2 = GuessStructureName(Art[art["ActiveAnimTwo"]]);
                anim3 = GuessStructureName(Art[art["ActiveAnimThree"]]);
            }
            idle = GuessStructureName(Art[art["IdleAnim"]]);
            super = GuessStructureName(Art[art["SuperAnim"]]);
            if (!GlobalConfig.DeactiveBibList.Contains(artname))
            {
                bib = GuessStructureName(art["BibShape"]);
            }
            isVox = ParseBool(this[id]["TurretAnimIsVoxel"]);
            if (isVox)
            {
                turret = this[id]["TurretAnim"] + ".vxl";
                barl = turret.ToLower().Replace("tur", "barl");
            }
            else turret = GuessStructureName(Art[this[id]["TurretAnim"]]);

            nSelf = GlobalDir.GetShpFrameCount(img);
            if (!isVox) nTurret = GlobalDir.GetShpFrameCount(turret);
            else nTurret = 0;
            nAnim = GlobalDir.GetShpFrameCount(anim);
            nAnim2 = GlobalDir.GetShpFrameCount(anim2);
            nAnim3 = GlobalDir.GetShpFrameCount(anim3);
            nIdle = GlobalDir.GetShpFrameCount(idle);
            nSuper = GlobalDir.GetShpFrameCount(super);
            nBib = GlobalDir.GetShpFrameCount(bib);
            return img;
        }
        public string GetObjectImgName(ObjectItemBase inf, out short frame)
        {
            frame = 0;
            string img = GetArtEntityName(inf.RegName) + ".shp";
            frame = GlobalDir.GetShpFrameCount(img);
            return img;
        }
        public void LoadArt(INIFile f)
        {
            Art = f;
        }
        public string GetOverlayName(byte overlayid)
        {
            INIEntity ov = this["OverlayTypes"];
            return ov[overlayid.ToString()];
        }
        private bool sideInitialized = false;
        private int side = -1;
        public int GetSideCount()
        {
            if (!sideInitialized)
            {
                side = 0;
                INIEntity sidelist = this["Sides"];
                foreach (INIPair p in sidelist)
                {
                    if (p.Name != "Civilian" && p.Name != "Mutant") side++;
                }
                BuildingRoots = InitializeListWithCap<string>(side);
                InfantryRoots = InitializeListWithCap<string>(side);
                UnitRoots = InitializeListWithCap<string>(side);
                NavalRoots = InitializeListWithCap<string>(side);
                AircraftRoots = InitializeListWithCap<string>(side);
                Log.Write(string.Format("{0} sides loaded", side));
                sideInitialized = true;
            }
            return side;
        }
        public string GetSideName(int index)
        {
            return this["Sides"].GetPair(index).Name;
        }
        public bool IsTechBuilding(string regname)
        {
            return this["AI"].GetPair("NeutralTechBuildings").ParseStringList().Contains(regname);
        }
        public int GuessSide(string regname, CombatObjectType type, bool isBuilding = false)
        {
            //has planning side
            if (isBuilding)
            {
                INIEntity ent = this[regname];
                int planning = ent.GetPair("AIBasePlanningSide").ParseInt(-1);
                if (planning >= GetSideCount()) return -1;
                if (planning >= 0)
                {
                    if (string.IsNullOrEmpty(BuildingRoots[planning]) && ent["Factory"] == "BuildingType") BuildingRoots[planning] = regname;
                    if (string.IsNullOrEmpty(InfantryRoots[planning]) && ent["Factory"] == "InfantryType") InfantryRoots[planning] = regname;
                    if (string.IsNullOrEmpty(UnitRoots[planning]) && ent["Factory"] == "UnitType" && !ent.GetPair("Naval").ParseBool()) UnitRoots[planning] = regname;
                    if (string.IsNullOrEmpty(NavalRoots[planning]) && ent["Factory"] == "UnitType" && ent.GetPair("Naval").ParseBool()) NavalRoots[planning] = regname;
                    if (string.IsNullOrEmpty(AircraftRoots[planning]) && ent["Factory"] == "AircraftType") AircraftRoots[planning] = regname;
                    return planning;
                }
            }

            //guess by root
            List<string> root = new List<string>();
            switch (type)
            {
                case CombatObjectType.Aircraft:
                    root = AircraftRoots;
                    break;
                case CombatObjectType.Vehicle:
                    root = UnitRoots;
                    break;
                case CombatObjectType.Infantry:
                    root = InfantryRoots;
                    break;
                case CombatObjectType.Building:
                    root = BuildingRoots;
                    break;
                case CombatObjectType.Naval:
                    root = NavalRoots;
                    break;
            }
            foreach (string prerequest in this[regname].GetPair("Prerequisite").ParseStringList())
            {
                if (this["GenericPrerequisites"].HasPair(prerequest))
                {
                    foreach (string subrequest in this["GenericPrerequisites"].GetPair(prerequest).ParseStringList())
                    {
                        if (root.Contains(subrequest)) return root.IndexOf(subrequest);
                    }
                }
                else
                {
                    if (root.Contains(prerequest)) return root.IndexOf(prerequest);
                }
            }

            //no side
            return -1;
        }
        public string FormatTreeNodeName(string regname)
        {
            return string.Format("{1}:{2}({0})", regname, GlobalCsf[this[regname]["UIName"]].ContentString, this[regname]["Name"]);
        }
        #endregion


        #region Public Calls - Rules
        public List<string> BuildingRoots { get; private set; }
        public List<string> InfantryRoots { get; private set; } 
        public List<string> UnitRoots { get; private set; } 
        public List<string> NavalRoots { get; private set; } 
        public List<string> AircraftRoots { get; private set; } 
        public Dictionary<string, INIEntity> MapIniData { get; set; }
        public INIFile Art { get; private set; }
        public List<TechnoPair> VehicleList
        {
            get
            {
                return GetTechnoPairs("VehicleTypes", TechnoPair.AbstractType.Name);
            }
        }
        public List<TechnoPair> InfantryList
        {
            get
            {
                return GetTechnoPairs("InfantryTypes", TechnoPair.AbstractType.Name);
            }
        }
        public List<TechnoPair> AircraftList
        {
            get
            {
                return GetTechnoPairs("AircraftTypes", TechnoPair.AbstractType.Name);
            }
        }
        public List<TechnoPair> BuildingList
        {
            get
            {
                return GetTechnoPairs("BuildingTypes", TechnoPair.AbstractType.Name);
            }
        }
        public List<TechnoPair> BuildingList0
        {
            get
            {
                return GetTechnoPairs(this["BuildingTypes"].ReorganizeTo());
            }
        }
        public List<TechnoPair> BuildingIDList
        {
            get
            {
                return GetTechnoPairs("BuildingTypes", TechnoPair.AbstractType.Name, TechnoPair.IndexType.RegName);
            }
        }
        public List<TechnoPair> SuperWeaponList
        {
            get
            {
                return GetTechnoPairs("SuperWeaponTypes", TechnoPair.AbstractType.RegName);
            }
        }
        public List<TechnoPair> WarheadList
        {
            get
            {
                return GetTechnoPairs("Warheads", TechnoPair.AbstractType.RegName);
            }
        }
        public List<TechnoPair> AnimationList
        {
            get
            {
                return GetTechnoPairs(this["Animations"]);
            }
        }
        public List<TechnoPair> AnimationList0
        {
            get
            {
                return GetTechnoPairs(this["Animations"].ReorganizeTo());
            }
        }
        public List<TechnoPair> ParticalList
        {
            get
            {
                return GetTechnoPairs(this["Particles"]);
            }
        }
        public List<TechnoPair> VoxAnimList
        {
            get
            {
                return GetTechnoPairs(this["VoxelAnims"]);
            }
        }
        public List<TechnoPair> MovieList
        {
            get
            {
                return GetTechnoPairs(Art["Movies"]);
            }
        }
        public List<TechnoPair> MovieList0
        {
            get
            {
                return GetTechnoPairs(Art["Movies"].ReorganizeTo());
            }
        }
        public List<TechnoPair> TechnoList
        {
            get
            {
                List<TechnoPair> result = new List<TechnoPair>();
                result.AddRange(GetTechnoPairs("InfantryTypes", TechnoPair.AbstractType.Name, TechnoPair.IndexType.RegName));
                result.AddRange(GetTechnoPairs("VehicleTypes", TechnoPair.AbstractType.Name, TechnoPair.IndexType.RegName));
                result.AddRange(GetTechnoPairs("AircraftTypes", TechnoPair.AbstractType.Name, TechnoPair.IndexType.RegName));
                result.AddRange(GetTechnoPairs("BuildingTypes", TechnoPair.AbstractType.Name, TechnoPair.IndexType.RegName));
                return result;
            }
        }
        public List<TechnoPair> TaskforceUnitAvailableList
        {
            get
            {
                List<TechnoPair> result = new List<TechnoPair>();
                result.AddRange(GetTechnoPairs("InfantryTypes", TechnoPair.AbstractType.Name, TechnoPair.IndexType.RegName));
                result.AddRange(GetTechnoPairs("VehicleTypes", TechnoPair.AbstractType.Name, TechnoPair.IndexType.RegName));
                result.AddRange(GetTechnoPairs("AircraftTypes", TechnoPair.AbstractType.Name, TechnoPair.IndexType.RegName));
                return result;
            }
        }
        public List<TechnoPair> GlobalVar
        {
            get
            {
                List<TechnoPair> result = new List<TechnoPair>();
                INIEntity ent = this["VariableNames"];
                foreach (INIPair p in ent)
                {
                    result.Add(new TechnoPair(p.Name, p.Value));
                }
                return result;
            }
        }
        #endregion
    }
}
