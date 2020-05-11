using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private void HandlingKey(PreviewKeyDownEventArgs e)
        {
            if (drew)
            {
                switch (e.KeyCode)
                {
                    case Keys.Escape:
                        if ((Current.CurrentMouseAction & MainWindowDataModel.MouseActionType.Selecting) != 0) Current.ReleaseAll();
                        else if (Current.CurrentMouseAction == MainWindowDataModel.MouseActionType.AttributeBrush)
                        {
                            rbPanelAttribute.Visible = false;
                            GlobalVar.Engine.Refresh();
                        }
                        break;
                    case Keys.Delete:
                        Current.RemoveAll();
                        break;
                    case Keys.M:
                        if (e.Shift) Current.CurrentMouseAction = MainWindowDataModel.MouseActionType.PreciseSelect;
                        else Current.CurrentMouseAction = MainWindowDataModel.MouseActionType.BoxSelecting;
                        break;
                    case Keys.B:
                        Current.CurrentMouseAction = MainWindowDataModel.MouseActionType.AttributeBrush;
                        break;
                    case Keys.V:
                        Current.CurrentMouseAction = MainWindowDataModel.MouseActionType.None;
                        break;
                    case Keys.W:
                        break;
                    case Keys.A:
                        break;
                    case Keys.S:
                        break;
                    case Keys.D:
                        break;
                }
            }
        }
    }
}
