using System.Drawing;
using RelertSharp.DrawingEngine.Presenting;
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
        public void RefreshMinimap()
        {
            SetMinimapClientPos();
        }
        public void MinimapMoving(Point panelClicked)
        {
            I2dLocateable pos = minimap.GetPointFromMinimapSeeking(Pnt.FromPoint(panelClicked));
            MoveTo(pos);
            Refresh();
        }
        public void RedrawMinimapAll()
        {
            foreach (PresentTile t in Buffer.Scenes.Tiles.Values)
            {
                if (t.TileObjects.Count == 0) minimap.DrawTile(t);
                else
                {
                    IPresentBase c = t.TileObjects[t.TileObjects.Count - 1];
                    minimap.DrawColorable(t, c);
                }
            }
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
