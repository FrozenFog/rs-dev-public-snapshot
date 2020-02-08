using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using relert_sharp.IniSystem;

namespace relert_sharp.MapStructure.Logic
{
    public class CountryCollection : TeamLogicCollection
    {
        public CountryCollection() { }


        #region Public Methods - CountryCollection
        #endregion
    }


    public class CountryItem : TeamLogicItem
    {
        private Dictionary<string, INIPair> residual = new Dictionary<string, INIPair>();


        #region Constructor - CountryItem
        public CountryItem(INIEntity ent) : base(ent)
        {
            Name = ent.PopPair("Name").Value;
            Side = ent.PopPair("Side").Value;
            ColorName = ent.PopPair("Color").Value;
            Prefix = ent.PopPair("Prefix").Value;
            Suffix = ent.PopPair("Suffix").Value;
            SmartAI = ent.PopPair("SmartAI").ParseBool();
            ParentCountryName = ent.PopPair("ParentCountry").Value;
            residual = ent.DictData;
        }
        #endregion


        #region Public Methods - CountryItem
        public override string ToString()
        {
            return Name;
        }
        #endregion


        #region Public Calls - CountryItem
        public string Name { get; set; }
        public string Side { get; set; }
        public string ColorName { get; set; }
        public string ParentCountryName { get; set; }
        public string Suffix { get; set; }
        public string Prefix { get; set; }
        public bool SmartAI { get; set; }
        #endregion
    }
}
