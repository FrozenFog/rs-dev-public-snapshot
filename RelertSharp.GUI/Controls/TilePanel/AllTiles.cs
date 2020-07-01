using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using RelertSharp.MapStructure;
using RelertSharp.FileSystem;
using static RelertSharp.Common.GlobalVar;
using static RelertSharp.GUI.GuiUtils;

namespace RelertSharp.GUI.Controls
{
    public partial class TilePanel
    {
        private void InitializeAllTilePanel()
        {
            LoadToObjectCollection(cbbAllTiles, TileDictionary.TileSets.Where(x=>!x.IsFramework && x.AllowPlace));
            cbbAllTiles.SelectedIndex = 0;
        }


        private bool updatingCbbAllTiles = false;
        private void cbbAllTiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!updatingCbbAllTiles)
            {
                GlobalDir.BeginPreload();
                flpAllTiles.Controls.Clear();
                imgAllTiles.Images.Clear();
                if (cbbAllTiles.SelectedItem is TileSet set)
                {
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
                    foreach (string filename in imgs.Keys)
                    {
                        PictureBox box = new PictureBox
                        {
                            Size = sz,
                            Image = Utils.Misc.ResizeImage(imgs[filename], sz),
                            Tag = i,
                        };
                        box.MouseClick += PictureBoxClicked;
                        flpAllTiles.Controls.Add(box);
                        //imgAllTiles.Images.Add(filename, Utils.Misc.ResizeImage(imgs[filename], sz));
                    }
                }
                GlobalDir.DisposePreloaded();
            }
        }
        PictureBox prevBox;
        Image prevImg;
        private void PictureBoxClicked(object sender, EventArgs e)
        {
            PictureBox box = sender as PictureBox;
            int index = (int)box.Tag;
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
        }
    }
}
