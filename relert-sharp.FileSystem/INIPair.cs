using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using relert_sharp.Utils;

namespace relert_sharp.FileSystem
{
    public class INIPair
    {
        private string name;
        private dynamic value;
        private string comment;
        private INIKeyType keytype;
        public INIPair(string n, string val, string com)
        {
            name = n;
            comment = com;
            value = val;
            keytype = Misc.GetKeyType(n);
        }
        #region Public Methods - INIPair
        public void ConvValue()
        {
            if (Constant.BoolFalse.Contains((string)value)) value = false;
            else if (Constant.BoolTrue.Contains((string)value)) value = true;
            else if (Constant.NullString.Contains((string)value) && keytype != INIKeyType.Armor)
            {
                value = null;
            }
            if (Constant.KeyName.IntKey.Contains(name)) value = int.Parse(value);
            else if (Constant.KeyName.FloatKey.Contains(name)) value = float.Parse(value);
            else if (Constant.KeyName.PercentKey.Contains(name)) value = float.Parse(value.Replace("%", string.Empty)) / 100;
        }
        public bool ParseBool(bool def = false)
        {
            if ((string)value != "")
            {
                if (Constant.BoolTrue.Contains((string)value)) return true;
                else if (Constant.BoolFalse.Contains((string)value)) return false;
                else if (int.Parse(value) == 1) return true;
                else if (int.Parse(value) == 0) return false;
            }
            return def;
        }
        public int ParseInt(int def = 0)
        {
            if ((string)value != "")
            {
                return int.Parse(value);
            }
            return def;
        }
        public float ParseFloat(float def = 0F)
        {
            if ((string)value != "")
            {
                return float.Parse(value);
            }
            return def;
        }
        public string[] ParseStringList()
        {
            return ((string)value).Split(new char[] { ',' });
        }
        public int[] ParseIntList()
        {
            List<int> result = new List<int>();
            foreach (string s in ParseStringList()) result.Add(int.Parse(s));
            return result.ToArray();
        }
        #endregion
        #region Public Calls - INIPair
        public string Name
        {
            get { return name; }
        }
        public dynamic Value
        {
            get { return value; }
        }
        public string Comment
        {
            get { return comment; }
        }
        public INIKeyType KeyType
        {
            get { return keytype; }
        }
        public static INIPair NullPair
        {
            get { return new INIPair("", "", ""); }
        }
        #endregion
    }
}
