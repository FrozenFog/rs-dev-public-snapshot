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

    public interface IOwnableObject
    {
        string Owner { get; set; }
    }

    public interface IGroupable
    {
        string Group { get; set; }
    }

    public interface ITaggableObject
    {
        string TagId { get; set; }
    }

    public interface I2dLocateable
    {
        int X { get; set; }
        int Y { get; set; }
        int Coord { get; }
    }

    public interface IExtractableObject
    {
        string[] ExtractParameter();
    }

    public interface IAbstractObjectDescriber : IBaseObject, IRegistable
    {

    }

    public interface IBaseObject
    {
        MapObjectType ObjectType { get; }
    }

    public interface ILogicItem : IExtractableObject
    {
        LogicType ItemType { get; }
    }
    
    public interface IPosition : I2dLocateable
    {
        int SubCell { get; }
    }


    public interface I3dLocateable : I2dLocateable
    {
        int Z { get; set; }
    }

    public interface ITile : I3dLocateable
    {
        int TileIndex { get; set; }
        byte SubIndex { get; set; }
    }

    public interface IOverlay
    {
        byte OverlayIndex { get; set; }
        byte OverlayFrame { get; set; }
    }

    public interface IMapObject : I2dLocateable, IAbstractObjectDescriber, IExtractableObject
    {
        ISceneObject SceneObject { get; }
        int GetHeight(Map source = null);
        void Dispose();
        void MoveTo(I3dLocateable pos, int subcell = -1);
        void ShiftBy(I3dLocateable delta);
        void ApplyConfig(IMapObjectBrushConfig config, IObjectBrushFilter filter, bool applyPosAndNameAndName = true);
        void Select(bool force = false);
        void CancelSelection();
        void Hide(bool force = false);
        void Reveal();
        IMapObject ConstructFromParameter(string[] commands);
        string Id { get; }
        bool IsSelected { get; }
    }

    public interface ICombatObject : IMapObject, IGroupable, IOwnableObject
    {
        new string Owner { get; set; }
        int HealthPoint { get; set; }
        string Status { get; set; }
        string TagId { get; set; }
        int Rotation { get; set; }
        int VeterancyPercentage { get; set; }
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
    public interface IMapCreationConfig
    {
        int Width { get; }
        int Height { get; }
        int Altitude { get; }
        string TheaterKey { get; }
        bool UseDefaultHouse { get; }
        string MapName { get; }
        bool IsSinglePlayer { get; }
        string PlayerHouseName { get; }
    }
    public interface IMapObjectBrushConfig : IOverlay
    {
        string Owner { get; }
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
        bool Owner { get; }
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
