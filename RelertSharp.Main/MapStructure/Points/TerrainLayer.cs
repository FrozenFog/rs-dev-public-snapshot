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


        #region Public Methods - TerrainItem
        public IMapObject CopyNew()
        {
            TerrainItem terr = new TerrainItem(this);
            return terr;
        }
        #endregion


        #region Public Calls - TerrainItem
        public new string RegName { get; set; }
        #endregion
    }
}
