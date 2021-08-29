using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;

namespace RelertSharp.Common.Config.Model
{
    public class LogicAttributes
    {
        [XmlElement]
        public AttributeGroup Team { get; set; }
        [XmlElement]
        public AttributeGroup AiTrigger { get; set; }
        [XmlElement]
        public AttributeGroup Unit { get; set; }
        [XmlElement]
        public AttributeGroup Building { get; set; }
        [XmlElement]
        public AttributeGroup Infantry { get; set; }
        [XmlElement]
        public AttributeGroup Aircraft { get; set; }
    }

    public class AttributeGroup
    {
        [XmlArrayItem("class")]
        public List<AttributeClass> Classes { get; set; }
        [XmlArrayItem("attr")]
        public List<AttributeItem> Items { get; set; }
    }

    public class AttributeClass
    {
        [XmlAttribute("id")]
        public string Id { get; set; }
        [XmlAttribute("name")]
        public string Name { get; set; }
        [XmlAttribute("desc")]
        public string Description { get; set; }
    }

    public class AttributeItem
    {
        public const string PARSE_WP = "WPIndex";

        /// <summary>
        /// Lookup for ini key, crutial
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; }


        /// <summary>
        /// Used in left label, leave it empty to show name
        /// </summary>
        [XmlAttribute("label")]
        public string LabelValue { get; set; }
        [XmlIgnore]
        public string Label { get { return LabelValue.IsNullOrEmpty() ? Name : LabelValue; } set { if (!value.IsNullOrEmpty()) LabelValue = value; } }


        /// <summary>
        /// Used in tool tip
        /// </summary>
        [XmlAttribute("desc")]
        public string Description { get; set; }

        /// <summary>
        /// Used in combo lookup
        /// </summary>
        [XmlAttribute("type")]
        public string ValueType { get; set; }

        /// <summary>
        /// Used in sorting
        /// </summary>
        [XmlAttribute("class")]
        public string Parent { get; set; }

        /// <summary>
        /// Used in parsing(like base26 waypoint)
        /// </summary>
        [XmlAttribute("parsing")]
        public string ParseMethod { get; set; }

        /// <summary>
        /// Default value when initialized (in game)
        /// If target is default value when saving, will be discard
        /// </summary>
        [XmlAttribute("default")]
        public string DefaultValue { get; set; }
        [XmlAttribute("trace")]
        public int TraceTypeInt { get; set; }
        [XmlIgnore]
        public TriggerInfoTraceType Trace
        {
            get { return (TriggerInfoTraceType)TraceTypeInt; }
        }
    }
}
