using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using relert_sharp.Common;

namespace relert_sharp.FileSystem
{
    public class INIFile
    {
        private Dictionary<string, string> comment = new Dictionary<string, string>();
        private Dictionary<string, INIEntity> inidata = new Dictionary<string, INIEntity>();
        private INIFileType initype;
        private string filename;
        private string fullname;
        private string filepath;
        private string nameext;
        public INIFile(string path,  INIFileType itype = INIFileType.DefaultINI)
        {
            bool init = true;
            initype = itype;
            File f = new File(path, FileMode.Open, FileAccess.Read);
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
                        continue;
                    }
                    INIEntity ent = new INIEntity(buffer, rootname);
                    buffer = new List<INIPair>();
                    keyItems = new List<string>();
                    inidata[rootname] = ent;
                    rootname = line.Replace("[", string.Empty).Replace("]", string.Empty);
                    rootcom = combuf;
                }
                else
                {
                    string[] tmp = line.Split(new char[] { '=' }, 2);
                    INIPair p = new INIPair(tmp[0].Trim(), tmp[1].Trim(), combuf);
                    if (!keyItems.Contains(p.Name))
                    {
                        buffer.Add(p);
                        keyItems.Add(p.Name);
                    }
                }
            }
            inidata[rootname] = new INIEntity(buffer, rootname);
            f.Close();
        }

        #region Public Methods - INIFile
        public void SaveIni(bool ignoreComment = false)
        {
            if (System.IO.File.Exists(filepath)) System.IO.File.Delete(filepath);
            FileStream fs = new FileStream(filepath, FileMode.CreateNew, FileAccess.Write);
            MemoryStream msbuffer = new MemoryStream();
            
        }
        public void RemoveEnt(INIEntity ent)
        {
            if (inidata.Keys.Contains(ent.Name)) inidata.Remove(ent.Name);
        }
        public bool HasIniEnt(INIEntity ent)
        {
            return inidata.Keys.Contains(ent.Name);
        }
        public INIEntity GetEnt(string entName)
        {
            if (inidata.Keys.Contains(entName)) return inidata[entName];
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
        }
        #endregion
        #region Public Calls - INIFile
        public Dictionary<string, INIEntity> IniDict
        {
            get { return inidata; }
            set { inidata = value; }
        }
        public INIEntity this[string name]
        {
            get { return GetEnt(name); }
            set { inidata[name] = value; }
        }
        public Dictionary<string, string> Comment
        {
            get { return comment; }
        }
        public List<INIEntity> IniData
        {
            get { return inidata.Values.ToList(); }
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
        public INIFileType INIType
        {
            get { return initype; }
            set { initype = value; }
        }
        #endregion
    }
}
