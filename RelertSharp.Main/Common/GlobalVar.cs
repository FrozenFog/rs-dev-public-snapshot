using RelertSharp.Common.Config;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RelertSharp.Common
{
    public static class GlobalVar
    {
        public static event EventHandler MapDocumentLoaded;
        public static event Action MapLoadComplete;
        public static event Action MapLoadCompleteAsync;
        public static event Action MapDisposed;
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
        public static bool HasMap { get { return GlobalMap != null; } }
        public static async void LoadMapDocument(string path)
        {
            if (CurrentMapDocument != null) MapDisposed?.Invoke();
            CurrentMapDocument = new FileSystem.MapFile(path);
            MapDocumentLoaded?.Invoke(null, null);
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
        public static ELanguage CurrentLanguage { get; set; } = ELanguage.EnglishUS;
        public static RsConfig GlobalConfig { get; set; }
        public static FileSystem.VirtualDir GlobalDir { get; set; }
        public static IniSystem.Rules GlobalRules { get; set; }
        public static IniSystem.SoundRules GlobalSound { get; set; }
        public static FileSystem.SoundBank GlobalSoundBank { get; set; }
        public static FileSystem.CsfFile GlobalCsf { get; set; }
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
