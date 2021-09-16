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
using System.IO;
using System.Linq;

namespace RelertSharp.Encoding
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
            for (int i = 0; i < src.Length - 1; i++)
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
                    while (i < src.Length - 1 && src[i + 1] == _savedByte)
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
        #region Encode Method from Electronic Art
        // https://github.com/electronicarts/CnC_Remastered_Collection/blob/7d496e8a633a8bbf8a14b65f490b4d21fa32ca03/CnCTDRAMapEditor/Utility/WWCompression.cs#L89
        // no more asm thank you ea

        //
        // Copyright 2020 Electronic Arts Inc.
        //
        // The Command & Conquer Map Editor and corresponding source code is free 
        // software: you can redistribute it and/or modify it under the terms of 
        // the GNU General Public License as published by the Free Software Foundation, 
        // either version 3 of the License, or (at your option) any later version.

        // The Command & Conquer Map Editor and corresponding source code is distributed 
        // in the hope that it will be useful, but with permitted additional restrictions 
        // under Section 7 of the GPL. See the GNU General Public License in LICENSE.TXT 
        // distributed with this program. You should have received a copy of the 
        // GNU General Public License along with permitted additional restrictions 
        // with this program. If not, see https://github.com/electronicarts/CnC_Remastered_Collection

        /// <summary>
        ///    Compresses data to the proprietary LCW format used in
        ///    many games developed by Westwood Studios. Compression is better
        ///    than that achieved by popular community tools. This is a new
        ///    implementation based on understanding of the compression gained from
        ///    the reference code.
        /// </summary>
        /// <param name="input">Array of the data to compress.</param>
        /// <returns>The compressed data.</returns>
        /// <remarks>Commonly known in the community as "format80".</remarks>
        public static Byte[] LcwCompress(Byte[] input)
        {
            if (input == null || input.Length == 0)
                return new Byte[0];

            //Decide if we are going to do relative offsets for 3 and 5 byte commands
            Boolean relative = input.Length > UInt16.MaxValue;

            // Nyer's C# conversion: replacements for write and read for pointers.
            Int32 getp = 0;
            Int32 putp = 0;
            // Input length. Used commonly enough to warrant getting it out in advance I guess.
            Int32 getend = input.Length;
            // "Worst case length" code by OmniBlade. We'll just use a buffer of
            // that max length and cut it down to the actual used size at the end.
            // Not using it- it's not big enough in case of some small images.
            //LCWWorstCase(getend)
            Int32 worstcase = Math.Max(10000, getend * 2);
            Byte[] output = new Byte[worstcase];
            // relative LCW starts with 0 as flag to decoder.
            // this is only used by later games for decoding hi-color vqa files.
            if (relative)
                output[putp++] = 0;

            //Implementations that properly conform to the WestWood encoder should
            //write a starting cmd1. It's important for using the offset copy commands
            //to do more efficient RLE in some cases than the cmd4.

            //we also set bool to flag that we have an on going cmd1.
            Int32 cmd_onep = putp;
            output[putp++] = 0x81;
            output[putp++] = input[getp++];
            Boolean cmd_one = true;

            //Compress data until we reach end of input buffer.
            while (getp < getend)
            {
                //Is RLE encode (4bytes) worth evaluating?
                if (getend - getp > 64 && input[getp] == input[getp + 64])
                {
                    //RLE run length is encoded as a short so max is UINT16_MAX
                    Int32 rlemax = (getend - getp) < UInt16.MaxValue ? getend : getp + UInt16.MaxValue;
                    Int32 rlep = getp + 1;
                    while (rlep < rlemax && input[rlep] == input[getp])
                        rlep++;

                    UInt16 run_length = (UInt16)(rlep - getp);

                    //If run length is long enough, write the command and start loop again
                    if (run_length >= 0x41)
                    {
                        //write 4byte command 0b11111110
                        cmd_one = false;
                        output[putp++] = 0xFE;
                        output[putp++] = (Byte)(run_length & 0xFF);
                        output[putp++] = (Byte)((run_length >> 8) & 0xFF);
                        output[putp++] = input[getp];
                        getp = rlep;
                        continue;
                    }
                }

                //current block size for an offset copy
                UInt16 block_size = 0;
                //Set where we start looking for matching runs.
                Int32 offstart = relative ? getp < UInt16.MaxValue ? 0 : getp - UInt16.MaxValue : 0;

                //Look for matching runs
                Int32 offchk = offstart;
                Int32 offsetp = getp;
                while (offchk < getp)
                {
                    //Move offchk to next matching position
                    while (offchk < getp && input[offchk] != input[getp])
                        offchk++;

                    //If the checking pointer has reached current pos, break
                    if (offchk >= getp)
                        break;

                    //find out how long the run of matches goes for
                    Int32 i;
                    for (i = 1; getp + i < getend; ++i)
                        if (input[offchk + i] != input[getp + i])
                            break;
                    if (i >= block_size)
                    {
                        block_size = (UInt16)i;
                        offsetp = offchk;
                    }
                    offchk++;
                }

                //decide what encoding to use for current run
                //If it's less than 2 bytes long, we store as is with cmd1
                if (block_size <= 2)
                {
                    //short copy 0b10??????
                    //check we have an existing 1 byte command and if its value is still
                    //small enough to handle additional bytes
                    //start a new command if current one doesn't have space or we don't
                    //have one to continue
                    if (cmd_one && output[cmd_onep] < 0xBF)
                    {
                        //increment command value
                        output[cmd_onep]++;
                        output[putp++] = input[getp++];
                    }
                    else
                    {
                        cmd_onep = putp;
                        output[putp++] = 0x81;
                        output[putp++] = input[getp++];
                        cmd_one = true;
                    }
                    //Otherwise we need to decide what relative copy command is most efficient
                }
                else
                {
                    Int32 offset;
                    Int32 rel_offset = getp - offsetp;
                    if (block_size > 0xA || ((rel_offset) > 0xFFF))
                    {
                        //write 5 byte command 0b11111111
                        if (block_size > 0x40)
                        {
                            output[putp++] = 0xFF;
                            output[putp++] = (Byte)(block_size & 0xFF);
                            output[putp++] = (Byte)((block_size >> 8) & 0xFF);
                            //write 3 byte command 0b11??????
                        }
                        else
                        {
                            output[putp++] = (Byte)((block_size - 3) | 0xC0);
                        }

                        offset = relative ? rel_offset : offsetp;
                        //write 2 byte command? 0b0???????
                    }
                    else
                    {
                        offset = rel_offset << 8 | (16 * (block_size - 3) + (rel_offset >> 8));
                    }
                    output[putp++] = (Byte)(offset & 0xFF);
                    output[putp++] = (Byte)((offset >> 8) & 0xFF);
                    getp += block_size;
                    cmd_one = false;
                }
            }

            //write final 0x80, basically an empty cmd1 to signal the end of the stream.
            output[putp++] = 0x80;

            Byte[] finalOutput = new Byte[putp];
            Array.Copy(output, 0, finalOutput, 0, putp);
            // Return the final compressed data.
            return finalOutput;
        }
        #endregion
    }
}