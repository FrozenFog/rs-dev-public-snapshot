using RelertSharp.Common;
using RelertSharp.IniSystem;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RelertSharp.MapStructure.Logic
{
    public class CountryCollection : TeamLogicCollection<CountryItem>
    {
        #region Ctor - CountryCollection
        public CountryCollection() { }
        #endregion


        #region Public Methods - CountryCollection

        public IEnumerable<TechnoPair> ToTechno()
        {
            List<TechnoPair> result = new List<TechnoPair>();
            foreach (CountryItem item in this)
            {
                result.Add(new TechnoPair(item.Index, item.Name));
            }
            return result.OrderBy(x => int.Parse(x.Index));
        }
        public CountryItem GetCountry(string name)
        {
            foreach (CountryItem c in this)
            {
                if (c.Name == name) return c;
            }
            return null;
        }
        public int NewIndex()
        {
            for (int i = 0; i < int.MaxValue; ++i)
                if (!Exists(i.ToString())) return i;
            return -1;
        }
        #endregion
    }


    public class CountryItem : TeamLogicItem, IIndexableItem
    {
        private Dictionary<string, INIPair> residual = new Dictionary<string, INIPair>();


        #region Ctor - CountryItem
        public CountryItem(INIEntity ent) : base(ent)
        {
            Name = ent.PopPair("Name").Value;
            Side = ent.PopPair("Side").Value;
            ColorName = ent.PopPair("Color").Value;
            Prefix = ent.PopPair("Prefix").Value;
            Suffix = ent.PopPair("Suffix").Value;
            ParentCountryName = ent.PopPair("ParentCountry").Value;
            SmartAi = ent.PopPair("SmartAI").ParseBool(true);
            residual = ent.DictData;
        }
        public CountryItem() { }
        #endregion


        #region Public Methods - CountryItem
        public INIEntity GetSaveData()
        {
            INIEntity data = new INIEntity(Name);
            data.AddPair("Name", Name);
            data.AddPair("Color", ColorName);
            data.AddPair("ParentCountry", ParentCountryName);
            data.AddPair("Suffix", Suffix);
            data.AddPair("Prefix", Prefix);
            data.AddPair("Side", Side);
            data.AddPair("SmartAI", SmartAi.YesNo());
            data.AddPair(residual.Values);
            return data;
        }
        public override string Value { get { return Name; } }
        public override string ToString()
        {
            return Name;
        }
        #endregion


        #region Public Calls - CountryItem
        public override string Id
        {
            get { return Index; }
            set { Index = value; }
        }
        public string Index { get; set; }
        public string Side { get; set; }
        public string ColorName { get; set; }
        public string ParentCountryName { get; set; }
        public string Suffix { get; set; }
        public string Prefix { get; set; }
        public bool SmartAi { get; set; }
        public IEnumerable<INIPair> Residual
        {
            get { return residual.Values; }
            set
            {
                Dictionary<string, INIPair> r = new Dictionary<string, INIPair>();
                foreach (INIPair p in value) r[p.Name] = p;
                residual = r;
            }
        }
        #endregion
    }
}
