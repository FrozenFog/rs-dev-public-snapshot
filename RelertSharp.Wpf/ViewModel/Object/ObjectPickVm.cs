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
        public ObjectPickVm(string title, string regname, byte overlayIndex, byte overlayData = 0)
        {
            RegName = regname;
            Title = title;
            OverlayIndex = overlayIndex;
            OverlayData = overlayData;
            Type = MapObjectType.Overlay;
        }

        #region Methods
        public void SetIcon(object src)
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
        public override bool IsTree => Type == MapObjectType.Undefined;
        public bool IsFavourite { get; set; }
        public bool IsFavRoot { get; set; }
        public int WaypointIndex { get; set; }
        public bool AssignWaypoint { get; set; }
        public byte OverlayIndex { get; private set; }
        public byte OverlayData { get; private set; }
        public override string Title { get; }
        public string RegName { get; private set; }
        public object Icon { get; private set; }
        public MapObjectType Type { get; private set; } = MapObjectType.Undefined;
        public ImageSource OverlayImage { get; internal set; }
        public int ImgWidth { get; internal set; }
        public int ImgHeight { get; internal set; }
        public bool CanRandomize
        {
            get
            {
                if (Type == MapObjectType.Overlay)
                {
                    if (Title.IsNullOrEmpty()) return true;
                    return false;
                }
                return (Type & (MapObjectType.CombatObject | MapObjectType.MiscObject)) != 0;
            }
        }
        public bool CanFavourite
        {
            get
            {
                if (IsFavourite || IsTree) return false;
                if (Type == MapObjectType.Overlay) return false;
                return (Type & (MapObjectType.CombatObject | MapObjectType.MiscObject)) != 0;
            }
        }
        public bool CanRemoveFromFav
        {
            get
            {
                if (IsFavourite) return !IsFavRoot;
                return false;
            }
        }
        #endregion
    }
}
