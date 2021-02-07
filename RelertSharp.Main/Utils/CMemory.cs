// I wanna some C-style functions, I mean, the effeciency
// use these helpers carefully as I haven't tested tbem enough
// SEC_SOME 9/30/2020

// Marshal is still easier to use, don't u think so?

using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;

namespace RelertSharp.Utils
{
    using size_t = UInt32;

    public static class CMemory
    {
        private static Action<IntPtr, byte, int> memsetDelegate;
        static CMemory()
        {
            DynamicMethod dynamicMethod = new DynamicMethod(
                "memset",
                MethodAttributes.Public | MethodAttributes.Static, CallingConventions.Standard,
                null,
                new[] { typeof(IntPtr), typeof(byte), typeof(int) }, typeof(CMemory), true
                );
            ILGenerator generator = dynamicMethod.GetILGenerator();
            generator.Emit(OpCodes.Ldarg_0);
            generator.Emit(OpCodes.Ldarg_1);
            generator.Emit(OpCodes.Ldarg_2);
            generator.Emit(OpCodes.Initblk);
            generator.Emit(OpCodes.Ret);
            memsetDelegate = (Action<IntPtr, byte, int>)dynamicMethod.CreateDelegate(typeof(Action<IntPtr, byte, int>));
        }

        public static void memset(byte[] array, byte value, int length)
        {
            GCHandle gcHandle = GCHandle.Alloc(array, GCHandleType.Pinned);
            memsetDelegate(gcHandle.AddrOfPinnedObject(), value, length);
            gcHandle.Free();
        }
        
        public static void memcpy(byte[] des, size_t des_size, byte[] src, size_t src_size)
        {
            
        }

        public static int memcmp()
        {
            return 0;
        }


    }
}
