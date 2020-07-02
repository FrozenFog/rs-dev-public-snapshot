using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.FileSystem;
using RelertSharp.MapStructure;
using RelertSharp.Common;
using static RelertSharp.Common.GlobalVar;

namespace RelertSharp.GUI.Model.TileBrush
{
    public class TileBrush
    {
        private I2dLocateable pos;
        private string filenameNow;
        private bool isFramework;
        private List<Tile> body = new List<Tile>();
        private List<Tile> under = new List<Tile>();
        private List<I3dLocateable> posEnum = new List<I3dLocateable>();
        private Map Map { get { return CurrentMapDocument.Map; } }


        #region Ctor
        public TileBrush() { }
        #endregion


        #region Public Methods
        public void Reload(TileSet set, int index)
        {
            foreach (Tile t in body) t.SceneObject.Dispose();
            foreach (Tile t in under) t.RevealAllTileImg();
            filenameNow = set.GetName(index, false);
            int idx = set.Offset + index;
            TmpFile tmp = new TmpFile(GlobalDir.GetRawByte(filenameNow), filenameNow);
            short x = -100, y = -100;
            pos = new Pnt(x, y);
            posEnum.Clear();
            body.Clear();
            under.Clear();
            for (byte i = 0; i < tmp.Images.Count; i++)
            {
                TmpImage img = tmp.Images[i];
                if (!img.IsNullTile)
                {
                    int nx = img.X / 30;
                    int ny = img.Y / 15;
                    int dx = (nx + ny) / 2;
                    int dy = (ny - nx) / 2;
                    int z = img.Height;
                    posEnum.Add(new Pnt3(dx, dy, z));
                    Tile t = new Tile(idx, i, x + dx, y + dy, z);
                    Engine.DrawGeneralItem(t);
                    body.Add(t);
                }
            }
        }
        public void MoveTo(I3dLocateable cell)
        {
            if (pos.Coord != cell.Coord)
            {
                foreach (Tile t in under) t.RevealAllTileImg();
                under.Clear();
                int i = 0;

                foreach (I2dLocateable pos in new TileSet2D(cell, posEnum))
                {
                    Tile dest = Map.TilesData[pos];
                    if (dest != null)
                    {
                        dest.HideExtraImg();
                        dest.HideTileImg();
                        under.Add(dest);
                        body[i].MoveTo(dest, posEnum[i].Z + cell.Z);
                        body[i++].RevealAllTileImg();
                    }
                    else
                    {
                        body[i].HideExtraImg();
                        body[i++].HideTileImg();
                    }
                    //switcher[i++].MoveTo(dest);
                }
            }
        }
        #endregion


        #region Private Methods

        #endregion


        private class TileSwitcher
        {
            private int dz;
            private Tile tNow;
            private Tile tPrev;

            public TileSwitcher(Tile t)
            {
                tNow = t;
                tPrev = null;
                dz = t.Height;
            }

            public void MoveTo(Tile dest)
            {
                if (tNow.Coord != dest.Coord)
                {
                    dest.HideTileImg();
                    dest.HideExtraImg();
                    if (tPrev != null) tPrev.RevealAllTileImg();
                    tPrev = dest;
                    tNow.MoveTo(dest, dest.Height + dz);
                }
            }
            public void Dispose()
            {
                if (tPrev != null) tPrev.RevealAllTileImg();
                tNow.SceneObject.Dispose();
            }
        }
    }
}
