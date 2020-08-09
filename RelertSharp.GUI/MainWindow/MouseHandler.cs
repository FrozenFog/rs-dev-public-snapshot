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
            if (/*!panel1.Focused ||*/ RbPanelVisible())
            {
                //panel1.Focus();
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
                    case MainWindowDataModel.MouseActionType.TileBrush:
                        AddTileToPos(cell);
                        break;
                    case MainWindowDataModel.MouseActionType.TileSelecting:
                        BeginTileSelecting();
                        SelectTileAt(cell);
                        break;
                    case MainWindowDataModel.MouseActionType.TileWand:
                        WandSelectAt(cell);
                        break;
                    case MainWindowDataModel.MouseActionType.TileBucket:
                        FillAt(cell);
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
                Vec3 cell = GlobalVar.Engine.ClientPointToCellPos(e.Location, out int subcell);
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
                    case MainWindowDataModel.MouseActionType.TileSelecting:
                        BeginTileDeSelecting();
                        DeSelectTileAt(cell);
                        break;
                    case MainWindowDataModel.MouseActionType.TileWand:
                        prevCur = panel1.Cursor;
                        panel1.Cursor = Cursors.Arrow;
                        rbPanelWand.Location = e.Location;
                        rbPanelWand.Visible = true;
                        GlobalVar.Engine.Refresh();
                        break;
                    case MainWindowDataModel.MouseActionType.TileBucket:
                        prevCur = panel1.Cursor;
                        panel1.Cursor = Cursors.Arrow;
                        rbPanelBucket.Location = e.Location;
                        rbPanelBucket.Visible = true;
                        GlobalVar.Engine.Refresh();
                        break;
                }
            }

        }
        #endregion


        #region Moving
        private void MouseMoving(MouseEventArgs e)
        {
            if (initialized && drew && !RbPanelVisible())
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
                    case MainWindowDataModel.MouseActionType.TileSelecting:
                        if (isSelectingTile || isDeSelectingTile) markTile = false;
                        if (isSelectingTile) SelectTileAt(cell);
                        if (isDeSelectingTile) DeSelectTileAt(cell);
                        break;
                }
                if (!onRmbMoving && !bgwRmbMoving.IsBusy) GeneralMouseMovingUpdate(cell, subcell, markTile);
            }
        }
        private void GeneralMouseMovingUpdate(Vec3 pos ,int subcell, bool markTile)
        {
            if (pos != Vec3.Zero)
            {
                if (Map.TilesData[pos.To2dLocateable()] is Tile t)
                {
                    lblx.Text = string.Format("X : {0}", t.X);
                    lbly.Text = string.Format("Y : {0}", t.Y);
                    lblz.Text = string.Format("Z : {0}", t.RealHeight);
                    lblSubcell.Text = string.Format("Subcell : {0}", subcell);
                }
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
                case MainWindowDataModel.MouseActionType.TileSelecting:
                    EndTileSelecting();
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
                case MainWindowDataModel.MouseActionType.TileSelecting:
                    EndTileDeSelecting();
                    break;
            }
        }
        #endregion


        #region Click
        private void LmbClick(MouseEventArgs e)
        {
            //if (rbPanelAttribute.Visible)
            //{
            //    rbPanelAttribute.Visible = false;
            //    panel1.Cursor = prevCur;
            //    GlobalVar.Engine.Refresh();
            //}
            //if (!panel1.Focused || RbPanelVisible())
            //{
            //    if (drew)
            //    {
            //        panel1.Focus();
            //        HideRbPanel();
            //        panel1.Cursor = prevCur;
            //        GlobalVar.Engine.Refresh();
            //    }
            //}
            //else
            //{
            //    if (drew)
            //    {
            //        Vec3 cell = GlobalVar.Engine.ClientPointToCellPos(e.Location, out int subcell);
            //    }
            //}
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
            if (drew && requireFocus)
            {
                panel1.Focus();
                requireFocus = false;
            }
        }
        #endregion
    }
}
