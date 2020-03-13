using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RelertSharp.Utils.Misc;

namespace RelertSharp.MapStructure.Objects
{
    public class AircraftLayer : ObjectBase
    {
        public AircraftLayer() { }
    }


    public class AircraftItem : ObjectItemBase
    {
        public AircraftItem(string _id, string[] _args) : base(_id, _args)
        {
            ID = _id;
            OwnerHouse = _args[0];
            NameID = _args[1];
            HealthPoint = int.Parse(_args[2]);
            X = int.Parse(_args[3]);
            Y = int.Parse(_args[4]);
            Rotation = int.Parse(_args[5]);
            Status = _args[6];
            TaggedTrigger = _args[7];
            VeterancyPercentage = int.Parse(_args[8]);
            Group = int.Parse(_args[9]);
            AutoNORecruitType = ParseBool(_args[10]);
            AutoYESRecruitType = ParseBool(_args[11]);
        }
    }
}
