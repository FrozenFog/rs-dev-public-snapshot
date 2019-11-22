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
        private Dictionary<string, INIEntity> inidata = new Dictionary<string, INIEntity>();
        public INIFile(string path)
        {
            Utils.File f = new Utils.File(path);
            List<string> data = f.readlines();
            Dictionary<string, dynamic> buffer = new Dictionary<string, dynamic>();
            int j = 0;
            string rootname = "";
            foreach (string l in data)
            {
                string line = l;
                if (line.Contains(";"))
                {
                    string[] ls = Misc.Split(line, ';');
                    if (ls[0] == "")
                    {
                        comment[j.ToString()] = ls[1];
                        j += 1;
                        continue;
                    }
                    else
                    {
                        comment[ls[0]] = ls[1];
                    }
                    line = ls[0];
                }
                line = line.Replace("\t", string.Empty).Replace("\n", string.Empty).Replace(" ", string.Empty).Replace("\r", string.Empty);
                if (line == "")
                {
                    continue;
                }
                if (line.StartsWith("["))
                {
                    if (buffer.Count == 0)
                    {
                        rootname = line.Replace("[", string.Empty).Replace("]", string.Empty);
                        continue;
                    }
                    INIEntity ent = new INIEntity(buffer, rootname);
                    buffer = new Dictionary<string, dynamic>();
                    inidata[rootname] = ent;
                    rootname = line.Replace("[", string.Empty).Replace("]", string.Empty);
                }
                else
                {
                    string[] tmp = Misc.Split(line, '=');
                    buffer[tmp[0]] = tmp[1];
                }
            }
            inidata[rootname] = new INIEntity(buffer, rootname);
        }
        public Dictionary<string, string> Comment
        {
            get { return comment; }
        }
        public Dictionary<string, INIEntity> IniData
        {
            get { return inidata; }
        }
        public class INIEntity
        {
            private string name;
            private Dictionary<string, dynamic> inidata = new Dictionary<string, dynamic>();

            public INIEntity(Dictionary<string, dynamic> dict, string n)
            {
                name = n;
                inidata = dict;
            }
            public string Name
            {
                get { return name; }
            }
            public Dictionary<string, dynamic> IniData
            {
                get { return inidata; }
            }
        }
    }
}
