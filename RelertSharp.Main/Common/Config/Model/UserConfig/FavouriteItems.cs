using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;

namespace RelertSharp.Common.Config.Model
{
    public class FavouriteItems
    {
        [XmlElement(ElementName = "Objects")]
        public FavouriteItemTree Objects { get; set; }
        [XmlElement(ElementName = "TileSets")]
        public FavouriteItemTree TileSets { get; set; }
    }


    public class FavouriteItemTree
    {
        [XmlAttribute("title")]
        public string Title { get; set; }
        [XmlAttribute("value")]
        public string Value { get; set; }

        private string _type;
        [XmlAttribute("type")]
        public string _FavouriteType
        {
            get
            {
                if (Type == MapObjectType.Undefined) return null;
                return Type.ToString();
            }
            set
            {
                _type = value;
                Type = Utils.Misc.ParseEnum(value, MapObjectType.Undefined);
            }
        }
        private MapObjectType _Type = MapObjectType.Undefined;
        [XmlIgnore]
        public MapObjectType Type
        {
            get { if (_type.IsNullOrEmpty()) return MapObjectType.Undefined; return _Type; }
            set { _Type = value; }
        }
        [XmlArrayItem("item")]
        public List<FavouriteItemTree> Items { get; set; }
    }
}
