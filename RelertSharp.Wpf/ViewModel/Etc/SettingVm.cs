using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RelertSharp.Common.GlobalVar;
using RelertSharp.Common;

namespace RelertSharp.Wpf.ViewModel
{
    internal class SettingVm : BaseVm<SettingVm.SettingModel>
    {
        public SettingVm()
        {
            data = new SettingModel();
        }


        public void ApplyChanges()
        {
            var c = GlobalConfig;
            c.GamePath = data.GamePath;
            c.UserConfig.AutoSaveTime = data.AutosaveTime;
            c.UserConfig.General.ConfigPath = data.ConfigPath;
            c.UserConfig.General.MaxAutoSaveSizeKb = data.SaveSize;
            c.UserConfig.General.MaxLogSizeKb = data.LogSize;
        }


        #region Bind
        public int AutosaveTime
        {
            get { return data.AutosaveTime; }
            set
            {
                data.AutosaveTime = value.TrimTo(30, int.MaxValue);
                SetProperty();
            }
        }
        public string GamePath
        {
            get { return data.GamePath; }
            set
            {
                data.GamePath = value;
                SetProperty();
            }
        }
        public string ConfigPath
        {
            get { return data.ConfigPath; }
            set
            {
                data.ConfigPath = value;
                SetProperty();
            }
        }
        public long LogSize
        {
            get { return data.LogSize; }
            set
            {
                data.LogSize = value.TrimTo(100, long.MaxValue);
                SetProperty();
            }
        }
        public long SaveSize
        {
            get { return data.SaveSize; }
            set
            {
                data.SaveSize = value.TrimTo(100, long.MaxValue);
                SetProperty();
            }
        }
        #endregion
        #region Model
        public class SettingModel
        {
            public int AutosaveTime { get; set; } = (int)(GlobalConfig?.UserConfig?.AutoSaveTime);
            public string GamePath { get; set; } = GlobalConfig?.GamePath;
            public string ConfigPath { get; set; } = GlobalConfig?.UserConfig?.General.ConfigPath;
            public long LogSize { get; set; } = (long)(GlobalConfig?.UserConfig.General.MaxLogSizeKb);
            public long SaveSize { get; set; } = (long)(GlobalConfig?.UserConfig.General.MaxAutoSaveSizeKb);
        }
        #endregion
    }
}
