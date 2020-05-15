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
        private bool minimapMoving = false;
        private Point previousLocation;

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
            }
        }
    }
}
