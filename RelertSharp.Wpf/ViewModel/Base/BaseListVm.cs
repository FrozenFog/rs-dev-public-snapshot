using RelertSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelertSharp.Wpf.ViewModel
{
    internal abstract class BaseListVm<TData> : BaseVm<TData> where TData : class
    {
        #region Ctor
        public BaseListVm(object obj) : base(obj)
        {

        }
        public BaseListVm()
        {
            
        }
        #endregion


        #region Public
        public virtual void ChangeDisplay(IndexableDisplayType type)
        {
            SetProperty(nameof(Title));
        }
        #endregion


        #region Protected
        protected virtual void SetName(object sender, EventArgs e)
        {
            SetProperty(nameof(Title));
        }
        #endregion


        #region Call
        public virtual string Title { get; }
        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                SetProperty();
            }
        }
        public TData Data { get { return data; } }
        #endregion
    }
}
