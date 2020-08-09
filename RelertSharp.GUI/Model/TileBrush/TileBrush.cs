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
    public partial class TileBrush
    {
        private I3dLocateable posNow;
        private Pnt offset = new Pnt(0, 0);
        private string filenameNow;
        private bool isFramework, isFlat;
        private List<Tile> body = new List<Tile>();
        private List<Tile> under = new List<Tile>();
        private List<Tile> surrounding = new List<Tile>();
        private List<Tile> surroundUnder = new List<Tile>();
        private List<I3dLocateable> posEnum = new List<I3dLocateable>();
        private TileSet tilesetNow;
        private int tileIndexNow;
        private bool brushDisposed;
        private bool latEnable = true, isSelfLat;
        private bool Lat { get { return latEnable && isSelfLat; } }
        private Map Map { get { return CurrentMapDocument.Map; } }


        #region Ctor
        public TileBrush() { }
        #endregion


        #region Public Methods
        public void SetLatEnable(bool enable)
        {
            latEnable = enable;
        }
        public void SetFramework(bool frameworkEnable)
        {
            isFramework = frameworkEnable;
            //Reload(tilesetNow, tileIndexNow);
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
            if (removePrev)
            {
                foreach (Tile t in body) t.Dispose();
                foreach (Tile t in surrounding) t.Dispose();
            }
            foreach (Tile t in under) t.RevealAllTileImg();
            foreach (Tile t in surroundUnder) t.RevealAllTileImg();
            filenameNow = set.GetName(index, false);
            int idx = set.Offset + index;
            TmpFile tmp = new TmpFile(GlobalDir.GetRawByte(filenameNow), filenameNow);
            short x = -100, y = -100;
            posEnum.Clear();
            body.Clear();
            under.Clear();
            surroundUnder.Clear();
            surrounding.Clear();
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
                    isSelfLat = TileDictionary.IsLat(t);
                    Engine.DrawGeneralItem(t);
                    //t.SwitchToFramework(isFramework);
                    t.FlatToGround(isFlat);
                    body.Add(t);
                }
            }
            if (posNow == null) posNow = new Pnt3(x, y, 0);
            else MoveTo(posNow, true);
            brushDisposed = false;
        }
        public void MoveTo(I3dLocateable cell, bool force = false)
        {
            if (force || (posNow != null && posNow.Coord != cell.Coord))
            {
                RedrawImage();
                foreach (Tile t in under) t.RevealAllTileImg();
                foreach (Tile t in surroundUnder) t.RevealAllTileImg();
                foreach (Tile t in surrounding) t.Dispose();
                surrounding.Clear();
                surroundUnder.Clear();
                under.Clear();
                int i = 0;
                Pnt beginPos = new Pnt(cell);
                foreach (I2dLocateable pos in new TileSet2D(beginPos + offset, posEnum))
                {
                    Tile dest = Map.TilesData[pos];
                    if (dest != null)
                    {
                        dest.HideExtraImg();
                        dest.HideTileImg();
                        under.Add(dest);
                        body[i].MoveTo(dest, posEnum[i].Z + cell.Z, cell.Z);
                        body[i].RevealAllTileImg();
                        if (Lat)
                        {
                            Map.TilesData.SwitchLat(body[i]);
                            List<Tile> neb = Map.TilesData.GetNeighbor(dest, out List<WallDirection> directions);
                            for (int nb = 0; nb < neb.Count; nb++)
                            {
                                if (Map.TilesData.NeedLat(neb[nb]))
                                {
                                    neb[nb].HideExtraImg();
                                    neb[nb].HideTileImg();
                                    surroundUnder.Add(neb[nb]);
                                    Tile surround = new Tile(neb[nb]);
                                    Map.TilesData.SwitchLat(surround, body[i], directions[nb]);
                                    surrounding.Add(surround);
                                }
                            }
                        }
                    }
                    else
                    {
                        body[i].HideExtraImg();
                        body[i].HideTileImg();
                    }
                    i++;
                }
                posNow = cell;
            }
        }
        public void Dispose()
        {
            foreach (Tile t in body) t.Dispose();
            foreach (Tile t in under) t.RevealAllTileImg();
            foreach (Tile t in surrounding) t.Dispose();
            foreach (Tile t in surroundUnder) t.RevealAllTileImg();
            brushDisposed = true;
        }
        public void AddTileAt(I3dLocateable cell)
        {
            foreach (Tile t in body)
            {
                t.SwitchToFramework(isFramework);
                Map.AddTile(t);
            }
            if (Lat)
            {
                foreach (Tile t in surrounding) Map.AddTile(t);
            }
            Reload(tilesetNow, tileIndexNow, false);
        }
        public void RedrawImage()
        {
            if (body.Count > 0 && brushDisposed)
            {
                foreach (Tile t in body)
                {
                    Engine.DrawGeneralItem(t);
                    //t.SwitchToFramework(isFramework);
                    t.FlatToGround(isFlat);
                }
                brushDisposed = false;
            }
        }
        public void ShiftOffsetX(int num)
        {
            offset.X += num;
        }
        public void ShiftOffsetY(int num)
        {
            offset.Y += num;
        }
        #endregion


        #region Private Methods
        #endregion
    }
}
