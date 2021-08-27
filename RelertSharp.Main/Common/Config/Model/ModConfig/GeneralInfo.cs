using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RelertSharp.Common.Config.Model
{
    public class GeneralInfo
    {
        [XmlElement]
        public string Name { get; set; }
        [XmlElement]
        public string Version { get; set; }
        [XmlArrayItem("entry")]
        public List<MixEntryInfo> MixInfo { get; set; }
        [XmlArrayItem("theater")]
        public List<TheaterEntryInfo> Theater { get; set; }
        [XmlElement(ElementName = "Sound")]
        public SoundInfo Sound { get; set; }
        [XmlArrayItem("entry")]
        public List<NamedEntry> StringTable { get; set; }
        [XmlElement]
        public IniInfo IniFiles { get; set; }
        [XmlElement]
        public EngineAdjust DrawingAdjust { get; set; }
        [XmlArrayItem("entry")]
        public List<SideEntryInfo> SideInfo { get; set; }
        [XmlArrayItem("tib")]
        public List<TiberiumInfo> TiberiumInfo { get; set; }
    }

    public class TiberiumInfo
    {
        [XmlAttribute("min")]
        public int RangeMin { get; set; }
        [XmlAttribute("max")]
        public int RangeMax { get; set; }
        [XmlAttribute("type")]
        public int Type { get; set; }
        [XmlAttribute("disable")]
        public bool Disabled { get; set; }
    }

    public class SideEntryInfo : NamedEntry
    {
        [XmlAttribute("idx")]
        public int Index { get; set; }
        [XmlAttribute("side")]
        public string SideId { get; set; }
        [XmlAttribute("evaPrefix")]
        public string EvaPrefix { get; set; }
    }

    public class EngineAdjust
    {
        [XmlElement("BridgeOffsetFrames")]
        public string BridgeOffsets { get; set; }
        [XmlElement("IgnoreBuildingTheaterArt")]
        public bool NoBudAltArt { get; set; }
        [XmlElement("IgnoreInfantryTheaterArt")]
        public bool NoInfAltArt { get; set; }
        [XmlArrayItem("reg")]
        public List<NamedEntry> DeactivateAnim { get; set; }
        [XmlArrayItem("reg")]
        public List<NamedEntry> DeactivateShadow { get; set; }
        [XmlArrayItem("reg")]
        public List<NamedEntry> DeactivateBib { get; set; }
    }

    public class IniInfo
    {
        [XmlElement]
        public string Rules { get; set; }
        [XmlElement]
        public string Art { get; set; }
        [XmlElement]
        public string Sound { get; set; }
        [XmlElement]
        public string Theme { get; set; }
        [XmlElement]
        public string Ai { get; set; }
        [XmlElement]
        public string Eva { get; set; }
        [XmlElement]
        public string Mission { get; set; }
        [XmlElement]
        public string Config { get; set; }
    }

    public class SoundInfo
    {
        [XmlElement(ElementName = "Bags")]
        public string BagList { get; set; }
    }

    public class TheaterEntryInfo : NamedEntry
    {
        [XmlAttribute("pal")]
        public string Palette { get; set; }
        [XmlAttribute("mix")]
        public string MixList { get; set; }
        [XmlAttribute("virtual")]
        public bool IsVirtual { get; set; }
        [XmlAttribute("ini")]
        public string TheaterIni { get; set; }
        [XmlAttribute("type")]
        public int TheaterType { get; set; }
        [XmlAttribute("suffix")]
        public string Suffix { get; set; }
    }

    public class MixEntryInfo : NamedEntry
    {
        [XmlAttribute("tatic")]
        public int ReadTatic { get; set; }
    }


    public class NamedEntry
    {
        [XmlAttribute("name")]
        public string Name { get; set; }
        [XmlAttribute("expand")]
        public bool IsExpand { get; set; }
    }
}
