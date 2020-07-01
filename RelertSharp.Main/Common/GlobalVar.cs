using System.Collections.Generic;
namespace RelertSharp.Common
{
    public static class GlobalVar
    {
        public static FileSystem.MapFile CurrentMapDocument { get; set; }
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
        public static DrawingEngine.Engine Engine { get; set; }
        public static TheaterType CurrentTheater { get; set; }
        public static string RunPath { get { return System.Windows.Forms.Application.StartupPath + "\\"; } }
        public static RsLog Log { get; set; }
        public static class Scripts
        {
            public static List<IniSystem.TechnoPair> AttackTargetType { get; set; }
            public static List<IniSystem.TechnoPair> UnloadBehavior { get; set; }
            public static List<IniSystem.TechnoPair> Missions { get; set; }
            public static List<IniSystem.TechnoPair> FacingDirections { get; set; }
            public static List<IniSystem.TechnoPair> TalkBubbles { get; set; }
        }
    }
}
