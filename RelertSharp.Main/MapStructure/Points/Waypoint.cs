﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.Common;
using RelertSharp.DrawingEngine.Presenting;

namespace RelertSharp.MapStructure.Points
{
    public class WaypointCollection : PointCollectionBase<WaypointItem>
    {
        private HashSet<string> wpnum = new HashSet<string>();


        public WaypointCollection() { }

        
        public WaypointItem FindByID(string id)
        {
            foreach (WaypointItem item in this)
            {
                if (item.Num == id) return item;
            }
            return null;
        }
        public WaypointItem FindByPos(I2dLocateable pos)
        {
            foreach (WaypointItem item in this)
            {
                if (item.Coord == pos.Coord) return item;
            }
            return null;
        }
        public bool IsValidNum(int num)
        {
            return !wpnum.Contains(num.ToString());
        }
        public override void AddObject(WaypointItem item)
        {
            wpnum.Add(item.ID);
            base.AddObject(item);
        }
        public int NewID()
        {
            for (int i = 0; i < 701; i++)
            {
                if (!wpnum.Contains(i.ToString()))
                {
                    return i;
                }
            }
            throw new RSException.InvalidWaypointException(-1);
        }
        public void RemoveWaypoint(WaypointItem wp)
        {
            if (wpnum.Contains(wp.Num))
            {
                data.Remove(wp.Num);
                wpnum.Remove(wp.Num);
            }
        }
    }


    public class WaypointItem : PointItemBase, IMapObject
    {
        public WaypointItem(string _coord, string _index) : base(_coord)
        {
            Num = _index;
        }
        public WaypointItem(I2dLocateable pos, int index) : base(pos)
        {
            Num = index.ToString();
        }


        #region Public Calls - WaypointItem
        public override string ToString()
        {
            return string.Format("{0} - ({1}, {2})", Num, X, Y);
        }
        public string Num { get { return ID; }set { ID = value; } }
        public new PresentMisc SceneObject { get { return (PresentMisc)base.SceneObject; } set { base.SceneObject = value; } }
        IPresentBase IMapScenePresentable.SceneObject { get { return base.SceneObject; } set { base.SceneObject = value; } }
        #endregion
    }
}
