using RelertSharp.Common.Config;
using RelertSharp.MapStructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RelertSharp.Common
{
    public static class GlobalVar
    {
        public static event Action MapDocumentLoaded;
        public static event Action MapLoadComplete;
        public static event Action MapLoadCompleteAsync;
        public static event Action MapDisposed;
        public static event Action MapSaveBegin;
        public static event Action MapSaved;
        public static event EventHandler MapDocumentRedrawRequested;
        public static FileSystem.MapFile CurrentMapDocument { get; private set; }
        public static MapStructure.Map GlobalMap
        {
            get
            {
                if (CurrentMapDocument != null) return CurrentMapDocument.Map;
                return null;
            }
        }
        public static void ForceReload()
        {
            MapDocumentLoaded?.Invoke();
        }
        public static bool HasMap { get { return GlobalMap != null; } }
        public static void SaveMapDocument(string path)
        {
            Log.Info("Saving {0}", path);
            Monitor.EnableMonitor();
            MapSaveBegin?.Invoke();
            CurrentMapDocument.SaveMapAs(path);
            Monitor.EndMonitorLog();
            MapSaved?.Invoke();
        }
        public static void DisposeMapDocument()
        {
            bool launchEvent = CurrentMapDocument != null;
            CurrentMapDocument = null;
            if (launchEvent) MapDisposed?.Invoke();
            MapDocumentLoaded?.Invoke();
        }
        public static async void LoadMapDocument(string path)
        {
            if (CurrentMapDocument != null) MapDisposed?.Invoke();
            ReloadRules();
            Monitor.EnableMonitor();
            CurrentMapDocument = new FileSystem.MapFile(path);
            Monitor.EndMonitorLog();
            GlobalRules?.MergeIncludes(CurrentMapDocument, GlobalDir);
            MapDocumentLoaded?.Invoke();
            MapDocumentRedrawRequested?.Invoke(null, null);
            MapLoadComplete?.Invoke();
            await MapLoadedAsync();
        }
        public static async void CreateNewMap(IMapCreationConfig cfg)
        {
            var file = new FileSystem.MapFile(cfg);
            if (CurrentMapDocument != null) MapDisposed?.Invoke();
            CurrentMapDocument = file;
            MapDocumentLoaded?.Invoke();
            MapDocumentRedrawRequested?.Invoke(null, null);
            MapLoadComplete?.Invoke();
            await MapLoadedAsync();
        }
        private static async Task MapLoadedAsync()
        {
            await Task.Run(() =>
            {
                MapLoadCompleteAsync?.Invoke();
            });
        }
        private static void ReloadRules()
        {
            string rules = GlobalConfig.ModGeneral.IniFiles.Rules + Constant.EX_INI;
            string arts = GlobalConfig.ModGeneral.IniFiles.Art + Constant.EX_INI;
            GlobalRules = new IniSystem.Rules(GlobalDir.GetRawByte(rules, true), rules);
            GlobalRules.LoadArt(GlobalDir.GetFile(arts, FileExtension.INI, true));
            GlobalRules.MergeIncludes(GlobalDir);
        }
        public static ELanguage CurrentLanguage { get; set; } = ELanguage.EnglishUS;
        public static RsConfig GlobalConfig { get; set; }
        public static FileSystem.VirtualDir GlobalDir { get; set; }
        public static IniSystem.Rules GlobalRules { get; set; }
        public static IniSystem.SoundRules GlobalSound { get; set; }
        public static FileSystem.SoundBank GlobalSoundBank { get; set; }
        public static FileSystem.CsfFile GlobalCsf { get; set; }
        public static MapReadingMonitor Monitor { get; set; } = new MapReadingMonitor();
        public static FileSystem.PalFile TilePalette { get; set; }
        public static string PlayerSide { get; set; }
        public static MapStructure.MapTheaterTileSet TileDictionary { get; set; }
        public static TheaterType CurrentTheater { get; set; }
        public static Language Language { get; set; }
        public static string RunPath { get { return System.Windows.Forms.Application.StartupPath + "\\"; } }
        private static RsLog log;
        public static RsLog Log
        {
            get { if (log == null) log = new RsLog(); return log; }
            set { log = value; }
        }
    }
}
