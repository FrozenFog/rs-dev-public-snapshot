using RelertSharp.Common;
using RelertSharp.MapStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelertSharp.Wpf.ViewModel
{
    internal class RankingVm : BaseVm<RankInfo>
    {
        public RankingVm()
        {
            data = new RankInfo();
        }
        public RankingVm(RankInfo src)
        {
            data = src;
        }


        #region Bind Call
        public string TimeEasy
        {
            get { return RankInfo.TimeString(data.ETime); }
            set
            {
                data.ETime = RankInfo.TimeInt(value);
                SetProperty();
            }
        }
        public string TimeNormal
        {
            get { return RankInfo.TimeString(data.NTime); }
            set
            {
                data.NTime = RankInfo.TimeInt(value);
                SetProperty();
            }
        }
        public string TimeHard
        {
            get { return RankInfo.TimeString(data.HTime); }
            set
            {
                data.HTime = RankInfo.TimeInt(value);
                SetProperty();
            }
        }
        public string TitleOverUI
        {
            get { return data.TitleOver; }
            set
            {
                data.TitleOver = value;
                SetProperty();
                SetProperty(nameof(TitleOverValue));
            }
        }
        public string TitleOverValue
        {
            get { return GlobalVar.GlobalCsf[data.TitleOver].ContentString; }
        }
        public string TitleUnderUI
        {
            get { return data.TitleUnder; }
            set
            {
                data.TitleUnder = value;
                SetProperty();
                SetProperty(nameof(TitleUnderValue));
            }
        }
        public string TitleUnderValue
        {
            get { return GlobalVar.GlobalCsf[data.TitleUnder].ContentString; }
        }
        public string MessageOverUI
        {
            get { return data.MsgOver; }
            set
            {
                data.MsgOver = value;
                SetProperty();
                SetProperty(nameof(MessageOverValue));
            }
        }
        public string MessageOverValue
        {
            get { return GlobalVar.GlobalCsf[data.MsgOver].ContentString; }
        }
        public string MessageUnderUI
        {
            get { return data.MsgUnder; }
            set
            {
                data.MsgUnder = value;
                SetProperty();
                SetProperty(nameof(MessageUnderValue));
            }
        }
        public string MessageUnderValue
        {
            get { return GlobalVar.GlobalCsf[data.MsgUnder].ContentString; }
        }
        #endregion
    }
}
