using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RelertSharp.MapStructure;
using RelertSharp.Common;
using static RelertSharp.Common.GlobalVar;

namespace RelertSharp.GUI.Controls
{
    public partial class RbPanelBucket : RbPanelBase
    {
        private class SetCombo
        {
            private string text;
            public SetCombo(string text, int index)
            {
                Index = index;
                this.text = text;
            }
            public override string ToString()
            {
                return text;
            }
            public int Index { get; private set; }
        }
        public RbPanelBucket()
        {
            InitializeComponent();
        }


        public void Initialize()
        {
            SetLanguage();
            List<object> objs = new List<object>();
            foreach (string set in TileDictionary.GeneralTilesets.Keys)
            {
                if (set != Language.DICT[Constant.TileSetClass.Ramp])
                {
                    int index = TileDictionary.GeneralTilesets[set];
                    TileSet tileset = TileDictionary.GetTileSetFromIndex(index);
                    if (tileset != null) objs.Add(new SetCombo(tileset.SetName, TileDictionary.GeneralTilesets[set]));
                }
            }
            GuiUtils.LoadToObjectCollection(cbbFillBy, objs);
            cbbFillBy.SelectedIndex = 0;
        }


        public TileSet BucketSet
        {
            get
            {
                SetCombo c = cbbFillBy.SelectedItem as SetCombo;
                return TileDictionary.GetTileSetFromIndex(c.Index);
            }
        }
    }
}
