using RelertSharp.MapStructure.Objects;
using System.Collections.Generic;
using RelertSharp.MapStructure;

namespace RelertSharp.Common
{
    public interface IGlobalIdContainer
    {
        int Length { get; }
        bool HasId(string id);
        IEnumerable<string> AllId { get; }
        void AscendingSort();
        void DescendingSort();
        void ChangeDisplay(IndexableDisplayType type);
    }
    public interface IIndexableItem
    {
        string Id { get; }
        string Name { get; }
        void ChangeDisplay(IndexableDisplayType type);
        string Value { get; }
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

    }


    public interface IMapObject : IRegistable, I2dLocateable
    {
        ISceneObject SceneObject { get; }
        MapObjectType ObjectType { get; }
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
