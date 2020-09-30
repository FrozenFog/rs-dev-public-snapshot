using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Drawing;
using RelertSharp.FileSystem;
using RelertSharp.Common;
using RelertSharp.MapStructure;
using static RelertSharp.Common.GlobalVar;
using static RelertSharp.GUI.GuiUtils;
using static RelertSharp.Language;

namespace RelertSharp.GUI.Controls
{
    public partial class TilePanel
    {
        private const string TNodeOther = "\nOther";
        private const string TNodeCliff = "\nCliff";
        private const string TNodeInWater = "\nInW";
        private const string TNodeRamps = "\nRamps";
        private const string TNodeBridge = "\nBridge";
        private const string TNodeRoad = "\nRoads";
        private const string TNodeFeat = "\nFeat";
        private const string TNodeTunnel = "\nTunnel";
        private const string TNodeShore = "\nShore";
        private const string TNodeRail = "\nRail";
        private const string TNodePavs = "\nPaves";
        private const string TNodeFix = "\nFixes";


        private void InitializeGeneralTilePanel()
        {
            foreach (string name in TileDictionary.GeneralTilesets.Keys)
            {
                trvGeneral.Nodes.Add(TileDictionary.GeneralTilesets[name].ToString(), name);
            }
            LoadOtherNodes();
        }
        private void LoadOtherNodes()
        {
            TreeNode other = NewNode(Constant.TileSetClass.OtherClass.ToLang(), TNodeOther);
            TreeNode cliff = NewNode(Constant.TileSetClass.Cliffs.ToLang(), TNodeCliff);
            TreeNode water = NewNode(Constant.TileSetClass.InWater.ToLang(), TNodeInWater);
            TreeNode ramp = NewNode(Constant.TileSetClass.Ramps.ToLang(), TNodeRamps);
            TreeNode bridge = NewNode(Constant.TileSetClass.Bridges.ToLang(), TNodeBridge);
            TreeNode road = NewNode(Constant.TileSetClass.Roads.ToLang(), TNodeRoad);
            TreeNode feature = NewNode(Constant.TileSetClass.Features.ToLang(), TNodeFeat);
            TreeNode rail = NewNode(Constant.TileSetClass.Railroad.ToLang(), TNodeRail);
            TreeNode tunnel = NewNode(Constant.TileSetClass.Tunnel.ToLang(), TNodeTunnel);
            TreeNode shore = NewNode(Constant.TileSetClass.Shore.ToLang(), TNodeShore);
            TreeNode paves = NewNode(Constant.TileSetClass.Paves.ToLang(), TNodePavs);
            TreeNode fix = NewNode(Constant.TileSetClass.Fixes.ToLang(), TNodeFix);
            foreach (TileSet set in TileDictionary.TileSets)
            {
                if (set.AllowPlace && !set.IsFramework)
                {
                    AddSetToNode(set, cliff, x => x.ClassifyAs("Cliff"));
                    AddSetToNode(set, water, x => x.ClassifyAs("Water"));
                    AddSetToNode(set, ramp, x => x.ClassifyAs("Ramp") || x.ClassifyAs("Slope"));
                    AddSetToNode(set, fix, x => x.ClassifyAs("Fix"));
                    AddSetToNode(set, bridge, x => x.ClassifyAs("Bridge"));
                    AddSetToNode(set, road, x => x.ClassifyAs("Road") || x.ClassifyAs("Highway"));
                    AddSetToNode(set, feature, x => x.ClassifyAs("Feature") || x.ClassifyAs("Farm"));
                    AddSetToNode(set, tunnel, x => x.ClassifyAs("Tunnel"));
                    AddSetToNode(set, shore, x => x.ClassifyAs("Shore"));
                    AddSetToNode(set, rail, x => x.ClassifyAs("Rail") || x.ClassifyAs("Track") || x.ClassifyAs("Train"));
                    AddSetToNode(set, paves, x => x.ClassifyAs("Pave"));
                }
            }
            other.LoadAs(ramp, cliff, road, fix, paves, water, bridge, shore, feature, rail, tunnel);
            trvGeneral.Nodes.Add(other);
        }
        private void AddSetToNode(TileSet src, TreeNode dest, Predicate<TileSet> predicate)
        {
            if (predicate.Invoke(src)) dest.Nodes.Add(src.SetIndex.ToString(), src.SetName);
        }



        private TileSet currentGeneralTileset;
        private TreeNode nodeNow;
        private string nodenameNow = "\n";
        private void trvGeneral_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                TreeNode node = e.Node;
                nodeNow = node;
                if (!node.Name.StartsWith("\n"))
                {
                    int scroll = flpGeneral.VerticalScroll.Value;
                    int index = int.Parse(node.Name);
                    currentGeneralTileset = TileDictionary.GetTileSetFromIndex(index);
                    LoadTileSetToFlp(flpGeneral, currentGeneralTileset, GeneralPbxClicked);
                    //if (nodenameNow == node.Name)
                    //{
                    //    flpGeneral.VerticalScroll.Value = scroll;
                    //    GeneralPbxClicked(flpGeneral.Controls[pbxIndexGeneral], new EventArgs());
                    //}
                    nodenameNow = node.Name;
                }
            }
        }
        private int pbxIndexGeneral;
        private void GeneralPbxClicked(object sender, EventArgs e)
        {
            PictureBox box = sender as PictureBox;
            pbxIndexGeneral = (int)box.Tag;
            if (prevBox != null)
            {
                prevBox.Image = prevImg;
            }
            prevImg = new Bitmap(box.Image);
            prevBox = box;
            Bitmap selected = new Bitmap(prevImg);
            Graphics g = Graphics.FromImage(selected);
            Pen p = new Pen(Color.Red, 1f);
            g.DrawRectangle(p, new Rectangle(0, 0, prevImg.Width - 1, prevImg.Height - 1));
            g.Dispose();
            box.Image = selected;
            NewTileSelected?.Invoke(this, currentGeneralTileset, pbxIndexGeneral);
        }
    }
}
