using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelertSharp.Common.Config
{
    public class RsConfig
    {
        #region Port
        public ModConfig ModConfig { get; set; }
        public Model.GeneralInfo ModGeneral { get { return ModConfig.General; } }
        public UserConfig UserConfig { get; set; }
        public Model.EngineAdjust DrawingAdjust { get { return ModConfig.General.DrawingAdjust; } }
        #endregion


        public RsConfig()
        {
            ModConfig = new ModConfig(Constant.Config.Path);
            UserConfig = new UserConfig();
        }


        #region Modconfig reading
        public Model.SideEntryInfo GetSideInfo(Func<Model.SideEntryInfo, bool> predicate)
        {
            return ModConfig.General.SideInfo.First(predicate);
        }
        public Model.TheaterEntryInfo GetTheaterInfo(Func<Model.TheaterEntryInfo, bool> func)
        {
            return ModConfig.General.Theater.First(func);
        }
        #endregion


        #region Public
        public TheaterType GetTheater(string theaterName)
        {
            theaterName = theaterName.ToLower();
            var info = GetTheaterInfo(x => x.Name.ToLower() == theaterName);
            return (TheaterType)info.TheaterType;
        }
        public string GetTheaterPalName(TheaterType type)
        {
            var info = GetTheaterInfo(x => x.TheaterType == (int)type);
            return info.Palette;
        }
        public string GetTheaterPalName(string theaterName)
        {
            TheaterType type = GetTheater(theaterName);
            return GetTheaterPalName(type);
        }
        private List<int> bridgeOffset;
        public bool BridgeOffsetContains(byte overlayFrame)
        {
            if (bridgeOffset == null)
            {
                string[] arr = ModConfig.General.DrawingAdjust.BridgeOffsets.Split(',');
                bridgeOffset = arr.Select(x =>
                {
                    int.TryParse(x, out int i);
                    return i;
                }).ToList();
            }
            return bridgeOffset.Contains(overlayFrame);
        }
        #endregion



        #region Calls
        public string GamePath
        {
            get { return UserConfig.General.GamePath; }
            set { UserConfig.General.GamePath = value; }
        }
        public bool DevMode { get { return UserConfig.General.DevMode; } }
        public IniSystem.INIEntity LightPostTemplate { get; }
        #endregion
    }
}
