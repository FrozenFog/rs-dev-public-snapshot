using RelertSharp.Common;
using RelertSharp.IniSystem;

namespace RelertSharp.MapStructure.Points
{
    [IniEntitySerialize(Constant.MapStructure.ENT_CELLTAG)]
    public class CellTagCollection : PointCollectionBase<CellTagItem>
    {
        public CellTagCollection() { }
        protected override CellTagItem InvokeCtor()
        {
            return new CellTagItem();
        }
    }


    public class CellTagItem : PointItemBase, IMapObject, ITaggableObject
    {
        public CellTagItem(string _coord, string _tagID) : base(_coord)
        {
            TagId = _tagID;
            ObjectType = MapObjectType.Celltag;
        }
        public CellTagItem(CellTagItem src) : base(src)
        {
            TagId = src.TagId;
            ObjectType = MapObjectType.Celltag;
        }
        public CellTagItem(string tagid)
        {
            TagId = tagid;
            ObjectType = MapObjectType.Celltag;
        }
        public CellTagItem(I2dLocateable pos, string tagID) : base(pos)
        {
            TagId = TagId;
            ObjectType = MapObjectType.Celltag;
        }
        internal CellTagItem()
        {
            ObjectType = MapObjectType.Celltag;
        }
        public override void ReadFromIni(INIPair src)
        {
            TagId = src.Value;
            if (!base.ReadCoord(src.Name)) GlobalVar.Monitor.LogCritical(Id, TagId, ObjectType, this);
        }
        public override INIPair SaveAsIni()
        {
            INIPair p = new INIPair(CoordString);
            p.Value = TagId;
            return p;
        }


        #region Public Methods
        public override void ApplyConfig(IMapObjectBrushConfig config, IObjectBrushFilter filter, bool applyPosAndName = false)
        {
            base.ApplyConfig(config, filter, applyPosAndName);
            if (filter.Tag) TagId = config.AttatchedTag;
        }
        public override string ToString()
        {
            return string.Format("{0}, {1} - {2}", X, Y, TagId);
        }
        public string[] ExtractParameter()
        {
            return new string[]
            {
                X.ToString(),
                Y.ToString(),
                TagId
            };
        }
        public int GetChecksum()
        {
            unchecked
            {
                int hash = Constant.BASE_HASH;
                hash = hash * 31 + X;
                hash = hash * 31 + Y;
                hash = hash * 31 + TagId.GetHashCode();
                return hash;
            }
        }
        public IMapObject ConstructFromParameter(string[] command)
        {
            ParameterReader reader = new ParameterReader(command);
            CellTagItem cell = new CellTagItem()
            {
                X = reader.ReadInt(),
                Y = reader.ReadInt(),
                TagId = reader.ReadString()
            };
            return cell;
        }
        #endregion


        #region Public Calls - CellTagItem
        public string TagId { get; set; }
        #endregion
    }
}
