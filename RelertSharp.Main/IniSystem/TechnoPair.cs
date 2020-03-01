using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace relert_sharp.IniSystem
{
    public class TechnoPair
    {
        private string abst = "";
        public enum AbstractType
        {
            Default = 0,
            Name, RegName, UIName, IndexOnly
        }


        #region Constructor - TechnoPair
        public TechnoPair(INIEntity ent, string index, AbstractType abstractType = AbstractType.RegName)
        {
            Index = index;
            Name = ent["Name"];
            RegName = ent.Name;
            UIName = ent["UIName"];
            switch (abstractType)
            {
                default:
                case AbstractType.RegName:
                    abst = RegName;
                    break;
                case AbstractType.Name:
                    abst = Name;
                    break;
                case AbstractType.UIName:
                    abst = UIName;
                    break;
                case AbstractType.IndexOnly:
                    abst = "";
                    break;
            }
        }
        public TechnoPair(string index, string regname)
        {
            Index = index;
            RegName = regname;
            abst = regname;
        }
        #endregion


        #region Public Methods - TechnoPair
        public override string ToString()
        {
            return Index + " " + abst;
        }
        #endregion


        #region Public Calls - TechnoPair
        public string Index { get; set; } = "";
        public string Name { get; set; } = "";
        public string RegName { get; set; } = "";
        public string UIName { get; set; } = "";
        #endregion
    }
}
