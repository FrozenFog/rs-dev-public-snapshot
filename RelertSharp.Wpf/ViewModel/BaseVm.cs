using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RelertSharp.Wpf.ViewModel
{
    internal abstract class BaseVm<Tdata> : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected Tdata data;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public virtual void LoadData(Tdata data)
        {
            this.data = data;
        }
        protected virtual bool SetProperty([CallerMemberName]string propertyName = null)
        {
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
