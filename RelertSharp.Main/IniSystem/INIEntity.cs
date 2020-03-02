using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using relert_sharp.Common;

namespace relert_sharp.IniSystem
{
    public class INIEntity : IEnumerable<INIPair>
    {
        private string name, comment, preComment;
        private Dictionary<string, INIPair> data = new Dictionary<string, INIPair>();
        private INIEntType entitytype;


        #region Constructor - INIEntity
        public INIEntity() { }
        public INIEntity(string _name, string _preComment, string _comment)
        {
            name = _name;
            comment = _comment;
            preComment = _preComment;
            if (Constant.EntName.SystemEntity.Contains(_name)) entitytype = INIEntType.SystemType;
            else if (Constant.EntName.DictionaryList.Contains(_name)) entitytype = INIEntType.ListType;
            else if (Constant.EntName.MapList.Contains(_name)) entitytype = INIEntType.MapType;
            else entitytype = INIEntType.DefaultType;
        }
        public INIEntity(string _name, string packString, int startIndex)
        {
            name = _name;
            if (!string.IsNullOrEmpty(packString))
            {
                for (int i = 0; i < packString.Length; i += 70)
                {
                    int remain = packString.Length - i;
                    AddPair(new INIPair(startIndex.ToString(), packString.Substring(i, Math.Min(70, remain)), "", ""));
                    startIndex++;
                }
            }
            entitytype = INIEntType.ListType;
        }
        #endregion


        #region Public Methods - INIEntity
        public void JoinWith(INIEntity newEnt)
        {
            foreach (INIPair p in newEnt)
            {
                data[p.Name] = p;
            }
        }
        public TechnoPair ToTechno(string index, TechnoPair.AbstractType type = TechnoPair.AbstractType.RegName)
        {
            TechnoPair tp = new TechnoPair(this, index, type);
            return tp;
        }
        public INIPair PopPair(string pairKey)
        {
            if (data.Keys.Contains(pairKey))
            {
                INIPair p = data[pairKey];
                data.Remove(pairKey);
                return p;
            }
            else
            {
                return INIPair.NullPair;
            }
        }
        public void AddPair(INIPair p)
        {
            if (!data.Keys.Contains(p.Name)) data[p.Name] = p;
        }
        public List<string> TakeValuesToList()
        {
            List<string> result = new List<string>();
            foreach(INIPair p in data.Values) if(!result.Contains(p.Value)) result.Add(p.Value);
            return result;
        }
        public INIPair GetPair(string pairName)
        {
            if (data.Keys.Contains(pairName)) return data[pairName];
            return INIPair.NullPair;
        }
        public bool HasPair(INIPair p)
        {
            return data.Keys.Contains(p.Name);
        }
        public void ConvPairs()
        {
            if (entitytype != INIEntType.SystemType || entitytype != INIEntType.MapType)
                foreach (INIPair p in data.Values) p.ConvValue();
        }
        public void RemovePair(INIPair p)
        {
            data.Remove(p.Name);
        }
        public string JoinString()
        {
            int num = data.Count * 70;
            StringBuilder sb = new StringBuilder(num);
            foreach (INIPair p in data.Values)
            {
               sb.Append(p.Value.ToString());
            }
            return sb.ToString();
        }
        #region Enumerator
        public IEnumerator<INIPair> GetEnumerator()
        {
            return data.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return data.Values.GetEnumerator();
        }
        #endregion
        #endregion


        #region Public Calls
        public Dictionary<string, INIPair> DictData { get { return data; } }
        public string Comment { get { return comment; } }
        public string PreComment { get { return preComment; } }
        public string Name { get { return name; } }
        public bool HasComment { get { return !string.IsNullOrEmpty(comment); } }
        public dynamic this[string key]
        {
            get
            {
                if (data.Keys.Contains(key)) return data[key].Value;
                else return "";
            }
        }
        public List<INIPair> DataList { get { return data.Values.ToList(); } }
        public static INIEntity NullEntity { get { return new INIEntity("","",""); } }
        #endregion
    }
}
