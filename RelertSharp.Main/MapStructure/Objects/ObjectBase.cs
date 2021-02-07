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
        public virtual void AddFromBrush(T item)
        {
            for (int i = 0; i < 10000; i++)
            {
                if (!data.Keys.Contains(i.ToString()))
                {
                    item.ID = i.ToString();
                    data[item.ID] = item;
                    return;
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
            data.Remove(item.ID);
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
        #region Ctor
        public ObjectItemBase(string _id, string[] _args)
        {
            ID = _id;
            OwnerHouse = _args[0];
            RegName = _args[1];
            HealthPoint = int.Parse(_args[2]);
            X = int.Parse(_args[3]);
            Y = int.Parse(_args[4]);
        }
        public ObjectItemBase(ObjectItemBase src)
        {
            ID = src.ID;
            OwnerHouse = src.OwnerHouse;
            RegName = src.RegName;
            HealthPoint = src.HealthPoint;
            X = src.X;
            Y = src.Y;
        }
        public ObjectItemBase() { }
        #endregion


        #region Public Methods - ObjectItemBase
        public virtual void MoveTo(I3dLocateable pos)
        {
            X = pos.X;
            Y = pos.Y;
        }
        public void ShiftBy(I3dLocateable delta)
        {
            X += delta.X;
            Y += delta.Y;
        }
        public virtual void ApplyAttributeFrom(AttributeChanger obj)
        {
            ICombatObject host = obj.Host;
            if (obj.bOwnerHouse) OwnerHouse = host.OwnerHouse;
            if (obj.bHealthPoint) HealthPoint = host.HealthPoint;
            if (obj.bGroup) Group = host.Group;
            if (obj.bStatus) Status = host.Status;
            if (obj.bTaggedTrigger) TaggedTrigger = host.TaggedTrigger;
            if (obj.bVeteran) VeterancyPercentage = host.VeterancyPercentage;
            if (obj.bRotation) Rotation = host.Rotation;
        }
        public virtual void ApplyAttributeFrom(ICombatObject src)
        {
            if (src != null)
            {
                AttributeChanger c = new AttributeChanger(true, src);
                ApplyAttributeFrom(c);
            }
        }
        public virtual void Dispose()
        {
            SceneObject?.Dispose();
        }
        public override string ToString()
        {
            return string.Format("{0} at {1},{2}", RegName, X, Y);
        }
        #endregion


        #region Public Calls - ObjectItemBase
        public string ID { get; set; } = "NULL";
        public string RegName { get; set; } = "(NOTHING)";
        public string OwnerHouse { get; set; } = "<none>";
        public int HealthPoint { get; set; } = 256;
        public string Status { get; set; } = "<none>";
        public string TaggedTrigger { get; set; } = "None";
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
        public int VeterancyPercentage { get; set; } = 100;
        public int Group { get; set; } = -1;
        public bool IsAboveGround { get; set; } = false;
        public bool AutoNORecruitType { get; set; } = false;
        public bool AutoYESRecruitType { get; set; } = true;
        public MapObjectType ObjectType { get; protected set; } = MapObjectType.Undefined;
        public virtual ISceneObject SceneObject { get; set; }
        #endregion


        #region Protected

        #endregion
    }
}
