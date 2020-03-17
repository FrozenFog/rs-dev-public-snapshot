using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelertSharp.IniSystem
{
    public interface IRegistable
    {
        string ID { get; }
        string Name { get; }
    }
}
