﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RelertSharp.Common;

namespace RelertSharp.GUI
{
    public partial class MainWindowTest
    {
        public void MoveBrushObjectTo(MouseEventArgs e)
        {
            if (drew)
            {
                I3dLocateable cell;
                Vec3 pos = GlobalVar.Engine.ClientPointToCellPos(e.Location, out int subcell);
                if (pos == Vec3.Zero)
                {
                    cell = GlobalVar.Engine.GetPreviousLegalTile();
                    subcell = 1;
                }
                else cell = pos.To3dLocateable();
                pnlPick.MoveBurhObjectTo(cell, subcell);
            }
        }
        public void AddBrushObjectToMap()
        {
            if (drew && pnlPick.Result.BrushObject != null)
            {
                if (!rbPanelBrush.IsSimulating || pnlPick.CanBuild)
                {
                    if (rbPanelBrush.AddBaseNode)
                    {
                        IMapObject node = pnlPick.ReleaseAdditionalBaseNode(out string ownerhouse);
                        if (node != null) Map.AddBaseNode(node, ownerhouse);
                    }
                    if (!rbPanelBrush.IgnoreBuilding)
                    {
                        IMapObject obj = pnlPick.ReleaseBrushObject(rbPanelBrush.IsSimulating, out bool canBuild);
                        if (rbPanelBrush.IsSimulating && !canBuild)
                        {
                            obj.Dispose();
                            return;
                        }
                        Map.AddObjectFromBrush(obj);
                    }
                    GlobalVar.Engine.Refresh();
                    GlobalVar.Engine.RedrawMinimapAll();
                    pnlMiniMap.BackgroundImage = GlobalVar.Engine.MiniMap;
                }
            }
        }
    }
}
