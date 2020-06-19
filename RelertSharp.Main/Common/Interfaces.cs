using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.DrawingEngine.Presenting;
using RelertSharp.MapStructure.Objects;

namespace RelertSharp.Common
{
    public interface ILogicItem
    {
        string ID { get; }
        string Name { get; }
    }

    public interface IRegistable
    {
        string RegName { get; }
    }

    public interface I2dLocateable
    {
        int X { get; set; }
        int Y { get; set; }
        int Coord { get; }
    }


    public interface I3dLocateable : I2dLocateable
    {
        int Z { get; set; }
    }

    public interface IMapScenePresentable : I2dLocateable
    {
        bool Selected { get; set; }
        void MoveTo(I3dLocateable pos);
        void ShiftBy(I3dLocateable delta);
        void Select();
        void UnSelect();
        void Dispose();
        IPresentBase SceneObject { get; set; }
    }


    public interface IMapObject : IMapScenePresentable, IRegistable
    {
        void Hide();
        void Reveal();
    }

    public interface IMapMiscObject : I2dLocateable, IMapScenePresentable
    {

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
        void ApplyAttributeFrom(ICombatObject src);
    }
}
