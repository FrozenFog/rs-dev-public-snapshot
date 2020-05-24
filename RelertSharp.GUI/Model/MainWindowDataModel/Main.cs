using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.FileSystem;
using RelertSharp.MapStructure;
using RelertSharp.MapStructure.Objects;
using RelertSharp.MapStructure.Points;
using RelertSharp.Common;
using RelertSharp.DrawingEngine.Presenting;
using static RelertSharp.Common.GlobalVar;

namespace RelertSharp.GUI.Model
{
    public partial class MainWindowDataModel
    {
        private Map map { get { return CurrentMapDocument.Map; } }
        [Flags]
        public enum MouseActionType
        {
            BoxSelecting = 1,
            AddingObject = 1 << 1,
            TileEditing = 1 << 2,
            AttributeBrush = 1 << 3,
            Moving = 1 << 5,

            None = 0
        }


        #region Ctor - MainWindowDataModel
        public MainWindowDataModel()
        {

        }
        #endregion


        #region Public Methods - MainWindowDataModel
 
        #endregion


        #region Public Calls - MainWindowDataModel
        public MouseActionType CurrentMouseAction { get; set; } = MouseActionType.BoxSelecting;
        public LightningItem LightningItem { get; set; }
        #endregion
    }
}
