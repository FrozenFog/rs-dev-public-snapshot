using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using RelertSharp.FileSystem;
using RelertSharp.IniSystem;
using RelertSharp.Common;
using RelertSharp.GUI.SubWindows.LogicEditor;

namespace RelertSharp.GUI
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
#if DEBUG
            Initialization();
            _Test.Run();
#else
            if (args.Length < 1)
            {
                try
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    if (!Initialization())
                    {
                        return;
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("Initialization failed!\nTrace:\n" + e.StackTrace, "RelertSharp", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                try
                {
                    string name;
                    if (args.Count() == 0)
                    {
                        OpenFileDialog dlg = new OpenFileDialog()
                        {
                            Title = Language.DICT["OpenMapDlgTitle"],
                            InitialDirectory = Application.StartupPath,
                            Filter = "Red Alert 2 Map File|*.map;*.yrm;*.mpr",
                            AddExtension = true,
                            CheckFileExists = true,
                        };
                        if (dlg.ShowDialog() == DialogResult.OK) name = dlg.FileName;
                        else return;
                    }
                    else name = args[0];
                    MapFile map = new MapFile(name);
                    GlobalVar.CurrentMapDocument = map;
                    Application.Run(new MainWindowTest());
                }
                catch (Exception e)
                {
                    MessageBox.Show("Unhandled error!\nTrace:\n" + e.StackTrace, "Fatal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
#endif
        }
        static bool Initialization()
        {
            try
            {
                Utils.Misc.Init_Language();
            }
            catch
            {
                MessageBox.Show("Failed to read language file!", "Fatal", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return SetGlobalVar();
        }
        static bool SetGlobalVar()
        {
            bool isNewCfg = false;
            LocalConfig cfgLocal;
            LocalSettingWindow localSetting = new LocalSettingWindow();
            if (!File.Exists("local.rsc"))
            {
                isNewCfg = true;
                cfgLocal = new LocalConfig();
            }
            else
            {
                cfgLocal = new LocalConfig("local.rsc");
            }
            if (!cfgLocal.IsValid)
            {
                localSetting.Reload(cfgLocal);
                Application.Run(localSetting);
                if (localSetting.DialogResult == DialogResult.Cancel) return false;
                cfgLocal.SaveConfig();
            }
            LocalConfig local = new LocalConfig("local.rsc");
            GlobalVar.GlobalConfig = new RSConfig(local.PrimaryConfigName);
            GlobalVar.GlobalConfig.Merge(local);
            GlobalVar.GlobalDir = new VirtualDir();
            GlobalVar.GlobalRules = new Rules(GlobalVar.GlobalDir.GetRawByte(GlobalVar.GlobalConfig.RulesName + ".ini"), GlobalVar.GlobalConfig.RulesName + ".ini");
            GlobalVar.GlobalRules.LoadArt(GlobalVar.GlobalDir.GetFile(GlobalVar.GlobalConfig.ArtName, FileExtension.INI));
            GlobalVar.GlobalSound = new SoundRules(GlobalVar.GlobalConfig.SoundName, GlobalVar.GlobalConfig.EvaName, GlobalVar.GlobalConfig.ThemeName);
            GlobalVar.GlobalSoundBank = new SoundBank(GlobalVar.GlobalConfig.BagNameList);

            //csf
            if (GlobalVar.GlobalConfig.StringtableList.Count > 0)
            {
                GlobalVar.GlobalCsf = GlobalVar.GlobalDir.GetFile(GlobalVar.GlobalConfig.StringtableList[0], FileExtension.CSF);
                if (GlobalVar.GlobalConfig.StringtableList.Count > 1)
                {
                    foreach (string name in GlobalVar.GlobalConfig.StringtableList.Skip(1))
                    {
                        GlobalVar.GlobalCsf.AddCsfLib(GlobalVar.GlobalDir.GetFile(name, FileExtension.CSF));
                    }
                }
                GlobalVar.GlobalCsf.ToTechno();
            }
            else GlobalVar.GlobalCsf = new CsfFile();
            return true;
        }
    }
}
