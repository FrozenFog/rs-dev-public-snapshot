using RelertSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelertSharp.Wpf.ViewModel
{
    internal class MapCreatingVm : BaseVm<MapCreatingVm.MapCreationConfig>
    {
        public MapCreatingVm()
        {
            data = new MapCreationConfig();
        }




        #region Calls
        public IMapCreationConfig Config { get { return data; } }
        #region Bind
        public int Width
        {
            get { return data.Width; }
            set
            {
                data.Width = value;
                SetProperty();
            }
        }
        public int Height
        {
            get { return data.Height; }
            set
            {
                data.Height = value;
                SetProperty();
            }
        }
        public string Theater
        {
            get { return data.TheaterKey; }
            set
            {
                data.TheaterKey = value;
                SetProperty();
            }
        }
        public string MapName
        {
            get { return data.MapName; }
            set
            {
                data.MapName = value;
                SetProperty();
            }
        }
        public string PlayerHouseName
        {
            get { return data.PlayerHouseName; }
            set
            {
                data.PlayerHouseName = value;
                SetProperty();
            }
        }
        public bool IsRulesDefHouse
        {
            get { return data.UseDefaultHouse; }
            set
            {
                data.UseDefaultHouse = value;
                SetProperty();
                SetProperty(nameof(IsPlayerHouseEnable));
            }
        }
        public bool IsSingle
        {
            get { return data.IsSinglePlayer; }
            set
            {
                data.IsSinglePlayer = value;
                SetProperty();
                SetProperty(nameof(IsPlayerHouseEnable));
            }
        }
        public bool IsPlayerHouseEnable
        {
            get { return data.IsSinglePlayer && data.UseDefaultHouse; }
        }
        #endregion
        #endregion
        public class MapCreationConfig : IMapCreationConfig
        {
            public int Width { get; set; } = 50;

            public int Height { get; set; } = 50;

            public string TheaterKey { get; set; } = "Temperate";

            public bool UseDefaultHouse { get; set; }

            public string MapName { get; set; } = "New Map";

            public bool IsSinglePlayer { get; set; }

            public string PlayerHouseName { get; set; } = Constant.DefaultHouseName;
        }
    }
}
