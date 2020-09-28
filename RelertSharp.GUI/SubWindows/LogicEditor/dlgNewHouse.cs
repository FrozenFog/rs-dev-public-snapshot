using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RelertSharp.MapStructure;
using RelertSharp.MapStructure.Logic;
using RelertSharp.Common;
using RelertSharp.IniSystem;
using static RelertSharp.Language;
using static RelertSharp.GUI.GuiUtils;

namespace RelertSharp.GUI.SubWindows.LogicEditor
{
    public partial class dlgNewHouse : Form
    {
        public dlgNewHouse()
        {
            InitializeComponent();
            Text = DICT["LGCdlgNewHouse"];
            label1.Text = DICT[label1.Text];
            label2.Text = DICT[label2.Text];
            btnHouseNewConfirm.Text = DICT[btnHouseNewConfirm.Text];
            List<string> stdCountries = new List<string>();
            foreach (INIPair pair in GlobalVar.GlobalRules["Countries"]) stdCountries.Add(pair.Value);
            cbbParent.LoadAs(stdCountries);
        }

        private void btnHouseNewConfirm_Click(object sender, EventArgs e)
        {
            string Name = txbName.Text;
            if (string.IsNullOrWhiteSpace(Name)) return;
            if (GlobalVar.CurrentMapDocument.Map.Countries.GetCountry(Name) != null)
            {
                MessageBox.Show(DICT["LGCbtnHouseNewMsgboxMain"], DICT["LGCbtnHouseNewMsgboxTitle"], MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Name = Name.Replace(' ', '_');
            CountryItem newCountry = new CountryItem();
            newCountry.Name = Name;
            newCountry.ID = Name;
            newCountry.ParentCountryName = cbbParent.Text;
            newCountry.SmartAI = Utils.Misc.ParseBool(GlobalVar.GlobalRules[cbbParent.Text]["SmartAI"]);
            newCountry.Side = GlobalVar.GlobalRules[cbbParent.Text]["Side"];
            newCountry.Suffix = GlobalVar.GlobalRules[cbbParent.Text]["Suffix"];
            newCountry.Prefix = GlobalVar.GlobalRules[cbbParent.Text]["Prefix"];
            newCountry.ColorName = GlobalVar.GlobalRules[cbbParent.Text]["Color"];
            newCountry.Index = GlobalVar.CurrentMapDocument.Map.Countries.NewIndex().ToString();

            HouseItem newHouse = new HouseItem();
            newHouse.Name = Name + " House";
            newHouse.IQ = 5;
            newHouse.Edge = HouseEdges.North;
            newHouse.ColorName = newCountry.ColorName;
            newHouse.AlliesWith = new List<string>();
            newHouse.AlliesWith.Add(newHouse.Name);
            newHouse.Country = Name;
            newHouse.Credits = 0;
            newHouse.TechLevel = 10;
            newHouse.PlayerControl = false;
            newHouse.PercentBuilt = 100D;
            newHouse.NodeCounts = 0;
            newHouse.GetToUnit = new HouseUnit(newHouse);
            newHouse.Index = GlobalVar.CurrentMapDocument.Map.Houses.NewIndex().ToString();

            GlobalVar.CurrentMapDocument.Map.Countries[Name] = newCountry;
            GlobalVar.CurrentMapDocument.Map.Houses[newHouse.Name] = newHouse;


            DialogResult = DialogResult.OK;
            LogicEditor.retHouse = newHouse;
            Close();
        }
    }
}
