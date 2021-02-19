using RelertSharp.Common;
using RelertSharp.MapStructure;
using System;
using System.Drawing;
using System.Windows.Media;
using System.IO;

namespace RelertSharp.Engine.Api
{
    public static partial class EngineApi
    {
        public static event EventHandler MinimapRedrawed;
        public static event EventHandler MinimapClientResizeRequest;
        public static void ResetMinimap(Rectangle mapSize, Size panelSize)
        {
            ResetMinimap(mapSize, panelSize.Width, panelSize.Height);
        }
        public static void ResetMinimap(Rectangle mapSize, int panelWidth, int panelHeight, double scaleFactor = 1)
        {
            EngineMain.ResetMiniMap(mapSize, new Size(panelWidth, panelHeight), scaleFactor);
            MinimapRedrawed?.Invoke(null, null);
        }
        public static void ResizeMinimap(int panelWidth, int panelHeight)
        {
            EngineMain.ResizeMinimap(new Size(panelWidth, panelHeight));
            MinimapRedrawed?.Invoke(null, null);
        }
        public static void ResizeMinimapClientWindow(int clientWidth, int clientHeight)
        {
            EngineMain.ResizeMinimapClientWindow(new Size(clientWidth, clientHeight));
            MinimapRedrawed?.Invoke(null, null);
        }
        public static void MinimapMoveTo(Point panelPoint)
        {
            EngineMain.MinimapMoving(panelPoint);
            MinimapRedrawed?.Invoke(null, null);
        }
        public static void RedrawMinimapAll()
        {
            EngineMain.RedrawMinimapAll();
            MinimapRedrawed?.Invoke(null, null);
        }

        public static Bitmap MiniMapImg { get { return EngineMain.MiniMap; } }
        public static ImageSource MinimapImgSource
        {
            get
            {
                if (MiniMapImg == null) return null;
                MemoryStream ms = new MemoryStream();
                MiniMapImg.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                ms.Position = 0;
                var img = new System.Windows.Media.Imaging.BitmapImage();
                img.BeginInit();
                img.StreamSource = ms;
                img.EndInit();
                //ms.Dispose();
                return img;
            }
        }
    }
}
