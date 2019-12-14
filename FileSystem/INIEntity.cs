using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using relert_sharp.Common;

namespace relert_sharp.FileSystem
{
    public class INIEntity
    {
        private string name;
        private string comment;
        private List<INIPair> data = new List<INIPair>();
        private List<string> pairNameList = new List<string>();
        private INIEntType entitytype;

        public INIEntity(List<INIPair> pairs, string n, string com = "")
        {
            data = pairs;
            name = n;
            comment = com;
            foreach (INIPair p in data) pairNameList.Add(p.Name);
            if (Constant.EntName.SystemEntity.Contains(n)) entitytype = INIEntType.SystemType;
            else if (Constant.EntName.DictionaryList.Contains(n)) entitytype = INIEntType.ListType;
            else if (Constant.EntName.MapList.Contains(n)) entitytype = INIEntType.MapType;
            else entitytype = INIEntType.DefaultType;
        }
        public INIEntity(string _name, string packString, int startIndex)
        {
            name = _name;
            for (int i = 0; i < packString.Length; i++)
            {
                data.Add(new INIPair(startIndex.ToString(), packString.Substring(i * 70, 70), ""));
                pairNameList.Add(startIndex.ToString());
                startIndex++;
            }
            entitytype = INIEntType.ListType;
        }
        #region Public Methods - INIEntity
        public List<string> TakeValuesToList()
        {
            List<string> result = new List<string>();
            foreach(INIPair p in data) if(!result.Contains(p.Value)) result.Add(p.Value);
            return result;
        }
        public INIPair GetPair(string pairName)
        {
            if (pairNameList.Contains(pairName)) return data[pairNameList.IndexOf(pairName)];
            return INIPair.NullPair;
        }
        public bool HasPair(INIPair p)
        {
            return pairNameList.Contains(p.Name);
        }
        public void ConvPairs()
        {
            if (entitytype != INIEntType.SystemType || entitytype != INIEntType.MapType)
                foreach (INIPair p in data) p.ConvValue();
        }
        public void RemovePair(INIPair p)
        {
            data.Remove(p);
            pairNameList.Remove(p.Name);
        }
        public string JoinString()
        {
            string result = "";
            foreach (INIPair p in data)
            {
                result += p.Value.ToString();
            }
            return result;
        }
        #endregion
        #region Public Calls
        public string Name
        {
            get { return name; }
        }
        public List<INIPair> DataList
        {
            get { return data; }
        }
        public static INIEntity NullEntity
        {
            get { return new INIEntity(new List<INIPair>(), ""); }
        }
        #endregion
    }
}
