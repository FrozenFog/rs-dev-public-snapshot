using System;

namespace RelertSharp.Common
{
    public class EnumDisplayClass
    {
        public override bool Equals(object obj) => obj is EnumDisplayClass && Value == ((EnumDisplayClass)obj).Value;
        public override int GetHashCode() => base.GetHashCode();

        public int Value;
        public string Name;
        public EnumDisplayClass(int v, string n) { Value = v; Name = n; }
        public override string ToString()
        {
            return Value + " " + Name;
        }
        public static bool operator ==(EnumDisplayClass a, int b) { return b == a.Value; }
        public static bool operator !=(EnumDisplayClass a, int b) { return b != a.Value; }
    }
    [Flags]
    public enum WallDirection
    {
        Sole = 0,
        NE = 1,
        SE = 2,
        SW = 4,
        NW = 8,
        CornerOfEast = NE | SE,
        LineNeSw = NE | SW,
        CornerOfSouth = SE | SW,
        TriSE = NE | SE | SW,
        CornerOfNorth = NE | NW,
        LineNwSe = NW | SE,
        TriNE = NW | NE | SE,
        CornerOfWest = NW | SW,
        TriNW = SW | NW | NE,
        TriSW = SE | SW | NW,
        All = NE | SE | SW | NW
    }
    public enum PickPanelType
    {
        CombatObject = 0,
        Terrains = 1,
        Smudges = 2,
        Overlays = 3,
        Waypoints = 4,
        Celltags = 5,
        BaseNodes = 6
    }
    [Flags]
    public enum CombatObjectType
    {
        Building = 1,
        Vehicle = 4,
        Naval = 8,
        Infantry = 2,
        Aircraft = 16
    }
    [Flags]
    public enum MapObjectType
    {
        Undefined = -1,

        Building = 1,
        Vehicle = 2,
        Infantry = 4,
        Aircraft = 8,
        Unit = Vehicle | Aircraft,
        Tile = 16,
        Overlay = 32,
        Terrain = 64,
        Smudge = 128,
        BaseNode = 256,
        MiscObject = Overlay | Terrain | Smudge,
        AllBuilding = Building | BaseNode,
        Celltag = 512,
        Waypoint = 1024,
        LogicObject = Celltag | Waypoint,
        LightSource = 2048,

        MinimapRenderable = Building | Unit | Overlay | Terrain | Smudge
    }
    [Flags]
    public enum AudType
    {
        None = 0,
        Stereo = 1,
        Bit16 = 2
    }
    public enum SoundType
    {
        SoundBankRnd = 14,
        Eva = 15,
        Theme = 20,
        SoundBankRnd0 = 28,
        Eva0 = 27,
        Theme0 = 29
    }
    public enum CsfLanguage
    {
        EnUs = 0,
        EnUk = 1,
        German = 2,
        French = 3,
        Spanish = 4,
        Italian = 5,
        Japanese = 6,
        Jabberwockie = 7,
        Korean = 8,
        Chinese = 9,
        Unknown = -1
    }
    public enum ShpFrameCompressionType
    {
        RawData = 1,
        ScanLineRaw = 2,
        Compressed = 3
    }
    public enum HouseEdges
    {
        North = 1,
        West = 2,
        South = 3,
        East = 4
    }
    public enum AITriggerConditionOperator
    {
        LessThan = 0,
        LessOrEqual = 1,
        Equal = 2,
        GreaterOrEqual = 3,
        GreaterThan = 4,
        NotEqual = 5
    }
    public enum AITriggerConditionType
    {
        ConditionTrue = -1,
        EnemyHouseOwnsObj = 0,
        OwningHouseOwnsObj = 1,
        EnemyHouseLowPower = 2,
        EnemyHouseNoPower = 3,
        EnemyHouseHasMoneyOf = 4,
        OwnerHouseIronCurtainChargeInPercentage = 5,
        OwnerHouseChronoChargeInPercentage = 6,
        NeutralOrCivOwnsObj = 7
    }
    public enum BuildingSpotlightType
    {
        None = 0,
        Arcing = 1,
        Circular = 2
    }
    public enum TeamMCDecision
    {
        Default = 0,
        AddToTeam = 1,
        Grinder = 2,
        BioReaCtor = 3,
        GoHunt = 4,
        Nothing = 5
    }
    public enum TeamVeteranLevel
    {
        Rookie = 1,
        Veteran = 2,
        Elite = 3
    }
    public enum LogicType
    {
        ActionLogic,
        EventLogic
    }
    public enum TriggerRepeatingType
    {
        NoRepeating = 0,
        OneTimeLogicAND = 1,
        RepeatLogicOR = 2
    }
    public enum PackType
    {
        OverlayPack, IsoMapPack
    }
    public enum MapType
    {
        MissionMap, MultiplayerMap
    }
    public enum ChangeStatus { Changed, New, Removed }
    public enum ELanguage
    {
        EnglishUS, Chinese
    }
    public enum FileExtension
    {
        Undefined, CSV, TXT, YRM, MAP, INI, LANG, MIX, PAL, SHP, VXL, HVA, CSF,
        UnknownBinary = -1
    }
    public enum INIKeyType
    {
        SightLike, ActiveLike, PassiveLike, AcquireLike, NameListLike, MultiplierLike, NameLike, VersusLike, VersesListLike, NumListLike, Armor,
        DefaultString,
        Null = -1
    }
    public enum INIEntType
    {
        DefaultType,
        MapType, SystemType, ListType,
        UnknownType = -1
    }
    public enum INIFileType
    {
        DefaultINI,
        MapFile, RulesINI, ThemeINI, AIINI, SoundINI, ArtINI, EvaINI, Language,
        UnknownINI = -1
    }
    public enum TheaterType
    {
        Temprate, Snow, Desert, Urban, NewUrban, Lunar,
        Custom1, Custom2, Custom3,
        Unknown = -1
    }
    public enum MixTatics
    {
        Ciphed, Plain, Old,
        Unknown = -1
    }
}
