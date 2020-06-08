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
using static RelertSharp.GUI.GuiUtils;

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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (!Initialization()) return;
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
                    Fatal("Initialization failed!\nTrace:\n" + e.StackTrace);
                    return;
                }
                SafeRun(() =>
                {
                    string name;
                    if (args.Count() == 0)
                    {
                        WelcomeWindow welcome = new WelcomeWindow();
                        Application.Run(welcome);
                        if (welcome.DialogResult == DialogResult.OK)
                        {
                            name = welcome.MapName;
                        }
                        else return;
                    }
                    else name = args[0];
                    MapFile map = new MapFile(name);
                    GlobalVar.CurrentMapDocument = map;
                    Application.Run(new MainWindowTest());
                },
                "Unhandled error!");
            }
#endif
        }
        static bool Initialization()
        {
            if (!SafeRun(() => { Utils.Misc.Init_Language(); }, "Failed to read language file!")) return false;
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
                if (localSetting.DialogResult != DialogResult.OK) return false;
                cfgLocal.SaveConfig();
            }
            LocalConfig local = new LocalConfig("local.rsc");
            GlobalVar.GlobalConfig = new RSConfig(local.PrimaryConfigName);
            GlobalVar.GlobalConfig.Merge(local);
            if (!SafeRun(() => { GlobalVar.GlobalDir = new VirtualDir(); },
                "Virtual mix directiory initialization failed!")) return false;
            if (!SafeRun(() => { GlobalVar.GlobalRules = new Rules(GlobalVar.GlobalDir.GetRawByte(GlobalVar.GlobalConfig.RulesName + ".ini"), GlobalVar.GlobalConfig.RulesName + ".ini"); },
                "Rules not found or corrupted!")) return false;
            if (!SafeRun(() => { GlobalVar.GlobalRules.LoadArt(GlobalVar.GlobalDir.GetFile(GlobalVar.GlobalConfig.ArtName, FileExtension.INI)); },
                "Art not found or corrupted!")) return false;
            if (!SafeRun(() =>
            {
                GlobalVar.GlobalSound = new SoundRules(GlobalVar.GlobalConfig.SoundName, GlobalVar.GlobalConfig.EvaName, GlobalVar.GlobalConfig.ThemeName);
                GlobalVar.GlobalSoundBank = new SoundBank(GlobalVar.GlobalConfig.BagNameList);
            },
            "Sound library initialization failed!")) return false;

            //csf
            if (!SafeRun(() =>
            {
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
            },
            "Csf library initialization failed!")) return false;
            return true;
        }
    }
}
