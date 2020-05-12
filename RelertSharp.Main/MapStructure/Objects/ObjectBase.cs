using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.IniSystem;
using RelertSharp.Common;

namespace RelertSharp.MapStructure.Objects
{
    public class ObjectBase<T> : IEnumerable<T> where T : ObjectItemBase
    {
        private Dictionary<string, T> data = new Dictionary<string, T>();
        public ObjectBase() { }


        #region Public Methods - ObjectBase
        public virtual T FindByCoord(I2dLocateable src)
        {
            foreach (T item in data.Values)
            {
                if (item.X == src.X && item.Y == src.Y) return item;
            }
            return null;
        }
        public virtual void RemoveByCoord(I2dLocateable src)
        {
            Dictionary<string, T> tmp = new Dictionary<string, T>(data);
            foreach (T item in tmp.Values)
            {
                if (item.X == src.X && item.Y == src.Y)
                {
                    data.Remove(item.ID);
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
                return default(T);
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


    public class ObjectItemBase : I2dLocateable, IMapObject
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
        public ObjectItemBase() { }
        #endregion


        #region Public Methods - ObjectItemBase
        public virtual void ApplyAttributeFrom(ICombatObject obj, AttributeChanger ckb)
        {
            if (ckb.bOwnerHouse) OwnerHouse = obj.OwnerHouse;
            if (ckb.bHealthPoint) HealthPoint = obj.HealthPoint;
            if (ckb.bGroup) Group = obj.Group;
            if (ckb.bStatus) Status = obj.Status;
            if (ckb.bTaggedTrigger) TaggedTrigger = obj.TaggedTrigger;
            if (ckb.bVeteran) VeterancyPercentage = obj.VeterancyPercentage;
            if (ckb.bRotation) Rotation = obj.Rotation;
        }
        #endregion


        #region Public Calls - ObjectItemBase
        public string ID { get; set; } = "NULL";
        public string RegName { get; set; } = "(NOTHING)";
        public string OwnerHouse { get; set; } = "<none>";
        public int HealthPoint { get; set; } = 256;
        public string Status { get; set; } = "<none>";
        public string TaggedTrigger { get; set; } = "<none>";
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
        #endregion
    }
}
