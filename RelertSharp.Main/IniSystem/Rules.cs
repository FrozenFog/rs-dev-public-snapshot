﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using relert_sharp.Common;

namespace relert_sharp.IniSystem
{
    public class Rules : INIFile
    {
        #region Constructor - Rules
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
        #endregion


        #region Public Calls - Rules
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
        public List<TechnoPair> SuperWeaponList
        {
            get
            {
                return GetTechnoPairs("SuperWeaponTypes", TechnoPair.AbstractType.RegName);
            }
        }
        public List<TechnoPair> WeaponList
        {
            get
            {
                return GetTechnoPairs("WeaponTypes", TechnoPair.AbstractType.RegName);
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
