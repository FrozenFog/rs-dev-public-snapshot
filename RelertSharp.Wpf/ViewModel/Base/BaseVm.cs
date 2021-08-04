using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Reflection;

namespace RelertSharp.Wpf.ViewModel
{
    internal abstract class BaseVm<Tdata> : INotifyPropertyChanged where Tdata : class
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event ContentCarrierHandler NameChanged;
        protected Tdata data;

        public BaseVm(object obj = null)
        {
            data = obj as Tdata;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        protected virtual void OnDesignatedPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        protected virtual void OnNameChanged()
        {
            NameChanged?.Invoke(this, data);
        }
        protected virtual void OnAllPropertyChanged()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
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
        protected virtual void AutoUpdateAll(Type src, string group = null)
        {
            foreach (var info in src.GetProperties())
            {
                if (info.GetCustomAttribute<AutoUpdateAttribute>() is AutoUpdateAttribute auto)
                {
                    if (group != null)
                    {
                        if (auto.GroupName == group) OnPropertyChanged(info.Name);
                    }
                    else OnPropertyChanged(info.Name);
                }
            }
        }
    }

    internal class AutoUpdateAttribute : Attribute
    {
        public AutoUpdateAttribute(string groupname = null)
        {
            GroupName = groupname;
        }
        public string GroupName { get; private set; }
    }
}
