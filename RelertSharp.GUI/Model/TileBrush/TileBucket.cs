using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using RelertSharp.Common;
using RelertSharp.MapStructure;
using RelertSharp.FileSystem;
using static RelertSharp.Common.GlobalVar;

namespace RelertSharp.GUI.Model.TileBrush
{
    public class TileBucket
    {
        private TileLayer Tiles { get { return CurrentMapDocument.Map.TilesData; } }
        private List<List<I3dLocateable>> randEnum = new List<List<I3dLocateable>>();
        private List<int> randIndex = new List<int>();
        private int numWidth, numHeight;
        private int increX, increY;
        private int maxX, maxY;
        private bool isFramework, isFlat;
        public TileBucket() { }


        public IEnumerable<Tile> FillTilesAt(I3dLocateable cell, Rectangle area, TileSet set)
        {
            InitializeInfo(set, area);
            ResetFillerInfo(set, area);
            maxX = cell.X + area.Width;
            maxY = cell.Y + area.Height;
            return Fill(new Pnt(area.X, area.Y), cell.Z);
        }


        private IEnumerable<Tile> Fill(I2dLocateable beginPos, int height)
        {
            List<Tile> result = new List<Tile>();
            for (int j = beginPos.Y; j < maxY; j += increY)
            {
                for (int i = beginPos.X; i < maxX; i += increX)
                {
                    byte sub = 0;
                    GetRandomTile((i << 8) + j, out List<I3dLocateable> posEnum, out int tileIndex);
                    foreach (I2dLocateable pos in new TileSet2D(new Pnt(i, j), posEnum))
                    {
                        if (Tiles[pos] is Tile org && org.Selected)
                        {
                            Tile t = new Tile(tileIndex, sub, pos.X, pos.Y, height);
                            Engine.DrawGeneralItem(t);
                            t.SwitchToFramework(isFramework);
                            t.FlatToGround(isFlat);
                            t.Select();
                            Tiles[pos].ReplaceWith(t);
                            Tiles[pos].Dispose();
                            Tiles[pos] = t;
                            result.Add(t);
                        }
                        sub++;
                    }

                }
            }
            return result;
        }
        private void InitializeInfo(TileSet set, Rectangle area)
        {
            string filename = set.GetName(0, false);
            TmpFile tmp = new TmpFile(GlobalDir.GetRawByte(filename), filename);
            numWidth = (int)Math.Ceiling(area.Width / (float)tmp.Width);
            numHeight = (int)Math.Ceiling(area.Height / (float)tmp.Height);
            increX = tmp.Width;
            increY = tmp.Height;
            tmp.Dispose();
        }
        private void ResetFillerInfo(TileSet set, Rectangle area)
        {
            randEnum.Clear();
            randIndex.Clear();
            for (int idx = 0; idx < set.Count; idx++)
            {
                string filename = set.GetName(idx, false);
                TmpFile tmp = new TmpFile(GlobalDir.GetRawByte(filename), filename);
                if (increX != tmp.Width || increY != tmp.Height) continue;

                randIndex.Add(set.Offset + idx);
                List<I3dLocateable> posEnum = new List<I3dLocateable>();
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
                    }
                }
                randEnum.Add(posEnum);
                tmp.Dispose();
            }
        }
        private void GetRandomTile(int seed, out List<I3dLocateable> posEnum, out int index)
        {
            Random r = new Random(seed);
            if (randEnum.Count > 2) posEnum = randEnum[r.Next(0, randEnum.Count - 2)];
            else posEnum = randEnum[0];
            if (randIndex.Count > 2) index = randIndex[r.Next(0, randIndex.Count - 2)];
            else index = randIndex[0];
        }
    }
}
