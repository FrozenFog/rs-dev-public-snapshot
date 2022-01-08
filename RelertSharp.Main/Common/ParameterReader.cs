using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelertSharp.Common
{
    public class ParameterReader
    {
        private string[] parameters;
        private int i = 0;
        private bool IsEmpty { get { return parameters.Length == 0; } }
        public ParameterReader(string parameter, char spliter = ',')
        {
            parameters = parameter.Split(spliter);
        }
        public ParameterReader(string[] param)
        {
            parameters = param;
        }


        public byte ReadByte(byte def = 0)
        {
            if (IsEmpty) return def;
            string value = parameters[i].Trim();
            def = (byte)value.ParseInt(def);
            Incre();
            return def;
        }
        public float ReadFloat(float def = 0f)
        {
            if (IsEmpty) return def;
            string value = parameters[i].Trim();
            def = value.ParseFloat(def);
            Incre();
            return def;
        }
        public int ReadInt(int def = 0)
        {
            if (IsEmpty) return def;
            string value = parameters[i].Trim();
            def = value.ParseInt(def);
            Incre();
            return def;
        }
        public bool ReadBool(bool def = false)
        {
            if (IsEmpty) return def;
            string value = parameters[i].Trim();
            def = value.ParseBool(def);
            Incre();
            return def;
        }
        public string ReadString(bool trim = true)
        {
            if (IsEmpty) return string.Empty;
            string result = parameters[i];
            if (trim) result = result.Trim();
            Incre();
            return result;
        }
        public string[] Take(int count)
        {
            if (count == 0) return new string[] { };
            string[] result = parameters.Skip(i).Take(count).ToArray();
            Skip(count);
            return result;
        }
        public string[] TakeToEnd()
        {
            int length = parameters.Length - i;
            string[] result = parameters.Skip(i).Take(length).ToArray();
            Skip(length);
            return result;
        }
        public void Skip(int count = 1)
        {
            while (count-- > 0) Incre();
        }

        private void Incre()
        {
            if (i == parameters.Length) i = 0;
            else i++;
        }



        public bool CanRead { get { return i < parameters.Length; } }
    }

    public class ParameterWriter
    {
        private StringBuilder sb;
        private string sep = ",";
        private bool begin = true;
        public ParameterWriter(string separator = ",")
        {
            sb = new StringBuilder();
            sep = separator;
        }


        public void Write(int value)
        {
            Write(value.ToString());
        }
        public void Write(string value)
        {
            if (begin)
            {
                sb.Append(value);
                begin = false;
            }
            else
            {
                sb.Append(sep);
                sb.Append(value);
            }
        }
        public void Write(string[] arr)
        {
            Write(arr.JoinBy(sep));
        }
        public override string ToString()
        {
            return sb.ToString();
        }
        public void Reset(string separator = ",")
        {
            begin = true;
            sb.Clear();
            sep = separator;
        }
    }
}
