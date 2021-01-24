using RelertSharp.Common;
using RelertSharp.MapStructure.Objects;
using System.Collections.Generic;
using System.Linq;
using System;
using static RelertSharp.Common.GlobalVar;
using static RelertSharp.Utils.Misc;

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
        private const string EX_SHP = ".shp";
        private const string EX_VXL = ".vxl";
        private const string EX_HVA = ".hva";
        private const string SUFF_BARREL = "barr";
        private const string SUFF_TURRET = "tur";
        private const string NULL_ICON = "xxicon.shp";


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
            if (!GlobalConfig.IgnoreBuildingTheaterArt) name = id.Replace(1, _suff) + ".shp";
            if (GlobalDir.HasFile(name)) return name;
            if (GlobalDir.HasFile(name.Replace(1, 'G'))) return name.Replace(1, 'G');
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
                filename = filename.Replace(1, _suff);
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
                if (string.IsNullOrEmpty(shp)) return NULL_ICON;
                else return shp + EX_SHP;
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
            foundx = ent.ParseInt("Width", 1);
            foundy = ent.ParseInt("Height", 1);
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
                        int[] tmp = art.ParseIntList(found);
                        if (tmp.Length != 2)
                        {
                            Log.Critical("Building foundation error! Item {0} has unreadable foundation!", regname);
                            shape.SetValueAll(true);
                            return shape;
                        }
                        try
                        {
                            shape[tmp[0] + tmp[1] * sizeX] = true;
                        }
                        catch
                        {
                            Log.Critical("Building foundation error! Item {0}: size {1},{2} has unreadable foundation!", regname, sizeX, sizeY);
                            shape.SetValueAll(true);
                        }
                    }
                }
            }
            else shape.SetValueAll(true);
            return shape;
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
                visibility = item.ParseInt("LightVisibility", 5000);
                intensity = item.ParseFloat("LightIntensity");
                float r = item.ParseFloat("LightRedTint") + 1;
                float g = item.ParseFloat("LightGreenTint") + 1;
                float b = item.ParseFloat("LightBlueTint") + 1;
                return new Vec4(r, g, b, 1);
            }
        }
        public bool IsVxl(string id)
        {
            return IniParseBool(Art[id]["Voxel"]);
        }
        public int GetFrameFromDirection(int direction, string nameID)
        {
            string art = GetArtEntityName(nameID);
            INIEntity sequence = Art[Art[art]["Sequence"]];
            direction >>= 5;
            if (sequence.Name == "") return direction;
            int[] ready = sequence.ParseIntList("Ready");
            //int result = ready[0];
            int result = 7 - direction;
            return result < 0 ? 0 : result;
        }
        public string GetArtEntityName(string nameID, bool isinfantry = false)
        {
            string artname = this[nameID]["Image"];
            if (string.IsNullOrEmpty(artname)) artname = nameID;
            if (isinfantry && IniParseBool(this[nameID]["AlternateTheaterArt"]))
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
        public string GetObjectImgName(ObjectItemBase inf, out short frame)
        {
            frame = 0;
            string img = GetArtEntityName(inf.RegName) + ".shp";
            frame = GlobalDir.GetShpFrameCount(img, out bool b);
            return img;
        }
        public void LoadArt(INIFile f)
        {
            Art = f;
        }
        public string GetOverlayPalette(string regName)
        {
            INIEntity ov = this[regName];
            if (ov.ParseBool("Wall")) return string.Format("unit{0}.pal", TileDictionary.TheaterSub);
            if (ov.ParseBool("Tiberium")) return string.Format("{0}.pal", GlobalConfig.GetTheaterPalName(CurrentMapDocument.Map.Info.TheaterName));
            return string.Format("iso{0}.pal", TileDictionary.TheaterSub);
        }
        public string GetOverlayFileName(string regName)
        {
            string filename = regName;
            INIEntity ov = this[regName];
            string img = ov["Image"];
            bool wall = ov.ParseBool("Wall");
            if (wall) FixWallOverlayName(ref filename);
            if (!string.IsNullOrEmpty(img) && regName != img) filename = img;
            if (GlobalDir.HasFile(filename + ".shp")) return filename.ToLower() + ".shp";
            else if (wall) return filename.Replace(1, 'G').ToLower() + ".shp";
            else return string.Format("{0}.{1}", filename.ToLower(), TileDictionary.TheaterSub);
        }
        public string GetOverlayName(byte overlayid)
        {
            INIEntity ov = this["OverlayTypes"];
            return ov[overlayid.ToString()];
        }
        public byte GetOverlayIndex(string regName)
        {
            INIEntity lst = this["OverlayTypes"];
            int index = lst.IndexOfValue(regName);
            if (index > -1 && index < 256) return (byte)index;
            else return 0;
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
            return this["AI"].ParseStringList("NeutralTechBuildings").Contains(regname);
        }
        public int GuessSide(string regname, CombatObjectType type, bool isBuilding = false)
        {
            //has planning side
            if (isBuilding)
            {
                INIEntity ent = this[regname];
                int planning = ent.ParseInt("AIBasePlanningSide", -1);
                if (planning >= GetSideCount()) return -1;
                if (planning >= 0)
                {
                    if (string.IsNullOrEmpty(BuildingRoots[planning]) && ent["Factory"] == "BuildingType") BuildingRoots[planning] = regname;
                    if (string.IsNullOrEmpty(InfantryRoots[planning]) && ent["Factory"] == "InfantryType") InfantryRoots[planning] = regname;
                    if (string.IsNullOrEmpty(UnitRoots[planning]) && ent["Factory"] == "UnitType" && !ent.ParseBool("Naval")) UnitRoots[planning] = regname;
                    if (string.IsNullOrEmpty(NavalRoots[planning]) && ent["Factory"] == "UnitType" && ent.ParseBool("Naval")) NavalRoots[planning] = regname;
                    if (string.IsNullOrEmpty(AircraftRoots[planning]) && ent["Factory"] == "AircraftType") AircraftRoots[planning] = regname;
                    return planning;
                }
                else
                {
                    List<string> conyards = this["AI"].ParseStringList("BuildConst").ToList(); ;
                    if (conyards.Contains(regname)) return conyards.IndexOf(regname);
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
            foreach (string prerequest in this[regname].ParseStringList("Prerequisite"))
            {
                if (this["GenericPrerequisites"].HasPair(prerequest))
                {
                    foreach (string subrequest in this["GenericPrerequisites"].ParseStringList(prerequest))
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
        public string FormatTreeNodeName(string regname, bool hasUiName = true)
        {
            if (hasUiName)
                return string.Format("{1}:{2}({0})",
                    regname,
                    GlobalCsf[this[regname]["UIName"]].ContentString,
                    this[regname]["Name"]);
            else
                return string.Format("{0}({1})",
                    this[regname]["Name"],
                    regname);
        }
        /// <summary>
        /// include internal rules carried by map
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool HasIniEntAll(string name)
        {
            return HasIniEnt(name) || (MapIniData == null ? false : MapIniData.Keys.Contains(name));
        }
        #endregion


        #region Internal - Rules
        internal void GetBuildingShapeData(string nameid, out int height, out int foundX, out int foundY)
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
                        sz.X = art.ParseInt("Foundation.X", 1);
                        sz.Y = art.ParseInt("Foundation.Y", 1);
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
                sz.Z = art.ParseInt("Height", 5) + 5;
                bufferedBuildingShape[nameid] = sz;
            }
            else sz = bufferedBuildingShape[nameid];
            foundX = (int)sz.X == 0 ? 1 : (int)sz.X;
            foundY = (int)sz.Y == 0 ? 1 : (int)sz.Y;
            height = (int)sz.Z;
        }
        internal BuildingData GetBuildingData(string name)
        {
            BuildingData data = new BuildingData();
            string artname = GetArtEntityName(name);
            INIEntity art = Art[artname];
            if (string.IsNullOrEmpty(art.Name)) data.SelfId = name + EX_SHP;
            else data.SelfId = GuessStructureName(art);

            data.AlphaImage = this[name]["AlphaImage"];
            if (!string.IsNullOrEmpty(data.AlphaImage)) data.AlphaImage += EX_SHP;

            if (!GlobalConfig.DeactiveAnimList.Contains(artname))
            {
                data.ActivateAnim = GuessStructureName(Art[art["ActiveAnim"]]);
                data.ActivateAnimTwo = GuessStructureName(Art[art["ActiveAnimTwo"]]);
                data.ActivateAnimThree = GuessStructureName(Art[art["ActiveAnimThree"]]);
            }

            data.IdleAnim = GuessStructureName(Art[art["IdleAnim"]]);
            data.SuperAnim = GuessStructureName(Art[art["SuperAnim"]]);
            if (!GlobalConfig.DeactiveBibList.Contains(artname))
            {
                data.BibAnim = GuessStructureName(art["BibShape"]);
            }

            data.TurretAnimIsVoxel = this[name].ParseBool("TurretAnimIsVoxel");
            if (data.TurretAnimIsVoxel)
            {
                data.TurretAnim = this[name]["TurretAnim"] + EX_VXL;
                data.TurretBarrel = data.TurretAnim.ToLower().Replace("tur", "barl");
            }
            else data.TurretAnim = GuessStructureName(Art[this[name]["TurretAnim"]]);

            data.nSelf = GlobalDir.GetShpFrameCount(data.SelfId, out bool bSelf);
            bool bTurret = true;
            if (!data.TurretAnimIsVoxel)
            {
                data.nTurretAnim = GlobalDir.GetShpFrameCount(data.TurretAnim, out bool turrEmpty);
                bTurret = turrEmpty;
            }
            else
            {
                data.nTurretAnim = 0;
                bTurret = false;
            }

            data.TurretAnimZAdjust = this[name].ParseInt("TurretAnimZAdjust");
            data.ActiveAnimZAdjust = art.ParseInt("ActiveAnimZAdjust");
            data.ActiveAnimTwoZAdjust = art.ParseInt("ActiveAnimTwoZAdjust");
            data.ActiveAnimThreeZAdjust = art.ParseInt("ActiveAnimThreeZAdjust");
            data.ActiveAnimFourZAdjust = art.ParseInt("ActiveAnimZFourAdjust");
            data.IdleAnimZAdjust = art.ParseInt("IdleAnimZAdjust");
            data.SuperAnimZAdjust = art.ParseInt("SuperAnimZAdjust");

            data.nActivateAnim = GlobalDir.GetShpFrameCount(data.ActivateAnim, out bool bAnim);
            data.nActivateAnimTwo = GlobalDir.GetShpFrameCount(data.ActivateAnimTwo, out bool bAnim2);
            data.nActivateAnimThree = GlobalDir.GetShpFrameCount(data.ActivateAnimThree, out bool bAnim3);
            data.nIdleAnim = GlobalDir.GetShpFrameCount(data.IdleAnim, out bool bIdle);
            data.nSuperAnim = GlobalDir.GetShpFrameCount(data.SuperAnim, out bool bSuper);
            data.nBibAnim = GlobalDir.GetShpFrameCount(data.BibAnim, out bool bBib);
            data.IsEmpty = bSelf && bAnim && bAnim2 && bAnim3 && bIdle && bSuper && bBib && bTurret;
            return data;
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
        private List<IIndexableItem> Sides;
        public List<IIndexableItem> SideIndexes
        {
            get
            {
                if (Sides == null)
                {
                    Sides = new List<IIndexableItem>();
                    int i = 1;
                    foreach (INIPair pSideName in this["Sides"])
                    {
                        if (pSideName.Value == "Neutral" || pSideName.Value == "Civilian") continue;
                        ComboItem item = new ComboItem(i.ToString(), pSideName.PreComment ?? pSideName.Name);
                        Sides.Add(item);
                        i++;
                    }
                }
                return Sides;
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
        public new INIEntity this[string name]
        {
            get
            {
                if (MapIniData != null && MapIniData.Keys.Contains(name))
                {
                    INIEntity result = MapIniData[name];
                    if (HasIniEnt(name))
                    {
                        INIEntity org = IniDict[name];
                        org.JoinWith(result);
                        return org;
                    }
                    return result;
                }
                else if (HasIniEnt(name)) return IniDict[name];
                else return INIEntity.NullEntity;
            }
        }
        #endregion
    }
}
