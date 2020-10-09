using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RelertSharp.Common;
using RelertSharp.MapStructure;
using RelertSharp.IniSystem;
using RelertSharp.GUI.Model.BrushModel;
using static RelertSharp.Common.GlobalVar;
using static RelertSharp.GUI.GuiUtils;

namespace RelertSharp.GUI.Controls
{
    public partial class PickPanel : UserControl
    {
        internal event EventHandler BrushObjectSelected;
        internal event I2dLocateableHandler BaseNodeTracing;
        internal event TriggerTagItemHandler TraceCelltag;


        private Map Map { get { return CurrentMapDocument.Map; } }
        private BrushModel brush = new BrushModel();
        private bool drew = false;
        private bool shiftHidden = false;


        public PickPanel()
        {
            InitializeComponent();
        }


        public void Initialize()
        {
            SetLanguage();
            InitializeGeneralPanel();
            InitializeTerrainPanel();
            InitializeOverlayPanel();
            InitializeSmudgePanel();
            InitializeNodePanel();
            InitializeCelltagPanel();
            InitializeWpPanel();

            initialized = true;
        }
        public void DrawComplete()
        {
            drew = true;
        }
        public void DisableDrawing()
        {
            drew = false;
        }
        public void RefreshContainerMember()
        {
            LoadWaypointAll();
            cbbNodeHouse_SelectedIndexChanged(null, null);
        }
        public void ShiftHide()
        {
            if (!shiftHidden && drew)
            {
                shiftHidden = true;
                Result.BrushObject?.Dispose();
                Engine.Refresh();
            }
        }
        public void ShiftUnHide()
        {
            if (shiftHidden && drew)
            {
                shiftHidden = false;
                Result.RedrawBrushObject();
                Engine.Refresh();
            }
        }
        private void SetLanguage()
        {
            foreach (Control c in Controls) c.SetLanguage();
        }
        private void tbcMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrentType = (PickPanelType)tbcMain.SelectedIndex;
            rdbWpNone.Checked = true;
            if (CurrentType == PickPanelType.Waypoints) LoadWaypointAll();
        }
        public BrushModel Result { get { return brush; } }
        public PickPanelType CurrentType { get; private set; }
    }
}
