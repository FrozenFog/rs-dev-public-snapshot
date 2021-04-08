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

namespace RelertSharp.Wpf.MapEngine.Helper
{
    internal static class Navigating
    {
        private static Point delta;
        private static Point downPos;
        private static bool moving = false;
        private static BackgroundWorker worker;


        internal static void BeginRightClickMove(Point down)
        {
            if (!moving)
            {
                moving = true;
                downPos = down;
                delta = new Point();
                RunMoving();
            }
        }

        internal static void UpdateDelta(Point now)
        {
            if (moving)
            {
                delta = (Point)(now - downPos);
            }
        }

        internal static void EndRightClickMove()
        {
            if (moving)
            {
                moving = false;
                HaltMoving();
            }
        }

        private static void RunMoving()
        {
            worker = new BackgroundWorker();
            worker.WorkerSupportsCancellation = true;
            worker.DoWork += DoWork;
            worker.RunWorkerAsync();
        }
        private static void HaltMoving()
        {
            worker.CancelAsync();
            worker.Dispose();
        }

        private static void DoWork(object sender, DoWorkEventArgs e)
        {
            Stopwatch t = new Stopwatch();
            long restMilSec = 1000 / EngineApi.MaxRate;
            while (true)
            {
                if (moving)
                {
                    t.Restart();
                    EngineApi.ShiftViewBy(delta.GdiPoint());
                    EngineApi.InvokeRedraw();
                    EngineApi.RedrawMinimapAll();
                    t.Stop();
                    long elapsed = t.ElapsedMilliseconds;
                    if (elapsed < restMilSec) Thread.Sleep((int)(restMilSec - elapsed));
                }
                else break;
            }
        }
    }
}
