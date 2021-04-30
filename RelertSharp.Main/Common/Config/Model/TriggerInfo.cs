using System.Collections.Generic;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace RelertSharp.Common.Config.Model
{
    public enum TriggerInfoTraceType
    {
        None = -1,
        TeamRegTrace,
        TeamIdxTrace,
        TriggerRegTrace,
        SoundRegTrace,
        EvaIdxTrace,
        SoundIdxTrace,
        ThemeIdxTrace,
        AnimIdxTrace,
        TagIdxTrace,
        ScriptIdxTrace,
        I2dWaypointTrace,
        I2dBase128Trace,
        CsfTrace
    }
    public enum ParamParseMethod
    {
        Plain = -1,
        Waypoint,
        NestedFloat,
        Base128,
        LowMask16,
        HighMask16
    }
    public class TriggerInfo
    {
        [XmlArrayItem("info")]
        public List<LogicInfo> TriggerEvents { get; set; }
        [XmlArrayItem("info")]
        public List<LogicInfo> TriggerActions { get; set; }
        [XmlArrayItem("info")]
        public List<LogicInfo> ScriptActions { get; set; }
    }

    public class LogicInfo
    {
        [XmlAttribute("id")]
        public int Id { get; set; }
        [XmlIgnore]
        public string Abstract { get; set; }
        [XmlIgnore]
        public string Description { get; set; }
        [XmlAttribute("defaultArr")]
        public string DefaultParameters { get; set; }
        [XmlIgnore]
        public string FormatString { get; set; }
        [XmlAttribute("length")]
        public int ParamLength { get; set; }
        [XmlArrayItem("param")]
        public List<LogicInfoParameter> Parameters { get; set; }
    }

    public class LogicInfoParameter
    {
        [XmlAttribute("type")]
        public string ValueType { get; set; }
        [XmlAttribute("trace")]
        public string TraceTarget { get; set; }
        [XmlAttribute("label")]
        public string Label { get; set; }
        [XmlAttribute("arrPos")]
        public int ParamPos { get; set; }
        [XmlAttribute("parseType")]
        public string ParseMethodType { get; set; }
    }
}