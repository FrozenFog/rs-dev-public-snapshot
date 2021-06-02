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
    public interface ICurdContainer<TItem>
    {
        TItem AddItem(string param1, string param2);
        TItem CopyItem(TItem src, string param);
        bool ContainsItem(TItem look);
        bool ContainsItem(string param1, string param2);
        bool RemoveItem(TItem target);
    }
    public interface IIndexableItem
    {
        string Id { get; set; }
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

    public interface IMapObject : IRegistable, I2dLocateable
    {
        ISceneObject SceneObject { get; }
        MapObjectType ObjectType { get; }
        int GetHeight();
        void Dispose();
        void MoveTo(I3dLocateable pos, int subcell = -1);
        void ShiftBy(I3dLocateable delta);
        void ApplyConfig(IMapObjectBrushConfig config);
        string ID { get; }
    }

    public interface ICombatObject : IMapObject
    {
        string ID { get; }
        string OwnerHouse { get; set; }
        int HealthPoint { get; set; }
        string Status { get; set; }
        string TaggedTrigger { get; set; }
        int Rotation { get; set; }
        int VeterancyPercentage { get; set; }
        string Group { get; set; }
        void ApplyAttributeFrom(AttributeChanger ckb);
        void ApplyAttributeFrom(ICombatObject src);
    }

    public interface IMapObjectBrushConfig
    {
        string OwnerHouse { get; }
        string RegName { get; }
        I2dLocateable Pos { get; }
        int Height { get; }
        int SubCell { get; }
        int FacingRotation { get; }
        string AttatchedTag { get; }
        /// <summary>
        /// Area guard, Sleep, etc
        /// </summary>
        string MissionStatus { get; }
        string Group { get; }
        int HealthPoint { get; }
        int VeterancyPercentage { get; }
        bool AboveGround { get; }
        /// <summary>
        /// Default false
        /// </summary>
        bool AutoRecruitYes { get; }
        /// <summary>
        /// Default true
        /// </summary>
        bool AutoRecruitNo { get; }
        string WaypointNum { get; }
        byte OverlayIndex { get; }
        byte OverlayFrame { get; }
    }
}
