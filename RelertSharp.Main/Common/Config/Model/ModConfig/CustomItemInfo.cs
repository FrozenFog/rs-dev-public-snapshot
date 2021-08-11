using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RelertSharp.Common.Config.Model
{
    public class TileSetInfo
    {
        [XmlArrayItem("info")]
        public List<CustomItemInfo> Classes { get; set; }
    }

    public class ObjectInfo
    {
        [XmlArrayItem("info")]
        public List<CustomItemInfo> Buildings { get; set; } = new List<CustomItemInfo>();
        [XmlArrayItem("info")]
        public List<CustomItemInfo> Infantries { get; set; } = new List<CustomItemInfo>();
        [XmlArrayItem("info")]
        public List<CustomItemInfo> Units { get; set; } = new List<CustomItemInfo>();
        [XmlArrayItem("info")]
        public List<CustomItemInfo> Aircrafts { get; set; } = new List<CustomItemInfo>();
        [XmlArrayItem("info")]
        public List<CustomItemInfo> Terrains { get; set; } = new List<CustomItemInfo>();
        [XmlArrayItem("info")]
        public List<CustomItemInfo> Overlays { get; set; } = new List<CustomItemInfo>();
    }


    public class CustomItemInfo
    {
        [XmlAttribute("re")]
        public string RegexFormat { get; set; }
        [XmlAttribute("title")]
        public string Title { get; set; }
    }
}
