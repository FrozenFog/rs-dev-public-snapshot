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
        internal event I2dLocateableHandler JumpToWaypoint;


        internal LogicGroup EventCollection { get; set; }
        private LogicItem CurrentEvent { get; set; }
        private TriggerItem ParentTrigger { get; set; }
        private Map Map { get { return GlobalVar.CurrentMapDocument.Map; } }


        private bool isEvent = true;
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
            if (isEvent)
            {
                desc = coll.Events;

            }
            else desc = coll.Actions;
            pnlParameter.Initialize(desc, lbxEventList, isEvent);
            pnlParameter.ItemUpdated += PnlParameter_ItemUpdated;
            pnlParameter.TriggerTracing += PnlParameter_TriggerTracing;
            pnlParameter.NeedPlayingShound += PnlParameter_NeedPlayingShound;
            pnlParameter.JumpToWaypoint += PnlParameter_JumpToWaypoint;
        }

        private void PnlParameter_JumpToWaypoint(object sender, I2dLocateable pos)
        {
            OnJumpToWaypoint(pos);
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
            lbxEventList.UpdateAt(item, ref updatingLbxEventList);
            previousIndex = lbxEventList.SelectedIndex;
        }

        public void Reload(TriggerItem trigger)
        {
            ParentTrigger = trigger;
            if (trigger == null)
            {
                EventCollection = null;
            }
            else
            {
                if (isEvent) EventCollection = trigger.Events;
                else EventCollection = trigger.Actions;
            }
            if (EventCollection != null && EventCollection.Count() > 0)
            {
                CurrentEvent = EventCollection.First();
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
        protected virtual void OnJumpToWaypoint(I2dLocateable cell)
        {
            JumpToWaypoint?.Invoke(this, cell);
        }
        #endregion


        private bool isControlRefreshing = false;
        private void RefreshControl()
        {
            isControlRefreshing = true;
            EventCollection.Sort();
            lbxEventList.LoadAs(EventCollection);
            if (EventCollection.Count() > 0)
            {
                lbxEventList.SelectedIndex = 0;
                pnlParameter.Reload(CurrentEvent);
            }
            else pnlParameter.Reload(null);
            isControlRefreshing = false;
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
            if (c is ParameterPanel pnl)
            {
                foreach (Control sub in pnl.Controls) SetActionLabel(sub);
            }
            else if (c is GroupBox gpb)
            {
                foreach (Control gsub in gpb.Controls) SetActionLabel(gsub);
            }
            else if (c is SplitContainer split)
            {
                foreach (Control ssub in split.Panel1.Controls) SetActionLabel(ssub);
                foreach (Control ssub in split.Panel2.Controls) SetActionLabel(ssub);
            }
            else if (c is Panel panel)
            {
                foreach (Control psub in panel.Controls) SetActionLabel(psub);
            }
            else if (c is TableLayoutPanel tlp)
            {
                foreach (Control tsub in tlp.Controls) SetActionLabel(tsub);
            }
            else c.Text = c.Text.Replace("Event", "Action");
        }

        private void btnNewEvent_Click(object sender, EventArgs e)
        {
            if (ParentTrigger == null) return;
            if (lbxEventList.Items.Count > 30) return;
            isControlRefreshing = true;
            LogicItem item;
            if (isEvent) item = EventCollection.NewEvent();
            else item = EventCollection.NewAction();
            lbxEventList.Add(item, ref updatingLbxEventList);
            pnlParameter.Reload(item);
            isControlRefreshing = false;
        }

        private void btnDeleteEvent_Click(object sender, EventArgs e)
        {
            if (lbxEventList.SelectedItem == null) return;
            LogicItem ev = pnlParameter.CurrentItem;
            EventCollection.Remove(ev);
            pnlParameter.CurrentItem = null;
            lbxEventList.RemoveAt(lbxEventList.SelectedIndex, ref updatingLbxEventList);
            if (lbxEventList.Items.Count == 0) pnlParameter.Reload(null);
        }

        private bool updatingLbxEventList = false;
        private void lbxEventList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!updatingLbxEventList && !isControlRefreshing)
            {
                LogicItem item = lbxEventList.SelectedItem as LogicItem;
                CurrentEvent = item;
                pnlParameter.Reload(item);
            }
        }

        private void tsmiCopyEventAdv_Click(object sender, EventArgs e)
        {
            dlgCopy d = new dlgCopy();
            if (d.ShowDialog() == DialogResult.OK)
            {
                EventCollection.Multiply(d.Result.Split(new char[] { '\n' }), CurrentEvent, pnlParameter.CurrentParams);
            }
        }

        private void btnCopyEvent_Click(object sender, EventArgs e)
        {
            if (ParentTrigger != null && EventCollection != null)
            {
                int num = EventCollection.GetCount();
                LogicItem lg = new LogicItem(CurrentEvent, num);
                EventCollection.Add(lg);
                lbxEventList.Add(lg, ref updatingLbxEventList);
            }
        }
    }
}
