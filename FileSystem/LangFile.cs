using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using relert_sharp.Utils;

namespace relert_sharp.FileSystem
{
    public class LangFile : INIFile
    {
        public LangFile(string path, bool removeSpace) : base(path,removeSpace=false)
        {
            
        }
        
    }
}
