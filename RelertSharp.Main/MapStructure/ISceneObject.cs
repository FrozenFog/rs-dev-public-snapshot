using RelertSharp.Common;

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
    }
    public interface ISceneTile : ISceneObject
    {
        bool WaterPassable { get; }
        bool LandPassable { get; }
        bool Buildable { get; }
        void SwitchToFramework(bool enable);
    }
}
