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
using RelertSharp.IniSystem;
using RelertSharp.MapStructure;
using static RelertSharp.GUI.GuiUtils;
using static RelertSharp.Language;

namespace RelertSharp.GUI.SubWindows.LogicEditor
{
    internal partial class PanelGlobalSearch : UserControl
    {
        internal SearchCollection SearchResult { get; private set; } = new SearchCollection();
        private Map Map { get { return GlobalVar.CurrentMapDocument.Map; } }
        public PanelGlobalSearch()
        {
            InitializeComponent();
        }
        public void Initialize()
        {
            foreach (Control c in Controls) c.SetLanguage();
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txbSearchName.Text;
            lvSearchResult.Items.Clear();
            if (keyword == DICT["LGClblFakeSearch"]) return;
            SearchResult.SetKeyword(keyword);

            if (ckbTrigger.Checked) LoadSearch(Map.Triggers, SearchItem.SearchType.LGCckbTrig);
            if (ckbTag.Checked) LoadSearch(Map.Tags, SearchItem.SearchType.LGCckbTag);
            if (ckbLocal.Checked) LoadSearch(Map.LocalVariables, SearchItem.SearchType.LGCckbLocal);
            if (ckbTeam.Checked) LoadSearch(Map.Teams, SearchItem.SearchType.LGCckbTeam);
            if (ckbTaskForce.Checked) LoadSearch(Map.TaskForces, SearchItem.SearchType.LGCckbTF);
            if (ckbScript.Checked) LoadSearch(Map.Scripts, SearchItem.SearchType.LGCckbTScp);
            if (ckbAiTrigger.Checked) LoadSearch(Map.AiTriggers, SearchItem.SearchType.LGCckbAiTrg);
            if (ckbHouse.Checked) LoadSearch(Map.Countries, SearchItem.SearchType.LGCckbHouse);
            if (ckbCsf.Checked) LoadSearch(GlobalVar.GlobalCsf, SearchItem.SearchType.LGCckbCsf);
            if (ckbTechno.Checked) LoadSearch(GlobalVar.GlobalRules.TechnoList, SearchItem.SearchType.LGCckbTechno);
            if (ckbSound.Checked) LoadSearch(GlobalVar.GlobalSound.SoundList, SearchItem.SearchType.LGCckbSnd);
            if (ckbEva.Checked) LoadSearch(GlobalVar.GlobalSound.EvaList, SearchItem.SearchType.LGCckbEva);
            if (ckbTheme.Checked) LoadSearch(GlobalVar.GlobalSound.ThemeList, SearchItem.SearchType.LGCckbMus);
            if (ckbAnim.Checked) LoadSearch(GlobalVar.GlobalRules.AnimationList, SearchItem.SearchType.LGCckbAnim);
            if (ckbSuper.Checked) LoadSearch(GlobalVar.GlobalRules.SuperWeaponList, SearchItem.SearchType.LGCckbSuper);
            if (ckbGlobal.Checked) LoadSearch(GlobalVar.GlobalRules.GlobalVar, SearchItem.SearchType.LGCckbGlobal);
            lblSearchResult.Text = string.Format(DICT["LGClblSearchNum"], SearchResult.Length);
        }
        private void LoadSearch(IEnumerable<ILogicItem> src, SearchItem.SearchType type)
        {
            lvSearchResult.LoadAs(SearchResult.SearchIn(src, type));
        }
        private void txbSearchName_KeyDown(object sender, KeyEventArgs e)
        {
            GoEnter(e, () => { btnSearch_Click(null, null); });
        }

        private void txbSearchName_Enter(object sender, EventArgs e)
        {
            if (txbSearchName.Text == DICT["LGClblFakeSearch"])
            {
                txbSearchName.Text = "";
                txbSearchName.ForeColor = Color.Black;
            }
        }

        private void txbSearchName_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txbSearchName.Text))
            {
                txbSearchName.Text = DICT["LGClblFakeSearch"];
                txbSearchName.ForeColor = SystemColors.GrayText;
            }
        }
        private void lvSearchResult_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            lvSearchResult.BeginUpdate();
            lvSearchResult.Items.Clear();
            lvSearchResult.Items.AddRange(SearchResult.SortBy(e.Column));
            lvSearchResult.EndUpdate();
        }
    }
}
