using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Runtime.InteropServices;
using RelertSharp.Encoding;
using RelertSharp.Common;
using RelertSharp.IniSystem;
using static RelertSharp.Common.GlobalVar;

namespace RelertSharp.FileSystem
{
    public class VirtualDir
    {
        private Dictionary<uint, VirtualFileInfo> fileOrigin = new Dictionary<uint, VirtualFileInfo>();
        private Dictionary<string, MixTatics> mixTatics = new Dictionary<string, MixTatics>();
        private Dictionary<string, byte[]> preLoadMixes = new Dictionary<string, byte[]>();
        private bool preloadInitialized = false;


        #region Ctor - VirtualDir
        public VirtualDir()
        {
            LoadMixIndex(GlobalConfig.MixNameList);
            LoadMixIndex(GlobalConfig.TheaterMixList);
            LoadMixIndex(GlobalConfig.ExpandMixList);
            if (!File.Exists(RunPath + "data.mix")) System.Windows.Forms.MessageBox.Show("Critical File Missing!");
            MixFile mx = new MixFile(RunPath + "data.mix", MixTatics.Plain);
            AddMixDir(mx);
        }
        #endregion


        #region Private Methods - VirtualDir
        private void LoadMixIndex(List<string> src)
        {
            foreach (string mixname in src)
            {
                MixFile mx;
                string path = GlobalConfig.GamePath + mixname + ".mix";
                if (File.Exists(path))
                {
                    if (GlobalConfig.CiphedMix.Contains(mixname)) mx = new MixFile(path, MixTatics.Ciphed);
                    else mx = new MixFile(path, MixTatics.Plain);
                    AddMixDir(mx);
                    mx.Dispose();
                }
                else
                {
                    if (HasFile(mixname + ".mix"))
                    {
                        if (GlobalConfig.CiphedMix.Contains(mixname)) mx = new MixFile(GetRawByte(mixname + ".mix"), mixname + ".mix", MixTatics.Ciphed);
                        else if (GlobalConfig.OldMix.Contains(mixname)) mx = new MixFile(GetRawByte(mixname + ".mix"), mixname + ".mix", MixTatics.Old);
                        else mx = new MixFile(GetRawByte(mixname + ".mix"), mixname + ".mix", MixTatics.Plain);
                        AddMixDir(mx, true, GetParentPath(mixname + ".mix"));
                        mx.Dispose();
                    }
                }
            }
        }
        #endregion


        #region Public Methods - VirtualDir
        public void BeginPreload()
        {
            foreach (string name in GlobalConfig.PreloadMixes)
            {
                string filename = name + ".mix";
                if (HasFile(filename)) preLoadMixes[name] = GetRawByte(filename);
            }
            preloadInitialized = true;
        }
        public void DisposePreloaded()
        {
            preLoadMixes.Clear();
            GC.Collect();
            preloadInitialized = false;
        }
        public Dictionary<string, Image> GetPcxImages(IEnumerable<string> src)
        {
            Dictionary<string, Image> result = new Dictionary<string, Image>();
            foreach(string pcx in src)
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
        public void AddMixDir(MixFile _mixfile, bool _isSub = false, string _parentMixPath = "")
        {
            mixTatics[_mixfile.FileName] = _mixfile.Tatics;
            foreach (MixEntry ent in _mixfile.Index.Entries.Values)
            {
                VirtualFileInfo info;
                if (_isSub)
                {
                    info = new VirtualFileInfo(_mixfile.FilePath, _mixfile.FileName, ent, _mixfile.BodyPos, _parentMixPath);
                }
                else info = new VirtualFileInfo(_mixfile.FilePath, _mixfile.FileName, ent, _mixfile.BodyPos);
                fileOrigin[ent.fileID] = info;
            }
        }
        public VFileInfo GetFilePtr(string filename)
        {
            VFileInfo result = new VFileInfo();
            byte[] buf = GetRawByte(filename);
            result.ptr = Marshal.AllocHGlobal(buf.Length);
            Marshal.Copy(buf, 0, result.ptr, buf.Length);
            result.size = (uint)buf.Length;
            return result;
        }
        public bool HasFile(string _fullName)
        {
            return fileOrigin.Keys.Contains(CRC.GetCRC(_fullName));
        }
        public string GetParentPath(string _fileFullName)
        {
            uint crc = CRC.GetCRC(_fileFullName);
            return fileOrigin[crc].MixPath;
        }
        public byte[] GetRawByte(string _fullName)
        {
            uint fileID = CRC.GetCRC(_fullName);
            VirtualFileInfo info = fileOrigin[fileID];
            if (!info.HasParent)
            {
                FileStream fs = new FileStream(info.MixPath, FileMode.Open, FileAccess.Read);
                fs.Seek(info.FileOffset, SeekOrigin.Begin);
                BinaryReader br = new BinaryReader(fs);
                byte[] result = br.ReadBytes(info.FileSize);
                br.Dispose();
                fs.Dispose();
                return result;
            }
            else
            {
                byte[] data;
                if (preloadInitialized && preLoadMixes.Keys.Contains(info.MixName)) data = preLoadMixes[info.MixName];
                else data = GetRawByte(info.MixName + ".mix");
                MemoryStream mix = new MemoryStream(data);
                mix.Seek(info.FileOffset, SeekOrigin.Begin);
                BinaryReader br = new BinaryReader(mix);
                byte[] result = br.ReadBytes(info.FileSize);
                br.Dispose();
                mix.Dispose();
                return result;
            }
        }
        public T GetFile<T>(string name, Func<byte[], string, T> func) where T : BaseFile
        {
            byte[] data = GetRawByte(name);
            return func(data, name);
        }
        public short GetShpFrameCount(string filename)
        {
            if (string.IsNullOrEmpty(filename)) return 0;
            if (!HasFile(filename)) return 0;
            ShpFile shp = GetFile(filename, FileExtension.SHP);
            return (short)shp.Count;
        }
        public dynamic GetFile(string _fileName, FileExtension _fileType)
        {
            _fileName = _fileName.ToLower();
            switch (_fileType)
            {
                case FileExtension.PAL:
                    if (!_fileName.EndsWith(".pal")) _fileName += ".pal";
                    return new PalFile(GetRawByte(_fileName), _fileName);
                case FileExtension.INI:
                    if (string.IsNullOrEmpty(_fileName)) return new INIFile();
                    if (!_fileName.EndsWith(".ini")) _fileName += ".ini";
                    return new INIFile(GetRawByte(_fileName), _fileName);
                case FileExtension.VXL:
                    if (!_fileName.EndsWith(".vxl")) _fileName += ".vxl";
                    return new VxlFile(GetRawByte(_fileName), _fileName);
                case FileExtension.SHP:
                    return new ShpFile(GetRawByte(_fileName), _fileName);
                case FileExtension.HVA:
                    if (!_fileName.EndsWith(".hva")) _fileName += ".hva";
                    return new HvaFile(GetRawByte(_fileName), _fileName);
                case FileExtension.CSF:
                    if (!_fileName.EndsWith(".csf")) _fileName += ".csf";
                    return new CsfFile(GetRawByte(_fileName), _fileName);
                default:
                    return GetRawByte(_fileName);
            }
        }
        public dynamic GetTheaterTmpFile(string _fileName, TheaterType _type)
        {
            switch (_type)
            {
                case TheaterType.Temprate:
                    _fileName += ".tem";
                    return new TmpFile(GetRawByte(_fileName), _fileName);
                case TheaterType.Snow:
                    _fileName += ".sno";
                    return new TmpFile(GetRawByte(_fileName), _fileName);
                case TheaterType.Urban:
                    _fileName += ".urb";
                    return new TmpFile(GetRawByte(_fileName), _fileName);
                case TheaterType.Desert:
                    _fileName += ".des";
                    return new TmpFile(GetRawByte(_fileName), _fileName);
                case TheaterType.NewUrban:
                    _fileName += ".ubn";
                    return new TmpFile(GetRawByte(_fileName), _fileName);
                case TheaterType.Lunar:
                    _fileName += ".lun";
                    return new TmpFile(GetRawByte(_fileName), _fileName);
                case TheaterType.Custom1:
                    _fileName += "." + GlobalConfig["CustomTheater"]["Custom1Sub"];
                    return new TmpFile(GetRawByte(_fileName), _fileName);
                case TheaterType.Custom2:
                    _fileName += "." + GlobalConfig["CustomTheater"]["Custom2Sub"];
                    return new TmpFile(GetRawByte(_fileName), _fileName);
                case TheaterType.Custom3:
                    _fileName += "." + GlobalConfig["CustomTheater"]["Custom3Sub"];
                    return new TmpFile(GetRawByte(_fileName), _fileName);
                default:
                    return GetRawByte(_fileName);
            }
        }
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
        #endregion
    }
}
