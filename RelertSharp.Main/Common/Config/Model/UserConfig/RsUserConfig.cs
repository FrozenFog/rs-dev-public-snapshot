using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;

namespace RelertSharp.Common.Config.Model
{
    public class RsUserConfig
    {
        [XmlElement(ElementName = "GuiStatus")]
        public GuiStatus GuiStatus { get; set; }
        [XmlElement(ElementName = "Favourites")]
        public FavouriteItems FavouriteItems { get; set; }



        public static RsUserConfig EmptyConfig()
        {
            RsUserConfig cfg = new RsUserConfig()
            {
                GuiStatus = new GuiStatus(),
                FavouriteItems = new FavouriteItems()
                {
                    Objects = new FavouriteItemTree(),
                    TileSets = new List<TheaterTilesets>()
                }
            };
            return cfg;
        }
    }
}
