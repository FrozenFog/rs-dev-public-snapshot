using RelertSharp.Common;
using System.Collections.Generic;
using System.Linq;
using static RelertSharp.Common.GlobalVar;
using static RelertSharp.Utils.Misc;
using static RelertSharp.Common.Constant;

namespace RelertSharp.IniSystem
{
    public class Rules : INIFile
    {
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
        private IEnumerable<IIndexableItem> GetIdxItem_Reg_Name(string entName)
        {
            INIEntity entList = this[entName];
            List<ComboItem> result = new List<ComboItem>();
            foreach (INIPair p in entList)
            {
                INIEntity ent = this[p.Value.ToString()];
                result.Add(new ComboItem(p.Value, ent[KEY_NAME]));
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
        public string GetArtEntityName(string nameID, bool isinfantry = false)
        {
            string artname = this[nameID][KEY_IMAGE];
            if (string.IsNullOrEmpty(artname)) artname = nameID;
            if (isinfantry && IniParseBool(this[nameID]["AlternateTheaterArt"]))
            {
                if (GlobalDir.HasFile(artname + _suff + ".shp")) artname += _suff;
            }
            return artname;
        }
        private bool powerupInitialize = false;
        private Dictionary<string, List<IIndexableItem>> powerups = new Dictionary<string, List<IIndexableItem>>();
        public IEnumerable<IIndexableItem> GetBuildingUpgradeList(string regid)
        {
            List<IIndexableItem> result = new List<IIndexableItem>
            {
                new ComboItem(VALUE_NONE)
            };
            if (string.IsNullOrEmpty(regid)) return result;
            if (!powerupInitialize)
            {
                InitializePowerupDictionary();
            }
            if (powerups.Keys.Contains(regid))
            {
                result.AddRange(powerups[regid]);
            }
            return result;
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
                        powerups[host] = new List<IIndexableItem>();
                    }
                    IIndexableItem pwups = new ComboItem(ent.Name, ent["Name"]);
                    powerups[host].Add(pwups);
                }
            }
            powerupInitialize = true;
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
        /// <summary>
        /// Set local rules that used in map ini section, readonly
        /// </summary>
        /// <param name="localRules"></param>
        public void SetLocalRules(Dictionary<string, INIEntity> localRules)
        {
            MapIniData = new Dictionary<string, INIEntity>(localRules);
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
        public Dictionary<string, INIEntity> MapIniData { get; private set; }
        public INIFile Art { get; private set; }
        //public List<TechnoPair> VehicleList
        //{
        //    get
        //    {
        //        return GetTechnoPairs("VehicleTypes", TechnoPair.AbstractType.Name);
        //    }
        //}
        //public List<TechnoPair> InfantryList
        //{
        //    get
        //    {
        //        return GetTechnoPairs("InfantryTypes", TechnoPair.AbstractType.Name);
        //    }
        //}
        //public List<TechnoPair> AircraftList
        //{
        //    get
        //    {
        //        return GetTechnoPairs("AircraftTypes", TechnoPair.AbstractType.Name);
        //    }
        //}
        //public List<TechnoPair> BuildingList
        //{
        //    get
        //    {
        //        return GetTechnoPairs("BuildingTypes", TechnoPair.AbstractType.Name);
        //    }
        //}
        //public List<TechnoPair> BuildingList0
        //{
        //    get
        //    {
        //        return GetTechnoPairs(this["BuildingTypes"].ReorganizeTo());
        //    }
        //}
        //public List<TechnoPair> BuildingIDList
        //{
        //    get
        //    {
        //        return GetTechnoPairs("BuildingTypes", TechnoPair.AbstractType.Name, TechnoPair.IndexType.RegName);
        //    }
        //}
        //public List<TechnoPair> SuperWeaponList
        //{
        //    get
        //    {
        //        return GetTechnoPairs("SuperWeaponTypes", TechnoPair.AbstractType.RegName);
        //    }
        //}
        //public List<TechnoPair> WarheadList
        //{
        //    get
        //    {
        //        return GetTechnoPairs("Warheads", TechnoPair.AbstractType.RegName);
        //    }
        //}
        //public List<TechnoPair> AnimationList
        //{
        //    get
        //    {
        //        return GetTechnoPairs(this["Animations"]);
        //    }
        //}
        //public List<TechnoPair> AnimationList0
        //{
        //    get
        //    {
        //        return GetTechnoPairs(this["Animations"].ReorganizeTo());
        //    }
        //}
        //public List<TechnoPair> ParticalList
        //{
        //    get
        //    {
        //        return GetTechnoPairs(this["Particles"]);
        //    }
        //}
        //public List<TechnoPair> VoxAnimList
        //{
        //    get
        //    {
        //        return GetTechnoPairs(this["VoxelAnims"]);
        //    }
        //}
        //public List<TechnoPair> MovieList
        //{
        //    get
        //    {
        //        return GetTechnoPairs(Art["Movies"]);
        //    }
        //}
        //public List<TechnoPair> MovieList0
        //{
        //    get
        //    {
        //        return GetTechnoPairs(Art["Movies"].ReorganizeTo());
        //    }
        //}
        public IEnumerable<IIndexableItem> TechnoList
        {
            get
            {
                List<IIndexableItem> result = new List<IIndexableItem>();
                result.AddRange(GetIdxItem_Reg_Name("InfantryTypes"));
                result.AddRange(GetIdxItem_Reg_Name("VehicleTypes"));
                result.AddRange(GetIdxItem_Reg_Name("AircraftTypes"));
                result.AddRange(GetIdxItem_Reg_Name("BuildingTypes"));
                return result;
            }
        }
        public IEnumerable<IIndexableItem> TaskforceUnitAvailableList
        {
            get
            {
                List<IIndexableItem> result = new List<IIndexableItem>();
                result.AddRange(GetIdxItem_Reg_Name("InfantryTypes"));
                result.AddRange(GetIdxItem_Reg_Name("VehicleTypes"));
                result.AddRange(GetIdxItem_Reg_Name("AircraftTypes")); 
                return result;
            }
        }
        public IEnumerable<IIndexableItem> GlobalVar
        {
            get
            {
                List<ComboItem> result = new List<ComboItem>();
                INIEntity ent = this["VariableNames"];
                foreach (INIPair p in ent)
                {
                    result.Add(new ComboItem(p.Name, p.Value));
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
