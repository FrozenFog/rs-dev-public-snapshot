﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.Common;
using RelertSharp.MapStructure.Logic;
using RelertSharp.MapStructure.Points;
using RelertSharp.MapStructure.Objects;

namespace RelertSharp.GUI
{
    public partial class MainWindowTest
    {
        private void InitializeHandler()
        {
            logicEditor.JumpToWaypoint += JumpToWaypoint;

            pnlPick.BrushObjectSelected += FocusOnMainPanel;
            pnlPick.BaseNodeTracing += JumpToWaypoint;
            pnlPick.TraceCelltag += PnlPick_TraceCelltag;
            pnlPick.SelectCelltagCollection += PnlPick_SelectCelltagCollection;
            pnlPick.ReleaseCelltags += PnlPick_ReleaseCelltags;

            pnlTile.NewTileSelected += PnlTile_NewTileSelected;

            verifyForm.TraceLogic += VerifyForm_TraceLogic;
            verifyForm.TraceMapPosition += JumpToWaypoint;

            rbPanelLightRoom.LightItemChanged += LightChanged;
            rbPanelLightRoom.TraceLight += JumpToWaypoint;
        }

        private void VerifyForm_TraceLogic(object sender, LogicType logicType, string id)
        {
            logicEditor.Show();
            logicEditor.TraceLogicItem(logicType, id);
        }

        private void JumpToWaypoint(object logic, I2dLocateable cell)
        {
            if (drew && cell != null)
            {
                int height = Map.GetHeightFromTile(cell);
                GlobalVar.Engine.MoveTo(cell, height);
                pnlMiniMap.BackgroundImage = GlobalVar.Engine.MiniMap;
                GlobalVar.Engine.Refresh();
            }
        }
        private void PnlPick_ReleaseCelltags(object sender, EventArgs e)
        {
            if (drew)
            {
                foreach (CellTagItem cell in Current.Celltags) cell.UnSelect();
                Current.Celltags.Clear();
                GlobalVar.Engine.Refresh();
            }
        }

        private void PnlPick_SelectCelltagCollection(object sender, IEnumerable<CellTagItem> cells)
        {
            if (drew)
            {
                foreach (CellTagItem cell in cells)
                {
                    cell.Select();
                    if (!Current.Celltags.Contains(cell)) Current.Celltags.Add(cell);
                }
                GlobalVar.Engine.Refresh();
            }
        }

        private void PnlPick_TraceCelltag(object sender, TagItem tag)
        {
            logicEditor.Show();
            logicEditor.TraceTag(tag);
        }

        private void FocusOnMainPanel(object sender, EventArgs e)
        {
            if (drew)
            {
                requireFocus = true;
            }
        }

        private void LightChanged(object sender, EventArgs e)
        {
            if (drew)
            {
                RefreshLightningSilent();
            }
        }
    }
}
