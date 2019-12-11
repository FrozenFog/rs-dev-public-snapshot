using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace relert_sharp.Utils
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
        private Constant.FileExtension extension = Constant.FileExtension.Undefined;
        private FileAccess access;
        public File(string path, FileMode m, FileAccess a)
        {
            fs = new FileStream(path, m, a);
            access = a;
            InitStream();
            filepath = fs.Name;
            string[] sl = filepath.Split(new char[] { '\\' });
            fullname = sl[sl.Count() - 1];
            if (fullname.Contains("."))
            {
                sl = fullname.Split(new char[] { '.' });
                filename = sl[0];
                nameext = sl[1];
            }
            else
            {
                filename = fs.Name;
            }
            switch (nameext.ToLower())
            {
                case "ini":
                    extension = Constant.FileExtension.INI;
                    break;
                case "csv":
                    extension = Constant.FileExtension.CSV;
                    break;
                case "map":
                    extension = Constant.FileExtension.MAP;
                    break;
                case "yrm":
                    extension = Constant.FileExtension.YRM;
                    break;
                case "txt":
                    extension = Constant.FileExtension.TXT;
                    break;
                case "lang":
                    extension = Constant.FileExtension.LANG;
                    break;
                default:
                    extension = Constant.FileExtension.Undefined;
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
        private Constant.FileExtension Get_File_Ext()
        {
            return Constant.FileExtension.UnknownBinary;
        }
        #region Public Methods
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
        #region Public Calls
        public Constant.FileExtension FileExt
        {
            get
            {
                if (extension == Constant.FileExtension.Undefined)
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
