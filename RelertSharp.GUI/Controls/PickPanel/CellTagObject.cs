using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RelertSharp.MapStructure.Points;
using RelertSharp.MapStructure.Logic;
using RelertSharp.IniSystem;
using RelertSharp.Common;
using static RelertSharp.Common.GlobalVar;
using static RelertSharp.GUI.GuiUtils;


namespace RelertSharp.GUI.Controls
{
    public partial class PickPanel
    {
        internal event CelltagCollectionHandler SelectCelltagCollection;
        internal event EventHandler ReleaseCelltags;

        private TagItem selectedTag;


        private void InitializeCelltagPanel()
        {
            updatingCbbCelltag = true;
            LoadToObjectCollection(cbbCelltag, Map.Tags);
            LoadToObjectCollection(lbxCelltags, Map.Celltags);
            updatingCbbCelltag = false;
        }
        private void ReloadCellTag()
        {
            if (drew)
            {
                InnerSource = new CellTagItem(selectedTag.ID);
                brush.Reload(InnerSource, MapObjectType.Celltag);
            }
        }


        private bool updatingCbbCelltag = false;
        private void cbbCelltag_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!updatingCbbCelltag)
            {
                if (cbbCelltag.SelectedItem is TagItem tag)
                {
                    selectedTag = tag;
                    ReloadCellTag();
                }
                else
                {
                    InnerSource.Dispose();
                }
            }
        }
        private void lbxCelltags_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (drew && (sender as ListBox).SelectedItem is CellTagItem cell)
            {
                BaseNodeTracing?.Invoke(this, cell);
            }
        }
        private void lbxCelltags_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbxCelltags.SelectedItem is CellTagItem cell)
            {
                if (drew)
                {
                    ReleaseCelltags?.Invoke(this, new EventArgs());
                }
                IEnumerable<CellTagItem> same = Map.Celltags.Where(x => x.TagID == cell.TagID);
                LoadToObjectCollection(lbxSameTags, same);
            }
        }
        private void btnSelectAllSameCell_Click(object sender, EventArgs e)
        {
            if (drew)
            {
                SelectCelltagCollection?.Invoke(this, lbxSameTags.Items.Cast<CellTagItem>());
            }
        }
        private void tsmiTraceTag_Click(object sender, EventArgs e)
        {
            if (lbxCelltags.SelectedItem is CellTagItem cell)
            {
                if (Map.Tags[cell.TagID] is TagItem tag)
                {
                    TraceCelltag?.Invoke(this, tag);
                }
            }
        }
    }
}
