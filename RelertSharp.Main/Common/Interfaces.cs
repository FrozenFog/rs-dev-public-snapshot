using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelertSharp.Common
{
    public interface I2dLocateable
    {
        int X { get; }
        int Y { get; }
    }


    public interface I3dLocateable : I2dLocateable
    {
        int Z { get; }
    }
}
