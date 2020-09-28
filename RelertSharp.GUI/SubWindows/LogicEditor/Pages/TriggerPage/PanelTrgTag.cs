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
using RelertSharp.MapStructure.Logic;
using RelertSharp.MapStructure;
using static RelertSharp.Common.GlobalVar;
using static RelertSharp.GUI.GuiUtils;

namespace RelertSharp.GUI.SubWindows.LogicEditor
{
    internal partial class PanelTrgTag : UserControl
    {
        internal event TriggerUpdateHandler TriggerUpdated;
        internal event TemplateStatChangedHandler TemplateStatChanged;
        internal event TriggerUpdateHandler TraceFired;
        internal event TriggerUpdateHandler NewTriggerFired;
        internal event EventHandler TriggerDeleted;


        internal TriggerItem CurrentTrigger { get; set; }
        internal TagItem CurrentTag { get; set; }
        internal IEnumerable<TagItem> CurrentTagCollection { get; set; }
        internal TriggerItem.DisplayingType DisplayingType { get; set; }
        private Map Map { get { return CurrentMapDocument.Map; } }


        private ListBox lbxTriggerList;


        public PanelTrgTag()
        {
            InitializeComponent();
        }


        #region Public Methods
        public void Initialize(ListBox lbxTrigger)
        {
            lbxTriggerList = lbxTrigger;
            SetLanguage();
            RefreshAttatchedList();
            RefreshHouseList();
        }
        public void Reload(TriggerItem trg, IEnumerable<TagItem> tags)
        {
            CurrentTagCollection = tags;
            CurrentTrigger = trg;
            if (tags == null || tags.Count() == 0) CurrentTag = null;
            else CurrentTag = tags.First();
            RefreshControl();
        }
        private void Reload(TriggerItem tmpTrg, TagItem tmpTag)
        {
            CurrentTagCollection = new TagItem[] { tmpTag };
            CurrentTrigger = tmpTrg;
            CurrentTag = tmpTag;
            RefreshControl();
        }
        public void RefreshAttatchedList()
        {
            cbbAttatchedTrg.LoadAs(TriggerItem.NullTrigger, Map.Triggers);
        }
        public void RefreshHouseList()
        {
            if (Map.Countries.Count() == 0) Warning(Language.DICT["WarHouseEmpty"]);
            lbxTriggerHouses.LoadAs(Map.Countries);
        }
        #endregion


        public void SetLanguage()
        {
            foreach (Control c in Controls) Language.SetControlLanguage(c);
        }
        private bool isControlRefreshing = false;
        private void RefreshControl()
        {
            isControlRefreshing = true;
            lklTraceTrigger.Enabled = false;
            if (CurrentTrigger == null || CurrentTag == null)
            {
                ClearControlContent(txbTrgID, txbTrgName, txbTagName, cbbTagID, ckbDisabled, ckbEasy, ckbNormal, ckbHard, rdbRepeat0, rdbRepeat1, rdbRepeat2);
                lbxTriggerHouses.SelectedIndex = 0;
                cbbAttatchedTrg.Text = "";
            }
            else
            {
                txbTrgID.Text = CurrentTrigger.ID;
                txbTrgName.Text = CurrentTrigger.Name;
                txbTagName.Text = CurrentTag.Name;
                cbbTagID.LoadAs(CurrentTagCollection);
                cbbTagID.SelectedItem = CurrentTag;
                ckbDisabled.Checked = CurrentTrigger.Disabled;
                ckbEasy.Checked = CurrentTrigger.EasyOn;
                ckbNormal.Checked = CurrentTrigger.NormalOn;
                ckbHard.Checked = CurrentTrigger.HardOn;
                rdbRepeat0.Checked = false;
                rdbRepeat0.Checked = false;
                rdbRepeat0.Checked = false;
                switch (CurrentTag.Repeating)
                {
                    case TriggerRepeatingType.NoRepeating:
                        rdbRepeat0.Checked = true;
                        break;
                    case TriggerRepeatingType.OneTimeLogicAND:
                        rdbRepeat1.Checked = true;
                        break;
                    case TriggerRepeatingType.RepeatLogicOR:
                        rdbRepeat2.Checked = true;
                        break;
                }
                lbxTriggerHouses.SelectedItem = Map.Countries.GetCountry(CurrentTrigger.House);
                if (CurrentTrigger.LinkedWith != "<none>")
                {
                    cbbAttatchedTrg.SelectFirst<TriggerItem>(x => x.ID == CurrentTrigger.LinkedWith);
                    lklTraceTrigger.Enabled = true;
                }
                else cbbAttatchedTrg.Text = "<none>";
            }
            isControlRefreshing = false;
        }


        private void DiffRepeatCheckChanged(object sender, EventArgs e)
        {
            if (!isControlRefreshing && CurrentTrigger != null && CurrentTag != null)
            {
                bool stat = sender.GetType() == typeof(CheckBox) ? ((CheckBox)sender).Checked : ((RadioButton)sender).Checked;
                string tag = ((Control)sender).Tag.ToString();
                switch (tag)
                {
                    case "e":
                        CurrentTrigger.EasyOn = stat;
                        break;
                    case "n":
                        CurrentTrigger.NormalOn = stat;
                        break;
                    case "h":
                        CurrentTrigger.HardOn = stat;
                        break;
                    case "d":
                        CurrentTrigger.Disabled = stat;
                        break;
                    case "0":
                    case "1":
                    case "2":
                        if (stat)
                        {
                            CurrentTrigger.Repeating = (TriggerRepeatingType)int.Parse(tag);
                            foreach (TagItem t in CurrentTagCollection) t.Repeating = CurrentTrigger.Repeating;
                        }
                        break;
                }
            }
        }


        #region OnEvent
        protected virtual void OnTriggerUpdated()
        {
            TriggerUpdated?.Invoke(this, CurrentTrigger);
        }
        protected virtual void OnTemplateChanged(bool stat)
        {
            TemplateStatChanged?.Invoke(this, stat);
        }
        protected virtual void OnTraceFired(TriggerItem target)
        {
            TraceFired?.Invoke(this, target);
        }
        protected virtual void OnNewTriggerAdded()
        {
            NewTriggerFired?.Invoke(this, CurrentTrigger);
        }
        protected virtual void OnTriggerDeleted()
        {
            TriggerDeleted?.Invoke(this, new EventArgs());
        }
        #endregion


        private void txbTrgName_Validated(object sender, EventArgs e)
        {
            if (!isControlRefreshing && CurrentTrigger != null && CurrentTag != null)
            {
                if (CurrentTag.Name == CurrentTrigger.Name + " - Tag")
                {
                    CurrentTag.Name = txbTrgName.Text + " - Tag";
                }
                CurrentTrigger.Name = txbTrgName.Text;
                txbTagName.Text = CurrentTag.Name;
                OnTriggerUpdated();
            }
        }

        private void lbxTriggerHouses_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isControlRefreshing && CurrentTrigger != null && CurrentTag != null)
            {
                CurrentTrigger.House = (lbxTriggerHouses.SelectedItem as CountryItem).Name;
            }
        }

        private void cbbAttatchedTrg_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isControlRefreshing && CurrentTrigger != null && CurrentTag != null)
            {
                TriggerItem trg = cbbAttatchedTrg.SelectedItem as TriggerItem;
                if (trg != null)
                {
                    CurrentTrigger.LinkedWith = trg.ID;
                    lklTraceTrigger.Enabled = trg.ID != "<none>";
                }
                else
                {
                    lklTraceTrigger.Enabled = false;
                }
            }
        }

        private TriggerItem prevTrg;
        private TagItem prevTag;
        private void tsmiEditTemp_Click(object sender, EventArgs e)
        {
            btnNewTrigger.Enabled = false;
            btnDelTrigger.Enabled = false;
            btnCopyTrigger.Enabled = false;
            cbbAttatchedTrg.Enabled = false;
            cbbCustomGroup.Enabled = false;
            btnSaveTemp.Visible = true;
            prevTrg = CurrentTrigger;
            prevTag = CurrentTag;
            Reload(Map.Triggers.TemplateTrigger, Map.Tags.TemplateTag);
            OnTemplateChanged(true);
            RefreshControl();
        }
        private void btnSaveTemp_Click(object sender, EventArgs e)
        {
            Reload(prevTrg, prevTag);
            OnTemplateChanged(false);
            RefreshControl();
            btnNewTrigger.Enabled = true;
            btnDelTrigger.Enabled = true;
            btnCopyTrigger.Enabled = true;
            cbbAttatchedTrg.Enabled = true;
            cbbCustomGroup.Enabled = true;
            btnSaveTemp.Visible = false;
        }

        private void lklTraceTrigger_Click(object sender, EventArgs e)
        {
            if (CurrentTrigger != null && CurrentTag != null)
            {
                TriggerItem asso = cbbAttatchedTrg.SelectedItem as TriggerItem;
                if (asso != null) OnTraceFired(asso);
            }
        }

        private void btnNewTrigger_Click(object sender, EventArgs e)
        {
            TriggerItem newtrigger = Map.NewTrigger(DisplayingType, out TagItem tag);
            CurrentTrigger = newtrigger;
            CurrentTag = tag;
            cbbAttatchedTrg.Items.Add(newtrigger);
            OnNewTriggerAdded();
        }

        private void btnDelTrigger_Click(object sender, EventArgs e)
        {
            if (CurrentTrigger != null && CurrentTag != null)
            {
                Map.RemoveTrigger(CurrentTrigger);
                CurrentTrigger = null;
                CurrentTagCollection = null;
                OnTriggerDeleted();
            }
        }

        private void btnCopyTrigger_Click(object sender, EventArgs e)
        {
            if (CurrentTrigger != null && CurrentTag != null)
            {
                TriggerItem t = Map.NewTrigger(CurrentTrigger, DisplayingType, out TagItem tag);
                CurrentTrigger = t;
                CurrentTag = tag;
                cbbAttatchedTrg.Items.Add(t);
                OnNewTriggerAdded();
            }
        }

        private void cbbTagID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isControlRefreshing && CurrentTrigger != null)
            {
                CurrentTag = cbbTagID.SelectedItem as TagItem;
                txbTagName.Text = CurrentTag.Name;
            }
        }
    }
}
