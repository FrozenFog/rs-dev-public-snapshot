using RelertSharp.Common;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RelertSharp.IniSystem
{
    public class INIFile : FileSystem.BaseFile, IEnumerable<INIEntity>
    {
        private Dictionary<string, INIEntity> inidata = new Dictionary<string, INIEntity>();
        private INIFileType initype;
        private bool ciphed = false;


        #region Ctor - INIFile
        public INIFile(string path, INIFileType itype = INIFileType.DefaultINI, bool ciphed = false) : base(path, FileMode.Open, FileAccess.Read, false, ciphed)
        {
            initype = itype;
            this.ciphed = ciphed;
            Load();
        }
        public INIFile(byte[] _data, string _filename, INIFileType _type = INIFileType.DefaultINI) : base(_data, _filename)
        {
            initype = _type;
            Load();
        }
        public INIFile(bool ciphed)
        {
            initype = INIFileType.UnknownINI;
            this.ciphed = ciphed;
        }
        public INIFile(string filename, bool newfile)
        {
            FileName = filename;
        }
        public INIFile() { initype = INIFileType.UnknownINI; }
        #endregion


        #region Private Methods - INIFile
        //[DllImport("rslib.dll")]
        //private static extern IntPtr Encode(IntPtr src, int length);
        //private byte[] Encode(byte[] src)
        //{
        //    IntPtr p = Marshal.AllocHGlobal(src.Length);
        //    byte[] dest = new byte[src.Length];
        //    Marshal.Copy(src, 0, p, src.Length);
        //    Marshal.Copy(Encode(p, src.Length), dest, 0, src.Length);
        //    return dest;
        //}
        private void Load()
        {
            bool init = true;
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
                        preCommentBuffer += ";" + ls[1] + "\n";
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
                else if (line.StartsWith("*")) continue;
                else
                {
                    INIPair p;
                    string[] tmp = line.Split(new char[] { '=' }, 2);
                    if (tmp.Length == 1) p = new INIPair(tmp[0].Trim(), "", combuf, preCommentBuffer);
                    else p = new INIPair(tmp[0].Trim(), tmp[1].Trim(), combuf, preCommentBuffer);
                    ent.AddPair(p);
                    preCommentBuffer = "";
                }
            }
            AddEnt(ent);
        }
        #endregion


        #region Public Methods - INIFile
        /// <summary>
        /// Compare two ini files, will not modify either of them
        /// </summary>
        /// <param name="newversion">the NEW version of ini file</param>
        /// <returns></returns>
        public IniFileCompareLog Distinct(INIFile newversion)
        {
            IniFileCompareLog log = new IniFileCompareLog();
            foreach (INIEntity newent in newversion)
            {
                if (HasIniEnt(newent))
                {
                    INIEntity exist = this[newent.Name];
                    IniEntityCompareLog entLog = new IniEntityCompareLog(newent.Name);
                    if (exist.IsSystemList)
                    {
                        HashSet<string> listOrg = exist.TakeValuesToList().ToHashSet();
                        HashSet<string> listNew = newent.TakeValuesToList().ToHashSet();
                        var removed = listOrg.Except(listNew);
                        foreach (string value in removed) entLog.RemovedPairs.Add(new IniPairCompareLog(value));
                        var added = listNew.Except(listOrg);
                        foreach (string value in added) entLog.AddedPairs.Add(new IniPairCompareLog(value));
                    }
                    else
                    {
                        foreach (INIPair pOrg in exist)
                        {
                            if (newent.HasPair(pOrg))
                            {
                                INIPair pNew = newent.GetPair(pOrg.Name);
                                if (pNew.Value != pOrg.Value)
                                {
                                    IniPairCompareLog pLog = new IniPairCompareLog()
                                    {
                                        Name = pNew.Name,
                                        NewValue = pNew.Value,
                                        OldValue = pOrg.Value
                                    };
                                    entLog.ModifiedPairs.Add(pLog);
                                }
                            }
                            else
                            {
                                entLog.RemovedPairs.Add(new IniPairCompareLog(pOrg, true));
                            }
                        }
                        foreach (INIPair newPair in newent)
                        {
                            if (!exist.HasPair(newPair)) entLog.AddedPairs.Add(new IniPairCompareLog(newPair, false));
                        }
                    }
                    if (!entLog.IsEmpty) log.Modified.Add(entLog);
                }
                else
                {
                    log.Added.Add(new IniEntityCompareLog(newent, false));
                }
            }
            foreach (INIEntity oldent in this)
            {
                if (!newversion.HasIniEnt(oldent)) log.Removed.Add(new IniEntityCompareLog(oldent, true));
            }
            return log;
        }
        /// <summary>
        /// Similar to Override, but remain original key/value when src have same key/value 
        /// </summary>
        /// <param name="src"></param>
        public void Merge(IEnumerable<INIEntity> src)
        {
            foreach (INIEntity ent in src)
            {
                if (HasIniEnt(ent))
                {
                    IniDict[ent.Name].MergeWith(ent);
                }
                else
                {
                    AddEnt(ent);
                }
            }
        }
        /// <summary>
        /// Merge two IniFile, Entity with same name will be merged, IniPair with same key will be overwrite by new one
        /// </summary>
        /// <param name="newEnt">New Ent</param>
        public void Override(IEnumerable<INIEntity> src)
        {
            foreach (INIEntity newent in src)
            {
                if (HasIniEnt(newent))
                {
                    IniDict[newent.Name].JoinWith(newent);
                }
                else
                {
                    if (newent.IsSystemList)
                    {
                        INIEntity convert = new INIEntity(newent);
                        convert.Reorganize();
                        AddEnt(convert);
                    }
                    else AddEnt(newent);
                }
            }
        }
        /// <summary>
        /// Save current IniFile structure into a file
        /// </summary>
        /// <param name="ignoreComment"></param>
        public void SaveIni(string filePath, bool ignoreComment = false)
        {
            if (File.Exists(filePath)) File.Delete(filePath);
            FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write);
            MemoryStream msbuffer = new MemoryStream();
            StreamWriter sw = new StreamWriter(msbuffer);
            foreach (INIEntity ent in IniData)
            {
                sw.Write(ent.SaveString(ignoreComment));
                sw.Flush();
            }
            if (ciphed)
            {
                msbuffer = new MemoryStream(CipherLib.Cipher.EncodeArray(msbuffer.ToArray()));
            }
            msbuffer.WriteTo(fs);
            fs.Flush();
            sw.Dispose();
            fs.Dispose();
            msbuffer.Dispose();
            Dispose();
        }
        /// <summary>
        /// Add an IniEnt, if there is an IniEnt with same name, it will do nothing
        /// </summary>
        /// <param name="ent"></param>
        public void AddEnt(INIEntity ent)
        {
            if (string.IsNullOrEmpty(ent.Name)) return;
            if (!inidata.Keys.Contains(ent.Name)) inidata[ent.Name] = ent;
        }
        public void AddEnt(IEnumerable<INIEntity> src)
        {
            foreach (INIEntity ent in src) AddEnt(ent);
        }
        /// <summary>
        /// Remove an IniEnt with a specified name, will do nothing if not found
        /// </summary>
        /// <param name="ent"></param>
        public void RemoveEnt(INIEntity ent)
        {
            if (inidata.Keys.Contains(ent.Name)) inidata.Remove(ent.Name);
        }
        /// <summary>
        /// Only check the name
        /// </summary>
        /// <param name="ent"></param>
        /// <returns></returns>
        public bool HasIniEnt(INIEntity ent)
        {
            return inidata.Keys.Contains(ent.Name);
        }
        public virtual bool HasIniEnt(string name)
        {
            return inidata.Keys.Contains(name);
        }
        /// <summary>
        /// Find an IniEnt with specified name, return a NullEntity(not null) if not found
        /// </summary>
        /// <param name="entName"></param>
        /// <returns></returns>
        public INIEntity GetEnt(string entName)
        {
            if (inidata.Keys.Contains(entName)) return inidata[entName];
            return INIEntity.NullEntity;
            //throw new RSException.EntityNotFoundException(entName, FullName);
        }
        /// <summary>
        /// Find an IniEnt with specified name and remove it from file, return a NullEntity(not null) if not found
        /// </summary>
        /// <param name="entName"></param>
        /// <returns></returns>
        public INIEntity PopEnt(string entName)
        {
            INIEntity result = GetEnt(entName);
            RemoveEnt(result);
            return result;
        }
        /// <summary>
        /// Remove every item in this file
        /// </summary>
        public void ClearAllIniEnt()
        {
            inidata.Clear();
        }

        public IEnumerator<INIEntity> GetEnumerator()
        {
            return IniData.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return IniData.GetEnumerator();
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
            get
            {
                if (!IniDict.Keys.Contains(name))
                {
                    INIEntity ent = new INIEntity(name);
                    IniDict[name] = ent;
                    return ent;
                }
                else return GetEnt(name);
            }
            set
            {
                IniDict[name] = value;
            }
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




    public class IniFileCompareLog
    {
        public List<IniEntityCompareLog> Removed { get; set; } = new List<IniEntityCompareLog>();
        public List<IniEntityCompareLog> Added { get; set; } = new List<IniEntityCompareLog>();
        public List<IniEntityCompareLog> Modified { get; set; } = new List<IniEntityCompareLog>();


        public void SaveReport(string filepath)
        {
            using (FileStream fs = new FileStream(filepath, FileMode.Create, FileAccess.Write))
            {
                StreamWriter sw = new StreamWriter(fs);
                StringBuilder sb = new StringBuilder();
                void process(List<IniEntityCompareLog> src, string title)
                {
                    sb.AppendLine(title);
                    foreach (IniEntityCompareLog log in src)
                    {
                        sb.AppendLine(log.GenerateReport());
                    }
                }
                process(Removed, "Removed:");
                process(Added, "Added:");
                process(Modified, "Modified:");
                sw.WriteLine(sb.ToString());
                sw.Flush();
            }
        }
    }

    public class IniEntityCompareLog
    {
        public IniEntityCompareLog(INIEntity src, bool isRemoved)
        {
            Name = src.Name;
            foreach (INIPair p in src)
            {
                var log = new IniPairCompareLog(p, isRemoved);
                if (isRemoved) RemovedPairs.Add(log);
                else AddedPairs.Add(log);
            }
        }
        public IniEntityCompareLog(string name)
        {
            Name = name;
        }
        public string GenerateReport()
        {
            StringBuilder sb = new StringBuilder(string.Format("[{0}]\n", Name));
            if (RemovedPairs.Count > 0)
            {
                sb.AppendLine("Removed Pairs:");
                foreach (var p in RemovedPairs) sb.AppendLine(p.AsRemoved());
                sb.AppendLine();
            }
            if (AddedPairs.Count > 0)
            {
                sb.AppendLine("Added Pairs:");
                foreach (var p in AddedPairs) sb.AppendLine(p.AsAdded());
                sb.AppendLine();
            }
            if (ModifiedPairs.Count > 0)
            {
                sb.AppendLine("Modified Pairs:");
                foreach (var p in ModifiedPairs) sb.AppendLine(p.GenerateReport());
                sb.AppendLine();
            }
            return sb.ToString();
        }
        public string Name { get; set; }
        public List<IniPairCompareLog> RemovedPairs { get; set; } = new List<IniPairCompareLog>();
        public List<IniPairCompareLog> AddedPairs { get; set; } = new List<IniPairCompareLog>();
        public List<IniPairCompareLog> ModifiedPairs { get; set; } = new List<IniPairCompareLog>();
        public bool IsEmpty { get { return RemovedPairs.Count + AddedPairs.Count + ModifiedPairs.Count == 0; } }
    }

    public class IniPairCompareLog
    {
        public IniPairCompareLog(INIPair src, bool isRemoved)
        {
            Name = src.Name;
            if (isRemoved) OldValue = src.Value;
            else NewValue = src.Value;
        }
        public IniPairCompareLog(string value)
        {
            Name = value;
        }
        public IniPairCompareLog()
        {

        }
        public string GenerateReport()
        {
            return string.Format("{0}\tfrom {1}\tto {2}", Name, OldValue, NewValue);
        }
        public string AsRemoved()
        {
            if (IsListValue) return Name;
            return string.Format("{0}={1}", Name, OldValue);
        }
        public string AsAdded()
        {
            if (IsListValue) return Name;
            return string.Format("{0}={1}", Name, NewValue);
        }
        public string Name { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public bool IsListValue { get { return string.IsNullOrEmpty(OldValue) && string.IsNullOrEmpty(NewValue) && !string.IsNullOrEmpty(Name); } }
    }
}
