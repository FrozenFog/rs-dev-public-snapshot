using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using relert_sharp.Utils;

namespace relert_sharp.FileSystem
{
    public class INIFile
    {
        private Dictionary<string, string> comment = new Dictionary<string, string>();
        private List<INIEntity> inidata = new List<INIEntity>();
        private List<string> entityNameList = new List<string>();
        private Constant.INIFileType initype;
        private string filename;
        private string fullname;
        private string filepath;
        private string nameext;
        public INIFile(string path,  Constant.INIFileType itype = Constant.INIFileType.DefaultINI)
        {
            bool init = true;
            initype = itype;
            Utils.File f = new Utils.File(path, FileMode.Open, FileAccess.Read);
            filename = f.FileName;
            fullname = f.FullName;
            filepath = f.FilePath;
            nameext = f.NameExt;
            List<string> data = f.readlines();
            List<INIPair> buffer = new List<INIPair>();
            List<string> keyItems = new List<string>();
            int j = 0;
            string rootname = "";
            string rootcom = "";
            foreach (string l in data)
            {
                string line = l;
                string combuf = "";
                if (line.Contains(";"))
                {
                    string[] ls = line.Split(new char[] { ';' }, 2);
                    if (ls[0] == "")
                    {
                        comment[j.ToString()] = ls[1];
                        j += 1;
                        continue;
                    }
                    else combuf = ls[1];
                    line = ls[0];
                }
                line = line.Replace("\t", string.Empty).Replace("\r\n", string.Empty).Replace("\r", string.Empty);
                line = line.Trim();
                //if (removeSpace) line = line.Replace(" ", string.Empty);
                if (line == "") continue;
                if (line.StartsWith("["))
                {
                    if (init)
                    {
                        init = false;
                        rootname = line.Replace("[", string.Empty).Replace("]", string.Empty);
                        rootcom = combuf;
                        entityNameList.Add(rootname);
                        continue;
                    }
                    INIEntity ent = new INIEntity(buffer, rootname);
                    buffer = new List<INIPair>();
                    keyItems = new List<string>();
                    inidata.Add(ent);
                    rootname = line.Replace("[", string.Empty).Replace("]", string.Empty);
                    rootcom = combuf;
                    entityNameList.Add(rootname);
                }
                else
                {
                    string[] tmp = line.Split(new char[] { '=' }, 2);
                    INIPair p = new INIPair(tmp[0], tmp[1], combuf);
                    if (!keyItems.Contains(p.Name))
                    {
                        buffer.Add(p);
                        keyItems.Add(p.Name);
                    }
                }
            }
            inidata.Add(new INIEntity(buffer, rootname));
            f.Close();
        }
        #region Public Methods - INIFile
        public void RemoveEnt(INIEntity ent)
        {
            if (entityNameList.Contains(ent.Name))
            {
                int index = entityNameList.IndexOf(ent.Name);
                inidata.RemoveAt(index);
                entityNameList.Remove(ent.Name);
            }
        }
        public bool HasIniEnt(INIEntity ent)
        {
            return entityNameList.Contains(ent.Name);
        }
        public INIEntity GetEnt(string entName)
        {
            if (entityNameList.Contains(entName)) return inidata[entityNameList.IndexOf(entName)];
            throw new RSException.EntityNotFoundException(entName, fullname);
        }
        public INIEntity PopEnt(string entName)
        {
            INIEntity result = GetEnt(entName);
            RemoveEnt(result);
            return result;
        }
        public void Dispose()
        {
            inidata.Clear();
            comment.Clear();
            entityNameList.Clear();
        }
        #endregion
        #region Public Calls - INIFile
        public Dictionary<string, string> Comment
        {
            get { return comment; }
        }
        public List<INIEntity> IniData
        {
            get { return inidata; }
        }
        public string FileName
        {
            get { return filename; }
        }
        public string FullName
        {
            get { return fullname; }
        }
        public string FilePath
        {
            get { return filepath; }
        }
        public string NameExt
        {
            get { return nameext; }
        }
        public Constant.INIFileType INIType
        {
            get { return initype; }
            set { initype = value; }
        }
        #endregion
    }
    public class INIEntity
    {
        private string name;
        private string comment;
        private List<INIPair> data = new List<INIPair>();
        private List<string> pairNameList = new List<string>();
        private Constant.INIEntType entitytype;

        public INIEntity(List<INIPair> pairs, string n, string com = "")
        {
            data = pairs;
            name = n;
            comment = com;
            foreach (INIPair p in data) pairNameList.Add(p.Name);
            if (Constant.EntName.SystemEntity.Contains(n)) entitytype = Constant.INIEntType.SystemType;
            else if (Constant.EntName.DictionaryList.Contains(n)) entitytype = Constant.INIEntType.ListType;
            else if (Constant.EntName.MapList.Contains(n)) entitytype = Constant.INIEntType.MapType;
            else entitytype = Constant.INIEntType.DefaultType;
        }
        #region Public Methods - INIEntity
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
            if (entitytype != Constant.INIEntType.SystemType || entitytype != Constant.INIEntType.MapType)
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
    public class INIPair
    {
        private string name;
        private dynamic value;
        private string comment;
        private Constant.INIKeyType keytype;
        public INIPair(string n, string val, string com)
        {
            name = n;
            comment = com;
            value = val;
            keytype = Misc.GetKeyType(n);
        }
        #region Public Methods - INIPair
        public void ConvValue()
        {
            if (Constant.BoolFalse.Contains((string)value)) value = false;
            else if (Constant.BoolTrue.Contains((string)value)) value = true;
            else if (Constant.NullString.Contains((string)value) && keytype != Constant.INIKeyType.Armor)
            {
                value = null;
            }
            if (Constant.KeyName.IntKey.Contains(name)) value = int.Parse(value);
            else if (Constant.KeyName.FloatKey.Contains(name)) value = float.Parse(value);
            else if (Constant.KeyName.PercentKey.Contains(name)) value = float.Parse(value.Replace("%", string.Empty)) / 100;
        }
        public bool ParseBool(bool def = false)
        {
            if ((string)value != "")
            {
                if (Constant.BoolTrue.Contains((string)value)) return true;
                else if (Constant.BoolFalse.Contains((string)value)) return false;
                else if (int.Parse(value) == 1) return true;
                else if (int.Parse(value) == 0) return false;
            }
            return def;
        }
        public int ParseInt(int def = 0)
        {
            if ((string)value != "")
            {
                return int.Parse(value);
            }
            return def;
        }
        public float ParseFloat(float def = 0F)
        {
            if ((string)value != "")
            {
                return float.Parse(value);
            }
            return def;
        }
        public string[] ParseStringList()
        {
            return ((string)value).Split(new char[] { ',' });
        }
        public int[] ParseIntList()
        {
            List<int> result = new List<int>();
            foreach(string s in ParseStringList()) result.Add(int.Parse(s));
            return result.ToArray();
        }
        #endregion
        #region Public Calls - INIPair
        public string Name
        {
            get { return name; }
        }
        public dynamic Value
        {
            get { return value; }
        }
        public string Comment
        {
            get { return comment; }
        }
        public Constant.INIKeyType KeyType
        {
            get { return keytype; }
        }
        public static INIPair NullPair
        {
            get { return new INIPair("", "", ""); }
        }
        #endregion
    }
}
