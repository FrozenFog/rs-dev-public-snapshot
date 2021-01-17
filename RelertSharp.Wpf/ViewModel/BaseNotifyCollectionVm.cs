using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;

namespace RelertSharp.Wpf.ViewModel
{
    internal abstract class BaseNotifyCollectionVm<Titem> : INotifyCollectionChanged
    {
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        protected virtual void OnAdd(Titem item, int visualIndex)
        {
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, visualIndex));
        }

        protected virtual void OnDelete(Titem item, int visualIndex)
        {
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item, visualIndex));
        }

        protected virtual void OnUpdate(Titem item, int visualIndex)
        {
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, item, visualIndex));
        }

        protected virtual void OnReset()
        {
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        protected static IEnumerator EmptyEnumerator { get { return new List<object>().GetEnumerator(); } }
    }
}
