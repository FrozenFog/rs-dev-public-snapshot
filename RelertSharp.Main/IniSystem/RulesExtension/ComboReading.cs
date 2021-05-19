using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.Common;

namespace RelertSharp.IniSystem
{
    public static class RulesComboExtension
    {
        private static IEnumerable<ComboItem> GetComboItems_Index_Name(INIFile r, string listName)
        {
            INIEntity list = r[listName];
            List<ComboItem> result = new List<ComboItem>();
            int i = 0;
            foreach (INIPair p in list)
            {
                INIEntity ent = r[p.Value];
                string value = string.Format("{0}({1})", ent[Constant.KEY_NAME], p.Value);
                result.Add(new ComboItem(i++.ToString(), value));
            }
            return result;
        }
        private static IEnumerable<ComboItem> GetComboFromRules(INIFile r, string listName, bool zeroIdx = false, bool regOnly = false)
        {
            INIEntity entList = r[listName];
            List<ComboItem> result = new List<ComboItem>();
            if (zeroIdx)
            {
                int i = 0;
                foreach (INIPair pair in entList)
                {
                    result.Add(new ComboItem(i++.ToString(), pair.Value));
                }
            }
            else if (regOnly)
            {
                foreach (INIPair pair in entList)
                {
                    result.Add(new ComboItem(pair.Value));
                }
            }
            else
            {
                foreach (INIPair pair in entList)
                {
                    result.Add(new ComboItem(pair.Name, pair.Value));
                }
            }
            return result;
        }
        private static IEnumerable<ComboItem> Wrapper(INIFile r, string title, bool zeroIdx, bool regOnly = false)
        {
            string key = title + zeroIdx.ZeroOne() + regOnly.ZeroOne();
            if (!buffer.ContainsKey(key)) buffer[key] = GetComboFromRules(r, title, zeroIdx, regOnly);
            return buffer[key];
        }
        private static Dictionary<string, IEnumerable<ComboItem>> buffer = new Dictionary<string, IEnumerable<ComboItem>>();

        public static IEnumerable<ComboItem> VehicleList(this Rules r, bool zeroIdx = false)
        {
            return GetComboItems_Index_Name(r, "VehicleTypes");
        }
        public static IEnumerable<ComboItem> InfantryList(this Rules r, bool zeroIdx = false)
        {
            return GetComboItems_Index_Name(r, "InfantryTypes");
        }
        public static IEnumerable<ComboItem> AircraftList(this Rules r, bool zeroIdx = false)
        {
            return GetComboItems_Index_Name(r, "AircraftTypes");
        }
        public static IEnumerable<ComboItem> BuildingList(this Rules r, bool zeroIdx = false, bool regOnly = false)
        {
            return GetComboItems_Index_Name(r, "BuildingTypes");
        }
        public static IEnumerable<ComboItem> SuperWeaponList(this Rules r, bool zeroIdx = false, bool regOnly = false)
        {
            return Wrapper(r, "SuperWeaponTypes", zeroIdx);
        }
        public static IEnumerable<ComboItem> WeaponList(this Rules r, bool zeroIdx = false)
        {
            return Wrapper(r, "WeaponTypes", zeroIdx);
        }
        public static IEnumerable<ComboItem> AnimationList(this Rules r, bool zeroIdx = false)
        {
            return Wrapper(r, "Animations", zeroIdx);
        }
        public static IEnumerable<ComboItem> ParticalList(this Rules r, bool zeroIdx = false)
        {
            return Wrapper(r, "Particles", zeroIdx);
        }
        public static IEnumerable<ComboItem> VoxAnimList(this Rules r, bool zeroIdx = false)
        {
            return Wrapper(r, "VoxelAnims", zeroIdx);
        }
        public static IEnumerable<ComboItem> MovieList(this Rules r, bool zeroIdx = false)
        {
            return Wrapper(r.Art, "Movies", zeroIdx);
        }
        public static IEnumerable<ComboItem> GlobalVarList(this Rules r, bool zeroIdx = false)
        {
            return Wrapper(r, "VariableNames", zeroIdx);
        }
    }
}
