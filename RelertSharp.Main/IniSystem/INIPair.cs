using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.Common;

namespace RelertSharp.IniSystem
{
    [Serializable]
    public class INIPair
    {
        private string comment, preComment;
        private INIKeyType keytype;


        #region Ctor - INIPair
        public INIPair(string n, string val, string com, string _preComment)
        {
            Name = n;
            comment = com;
            preComment = _preComment;
            Value = val;
            keytype = GetKeyType(n);
        }
        #endregion


        #region Private Methods - INIPair
        /// <summary>
        /// Interpreter only
        /// </summary>
        /// <param name="keyname"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Try convert Pair value into destinate type.
        /// Eg: string to bool, int, etc.
        /// Works well with IniInterpreter, not recommended using elsewhere
        /// </summary>
        public void ConvValue()
        {
            if (Constant.BoolFalse.Contains((string)Value)) Value = false;
            else if (Constant.BoolTrue.Contains((string)Value)) Value = true;
            else if (Constant.NullString.Contains((string)Value) && keytype != INIKeyType.Armor)
            {
                Value = null;
            }
            if (Constant.KeyName.IntKey.Contains(Name)) Value = int.Parse(Value);
            else if (Constant.KeyName.FloatKey.Contains(Name)) Value = float.Parse(Value);
            else if (Constant.KeyName.PercentKey.Contains(Name)) Value = float.Parse(Value.Replace("%", string.Empty)) / 100;
        }
        /// <summary>
        /// Try parse the value as bool
        /// </summary>
        /// <param name="def"></param>
        /// <returns></returns>
        public bool ParseBool(bool def = false)
        {
            if ((string)Value != "")
            {
                if (Constant.BoolTrue.Contains((string)Value)) return true;
                else if (Constant.BoolFalse.Contains((string)Value)) return false;
                else if (int.Parse(Value) == 1) return true;
                else if (int.Parse(Value) == 0) return false;
            }
            return def;
        }
        /// <summary>
        /// Try parse the value as int
        /// </summary>
        /// <param name="def"></param>
        /// <returns></returns>
        public int ParseInt(int def = 0)
        {
            try
            {
                if ((string)Value != "")
                {
                    return int.Parse(Value);
                }
                return def;
            }
            catch { return 0; }
        }
        /// <summary>
        /// Try parse the value as float
        /// </summary>
        /// <param name="def"></param>
        /// <returns></returns>
        public float ParseFloat(float def = 0.0F)
        {
            try
            {
                if ((string)Value != "")
                {
                    return float.Parse(Value);
                }
                return def;
            }
            catch { return 0.0F; }
        }
        /// <summary>
        /// Try parse the value as string list, default seperator is ","
        /// </summary>
        /// <returns></returns>
        public string[] ParseStringList()
        {
            return ((string)Value).Split(new char[] { ',' });
        }
        /// <summary>
        /// Try parse the value as int list, default seperator is ","
        /// </summary>
        /// <returns></returns>
        public int[] ParseIntList()
        {
            List<int> result = new List<int>();
            foreach (string s in ParseStringList()) result.Add(int.Parse(s));
            return result.ToArray();
        }
        #endregion


        #region Public Calls - INIPair
        public string Name { get; set; }
        public dynamic Value { get; set; }
        public string Comment { get { return comment; } }
        public string PreComment { get { return preComment; } }
        public bool HasComment { get { return !string.IsNullOrEmpty(comment); } }
        public INIKeyType KeyType { get { return keytype; } }
        public static INIPair NullPair { get { return new INIPair("", "", "", ""); } }
        #endregion
    }
}
