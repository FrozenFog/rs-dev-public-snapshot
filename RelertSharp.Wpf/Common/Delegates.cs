using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.Common;

namespace RelertSharp.Wpf
{
    public delegate void ContentCarrierHandler(object sender, object item);
    public delegate void IndexableHandler(IIndexableItem item);
    public delegate void RegnameHandler(string regname);
    public delegate void SoundPlayingHandler(string regname, SoundType type);
    public delegate void EnumerableObjectHandler(IEnumerable<object> objects);
}
