using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelertSharp.MapStructure.Objects
{
    public class ObjectBase
    {
        private Dictionary<string, ObjectItemBase> data = new Dictionary<string, ObjectItemBase>();
        public ObjectBase() { }


        #region Public Calls - ObjectBase
        public ObjectItemBase this[string _id]
        {
            get
            {
                if (data.Keys.Contains(_id)) return data[_id];
                return new ObjectItemBase();
            }
            set
            {
                data[_id] = value;
            }
        }
        #endregion
    }

    public class ObjectItemBase
    {
        public ObjectItemBase(string _id, string[] _args)
        {

        }
        public ObjectItemBase() { }


        #region Public Calls - ObjectItemName
        public string ID { get; set; }
        public string NameID { get; set; }
        public string OwnerHouse { get; set; }
        public int HealthPoint { get; set; }
        public string Status { get; set; }
        public string TaggedTrigger { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Rotation { get; set; }
        public int VeterancyPercentage { get; set; }
        public int Group { get; set; }
        public bool IsAboveGround { get; set; }
        public bool AutoNORecruitType { get; set; }
        public bool AutoYESRecruitType { get; set; }
        #endregion
    }
}
