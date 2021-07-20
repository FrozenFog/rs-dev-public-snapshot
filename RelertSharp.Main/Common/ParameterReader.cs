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
        public void Skip(int count = 1)
        {
            while (count-- > 0) Incre();
        }

        private void Incre()
        {
            if (i == parameters.Length) i = 0;
            else i++;
        }
    }
}
