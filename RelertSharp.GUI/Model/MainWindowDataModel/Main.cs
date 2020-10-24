﻿using System;
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
            TileSelecting = 1 << 2,
            AttributeBrush = 1 << 3,
            ArrowInspect = 1 << 4,
            Moving = 1 << 5,
            TileBrush = 1 << 6,
            TileWand = 1 << 7,
            TileBucket = 1 << 8,
            Lightroom = 1 << 9,


            None = 0,
            DrawingMode = AddingObject | TileBrush,
            TileEditingMode = TileWand | TileSelecting | TileBucket
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
        internal UndoRedo UndoRedo { get; private set; } = new UndoRedo();
        #endregion
    }
}
