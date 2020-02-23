using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using relert_sharp.Common;
using relert_sharp.Encoding;

namespace relert_sharp.FileSystem
{
    public class CsfFile : BaseFile
    {
        private Dictionary<string, CsfString> data = new Dictionary<string, CsfString>();


        #region Constructor - CsfFile
        public CsfFile(byte[] _rawData, string _fullName) : base(_rawData, _fullName)
        {
            Read();
        }
        public CsfFile(Stream _baseStream, string _fullName) : base(_baseStream, _fullName)
        {
            Read();
        }
        #endregion


        #region Private Methods - CsfFile
        private void Read()
        {
            ReadBytes(4);//0x20465343
            CsfVersion = ReadInt32();
            LabelCount = ReadInt32();
            StringCount = ReadInt32();
            ReadInt32();//unused
            int langID = ReadInt32();
            if (langID > 9 || langID < 0) langID = -1;
            Language = (CsfLanguage)langID;
            //labels
            for(int i = 0; i < LabelCount; i++)
            {
                int _identifier = ReadInt32();
                if (_identifier == 0x4C424C20)//" LBL"
                {
                    int _numPairs = ReadInt32();
                    int _lenLblName = ReadInt32();
                    string labelName = ReadString(_lenLblName);
                    int _subidentity = ReadInt32();
                    int _lenLblValue = ReadInt32();
                    byte[] _value = ReadBytes(_lenLblValue * 2);
                    data[labelName] = new CsfString(labelName);
                    data[labelName].ContentString = CsfEncoding.Decode(_value);
                    switch (_subidentity)
                    {
                        case 0x53545220://" RTS"
                            break;
                        case 0x53545257://"WRTS"
                            int _lenEx = ReadInt32();
                            string _valueEx = ReadString(_lenEx);
                            data[labelName].ExtraString = _valueEx;
                            break;
                    }
                }
            }
        }
        #endregion


        #region Public Methods - CsfFile
        public bool HasString(string _uiTag) { return data.Keys.Contains(_uiTag); }
        #endregion


        #region Public Calls - CsfFile
        public int CsfVersion { get; private set; }
        public int LabelCount { get; private set; }
        public int StringCount { get; private set; }
        public CsfLanguage Language { get; private set; }
        public CsfString this[string _uiTag] { get { return data[_uiTag.ToLower()]; } }
        #endregion
    }


    public class CsfString
    {



        #region Constructor - CSFString
        public CsfString(string _uiTag)
        {
            UIName = _uiTag;
        }
        #endregion


        #region Public Calls - CSFString
        public string UIName { get; set; } = "";
        public string ContentString { get; set; } = "";
        public string ExtraString { get; set; } = "";
        public bool HasExtra { get { return !string.IsNullOrEmpty(ExtraString); } }
        #endregion
    }
}
