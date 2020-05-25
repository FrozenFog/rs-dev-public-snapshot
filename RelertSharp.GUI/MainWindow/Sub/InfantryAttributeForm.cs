using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RelertSharp.MapStructure.Objects;
using RelertSharp.MapStructure.Logic;
using RelertSharp.Common;

namespace RelertSharp.GUI
{
    internal partial class InfantryAttributeForm : BaseAttributeForm
    {
        private InfantryItem infhost, original;
        private bool confirmed = false;


        public InfantryAttributeForm(InfantryItem src)
        {
            InitializeComponent();
            LoadCombobox(GlobalVar.CurrentMapDocument.Map.Houses, GlobalVar.CurrentMapDocument.Map.Tags);
            original = new InfantryItem(src);
            infhost = src;
            changer.Host = infhost;
            UpdateGuiFromHost();
            SetLanguage();
            initialized = true;
        }

        public void Reload(ICombatObject src)
        {
            initialized = false;
            confirmed = false;
            InfantryItem newitem = src as InfantryItem;
            Result = null;
            original = new InfantryItem(newitem);
            infhost = newitem;
            changer.Host = infhost;
            UpdateGuiFromHost();
            initialized = true;
        }
        protected override void UpdateGuiFromHost()
        {
            ckbOnBridge.Checked = infhost.IsAboveGround;
            ckbRecruit.Checked = infhost.AutoNORecruitType;
            ckbRebuild.Checked = infhost.AutoYESRecruitType;
            lblUnitID.Text = string.Format("Infantry ID : {0}", infhost.ID);
            lblUnitRegName.Text = string.Format("Infantry Registion Name : {0}", infhost.RegName);
            lklTrace.Enabled = infhost.TaggedTrigger != "None";
            base.UpdateGuiFromHost();
        }

        private void ckbOnBridge_CheckedChanged(object sender, EventArgs e)
        {
            if (initialized)
            {
                infhost.IsAboveGround = ckbOnBridge.Checked;
            }
        }

        private void ckbRecruit_CheckedChanged(object sender, EventArgs e)
        {
            if (initialized)
            {
                infhost.AutoNORecruitType = ckbRecruit.Checked;
            }
        }

        private void ckbRebuild_CheckedChanged(object sender, EventArgs e)
        {
            if (initialized)
            {
                infhost.AutoYESRecruitType = ckbRebuild.Checked;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            confirmed = true;
        }

        private void UnitAttributeForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (confirmed)
            {
                Result = infhost;
                Result.ApplyAttributeFrom(changer);
            }
            else
            {
                Result = original;
            }
        }

        public InfantryItem Result { get; private set; }
    }
}
