using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using RelertSharp.MapStructure;
using RelertSharp.IniSystem;
using RelertSharp.Common;
using RelertSharp.FileSystem;
using static RelertSharp.GUI.GuiUtils;
using static RelertSharp.Common.GlobalVar;

namespace RelertSharp.GUI.Controls
{
    public partial class PickPanel
    {
        private const string TNodeOvResource = "\nOvResource";
        private const string TNodeOvWall = "\nOvWall";
        private const string TNodeOvOther = "\nOvOther";
        private const string TNodeOvLoBrg = "\nOvLow";
        private const string TNodeOvHiBrg = "\nOvHi";
        private const string TNodeOvRail = "\nOvRail";
        private const string TNodeOvRock = "\nOvRock";
        private static string[] OverlayRootNode = { TNodeOvHiBrg, TNodeOvLoBrg, TNodeOvOther, TNodeOvRail, TNodeOvResource, TNodeOvWall, TNodeOvRock };


        private bool isResourceMode = false;
        private string overlayName = string.Empty;
        private byte overlayFrame = 0;


        private void InitializeOverlayPanel()
        {
            LoadOverlays();
        }
        private void LoadOverlays()
        {
            TreeNode resource = NewNode("Resources", TNodeOvResource);
            TreeNode wall = NewNode("Walls", TNodeOvWall);
            TreeNode other = NewNode("Others", TNodeOvOther);
            TreeNode low = NewNode("Low Bridge", TNodeOvLoBrg);
            TreeNode hi = NewNode("High Bridge", TNodeOvHiBrg);
            TreeNode rail = NewNode("Railway", TNodeOvRail);
            TreeNode rock = NewNode("Rocks", TNodeOvRock);
            foreach (INIPair p in GlobalRules["OverlayTypes"])
            {
                INIEntity item = GlobalRules[p.Value];
                if (item.ParseBool("Tiberium")) AddObjectToNode(resource, p.Value, false);
                else if (item.ParseBool("Wall")) AddObjectToNode(wall, p.Value, false);
                else if (item["Land"] == "Railroad") AddObjectToNode(rail, p.Value, false);
                else if (item["Land"] == "Road") AddObjectToNode(low, p.Value, false);
                else if (item.ParseBool("Overrides")) AddObjectToNode(hi, p.Value, false);
                else if (item.ParseBool("IsARock")) AddObjectToNode(rock, p.Value, false);
                else AddObjectToNode(other, p.Value, false);
            }
            LoadToObjectCollection(trvOverlay, wall, resource, low, hi, rock, rail, other);
        }
        private void ReloadOverlay()
        {
            if (drew)
            {
                byte index = GlobalRules.GetOverlayIndex(overlayName);
                InnerSource = new OverlayUnit(index, overlayFrame);
                brush.Reload(InnerSource, MapObjectType.Overlay);
            }
        }
        private void ReloadOverlayFrames()
        {
            updatingLvOverlayFrames = true;
            string pal = GlobalRules.GetOverlayPalette(overlayName);
            string file = GlobalRules.GetOverlayFileName(overlayName);
            imgOverlayFrame.Images.Clear();
            lvOverlayFrames.Items.Clear();
            if (GlobalDir.HasFile(file))
            {
                try
                {
                    PalFile p = GlobalDir.GetFile(pal, FileExtension.PAL);
                    ShpFile pic = new ShpFile(GlobalDir.GetRawByte(file), file);
                    pic.LoadColor(p);
                    Size maxSize = pic.GetMaxSize();
                    imgOverlayFrame.ImageSize = maxSize;

                    for (int i = 0; i < pic.Frames.Count / 2; i++)
                    {
                        ShpFrame frame = pic.Frames[i];
                        ListViewItem item = new ListViewItem(string.Format("{0}-Frame{1}", overlayName, i.ToString()), i.ToString());
                        imgOverlayFrame.Images.Add(i.ToString(), Utils.Misc.ResizeImage(frame.Image, maxSize));
                        lvOverlayFrames.Items.Add(item);
                    }
                }
                catch (RSException.InvalidFileException e)
                {
                    Warning("Frame error!\nShp file may corrupted, file name: {0}", e.FileName);
                }
            }
            updatingLvOverlayFrames = false;
        }


        private void trvOverlay_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode node = e.Node;
            if (!string.IsNullOrEmpty(node.Name) && !OverlayRootNode.Contains(node.Name))
            {
                bool changed = overlayName != node.Name;
                overlayName = node.Name;

                if (drew)
                {
                    ReloadOverlayFrames();
                    if (changed)
                    {
                        updatingLvOverlayFrames = true;
                        lvOverlayFrames.SelectedIndices.Clear();
                        updatingLvOverlayFrames = false;
                        if (lvOverlayFrames.Items.Count > 0) lvOverlayFrames.SelectedIndices.Add(0);
                    }
                    else ReloadOverlay();
                    BrushObjectSelected?.Invoke(this, new EventArgs());
                }
            }
        }
        private bool updatingLvOverlayFrames = false;
        private void lvOverlayFrames_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!updatingLvOverlayFrames)
            {
                if (lvOverlayFrames.SelectedIndices.Count != 0)
                {
                    overlayFrame = (byte)lvOverlayFrames.SelectedIndices[0];
                    ReloadOverlay();
                }
            }
        }
    }
}
