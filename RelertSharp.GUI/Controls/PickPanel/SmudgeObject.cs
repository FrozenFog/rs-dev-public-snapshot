using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RelertSharp.MapStructure.Points;
using RelertSharp.Common;
using RelertSharp.IniSystem;
using static RelertSharp.GUI.GuiUtils;
using static RelertSharp.Common.GlobalVar;

namespace RelertSharp.GUI.Controls
{
    public partial class PickPanel
    {
        private const string TNodeSmgSingle = "\nSmgSingle";
        private const string TNodeSmgMulti = "\nSmgMulti";
        private static string[] SmudgeRootNode = { TNodeSmgMulti, TNodeSmgSingle };


        private string smgName = string.Empty;
        private bool isRndSmg = false;


        public void ReloadRandomSmudge()
        {
            if (isRndSmg)
            {
                brush.BrushObject?.Dispose();
                if (lbxRndSmg.Items.Count > 0)
                {
                    Random r = new Random();
                    string regname = (string)lbxRndSmg.Items[r.Next(0, lbxRndSmg.Items.Count - 1)];
                    smgName = regname;
                    ReloadSmudge();
                }
            }
        }


        private void InitializeSmudgePanel()
        {
            LoadSmudges();
        }
        private void LoadSmudges()
        {
            TreeNode single = NewNode("Single-cell", TNodeSmgSingle);
            TreeNode multi = NewNode("Multi-cell", TNodeSmgMulti);
            foreach (INIPair p in GlobalRules["SmudgeTypes"])
            {
                INIEntity item = GlobalRules[p.Value];
                if (item.Count() == 0) continue;
                if (item.ParseInt("Width", 1) > 1 || item.ParseInt("Height", 1) > 1) AddObjectToNode(multi, p.Value, false);
                else AddObjectToNode(single, p.Value, false);
            }
            LoadToObjectCollection(trvSmudge, single, multi);
        }
        private void ReloadSmudge()
        {
            if (drew)
            {
                InnerSource = new SmudgeItem(smgName);
                brush.Reload(InnerSource, MapObjectType.Smudge);
            }
        }


        private void trvSmudge_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode node = e.Node;
            if (!string.IsNullOrEmpty(node.Name) && !SmudgeRootNode.Contains(node.Name))
            {
                smgName = node.Name;
                if (e.Button == MouseButtons.Left)
                {
                    if (drew && !isRndSmg)
                    {
                        ReloadSmudge();
                        BrushObjectSelected?.Invoke(this, new EventArgs());
                    }
                }
                else if (e.Button == MouseButtons.Right)
                {
                    trvSmudge.SelectedNode = node;
                    cmsSmudgeAddRnd.Show(sender as Control, e.Location);
                }
            }
        }
        private void ckbRndSmg_CheckedChanged(object sender, EventArgs e)
        {
            lbxRndSmg.Enabled = ckbRndSmg.Checked;
            isRndSmg = ckbRndSmg.Checked;
            if (!isRndSmg && trvSmudge.SelectedNode != null)
            {
                string nodename = trvSmudge.SelectedNode.Name;
                if (!string.IsNullOrEmpty(nodename) && !nodename.StartsWith("\n"))
                {
                    smgName = nodename;
                    ReloadSmudge();
                }
            }
        }
        private void tsmiSmgAddRnd_Click(object sender, EventArgs e)
        {
            if (!lbxRndSmg.Items.Contains(smgName))
            {
                lbxRndSmg.Items.Add(smgName);
            }
        }
        private void tsmiSmgRemoveSel_Click(object sender, EventArgs e)
        {
            if (lbxRndSmg.SelectedItem != null)
            {
                bool b = false;
                RemoveAt(lbxRndSmg, ref b);
            }
        }
        private void tsmiSmgRemoveAll_Click(object sender, EventArgs e)
        {
            lbxRndSmg.Items.Clear();
        }
    }
}
