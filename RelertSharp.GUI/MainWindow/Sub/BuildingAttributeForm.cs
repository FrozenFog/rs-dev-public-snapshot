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
    internal partial class BuildingAttributeForm : BaseAttributeForm
    {
        private StructureItem budhost, original;
        private bool confirmed = false;


        public BuildingAttributeForm(StructureItem src)
        {
            InitializeComponent();
            LoadCombobox(GlobalVar.CurrentMapDocument.Map.Houses, GlobalVar.CurrentMapDocument.Map.Tags);
            LoadUpgrades(src.RegName);
            LoadSpotlight();
            original = new StructureItem(src);
            budhost = src;
            changer.Host = budhost;
            UpdateGuiFromHost();
            SetLanguage();
            initialized = true;
        }

        private void LoadSpotlight()
        {
            cbbSpotlight.Items.Add(BuildingSpotlightType.None);
            cbbSpotlight.Items.Add(BuildingSpotlightType.Arcing);
            cbbSpotlight.Items.Add(BuildingSpotlightType.Circular);
            cbbSpotlight.DropDownWidth = 55;
        }
        private void LoadUpgrades(string regname)
        {
            IEnumerable<string> src = GlobalVar.GlobalRules.GetBuildingUpgradeList(regname);
            if (src != null)
            {
                int num = src.Count();
                InsertToUpgradeComboBox(src, cbbUpg1);
                InsertToUpgradeComboBox(src, cbbUpg2);
                InsertToUpgradeComboBox(src, cbbUpg3);
                cbbUpg1.Enabled = true;
                cbbUpg2.Enabled = num >= 2;
                cbbUpg3.Enabled = num == 3;
            }
        }
        private void InsertToUpgradeComboBox(IEnumerable<string> src, ComboBox dest)
        {
            dest.Enabled = false;
            dest.Items.Clear();
            dest.Items.Add("None");
            dest.Items.AddRange(src.ToArray());
            dest.DropDownWidth = src.Max(x => x.Length) * 7;
        }



        public void Reload(ICombatObject src)
        {
            initialized = false;
            confirmed = false;
            StructureItem newitem = src as StructureItem;
            Result = null;
            original = new StructureItem(newitem);
            budhost = newitem;
            changer.Host = budhost;
            LoadUpgrades(src.RegName);
            UpdateGuiFromHost();
            initialized = true;
        }
        protected override void UpdateGuiFromHost()
        {
            ckbRebuild.Checked = budhost.AIRebuildable;
            ckbSellable.Checked = budhost.AISellable;
            ckbRepair.Checked = budhost.AIRepairable;
            ckbRebuild.Checked = budhost.AIRebuildable;
            ckbShowName.Checked = budhost.Nominal;
            ckbPowered.Checked = budhost.BuildingOnline;
            lblUnitID.Text = string.Format("Building ID : {0}", budhost.ID);
            lblUnitRegName.Text = string.Format("Building Registion Name : {0}", budhost.RegName);
            lklTrace.Enabled = budhost.TaggedTrigger != "None";
            cbbUpg1.Text = budhost.Upgrade1;
            cbbUpg2.Text = budhost.Upgrade2;
            cbbUpg3.Text = budhost.Upgrade3;
            cbbSpotlight.SelectedIndex = (int)budhost.SpotlightType;
            mtxbUpCount.Text = budhost.UpgradeNum.ToString();
            base.UpdateGuiFromHost();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            confirmed = true;
        }

        private void UnitAttributeForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (confirmed)
            {
                Result = budhost;
                Result.ApplyAttributeFrom(changer);
            }
            else
            {
                Result = original;
            }
        }

        private void cbbUpg1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (initialized)
            {
                budhost.Upgrade1 = cbbUpg1.Text;
            }
        }

        private void cbbUpg2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (initialized)
            {
                budhost.Upgrade2 = cbbUpg2.Text;
            }
        }

        private void cbbUpg3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (initialized)
            {
                budhost.Upgrade3 = cbbUpg3.Text;
            }
        }

        private void mtxbUpCount_Validated(object sender, EventArgs e)
        {
            try
            {
                int num = int.Parse(mtxbUpCount.Text);
                if (num < 0 || num > 3) budhost.UpgradeNum = 0;
                else budhost.UpgradeNum = num;
            }
            catch
            {
                mtxbUpCount.Text = "";
            }
        }

        private void buildingCkbSetHandler(object sender, EventArgs e)
        {
            if (initialized)
            {
                budhost.AISellable = ckbSellable.Checked;
                budhost.AIRepairable = ckbRepair.Checked;
                budhost.BuildingOnline = ckbPowered.Checked;
            }
        }

        private void cbbSpotlight_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (initialized)
            {
                budhost.SpotlightType = (BuildingSpotlightType)cbbSpotlight.SelectedItem;
            }
        }

        public StructureItem Result { get; private set; }
    }
}
