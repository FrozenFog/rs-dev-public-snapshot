using RelertSharp.Common;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RelertSharp.MapStructure.Objects
{
    public class ObjectBase<T> : IEnumerable<T> where T : ObjectItemBase
    {
        private Dictionary<string, T> data = new Dictionary<string, T>();
        public ObjectBase() { }


        #region Public Methods - ObjectBase
        public virtual void RemoveByID(string id)
        {
            if (data.Keys.Contains(id)) data.Remove(id);
        }
        /// <summary>
        /// Only use this to generate id
        /// </summary>
        /// <param name="item"></param>
        public virtual void AddItem(T item)
        {
            if (item.Id != Constant.ITEM_NONE) data[item.Id] = item;
            else
            {
                for (int i = 0; i < 10000; i++)
                {
                    if (!data.Keys.Contains(i.ToString()))
                    {
                        item.Id = i.ToString();
                        data[item.Id] = item;
                        return;
                    }
                }
            }
        }
        #endregion


        #region Protected
        protected Dictionary<string, T> GetDictionary()
        {
            return data;
        }
        protected void RemoveItem(T item)
        {
            data.Remove(item.Id);
        }
        #endregion


        #region Public Calls - ObjectBase
        public T this[string _id]
        {
            get
            {
                if (data.Keys.Contains(_id)) return data[_id];
                return null;
            }
            set
            {
                data[_id] = value;
            }
        }
        #region Enumerator
        public IEnumerator<T> GetEnumerator()
        {
            return data.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return data.Values.GetEnumerator();
        }
        #endregion
        #endregion
    }


    public class ObjectItemBase : I2dLocateable, IRegistable, ITaggableObject, IIndexableItem
    {
        private bool isSelected = false;


        #region Ctor
        public ObjectItemBase(string _id, string[] _args)
        {
            Id = _id;
            Owner = _args[0];
            RegName = _args[1];
            HealthPoint = int.Parse(_args[2]);
            X = int.Parse(_args[3]);
            Y = int.Parse(_args[4]);
        }
        public ObjectItemBase(ObjectItemBase src)
        {
            Id = src.Id;
            Owner = src.Owner;
            RegName = src.RegName;
            HealthPoint = src.HealthPoint;
            X = src.X;
            Y = src.Y;
        }
        public ObjectItemBase() { }
        #endregion


        #region Public Methods - ObjectItemBase
        public virtual void Select(bool force = false)
        {
            if (force || !isSelected)
            {
                SceneObject.ApplyTempColor(Vec4.Selector);
                isSelected = true;
            }
        }
        public virtual void CancelSelection()
        {
            if (isSelected)
            {
                SceneObject.RemoveTempColor();
                isSelected = false;
            }
        }
        public virtual void MoveTo(I3dLocateable pos, int subcell = -1)
        {
            X = pos.X;
            Y = pos.Y;
            SceneObject?.MoveTo(pos, subcell);
        }
        public void ShiftBy(I3dLocateable delta)
        {
            X += delta.X;
            Y += delta.Y;
            SceneObject?.ShiftBy(delta);
        }
        public virtual void ApplyConfig(IMapObjectBrushConfig config, IObjectBrushFilter filter, bool applyPosAndName = false)
        {
            if (applyPosAndName)
            {
                RegName = config.RegName;
                X = config.Pos.X;
                Y = config.Pos.Y;
            }
            if (filter.Owner) Owner = config.Owner;
            if (filter.HealthPoint) HealthPoint = config.HealthPoint;
            if (filter.Group) Group = config.Group;
            if (filter.MissionStatus) Status = config.MissionStatus;
            if (filter.Tag) TagId = config.AttatchedTag;
            if (filter.Veteran) VeterancyPercentage = config.VeterancyPercentage;
            if (filter.Facing) Rotation = config.FacingRotation;
            if (filter.RecruitYes) AutoYESRecruitType = config.AutoRecruitYes;
            if (filter.RecruitNo) AutoNORecruitType = config.AutoRecruitNo;
            if (filter.AboveGround) IsAboveGround = config.AboveGround;
        }
        public virtual int GetHeight(Map source = null)
        {
            if (source != null) return source.GetHeightFromTile(this);
            else if (GlobalVar.HasMap) return GlobalVar.GlobalMap.GetHeightFromTile(this);
            else return Constant.MapStructure.INVALID_HEIGHT;
        }
        public virtual void Dispose()
        {
            isSelected = false;
            SceneObject?.Dispose();
        }
        public override string ToString()
        {
            return string.Format("{0} at {1},{2}", RegName, X, Y);
        }

        public void ChangeDisplay(IndexableDisplayType type)
        {
            // do nothing
        }
        #endregion


        #region Public Calls - ObjectItemBase
        public string Id { get; internal set; } = Constant.ITEM_NONE;
        public string RegName { get; set; } = "(NOTHING)";
        public string Owner { get; set; } = Constant.ITEM_NONE;
        /// <summary>
        /// Default: 256
        /// </summary>
        public int HealthPoint { get; set; } = 256;
        public string Status { get; set; } = Constant.ITEM_NONE;
        public string TagId { get; set; } = Constant.VALUE_NONE;
        public int X { get; set; } = 0;
        public int Y { get; set; } = 0;
        public int Coord
        {
            get { return Utils.Misc.CoordInt(X, Y); }
            set
            {
                X = Utils.Misc.CoordIntX(value);
                Y = Utils.Misc.CoordIntY(value);
            }
        }
        public int Rotation { get; set; } = 0;
        /// <summary>
        /// Default: 100
        /// </summary>
        public int VeterancyPercentage { get; set; } = 100;
        /// <summary>
        /// Default: -1
        /// </summary>
        public string Group { get; set; } = Constant.ID_INVALID;
        public bool IsAboveGround { get; set; } = false;
        public bool AutoNORecruitType { get; set; } = false;
        /// <summary>
        /// Default: true
        /// </summary>
        public bool AutoYESRecruitType { get; set; } = true;
        public MapObjectType ObjectType { get; protected set; } = MapObjectType.Undefined;
        public virtual ISceneObject SceneObject { get; set; }
        string IIndexableItem.Id { get { return Id; } set { } }
        public virtual string Name
        {
            get
            {
                if (GlobalVar.GlobalRules != null) return GlobalVar.GlobalRules[RegName].GetString(Constant.KEY_NAME);
                else return string.Empty;
            }
            set { }
        }

        public string Value { get { return RegName; } }
        public bool IsSelected { get { return isSelected; } }
        #endregion


        #region Protected

        #endregion
    }

    public sealed class VirtualMapObject : ObjectItemBase, IMapObject
    {
        public IMapObject ConstructFromParameter(string[] commands)
        {
            return new VirtualMapObject();
        }

        public string[] ExtractParameter()
        {
            return new string[0];
        }
    }
}
