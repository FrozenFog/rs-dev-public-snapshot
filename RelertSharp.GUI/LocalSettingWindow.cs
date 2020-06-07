using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using RelertSharp.Common;

namespace RelertSharp.GUI
{
    public partial class LocalSettingWindow : Form
    {
        private LocalConfig cfg;
        private bool validConfig, validPath;

        public LocalSettingWindow()
        {
            InitializeComponent();
            SetLanguage();
        }


        public void Reload(LocalConfig config)
        {
            cfg = config;
            txbGamePath.Text = cfg.GamePath;
            txbCfgPath.Text = cfg.PrimaryConfigName;
            validPath = Directory.Exists(cfg.GamePath);
            CheckValidConfig(cfg.PrimaryConfigName);
            btnAccept.Enabled = validPath && validConfig;
        }


        private void SetLanguage()
        {
            foreach (Control c in Controls) Language.SetControlLanguage(c);
            Text = Language.DICT[Text];
        }
        private void CheckValidConfig(string configPath)
        {
            try
            {
                RSConfig c = new RSConfig(configPath);
                if (string.IsNullOrEmpty(c.ConfigName) || string.IsNullOrEmpty(c.ConfigVersion)) throw new Exception();
                rtxbDetail.Text = string.Format(Language.DICT["LcfgDetail"], c.ConfigName, c.ConfigVersion);
                txbCfgPath.Text = configPath;
                validConfig = true;
                ConfigPath = configPath;
            }
            catch
            {
                validConfig = false;
            }
        }
        private void btnGamePath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog()
            {
                Description = Language.DICT["LcfgGPathTitle"],
                RootFolder = Environment.SpecialFolder.MyComputer,
            };
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txbGamePath.Text = dlg.SelectedPath;
                cfg.GamePath = dlg.SelectedPath;
                validPath = Directory.Exists(dlg.SelectedPath);
                btnAccept.Enabled = validPath && validConfig;
            }
        }

        private void btnCfgPath_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog()
            {
                Title = Language.DICT["LcfgCPathTitle"],
                Multiselect = false,
                InitialDirectory = Application.StartupPath,
                CheckFileExists = true,
                CheckPathExists = true,
                Filter = "Relert Sharp Config-file|*.rsc|All files|*.*"
            };
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                CheckValidConfig(dlg.FileName);
                if (!validConfig) MessageBox.Show(Language.DICT["LcfgNa"], Language.DICT["RSError"], MessageBoxButtons.OK, MessageBoxIcon.Error);
                cfg.PrimaryConfigName = dlg.FileName;
                btnAccept.Enabled = validPath && validConfig;
            }
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            Close();
        }

        public string ConfigPath { get; private set; }
    }
}
