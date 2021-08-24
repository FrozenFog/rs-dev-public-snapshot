using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelertSharp.Wpf.ViewModel
{
    internal class LoadingProgressVm : BaseListVm<LoadingProgressVm.LoadingProgressModel>
    {
        public LoadingProgressVm()
        {
            data = new LoadingProgressModel();
        }

        public void Incre()
        {
            data.CurrentCount++;
        }
        public void Report()
        {
            SetProperty(nameof(FormatCount));
            SetProperty(nameof(CurrentCount));
        }


        #region Calls
        public bool CanLoad { get { return data.MaxCount != 0; } }
        public int MaxCount
        {
            get { return data.MaxCount; }
            set
            {
                data.MaxCount = value;
                SetProperty();
                SetProperty(nameof(CanLoad));
            }
        }
        public int CurrentCount
        {
            get { return data.CurrentCount; }
            set
            {
                data.CurrentCount = value;
                SetProperty();
            }
        }
        public string Label
        {
            get { return data.Label; }
            set
            {
                data.Label = value;
                SetProperty();
            }
        }
        public string FormatCount
        {
            get { return string.Format("({0}/{1})", CurrentCount, MaxCount); }
        }
        public int Id { get { return data.RegistId; } set { data.RegistId = value; } }
        #endregion




        internal class LoadingProgressModel
        {
            public int MaxCount { get; set; }
            public int CurrentCount { get; set; }
            public int RegistId { get; set; }
            public string Label { get; set; }
        }
    }
}
