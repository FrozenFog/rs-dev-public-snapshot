using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RelertSharp.MapStructure.Points;
using RelertSharp.Common;
using static RelertSharp.GUI.GuiUtils;
using static RelertSharp.Common.GlobalVar;

namespace RelertSharp.GUI.Controls
{
    public partial class PickPanel
    {
        private void InitializeWpPanel()
        {
            LoadWaypointAll();
        }
        private void LoadWaypointAll()
        {
            LoadToObjectCollection(lbxWaypoint, CurrentMapDocument.Map.Waypoints);
        }


        private void lbxWaypoint_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (drew)
            {
                if (lbxWaypoint.Items.Count !=0 && lbxWaypoint.SelectedItem is WaypointItem wp)
                {
                    BaseNodeTracing?.Invoke(this, wp);
                }
            }
        }
        private void rdbWpNum_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is RadioButton rdb && rdb.Checked)
            {
                if (rdb.Tag.ToString() != "0")
                {
                    brush.BrushObject?.Dispose();
                    brush.BrushObject = null;
                }
            }
        }
        public bool IsWpFirstNum { get { return rdbWpNum.Checked; } }
        public bool IsWpDesignatedNum { get { return rdbWpDesignated.Checked; } }
    }
}
