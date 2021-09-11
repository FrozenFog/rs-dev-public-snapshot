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
        public UserGeneral General { get; set; }
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
                },
                General = new UserGeneral()
            };
            return cfg;
        }
    }

    public class UserGeneral
    {
        public string GamePath { get; set; }
        public string ConfigPath { get; set; }
#if DEBUG
        public bool DevMode { get; set; } = true;
#endif
#if RELEASE
        [XmlElement]
        public bool DevMode { get; set; } = true;
#endif
        [XmlElement("AutoSave")]
        public int AutoSaveTime { get; set; } = 60;
        public LogLevel LogLevel { get; set; } = LogLevel.Asterisk;
        public long MaxLogSizeKb { get; set; } = 10240;
        public long MaxAutoSaveSizeKb { get; set; } = 10240;
    }
}
