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
using RelertSharp.MapStructure.Logic;
using RelertSharp.IniSystem;
using static RelertSharp.GUI.GuiUtils;

namespace RelertSharp.GUI.SubWindows.LogicEditor
{
    internal partial class PanelEvent : UserControl
    {
        internal event TriggerUpdateHandler TriggerRefreshing;
        internal event TriggerUpdateHandler TriggerTracing;
        internal event SoundPlayingHandler NeedPlayingSound;


        internal LogicGroup EventCollection { get; set; }
        private LogicItem CurrentEvent { get; set; }
        private Map Map { get { return GlobalVar.CurrentMapDocument.Map; } }


        private bool isEvent = true;
        private bool initialized = false;
        private string parentID;
        private ListBox lbxTriggerList;


        public PanelEvent()
        {
            InitializeComponent();
        }


        #region Public Methods
        public void Initialize(ListBox lbx, bool isEvent, DescriptCollection coll)
        {
            SetLanguage(isEvent);
            lbxTriggerList = lbx;
            this.isEvent = isEvent;
            IEnumerable<TriggerDescription> desc;
            if (isEvent) desc = coll.Events;
            else desc = coll.Actions;
            pnlParameter.Initialize(desc, lbxEventList);
            pnlParameter.ItemUpdated += PnlParameter_ItemUpdated;
            pnlParameter.TriggerTracing += PnlParameter_TriggerTracing;
            pnlParameter.NeedPlayingShound += PnlParameter_NeedPlayingShound;
        }

        private void PnlParameter_NeedPlayingShound(object sender, TriggerParam param, TechnoPair p)
        {
            OnPlayingSound(param, p);
        }

        private void PnlParameter_TriggerTracing(object sender, TriggerItem trigger)
        {
            OnTracingTrigger(trigger);
        }

        private int previousIndex = 0;
        private void PnlParameter_ItemUpdated(object sender, LogicItem item)
        {
            UpdateAt(lbxEventList, item, ref updatingLbxEventList);
            previousIndex = lbxEventList.SelectedIndex;
        }

        public void Reload(TriggerItem trigger)
        {
            if (initialized)
            {
                if (isEvent) Map.Triggers[parentID].Events = EventCollection;
                else Map.Triggers[parentID].Actions = EventCollection;
            }
            else
            {
                initialized = true;
            }
            if (isEvent) EventCollection = trigger.Events;
            else EventCollection = trigger.Actions;
            parentID = trigger.ID;
            if (EventCollection.Count() > 0)
            {
                CurrentEvent = EventCollection.First(); ;
            }
            else
            {
                lbxEventList.Items.Clear();
                pnlParameter.ClearContent();
            }
            RefreshControl();
        }
        #endregion


        #region OnEvent
        protected virtual void OnPlayingSound(TriggerParam param, TechnoPair p)
        {
            NeedPlayingSound?.Invoke(this, param, p);
        }
        protected virtual void OnTracingTrigger(TriggerItem trigger)
        {
            TriggerTracing?.Invoke(this, trigger);
        }
        protected virtual void OnTriggerRefreshing(TriggerItem trigger)
        {
            TriggerRefreshing?.Invoke(this, trigger);
        }
        #endregion


        private bool isControlRefreshing = false;
        private void RefreshControl()
        {
            isControlRefreshing = true;
            LoadToObjectCollection(lbxEventList, EventCollection);
            isControlRefreshing = false;
            if (EventCollection.Count() > 0)
            {
                lbxEventList.SelectedIndex = 0;
                pnlParameter.Reload(CurrentEvent);
            }
        }
        private void SetLanguage(bool isEvent)
        {
            if (!isEvent)
            {
                foreach(Control c in Controls)
                {
                    SetActionLabel(c);
                }
            }
            foreach (Control c in Controls)
            {
                Language.SetControlLanguage(c);
            }
        }
        private void SetActionLabel(Control c)
        {
            Type t = c.GetType();
            if (t == typeof(ParameterPanel) || t == typeof(GroupBox))
            {
                ControlCollection iter;
                ParameterPanel pnl = c as ParameterPanel;
                if (pnl == null) iter = (c as GroupBox).Controls;
                else iter = pnl.Controls;
                foreach (Control child in iter)
                {
                    SetActionLabel(child);
                }
            }
            else c.Text = c.Text.Replace("Event", "Action");
        }

        private void btnNewEvent_Click(object sender, EventArgs e)
        {
            if (lbxTriggerList.SelectedItem == null) return;
            if (lbxEventList.Items.Count > 19) return;
            isControlRefreshing = true;
            LogicItem item;
            if (isEvent) item = Map.Triggers[parentID].Events.NewEvent();
            else item = Map.Triggers[parentID].Actions.NewAction();
            lbxEventList.Items.Add(item);
            lbxEventList.SelectedItem = item;
            pnlParameter.Reload(item);
            isControlRefreshing = false;
        }

        private void btnDeleteEvent_Click(object sender, EventArgs e)
        {
            if (lbxEventList.SelectedItem == null) return;
            LogicItem ev = pnlParameter.CurrentItem;
            Map.Triggers[parentID].Events.Remove(ev);
            pnlParameter.CurrentItem = null;
            RemoveAt(lbxEventList, lbxEventList.SelectedIndex, ref updatingLbxEventList);
        }

        private bool updatingLbxEventList = false;
        private void lbxEventList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!updatingLbxEventList)
            {
                LogicItem item = lbxEventList.SelectedItem as LogicItem;
                pnlParameter.Reload(item);
            }
        }

        private void tsmiCopyEventAdv_Click(object sender, EventArgs e)
        {
            dlgCopy d = new dlgCopy();
            if (d.ShowDialog() == DialogResult.OK)
            {
                Map.Triggers[parentID].Events.Multiply(d.Result.Split(new char[] { '\n' }), CurrentEvent, pnlParameter.CurrentParams);
                OnTriggerRefreshing(Map.Triggers[parentID]);
            }
        }
    }
}
