using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Diagnostics;
using RelertSharp.FileSystem;
using RelertSharp.IniSystem;
using RelertSharp.Common;
using RelertSharp.Wpf.Dialogs;
using System.Runtime.InteropServices;
using System.Threading;
using static RelertSharp.Wpf.GuiUtil;
using System.Text;
using RelertSharp.Common.Config;

namespace RelertSharp.Wpf
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        private static string _guid = "c0a76b5a-12ab-45c5-b9d9-d693faa6e7b9";
        private static RsLog Log { get { return GlobalVar.Log; } }
        private static string name = "areddawn.map";
        private static string StartupPath { get { return AppDomain.CurrentDomain.BaseDirectory; } }
        [DllImport("kernel32.dll")]
        static extern bool SetDllDirectory(string pathname);
        private bool initialized = false;
        private DlgInitialize init;

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            #region when complete
            //            #region Run
            //            if (NoWinXP()) return;
            //            SetDllDirectory(StartupPath);
            //            GlobalVar.Log = new RsLog();
            //            if (!File.Exists("CncVxlRenderText.dll"))
            //            {
            //                Fatal("Cannot find Render Dll, please check your folder.");
            //                return;
            //            }
            //            if (args.Length < 1)
            //            {
            //                using (Mutex mutex = new Mutex(false, _guid))
            //                {
            //                    if (!mutex.WaitOne(0, false))
            //                    {
            //                        Fatal("May only run 1 process.");
            //                        return;
            //                    }
            //#if RELEASE
            //                try
            //                {
            //#endif
            //                    if (!Initialization())
            //                    {
            //                        return;
            //                    }
            //#if RELEASE
            //                }
            //                catch (Exception e)
            //                {
            //                    Fatal("Initialization failed!\nTrace:\n" + e.StackTrace);
            //                    return;
            //                }
            //#endif
            //                    SafeRun(() =>
            //                    {
            //                        //string name;
            //                        //if (args.Count() == 0)
            //                        //{
            //                        //    WelcomeWindow welcome = new WelcomeWindow();
            //                        //    Application.Run(welcome);
            //                        //    if (welcome.DialogResult == DialogResult.OK)
            //                        //    {
            //                        //        if (welcome.Reboot)
            //                        //        {
            //                        //            Process.Start("rsdata.exe", "/reboot");
            //                        //            return;
            //                        //        }
            //                        //        else
            //                        //        {
            //                        //            name = welcome.MapName;
            //                        //        }
            //                        //    }
            //                        //    else return;
            //                        //}
            //                        //else name = args[0];
            //                        MapFile map = new MapFile(name);
            //                        GlobalVar.GlobalRules.MapIniData = map.Map.IniResidue;
            //                        GlobalVar.CurrentMapDocument = map;
            //                        GlobalVar.GlobalMap.DumpStructure();
            //                        if (Log.HasCritical)
            //                        {
            //                            Log.Critical("These object will not show in scene(or in logic editor)");
            //                            Warning(Log.ShowCritical());
            //                        }
            //                        Application.Run(new MainWindowTest());
            //                    },
            //                    "Unhandled error!",
            //                    () =>
            //                    {
            //                        GlobalVar.CurrentMapDocument.SaveMap("failsafe.map");
            //                    });
            //                    GlobalVar.GlobalConfig.Local.SaveConfig();
            //                    Log.Write("PROGRAM EXITING\n\n\n");
            //                    Log.Dispose();
            //                }
            //            }
            //            #endregion
            #endregion
            App application = new App()
            {
                ShutdownMode = ShutdownMode.OnExplicitShutdown
            };
            application.InitializeComponent();
            application.Run();
            Finalization();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            bool canStart = Start();
            if (canStart)
            {
                MainWindow mw = new MainWindow();
                Application.Current.MainWindow = mw;
                mw.Show();
            }
            base.OnStartup(e);
            if (!canStart) Shutdown();
        }
        private bool Start()
        {
            using (Mutex mutex = new Mutex(false, _guid))
            {
                if (!mutex.WaitOne(0, false))
                {
                    Fatal("May only run 1 process.");
                    return false;
                }
            }
            if (!File.Exists("CncVxlRenderText.dll"))
            {
                Fatal("Cannot find Render Dll, please check your folder.");
                return false;
            }
            if (Validate(out bool setupMw) && NoWinXP())
            {
                init = new DlgInitialize();
                Initialization();
                init.CallShowDlg();
                if (!initialized)
                {
                    return false;
                }
                return true;
            }
            return false;
        }
        private static void Finalization()
        {
            GlobalVar.GlobalConfig?.UserConfig?.Save();
        }
        #region Validate
        private bool Validate(out bool setupMw)
        {
            setupMw = false;
#if DEBUG
            File.Delete(Constant.Config.Path);
#endif
            try
            {
                GlobalVar.Language = new RelertSharp.Common.Language();
            }
            catch
            {
                GuiUtil.Fatal("Can't find language.json!\nMust have at least one language file.");
                return false;
            }
            try
            {
                GlobalVar.GlobalConfig = new RsConfig();
                Log.Write("Primary config loaded");
                return true;
            }
            catch
            {
                DlgConfig config = new DlgConfig();
                if (config.ShowDialog().Value)
                {
                    setupMw = true;
                    return true;
                }
                return false;
            }
        }
        #endregion
        #region Initialization
        private async Task Initialization()
        {
            await Task.Run(() =>
            {
                init.SetStatus("Initializing language");
                SafeRun(() => { Utils.Misc.Init_Language(); }, "Failed to read language file!");
                initialized = SetGlobalVar();
                init.SetStatus("Initialization Complete.");
                Thread.Sleep(1000);
                init.CallClose();
            });
        }
        private bool SetGlobalVar()
        {
            var general = GlobalVar.GlobalConfig.ModConfig.General;
            init.SetStatus("Reading Mix...");
            if (!SafeRun(() => { GlobalVar.GlobalDir = new VirtualDir(); },
                "Virtual mix directiory initialization failed!")) return false;
            Log.Write("Loading Rules");
            init.SetStatus("Reading Rules and Art");
            if (!SafeRun(() => { GlobalVar.GlobalRules = new Rules(GlobalVar.GlobalDir.GetRawByte(general.IniFiles.Rules + Constant.EX_INI, true), general.IniFiles.Rules + Constant.EX_INI); },
                "Rules not found or corrupted!")) return false;
            Log.Write("Loading Art");
            if (!SafeRun(() => { GlobalVar.GlobalRules.LoadArt(GlobalVar.GlobalDir.GetFile(general.IniFiles.Art, FileExtension.INI, true)); },
                "Art not found or corrupted!")) return false;
            Log.Write("Loading Sound");
            init.SetStatus("Loading sound bag file");
            if (!SafeRun(() =>
            {
                GlobalVar.GlobalSound = new SoundRules(general.IniFiles.Sound, general.IniFiles.Eva, general.IniFiles.Theme);
                GlobalVar.GlobalSoundBank = new SoundBank(general.Sound.BagList.Split(','));
            },
            "Sound library initialization failed!")) return false;

            //csf
            init.SetStatus("Loading csf library");
            Log.Write("Loading Csf");
            if (!SafeRun(() =>
            {
                LoadCsf();
            },
            "Csf library initialization failed!")) return false;
            Log.Write("INITIALIZATION COMPLETE\n\n\n");
            return true;
        }
        private void LoadCsf()
        {
            var general = GlobalVar.GlobalConfig.ModConfig.General;
            if (general.StringTable.Count > 0)
            {
                GlobalVar.GlobalCsf = GlobalVar.GlobalDir.GetFile(general.StringTable.First().Name, FileExtension.CSF);
                if (general.StringTable.Count > 1)
                {
                    foreach (var entry in general.StringTable.Skip(1))
                    {
                        string name = entry.Name;
                        if (name.EndsWith("##"))
                        {
                            for (int i = 0; i < 100; i++)
                            {
                                string sttname = string.Format("{0}{1:D2}.csf", name.Substring(0, name.Length - 2), i);
                                if (GlobalVar.GlobalDir.HasFile(sttname)) GlobalVar.GlobalCsf.AddCsfLib(GlobalVar.GlobalDir.GetFile(sttname, FileExtension.CSF));
                            }
                        }
                        else
                        {
                            string csfname = string.Format("{0}.csf", name);
                            if (GlobalVar.GlobalDir.HasFile(csfname)) GlobalVar.GlobalCsf.AddCsfLib(GlobalVar.GlobalDir.GetFile(csfname, FileExtension.CSF));
                        }
                    }
                }
            }
            else GlobalVar.GlobalCsf = new CsfFile();
        }
        #endregion
        private bool NoWinXP()
        {
            if (Environment.OSVersion.Platform == PlatformID.Win32NT && Environment.OSVersion.Version.Major < 6)
            {
                MessageBox.Show("Operating system not support.", "Relert Sharp", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
    }
}
