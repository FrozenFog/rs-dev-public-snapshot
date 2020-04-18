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
    public class ObjectBase<T> : IEnumerable<T>
    {
        private Dictionary<string, T> data = new Dictionary<string, T>();
        public ObjectBase() { }


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

        }
        public ObjectItemBase() { }
        #endregion


        #region Public Methods - ObjectItemBase

        #endregion


        #region Public Calls - ObjectItemBase
        public string ID { get; set; }
        public string RegName { get; set; }
        public string OwnerHouse { get; set; }
        public int HealthPoint { get; set; }
        public string Status { get; set; }
        public string TaggedTrigger { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int CoordInt
        {
            get { return Utils.Misc.CoordInt(X, Y); }
            set
            {
                X = Utils.Misc.CoordIntX(value);
                Y = Utils.Misc.CoordIntY(value);
            }
        }
        public int Rotation { get; set; }
        public int VeterancyPercentage { get; set; }
        public int Group { get; set; }
        public bool IsAboveGround { get; set; }
        public bool AutoNORecruitType { get; set; }
        public bool AutoYESRecruitType { get; set; }
        #endregion
    }
}
