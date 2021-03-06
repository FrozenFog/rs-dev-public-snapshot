using RelertSharp.Common.Config.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RelertSharp.Common
{
    public class UserConfig
    {
        private const string path = Constant.Config.UserConfig;
        private RsUserConfig data;
        public UserConfig(out bool exist)
        {
            if (File.Exists(path))
            {
                exist = true;
                XmlSerializer serializer = new XmlSerializer(typeof(RsUserConfig));
                TextReader reader = new StreamReader(path);
                data = serializer.Deserialize(reader) as RsUserConfig;
                reader.Dispose();
            }
            else
            {
                exist = false;
                data = RsUserConfig.EmptyConfig();
            }
        }


        #region Public 
        public void Save()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(RsUserConfig));
            if (!File.Exists(path))
            {
                File.Create(path).Close();
            }
            TextWriter writer = new StreamWriter(path);
            serializer.Serialize(writer, data);
            writer.Close();
        }
        public void SetGuiStatus(double x, double y, double w, double h, bool maximized)
        {
            GuiStatus.PosX = x;
            GuiStatus.PosY = y;
            GuiStatus.Width = w;
            GuiStatus.Height = h;
            GuiStatus.IsMaximized = maximized;
        }
        public bool IsGuiValid()
        {
            return GuiStatus.Width > 5 && GuiStatus.Height > 5;
        }
        public List<FavouriteItemTree> GetFavTilesetsByTheater(string theaterName)
        {
            var fav = FavouriteTileSet.Find(x => x.TheaterName == theaterName);
            if (fav == null)
            {
                fav = new TheaterTilesets()
                {
                    TheaterName = theaterName,
                    Items = new List<FavouriteItemTree>()
                };
            }
            return fav.Items;
        }
        #endregion


        #region Calls
        public UserGeneral General { get { return data.General; } }
        public int AutoSaveTime
        {
            get { return General.AutoSaveTime; }
            set { General.AutoSaveTime = value.TrimTo(10, int.MaxValue); }
        }
        public LogLevel LogLevel
        {
            get { return General.LogLevel; }
            set { General.LogLevel = value; }
        }
        public GuiStatus GuiStatus { get { return data.GuiStatus; } }
        public List<TheaterTilesets> FavouriteTileSet { get { return data.FavouriteItems.TileSets; } }
        public FavouriteItemTree FavouriteObjects { get { return data.FavouriteItems.Objects; } }
        #endregion
    }
}
