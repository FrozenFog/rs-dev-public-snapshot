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
        TItem AddItem(string id, string name);
        TItem CopyItem(TItem src, string id);
        bool ContainsItem(TItem look);
        bool ContainsItem(string id, string param2);
        bool RemoveItem(TItem target);
    }
    public interface ISubCurdContainer<TItem>
    {
        TItem AddItemAt(int pos);
        void RemoveItemAt(int pos);
        void RemoveAll();
        void MoveItemTo(int from, int to);
        TItem CopyItemAt(int pos);
    }
    public interface IIndexableItem
    {
        string Id { get; set; }
        string Name { get; set; }
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
        void ApplyConfig(IMapObjectBrushConfig config, IObjectBrushFilter filter, bool applyPosAndNameAndName = true);
        void Select();
        void CancelSelection();
        string Id { get; }
    }

    public interface ICombatObject : IMapObject
    {
        string OwnerHouse { get; set; }
        int HealthPoint { get; set; }
        string Status { get; set; }
        string TaggedTrigger { get; set; }
        int Rotation { get; set; }
        int VeterancyPercentage { get; set; }
        string Group { get; set; }
    }

    public interface IMapTileBrushConfig
    {
        I3dLocateable Pos { get; }
        int TileIndex { get; }
        byte TileSubIndex { get; }
        byte IceGrowth { get; }
        /// <summary>
        /// Return a virtual tile, no scene object
        /// </summary>
        /// <returns></returns>
        Tile ComposeTile();

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
        string FollowsIndex { get; }
        /// <summary>
        /// Default true
        /// </summary>
        bool IsSellable { get; }
        bool AiRebuildable { get; }
        /// <summary>
        /// Default true
        /// </summary>
        bool Powered { get; }
        int UpgradeNum { get; }
        string Upg1 { get; }
        string Upg2 { get; }
        string Upg3 { get; }
        bool AiRepairable { get; }
        BuildingSpotlightType SpotlightType { get; }

        string WaypointNum { get; }
        byte OverlayIndex { get; }
        byte OverlayFrame { get; }
    }

    public interface IObjectBrushFilter
    {
        bool AboveGround { get; }
        bool AiRepairable { get; }
        bool Facing { get; }
        bool Follows { get; }
        bool Group { get; }
        bool HealthPoint { get; }
        bool MissionStatus { get; }
        bool OwnerHouse { get; }
        bool Powered { get; }
        bool Rebuild { get; }
        bool RecruitNo { get; }
        bool RecruitYes { get; }
        bool Sellable { get; }
        bool Spotlight { get; }
        bool Tag { get; }
        bool Upg1 { get; }
        bool Upg2 { get; }
        bool Upg3 { get; }
        bool UpgradeNum { get; }
        bool Veteran { get; }
    }
}
