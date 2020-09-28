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
            cbbAllTiles.LoadAs(TileDictionary.TileSets.Where(x => !x.IsFramework && x.AllowPlace));
            cbbAllTiles.SelectedIndex = 0;
        }


        private bool updatingCbbAllTiles = false;
        private void cbbAllTiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!updatingCbbAllTiles)
            {
                if (cbbAllTiles.SelectedItem is TileSet set)
                {
                    int slide = flpAllTiles.VerticalScroll.Value;
                    LoadTileSetToFlp(flpAllTiles, set, PictureBoxClicked);
                    if (reserveSelection)
                    {
                        flpAllTiles.VerticalScroll.Value = slide;
                        PictureBoxClicked(flpAllTiles.Controls[pbxIndexAllTiles], new EventArgs());
                    }
                }
            }
        }
        PictureBox prevBox;
        Image prevImg;
        private int pbxIndexAllTiles;
        private void PictureBoxClicked(object sender, EventArgs e)
        {
            PictureBox box = sender as PictureBox;
            pbxIndexAllTiles = (int)box.Tag;
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
            NewTileSelected?.Invoke(this, cbbAllTiles.SelectedItem as TileSet, pbxIndexAllTiles);
        }
    }
}
