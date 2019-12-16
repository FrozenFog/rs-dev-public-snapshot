using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using relert_sharp.Encoding;

namespace relert_sharp.MapStructure
{
    public class OverlayLayer
    {
        public OverlayLayer(string _overlayPackString, string _overlayDataPackString)
        {
            byte[] _ovlOut = new byte[262144];
            byte[] _ovldOut = new byte[262144];
            byte[] _frombase64O = Convert.FromBase64String(_overlayPackString);
            byte[] _frombase64D = Convert.FromBase64String(_overlayDataPackString);
            Format5.DecodeInto(_frombase64O, _ovlOut, 80);
            //Format80.DecodeInto(_frombase64O, _ovlOut);
            //Format80.DecodeInto(_frombase64D, _ovldOut);
            
        }
    }
}
