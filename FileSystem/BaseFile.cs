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
        private FileStream fs;
        private BinaryReader br;
        private BinaryWriter bw;
        private MemoryStream ms = new MemoryStream();
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
            fs = new FileStream(path, m, a);
            access = a;
            InitStream();
            filepath = fs.Name;
            string[] sl = filepath.Split(new char[] { '\\' });
            fullname = sl[sl.Count() - 1];
            GetNames();
        }
        public BaseFile(byte[] rawData, string fileName)
        {
            MemoryStream buffer = new MemoryStream(rawData);
            buffer.CopyTo(fs);
            access = FileAccess.Read;
            fullname = filename;
            GetNames();
            buffer.Dispose();
        }
        public BaseFile(Stream stream, string fileName)
        {
            stream.CopyTo(fs);
            access = FileAccess.Read;
            fullname = filename;
            GetNames();
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
                filename = fs.Name;
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
                default:
                    extension = FileExtension.Undefined;
                    break;
            }
        }
        private void InitStream()
        {
            if (fs.CanRead)
            {
                sr = new StreamReader(fs);
                br = new BinaryReader(fs);
            }
            if (fs.CanWrite)
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
        protected void ReadSeek(int offset, SeekOrigin origin) { fs.Seek(offset, origin); }
        protected void WriteSeek(int offset, SeekOrigin origin) { ms.Seek(offset, origin); }
        #endregion


        #region Public Methods - BaseFile
        public void Dump() { ms.WriteTo(fs); ms = new MemoryStream(); }
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
            fs.Dispose();
            ms.Dispose();
        }
        #endregion


        #region Public Calls - BaseFile
        public Stream ReadStream
        {
            get
            {
                MemoryStream mem = new MemoryStream();
                fs.CopyTo(mem);
                return mem;
            }
        }
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
