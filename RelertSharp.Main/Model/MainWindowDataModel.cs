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
using RelertSharp.DrawingEngine;

namespace RelertSharp.Model
{
    public class MainWindowDataModel
    {
        #region Ctor - MainWindowDataModel
        public MainWindowDataModel() { }
        #endregion


        #region Public Calls - MainWindowDataModel
        public LightningItem LightningItem { get; set; }
        #endregion
    }
}
