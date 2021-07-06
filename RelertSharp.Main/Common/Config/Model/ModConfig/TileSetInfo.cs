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
        public List<TileSetClass> Classes { get; set; }
    }


    public class TileSetClass
    {
        [XmlAttribute("re")]
        public string RegexFormat { get; set; }
        [XmlAttribute("title")]
        public string Title { get; set; }
    }
}
