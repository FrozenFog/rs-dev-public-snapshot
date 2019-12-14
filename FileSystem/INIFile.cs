using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using relert_sharp.Common;

namespace relert_sharp.FileSystem
{
    public class INIFile : File
    {
        private Dictionary<string, INIEntity> inidata = new Dictionary<string, INIEntity>();
        private INIFileType initype;
        private bool keepAlive;
        public INIFile(string path,  INIFileType itype = INIFileType.DefaultINI, bool _keepAlive = false) : base(path, FileMode.Open, FileAccess.Read)
        {
            bool init = true;
            keepAlive = _keepAlive;
            initype = itype;
            string preCommentBuffer = "";
            INIEntity ent = new INIEntity();
            string rootname = "";
            string rootcom = "";
            while (CanRead())
            {
                string line = Readline();
                string combuf = "";
                if (line.Contains(";"))
                {
                    string[] ls = line.Split(new char[] { ';' }, 2);
                    if (ls[0] == "")
                    {
                        preCommentBuffer += ";" + ls[1] + "\r\n";
                        continue;
                    }
                    else combuf = ls[1];
                    line = ls[0];
                }
                line = line.Replace("\t", string.Empty).Replace("\r\n", string.Empty).Replace("\r", string.Empty);
                line = line.Trim();
                if (line == "") continue;
                if (line.StartsWith("["))
                {
                    if (init)
                    {
                        init = false;
                        rootname = line.Replace("[", string.Empty).Replace("]", string.Empty);
                        ent = new INIEntity(rootname, preCommentBuffer, combuf);
                        preCommentBuffer = "";
                        continue;
                    }
                    AddEnt(ent);
                    rootname = line.Replace("[", string.Empty).Replace("]", string.Empty);
                    ent = new INIEntity(rootname, preCommentBuffer, combuf);
                    rootcom = combuf;
                }
                else
                {
                    string[] tmp = line.Split(new char[] { '=' }, 2);
                    INIPair p = new INIPair(tmp[0].Trim(), tmp[1].Trim(), combuf, preCommentBuffer);
                    ent.AddPair(p);
                    preCommentBuffer = "";
                }
            }
            AddEnt(ent);
            if (!keepAlive) Close();
        }

        #region Public Methods - INIFile
        public void SaveIni(bool ignoreComment = false)
        {
            if (System.IO.File.Exists(FilePath)) System.IO.File.Delete(FilePath);
            FileStream fs = new FileStream(FilePath, FileMode.CreateNew, FileAccess.Write);
            MemoryStream msbuffer = new MemoryStream();
            //unfinished
        }
        public void AddEnt(INIEntity ent)
        {
            if (!inidata.Keys.Contains(ent.Name)) inidata[ent.Name] = ent;
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
            throw new RSException.EntityNotFoundException(entName, FullName);
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
        public List<INIEntity> IniData
        {
            get { return inidata.Values.ToList(); }
        }
        public INIFileType INIType
        {
            get { return initype; }
            set { initype = value; }
        }
        #endregion
    }
}
