using System;

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
