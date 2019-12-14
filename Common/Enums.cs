using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace relert_sharp.Common
{
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
        Undefined, CSV, TXT, YRM, MAP, INI, LANG, MIX,
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
        MapFile, RulesINI, ThemeINI, AIINI, SoundINI, ArtINI, Language,
        UnknownINI = -1
    }
    public enum TheaterType
    {
        Template, Snow, Desert, Urban, NewUrban, Lunar,
        Unknown = -1
    }
    public enum MixTatics
    {
        Ciphed, Plain,
        Unknown = -1
    }
}
