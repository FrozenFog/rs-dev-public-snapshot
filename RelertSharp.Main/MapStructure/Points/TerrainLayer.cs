﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.DrawingEngine.Presenting;
using RelertSharp.Common;

namespace RelertSharp.MapStructure.Points
{
    public class TerrainLayer : PointCollectionBase<TerrainItem>
    {
        public TerrainLayer() { }
    }


    public class TerrainItem : PointItemBase, IMapObject
    {
        public TerrainItem(string _coord, string _name) : base(_coord)
        {
            RegName = _name;
        }
        public TerrainItem(string regname)
        {
            RegName = regname;
        }
        public TerrainItem(TerrainItem src) : base(src)
        {
            RegName = src.RegName;
        }


        #region Public Methods - TerrainItem
        public IMapObject CopyNew()
        {
            TerrainItem terr = new TerrainItem(this);
            return terr;
        }
        #endregion


        #region Public Calls - TerrainItem
        public string RegName { get; set; }
        public new PresentMisc SceneObject { get { return (PresentMisc)base.SceneObject; } set { base.SceneObject = value; } }
        IPresentBase IMapScenePresentable.SceneObject { get { return base.SceneObject; } set { base.SceneObject = value; } }
        #endregion
    }
}
