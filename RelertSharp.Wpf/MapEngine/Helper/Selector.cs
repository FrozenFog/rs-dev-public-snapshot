using RelertSharp.Engine.Api;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RelertSharp.Wpf.MapEngine.Helper
{
    internal static class Selector
    {
        private static Point selectDown, selectNew;
        private static int subCell;
        private static bool dragingSelectBox;
        private static bool isIsometric;

        private static Canvas src;
        private static SolidColorBrush b = new SolidColorBrush(Colors.White);
        private static Rectangle rect;


        internal static void BeginSelecting(Point downPos, bool isometric, Canvas graphic)
        {
            if (!dragingSelectBox)
            {
                selectDown = downPos;
                dragingSelectBox = true;
                isIsometric = isometric;
                src = graphic;
                rect = new Rectangle()
                {
                    Stroke = b,
                    StrokeThickness = 1
                };
                src.Children.Add(rect);
                Canvas.SetTop(rect, downPos.Y);
                Canvas.SetLeft(rect, downPos.X);
            }
        }
        internal static void EndSelecting()
        {
            if (dragingSelectBox)
            {
                dragingSelectBox = false;

                src.Children.Clear();
                rect = null;
                EngineApi.InvokeRedraw();
            }
        }
        internal static void UpdateSelectingRectangle(Point newPos)
        {
            if (dragingSelectBox)
            {
                selectNew = newPos;
                Canvas.SetTop(rect, Math.Min(selectNew.Y, selectDown.Y));
                Canvas.SetLeft(rect, Math.Min(selectNew.X, selectDown.X));
                rect.Width = Math.Abs(selectNew.X - selectDown.X);
                rect.Height = Math.Abs(selectNew.Y - selectDown.Y);
            }
        }


        //private static void DoWork(object sender, DoWorkEventArgs e)
        //{
        //    Stopwatch t = new Stopwatch();
        //    long restMilSec = 1000 / EngineApi.MaxRate;
        //    while (true)
        //    {
        //        if (dragingSelectBox)
        //        {
        //            t.Restart();

        //            t.Stop();
        //            long elapsed = t.ElapsedMilliseconds;
        //            if (elapsed < restMilSec) Thread.Sleep((int)(restMilSec - elapsed));
        //        }
        //        else break;
        //    }
        //}
    }
}
