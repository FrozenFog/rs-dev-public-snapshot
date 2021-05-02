using System;
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
    public enum ParamFormatType
    {
        KvValue = -1,
        Plain,
        KvKey,
        WpPos,
        Bool
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
        public string _TraceType
        {
            get
            {
                if (TraceTarget == TriggerInfoTraceType.None) return null;
                return TraceTarget.ToString();
            }
            set
            {
                TraceTarget = Utils.Misc.ParseEnum(value, TriggerInfoTraceType.None);
            }
        }
        [XmlIgnore]
        public TriggerInfoTraceType TraceTarget { get; set; }
        [XmlAttribute("parseType")]
        public string _ParseMethodType
        {
            get
            {
                if (ParseMethod == ParamParseMethod.Plain) return null;
                return ParseMethod.ToString();
            }
            set
            {
                ParseMethod = Utils.Misc.ParseEnum(value, ParamParseMethod.Plain);
            }
        }
        [XmlIgnore]
        public ParamParseMethod ParseMethod { get; set; }
        [XmlAttribute("fmtType")]
        public string _FormatType
        {
            get
            {
                if (ParamFormat == ParamFormatType.KvValue) return null;
                return ParamFormat.ToString();
            }
            set
            {
                ParamFormat = Utils.Misc.ParseEnum(value, ParamFormatType.KvValue);
            }
        }
        public ParamFormatType ParamFormat { get; set; }
        [XmlAttribute("label")]
        public string Label { get; set; }
        [XmlAttribute("arrPos")]
        public int ParamPos { get; set; }
    }
}