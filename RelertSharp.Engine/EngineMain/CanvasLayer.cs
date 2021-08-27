using System;
using System.Collections.Generic;
using System.Windows.Shapes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using RelertSharp.Common;
using System.Windows.Media;
using System.Windows;

namespace RelertSharp.Engine
{
    internal static partial class EngineMain
    {
        private static Canvas cnvMain;
        private static double windowScale = 1;
        private static Rectangle visibleArea, movableArea;
        private static int mapWidth;
        private static double canvasTop, canvasLeft;

        private static double Scale(int input)
        {
            return input / (windowScale * Api.EngineApi.ScaleFactor);
        }
        public static void BindCanvas(Canvas canvas)
        {
            cnvMain = canvas;
            PresentationSource source = PresentationSource.FromVisual(cnvMain);
            if (source != null) windowScale = source.CompositionTarget.TransformToDevice.M11;
        }
        public static void RefreshCanvasPosition()
        {
            var pos = CellPosToClientPos(new Pnt3(1, mapWidth, 0));
            cnvMain.Parent.Dispatcher.Invoke(() =>
            {
                canvasTop = Scale(pos.Y - 15);
                canvasLeft = Scale(pos.X - 30);
                cnvMain.Margin = new System.Windows.Thickness()
                {
                    Top = canvasTop,
                    Left = canvasLeft
                };
            });
        }
        public static void SetCanvasSize(System.Drawing.Rectangle size)
        {
            cnvMain.Width = Scale(size.Width * 60);
            cnvMain.Height = Scale(size.Height * 30 + 15);
            mapWidth = size.Width;
        }
        public static void DrawMapBorder(int mapWidth, System.Drawing.Rectangle visArea)
        {
            bool add = visibleArea == null || movableArea == null;
            Algorithm.MapPosition.GetVisibleArea(mapWidth, visArea, out I2dLocateable visLT, out I2dLocateable visRB);
            Algorithm.MapPosition.GetMovableArea(mapWidth, visArea, out I2dLocateable movLT, out I2dLocateable movRB);
            var vLt = CellPosToClientPos(new Pnt3(visLT, 0));
            var vRb = CellPosToClientPos(new Pnt3(visRB, 0));
            var mLt = CellPosToClientPos(new Pnt3(movLT, 0));
            var mRb = CellPosToClientPos(new Pnt3(movRB, 0));
            if (add)
            {
                movableArea = new Rectangle()
                {
                    Width = Scale(mRb.X - mLt.X),
                    Height = Scale(mRb.Y - mLt.Y + 20),
                    Stroke = new SolidColorBrush(Colors.Green),
                    StrokeThickness = 5
                };
                visibleArea = new Rectangle()
                {
                    Width = Scale(vRb.X - vLt.X),
                    Height = Scale(vRb.Y - vLt.Y + 20),
                    Stroke = new SolidColorBrush(Colors.Red),
                    StrokeThickness = 5
                };
                cnvMain.Children.Add(visibleArea);
                cnvMain.Children.Add(movableArea);
            }
            else
            {
                movableArea.Width = Scale(mRb.X - mLt.X);
                movableArea.Height = Scale(mRb.Y - mLt.Y + 20);
                visibleArea.Width = Scale(vRb.X - vLt.X);
                visibleArea.Height = Scale(vRb.Y - vLt.Y + 20);
            }
            Canvas.SetTop(visibleArea, Scale(vLt.Y) - canvasTop);
            Canvas.SetLeft(visibleArea, Scale(vLt.X) - canvasLeft);
            Canvas.SetTop(movableArea, Scale(mLt.Y) - canvasTop);
            Canvas.SetLeft(movableArea, Scale(mLt.X) - canvasLeft);
        }
    }
}
