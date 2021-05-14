﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace RelertSharp.Common.Config.Model
{
    public enum TriggerInfoTraceType
    {
        None = 0,
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
        Plain = 0,
        Waypoint,
        NestedFloat,
        Base128,
        LowMask16,
        HighMask16
    }
    public enum ParamFormatType
    {
        KvValue = 0,
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
        #region Model
        [XmlAttribute("type")]
        public string ValueType { get; set; }
        private string _traceType;
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
                _traceType = value;
                TraceTarget = Utils.Misc.ParseEnum(value, TriggerInfoTraceType.None);
            }
        }
        private TriggerInfoTraceType _TraceTarget;
        [XmlIgnore]
        public TriggerInfoTraceType TraceTarget
        {
            get { if (_traceType.IsNullOrEmpty()) return TriggerInfoTraceType.None; return _TraceTarget; }
            set { _TraceTarget = value; }
        }
        private string _parsetype;
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
                _parsetype = value;
                ParseMethod = Utils.Misc.ParseEnum(value, ParamParseMethod.Plain);
            }
        }
        private ParamParseMethod _ParseMethod;
        [XmlIgnore]
        public ParamParseMethod ParseMethod
        {
            get { if (_parsetype.IsNullOrEmpty()) return ParamParseMethod.Plain; return _ParseMethod; }
            set { _ParseMethod = value; }
        }
        private string _fmtType;
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
                _fmtType = value;
                ParamFormat = Utils.Misc.ParseEnum(value, ParamFormatType.KvValue);
            }
        }
        private ParamFormatType _fmt;
        [XmlIgnore]
        public ParamFormatType ParamFormat
        {
            get { if (_fmtType.IsNullOrEmpty()) return ParamFormatType.KvValue; return _fmt; }
            set { _fmt = value; }
        }
        [XmlAttribute("label")]
        public string Label { get; set; }
        [XmlAttribute("arrPos")]
        public int ParamPos { get; set; }
        #endregion


        #region Trigger Parameter Parsing
        private delegate string ParameterParseHandler(string input);
        private delegate string ParameterFormatHandler(string input, IEnumerable<IIndexableItem> lookup);
        private const string PARAM_DEF = "0";
        private const string PARAM_LAST = "A";
        private const string FMT_TRUE = "True";
        private const string FMT_FALSE = "False";
        #region Parse & Write
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
        #endregion
        #region Formating
        private static readonly Dictionary<ParamFormatType, ParameterFormatHandler> Format = new Dictionary<ParamFormatType, ParameterFormatHandler>()
        {
            {ParamFormatType.Plain, PlainFormat },
            {ParamFormatType.KvKey, PlainFormat },
            {ParamFormatType.KvValue, KvValueFormat },
            {ParamFormatType.Bool, BoolFormat },
            {ParamFormatType.WpPos, WpPosFormat }
        };
        private static string PlainFormat(string @in, IEnumerable<IIndexableItem> lookup)
        {
            return @in;
        }
        private static string KvValueFormat(string @in, IEnumerable<IIndexableItem> lookup)
        {
            IIndexableItem item = lookup.FirstOrDefault(x => x.Id == @in);
            if (item != null) return item.Name;
            return @in;
        }
        private static string WpPosFormat(string @in, IEnumerable<IIndexableItem> lookup)
        {
            IIndexableItem item = lookup.FirstOrDefault(x => x.Id == @in);
            if (item != null && item is I2dLocateable pos)
            {
                return string.Format("{2}({0}, {1})", pos.X, pos.Y, item.Id);
            }
            return @in;
        }
        private static string BoolFormat(string @in, IEnumerable<IIndexableItem> lookup)
        {
            if (@in.ParseBool()) return FMT_TRUE;
            return FMT_FALSE;
        }
        #endregion
        public static string GetParameter(string input, LogicInfoParameter info)
        {
            string result = Parse[info.ParseMethod](input);
            return result;
        }
        public static string GetFormatParam(string parameter, LogicInfoParameter info)
        {
            IEnumerable<IIndexableItem> combo = GlobalVar.GlobalConfig.ModConfig.GetCombo(info.ValueType);
            string result = Format[info.ParamFormat](parameter, combo);
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