using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using relert_sharp.Common;

namespace relert_sharp.FileSystem
{
    public class INIPair
    {
        private string name;
        private dynamic value;
        private string comment, preComment;
        private INIKeyType keytype;


        public INIPair(string n, string val, string com, string _preComment)
        {
            name = n;
            comment = com;
            preComment = _preComment;
            value = val;
            keytype = GetKeyType(n);
        }


        #region Private Methods - INIPair
        private INIKeyType GetKeyType(string keyname)
        {
            if (Constant.Interpreter.SightLike.Contains(keyname))
            {
                return INIKeyType.SightLike;
            }
            else if (Constant.Interpreter.ActiveBoolLike.Contains(keyname))
            {
                return INIKeyType.ActiveLike;
            }
            else if (Constant.Interpreter.PassiveBoolLike.Contains(keyname))
            {
                return INIKeyType.PassiveLike;
            }
            else if (Constant.Interpreter.AcquireBoolLike.Contains(keyname))
            {
                return INIKeyType.AcquireLike;
            }
            else if (Constant.Interpreter.MultiplierLike.Contains(keyname))
            {
                return INIKeyType.MultiplierLike;
            }
            else if (Constant.Interpreter.NameLike.Contains(keyname))
            {
                return INIKeyType.NameLike;
            }
            else if (Constant.Interpreter.NameListLike.Contains(keyname))
            {
                return INIKeyType.NameListLike;
            }
            else if (Constant.Interpreter.NumListLike.Contains(keyname))
            {
                return INIKeyType.NumListLike;
            }
            else if (keyname.Contains("Versus.") && !keyname.Contains("Retaliate") && !keyname.Contains("PassiveAcquire"))
            {
                return INIKeyType.VersusLike;
            }
            else if (keyname == "Verses")
            {
                return INIKeyType.VersesListLike;
            }
            else if (keyname == "Armor")
            {
                return INIKeyType.Armor;
            }
            else if (keyname == "")
            {
                return INIKeyType.Null;
            }
            else
            {
                return INIKeyType.DefaultString;
            }
        }
        #endregion


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
        public string Name { get { return name; } }
        public dynamic Value { get { return value; } }
        public string Comment { get { return comment; } }
        public string PreComment { get { return preComment; } }
        public bool HasComment { get { return !string.IsNullOrEmpty(comment); } }
        public INIKeyType KeyType { get { return keytype; } }
        public static INIPair NullPair { get { return new INIPair("", "", "", ""); } }
        #endregion
    }
}
