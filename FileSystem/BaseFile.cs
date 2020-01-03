using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using relert_sharp.Common;

namespace relert_sharp.FileSystem
{
    public abstract class BaseFile : IDisposable
    {
        private FileStream _fs;
        private BinaryReader br;
        private BinaryWriter bw;
        private MemoryStream ms = new MemoryStream();
        private MemoryStream memoryRead = new MemoryStream();
        private StreamReader sr;
        private StreamWriter sw;
        private string filename;
        private string fullname;
        private string filepath;
        private string nameext;
        private FileExtension extension = FileExtension.Undefined;
        private FileAccess access;


        #region Constructor - BaseFile
        public BaseFile(string path, FileMode m, FileAccess a, bool _keepAlive = true)
        {
            if (m == FileMode.Create && File.Exists(path)) File.Delete(path);
            _fs = new FileStream(path, m, a);
            if (!_keepAlive) _fs.CopyTo(memoryRead);
            memoryRead.Seek(0, SeekOrigin.Begin);
            access = a;
            filepath = _fs.Name;
            string[] sl = filepath.Split(new char[] { '\\' });
            fullname = sl[sl.Count() - 1];
            GetNames();
            if (!_keepAlive) _fs.Dispose();
            InitStream();
        }
        public BaseFile(byte[] _rawData, string _fileName, FileAccess _acc = FileAccess.Read)
        {
            memoryRead = new MemoryStream(_rawData);
            memoryRead.Seek(0, SeekOrigin.Begin);
            access = _acc;
            fullname = _fileName;
            GetNames();
            InitStream();
        }
        public BaseFile(Stream stream, string _fileName)
        {
            stream.CopyTo(memoryRead);
            memoryRead.Seek(0, SeekOrigin.Begin);
            access = FileAccess.Read;
            fullname = _fileName;
            GetNames();
            InitStream();
        }
        #endregion


        #region Private Methods
        private void GetNames()
        {
            if (fullname.Contains("."))
            {
                string[] namesl = fullname.Split(new char[] { '.' });
                filename = namesl[0];
                nameext = namesl[1];
            }
            else
            {
                filename = _fs.Name;
            }
            switch (nameext.ToLower())
            {
                case "ini":
                    extension = FileExtension.INI;
                    break;
                case "csv":
                    extension = FileExtension.CSV;
                    break;
                case "map":
                    extension = FileExtension.MAP;
                    break;
                case "yrm":
                    extension = FileExtension.YRM;
                    break;
                case "txt":
                    extension = FileExtension.TXT;
                    break;
                case "lang":
                    extension = FileExtension.LANG;
                    break;
                case "mix":
                    extension = FileExtension.MIX;
                    break;
                case "pal":
                    extension = FileExtension.PAL;
                    break;
                default:
                    extension = FileExtension.Undefined;
                    break;
            }
        }
        private void InitStream()
        {
            if (access == FileAccess.Read)
            {
                if (_fs == null || !_fs.CanRead)
                {
                    sr = new StreamReader(memoryRead);
                    br = new BinaryReader(memoryRead);
                }
                else
                {
                    sr = new StreamReader(_fs);
                    br = new BinaryReader(_fs);
                }
            }
            if (access == FileAccess.Write)
            {
                sw = new StreamWriter(ms);
                bw = new BinaryWriter(ms);
            }
        }
        private FileExtension Get_File_Ext()
        {
            ////unfinished
            return FileExtension.UnknownBinary;
        }
        #endregion


        #region Protected - BaseFile
        protected BinaryReader BReader { get { return br; } }
        protected int ReadInt32() { return br.ReadInt32(); }
        protected ushort ReadUInt16() { return br.ReadUInt16(); }
        protected byte[] ReadBytes(int count) { return br.ReadBytes(count); }
        protected byte ReadByte() { return br.ReadByte(); }
        protected string Readline() { return sr.ReadLine(); }
        protected bool CanRead() { return !sr.EndOfStream; }
        protected bool CanWrite() { return ms.CanWrite; }
        protected void Write(string s) { sw.Write(s);sw.Flush(); }
        protected void ReadSeek(int offset, SeekOrigin origin) { br.BaseStream.Seek(offset, origin); }
        protected void WriteSeek(int offset, SeekOrigin origin) { ms.Seek(offset, origin); }
        #endregion


        #region Public Methods - BaseFile
        public void Dump() { ms.WriteTo(_fs); ms = new MemoryStream(); }
        public void Dispose()
        {
            if (access == FileAccess.Read)
            {
                sr.Dispose();
                br.Dispose();
            }
            if (access == FileAccess.Write)
            {
                sw.Dispose();
                bw.Dispose();
            }
            if(_fs != null)_fs.Dispose();
            ms.Dispose();
            memoryRead.Dispose();
        }
        #endregion


        #region Public Calls - BaseFile
        public Stream ReadStream { get { return memoryRead; } }
        public FileExtension FileExt
        {
            get
            {
                if (extension == FileExtension.Undefined)
                {
                    return Get_File_Ext();
                }
                else
                {
                    return extension;
                }
            }
        }
        public string FullName
        {
            get { return fullname; }
        }
        public string FileName
        {
            get { return filename; }
        }
        public string FilePath
        {
            get { return filepath; }
        }
        public string NameExt
        {
            get { return nameext; }
        }
        #endregion
    }
}
