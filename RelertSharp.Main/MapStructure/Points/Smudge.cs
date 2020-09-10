using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.Common;
using RelertSharp.DrawingEngine.Presenting;

namespace RelertSharp.MapStructure.Points
{
    public class SmudgeLayer : PointCollectionBase<SmudgeItem>
    {
        public SmudgeLayer() { }
    }


    public class SmudgeItem : PointItemBase, IMapObject
    {
        private int szX = -1, szY = -1;
        public SmudgeItem(string _name, int x, int y, bool _ignore) : base(x, y)
        {
            RegName = _name;
            IgnoreSmudge = _ignore;
            ObjectType = MapObjectType.Smudge;
        }
        public SmudgeItem(string regname)
        {
            RegName = regname;
            ObjectType = MapObjectType.Smudge;
        }
        public SmudgeItem(SmudgeItem src) : base(src)
        {
            RegName = src.RegName;
            ObjectType = MapObjectType.Smudge;
        }


        #region Public Methods - SmudgeItem
        public IMapObject CopyNew()
        {
            SmudgeItem smg = new SmudgeItem(this);
            return smg;
        }
        #endregion


        #region Public Calls - SmudgeItem
        public new string RegName { get; set; }
        public bool IgnoreSmudge { get; set; }
        public int SizeX
        {
            get
            {
                if (szX == -1)
                {
                    GlobalVar.GlobalRules.GetSmudgeSizeData(RegName, out int x, out int y);
                    szX = x;
                    szY = y;
                }
                return szX;
            }
            set { szX = value; }
        }
        public int SizeY
        {
            get
            {
                if (szY == -1)
                {
                    GlobalVar.GlobalRules.GetSmudgeSizeData(RegName, out int x, out int y);
                    szX = x;
                    szY = y;
                }
                return szY;
            }
            set { szY = value; }
        }
        public new PresentMisc SceneObject { get { return (PresentMisc)base.SceneObject; } set { base.SceneObject = value; } }
        IPresentBase IMapScenePresentable.SceneObject { get { return base.SceneObject; } set { base.SceneObject = value; } }

        public IEnumerable<object> SaveData
        {
            get
            {
                return new List<object>
                {
                    RegName, X, Y, 0
                };
            }
        }
        #endregion
    }
}
