using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using RelertSharp.FileSystem;
using RelertSharp.IniSystem;
using RelertSharp.Common;
using RelertSharp.GUI.SubWindows.LogicEditor;
using System.Runtime.InteropServices;
using static RelertSharp.GUI.GuiUtils;

namespace RelertSharp.GUI
{
    static class Program
    {
        private static RsLog Log { get { return GlobalVar.Log; } }
        [DllImport("kernel32.dll")]
        static extern bool SetDllDirectory(string pathname);

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            SetDllDirectory(Application.StartupPath);
            GlobalVar.Log = new RsLog();
            if (!File.Exists("CncVxlRenderText.dll"))
            {
                Fatal("Cannot find Render Dll, please check your folder.");
                return;
            }
            else
            {
                try
                {
                    DrawingEngine.Engine.FirstRun();
                }
                catch
                {
                    Log.Write("Engine Catched");
                }
            }
            if (args.Length < 1)
            {
                Process[] ps = Process.GetProcesses();
                if (ps.Count(x=>x.ProcessName == "RelertSharp") > 1)
                {
                    Fatal("May only run 1 process.");
                    return;
                }
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
                    GlobalVar.GlobalRules.Override(map.Map.IniResidue.Values);
                    GlobalVar.CurrentMapDocument = map;
                    if (Log.HasCritical)
                    {
                        Log.Critical("These object will not show in scene(or in logic editor)");
                        Warning(Log.ShowCritical());
                    }
                    Application.Run(new MainWindowTest());
                },
                "Unhandled error!");
            }
            GlobalVar.GlobalConfig.Local.SaveConfig();
            Log.Write("PROGRAM EXITING\n\n\n");
            Log.Dispose();
        }



        static bool Initialization()
        {
            if (!SafeRun(() => { Utils.Misc.Init_Language(); }, "Failed to read language file!")) return false;
            return SetGlobalVar();
        }
        static bool SetGlobalVar()
        {
            LocalConfig cfgLocal;
            LocalSettingWindow localSetting = new LocalSettingWindow();
            if (!File.Exists("local.rsc"))
            {
                Log.Write("Local config not found, creating new one");
                cfgLocal = new LocalConfig();
            }
            else
            {
                cfgLocal = new LocalConfig("local.rsc");
                Log.Write("Local config loaded");
            }
            if (!cfgLocal.IsValid)
            {
                localSetting.Reload(cfgLocal);
                Application.Run(localSetting);
                if (localSetting.DialogResult != DialogResult.OK) return false;
                cfgLocal.SaveConfig();
            }
            localSetting.Dispose();
            LocalConfig local = new LocalConfig("local.rsc");
            GlobalVar.GlobalConfig = new RSConfig(local.PrimaryConfigName)
            {
                Local = local
            };
            Log.Write("Primary config loaded");
            if (!SafeRun(() => { GlobalVar.GlobalDir = new VirtualDir(); },
                "Virtual mix directiory initialization failed!")) return false;
            Log.Write("Loading Rules");
            if (!SafeRun(() => { GlobalVar.GlobalRules = new Rules(GlobalVar.GlobalDir.GetRawByte(GlobalVar.GlobalConfig.RulesName + ".ini", true), GlobalVar.GlobalConfig.RulesName + ".ini"); },
                "Rules not found or corrupted!")) return false;
            Log.Write("Loading Art");
            if (!SafeRun(() => { GlobalVar.GlobalRules.LoadArt(GlobalVar.GlobalDir.GetFile(GlobalVar.GlobalConfig.ArtName, FileExtension.INI, true)); },
                "Art not found or corrupted!")) return false;
            Log.Write("Loading Sound");
            if (!SafeRun(() =>
            {
                GlobalVar.GlobalSound = new SoundRules(GlobalVar.GlobalConfig.SoundName, GlobalVar.GlobalConfig.EvaName, GlobalVar.GlobalConfig.ThemeName);
                GlobalVar.GlobalSoundBank = new SoundBank(GlobalVar.GlobalConfig.BagNameList);
            },
            "Sound library initialization failed!")) return false;

            //csf
            Log.Write("Loading Csf");
            if (!SafeRun(() =>
            {
                LoadCsf();
            },
            "Csf library initialization failed!")) return false;
            Log.Write("INITIALIZATION COMPLETE\n\n\n");
            return true;
        }
        static void LoadCsf()
        {
            if (GlobalVar.GlobalConfig.StringtableList.Count > 0)
            {
                GlobalVar.GlobalCsf = GlobalVar.GlobalDir.GetFile(GlobalVar.GlobalConfig.StringtableList[0], FileExtension.CSF);
                if (GlobalVar.GlobalConfig.StringtableList.Count > 1)
                {
                    foreach (string name in GlobalVar.GlobalConfig.StringtableList.Skip(1))
                    {
                        if (name.EndsWith("##"))
                        {
                            for (int i = 0; i < 100; i++)
                            {
                                string sttname = string.Format("{0}{1:D2}.csf", name.Substring(0, name.Length - 2), i);
                                if (GlobalVar.GlobalDir.HasFile(sttname)) GlobalVar.GlobalCsf.AddCsfLib(GlobalVar.GlobalDir.GetFile(sttname, FileExtension.CSF));
                            }
                        }
                    }
                    GlobalVar.GlobalCsf.ToTechno();
                }
            }
            else GlobalVar.GlobalCsf = new CsfFile();
        }
    }
}
