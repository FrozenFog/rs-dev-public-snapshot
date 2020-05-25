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
    internal partial class AircraftAttributeForm : BaseAttributeForm
    {
        private AircraftItem airhost, original;
        private bool confirmed = false;


        public AircraftAttributeForm(AircraftItem src)
        {
            InitializeComponent();
            LoadCombobox(GlobalVar.CurrentMapDocument.Map.Houses, GlobalVar.CurrentMapDocument.Map.Tags);
            original = new AircraftItem(src);
            airhost = src;
            changer.Host = airhost;
            UpdateGuiFromHost();
            SetLanguage();
            initialized = true;
        }

        public void Reload(ICombatObject src)
        {
            initialized = false;
            confirmed = false;
            AircraftItem newitem = src as AircraftItem;
            Result = null;
            original = new AircraftItem(newitem);
            airhost = newitem;
            changer.Host = airhost;
            UpdateGuiFromHost();
            initialized = true;
        }
        protected override void UpdateGuiFromHost()
        {
            ckbRecruit.Checked = airhost.AutoNORecruitType;
            ckbRebuild.Checked = airhost.AutoYESRecruitType;
            lblUnitID.Text = string.Format("Aircraft ID : {0}", airhost.ID);
            lblUnitRegName.Text = string.Format("Aircraft Registion Name : {0}", airhost.RegName);
            lklTrace.Enabled = airhost.TaggedTrigger != "None";
            base.UpdateGuiFromHost();
        }

        private void ckbRecruit_CheckedChanged(object sender, EventArgs e)
        {
            if (initialized)
            {
                airhost.AutoNORecruitType = ckbRecruit.Checked;
            }
        }

        private void ckbRebuild_CheckedChanged(object sender, EventArgs e)
        {
            if (initialized)
            {
                airhost.AutoYESRecruitType = ckbRebuild.Checked;
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
                Result = airhost;
                Result.ApplyAttributeFrom(changer);
            }
            else
            {
                Result = original;
            }
        }

        public AircraftItem Result { get; private set; }
    }
}
