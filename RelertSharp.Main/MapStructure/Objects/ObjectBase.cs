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


    public class ObjectItemBase : I2dLocateable, IRegistable
    {
        private bool isSelected = false;


        #region Ctor
        public ObjectItemBase(string _id, string[] _args)
        {
            Id = _id;
            OwnerHouse = _args[0];
            RegName = _args[1];
            HealthPoint = int.Parse(_args[2]);
            X = int.Parse(_args[3]);
            Y = int.Parse(_args[4]);
        }
        public ObjectItemBase(ObjectItemBase src)
        {
            Id = src.Id;
            OwnerHouse = src.OwnerHouse;
            RegName = src.RegName;
            HealthPoint = src.HealthPoint;
            X = src.X;
            Y = src.Y;
        }
        public ObjectItemBase() { }
        #endregion


        #region Public Methods - ObjectItemBase
        public virtual void Select()
        {
            if (!isSelected)
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
            if (filter.OwnerHouse) OwnerHouse = config.OwnerHouse;
            if (filter.HealthPoint) HealthPoint = config.HealthPoint;
            if (filter.Group) Group = config.Group;
            if (filter.MissionStatus) Status = config.MissionStatus;
            if (filter.Tag) TaggedTrigger = config.AttatchedTag;
            if (filter.Veteran) VeterancyPercentage = config.VeterancyPercentage;
            if (filter.Facing) Rotation = config.FacingRotation;
            if (filter.RecruitYes) AutoYESRecruitType = config.AutoRecruitYes;
            if (filter.RecruitNo) AutoNORecruitType = config.AutoRecruitNo;
            if (filter.AboveGround) IsAboveGround = config.AboveGround;
        }
        public virtual int GetHeight()
        {
            return GlobalVar.CurrentMapDocument.Map.GetHeightFromTile(this);
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
        #endregion


        #region Public Calls - ObjectItemBase
        public string Id { get; internal set; } = Constant.ITEM_NONE;
        public string RegName { get; set; } = "(NOTHING)";
        public string OwnerHouse { get; set; } = Constant.ITEM_NONE;
        /// <summary>
        /// Default: 256
        /// </summary>
        public int HealthPoint { get; set; } = 256;
        public string Status { get; set; } = Constant.ITEM_NONE;
        public string TaggedTrigger { get; set; } = Constant.VALUE_NONE;
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
        #endregion


        #region Protected

        #endregion
    }
}
