using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RelertSharp.IniSystem;
using RelertSharp.Common;
using RelertSharp.MapStructure.Points;
using static RelertSharp.Common.GlobalVar;
using static RelertSharp.GUI.GuiUtils;

namespace RelertSharp.GUI.Controls
{
    public partial class PickPanel
    {
        private const string TNodeTree = "\nTree";
        private const string TNodeTerrOther = "\nTerrOther";
        private const string TNodeTerrPole = "\nTerrPole";
        private const string TNodeTerrTib = "\nTerrTibTree";
        private static string[] TerrainRootNode = { TNodeTree, TNodeTerrOther, TNodeTerrPole, TNodeTerrTib };


        private bool isRndTerrain = false;


        public void ReloadRandomTerrain()
        {
            if (isRndTerrain)
            {
                brush.BrushObject?.Dispose();
                if (lbxRndTerrain.Items.Count > 0)
                {
                    Random r = new Random();
                    string regname = (string)lbxRndTerrain.Items[r.Next(0, lbxRndTerrain.Items.Count - 1)];
                    mouseNodeName = regname;
                    ReloadTerrain();
                }
            }
        }


        private void InitializeTerrainPanel()
        {
            LoadTerrains();
        }
        private void LoadTerrains()
        {
            TreeNode tree = NewNode("Trees", TNodeTree);
            TreeNode other = NewNode("Others", TNodeTerrOther);
            TreeNode pole = NewNode("Poles", TNodeTerrPole);
            TreeNode resource = NewNode("Resource Fountain", TNodeTerrTib);
            foreach (INIPair p in GlobalRules["TerrainTypes"])
            {
                INIEntity item = GlobalRules[p.Value];
                if (p.Value.ToLower().StartsWith("tree")) AddObjectToNode(tree, p.Value, false);
                else if (item.ParseBool("SpawnsTiberium") || item.ParseBool("IsVeinhole"))
                {
                    AddObjectToNode(resource, p.Value, false);
                }
                else if (item["Armor"] != "special" && !item.ParseBool("Immune"))
                {
                    AddObjectToNode(pole, p.Value, false);
                }
                else AddObjectToNode(other, p.Value, false);
            }
            trvTerrain.LoadAs(tree, pole, resource, other);
        }
        private void ReloadTerrain()
        {
            if (drew)
            {
                InnerSource = new TerrainItem(mouseNodeName);
                brush.Reload(mouseNodeName, MapObjectType.Terrain);
            }
        }


        private void ckbRndTerrainEnable_CheckedChanged(object sender, EventArgs e)
        {
            lbxRndTerrain.Enabled = ckbRndTerrainEnable.Checked;
            isRndTerrain = ckbRndTerrainEnable.Checked;
            if (!isRndTerrain && trvTerrain.SelectedNode != null)
            {
                string nodeName = trvTerrain.SelectedNode.Name;
                if (!string.IsNullOrEmpty(nodeName) && !nodeName.StartsWith("\n"))
                {
                    mouseNodeName = nodeName;
                    ReloadTerrain();
                }
            }
        }
        private string mouseNodeName = string.Empty;
        private void trvTerrain_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode node = e.Node;
            if (!string.IsNullOrEmpty(node.Name) && !TerrainRootNode.Contains(node.Name))
            {
                mouseNodeName = node.Name;
                if (e.Button == MouseButtons.Left)
                {
                    if (drew && !isRndTerrain)
                    {
                        ReloadTerrain();
                        BrushObjectSelected?.Invoke(this, new EventArgs());
                    }
                }
                else if (e.Button == MouseButtons.Right)
                {
                    trvTerrain.SelectedNode = node;
                    cmsTerrainAddRnd.Show(sender as Control, e.Location);
                }

            }
        }
        private void tsmiTerrAddRnd_Click(object sender, EventArgs e)
        {
            if (!lbxRndTerrain.Items.Contains(mouseNodeName))
            {
                lbxRndTerrain.Items.Add(mouseNodeName);
            }
        }
        private void tsmiTerrRemoveSelected_Click(object sender, EventArgs e)
        {
            if (lbxRndTerrain.SelectedItem != null)
            {
                bool b = false;
                lbxRndTerrain.RemoveAt(ref b);
            }
        }

        private void tsmiTerrRemoveAllRnd_Click(object sender, EventArgs e)
        {
            lbxRndTerrain.Items.Clear();
        }
    }
}
