using System.Collections.Generic;
using System.Linq;

namespace RelertSharp.Engine
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
