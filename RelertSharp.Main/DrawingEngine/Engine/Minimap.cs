using System.Drawing;

namespace RelertSharp.DrawingEngine
{
    public partial class Engine
    {
        private GdipSurface minimap;


        public void ResizeMinimap(Size src)
        {
            minimap.ResetSize(src);
        }
        public void ResetMinimapWindow(Size client)
        {
            minimap.SetClientWindowSize(new Rectangle(Point.Empty, client));
        }


        public Bitmap MiniMap { get { return minimap.MiniMap; } }
    }
}
