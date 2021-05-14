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


        #region Trigger Parameter Parsing
        private delegate string ParameterParseHandler(string input);
        private const string PARAM_DEF = "0";
        private const string PARAM_LAST = "A";
        private static readonly Dictionary<ParamParseMethod, ParameterParseHandler> Parse = new Dictionary<ParamParseMethod, ParameterParseHandler>()
        {
            {ParamParseMethod.Plain, PlainParse },
            {ParamParseMethod.Waypoint, WaypointParse },
            {ParamParseMethod.Base128, Base128Parse },
            {ParamParseMethod.HighMask16, HiMask16Parse },
            {ParamParseMethod.LowMask16, LowMask16Parse },
            {ParamParseMethod.NestedFloat, NestedFloatParse }
        };
        private static Dictionary<ParamParseMethod, ParameterParseHandler> Write = new Dictionary<ParamParseMethod, ParameterParseHandler>()
        {
            {ParamParseMethod.Plain, PlainWrite },
            {ParamParseMethod.Waypoint, WaypointWrite },
            {ParamParseMethod.Base128, Base128Write },
            {ParamParseMethod.HighMask16, HiMask16Write },
            {ParamParseMethod.LowMask16, LowMask16Write },
            {ParamParseMethod.NestedFloat, NestedFloatWrite }
        };
        private static string PlainParse(string @in)
        {
            return @in;
        }
        private static string PlainWrite(string @in)
        {
            return @in;
        }
        private static string WaypointParse(string @in)
        {
            return Utils.Misc.WaypointInt(@in).ToString();
        }
        private static string WaypointWrite(string @in)
        {
            if (int.TryParse(@in, out int i))
            {
                return Utils.Misc.WaypointString(i);
            }
            return PARAM_LAST;
        }
        private static string NestedFloatParse(string @in)
        {
            return Utils.Misc.FromNestedFloat(@in).ToString();
        }
        private static string NestedFloatWrite(string @in)
        {
            if (float.TryParse(@in, out float f))
            {
                return Utils.Misc.ToNestedFloat(f);
            }
            return PARAM_DEF;
        }
        private static string Base128Parse(string @in)
        {
            if (int.TryParse(@in, out int i))
            {
                int y = i >> 7;
                int x = i & 0x7f;
                return string.Format("{0},{1}", x, y);
            }
            return PARAM_DEF;
        }
        private static string Base128Write(string @in)
        {
            string[] s = @in.Split(',');
            if (s.Length == 2)
            {
                string sX = s[0].Trim();
                string sY = s[1].Trim();
                int.TryParse(sX, out int x);
                int.TryParse(sY, out int y);
                int result = (y << 7) | x;
                return result.ToString();
            }
            return PARAM_DEF;
        }
        private static string LowMask16Parse(string @in)
        {
            if (int.TryParse(@in, out int i))
            {
                int result = i & 0xffff;
                return result.ToString();
            }
            return PARAM_DEF;
        }
        private static string LowMask16Write(string @in)
        {
            string[] buffer = @in.Split(',');
            if (buffer.Length == 2)
            {
                string dest = buffer[0];
                string src = buffer[1];
                if (int.TryParse(src, out int i))
                {
                    int.TryParse(dest, out int write);
                    var result = (int)(write & 0xFFFF0000) | i;
                    return write.ToString();
                }
                return dest;
            }
            return PARAM_DEF;
        }
        private static string HiMask16Parse(string @in)
        {
            if (int.TryParse(@in, out int i))
            {
                int result = i >> 16;
                return result.ToString();
            }
            return PARAM_DEF;
        }
        private static string HiMask16Write(string @in)
        {
            string[] buffer = @in.Split(',');
            if (buffer.Length == 2)
            {
                string dest = buffer[0];
                string src = buffer[1];
                if (int.TryParse(src, out int i))
                {
                    int.TryParse(dest, out int write);
                    write = (write & 0xFFFF) | (i << 16);
                    return write.ToString();
                }
                return dest;
            }
            return PARAM_DEF;
        }
        public static string GetParameter(string input, LogicInfoParameter parameter)
        {
            string result = Parse[parameter.ParseMethod](input);
            return result;
        }
        public static string WriteParameter(string input, LogicInfoParameter parameter)
        {
            string result = Write[parameter.ParseMethod](input);
            return result;
        }
        #endregion
    }
}