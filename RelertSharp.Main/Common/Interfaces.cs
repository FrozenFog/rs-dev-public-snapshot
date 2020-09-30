using RelertSharp.DrawingEngine.Presenting;
using RelertSharp.MapStructure.Objects;
using System.Collections.Generic;

namespace RelertSharp.Common
{
    public interface IGlobalIdContainer
    {
        bool HasId(string id);
        IEnumerable<string> AllId { get; }
    }
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
    public static class ExtensionLocateable
    {
        public static string FormatXY(this I2dLocateable pos)
        {
            return string.Format("X: {0}, Y: {1}", pos.X, pos.Y);
        }
        public static string FormatXYZ(this I3dLocateable pos)
        {
            return string.Format("X: {0}, Y: {1}, Z: {2}", pos.X, pos.Y, pos.Z);
        }
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
