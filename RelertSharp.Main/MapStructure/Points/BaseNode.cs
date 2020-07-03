using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.Common;
using RelertSharp.DrawingEngine.Presenting;
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
                SceneObject.MarkSelected();
            }
        }

        public void UnSelect()
        {
            if (Selected)
            {
                Selected = false;
                SceneObject.Unmark();
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
        public bool IsHidden { get; private set; }
        public string RegName { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Coord { get { return Utils.Misc.CoordInt(this); } }
        public bool Selected { get; set; }
        public PresentStructure SceneObject { get; set; }
        IPresentBase IMapScenePresentable.SceneObject { get { return SceneObject; } set { SceneObject = (PresentStructure)value; } }
        #endregion
    }
}
