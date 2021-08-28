using RelertSharp.Common;

namespace RelertSharp.MapStructure.Points
{
    public class TerrainLayer : PointCollectionBase<TerrainItem>
    {
        public TerrainLayer() { }
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
