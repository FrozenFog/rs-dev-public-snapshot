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
            switch (Current.CurrentMouseAction)
            {
                case MainWindowDataModel.MouseActionType.BoxSelecting:
                    SceneSelectionBoxSet(e);
                    break;
            }
        }
        private void MmbDown(MouseEventArgs e)
        {
            BeginMove(e);
        }
        private void RmbDown(MouseEventArgs e)
        {
            switch (Current.CurrentMouseAction)
            {
                case MainWindowDataModel.MouseActionType.AttributeBrush:
                    rbPanelAttribute.Location = e.Location;
                    rbPanelAttribute.Visible = true;
                    break;
            }
        }
        #endregion


        private void MouseMoving(MouseEventArgs e)
        {
            if (initialized)
            {
                GeneralMouseMovingUpdate(e);
                DrawSelectingBoxOnScene(e);
                MainPanelMoving(e);
            }
        }
        private void GeneralMouseMovingUpdate(MouseEventArgs e)
        {
            Vec3 pos = GlobalVar.Engine.ClientPointToCellPos(e.Location);
            lblMouseX.Text = string.Format("MouseX : {0}", e.Location.X);
            lblMouseY.Text = string.Format("MouseY : {0}", e.Location.Y);
            if (pos != Vec3.Zero)
            {
                lblx.Text = string.Format("X : {0}", pos.X);
                lbly.Text = string.Format("Y : {0}", pos.Y);
                lblz.Text = string.Format("Z : {0}", pos.Z);
                if (GlobalVar.Engine.SelectTile(pos)) GlobalVar.Engine.Refresh();
            }
        }


        #region Up
        private void LmbUp(MouseEventArgs e)
        {
            switch (Current.CurrentMouseAction)
            {
                case MainWindowDataModel.MouseActionType.BoxSelecting:
                    SelectSceneItemsInsideBox(e);
                    break;
            }
        }
        private void MmbUp(MouseEventArgs e)
        {
            panel1_MouseLeave(null, null);
        }
        private void RmbUp(MouseEventArgs e)
        {

        }
        #endregion


        #region Click
        private void LmbClick(MouseEventArgs e)
        {
            switch (Current.CurrentMouseAction)
            {
                case MainWindowDataModel.MouseActionType.PreciseSelect:
                    PreciseSelecting(e);
                    break;
                case MainWindowDataModel.MouseActionType.AttributeBrush:
                    ApplyAttributeToPrecise(e);
                    break;
            }
        }
        private void RmbClick(MouseEventArgs e)
        {

        }
        #endregion
    }
}
