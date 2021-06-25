using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using RelertSharp.Common;

namespace RelertSharp.Wpf.ViewModel
{
    internal class ObjectPickVm : BaseTreeVm<IIndexableItem>
    {
        public ObjectPickVm(string regname, string title, MapObjectType type = MapObjectType.Undefined)
        {
            RegName = regname;
            Title = title;
            Type = type;
        }
        public ObjectPickVm(string title)
        {
            Title = title;
        }

        #region Methods
        public void SetIcon(ImageSource src)
        {
            Icon = src;
        }
        /// <summary>
        /// Regname is Id / Index
        /// </summary>
        /// <returns></returns>
        public IIndexableItem AsIndexable()
        {
            return new ComboItem(RegName);
        }
        #endregion


        #region Calls
        public string Title { get; private set; }
        public string RegName { get; private set; }
        public ImageSource Icon { get; private set; }
        public MapObjectType Type { get; private set; } = MapObjectType.Undefined;
        #endregion
    }
}
