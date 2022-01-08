using RelertSharp.Common;
using RelertSharp.IniSystem;

namespace RelertSharp.MapStructure.Points
{
    [IniEntitySerialize(Constant.MapStructure.ENT_TERR)]
    public class TerrainLayer : PointCollectionBase<TerrainItem>
    {
        public TerrainLayer() { }
        protected override TerrainItem InvokeCtor()
        {
            return new TerrainItem();
        }
    }


    public class TerrainItem : PointItemBase, IMapObject
    {
        public TerrainItem(string _coord, string _name) : base(_coord)
        {
            RegName = _name;
            ObjectType = MapObjectType.Terrain;
        }
        public TerrainItem(string regname)
        {
            RegName = regname;
            ObjectType = MapObjectType.Terrain;
        }
        public TerrainItem(TerrainItem src) : base(src)
        {
            RegName = src.RegName;
            ObjectType = MapObjectType.Terrain;
        }
        internal TerrainItem()
        {
            ObjectType = MapObjectType.Terrain;
        }
        public override void ReadFromIni(INIPair src)
        {
            RegName = src.Value;
            if (!base.ReadCoord(src.Name)) GlobalVar.Monitor.LogCritical(Id, RegName, ObjectType, this);
        }
        public override INIPair SaveAsIni()
        {
            INIPair p = new INIPair(CoordString);
            p.Value = RegName;
            return p;
        }


        #region Public Methods - TerrainItem
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
            TerrainItem terr = new TerrainItem()
            {
                X = reader.ReadInt(),
                Y = reader.ReadInt(),
                RegName = reader.ReadString()
            };
            return terr;
        }
        #endregion


        #region Public Calls - TerrainItem
        public override string RegName { get; set; }
        #endregion
    }
}
