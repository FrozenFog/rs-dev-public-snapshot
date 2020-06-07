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
                btnLoadMap.Enabled = false;
            }
            else
            {
                rtxbDetail.Text = string.Format(Language.DICT["RSWContent"], GlobalConfig.ConfigName, GlobalConfig.ConfigVersion, GlobalConfig.GamePath, CurrentLanguage, GlobalDir.Count);
            }
        }

        private void btnLoadMap_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog()
            {
                Title = Language.DICT["OpenMapDlgTitle"],
                InitialDirectory = Application.StartupPath,
                Filter = "Red Alert 2 Map File|*.map;*.yrm;*.mpr",
                AddExtension = true,
                CheckFileExists = true,
            };
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                MapName = dlg.FileName;
                Close();
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
                GlobalConfig.Override(cfg);
                cfg.SaveConfig();
                SetControl(true);
            }
        }
        public string MapName { get; private set; }
    }
}
