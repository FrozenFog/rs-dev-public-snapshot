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
                        if ((Current.CurrentMouseAction & MainWindowDataModel.MouseActionType.BoxSelecting) != 0) Current.ReleaseAll();
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
                        Current.CurrentMouseAction = MainWindowDataModel.MouseActionType.BoxSelecting;
                        break;
                    case Keys.B:
                        Current.CurrentMouseAction = MainWindowDataModel.MouseActionType.AttributeBrush;
                        break;
                    case Keys.V:
                        Current.CurrentMouseAction = MainWindowDataModel.MouseActionType.Moving;
                        break;
                    case Keys.Enter:
                        if (Current.CurrentMouseAction == MainWindowDataModel.MouseActionType.AttributeBrush)
                        {
                            ApplyAttributeToSelected();
                        }
                        break;
                }
            }
        }

        private Cursor prevCur = Cursors.Arrow;
        private MainWindowDataModel.MouseActionType prevType = MainWindowDataModel.MouseActionType.None;
        private void HandlingKeyDown(KeyEventArgs e)
        {
            if (drew)
            {
                switch (e.KeyCode)
                {
                    case Keys.Space:
                        if (panel1.Cursor != Cursors.SizeAll)
                        {
                            prevCur = panel1.Cursor;
                            prevType = Current.CurrentMouseAction;
                        }
                        spaceKeyMoving = true;
                        panel1.Cursor = Cursors.SizeAll;
                        Current.CurrentMouseAction = MainWindowDataModel.MouseActionType.Moving;
                        break;
                }
            }
        }

        private void HandlingKeyUp(KeyEventArgs e)
        {
            if (drew)
            {
                switch (e.KeyCode)
                {
                    case Keys.Space:
                        spaceKeyMoving = false;
                        panel1.Cursor = prevCur;
                        Current.CurrentMouseAction = prevType;
                        break;
                }
            }
        }
    }
}
