using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;

namespace RelertSharp.Common.Config.Model
{
    public class ComboGroup
    {
        [XmlAttribute("type")]
        public string Type { get; set; } = string.Empty;
        [XmlAttribute("default")]
        public bool IsDefault { get; set; } = false;
        [XmlAttribute("base")]
        public string BasedOn { get; set; } = string.Empty;
        [XmlArrayItem("item")]
        public List<ComboItem> Items { get; set; }
    }

    public class ComboItem
    {
        [XmlAttribute("key")]
        public string Key { get; set; } = string.Empty;
        [XmlAttribute("front")]
        public bool IsFront { get; set; } = false;
        [XmlAttribute("desc")]
        public string Description { get; set; } = string.Empty;
        [XmlAttribute("value")]
        public string Value { get; set; }


        public Common.ComboItem Convert()
        {
            return new Common.ComboItem(Key, Description, Value);
        }
    }
}
