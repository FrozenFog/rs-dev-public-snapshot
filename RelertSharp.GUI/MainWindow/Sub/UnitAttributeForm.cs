﻿using System;
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
    internal partial class UnitAttributeForm : BaseAttributeForm
    {
        private UnitItem unithost;
        private bool confirmed = false;


        public UnitAttributeForm(UnitItem src)
        {
            InitializeComponent();
            LoadCombobox(GlobalVar.CurrentMapDocument.Map.Houses, GlobalVar.CurrentMapDocument.Map.Tags);
            unithost = new UnitItem(src);
            changer.Host = unithost;
            UpdateGuiFromHost();
            SetLanguage();
            initialized = true;
        }

        public void Reload(ICombatObject src)
        {
            initialized = false;
            confirmed = false;
            UnitItem newitem = src as UnitItem;
            Result = null;
            unithost = new UnitItem(newitem);
            changer.Host = unithost;
            UpdateGuiFromHost();
            initialized = true;
        }
        protected override void UpdateGuiFromHost()
        {
            txbFollows.Text = unithost.FollowsIndex;
            ckbOnBridge.Checked = unithost.IsAboveGround;
            ckbRecruit.Checked = unithost.AutoNORecruitType;
            ckbRebuild.Checked = unithost.AutoYESRecruitType;
            lblUnitID.Text = string.Format("Unit ID : {0}", unithost.ID);
            lblUnitRegName.Text = string.Format("Unit Registion Name : {0}", unithost.RegName);
            lklTrace.Enabled = unithost.TaggedTrigger != "None";
            base.UpdateGuiFromHost();
        }

        private void txbFollows_Validated(object sender, EventArgs e)
        {
            if (initialized)
            {
                unithost.FollowsIndex = txbFollows.Text;
            }
        }

        private void ckbOnBridge_CheckedChanged(object sender, EventArgs e)
        {
            if (initialized)
            {
                unithost.IsAboveGround = ckbOnBridge.Checked;
            }
        }

        private void ckbRecruit_CheckedChanged(object sender, EventArgs e)
        {
            if (initialized)
            {
                unithost.AutoNORecruitType = ckbRecruit.Checked;
            }
        }

        private void ckbRebuild_CheckedChanged(object sender, EventArgs e)
        {
            if (initialized)
            {
                unithost.AutoYESRecruitType = ckbRebuild.Checked;
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
                Result = unithost;
                Result.ApplyAttributeFrom(changer);
            }
        }

        public UnitItem Result { get; private set; }
    }
}