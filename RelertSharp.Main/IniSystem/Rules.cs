using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        #region Ctor - Rules
        public Rules(string path, INIFileType itype = INIFileType.RulesINI) : base(path, INIFileType.RulesINI) { }
        public Rules(byte[] _data, string _filename) : base(_data, _filename, INIFileType.RulesINI) { }
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
        private string GuessStructureName(string id)
        {
            if (string.IsNullOrEmpty(id)) return "";
            string name = id + ".shp";
            string gname = Replace(name, 1, 'G');
            if (!GlobalConfig.IgnoreBuildingTheaterArt)
            {
                name = Replace(id + ".shp", 1, _suff);
                if (GlobalDir.HasFile(name)) return name;
            }
            if (GlobalDir.HasFile(gname)) return gname;
            if (GlobalDir.HasFile(name)) return name;
            return "";
        }
        private string GuessStructureName(INIEntity ent)
        {
            string img = ent["Image"];
            if (string.IsNullOrEmpty(img)) return GuessStructureName(ent.Name);
            else return GuessStructureName(img);
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
        public string GetCsfUIName(string regid)
        {
            if (!HasIniEnt(regid)) return GlobalCsf[regid].ContentString;
            string uiname = this[regid]["UIName"];
            return GlobalCsf[uiname].ContentString;
        }
        public string GetCustomPaletteName(string nameid)
        {
            string artname = GetArtEntityName(nameid);
            INIEntity art = Art[artname];

            string pal = art["Palette"];
            if (string.IsNullOrEmpty(pal)) return pal;
            else return string.Format("{0}{1}.{2}", pal, TileDictionary.TheaterSub, "pal");
        }
        public void GetBuildingShapeData(string nameid, out int height, out int foundX, out int foundY)
        {
            string artname = GetArtEntityName(nameid);
            INIEntity art = Art[artname];
            string img = this[nameid]["Image"];

            string foundation = (string)art["Foundation"].ToLower();
            if (!string.IsNullOrEmpty(foundation))
            {
                if (foundation == "custom")
                {
                    foundX = art.GetPair("Foundation.X").ParseInt(1);
                    foundY = art.GetPair("Foundation.Y").ParseInt(1);
                }
                else
                {
                    string[] tmp = foundation.Split('x');
                    foundX = int.Parse(tmp[0]);
                    foundY = int.Parse(tmp[1]);
                }
            }
            else
            {
                foundX = 1;
                foundY = 1;
            }
            height = art.GetPair("Height").ParseInt(5) + 3;
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
            if (direction == 7) direction = 0;
            else direction++;
            if (sequence.Name == "") return direction;
            int[] ready = sequence.GetPair("Ready").ParseIntList();
            int result = ready[0];
            return result + direction;
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
            out short nSelf, out short nAnim, out short nTurret, out short nBib, out short nIdle, out short nAnim2, out short nAnim3, out short nSuper)
        {
            string artname = GetArtEntityName(id);
            INIEntity art = Art[artname];
            string img;
            if (string.IsNullOrEmpty(art.Name)) img = id + ".shp";
            else img = GuessStructureName(art);
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
            string img = GetArtEntityName(inf.NameID) + ".shp";
            frame = GlobalDir.GetShpFrameCount(img);
            return img;
        }
        public void LoadArt(INIFile f)
        {
            Art = f;
        }
        public string GetOverlayName(byte overlayid)
        {
            return this["OverlayTypes"][overlayid.ToString()];
        }
        #endregion


        #region Public Calls - Rules
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
                return GetTechnoPairs(this["Movies"]);
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
