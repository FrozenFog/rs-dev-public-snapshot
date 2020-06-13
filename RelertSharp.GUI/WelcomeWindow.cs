using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RelertSharp.Common;
using static RelertSharp.Common.GlobalVar;

namespace RelertSharp.GUI
{
    public partial class WelcomeWindow : Form
    {
        internal bool Reboot = false;


        public WelcomeWindow()
        {
            InitializeComponent();
            SetLanguage();
            SetControl();
        }


        private void SetLanguage()
        {
            foreach (Control c in Controls) Language.SetControlLanguage(c);
            Text = Language.DICT[Text] + Constant.ReleaseDate;
        }
        private void SetControl(bool reboot = false)
        {
            if (reboot)
            {
                rtxbDetail.Text = Language.DICT["RSWReboot"];
                btnLoadMap.Text = "Reboot";
                Reboot = true;
            }
            else
            {
                rtxbDetail.Text = string.Format(Language.DICT["RSWContent"], GlobalConfig.ConfigName, GlobalConfig.ConfigVersion, GlobalConfig.GamePath, CurrentLanguage, GlobalDir.Count);
            }
        }
        private void btnLoadMap_Click(object sender, EventArgs e)
        {
            if (Reboot)
            {
                Close();
            }
            else
            {
                OpenFileDialog dlg = new OpenFileDialog()
                {
                    Title = Language.DICT["OpenMapDlgTitle"],
                    InitialDirectory = string.IsNullOrEmpty(GlobalConfig.Local.RecentPath) ? Application.StartupPath : GlobalConfig.Local.RecentPath,
                    Filter = "Red Alert 2 Map File|*.map;*.yrm;*.mpr",
                    AddExtension = true,
                    CheckFileExists = true,
                };
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    MapName = dlg.FileName;
                    GlobalConfig.Local.RecentPath = MapName.Substring(0, MapName.Length - MapName.LastIndexOf('\\'));
                    Close();
                }
                else
                {
                    DialogResult = DialogResult.Cancel;
                }
            }
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnChangeCfg_Click(object sender, EventArgs e)
        {
            LocalSettingWindow setting = new LocalSettingWindow();
            LocalConfig cfg = new LocalConfig("local.rsc");
            setting.Reload(cfg);
            if (setting.ShowDialog() == DialogResult.OK)
            {
                GlobalConfig.Local = cfg;
                SetControl(true);
            }
        }
        public string MapName { get; private set; }
    }
}
