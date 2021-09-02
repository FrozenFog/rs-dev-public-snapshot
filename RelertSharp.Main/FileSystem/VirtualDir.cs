using RelertSharp.Algorithm;
using RelertSharp.Common;
using RelertSharp.Encoding;
using RelertSharp.IniSystem;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using static RelertSharp.Common.GlobalVar;

namespace RelertSharp.FileSystem
{
    public class VirtualDir
    {
        private RsLog Log { get { return GlobalVar.Log; } }
        private bool isFindFileLogEnable = false;


        private Dictionary<uint, VirtualFileInfo> fileOrigin = new Dictionary<uint, VirtualFileInfo>();
        private Dictionary<string, MixTatics> mixTatics = new Dictionary<string, MixTatics>();


        #region Ctor - VirtualDir
        public VirtualDir()
        {
            Log.Info("Mix loading begin");
            MixFile framework = new MixFile(RunPath + "framework.mix", MixTatics.Plain);
            AddMixDir(framework);
            LoadMixFromConfig();
            Log.Info("General mix loaded");
            if (!File.Exists(RunPath + "data.mix")) System.Windows.Forms.MessageBox.Show("Critical File Missing!");
            MixFile mx = new MixFile(RunPath + "data.mix", MixTatics.Plain, true);
            AddMixDir(mx);
            Log.Info("Virtual Dir Initialization Complete");
        }
        #endregion


        #region Private Methods - VirtualDir
        private void LoadMixFromConfig()
        {
            List<MixFile> expand = new List<MixFile>();
            string[] getName(string entryName)
            {
                List<string> names = new List<string>();
                Regex re = new Regex("#+");
                Match m = re.Match(entryName);
                if (m.Success)
                {
                    int count = m.Length;
                    for (int i = 0; i < Math.Pow(10, count); i++)
                    {
                        string fmt = "{0:D" + count.ToString() + "}";
                        string name = re.Replace(entryName, string.Format(fmt, i));
                        names.Add(name + Constant.EX_MIX);
                    }
                }
                else names.Add(entryName + Constant.EX_MIX);
                return names.ToArray();
            }
            void load(string mixname, int tatic, bool isExpand)
            {
                MixFile mx;
                string path = Path.Combine(GlobalConfig.GamePath, mixname);
                Log.Info(string.Format("Loading mix: {0}. From {1}", mixname, path));
                if (File.Exists(path))
                {
                    mx = new MixFile(path, (MixTatics)tatic);
                    Log.Info(string.Format("{0} found in game path.", mixname));
                    if (isExpand) expand.Add(mx);
                    else
                    {
                        AddMixDir(mx);
                        mx.Dispose();
                    }
                }
                else
                {
                    if (HasFile(mixname))
                    {
                        mx = new MixFile(GetRawByte(mixname), mixname, (MixTatics)tatic);
                        VirtualFileInfo info = fileOrigin[CRC.GetCRC(mixname)];
                        if (isExpand) expand.Add(mx);
                        else
                        {
                            AddMixDir(mx, true, GetParentPath(mixname), info.FileOffset);
                            mx.Dispose();
                        }
                    }
                    else Log.Warning("{0} not found.", mixname);
                }
            }
            /// main info mix
            foreach (var entry in GlobalConfig.ModGeneral.MixInfo)
            {
                MixFile mx;
                string[] mixNames = getName(entry.Name);
                foreach (string mixname in mixNames)
                {
                    load(mixname, entry.ReadTatic, entry.IsExpand);
                }
            }
            /// theater mix
            foreach (var theater in GlobalConfig.ModGeneral.Theater)
            {
                foreach (string mix in theater.MixList.Split(','))
                {
                    MixFile mx;
                    string mixname = mix + Constant.EX_MIX;
                    load(mixname, (int)MixTatics.Plain, false);
                }
            }
            /// expand mix, higher priority
            foreach (MixFile mx in expand)
            {
                AddMixDir(mx);
            }
        }
        #endregion


        #region Public Methods - VirtualDir
        public void SuspendLog()
        {
            isFindFileLogEnable = false;
        }
        public void ResumeLog()
        {
            isFindFileLogEnable = true;
        }
        public void DumpFile(string filename)
        {
            if (HasFile(filename))
            {
                FileStream dump = new FileStream(filename, FileMode.Create, FileAccess.Write);
                BinaryWriter bw = new BinaryWriter(dump);
                bw.Write(GetRawByte(filename));
                bw.Flush();
                bw.Dispose();
                dump.Dispose();
            }
        }
        public Dictionary<string, Image> GetPcxImages(IEnumerable<string> src)
        {
            Dictionary<string, Image> result = new Dictionary<string, Image>();
            foreach (string pcx in src)
            {
                string key = pcx.ToLower();
                if (string.IsNullOrEmpty(key)) key = "xxicon.shp";
                if (key.EndsWith(".shp") && HasFile(key))
                {
                    ShpFile shp = GetFile(key, FileExtension.SHP);
                    shp.LoadColor(GetFile("cameo.pal", FileExtension.PAL));
                    result[key] = shp.Frames[0].Image;
                }
                else if (key.EndsWith(".pcx") && HasFile(key))
                {
                    result[key] = PcxDecoder.Decode(GetRawByte(key));
                }
            }
            return result;
        }
        public Image GetPcxImage(string regName)
        {
            string pcxName = GlobalRules.GetPcxName(regName).ToLower();
            if (pcxName.EndsWith(".shp") && HasFile(pcxName))
            {
                ShpFile shp = GetFile(pcxName, FileExtension.SHP);
                shp.LoadColor(GetFile("cameo.pal", FileExtension.PAL));
                return shp.Frames[0].Image;
            }
            else if (pcxName.EndsWith(".pcx") && HasFile(pcxName))
            {
                return PcxDecoder.Decode(GetRawByte(pcxName));
            }
            return null;
        }
        public void AddMixDir(MixFile _mixfile, bool _isSub = false, string _parentMixPath = "", int parentOffset = 0)
        {
            if (_isSub) Log.Info(string.Format("Loading mix {0} from {1}", _mixfile.FileName, _parentMixPath));
            mixTatics[_mixfile.FileName] = _mixfile.Tatics;
            foreach (MixEntry ent in _mixfile.Index.Entries.Values)
            {
                VirtualFileInfo info;
                if (_isSub)
                {
                    info = new VirtualFileInfo(_mixfile.FilePath, _mixfile.FileName, ent, _mixfile.BodyPos + parentOffset, _parentMixPath);
                }
                else info = new VirtualFileInfo(_mixfile.FilePath, _mixfile.FileName, ent, _mixfile.BodyPos);
                fileOrigin[ent.fileID] = info;
            }
            Log.Info(string.Format("{0} virtualization complete, {1} file(s) loaded", _mixfile.FileName, _mixfile.Index.Entries.Count), LogLevel.Asterisk);
        }
        public VFileInfo GetFilePtr(string filename)
        {
            VFileInfo result = new VFileInfo();
            byte[] buf = GetRawByte(filename);
            result.ptr = Marshal.AllocHGlobal(buf.Length);
            Marshal.Copy(buf, 0, result.ptr, buf.Length);
            //result.ptr = Marshal.UnsafeAddrOfPinnedArrayElement(buf, 0);
            result.size = (uint)buf.Length;
            return result;
        }
        public bool HasFile(string _fullName)
        {
            bool b = fileOrigin.Keys.Contains(CRC.GetCRC(_fullName));
            if (GlobalConfig.DevMode)
            {
                string path = Path.Combine(GlobalConfig.GamePath, _fullName);
                b |= File.Exists(path);
            }
            return b;
        }
        public string GetParentPath(string _fileFullName)
        {
            uint crc = CRC.GetCRC(_fileFullName);
            return fileOrigin[crc].MixPath;
        }
        private byte[] GetFromRoot(string _filename)
        {
            string path = Path.Combine(GlobalConfig.GamePath, _filename);
            if (File.Exists(path))
                return File.ReadAllBytes(path);
            else
                return null;
        }
        public bool ExtractFile(string _fullName, string savePath, bool fromRoot = false)
        {
            byte[] data = GetRawByte(_fullName, fromRoot);
            if (data != null)
            {
                FileStream fs = new FileStream(savePath, FileMode.Create, FileAccess.Write);
                MemoryStream ms = new MemoryStream(data);
                ms.WriteTo(fs);
                fs.Flush();
                ms.Dispose();
                fs.Dispose();
                return true;
            }
            return false;
        }
        public bool TryGetRawByte(string _fullName, out byte[] bytes, bool fromRoot = false)
        {
            if (fromRoot || GlobalConfig.DevMode)
            {
                bytes = GetFromRoot(_fullName);
                if (bytes != null) return true;
            }
            uint fileid = CRC.GetCRC(_fullName);
            if (!fileOrigin.Keys.Contains(fileid))
            {
                bytes = null;
                return false;
            }
            bytes = GetRawByte(_fullName, fromRoot);
            return true;
        }
        public byte[] GetRawByte(string _fullName, bool fromRoot = false)
        {
            if (isFindFileLogEnable) Log.Info("Finding " + _fullName);
            if (fromRoot || GlobalConfig.DevMode)
            {
                byte[] b = GetFromRoot(_fullName);
                if (b != null) return b;
            }
            uint fileID = CRC.GetCRC(_fullName);
            if (!fileOrigin.Keys.Contains(fileID))
            {
                throw new RSException.MixEntityNotFoundException("unknown mix", _fullName);
            }
            VirtualFileInfo info = fileOrigin[fileID];
            if (info.HostCiphed)
            {
                MixFile mx = new MixFile(info.MixPath);
                byte[] b = mx.GetByte(info);
                mx.Dispose();
                return b;
            }
            else
            {
                string mxPath = info.HasParent ? info.ParentPath : info.MixPath;
                FileStream fs = new FileStream(mxPath, FileMode.Open, FileAccess.Read);
                fs.Seek(info.FileOffset, SeekOrigin.Begin);
                BinaryReader br = new BinaryReader(fs);
                byte[] result = br.ReadBytes(info.FileSize);
                br.Dispose();
                fs.Dispose();
                return result;
            }
        }
        public short GetShpFrameCount(string filename, out bool isEmpty)
        {
            isEmpty = true;
            if (string.IsNullOrEmpty(filename)) return 0;
            if (!HasFile(filename)) return 0;
            try
            {
                ShpFile shp = GetFile(filename, FileExtension.SHP);
                isEmpty = shp.IsEmpty;
                return (short)shp.Count;
            }
            catch (RSException.InvalidFileException e)
            {
                Log.Critical("Shp corrupted! Shp name {0}", e.FileName);
                return 0;
            }
        }
        public dynamic GetFile(string _fileName, FileExtension _fileType, bool fromRoot = false)
        {
            _fileName = _fileName.ToLower();
            switch (_fileType)
            {
                case FileExtension.PAL:
                    if (!_fileName.EndsWith(".pal")) _fileName += ".pal";
                    return new PalFile(GetRawByte(_fileName, fromRoot), _fileName);
                case FileExtension.INI:
                    if (string.IsNullOrEmpty(_fileName)) return new INIFile();
                    if (!_fileName.EndsWith(".ini")) _fileName += ".ini";
                    return new INIFile(GetRawByte(_fileName, fromRoot), _fileName);
                case FileExtension.VXL:
                    if (!_fileName.EndsWith(".vxl")) _fileName += ".vxl";
                    return new VxlFile(GetRawByte(_fileName, fromRoot), _fileName);
                case FileExtension.SHP:
                    return new ShpFile(GetRawByte(_fileName, fromRoot), _fileName);
                case FileExtension.HVA:
                    if (!_fileName.EndsWith(".hva")) _fileName += ".hva";
                    return new HvaFile(GetRawByte(_fileName, fromRoot), _fileName);
                case FileExtension.CSF:
                    if (!_fileName.EndsWith(".csf")) _fileName += ".csf";
                    return new CsfFile(GetRawByte(_fileName, fromRoot), _fileName);
                default:
                    return GetRawByte(_fileName, fromRoot);
            }
        }
        #endregion


        #region Public Calls - VirtualDir
        public int Count { get { return fileOrigin.Count; } }
        #endregion
    }


    public class VirtualFileInfo
    {
        #region Ctor - VirtualFileInfo
        public VirtualFileInfo(string _mixpath, string _mixName, MixEntry _ent, int _bodyPos, string _parentMixPath = "")
        {
            MixPath = _mixpath;
            MixName = _mixName;
            ParentPath = _parentMixPath;
            HostCiphed = _ent.hostCiphed;
            if (!string.IsNullOrEmpty(_parentMixPath))
            {
                ParentMixName = _parentMixPath.Substring(_parentMixPath.LastIndexOf('\\') + 1);
                ParentMixName = ParentMixName.Substring(0, ParentMixName.Length - 4);
            }
            FileOffset = _ent.offset + _bodyPos;
            FileSize = _ent.size;
        }
        #endregion


        #region Public Calls - VirtualFileInfo
        public bool HasParent { get { return !string.IsNullOrEmpty(ParentMixName); } }
        public string ParentPath { get; private set; }
        public string ParentMixName { get; private set; }
        public string MixPath { get; private set; }
        public string MixName { get; private set; }
        public int FileOffset { get; private set; }
        public int FileSize { get; private set; }
        public bool HostCiphed { get; private set; }
        #endregion
    }
}
