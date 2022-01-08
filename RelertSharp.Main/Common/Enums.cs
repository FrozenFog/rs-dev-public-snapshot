using System;
using System.Xml.Serialization;

namespace RelertSharp.Common
{
    public enum LogLevel
    {
        /// <summary>
        /// just anything you want, blablabla
        /// </summary>
        [XmlEnum("0")]
        Anything = 0,
        /// <summary>
        /// log such as engine established
        /// </summary>
        [XmlEnum("1")]
        Info = 1,
        /// <summary>
        /// progress info that cannot be ignored
        /// </summary>
        [XmlEnum("2")]
        Asterisk = 2,
        /// <summary>
        /// some error occured and CAN still operate
        /// </summary>
        [XmlEnum("3")]
        Warning = 3,
        /// <summary>
        /// some error occured and CAUSE INFORMATION LOSS
        /// </summary>
        [XmlEnum("4")]
        Critical = 4,
        /// <summary>
        /// some error occured and GUI may not work properly
        /// </summary>
        [XmlEnum("5")]
        Error = 5,
        /// <summary>
        /// cause entire program shutdown
        /// </summary>
        [XmlEnum("6")]
        Fatal = 6,
        /// <summary>
        /// debug only, anything that need debug
        /// </summary>
        [XmlEnum("7")]
        DEBUG = 7
    }
    public enum MapLightningType
    {
        Normal = 0,
        Storm = 1,
        Dominator = 2
    }
    public enum IndexableDisplayType
    {
        NameOnly = 0,
        IdAndName = 1
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
    public enum TubeDirection
    {
        _END = -1,
        NE = 0,
        E = 1,
        SE = 2,
        S = 3,
        SW = 4,
        W = 5,
        NW = 6,
        N = 7
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
        Undefined = 0,

        Building = 1,
        Vehicle = 1 << 1,
        Infantry = 1 << 2,
        Aircraft = 1 << 3,
        Unit = Vehicle | Aircraft,
        Tile = 1 << 4,
        Overlay = 1 << 5,
        Terrain = 1 << 6,
        Smudge = 1 << 7,
        BaseNode = 1 << 8,
        MiscObject = Overlay | Terrain | Smudge,
        AllBuilding = Building | BaseNode,
        MovableCombanant = Vehicle | Infantry | Aircraft,
        GroundMovable = Vehicle | Infantry,
        CombatObject = Building | Vehicle | Infantry | Aircraft,
        Celltag = 1 << 9,
        Waypoint = 1 << 10,
        LogicObject = Celltag | Waypoint,
        TubeTunnel = 1 << 11,
        LightSource = 1 << 12,

        MinimapRenderable = Building | Unit | Infantry | Overlay | Terrain,

        RsObjects = LightSource,
        AllSelectableObjects = CombatObject | MiscObject | LogicObject | BaseNode | RsObjects,

        Anything = int.MaxValue
    }
    public enum LogicType
    {
        None = -1,
        Team = 0,
        Taskforce = 1,
        Script = 2,
        Trigger = 3,
        Tag = 4,
        AiTrigger = 5,
        LocalVariable = 6,
        House = 7,
        Country = 8,
        Event = 9,
        Action = 10
    }
    [Flags]
    public enum AudType
    {
        None = 0,
        Stereo = 1,
        Bit16 = 2,
        UnknownBit = 4
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
    public enum TriggerSubType
    {
        None = 0,
        Action = 10,
        Event = 9
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
        Temprate = 1, 
        Snow = 2, 
        Desert = 4, 
        Urban = 3, 
        NewUrban = 5, 
        Lunar = 6,
        Custom1 = 7, 
        Custom2 = 8, 
        Custom3 = 9,
        Unknown = -1
    }
    public enum MixTatics
    {
        Ciphed, Plain, Old,
        Unknown = -1
    }
}
