using RelertSharp.Common;
using RelertSharp.IniSystem;
using System.Collections.Generic;

namespace RelertSharp.MapStructure.Points
{
    [IniEntitySerialize(Constant.MapStructure.ENT_WP)]
    public class WaypointCollection : PointCollectionBase<WaypointItem>
    {
        private HashSet<string> wpnum = new HashSet<string>();


        public WaypointCollection() { }
        protected override WaypointItem InvokeCtor()
        {
            return new WaypointItem();
        }

        public WaypointItem FindByID(string id)
        {
            foreach (WaypointItem item in this)
            {
                if (item.Num == id) return item;
            }
            return null;
        }
        public WaypointItem FindByAlphabet(string alphabetId)
        {
            if (int.TryParse(alphabetId, out int i)) return FindByID(i.ToString());
            string id = Utils.Misc.WaypointInt(alphabetId).ToString();
            return FindByID(id);
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
        public override void AddObject(WaypointItem item, bool forceRenewId = false)
        {
            base.AddObject(item, forceRenewId);
            wpnum.Add(item.Id);
        }
        public int NewID()
        {
            for (int i = 0; i < int.MaxValue; i++)
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
            ObjectType = MapObjectType.Waypoint;
        }
        public WaypointItem(I2dLocateable pos, int index) : base(pos)
        {
            Num = index.ToString();
            ObjectType = MapObjectType.Waypoint;
        }
        public WaypointItem(I2dLocateable pos, string index) : base(pos)
        {
            Num = index;
            ObjectType = MapObjectType.Waypoint;
        }
        internal WaypointItem()
        {
            ObjectType = MapObjectType.Waypoint;
        }
        public override void ReadFromIni(INIPair src)
        {
            Id = src.Name;
            if (!base.ReadCoord(src.Name)) GlobalVar.Monitor.LogCritical(Id, string.Empty, ObjectType, this);
        }
        public override INIPair SaveAsIni()
        {
            INIPair p = new INIPair(Id);
            p.Value = CoordString;
            return p;
        }

        #region Public Methods
        public override string ToString()
        {
            return string.Format("{0} - ({1}, {2})", Num, X, Y);
        }
        public override void ApplyConfig(IMapObjectBrushConfig config, IObjectBrushFilter filter, bool applyPosAndName = true)
        {
            base.ApplyConfig(config, filter, applyPosAndName);
            if (applyPosAndName) Num = config.WaypointNum;
        }
        public string[] ExtractParameter()
        {
            return new string[]
            {
                Num,
                X.ToString(),
                Y.ToString()
            };
        }
        public int GetChecksum()
        {
            unchecked
            {
                int hash = Constant.BASE_HASH;
                hash = hash * 11 + X;
                hash = hash * 11 + Y;
                hash = hash * 11 + Num.GetHashCode();
                return hash;
            }
        }
        public IMapObject ConstructFromParameter(string[] command)
        {
            ParameterReader reader = new ParameterReader(command);
            WaypointItem wp = new WaypointItem()
            {
                Num = reader.ReadString(),
                X = reader.ReadInt(),
                Y = reader.ReadInt()
            };
            return wp;
        }
        #endregion


        #region Public Calls - WaypointItem
        public override string Value { get { return Utils.Misc.WaypointString(int.Parse(Num)); } }
        public string Num { get { return Id; } set { Id = value; } }
        #endregion
    }
}
