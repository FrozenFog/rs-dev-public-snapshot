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
        public INIFile(string path, bool removeSpace = false)
        {
            Utils.File f = new Utils.File(path, FileMode.Open, FileAccess.Read);
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
                    string[] ls = Misc.Split(line, ';');
                    if (ls[0] == "")
                    {
                        comment[j.ToString()] = ls[1];
                        j += 1;
                        continue;
                    }
                    else combuf = ls[1];
                    line = ls[0];
                }
                line = line.Replace("\t", string.Empty).Replace("\n", string.Empty).Replace("\r", string.Empty);
                line = line.Trim();
                if (removeSpace) line = line.Replace(" ", string.Empty);
                if (line == "") continue;
                if (line.StartsWith("["))
                {
                    if (buffer.Count == 0)
                    {
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
                    string[] tmp = Misc.Split(line, '=');
                    INIPair p = new INIPair(tmp[0], tmp[1], combuf);
                    if (!keyItems.Contains(p.Name))
                    {
                        buffer.Add(p);
                        keyItems.Add(p.Name);
                    }
                }
            }
            inidata.Add(new INIEntity(buffer, rootname));
            entityNameList.Add(rootname);
            f.Close();
        }
        public void RemoveEnt(INIEntity ent)
        {
            foreach (INIEntity e in inidata)
            {
                if (e.Name == ent.Name)
                {
                    inidata.Remove(e);
                    entityNameList.Remove(e.Name);
                    return;
                }
            }
        }
        public bool HasIniEnt(INIEntity ent)
        {
            return entityNameList.Contains(ent.Name);
        }
        public INIEntity GetEnt(string entName)
        {
            foreach (INIEntity ent in inidata) if (ent.Name == entName) return ent;
            return null;
        }
        #region Public Calls
        public Dictionary<string, string> Comment
        {
            get { return comment; }
        }
        public List<INIEntity> IniData
        {
            get { return inidata; }
        }
        #endregion
    }
    public class INIEntity
    {
        private string name;
        private string comment;
        private List<INIPair> data = new List<INIPair>();
        private List<string> pairNameList = new List<string>();

        public INIEntity(List<INIPair> pairs, string n, string com = "")
        {
            data = pairs;
            name = n;
            comment = com;
            foreach (INIPair p in data) pairNameList.Add(p.Name);
        }
        public INIPair GetPair(string pairName)
        {
            foreach (INIPair p in data) if (p.Name == pairName) return p;
            return null;
        }
        public bool HasPair(INIPair p)
        {
            return pairNameList.Contains(p.Name);
        }
        public void ConvPairs()
        {
            foreach (INIPair p in data) p.ConvValue();
        }
        public void RemovePair(INIPair p)
        {
            data.Remove(p);
        }
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
        public INIPair(string n, string val, string com)
        {
            name = n;
            comment = com;
            value = val;
        }
        public void ConvValue()
        {
            if (Cons.BoolFalse.Contains((string)value)) value = false;
            else if (Cons.BoolTrue.Contains((string)value)) value = true;
            else if (Cons.NullString.Contains((string)value)) value = null;
            if (Cons.KeyName.IntKey.Contains(name)) value = int.Parse(value);
            else if (Cons.KeyName.FloatKey.Contains(name)) value = float.Parse(value);
            else if (Cons.KeyName.PercentKey.Contains(name)) value = float.Parse(value.Replace("%", string.Empty)) / 100;
        }
        #region Public Calls
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
        public static INIPair NullPair
        {
            get { return new INIPair("", "", ""); }
        }
        #endregion
    }
}
