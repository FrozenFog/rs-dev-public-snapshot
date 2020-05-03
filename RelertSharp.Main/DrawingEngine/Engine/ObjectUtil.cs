using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using RelertSharp.FileSystem;
using RelertSharp.IniSystem;
using RelertSharp.MapStructure;
using RelertSharp.MapStructure.Points;
using RelertSharp.MapStructure.Objects;
using RelertSharp.DrawingEngine.Drawables;
using RelertSharp.DrawingEngine.Presenting;
using RelertSharp.Common;
using static RelertSharp.Utils.Misc;
using static RelertSharp.Common.GlobalVar;

namespace RelertSharp.DrawingEngine
{
    public partial class Engine
    {
        public void SelectUnitAt(I2dLocateable pos)
        {
            int coord = pos.Coord;
            if (Buffer.Scenes.Units.Keys.Contains(coord))
            {
                Buffer.Scenes.MarkUnit(coord);
                Refresh();
            }
        }
        public void UnSelectUnitAt(I2dLocateable pos)
        {
            int coord = pos.Coord;
            if (Buffer.Scenes.Units.Keys.Contains(coord))
            {
                Buffer.Scenes.UnMarkUnit(coord);
            }
        }
        public void RemoveUnitAt(I2dLocateable pos)
        {
            int coord = pos.Coord;
            if (Buffer.Scenes.Units.Keys.Contains(coord))
            {
                Buffer.Scenes.RemoveUnitAt(coord);
            }
        }
    }
}
