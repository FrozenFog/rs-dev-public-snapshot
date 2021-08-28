using RelertSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RelertSharp.IniSystem
{
    [Serializable]
    public class INIPair : IChecksum
    {
        private string comment = "", preComment = "";
        private INIKeyType keytype;


        #region Ctor - INIPair
        public INIPair(string key, string value)
        {
            Name = key;
            Value = value;
            keytype = GetKeyType(key);
        }
        public INIPair(string key, dynamic value)
        {
            Name = key;
            Value = value;
        }
        public INIPair(string n, string val, string com, string _preComment)
        {
            Name = n;
            comment = com;
            preComment = _preComment;
            Value = val;
            keytype = GetKeyType(n);
        }
        public INIPair(INIPair src)
        {
            Name = src.Name;
            comment = src.comment;
            preComment = src.preComment;
            Value = src.Value;
            keytype = src.keytype;
        }
        public INIPair(string key)
        {
            Name = key;
            Value = "";
            keytype = INIKeyType.DefaultString;
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
        public int GetChecksum()
        {
            return Name.GetHashCode() << Value.GetHashCode() << preComment.GetHashCode() << comment.GetHashCode();
        }
        /// <summary>
        /// Try parse the value as bool
        /// </summary>
        /// <param name="def"></param>
        /// <returns></returns>
        public bool ParseBool(bool def = false)
        {
            if (Constant.BoolTrue.Contains(Value)) return true;
            else if (Constant.BoolFalse.Contains(Value)) return false;
            if (bool.TryParse(Value, out bool b)) return b;
            return def;
        }
        /// <summary>
        /// Try parse the value as int
        /// </summary>
        /// <param name="def"></param>
        /// <returns></returns>
        public int ParseInt(int def = 0)
        {
            if (int.TryParse(Value, out int i)) return i;
            return def;
        }
        public string GetString(string def = null)
        {
            string value = Value.ToString();
            if (string.IsNullOrEmpty(value)) return def;
            return value;
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
            if (string.IsNullOrEmpty(Value)) return new string[0];
            return ((string)Value).Split(new char[] { ',' });
        }
        /// <summary>
        /// Try parse the value as int list, default seperator is ","
        /// </summary>
        /// <returns></returns>
        public int[] ParseIntList()
        {
            List<int> result = new List<int>();
            IEnumerable<string> tmp = ParseStringList();
            if (tmp.Count() == 0) return result.ToArray();
            foreach (string s in tmp)
            {
                if (!string.IsNullOrEmpty(s))
                    result.Add(int.Parse(s));
            }
            return result.ToArray();
        }
        public override string ToString()
        {
            return string.Format("{0}={1};{2}", Name, Value, comment);
        }
        public string SaveString(bool ignoreComment = false)
        {
            string result = string.Format("{0}={1}", Name, Value);
            if (ignoreComment) return result;
            else
            {
                if (!PreComment.IsNullOrEmpty()) result = PreComment + result;
                if (!Comment.IsNullOrEmpty()) result += string.Format(";{0}", Comment);
                return result;
            }
        }
        #endregion


        #region Public Calls - INIPair
        public string Name { get; set; }
        public string Value { get; set; }
        public string Comment { get { return comment; } }
        public string PreComment { get { return preComment; } }
        public bool HasComment { get { return !string.IsNullOrEmpty(comment); } }
        public INIKeyType KeyType { get { return keytype; } }
        public static INIPair NullPair { get { return new INIPair("", "", "", ""); } }
        #endregion
    }
}
