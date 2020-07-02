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
        #region Down
        private void LmbDown(MouseEventArgs e)
        {
            if (!panel1.Focused || RbPanelVisible())
            {
                panel1.Focus();
                HideRbPanel();
                panel1.Cursor = prevCur;
                GlobalVar.Engine.Refresh();
            }
            else
            {
                Vec3 cell = GlobalVar.Engine.ClientPointToCellPos(e.Location, out int subcell);
                switch (Current.CurrentMouseAction)
                {
                    case MainWindowDataModel.MouseActionType.BoxSelecting:
                        if (Current.SelectingBoxFlag != MainWindowDataModel.SelectingBoxMode.Precise) SceneSelectionBoxSet(e);
                        else PreciseSelecting(cell, subcell);
                        break;
                    case MainWindowDataModel.MouseActionType.Moving:
                        if (spaceKeyMoving)
                        {
                            BeginMove(e);
                        }
                        else
                        {
                            ObjectMovingBegin(cell, subcell);
                        }
                        break;
                    case MainWindowDataModel.MouseActionType.AttributeBrush:
                        ApplyAttributeToPrecise(e);
                        break;
                    case MainWindowDataModel.MouseActionType.AddingObject:
                        AddBrushObjectToMap();
                        if (pnlPick.CurrentType == PickPanelType.Smudges) pnlPick.ReloadRandomSmudge();
                        break;
                }
            }
        }
        private void MmbDown(MouseEventArgs e)
        {
            BeginMove(e);
        }
        private void RmbDown(MouseEventArgs e)
        {
            if (RbPanelVisible()) HideRbPanel();
            else
            {
                switch (Current.CurrentMouseAction)
                {
                    case MainWindowDataModel.MouseActionType.AttributeBrush:
                        prevCur = panel1.Cursor;
                        panel1.Cursor = Cursors.Arrow;
                        rbPanelAttribute.Location = e.Location;
                        rbPanelAttribute.Visible = true;
                        GlobalVar.Engine.Refresh();
                        break;
                    case MainWindowDataModel.MouseActionType.Moving:
                        BeginRmbMove(e);
                        break;
                    case MainWindowDataModel.MouseActionType.AddingObject:
                        prevCur = panel1.Cursor;
                        panel1.Cursor = Cursors.Arrow;
                        rbPanelBrush.Location = e.Location;
                        rbPanelBrush.Visible = true;
                        GlobalVar.Engine.Refresh();
                        break;
                }
            }

        }
        #endregion


        #region Moving
        private void MouseMoving(MouseEventArgs e)
        {
            if (initialized && drew && panel1.Focused)
            {
                Vec3 cell = GlobalVar.Engine.ClientPointToCellPos(e.Location, out int subcell);
                MainPanelMoving(e);
                bool markTile = true;
                switch (Current.CurrentMouseAction)
                {
                    case MainWindowDataModel.MouseActionType.Moving:
                        UpdateRmbMoveDelta(e);
                        OnObjectMoving(cell, subcell);
                        break;
                    case MainWindowDataModel.MouseActionType.BoxSelecting:
                        DrawSelectingBoxOnScene(e);
                        break;
                    case MainWindowDataModel.MouseActionType.AddingObject:
                        if (pnlPick.CurrentType == PickPanelType.Terrains) pnlPick.ReloadRandomTerrain();
                        MoveBrushObjectTo(cell, subcell);
                        break;
                    case MainWindowDataModel.MouseActionType.TileBrush:
                        MoveTileBrushObjectTo(cell);
                        markTile = false;
                        break;
                }
                if (!onRmbMoving && !bgwRmbMoving.IsBusy) GeneralMouseMovingUpdate(cell, subcell, markTile);
            }
        }
        private void GeneralMouseMovingUpdate(Vec3 pos ,int subcell, bool markTile)
        {
            if (pos != Vec3.Zero)
            {
                lblx.Text = string.Format("X : {0}", pos.X);
                lbly.Text = string.Format("Y : {0}", pos.Y);
                lblz.Text = string.Format("Z : {0}", pos.Z);
                lblSubcell.Text = string.Format("Subcell : {0}", subcell);
                if (markTile && GlobalVar.Engine.SelectTile(pos)) GlobalVar.Engine.Refresh();
            }
        }
        #endregion


        #region Up
        private void LmbUp(MouseEventArgs e)
        {
            Vec3 cell = GlobalVar.Engine.ClientPointToCellPos(e.Location, out int subcell);
            switch (Current.CurrentMouseAction)
            {
                case MainWindowDataModel.MouseActionType.BoxSelecting:
                    if (Current.SelectingBoxFlag != MainWindowDataModel.SelectingBoxMode.Precise) SelectSceneItemsInsideBox(cell);
                    break;
                case MainWindowDataModel.MouseActionType.Moving:
                    MmbUp(e);
                    ObjectMovingEnd(e);
                    break;
            }
            spaceKeyMoving = false;
        }
        private void MmbUp(MouseEventArgs e)
        {
            panel1_MouseLeave(null, null);
        }
        private void RmbUp(MouseEventArgs e)
        {
            switch (Current.CurrentMouseAction)
            {
                case MainWindowDataModel.MouseActionType.Moving:
                    EndRmbMove();
                    break;
            }
        }
        #endregion


        #region Click
        private void LmbClick(MouseEventArgs e)
        {
            if (rbPanelAttribute.Visible)
            {
                rbPanelAttribute.Visible = false;
                panel1.Cursor = prevCur;
                GlobalVar.Engine.Refresh();
            }
        }
        private void RmbClick(MouseEventArgs e)
        {

        }
        private void LmbDoubleClicked(MouseEventArgs e)
        {
            if (drew)
            {
                switch (Current.CurrentMouseAction)
                {
                    case MainWindowDataModel.MouseActionType.ArrowInspect:
                    case MainWindowDataModel.MouseActionType.None:
                        InspectItemAt(e);
                        break;
                }
            }
        }
        #endregion


        #region Enter
        private void PanelMouseEnter()
        {
            if (drew)
            {
                if ((Current.CurrentMouseAction | MainWindowDataModel.MouseActionType.DrawingMode) != 0 && !panel1.Focused)
                {
                    panel1.Focus();
                }
            }
        }
        #endregion
    }
}
