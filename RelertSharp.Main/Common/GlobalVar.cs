namespace RelertSharp.Common
{
    public static class GlobalVar
    {
        public static ELanguage CurrentLanguage { get; set; } = ELanguage.EnglishUS;
        public static RSConfig GlobalConfig { get; set; }
        public static FileSystem.VirtualDir GlobalDir { get; set; }
        public static IniSystem.Rules GlobalRules { get; set; }
        public static IniSystem.SoundRules GlobalSound { get; set; }
        public static FileSystem.SoundBank GlobalSoundBank { get; set; }
        public static FileSystem.CsfFile GlobalCsf { get; set; }
        public static string PlayerSide { get; set; }
        public static MapStructure.MapTheaterTileSet TileDictionary { get; set; }
        public static DrawingEngine.Engine Engine { get; set; }
    }
}
