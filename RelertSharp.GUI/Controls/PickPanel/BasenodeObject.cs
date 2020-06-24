using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RelertSharp.MapStructure.Points;
using RelertSharp.MapStructure.Logic;
using RelertSharp.IniSystem;
using RelertSharp.Common;
using static RelertSharp.Common.GlobalVar;
using static RelertSharp.GUI.GuiUtils;

namespace RelertSharp.GUI.Controls
{
    public partial class PickPanel
    {
        private HouseItem currentNodeHouse;


        private void InitializeNodePanel()
        {
            LoadNodeHouse();
        }
        private void LoadNodeHouse()
        {
            updatingCbbNodeHouse = true;
            LoadToObjectCollection(cbbNodeHouse, CurrentMapDocument.Map.Houses);
            updatingCbbNodeHouse = false;
            if (CurrentMapDocument.Map.Houses.Count() > 0) cbbNodeHouse.SelectedIndex = 0;
        }
        private void RemoveBaseNode(BaseNode node)
        {
            if (node != null && currentNodeHouse != null)
            {
                currentNodeHouse.BaseNodes.Remove(node);
                lbxNodes.Items.Remove(node);
                if (drew)
                {
                    node.Dispose();
                    Engine.Refresh();
                }
            }
        }


        private bool updatingCbbNodeHouse = false;
        private void cbbNodeHouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!updatingCbbNodeHouse)
            {
                if (cbbNodeHouse.SelectedItem is HouseItem house)
                {
                    currentNodeHouse = house;
                    LoadToObjectCollection(lbxNodes, house.BaseNodes);
                }
            }
        }
        private void lbxNodes_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (drew)
            {
                if (lbxNodes.Items.Count != 0)
                {
                    if (lbxNodes.SelectedItem is BaseNode n) BaseNodeTracing?.Invoke(this, n);
                }
            }
        }
        private void tsmiRemoveSelNode_Click(object sender, EventArgs e)
        {
            if (lbxNodes.SelectedItem is BaseNode n) RemoveBaseNode(n);
        }
        private void tsmiRefreshNodes_Click(object sender, EventArgs e)
        {
            LoadToObjectCollection(lbxNodes, currentNodeHouse.BaseNodes);
        }
        private void tsmiRemoveAllNodes_Click(object sender, EventArgs e)
        {
            foreach (BaseNode n in currentNodeHouse.BaseNodes)
            {
                n.Dispose();
            }
            currentNodeHouse.BaseNodes.Clear();
            lbxNodes.Items.Clear();
            Engine.Refresh();
        }
        private void tsmiNodeToTop_Click(object sender, EventArgs e)
        {
            if (lbxNodes.SelectedItem is BaseNode n)
            {
                RepositionItemInCollection(lbxNodes, 0);
                Utils.Misc.RepositionItemInList(currentNodeHouse.BaseNodes, n, 0, lbxNodes.SelectedIndex);
            }
        }

        private void tsmiNodeUp_Click(object sender, EventArgs e)
        {
            if (lbxNodes.SelectedItem is BaseNode n)
            {
                int index = lbxNodes.SelectedIndex;
                if (index != 0)
                {
                    RepositionItemInCollection(lbxNodes, index - 1);
                    Utils.Misc.RepositionItemInList(currentNodeHouse.BaseNodes, n, index - 1, index);
                }
            }
        }

        private void tsmiNodeDown_Click(object sender, EventArgs e)
        {
            if (lbxNodes.SelectedItem is BaseNode n)
            {
                int index = lbxNodes.SelectedIndex;
                if (index < lbxNodes.Items.Count - 1)
                {
                    RepositionItemInCollection(lbxNodes, index + 1);
                    Utils.Misc.RepositionItemInList(currentNodeHouse.BaseNodes, n, index + 1, index);
                }
            }
        }

        private void tsmiNodeToLast_Click(object sender, EventArgs e)
        {
            if (lbxNodes.SelectedItem is BaseNode n)
            {
                RepositionItemInCollection(lbxNodes, lbxNodes.Items.Count - 1);
                Utils.Misc.RepositionItemInList(currentNodeHouse.BaseNodes, n, lbxNodes.Items.Count - 1, lbxNodes.SelectedIndex);
            }
        }
    }
}
