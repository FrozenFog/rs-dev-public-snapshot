using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using relert_sharp.Common;

namespace relert_sharp.FileSystem
{
    public class File
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
        public File(string path, FileMode m, FileAccess a)
        {
            fs = new FileStream(path, m, a);
            access = a;
            InitStream();
            filepath = fs.Name;
            string[] sl = filepath.Split(new char[] { '\\' });
            fullname = sl[sl.Count() - 1];
            GetNames();
        }
        public File(byte[] rawData, string fileName)
        {
            MemoryStream buffer = new MemoryStream(rawData);
            buffer.CopyTo(fs);
            access = FileAccess.Read;
            fullname = filename;
            GetNames();
            buffer.Dispose();
        }
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
        #region Public Methods - File
        public List<string> readlines()
        {
            var result = new List<string>();
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                result.Add(line);
            }
            return result;
        }
        public void Write(string s)
        {
            sw.Write(s);
        }
        public void Dump()
        {
            ms.WriteTo(fs);
        }
        public void Close()
        {
            fs.Dispose();
            ms.Dispose();
        }
        #endregion
        #region Public Calls - File
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
