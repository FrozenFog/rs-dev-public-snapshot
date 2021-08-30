using RelertSharp.Common;
using RelertSharp.MapStructure;
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
    internal static class AutoSave
    {
        private static DispatcherTimer timer;
        private static Stopwatch watch = new Stopwatch();
        private const string PATH = "./AutoSave";
        private static bool autosaveFailed = false;


        static AutoSave()
        {
            timer = new DispatcherTimer()
            {
                Interval = new TimeSpan(0, 0, 60),
            };
            GlobalVar.MapLoadComplete += MapLoadComplete;
            timer.Tick += AutoSaveTick;
        }

        private static void MapLoadComplete()
        {
            timer.Interval = new TimeSpan(0, 0, GlobalVar.GlobalConfig.UserConfig.AutoSaveTile);
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
                string filename = Path.Combine(PATH, string.Format("AutoSave - {0}", DateTime.Now.ToString()));
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
