using RelertSharp.FileSystem;
using RelertSharp.MapStructure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace RelertSharp.Wpf.ViewModel
{
    internal class TileSetItemVm : BaseVm<TileSet>
    {
        private ImageSource image;
        private ImageSource framework;
        private bool isFramework;
        public TileSetItemVm()
        {
            data = new TileSet(false, false, -1);
        }
        public TileSetItemVm(TmpFile file, int offset, int id, TmpFile frameworkFile)
        {
            SubTiles = new List<SubTileInfo>();
            SetWidth = file.Width; SetHeight = file.Height;
            image = file.AssembleImage.ToWpfImage();
            image.Freeze();
            if (frameworkFile != null)
            {
                framework = frameworkFile.AssembleImage.ToWpfImage();
                framework.Freeze();
            }
            TileIndex = offset + id;
            Id = id;
            byte idx = 0;
            foreach (var img in file.Images)
            {
                if (!img.IsNullTile)
                {
                    img.GetDeltaPosition(out int dx, out int dy, out int dz);
                    SubTileInfo info = new SubTileInfo()
                    {
                        Dx = dx,
                        Dy = dy,
                        Dz = dz,
                        SubIndex = idx
                    };
                    SubTiles.Add(info);
                }
                idx++;
            }
            IsLat = RelertSharp.Common.GlobalVar.TileDictionary.IsLat(TileIndex);
        }



        #region Call
        public int SetWidth { get; private set; }
        public int SetHeight { get; private set; }
        public bool IsFrameworkEnabled
        {
            get { return isFramework; }
            set
            {
                isFramework = value;
                SetProperty(nameof(TileSetImage));
            }
        }
        #region Binding
        public ImageSource TileSetImage
        {
            get
            {
                if (IsFrameworkEnabled) return framework ?? image;
                return image;
            }
        }
        #endregion

        public int TileIndex { get; private set; }
        public int FrameworkIndex { get; private set; }
        public List<SubTileInfo> SubTiles { get; private set; }
        public bool IsLat { get; private set; }
        public int Id { get; private set; }
        #endregion


        internal class SubTileInfo
        {
            public int Dx { get; internal set; }
            public int Dy { get; internal set; }
            public int Dz { get; internal set; }
            public byte SubIndex { get; internal set; }
        }
    }
}
