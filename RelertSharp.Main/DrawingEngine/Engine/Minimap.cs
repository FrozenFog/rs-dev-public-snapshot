using System.Drawing;
using RelertSharp.Common;

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
        public void MinimapMoving(Point panelClicked)
        {
            I2dLocateable pos = minimap.GetPointFromMinimapSeeking(Pnt.FromPoint(panelClicked));
            MoveTo(pos);
            Refresh();
        }
        

        private void SetMinimapClientPos()
        {
            Vec3 LT = new Vec3();
            CppExtern.Scene.ClientPositionToScenePosition(Pnt.Zero, ref LT);
            Vec3 coord = ScenePosToCoord(LT);
            minimap.ClientPos = new Point((int)coord.X, (int)coord.Y);
        }

        public Bitmap MiniMap { get { return minimap.MiniMap; } }
    }
}
