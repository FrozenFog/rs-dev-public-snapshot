using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using relert_sharp.Encoding;
using relert_sharp.Common;
using static relert_sharp.Common.GlobalVar;

namespace relert_sharp.FileSystem
{
    public class VirtualDir
    {
        private Dictionary<uint, VirtualFileInfo> fileOrigin = new Dictionary<uint, VirtualFileInfo>();
        private Dictionary<string, MixTatics> mixTatics = new Dictionary<string, MixTatics>();
        #region Constructor - VirtualDir
        public VirtualDir()
        {
            LoadMixIndex(GlobalConfig.MixNameList);
            LoadMixIndex(GlobalConfig.TheaterMixList);
            LoadMixIndex(GlobalConfig.ExpandMixList);
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
                        else mx = new MixFile(GetRawByte(mixname + ".mix"), mixname + ".mix", MixTatics.Plain);
                        AddMixDir(mx, true, GetParentPath(mixname + ".mix"));
                        mx.Dispose();
                    }
                }
            }
        }
        #endregion


        #region Public Methods - VirtualDir
        public void AddMixDir(MixFile _mixfile, bool _isSub = false, string _parentMixPath = "")
        {
            mixTatics[_mixfile.FileName] = _mixfile.Tatics;
            foreach (MixEntry ent in _mixfile.Index.Entries.Values)
            {
                VirtualFileInfo info;
                if (_isSub) info = new VirtualFileInfo(_mixfile.FilePath, _mixfile.FileName, ent, _mixfile.BodyPos, _parentMixPath);
                else info = new VirtualFileInfo(_mixfile.FilePath, _mixfile.FileName, ent, _mixfile.BodyPos);
                fileOrigin[ent.fileID] = info;
            }
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
                MemoryStream mix = new MemoryStream(GetRawByte(info.MixName + ".mix"));
                mix.Seek(info.FileOffset, SeekOrigin.Begin);
                BinaryReader br = new BinaryReader(mix);
                byte[] result = br.ReadBytes(info.FileSize);
                br.Dispose();
                mix.Dispose();
                return result;
            }
        }
        #endregion
    }


    public class VirtualFileInfo
    {
        #region Constructor - VirtualFileInfo
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
