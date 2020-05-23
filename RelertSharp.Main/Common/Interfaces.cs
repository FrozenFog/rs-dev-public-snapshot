using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.MapStructure.Objects;

namespace RelertSharp.Common
{
    public interface I2dLocateable
    {
        int X { get; }
        int Y { get; }
        int Coord { get; }
    }


    public interface I3dLocateable : I2dLocateable
    {
        int Z { get; }
    }


    public interface IMapObject : I2dLocateable
    {
        string RegName { get; set; }
    }

    public interface ICombatObject : IMapObject
    {
        string ID { get; set; }
        string OwnerHouse { get; set; }
        int HealthPoint { get; set; }
        string Status { get; set; }
        string TaggedTrigger { get; set; }
        int Rotation { get; set; }
        int VeterancyPercentage { get; set; }
        int Group { get; set; }
        void ApplyAttributeFrom(AttributeChanger ckb);
    }
}
