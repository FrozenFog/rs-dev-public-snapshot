﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.Common;
using RelertSharp.MapStructure.Objects;
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
                switch (Common.GlobalVar.CurrentTheater)
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
            string name = Replace(id + ".shp", 1, _suff);
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
        #endregion


        #region Public Methods - Rules
        public string GetCustomPaletteName(string nameid)
        {
            string art = this[nameid]["Image"];
            if (!string.IsNullOrEmpty(art) && art.ToLower() != nameid.ToLower())
            {
                if (HasIniEnt(art))
                {
                    nameid = art;
                }
            }

            string pal = this[nameid]["Palette"];
            if (string.IsNullOrEmpty(pal)) return pal;
            else return string.Format("{0}{1}.{2}", pal, TileDictionary.TheaterSub, "pal");
        }
        public void GetBuildingShapeData(string nameid, out int height, out int foundX, out int foundY)
        {
            string img = this[nameid]["Image"];
            if (!string.IsNullOrEmpty(img) && img.ToLower() != nameid.ToLower())
            {
                if (HasIniEnt(img))
                {
                    nameid = img;
                }
            }

            string foundation = this[nameid]["Foundation"];
            if (!string.IsNullOrEmpty(foundation))
            {
                if (foundation == "Custom")
                {
                    foundX = this[nameid].GetPair("Foundation.X").ParseInt(1);
                    foundY = this[nameid].GetPair("Foundation.Y").ParseInt(1);
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
            height = this[nameid].GetPair("Height").ParseInt(5) + 3;
        }
        public bool IsVxl(string id)
        {
            return ParseBool(this[id]["Voxel"]);
        }
        public int GetFrameFromDirection(int direction, string nameID)
        {
            string img = GetObjectImgName(nameID);
            INIEntity sequence = this[this[img]["Sequence"]];
            direction >>= 5;
            if (direction == 7) direction = 0;
            else direction++;
            if (sequence.Name == "") return direction;
            int[] ready = sequence.GetPair("Ready").ParseIntList();
            int result = ready[0];
            return result + direction;
        }
        public string GetObjectImgName(string nameID)
        {
            string img = this[nameID]["Image"];
            if (string.IsNullOrEmpty(img)) img = nameID;
            if (ParseBool(this[nameID]["AlternateTheaterArt"]))
            {
                if (GlobalDir.HasFile(img + _suff + ".shp")) img += _suff;
            }
            return img;
        }
        public DrawingEngine.Pnt GetVoxTurOffset(string id)
        {
            string x = this[id]["TurretAnimX"];
            string y = this[id]["TurretAnimY"];
            return new DrawingEngine.Pnt()
            {
                X = string.IsNullOrEmpty(x) ? 0 : int.Parse(x),
                Y = string.IsNullOrEmpty(y) ? 0 : int.Parse(y)
            };
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
        public string GetUnitImgName(string id, ref string tur, ref string barl, ref bool vxl)
        {
            string img = this[id]["Image"];
            if (string.IsNullOrEmpty(img))
            {
                vxl = IsVxl(id);
                VxlFormating(id, vxl, ref img, ref tur, ref barl);
            }
            else
            {
                if (IniDict.Keys.Contains(img))
                {
                    string subimg = this[img]["Image"];
                    if (string.IsNullOrEmpty(subimg))
                    {
                        vxl = IsVxl(img);
                        VxlFormating(img, vxl, ref img, ref tur, ref barl);
                    }
                    else
                    {
                        vxl = IsVxl(subimg);
                        VxlFormating(subimg, vxl, ref img, ref tur, ref barl);
                    }
                }
                else
                {
                    vxl = IsVxl(img);
                    VxlFormating(img, vxl, ref img, ref tur, ref barl);
                }
            }
            return img;
        }
        public INIEntity GetBuildingTurret(string nameid)
        {
            INIEntity art = this[nameid];
            string img = this[nameid]["Image"];
            string turretAnim = art["TurretAnim"];
            if (!string.IsNullOrEmpty(turretAnim)) return this[turretAnim];
            if (string.IsNullOrEmpty(img)) art = this[nameid];
            else
            {
                if (HasIniEnt(img)) art = this[img];
                else art = this[nameid];
            }
            return this[art["TurretAnim"]];
        }
        public string GetObjectImgName(string id, ref string anim, ref string turret, ref string bib, ref bool isVox, ref string idle, ref string anim2, ref string anim3, ref string barl)
        {
            string img = this[id]["Image"];
            INIEntity art;
            if (string.IsNullOrEmpty(img))
            {
                art = this[id];
                img = GuessStructureName(id);
                if (string.IsNullOrEmpty(img)) img = id + ".shp";
            }
            else
            {
                if (IniDict.Keys.Contains(img))
                {
                    art = this[img];
                    string subimg = GuessStructureName(this[img]);
                    if (string.IsNullOrEmpty(subimg)) subimg = GuessStructureName(img);
                    img = subimg;
                }
                else
                {
                    img = GuessStructureName(img);
                    art = this[id];
                }
            }
            anim = GuessStructureName(this[art["ActiveAnim"]]);
            idle = GuessStructureName(this[art["IdleAnim"]]);
            anim2 = GuessStructureName(this[art["ActiveAnimTwo"]]);
            anim3 = GuessStructureName(this[art["ActiveAnimThree"]]);
            bib = GuessStructureName(art["BibShape"]);
            isVox = ParseBool(this[id]["TurretAnimIsVoxel"]);
            if (isVox)
            {
                turret = this[id]["TurretAnim"] + ".vxl";
                barl = turret.ToLower().Replace("tur", "barl");
            }
            else turret = GuessStructureName(this[this[id]["TurretAnim"]]);
            return img;
        }
        public string GetObjectImgName(ObjectItemBase inf)
        {
            string img = GetObjectImgName(inf.NameID);
            return img + ".shp";
        }
        public void LoadArt(INIFile f)
        {
            Override(f.IniData);
        }
        public string GetOverlayName(byte overlayid)
        {
            return this["OverlayTypes"][overlayid.ToString()];
        }
        #endregion


        #region Public Calls - Rules
        public Dictionary<string, INIEntity> MapIniData { get; set; }
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
