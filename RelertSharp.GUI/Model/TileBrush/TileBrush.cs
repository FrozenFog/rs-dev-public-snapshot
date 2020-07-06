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
        private bool isFramework, isFlat;
        private List<Tile> body = new List<Tile>();
        private List<Tile> under = new List<Tile>();
        private List<I3dLocateable> posEnum = new List<I3dLocateable>();
        private TileSet tilesetNow;
        private int tileIndexNow;
        private bool brushDisposed;
        private Map Map { get { return CurrentMapDocument.Map; } }


        #region Ctor
        public TileBrush() { }
        #endregion


        #region Public Methods
        public void SetFramework(bool frameworkEnable)
        {
            isFramework = frameworkEnable;
            Reload(tilesetNow, tileIndexNow);
        }
        public void SetFlat(bool flatEnable)
        {
            isFlat = flatEnable;
            Reload(tilesetNow, tileIndexNow);
        }
        public void Reload(TileSet set, int index, bool removePrev = true)
        {
            if (set == null) return;
            tilesetNow = set;
            tileIndexNow = index;
            if (removePrev) foreach (Tile t in body) t.Dispose();
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
                    t.SwitchToFramework(isFramework);
                    t.FlatToGround(isFlat);
                    body.Add(t);
                }
            }
            brushDisposed = false;
        }
        public void MoveTo(I3dLocateable cell)
        {
            if (pos != null && pos.Coord != cell.Coord)
            {
                RedrawImage();
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
                        body[i].MoveTo(dest, posEnum[i].Z + cell.Z, cell.Z);
                        body[i++].RevealAllTileImg();
                    }
                    else
                    {
                        body[i].HideExtraImg();
                        body[i++].HideTileImg();
                    }
                }
            }
        }
        public void Dispose()
        {
            foreach (Tile t in body) t.Dispose();
            foreach (Tile t in under) t.RevealAllTileImg();
            brushDisposed = true;
        }
        public void AddTileAt(I3dLocateable cell)
        {
            foreach (Tile t in body) Map.AddTile(t);
            Reload(tilesetNow, tileIndexNow, false);
        }
        public void RedrawImage()
        {
            if (body.Count > 0 && brushDisposed)
            {
                foreach (Tile t in body)
                {
                    Engine.DrawGeneralItem(t);
                    t.SwitchToFramework(isFramework);
                    t.FlatToGround(isFlat);
                }
                brushDisposed = false;
            }
        }
        #endregion


        #region Private Methods
        #endregion
    }
}
