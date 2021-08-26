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
            //Engine.Refresh();
        }
        public void SwitchFlatGround(bool enable)
        {
            foreach (Tile t in TilesData)
            {
                t.FlatToGround(enable);
            }
            //Engine.Refresh();
        }
        public void FixEmptyTiles(int altitude = 0)
        {
            Tiles.FixEmptyTiles(Info.Size.Width, Info.Size.Height, altitude);
        }
    }
}
