using RelertSharp.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RelertSharp.IniSystem
{
    [Serializable]
    public class INIEntity : IEnumerable<INIPair>, IDisposable, IChecksum
    {
        private string name, comment, preComment;
        private Dictionary<string, INIPair> data = new Dictionary<string, INIPair>();
        private INIEntType entitytype;


        #region Ctor - INIEntity
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
        public INIEntity(string name)
        {
            this.name = name;
        }
        public INIEntity(INIEntity src, string rename = "")
        {
            foreach (var kvpair in src.data) data[kvpair.Key] = new INIPair(kvpair.Value);
            Name = string.IsNullOrEmpty(rename) ? src.Name : rename;
        }
        #endregion


        #region Public Methods - INIEntity
        public int GetChecksum()
        {
            unchecked
            {
                int hash = Constant.BASE_HASH;
                foreach (INIPair p in this) hash = hash * 23 + p.GetChecksum();
                return hash;
            }
        }
        public int GetMaxIndex()
        {
            int mx = 0;
            foreach (string index in data.Keys)
            {
                if (!int.TryParse(index, out int i)) break;
                mx = Math.Max(mx, i);
            }
            return mx;
        }
        /// <summary>
        /// Returns the index of the designated pair VALUE. Return -1 when not found
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int IndexOfValue(string value)
        {
            int i = 0;
            foreach (INIPair p in this)
            {
                if (p.Value == value) return i;
                i++;
            }
            return -1;
        }
        /// <summary>
        /// Rename the key of each item as indexer, start from 0(def). Similar with IsoMapPack5.
        /// </summary>
        /// <param name="firstindex">Key num for first item</param>
        /// <returns>Max index</returns>
        public int Reorganize(int firstindex = 0)
        {
            Dictionary<string, INIPair> d = new Dictionary<string, INIPair>();
            foreach (INIPair p in data.Values)
            {
                p.Name = firstindex++.ToString();
                d[p.Name] = p;
            }
            data = d;
            return firstindex;
        }
        /// <summary>
        /// Similar to Reorganize, but returns the reorganized entity result, 
        /// will not modify the original
        /// </summary>
        /// <param name="firstindex"></param>
        /// <returns></returns>
        public INIEntity ReorganizeTo(int firstindex = 0)
        {
            INIEntity result = new INIEntity();
            HashSet<string> values = new HashSet<string>();
            foreach (INIPair p in data.Values)
            {
                if (!values.Contains(p.Value))
                {
                    INIPair np = new INIPair(p)
                    {
                        Name = firstindex++.ToString()
                    };
                    result.AddPair(np);
                    values.Add(p.Value);
                }
            }
            return result;
        }
        /// <summary>
        /// Similar to JoinWith, but remain original kvpair when the src has same kvpair
        /// </summary>
        /// <param name="src"></param>
        public void MergeWith(INIEntity src)
        {
            if (entitytype == INIEntType.ListType)
            {
                int maxindex = Reorganize();
                foreach (INIPair p in src)
                {
                    p.Name = maxindex++.ToString();
                    data[p.Name] = p;
                }
            }
            else
            {
                foreach (INIPair p in src)
                {
                    if (data.Keys.Contains(p.Name)) continue;
                    else data[p.Name] = p;
                }
            }
        }
        /// <summary>
        /// Merge two IniEnt, items with same key will be overwrite by new one.
        /// </summary>
        /// <param name="newEnt">New Ent</param>
        public void JoinWith(INIEntity newEnt)
        {
            if (entitytype == INIEntType.ListType)
            {
                HashSet<string> org = data.Values.Select(x => x.Value).ToHashSet();
                foreach (INIPair p in newEnt)
                {
                    if (org.Contains(p.Value))
                    {
                        org.Remove(p.Value);
                    }
                    org.Add(p.Value);
                }
                data.Clear();
                int i = 0;
                foreach (string s in org)
                {
                    string key = i++.ToString();
                    data[key] = new INIPair(key, s);
                }
            }
            else
            {
                foreach (INIPair p in newEnt)
                {
                    data[p.Name] = p;
                }
            }
        }
        /// <summary>
        /// Find and delete a specified Pair, return a NullPair(not null) if not found.
        /// </summary>
        /// <param name="pairKey"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Add a Pair.
        /// </summary>
        /// <param name="p"></param>
        public void AddPair(INIPair p)
        {
            if (!data.Keys.Contains(p.Name)) data[p.Name] = p;
        }

        public void AddPair(string key, object value)
        {
            string s = value == null ? string.Empty : value.ToString();
            if (!data.Keys.Contains(key)) data[key] = new INIPair(key, s);
        }
        public void AddPair(IEnumerable<INIPair> src)
        {
            foreach (INIPair p in src) AddPair(p);
        }
        /// <summary>
        /// Discard all item index and merge the value into a list, works well with List-like Ent.
        /// Any repeated value will be discarded.
        /// Eg: [InfantryTypes]
        /// </summary>
        /// <returns></returns>
        public List<string> TakeValuesToList()
        {
            List<string> result = new List<string>();
            foreach (INIPair p in data.Values) if (!result.Contains(p.Value)) result.Add(p.Value);
            return result;
        }
        public INIPair GetPair(int index)
        {
            return data[data.Keys.ElementAt(index)];
        }
        public string GetString(string key, string def = "")
        {
            if (data.ContainsKey(key)) return data[key].Value.ToString();
            else return def;
        }
        public INIPair Find(Predicate<INIPair> predicate)
        {
            foreach (INIPair p in this)
            {
                if (predicate(p)) return p;
            }
            return INIPair.NullPair;
        }
        /// <summary>
        /// Find and return specified IniPair, return NullPair(not null) if not found.
        /// </summary>
        /// <param name="pairName"></param>
        /// <returns></returns>
        public INIPair GetPair(string pairName)
        {
            if (data.Keys.Contains(pairName)) return data[pairName];
            return INIPair.NullPair;
        }
        /// <summary>
        /// Return true if this contains sanme key as P
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public bool HasPair(INIPair p)
        {
            return data.Keys.Contains(p.Name);
        }
        public bool HasPair(string keyname)
        {
            return data.Keys.Contains(keyname);
        }
        /// <summary>
        /// Remove a specified IniPair
        /// </summary>
        /// <param name="key"></param>
        public void RemovePair(string key)
        {
            data.Remove(key);
        }
        /// <summary>
        /// Remove a specified IniPair
        /// </summary>
        /// <param name="p"></param>
        public void RemovePair(INIPair p)
        {
            data.Remove(p.Name);
        }
        /// <summary>
        /// Set a specified IniPair's Value
        /// </summary>
        /// <param name="p"></param>
        public void SetPair(INIPair p)
        {
            data[p.Name] = p;
        }
        /// <summary>
        /// Set a specified IniPair's Value
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val"></param>
        public void SetPair(string key, dynamic val)
        {
            if (data.ContainsKey(key))
                data[key].Value = val;
        }
        /// <summary>
        /// Set a specified IniPair's Value
        /// </summary>
        /// <param name="p"></param>
        /// <param name="val"></param>
        public void SetPair(INIPair p, dynamic val)
        {
            if (data.ContainsKey(p.Name))
                data[p.Name].Value = val;
        }
        /// <summary>
        /// Discard all item index and merge all value into a single string, dedicated for IsoMapPack-like Ent
        /// </summary>
        /// <returns></returns>
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
        public bool ParseBool(string key, bool def = false)
        {
            return GetPair(key).ParseBool(def);
        }
        public int ParseInt(string key, int def = 0)
        {
            return GetPair(key).ParseInt(def);
        }
        public float ParseFloat(string key, float def = 0f)
        {
            return GetPair(key).ParseFloat(def);
        }
        public string[] ParseStringList(string key)
        {
            return GetPair(key).ParseStringList();
        }
        public int[] ParseIntList(string key)
        {
            return GetPair(key).ParseIntList();
        }
        public override string ToString()
        {
            return string.Format("[{0}]:{1} items", Name, data.Count);
        }
        public string SaveString(bool ignoreComment = false)
        {
            StringBuilder sb = new StringBuilder();
            string result = string.Format("[{0}]", Name);
            if (!ignoreComment)
            {
                result = preComment + result;
                if (!comment.IsNullOrEmpty()) result += string.Format(";{0}", comment);
            }
            sb.AppendLine(result);
            foreach (INIPair p in this) sb.AppendLine(p.SaveString(ignoreComment));
            return sb.ToString() + "\n";
        }
        // Just for using, no real use
        public void Dispose()
        {
            return;
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
        public bool IsEmpty { get { return data.Count == 0; } }
        public bool IsSystemList { get { return entitytype == INIEntType.ListType; } }
        public Dictionary<string, INIPair> DictData { get { return data; } }
        public string Comment { get { return comment; } }
        public string PreComment { get { return preComment; } }
        public string Name { get { return name; } private set { name = value; } }
        public bool HasComment { get { return !string.IsNullOrEmpty(comment); } }
        /// <summary>
        /// Before converting, every value is string-type
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string this[string key]
        {
            get
            {
                if (data.Keys.Contains(key)) return data[key].Value;
                else return "";
            }
            set
            {
                if (data.Keys.Contains(key)) data[key].Value = value;
                else data[key] = new INIPair(key, value);
            }
        }
        public List<INIPair> DataList { get { return data.Values.ToList(); } }
        public static INIEntity NullEntity { get { return new INIEntity("", "", ""); } }
        #endregion
    }
}
