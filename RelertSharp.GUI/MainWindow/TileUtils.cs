using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RelertSharp.MapStructure;
using RelertSharp.Common;
using static RelertSharp.Common.GlobalVar;

namespace RelertSharp.GUI
{
    public partial class MainWindowTest
    {
        private bool isSelectingTile, isDeSelectingTile;
        private void SwitchToFramework(bool enable)
        {
            if (drew)
            {
                Map.SwitchToFramework(enable);
            }
        }
        private void SwitchToFlatGround(bool enable)
        {
            if (drew)
            {
                Map.SwitchFlatGround(enable);
            }
        }




        private void BeginTileSelecting()
        {
            if (drew)
            {
                isSelectingTile = true;
            }
        }
        private void EndTileSelecting()
        {
            if (drew)
            {
                isSelectingTile = false;
            }
        }
        private void SelectTileAt(Vec3 vec)
        {
            if (vec != Vec3.Zero)
            {
                I3dLocateable pos = vec.To3dLocateable();
                if (Map.TilesData[pos] is Tile t && !t.Selected)
                {
                    Current.SelectTile(t);
                }
            }
        }


        private void BeginTileDeSelecting()
        {
            if (drew)
            {
                isDeSelectingTile = true;
            }
        }
        private void EndTileDeSelecting()
        {
            if (drew)
            {
                isDeSelectingTile = false;
            }
        }
        private void DeSelectTileAt(Vec3 vec)
        {
            if (vec != Vec3.Zero)
            {
                I3dLocateable pos = vec.To3dLocateable();
                if (Map.TilesData[pos] is Tile t && t.Selected)
                {
                    Current.DeSelectTile(t);
                }
            }
        }
    }
}
