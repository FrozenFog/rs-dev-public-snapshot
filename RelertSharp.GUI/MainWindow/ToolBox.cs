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
        private void ToolBoxClick(ToolStripButton btn)
        {
            foreach (ToolStripItem item in toolsMain.Items)
            {
                if (item.GetType() == typeof(ToolStripButton)) (item as ToolStripButton).Checked = false;
            }
            string btnName = btn.Tag.ToString();
            switch (btnName)
            {
                case "moving":
                    Current.CurrentMouseAction = MainWindowDataModel.MouseActionType.Moving;
                    panel1.Cursor = new Cursor(Properties.Resources.cursorSolid.Handle);
                    break;
                case "selecting":
                    Current.CurrentMouseAction = MainWindowDataModel.MouseActionType.BoxSelecting;
                    if (Current.SelectingBoxFlag == MainWindowDataModel.SelectingBoxMode.ClientRectangle) panel1.Cursor = new Cursor(Properties.Resources.curRect.Handle);
                    else if (Current.SelectingBoxFlag == MainWindowDataModel.SelectingBoxMode.Precise) panel1.Cursor = new Cursor(Properties.Resources.cursorSolid.Handle);
                    else panel1.Cursor = new Cursor(Properties.Resources.curIso.Handle);
                    break;
                case "brush":
                    Current.CurrentMouseAction = MainWindowDataModel.MouseActionType.AddingObject;
                    panel1.Cursor = new Cursor(Properties.Resources.curBrush.Handle);
                    break;
                case "attribute":
                    Current.CurrentMouseAction = MainWindowDataModel.MouseActionType.AttributeBrush;
                    panel1.Cursor = new Cursor(Properties.Resources.curAttrib.Handle);
                    break;
            }
            btn.Checked = true;
        }

        private void ToolBoxRightClick(ToolStripButton btn, Point p)
        {
            string btnName = btn.Tag.ToString();
            switch (btnName)
            {
                case "selecting":
                    cmsToolSelect.Show(p);
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
