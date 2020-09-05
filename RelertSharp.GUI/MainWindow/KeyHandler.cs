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
                        ToolBoxClick(toolBtnArrow);
                        break;
                    case Keys.Delete:
                        Current.RemoveAll();
                        pnlMiniMap.BackgroundImage = GlobalVar.Engine.MiniMap;
                        pnlPick.RefreshContainerMember();
                        break;
                    case Keys.M:
                        ToolBoxClick(toolBtnSelecting);
                        break;
                    case Keys.B:
                        ToolBoxClick(toolBtnBrush);
                        break;
                    case Keys.V:
                        ToolBoxClick(toolBtnMoving);
                        break;
                    case Keys.I:
                        ToolBoxClick(toolBtnAttributeBrush);
                        break;
                    case Keys.A:
                        ToolBoxClick(toolBtnArrow);
                        break;
                    case Keys.F:
                        ToolBtnCheckClick(toolBtnFramework, new EventArgs());
                        break;
                    case Keys.N:
                        ToolBoxClick(toolBtnTileBrush);
                        break;
                    case Keys.S:
                        ToolBoxClick(toolBtnTileSele);
                        break;
                    case Keys.W:
                        ToolBoxClick(toolBtnTileWand);
                        break;
                    case Keys.D:
                        if (e.Control)
                        {
                            if ((Current.CurrentMouseAction & MainWindowDataModel.MouseActionType.TileEditingMode) != 0) Current.DeSelectTileAll();
                            else Current.ReleaseAll();
                        }
                        ToolBtnCheckClick(toolBtnFlatGround, new EventArgs());
                        break;
                    case Keys.Z:
                        if (e.Control)
                        {
                            Current.UndoRedo.Undo();
                            GlobalVar.Engine.Refresh();
                            RedrawMinimapAll();
                        }
                        break;
                    case Keys.Y:
                        if (e.Control)
                        {
                            Current.UndoRedo.Redo();
                            GlobalVar.Engine.Refresh();
                            RedrawMinimapAll();
                        }
                        break;
                    case Keys.OemQuestion:
                        if (e.Control)
                        {
                            if (txbCommand.Visible) txbCommand.Visible = false;
                            else
                            {
                                txbCommand.Visible = true;
                                txbCommand.Focus();
                                txbCommand.Text = @"/";
                                txbCommand.SelectionLength = 0;
                                txbCommand.SelectionStart = 1;
                            }
                            GlobalVar.Engine.Refresh();
                        }
                        break;
                    case Keys.Enter:
                        if (Current.CurrentMouseAction == MainWindowDataModel.MouseActionType.AttributeBrush)
                        {
                            ApplyAttributeToSelected();
                            pnlMiniMap.BackgroundImage = GlobalVar.Engine.MiniMap;
                        }
                        break;
                    case Keys.PageUp:
                        Current.RiseTileAll();
                        break;
                    case Keys.PageDown:
                        Current.SinkTileAll();
                        break;
                    case Keys.Up:
                        if (Current.CurrentMouseAction == MainWindowDataModel.MouseActionType.TileBrush) pnlTile.Result.ShiftOffsetY(-1);
                        break;
                    case Keys.Down:
                        if (Current.CurrentMouseAction == MainWindowDataModel.MouseActionType.TileBrush) pnlTile.Result.ShiftOffsetY(1);
                        break;
                    case Keys.Left:
                        if (Current.CurrentMouseAction == MainWindowDataModel.MouseActionType.TileBrush) pnlTile.Result.ShiftOffsetX(-1);
                        break;
                    case Keys.Right:
                        if (Current.CurrentMouseAction == MainWindowDataModel.MouseActionType.TileBrush) pnlTile.Result.ShiftOffsetX(1);
                        break;
                    case Keys.Home:
                        if (Current.CurrentMouseAction == MainWindowDataModel.MouseActionType.TileBrush) pnlTile.Result.ResetOffset();
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
                    case Keys.ShiftKey:
                        if (Current.CurrentMouseAction == MainWindowDataModel.MouseActionType.AddingObject)
                        {
                            pnlPick.ShiftHide();
                        }
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
                    case Keys.ShiftKey:
                        if (Current.CurrentMouseAction == MainWindowDataModel.MouseActionType.AddingObject)
                        {
                            pnlPick.ShiftUnHide();
                        }
                        break;
                }
            }
        }
    }
}
