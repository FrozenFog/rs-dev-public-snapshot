using RelertSharp.Common;
using RelertSharp.MapStructure.Objects;

namespace RelertSharp.MapStructure.Points
{
    public class BaseNode : IMapObject
    {
        public BaseNode(string _name, int _x, int _y)
        {
            RegName = _name;
            X = _x;
            Y = _y;
        }
        public BaseNode(BaseNode src)
        {
            RegName = src.RegName;
            X = src.X;
            Y = src.Y;
        }
        public BaseNode(StructureItem src)
        {
            RegName = src.RegName;
            X = src.X;
            Y = src.Y;
        }


        #region Public Methods - BaseNode
        public void MoveTo(I3dLocateable pos)
        {
            X = pos.X;
            Y = pos.Y;
            SceneObject.MoveTo(pos);
        }
        public void ShiftBy(I3dLocateable delta)
        {
            X += delta.X;
            Y += delta.Y;
            SceneObject.ShiftBy(delta);
        }

        public void Select()
        {
            if (!Selected)
            {
                Selected = true;
                SceneObject.ApplyTempColor(Vec4.Selector);
            }
        }

        public void UnSelect()
        {
            if (Selected)
            {
                Selected = false;
                SceneObject.RemoveTempColor();
            }
        }
        public void Dispose()
        {
            Selected = false;
            SceneObject?.Dispose();
        }
        public void Hide()
        {
            if (!IsHidden)
            {
                SceneObject.Hide();
                IsHidden = true;
            }
        }
        public void Reveal()
        {
            if (IsHidden)
            {
                SceneObject.Reveal();
                IsHidden = false;
            }
        }
        public override string ToString()
        {
            return string.Format("{0} at {1}, {2}", RegName, X, Y);
        }
        #endregion


        #region Public Calls - BaseNode
        public string SaveData { get { return string.Format("{0},{1},{2}", RegName, X, Y); } }
        public bool IsHidden { get; private set; }
        public string RegName { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Coord { get { return Utils.Misc.CoordInt(this); } }
        public bool Selected { get; set; }
        public MapObjectType ObjectType { get { return MapObjectType.BaseNode; } }
        public ISceneObject SceneObject { get; set; }
        #endregion
    }
}
