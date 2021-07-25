using RelertSharp.Common;
using RelertSharp.MapStructure.Logic;
using RelertSharp.MapStructure.Objects;

namespace RelertSharp.MapStructure.Points
{
    public class BaseNode : BaseVisibleObject, IMapObject, IOwnableObject
    {
        public BaseNode(string _name, int _x, int _y, HouseItem parent)
        {
            RegName = _name;
            X = _x;
            Y = _y;
            Parent = parent;
        }
        public BaseNode(BaseNode src)
        {
            RegName = src.RegName;
            X = src.X;
            Y = src.Y;
            Parent = src.Parent;
        }
        internal BaseNode(HouseItem parent)
        {
            Parent = parent;
        }


        #region Public Methods - BaseNode
        public override string ToString()
        {
            return string.Format("{0} at {1}, {2}", RegName, X, Y);
        }

        public void ApplyConfig(IMapObjectBrushConfig config, IObjectBrushFilter filter, bool applyPosAndName = false)
        {
            if (applyPosAndName)
            {
                X = config.Pos.X;
                Y = config.Pos.Y;
                RegName = config.RegName;
            }
        }
        public string[] ExtractParameter()
        {
            return new string[]
            {
                Parent.Name,
                RegName,
                X.ToString(),
                Y.ToString()
            };
        }
        public IMapObject ConstructFromParameter(string [] commands)
        {
            ParameterReader reader = new ParameterReader(commands);
            string parentName = reader.ReadString();
            HouseItem parent = GlobalVar.GlobalMap.Houses.GetHouse(parentName);
            BaseNode node = new BaseNode(parent)
            {
                RegName = reader.ReadString(),
                X = reader.ReadInt(),
                Y = reader.ReadInt()
            };
            return node;
        }
        #endregion


        #region Public Calls - BaseNode
        /// <summary>
        /// Basenode won't have id, return null anyway
        /// </summary>
        public override string Id { get { return null; } }
        public override string Name
        {
            get
            {
                if (GlobalVar.GlobalRules != null) return GlobalVar.GlobalRules[RegName].GetString(Constant.KEY_NAME);
                return string.Empty;
            }
        }
        public HouseItem Parent { get; private set; }
        public string SaveData { get { return string.Format("{0},{1},{2}", RegName, X, Y); } }
        public string RegName { get; set; }
        public string Owner
        {
            get { if (Parent != null) return Parent.Name; return string.Empty; }
            set { }
        }
        public override int X { get; set; }
        public override int Y { get; set; }
        public override int Coord { get { return Utils.Misc.CoordInt(this); } }
        public override MapObjectType ObjectType { get { return MapObjectType.BaseNode; } }
        #endregion
    }
}
