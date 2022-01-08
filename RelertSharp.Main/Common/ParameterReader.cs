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
        private bool error;
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
            bool b = byte.TryParse(value, out byte r);
            error = error || !b;
            if (!b) r = def;
            Incre();
            return r;
        }
        public float ReadFloat(float def = 0f)
        {
            if (IsEmpty) return def;
            string value = parameters[i].Trim();
            bool b = float.TryParse(value, out float r);
            error = error || !b;
            if (!b) r = def;
            Incre();
            return r;
        }
        public int ReadInt(int def = 0)
        {
            if (IsEmpty) return def;
            string value = parameters[i].Trim();
            bool b = int.TryParse(value, out int r);
            error = error || !b;
            if (!b) r = def;
            Incre();
            return r;
        }
        public bool ReadBool(bool def = false)
        {
            if (IsEmpty) return def;
            string value = parameters[i].Trim();
            Incre();
            if (value == "0") return false;
            if (value == "1") return true;
            bool b = bool.TryParse(value, out bool r);
            error = error || !b;
            if (!b) r = def;
            return r;
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
            if (i == parameters.Length)
            {
                i = 0;
                ReadOverflow = true;
            }
            else i++;
        }



        public bool CanRead { get { return i < parameters.Length; } }
        public bool ReadOverflow { get; private set; }
        public bool ReadError { get { return ReadOverflow || error; } }
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
        public void Write(double value, int digits = 1)
        {
            digits = digits.TrimTo(0, 8);
            string format = "0.";
            if (digits <= 0) Write(value.ToString("0"));
            else
            {
                while (digits-- > 0) format += "0";
            }
            Write(value.ToString(format));
        }
        /// <summary>
        /// 0: Zero / One
        /// 1: Yes / No
        /// else: ToString()
        /// </summary>
        /// <param name="value"></param>
        /// <param name="type"></param>
        public void Write(bool value, int type = 0)
        {
            switch (type)
            {
                case 0:
                    Write(value.ZeroOne());
                    break;
                case 1:
                    Write(value.YesNo());
                    break;
                default:
                    Write(value.ToString());
                    break;
            }
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
