using RelertSharp.Common;
using RelertSharp.Engine.Api;
using RelertSharp.MapStructure;
using RelertSharp.Wpf.Dialogs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace RelertSharp.Wpf
{
    internal static class MainWindowUtil
    {
        private static DispatcherTimer timer;
        private static Stopwatch watch = new Stopwatch();
        private const string PATH = "AutoSave";
        private static bool autosaveFailed = false;


        static MainWindowUtil()
        {
            timer = new DispatcherTimer()
            {
                Interval = new TimeSpan(0, 0, 60),
            };
            GlobalVar.MapDocumentLoaded += HandleMapLoaded;
            EngineApi.MapDrawingComplete += MapLoadComplete;
            timer.Tick += AutoSaveTick;
        }

        private static void HandleMapLoaded()
        {
            if (GlobalVar.Monitor.HasLog)
            {
                DlgMonitorResult dlg = new DlgMonitorResult();
                dlg.ReadFromMonitor(GlobalVar.Monitor);
                dlg.ShowDialog();
            }
        }

        private static void MapLoadComplete()
        {
            timer.Interval = new TimeSpan(0, 0, GlobalVar.GlobalConfig.UserConfig.AutoSaveTime);
            timer.Start();
        }

        private static void AutoSaveTick(object sender, EventArgs e)
        {
            if (AutosaveEnabled && GlobalVar.CurrentMapDocument != null)
            {
                Save();
            }
        }

        private static void Save()
        {
            try
            {
                if (!Directory.Exists(PATH)) Directory.CreateDirectory(PATH);
                string date = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss");
                string filename = Path.Combine(PATH, string.Format("AutoSave - {0}.map", date));
                GlobalVar.CurrentMapDocument.SaveMapAs(filename);
                autosaveFailed = false;
            }
            catch (Exception e)
            {
                if (!autosaveFailed)
                {
                    autosaveFailed = true;
                    GuiUtil.Fatal("Autosave failed!\n", e);
                }
            }
        }


        #region Api
        public static void BeginAutoSave()
        {
            AutosaveEnabled = true;
            timer.Start();
        }
        public static void SuspendAutoSave()
        {
            timer.Stop();
            AutosaveEnabled = false;
        }
        #endregion



        public static bool AutosaveEnabled { get; private set; }
    }
}
