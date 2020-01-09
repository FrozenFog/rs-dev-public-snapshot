using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace relert_sharp.Encoding
{
    public static class CsfEncoding
    {
        public static string Decode(byte[] _data)
        {
            for (int i = 0; i < _data.Length; i++) _data[i] = (byte)~_data[i];
            return System.Text.Encoding.Unicode.GetString(_data);
        }
    }
}
