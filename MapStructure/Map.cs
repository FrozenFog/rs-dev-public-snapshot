using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using relert_sharp.FileSystem;
using relert_sharp.Utils;

namespace relert_sharp.MapStructure
{
    public class Map
    {
        public enum MapType
        {
            MissionMap, MultiplayerMap
        }

        private string mapFileName;
        private string mapPath;
        private string isomappack5String, overlayString, overlaydataString, previewString;
        private MapType maptype;
        private MapFile f;
        private MapInfo info;
        private Rectangle previewSize;
        private TileLayer Tiles;
        public Map(MapFile File)
        {
            f = File;
            mapFileName = f.FileName;
            mapPath = f.FilePath;
            isomappack5String = f.PopEnt("IsoMapPack5").JoinString();
            overlayString = f.PopEnt("OverlayPack").JoinString();
            overlaydataString = f.PopEnt("OverlayDataPack").JoinString();
            GetPreview();
            info = new MapInfo(f.PopEnt("Basic"), f.PopEnt("Map"), f.PopEnt("SpecialFlags"));
            Tiles = new TileLayer(isomappack5String, info.Size);
        }
        private void GetPreview()
        {
            INIEntity preview = f.PopEnt("Preview");
            if (preview.DataList.Count == 0) return;
            int[] buf = preview.GetPair("Size").ParseIntList();
            previewSize = new Rectangle(buf[0], buf[1], buf[2], buf[3]);
            previewString = f.PopEnt("PreviewPack").JoinString();
        }
        public void Dispose()
        {
            
        }
    }
}
