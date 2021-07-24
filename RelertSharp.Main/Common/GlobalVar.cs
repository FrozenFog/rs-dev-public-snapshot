using System;
using System.Collections.Generic;
namespace RelertSharp.Common
{
    public static class GlobalVar
    {
        public static event EventHandler MapDocumentLoaded;
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
        public static void LoadMapDocument(string path)
        {
            CurrentMapDocument = new FileSystem.MapFile(path);
            MapDocumentLoaded?.Invoke(null, null);
            MapDocumentRedrawRequested?.Invoke(null, null);
        }
        public static ELanguage CurrentLanguage { get; set; } = ELanguage.EnglishUS;
        public static RSConfig GlobalConfig { get; set; }
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
