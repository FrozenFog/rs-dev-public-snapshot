using RelertSharp.Common;
using RelertSharp.IniSystem;
using RelertSharp.IniSystem.Serialization;
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
        public CountryItem GetCountry(string name)
        {
            foreach (CountryItem c in this)
            {
                if (c.Name == name) return c;
            }
            return null;
        }
        #endregion
    }


    public class CountryItem : TeamLogicItem, IIndexableItem, ILogicItem
    {
        private Dictionary<string, INIPair> residual = new Dictionary<string, INIPair>();
        public NameChangedHandler CountryNameChanged;
        public EventHandler AllInfoUpdated;
        private bool initialized = false;


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
            initialized = true;
        }
        public CountryItem() { initialized = true; }
        public static CountryItem ParseFromRules(INIEntity src)
        {
            CountryItem c = new CountryItem()
            {
                Name = src.Name,
                Side = src.GetString("Side"),
                ColorName = src.GetString("Color"),
                Prefix = src.GetString("Prefix"),
                Suffix = src.GetString("Suffix"),
                ParentCountryName = src.Name,
                SmartAi = true,
                residual = new Dictionary<string, INIPair>(),
                initialized = true
            };
            return c;
        }
        public static CountryItem CreateEmpty(INIEntity src)
        {
            CountryItem c = new CountryItem()
            {
                Name = Constant.DefaultHouseName,
                Side = src.GetString("Side"),
                ColorName = src.GetString("Color"),
                Prefix = src.GetString("Prefix"),
                Suffix = src.GetString("Suffix"),
                ParentCountryName = src.Name,
                SmartAi = true,
                residual = new Dictionary<string, INIPair>(),
                initialized = true
            };
            return c;
        }
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
        public string[] ExtractParameter()
        {
            return new string[]
            {
                Name,
                ColorName,
                ParentCountryName,
                Suffix,
                Prefix,
                Side,
                SmartAi.ZeroOne()
            };
        }
        public int GetChecksum()
        {
            return GetSaveData().GetChecksum();
        }
        public override string Value { get { return Name; } }
        public override string ToString()
        {
            return Name;
        }
        internal void OnAllInfoUpdated()
        {
            AllInfoUpdated?.Invoke(null, null);
        }
        #endregion


        #region Public Calls - CountryItem
        [IniHeader]
        public override string Id
        {
            get { return Name; }
            set { Name = value; }
        }
        [IniPair]
        public string Side { get; set; }
        [IniPair("Color")]
        public string ColorName { get; set; }
        [IniPair("ParentCountry")]
        public string ParentCountryName { get; set; }
        [IniPair]
        public string Suffix { get; set; }
        [IniPair("Prefix")]
        public string Prefix { get; set; }
        [IniPair("SmartAI")]
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
        [IniPair]
        public override string Name
        {
            get { return base.Name; }
            set
            {
                string prev = base.Name;
                base.Name = value;
                if (initialized) CountryNameChanged?.Invoke(this, prev, value);
            }
        }
        public LogicType ItemType { get { return LogicType.Country; } }
        #endregion
    }
}
