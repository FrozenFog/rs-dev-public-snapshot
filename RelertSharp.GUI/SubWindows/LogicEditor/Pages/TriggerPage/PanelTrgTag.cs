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
        internal event TriggerUpdateHandler TriggerDeleted;


        internal TriggerItem CurrentTrigger { get; set; }
        internal TagItem CurrentTag { get; set; }
        private Map Map { get { return CurrentMapDocument.Map; } }


        private ListBox lbxTriggerList;
        private bool initialized = false;


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
        public void Reload(TriggerItem trg, IEnumerable<TagItem> tags, bool isTemplate = false)
        {
            if (!isTemplate)
            {
                if (initialized)
                {
                    Map.Triggers[CurrentTrigger.ID] = CurrentTrigger;
                    Map.Tags[CurrentTag.ID] = CurrentTag;
                }
                else initialized = true;
            }
            CurrentTrigger = trg;
            CurrentTag = tags.First();
            RefreshControl(tags);
        }
        private void Reload(TriggerItem tmpTrg, TagItem tmpTag)
        {
            CurrentTrigger = tmpTrg;
            CurrentTag = tmpTag;
            RefreshControl(new TagItem[] { tmpTag });
        }
        public void RefreshAttatchedList()
        {
            cbbAttatchedTrg.Items.Clear();
            cbbAttatchedTrg.Items.Add(TriggerItem.NullTrigger);
            cbbAttatchedTrg.Items.AddRange(Map.Triggers.ToArray());
        }
        public void RefreshHouseList()
        {
            LoadToObjectCollection(lbxTriggerHouses, Map.Countries);
        }
        #endregion


        public void SetLanguage()
        {
            foreach (Control c in Controls) Language.SetControlLanguage(c);
        }
        private bool isControlRefreshing = false;
        private void RefreshControl(IEnumerable<TagItem> tags)
        {
            isControlRefreshing = true;
            txbTrgID.Text = CurrentTrigger.ID;
            txbTrgName.Text = CurrentTrigger.Name;
            txbTagName.Text = CurrentTag.Name;
            LoadToObjectCollection(cbbTagID, tags);
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
            cbbAttatchedTrg.Text = CurrentTrigger.LinkedWith;
            lklTraceTrigger.Enabled = CurrentTag.AssoTrigger != "<none>";
            isControlRefreshing = false;
        }


        private void DiffRepeatCheckChanged(object sender, EventArgs e)
        {
            if (!isControlRefreshing)
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
                            CurrentTrigger.Repeating = (TriggerRepeatingType)int.Parse(tag);
                        break;
                }
            }
        }

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
        protected virtual void OnNewTriggerAdded(TriggerItem newtrigger)
        {
            NewTriggerFired?.Invoke(this, newtrigger);
        }
        protected virtual void OnTriggerDeleted(TriggerItem deletedTrigger)
        {
            TriggerDeleted?.Invoke(this, deletedTrigger);
        }
        private void txbTrgName_Validated(object sender, EventArgs e)
        {
            if (!isControlRefreshing)
            {
                if (CurrentTag.Name == CurrentTrigger.Name + " - Tag")
                {
                    CurrentTag.Name = txbTrgName.Text + " - Tag";
                }
                CurrentTrigger.Name = txbTrgName.Text;
                OnTriggerUpdated();
            }
        }

        private void lbxTriggerHouses_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isControlRefreshing)
            {
                CurrentTrigger.House = (lbxTriggerHouses.SelectedItem as CountryItem).Name;
            }
        }

        private void cbbAttatchedTrg_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isControlRefreshing)
            {
                TriggerItem trg = cbbAttatchedTrg.SelectedItem as TriggerItem;
                if (trg != null)
                {
                    CurrentTrigger.LinkedWith = trg.ID;
                    lklTraceTrigger.Visible = trg.ID != "<none>";
                }
                else
                {
                    lklTraceTrigger.Visible = false;
                }
            }
        }

        private bool isEditingTemplate = false;
        private TriggerItem prevTrg;
        private TagItem prevTag;
        private void tsmiEditTemp_Click(object sender, EventArgs e)
        {
            isEditingTemplate = true;
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
            RefreshControl(new TagItem[] { CurrentTag });
        }
        private void btnSaveTemp_Click(object sender, EventArgs e)
        {
            isEditingTemplate = false;
            Reload(prevTrg, prevTag);
            OnTemplateChanged(false);
            RefreshControl(new TagItem[] { CurrentTag });
            btnNewTrigger.Enabled = true;
            btnDelTrigger.Enabled = true;
            btnCopyTrigger.Enabled = true;
            cbbAttatchedTrg.Enabled = true;
            cbbCustomGroup.Enabled = true;
            btnSaveTemp.Visible = false;
        }

        private void lklTraceTrigger_Click(object sender, EventArgs e)
        {
            TriggerItem asso = cbbAttatchedTrg.SelectedItem as TriggerItem;
            OnTraceFired(asso);
        }

        private void btnNewTrigger_Click(object sender, EventArgs e)
        {
            TriggerItem newtrigger = Map.NewTrigger(out TagItem tag);
            CurrentTrigger = new TriggerItem(newtrigger);
            CurrentTag = new TagItem(tag);
            cbbAttatchedTrg.Items.Add(newtrigger);
            RefreshControl(new TagItem[] { tag });
            OnNewTriggerAdded(newtrigger);
        }

        private void btnDelTrigger_Click(object sender, EventArgs e)
        {
            IEnumerable<TagItem> tags = Map.Tags.GetTagFromTrigger(CurrentTrigger.ID);
            foreach (TagItem i in tags)
                Map.DelID(i.ID);
            Map.Triggers.RemoveTrigger(CurrentTrigger);
            foreach (var i in tags)
                Map.Tags.Remove(i, CurrentTrigger.ID);
            isControlRefreshing = true;
            cbbAttatchedTrg.Items.Remove(CurrentTrigger);
            isControlRefreshing = false;
            OnTriggerDeleted(CurrentTrigger);
        }

        private void btnCopyTrigger_Click(object sender, EventArgs e)
        {
            TriggerItem copytrigger = new TriggerItem(CurrentTrigger);
            copytrigger.ID = Map.NewID;
            copytrigger.Name += " Clone";
            copytrigger.SetDisplayingString(lbxTriggerList.Tag.ToString());
            TagItem newtag = new TagItem(copytrigger, Map.NewID);
            Map.Triggers[copytrigger.ID] = copytrigger;
            Map.Tags[newtag.ID] = newtag;
            Reload(copytrigger, new TagItem[] { newtag });
            cbbAttatchedTrg.Items.Add(CurrentTrigger);
            OnNewTriggerAdded(CurrentTrigger);
        }

        private void cbbTagID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isControlRefreshing)
            {
                CurrentTag = cbbTagID.SelectedItem as TagItem;
                txbTagName.Text = CurrentTag.Name;
            }
        }
    }
}
