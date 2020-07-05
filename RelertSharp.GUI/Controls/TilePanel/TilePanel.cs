using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RelertSharp.MapStructure;
using RelertSharp.FileSystem;
using RelertSharp.GUI.Model.TileBrush;
using static RelertSharp.Common.GlobalVar;
using static RelertSharp.GUI.GuiUtils;

namespace RelertSharp.GUI.Controls
{
    public partial class TilePanel : UserControl
    {
        internal event TileSetSelectedHandler NewTileSelected;


        private bool isFramework, isFlat, reserveSelection;
        private Map Map { get { return CurrentMapDocument.Map; } }
        public TilePanel()
        {
            InitializeComponent();
        }


        public void Initialize()
        {
            SetLanguage();

            InitializeAllTilePanel();
            InitializeGeneralTilePanel();
        }
        public void SetFramework(bool frameworkEnable)
        {
            isFramework = frameworkEnable;
            reserveSelection = true;
            cbbAllTiles_SelectedIndexChanged(cbbAllTiles, new EventArgs());
            if (nodeNow != null) trvGeneral_NodeMouseClick(trvGeneral, new TreeNodeMouseClickEventArgs(nodeNow, MouseButtons.Left, 1, 0, 0));
            Result.SetFramework(frameworkEnable);
            reserveSelection = false;
        }
        public void SetFlat(bool flatEnable)
        {
            isFlat = flatEnable;
            Result.SetFlat(flatEnable);
        }


        private void SetLanguage()
        {
            foreach (Control c in Controls) Language.SetControlLanguage(c);
        }
        private void LoadTileSetToFlp(FlowLayoutPanel dest, TileSet set, MouseEventHandler dele)
        {
            if (set == null) return;
            GlobalDir.BeginPreload();
            dest.Controls.Clear();
            //imgAllTiles.Images.Clear();
            if (isFramework)
            {
                set = TileDictionary.GetFrameworkFromSet(set, out bool isHyte);
            }
            Size sz = new Size();
            int i = 0;
            Dictionary<string, Image> imgs = new Dictionary<string, Image>();
            foreach (string filename in set.GetNames())
            {
                TmpFile tmp = new TmpFile(GlobalDir.GetRawByte(filename), filename);
                tmp.LoadColor(TilePalette);
                sz = Utils.Misc.GetMaxSize(sz, tmp.AssembleImage.Size);
                imgs[filename] = tmp.AssembleImage;
            }
            //if (sz.Width > 256 || sz.Height > 256)
            //{
            //    int oversize = Math.Max(sz.Width, sz.Height);
            //    float scale = 256 / (float)oversize;
            //    sz.Width = (int)(sz.Width * scale);
            //    sz.Height = (int)(sz.Height * scale);
            //}
            //imgAllTiles.ImageSize = sz;
            dest.SuspendLayout();
            foreach (string filename in imgs.Keys)
            {
                PictureBox box = new PictureBox
                {
                    Size = sz,
                    Image = Utils.Misc.ResizeImage(imgs[filename], sz),
                    Tag = i++,
                };
                box.MouseClick += dele;
                dest.Controls.Add(box);
                //imgAllTiles.Images.Add(filename, Utils.Misc.ResizeImage(imgs[filename], sz));
            }
            GlobalDir.DisposePreloaded();
            dest.ResumeLayout();
        }


        public TileBrush Result { get; private set; } = new TileBrush();
    }
}
