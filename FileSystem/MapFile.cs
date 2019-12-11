using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using relert_sharp.Utils;

namespace relert_sharp.FileSystem
{
    public class MapFile : INIFile
    {
        public MapFile(string path) : base(path)
        {
            INIType = Constant.INIFileType.MapFile;
        }
    }
}
