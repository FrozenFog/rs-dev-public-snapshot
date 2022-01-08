using RelertSharp.Common;
using RelertSharp.IniSystem;
using System.Collections.Generic;

namespace RelertSharp.MapStructure.Points
{
    [IniEntitySerialize(Constant.MapStructure.ENT_SMG)]
    public class SmudgeLayer : PointCollectionBase<SmudgeItem>
    {
        public SmudgeLayer() { }
        protected override SmudgeItem InvokeCtor()
        {
            return new SmudgeItem();
        }
    }


    public class SmudgeItem : PointItemBase, IMapObject
    {
        private int szX = -1, szY = -1;
        public SmudgeItem(string _name, int x, int y, bool _ignore) : base(x, y)
        {
            RegName = _name;
            IgnoreSmudge = _ignore;
            ObjectType = MapObjectType.Smudge;
        }
        public SmudgeItem(string regname)
        {
            RegName = regname;
            ObjectType = MapObjectType.Smudge;
        }
        public SmudgeItem(SmudgeItem src) : base(src)
        {
            RegName = src.RegName;
            ObjectType = MapObjectType.Smudge;
        }
        internal SmudgeItem()
        {
            ObjectType = MapObjectType.Smudge;
        }
        public override void ReadFromIni(INIPair src)
        {
            Id = src.Name;
            ParameterReader r = new ParameterReader(src.ParseStringList());
            RegName = r.ReadString();
            X = r.ReadInt();
            Y = r.ReadInt();
            IgnoreSmudge = r.ReadBool(true);
            if (r.ReadError) GlobalVar.Monitor.LogCritical(Id, RegName, ObjectType, this);
        }
        public override INIPair SaveAsIni()
        {
            INIPair p = new INIPair(Id);
            ParameterWriter w = new ParameterWriter();
            w.Write(RegName);
            w.Write(X);
            w.Write(Y);
            w.Write(IgnoreSmudge);
            p.Value = w.ToString();
            return p;
        }


        #region Public Methods - SmudgeItem
        public override void ApplyConfig(IMapObjectBrushConfig config, IObjectBrushFilter filter, bool applyPosAndName = false)
        {
            base.ApplyConfig(config, filter, applyPosAndName);
            if (applyPosAndName) RegName = config.RegName;
        }
        public string[] ExtractParameter()
        {
            return new string[]
            {
                X.ToString(),
                Y.ToString(),
                RegName
            };
        }
        public int GetChecksum()
        {
            unchecked
            {
                int hash = Constant.BASE_HASH;
                hash = hash * 11 + X;
                hash = hash * 11 + Y;
                hash = hash * 11 + RegName.GetHashCode();
                return hash;
            }
        }
        public IMapObject ConstructFromParameter(string[] command)
        {
            ParameterReader reader = new ParameterReader(command);
            SmudgeItem smg = new SmudgeItem()
            {
                X = reader.ReadInt(),
                Y = reader.ReadInt(),
                RegName = reader.ReadString()
            };
            return smg;
        }
        #endregion


        #region Public Calls - SmudgeItem
        public override string RegName { get; set; }
        public bool IgnoreSmudge { get; set; }
        public int SizeX
        {
            get
            {
                if (szX == -1)
                {
                    GlobalVar.GlobalRules.GetSmudgeSizeData(RegName, out int x, out int y);
                    szX = x;
                    szY = y;
                }
                return szX;
            }
            set { szX = value; }
        }
        public int SizeY
        {
            get
            {
                if (szY == -1)
                {
                    GlobalVar.GlobalRules.GetSmudgeSizeData(RegName, out int x, out int y);
                    szX = x;
                    szY = y;
                }
                return szY;
            }
            set { szY = value; }
        }
        public IEnumerable<object> SaveData
        {
            get
            {
                return new List<object>
                {
                    RegName, X, Y, 0
                };
            }
        }
        #endregion
    }
}
