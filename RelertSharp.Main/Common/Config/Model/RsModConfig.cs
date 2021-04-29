using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;

namespace RelertSharp.Common.Config.Model
{
    public class RsModConfig
    {
        [XmlArrayItem("cb")]
        public List<ComboGroup> ComboList { get; set; } 
        [XmlElement(ElementName = "AttributeInfo")]
        public LogicAttributes LogicAttributes { get; set; }
        [XmlElement(ElementName = "TriggerInfo")]
        public TriggerInfo TriggerInfo { get; set; }
    }
}
