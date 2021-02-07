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
        private Dictionary<string, Vec3> bufferedBuildingShape = new Dictionary<string, Vec3>();
        private static char _suff
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

        #region Ctor - Rules
        public Rules(string path, INIFileType itype = INIFileType.RulesINI) : base(path, INIFileType.RulesINI) { }
        public Rules(byte[] _data, string _filename) : base(_data, _filename, INIFileType.RulesINI)
        {
            this["OverlayTypes"].Reorganize();
        }
        #endregion


        #region Private Methods - Rules
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
        #endregion


        #region Public Methods - Rules
        public string GetPcxName(string regid)
        {
            string art = GetArtEntityName(regid);
            string pcx = Art[art]["CameoPCX"];
            string shp = Art[art]["Cameo"].ToLower();
            if (string.IsNullOrEmpty(pcx))
            {
                if (string.IsNullOrEmpty(shp)) return Constant.NULL_ICON;
                else return shp + Constant.EX_SHP;
            }
            return pcx;
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
        public void LoadArt(INIFile f)
        {
            Art = f;
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
