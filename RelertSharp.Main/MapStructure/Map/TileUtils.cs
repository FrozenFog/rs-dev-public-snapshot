using static RelertSharp.Common.GlobalVar;

namespace RelertSharp.MapStructure
{
    public partial class Map
    {
        public void SwitchToFramework(bool enable)
        {
            foreach (Tile t in TilesData)
            {
                t.SwitchToFramework(enable);
            }
            Engine.Refresh();
        }
        public void SwitchFlatGround(bool enable)
        {
            foreach (Tile t in TilesData)
            {
                t.FlatToGround(enable);
            }
            Engine.Refresh();
        }
        public void FixEmptyTiles()
        {
            Tiles.FixEmptyTiles(Info.Size.Width, info.Size.Height);
        }
    }
}
