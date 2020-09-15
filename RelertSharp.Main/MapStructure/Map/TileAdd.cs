namespace RelertSharp.MapStructure
{
    public partial class Map
    {
        public void AddTile(Tile t, out Tile original)
        {
            if (Tiles[t] is Tile org)
            {
                original = new Tile(org);
                t.SceneObject.SetColor(org.SceneObject.ColorVector);
                org.Dispose();
                org.ReplaceWith(t);
                Tiles[t] = t;
            }
            else original = null;
        }
    }
}
