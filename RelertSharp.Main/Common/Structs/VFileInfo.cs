using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace RelertSharp.Common
{
    public struct VFileInfo : IDisposable
    {
        public IntPtr ptr;
        public uint size;

        public void Dispose()
        {
            //if (ptr != IntPtr.Zero)
            //{
            //    Marshal.FreeHGlobal(ptr);
            //    ptr = IntPtr.Zero;
            //}
        }
    }
}
