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

        #region Public 
        public virtual void UpdateAll()
        {
            OnReset();
        }
        public virtual void UpdateDesignatedName()
        {
            OnReset();
        }
        public virtual void UpdateName(Titem item, Titem old, int visualIndex)
        {
            OnUpdate(item, old, visualIndex);
        }
        #endregion


        #region Protected
        protected virtual void OnAdd(Titem item, int visualIndex)
        {
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, visualIndex));
        }

        protected virtual void OnDelete(Titem item, int visualIndex)
        {
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item, visualIndex));
        }

        protected virtual void OnUpdate(Titem item, Titem old, int visualIndex)
        {
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, item, old, visualIndex));
        }

        protected virtual void OnReset()
        {
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
        #endregion

        protected static IEnumerator EmptyEnumerator { get { return new List<object>().GetEnumerator(); } }
    }
}
