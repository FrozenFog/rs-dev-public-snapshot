using RelertSharp.Common;
using RelertSharp.Engine.MapObjects;
using RelertSharp.MapStructure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelertSharp.Engine
{
    internal static partial class EngineMain
    {
        private static GdipSurface minimap;


        public static void ResizeMinimap(Size src)
        {
            minimap.ResetSize(src);
        }
        public static void ResizeMinimapClientWindow(Size client)
        {
            minimap.SetClientWindowSize(new Rectangle(Point.Empty, client));
        }
        public static void MinimapMoving(Point panelClicked)
        {
            I2dLocateable pos = minimap.GetPointFromMinimapSeeking(Pnt.FromPoint(panelClicked));
            MoveTo(pos);
        }
        public static void RedrawMinimapAll()
        {
            TileLayer tiles = GlobalVar.GlobalMap.TilesData;
            foreach (Tile t in tiles)
            {
                if (t.MinimapRenderableObjectCount == 0) minimap.DrawTile(t.SceneObject as MapTile);
                else
                {
                    IMapObject obj = t.GetObjects()[t.MinimapRenderableObjectCount - 1];
                    minimap.DrawColorable(t, obj);
                }
            }
        }


        private static void SetMinimapClientPos()
        {
            Vec3 LT = new Vec3();
            CppExtern.Scene.ClientPositionToScenePositionLock(Pnt.Zero, ref LT);
            Vec3 coord = ScenePosToCoord(LT);
            minimap.ClientPos = new Point((int)coord.X, (int)coord.Y);
        }

        public static Bitmap MiniMap { get { return minimap.MiniMap; } }
    }
}
