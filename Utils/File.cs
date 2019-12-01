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
        private string filename;
        private string fullname;
        private string filepath;
        private Cons.FileExtension extension = Cons.FileExtension.Undefined;
        public File(string path, FileMode m, FileAccess a)
        {
            fs = new FileStream(path, m, a);
            filepath = path;
            string ext = "";
            if (fs.Name.Contains("."))
            {
                string[] sl = path.Split(new char[] { '.' });
                filename = sl[0];
                ext = sl[1];
                fullname = filename + "." + ext;
            }
            else
            {
                filename = fs.Name;
            }
            switch (ext.ToLower())
            {
                case "ini":
                    extension = Cons.FileExtension.INI;
                    break;
                case "csv":
                    extension = Cons.FileExtension.CSV;
                    break;
                case "map":
                    extension = Cons.FileExtension.MAP;
                    break;
                case "yrm":
                    extension = Cons.FileExtension.YRM;
                    break;
                case "txt":
                    extension = Cons.FileExtension.TXT;
                    break;
                case "lang":
                    extension = Cons.FileExtension.LANG;
                    break;
            }
        }
        private Cons.FileExtension Get_File_Ext()
        {
            return Cons.FileExtension.UnknownBinary;
        }
        #region Public Methods
        public List<string> readlines()
        {
            StreamReader sr = new StreamReader(fs);
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
            Close();
            StreamWriter sw = new StreamWriter(filepath);
            sw.Write(s);
            sw.Close();
            sw.Dispose();
        }
        public void Close()
        {
            fs.Close();
            fs.Dispose();
        }
        #endregion
        #region Public Calls
        public Cons.FileExtension FileExt
        {
            get
            {
                if (extension == Cons.FileExtension.Undefined)
                {
                    return Get_File_Ext();
                }
                else
                {
                    return extension;
                }
            }
        }
        #endregion
    }
}
