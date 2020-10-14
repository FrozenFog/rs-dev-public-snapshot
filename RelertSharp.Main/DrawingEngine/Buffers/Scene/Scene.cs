using RelertSharp.DrawingEngine.Drawables;
using RelertSharp.DrawingEngine.Presenting;
using System.Collections.Generic;
using System.Linq;

namespace RelertSharp.DrawingEngine
{
    internal partial class BufferCollection
    {
        public partial class CScene
        {
            #region Ctor - Scene
            public CScene() { }
            #endregion




            #region ViewPort
            public void ResetSelectingRectangle()
            {
                foreach (int line in RectangleLines)
                {
                    if (line != 0) CppExtern.ObjectUtils.RemoveCommonFromScene(line);
                }
                RectangleLines.Clear();
            }
            #endregion

            #region Public Calls - Scene
            public List<int> RectangleLines { get; private set; } = new List<int>();
            #endregion
        }
    }

}
