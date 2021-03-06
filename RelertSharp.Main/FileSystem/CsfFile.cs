using RelertSharp.Common;
using RelertSharp.Encoding;
using RelertSharp.IniSystem;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RelertSharp.FileSystem
{
    public class CsfFile : BaseFile, IEnumerable<CsfString>
    {
        private Dictionary<string, CsfString> data = new Dictionary<string, CsfString>();


        #region Ctor - CsfFile
        public CsfFile(byte[] _rawData, string _fullName) : base(_rawData, _fullName)
        {
            GlobalVar.Log.Info("Reading Csf, name: " + _fullName);
            Read();
        }
        public CsfFile(Stream _baseStream, string _fullName) : base(_baseStream, _fullName)
        {
            Read();
        }
        public CsfFile() { }
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
            for (int i = 0; i < LabelCount; i++)
            {
                int _identifier = ReadInt32();
                if (_identifier == 0x4C424C20)//" LBL"
                {
                    int _numPairs = ReadInt32();
                    int _lenLblName = ReadInt32();
                    string labelName = ReadString(_lenLblName).ToLower();
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
                    for (int j = 1; j < _numPairs; ++j)
                    {
                        int sub = ReadInt32();
                        int tmp = ReadInt32();
                        ReadBytes(tmp << 1);
                        if (sub == 0x53545257)
                        {
                            tmp = ReadInt32();
                            ReadString(tmp);
                        }
                    }
                }
            }
        }
        #endregion


        #region Public Methods - CsfFile
        public bool HasString(string _uiTag) { return data.Keys.Contains(_uiTag); }
        public void AddCsfLib(CsfFile csf)
        {
            foreach (CsfString s in csf)
            {
                data[s.UIName] = s;
            }
        }
        #region Enumerator
        public IEnumerator<CsfString> GetEnumerator()
        {
            return data.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return data.Values.GetEnumerator();
        }
        #endregion
        #endregion


        #region Public Calls - CsfFile
        public int CsfVersion { get; private set; }
        public int LabelCount { get; private set; }
        public int StringCount { get; private set; }
        public CsfLanguage Language { get; private set; }
        public CsfString this[string _uiTag]
        {
            get
            {
                _uiTag = _uiTag.ToLower();
                if (HasString(_uiTag)) return data[_uiTag];
                else
                {
                    CsfString csf = new CsfString(_uiTag);
                    csf.ContentString = "MISSING:" + _uiTag;
                    return csf;
                }
            }
        }
        #endregion
    }


    public class CsfString : IIndexableItem
    {



        #region Ctor - CSFString
        public CsfString(string _uiTag)
        {
            UIName = _uiTag;
        }
        #endregion


        #region Public Methods - CsfString
        public override string ToString()
        {
            return UIName;
        }

        public void ChangeDisplay(IndexableDisplayType type)
        {
            // csf always show id and name
            return;
        }
        #endregion


        #region Public Calls - CSFString
        public string Id { get { return UIName; } set { } }
        public string Name { get { return ContentString; } set { } }
        public string Value { get { return ContentString; } }
        public string UIName { get; set; } = "";
        public string ContentString { get; set; } = "";
        public string ExtraString { get; set; } = "";
        public bool HasExtra { get { return !string.IsNullOrEmpty(ExtraString); } }
        #endregion
    }
}
