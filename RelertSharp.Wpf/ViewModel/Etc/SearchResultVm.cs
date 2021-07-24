using RelertSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelertSharp.Wpf.ViewModel
{
    internal class SearchResultVm : BaseListVm<object>
    {
        private const string NAN_ITEM = "N/A";
        private static MapStructure.Map map { get { return GlobalVar.GlobalMap; } }
        public SearchResultVm(object result)
        {
            data = result;
        }


        #region Public
        public string GenerateReport()
        {
            return string.Empty;
        }
        #endregion
        #region Calls


        #region Bind Calls
        public string Id
        {
            get
            {
                if (data is IIndexableItem idx) return idx.Id;
                return NAN_ITEM;
            }
        }
        public string Type
        {
            get
            {
                if (data is IBaseObject obj) return obj.ObjectType.ToString();
                if (data is ILogicItem item) return item.ItemType.ToString();
                return NAN_ITEM;
            }
        }
        public string Name
        {
            get
            {
                if (data is IIndexableItem idx) return idx.Name;
                return NAN_ITEM;
            }
        }
        public string Owner
        {
            get
            {
                if (data is IOwnableObject o) return o.Owner;
                return NAN_ITEM;
            }
        }
        public string PosX
        {
            get
            {
                if (data is I2dLocateable pos) return pos.X.ToString();
                return NAN_ITEM;
            }
        }
        public string PosY
        {
            get
            {
                if (data is I2dLocateable pos) return pos.Y.ToString();
                return NAN_ITEM;
            }
        }
        public string PosZ
        {
            get
            {
                if (data is I2dLocateable pos)
                {
                    if (map != null) return map.GetHeightFromTile(pos).ToString();
                }
                return NAN_ITEM;
            }
        }
        public string TagId
        {
            get
            {
                if (data is ITaggableObject tag) return tag.TagId;
                return NAN_ITEM;
            }
        }
        public string Health
        {
            get
            {
                if (data is ICombatObject com) return com.HealthPoint.ToString();
                return NAN_ITEM;
            }
        }
        public string Facing
        {
            get
            {
                if (data is ICombatObject com) return com.Rotation.ToString();
                return NAN_ITEM;
            }
        }
        #endregion
        #endregion
    }
}
