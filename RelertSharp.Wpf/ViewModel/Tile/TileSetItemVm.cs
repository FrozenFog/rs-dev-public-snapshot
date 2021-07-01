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
        public TileSetItemVm()
        {
            data = new TileSet(false, false, -1);
        }
        public TileSetItemVm(Image assembleImage)
        {
            image = assembleImage.ToWpfImage();
        }



        #region Binding
        public ImageSource TileSetImage
        {
            get { return image; }
            set
            {
                image = value;
                SetProperty();
            }
        }
        #endregion
    }
}
