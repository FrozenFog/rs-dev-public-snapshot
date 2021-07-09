using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;

namespace RelertSharp.Common.Config.Model
{
    public class GuiStatus
    {
        [XmlElement]
        public double PosX { get; set; }
        [XmlElement]
        public double PosY { get; set; }
        [XmlElement]
        public bool IsMaximized { get; set; }
        [XmlElement]
        public double Width { get; set; }
        [XmlElement]
        public double Height { get; set; }
    }
}
