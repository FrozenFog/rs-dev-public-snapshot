﻿using RelertSharp.Common;

namespace RelertSharp.MapStructure
{
    public interface ISceneObject : I3dLocateable
    {
        Vec4 ColorVector { get; }
        void Dispose();
        void MoveTo(I3dLocateable pos);
        void Hide();
        void Reveal();
        void ShiftBy(I3dLocateable delta);
        void SetColor(Vec4 color);
        void ApplyTempColor(Vec4 color);
        void RemoveTempColor();
    }
    public interface ISceneOverlay : ISceneObject
    {
        bool IsWall { get; }
        bool IsHiBridge { get; }
        bool IsTiberiumOverlay { get; }
        bool IsRubble { get; }
        bool IsMoveBlockingOverlay { get; }
    }
    public interface ISceneTile : ISceneObject
    {
        bool WaterPassable { get; }
        bool LandPassable { get; }
        bool Buildable { get; }
        bool Lamped { get; set; }
        void SwitchToFramework(bool enable);
        void MarkSelf(Vec4 color, bool unSelect = false);
        void MarkExtra(Vec4 color, bool unSelect = false);
        void HideSelf();
        void RevealSelf();
        void HideExtra();
        void RevealExtra();
        void RedrawTile(Tile t);
    }
}