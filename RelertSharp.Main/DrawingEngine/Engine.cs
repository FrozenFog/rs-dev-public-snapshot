using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using RelertSharp.MapStructure;
using RelertSharp.Common;

namespace RelertSharp.DrawingEngine
{
    public class Engine
    {
        private readonly float _30SQ2, _10SQ3;
        private Dictionary<int, int> tilesData = new Dictionary<int, int>();
        private Dictionary<string, int> buffer = new Dictionary<string, int>();
        private int pPal = 0;


        #region Ctor - Engine
        public Engine()
        {
            _30SQ2 = (float)(30F * Math.Sqrt(2));
            _10SQ3 = (float)(10F * Math.Sqrt(3));
        }
        #endregion


        #region Public Methods - Engine
        public bool Initialize(IntPtr ptr)
        {
            return CppExtern.Scene.SetUpScene(ptr) && CppExtern.Scene.ResetSceneView();
        }
        public bool DrawTile(Tile t)
        {
            string name = GlobalVar.TileDictionary[t.TileIndex];
            int id = 0;
            if (!buffer.Keys.Contains(name))
            {
                VFileInfo info = GlobalVar.GlobalDir.GetFilePtr(name);
                id = CppExtern.Files.CreateTmpFileFromFilenMemory(info.ptr, info.size);
                CppExtern.Files.LoadTmpTextures(id, pPal);
                buffer[name] = id;
            }
            else id = buffer[name];
            Vec3 pos = new Vec3() { X = t.fX * _30SQ2, Y = t.fY * _30SQ2, Z = t.fHeight * _10SQ3 };
            int o1 = 0, o2 = 0;
            if (CppExtern.ObjectUtils.CreateTmpObjectAtScene(id, pos, t.SubIndex, ref o1, ref o2))
            {
                tilesData[t.Coord] = id;
                return true;
            }
            else return false;
        }
        public void SetTheater(TheaterType type)
        {
            GlobalVar.TileDictionary = new MapTheaterTileSet(type);
            VFileInfo pal = GlobalVar.GlobalDir.GetFilePtr(string.Format("iso{0}.pal", GlobalVar.TileDictionary.TheaterSub));
            if (pPal != 0) CppExtern.Files.RemovePalette(pPal);
            pPal = CppExtern.Files.CreatePaletteFromFileInBuffer(pal.ptr);
        }
        public void Refresh()
        {
            CppExtern.Scene.PresentAllObject();
        }
        public void MoveTo(int x, int y, int z = 0)
        {
            CppExtern.Scene.MoveFocusOnScene(new Vec3() { X = x * _30SQ2, Y = y * _30SQ2, Z = z * _10SQ3 });
        }
        public void ResetView()
        {
            CppExtern.Scene.ResetSceneView();
            Refresh();
        }
        public void ViewShift(Point delta)
        {
            CppExtern.Scene.MoveFocusOnScreen(delta.X, delta.Y);
            Refresh();
        }
        #endregion
    }
}
