using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace relert_sharp.Utils
{
    class File
    {
        private FileStream fs;
        public File(string path)
        {
            fs = new FileStream(path, FileMode.Open, FileAccess.Read);
        }
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
    }
}
