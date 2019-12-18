#region Copyright & License Information

/*
 * Copyright 2007-2011 The OpenRA Developers
 * (see https://raw.github.com/OpenRA/OpenRA/master/AUTHORS)
 * This file is part of OpenRA, which is free software. It is made
 * available to you under the terms of the GNU General Public License
 * as published by the Free Software Foundation. For more information,
 * see COPYING.
 */

#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace relert_sharp.Encoding
{
    public static class Format80
    {
        private static void ReplicatePrevious(byte[] dest, int destIndex, int srcIndex, int count)
        {
            if (srcIndex > destIndex)
                throw new NotImplementedException(string.Format("srcIndex > destIndex  {0}  {1}", srcIndex, destIndex));

            if (destIndex - srcIndex == 1)
            {
                for (int i = 0; i < count; i++)
                    dest[destIndex + i] = dest[destIndex - 1];
            }
            else
            {
                for (int i = 0; i < count; i++)
                    dest[destIndex + i] = dest[srcIndex + i];
            }
        }

        public static int DecodeInto(byte[] src, byte[] dest)
        {
            MemoryStream ms = new MemoryStream(src);
            BinaryReader br = new BinaryReader(ms);
            int destIndex = 0;

            while (true)
            {
                byte i = br.ReadByte();
                if ((i & 0x80) == 0)
                {
                    // case 2
                    byte secondByte = br.ReadByte();
                    int count = ((i & 0x70) >> 4) + 3;
                    int rpos = ((i & 0xf) << 8) + secondByte;

                    ReplicatePrevious(dest, destIndex, destIndex - rpos, count);
                    destIndex += count;
                }
                else if ((i & 0x40) == 0)
                {
                    // case 1
                    int count = i & 0x3F;
                    if (count == 0)
                        return destIndex;

                    br.Read(dest, destIndex, count);
                    destIndex += count;
                }
                else
                {
                    int count3 = i & 0x3F;
                    if (count3 == 0x3E)
                    {
                        // case 4
                        int count = br.ReadInt16();
                        byte color = br.ReadByte();

                        for (int end = destIndex + count; destIndex < end; destIndex++)
                            dest[destIndex] = color;
                    }
                    else if (count3 == 0x3F)
                    {
                        // case 5
                        int count = br.ReadInt16();
                        int srcIndex = br.ReadInt16();
                        if (srcIndex >= destIndex)
                            throw new NotImplementedException(string.Format("srcIndex >= destIndex  {0}  {1}", srcIndex, destIndex));

                        for (int end = destIndex + count; destIndex < end; destIndex++)
                            dest[destIndex] = dest[srcIndex++];
                    }
                    else
                    {
                        // case 3
                        int count = count3 + 3;
                        int srcIndex = br.ReadInt16();
                        if (srcIndex >= destIndex)
                            throw new NotImplementedException(string.Format("srcIndex >= destIndex  {0}  {1}", srcIndex, destIndex));

                        for (int end = destIndex + count; destIndex < end; destIndex++)
                            dest[destIndex] = dest[srcIndex++];
                    }
                }
            }
        }
        #region Encode Methods Rewrite by FrozenFog
        public static byte[] Encode(byte[] src)
        {
            MemoryStream dest = new MemoryStream();
            BinaryWriter bw = new BinaryWriter(dest);
            byte[] buffer = new byte[63];
            int wBuffer = 0;
            for(int i = 0; i < src.Length - 1; i++)
            {
                if (src[i] == src[i + 1]) // single byte repeating, type 4
                {
                    byte[] type1Buffer = buffer.Take(wBuffer).ToArray();
                    if (type1Buffer.Length > 0)
                    {
                        byte flag = (byte)((byte)type1Buffer.Length + 0x80);
                        bw.Write(flag);         // flag and count
                        bw.Write(type1Buffer);  // content
                        wBuffer = 0;
                        buffer = new byte[63];
                    }
                    byte _savedByte = src[i++];
                    ushort count = 2;
                    while (i < src.Length - 1 && src[i+1] == _savedByte)
                    {
                        count++;
                        i++;
                    }
                    bw.Write((byte)0xFE); // flag
                    bw.Write(count); // count
                    bw.Write(_savedByte); // repeating byte
                    bw.Flush();
                }
                else // not single byte repeating, treat as type 1
                {
                    if (wBuffer > 63)
                    {
                        bw.Write((byte)0xBF);   // flag
                        bw.Write(buffer);       // content
                        buffer = new byte[63];
                        wBuffer = 0;
                        bw.Flush();
                    }
                    buffer[wBuffer++] = src[i];
                }
            }
            bw.Write((byte)0x80);
            bw.Flush();
            return dest.ToArray();
        }
        #region Original
        //public static byte[] Encode(byte[] src)
        //{
        //    /* quick & dirty format80 encoder -- only uses raw copy operator, terminated with a zero-run. */
        //    /* this does not produce good compression, but it's valid format80 */
        //    BinaryReader br = new BinaryReader(new MemoryStream(src));
        //    var ms = new MemoryStream();

        //    do
        //    {
        //        var len = Math.Min(br.BaseStream.Position, 0x3F);
        //        ms.WriteByte((byte)(0x80 | len));
        //        while (len-- > 0)
        //            ms.WriteByte(br.ReadByte());
        //    } while (br.BaseStream.CanRead);

        //    ms.WriteByte(0x80); // terminator -- 0-length run.
        //    br.Close();
        //    return ms.ToArray();
        //}
        #endregion
        #endregion
    }
}