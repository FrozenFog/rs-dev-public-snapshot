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
using RelertSharp.SubWindows.LogicEditor;
using RelertSharp.SubWindows.INIEditor;

namespace RelertSharp.GUI
{
    public partial class MainWindowTest
    {
        private const string BtnNameBrush = "brush";
        private const string BtnNameMove = "moving";
        private const string BtnNameSelect = "selecting";
        private const string BtnNameAttribute = "attribute";
        private const string BtnNameArrow = "arrow";
        private const string BtnNameTileSele = "tilesele";
        private const string BtnNameTileBrush = "tilebrush";
        private const string CkbNameFramework = "framework";
        private const string CkbNameFlat = "flatground";
        private readonly string[] ToolsButton = { BtnNameArrow, BtnNameBrush, BtnNameMove, BtnNameSelect, BtnNameAttribute, BtnNameTileBrush, BtnNameTileSele };

        private void ToolBoxClick(ToolStripButton btn)
        {
            foreach (ToolStripItem item in toolsMain.Items)
            {
                if (item.GetType() == typeof(ToolStripButton) && ToolsButton.Contains(item.Tag.ToString())) (item as ToolStripButton).Checked = false;
            }
            string btnName = btn.Tag.ToString();
            if (drew)
            {
                if (btnName != BtnNameBrush)
                {
                    pnlPick.Result.BrushObject?.Dispose();
                    GlobalVar.Engine.UnmarkBuildingShape();
                }
                if (btnName != BtnNameTileBrush)
                {
                    pnlTile.Result?.Dispose();
                }
                GlobalVar.Engine.Refresh();
            }
            switch (btnName)
            {
                case BtnNameMove:
                    Current.CurrentMouseAction = MainWindowDataModel.MouseActionType.Moving;
                    panel1.Cursor = new Cursor(Properties.Resources.arrMoving.Handle);
                    break;
                case BtnNameSelect:
                    Current.CurrentMouseAction = MainWindowDataModel.MouseActionType.BoxSelecting;
                    if (Current.SelectingBoxFlag == MainWindowDataModel.SelectingBoxMode.ClientRectangle) panel1.Cursor = new Cursor(Properties.Resources.curRect.Handle);
                    else if (Current.SelectingBoxFlag == MainWindowDataModel.SelectingBoxMode.Precise) panel1.Cursor = new Cursor(Properties.Resources.cursorSolid.Handle);
                    else panel1.Cursor = new Cursor(Properties.Resources.curIso.Handle);
                    break;
                case BtnNameBrush:
                    Current.CurrentMouseAction = MainWindowDataModel.MouseActionType.AddingObject;
                    pnlPick.Result?.RedrawBrushObject();
                    panel1.Cursor = new Cursor(Properties.Resources.curBrush.Handle);
                    pnlPick.Visible = true;
                    pnlTile.Visible = false;
                    break;
                case BtnNameAttribute:
                    Current.CurrentMouseAction = MainWindowDataModel.MouseActionType.AttributeBrush;
                    panel1.Cursor = new Cursor(Properties.Resources.curAttrib.Handle);
                    break;
                case BtnNameArrow:
                    Current.CurrentMouseAction = MainWindowDataModel.MouseActionType.ArrowInspect;
                    panel1.Cursor = new Cursor(Properties.Resources.cursorSolid.Handle);
                    break;
                case BtnNameTileBrush:
                    Current.CurrentMouseAction = MainWindowDataModel.MouseActionType.TileBrush;
                    GlobalVar.Engine.UnmarkAllTile();
                    GlobalVar.Engine.Refresh();
                    break;
                case BtnNameTileSele:
                    Current.CurrentMouseAction = MainWindowDataModel.MouseActionType.TileSelecting;
                    GlobalVar.Engine.UnmarkAllTile();
                    GlobalVar.Engine.Refresh();
                    break;
            }
            btn.Checked = true;
        }

        private void ToolBoxRightClick(ToolStripButton btn, Point p)
        {
            string btnName = btn.Tag.ToString();
            switch (btnName)
            {
                case BtnNameSelect:
                    cmsToolSelect.Show(p);
                    break;
            }
        }
        private void ToolBtnCheckClick(object sender, EventArgs e)
        {
            ToolStripButton btn = sender as ToolStripButton;
            string btnName = btn.Tag.ToString();
            switch (btnName)
            {
                case CkbNameFramework:
                    if (drew)
                    {
                        SwitchToFramework(btn.Checked);
                        pnlTile.SetFramework(btn.Checked);
                    }
                    else btn.Checked = false;
                    break;
                case CkbNameFlat:
                    if (drew)
                    {
                        SwitchToFlatGround(btn.Checked);
                        pnlTile.SetFlat(btn.Checked);
                    }
                    break;
            }
        }

        private void tsmiRectSelect_Click(object sender, EventArgs e)
        {
            toolBtnSelecting.Image = Properties.Resources.btnRectSelecting;
            Current.SelectingBoxFlag = MainWindowDataModel.SelectingBoxMode.ClientRectangle;
            ToolBoxClick(toolBtnSelecting);
        }

        private void tsmiIsoSelect_Click(object sender, EventArgs e)
        {
            toolBtnSelecting.Image = Properties.Resources.btnIsoSelecting;
            Current.SelectingBoxFlag = MainWindowDataModel.SelectingBoxMode.IsometricRectangle;
            ToolBoxClick(toolBtnSelecting);
        }

        private void tsmiPreciseSelect_Click(object sender, EventArgs e)
        {
            toolBtnSelecting.Image = Properties.Resources.btnPrecSelecting;
            Current.SelectingBoxFlag = MainWindowDataModel.SelectingBoxMode.Precise;
            ToolBoxClick(toolBtnSelecting);
        }
    }
}
