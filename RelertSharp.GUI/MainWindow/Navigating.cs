using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using RelertSharp.DrawingEngine;
using RelertSharp.FileSystem;
using RelertSharp.MapStructure;
using RelertSharp.MapStructure.Points;
using RelertSharp.MapStructure.Objects;
using RelertSharp.MapStructure.Logic;
using RelertSharp.IniSystem;
using RelertSharp.Common;
using RelertSharp.GUI.Model;

namespace RelertSharp.GUI
{
    public partial class MainWindowTest
    {
        private bool onMoving = false;
        private bool onRmbMoving = false;
        private bool minimapMoving = false;
        private bool spaceKeyMoving = false;
        private Point previousLocation;
        private Point rmbMoveDownLocation;

        private void BeginMove(MouseEventArgs e)
        {
            onMoving = true;
            previousLocation = e.Location;
        }
        private void MainPanelMoving(MouseEventArgs e)
        {
            if (onMoving)
            {
                GlobalVar.Engine.ViewShift(previousLocation, e.Location);
                pnlMiniMap.BackgroundImage = GlobalVar.Engine.MiniMap;
                previousLocation = e.Location;
            }
        }
        private void MinimapMoving(MouseEventArgs e)
        {
            if (initialized && drew && minimapMoving)
            {
                GlobalVar.Engine.MinimapMoving(e.Location);
                pnlMiniMap.BackgroundImage = GlobalVar.Engine.MiniMap;
                pnlMiniMap.Refresh();
            }
        }


        private Point rmbMoveDelta;
        private bool rmbMoving = false;
        private void BeginRmbMove(MouseEventArgs e)
        {
            if (!onRmbMoving && !bgwRmbMoving.IsBusy)
            {
                onRmbMoving = true;
                rmbMoving = true;
                rmbMoveDownLocation = e.Location;
                rmbMoveDelta = new Point();
                bgwRmbMoving.RunWorkerAsync();
            }
        }
        private void UpdateRmbMoveDelta(MouseEventArgs e)
        {
            if (onRmbMoving)
            {
                rmbMoveDelta = rmbMoveDownLocation.Delta(e.Location);
            }
        }
        private void EndRmbMove()
        {
            if (onRmbMoving)
            {
                rmbMoving = false;
                bgwRmbMoving.CancelAsync();
                bgwRmbMoving.Dispose();
                onRmbMoving = false;
            }
        }
        private void bgwRmbMoving_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                if (rmbMoving)
                {
                    GlobalVar.Engine.ViewShift(rmbMoveDelta);
                    pnlMiniMap.BackgroundImage = GlobalVar.Engine.MiniMap;
                    Thread.Sleep(16);
                }
                else
                {
                    return;
                }
            }
        }
    }
}
