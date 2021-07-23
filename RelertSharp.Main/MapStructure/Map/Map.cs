using RelertSharp.Common;
using RelertSharp.FileSystem;
using RelertSharp.IniSystem;
using RelertSharp.MapStructure.Logic;
using RelertSharp.MapStructure.Objects;
using RelertSharp.MapStructure.Points;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using static RelertSharp.Common.GlobalVar;
using static RelertSharp.Utils.Misc;

namespace RelertSharp.MapStructure
{
    public partial class Map
    {
        private string mapFileName;
        private string mapPath;
        private string isomappack5String, overlayString, overlaydataString, previewString;
        private string digest;
        private HashSet<string> globalid = new HashSet<string>();
        private int genID = 1000000;

        private RankInfo ranks;
        private HeaderInfo headers;
        private Rectangle previewSize;

        private TileLayer Tiles;

        private Dictionary<string, INIEntity> residual;

        #region Ctor - Map
        public Map(MapFile f)
        {
            ReadFromMapFile(f);
            TileDictionary = new MapTheaterTileSet(this.Info.TheaterType);
        }
        #endregion


        #region Public Methods - Map


        public I3dLocateable ReferanceDeltaCell(I2dLocateable originalLocation, I2dLocateable groundDelta)
        {
            Pnt3 result = new Pnt3(groundDelta.X, groundDelta.Y, 0);
            Pnt newLocation = new Pnt(originalLocation.X + groundDelta.X, originalLocation.Y + groundDelta.Y);
            result.Z = GetHeightFromTile(newLocation) - GetHeightFromTile(originalLocation);
            return result;
        }
        public bool IsOutOfSize(I2dLocateable objectLocation, I2dLocateable delta = null)
        {
            if (delta == null) return !TilesData.HasTileOn(objectLocation);
            Pnt tmp = new Pnt(objectLocation.X + delta.X, objectLocation.Y + delta.Y);
            return !TilesData.HasTileOn(tmp);
        }
        public uint GetHouseColor(string housename)
        {
            HouseItem house = Houses.GetHouse(housename);
            if (house == null) return 0;
            return 0xFF000000 | (uint)(house.DrawingColor.B << 16 | house.DrawingColor.G << 8 | house.DrawingColor.R);
        }
        public uint GetHouseColor(ICombatObject obj)
        {
            HouseItem house = Houses.GetHouse(obj.Owner);
            if (house == null) return 0;
            return 0xFF000000 | (uint)(house.DrawingColor.B << 16 | house.DrawingColor.G << 8 | house.DrawingColor.R);
        }
        public string GetCivilianHouse()
        {
            if (Countries.Length == 0 || Houses.Length == 0) return null; 
            CountryItem con = Countries.First(x => x.Side == Constant.MapStructure.CivilianCountrySide);
            if (con != null)
            {
                HouseItem house = Houses.First(x => x.Country == con.Name);
                if (house != null) return house.Name;
            }
            return null;
        }
        public void CompressTile(bool highCompress = false)
        {
            if (highCompress) foreach (Tile t in Tiles.Data.Values) t.Height -= Tiles.BottomLevel;
            isomappack5String = Tiles.CompressToString();
        }
        public void CompressOverlay()
        {
            overlayString = Overlays.CompressIndex();
            overlaydataString = Overlays.CompressFrame();
        }
        public int GetHeightFromTile(I2dLocateable obj)
        {
            Tile t = Tiles[CoordInt(obj.X, obj.Y)];
            return t == null ? 0 : t.Height;
        }
        public bool DelID(string ID)
        {
            if (!globalid.Contains(ID)) return false;
            globalid.Remove(ID);
            return true;
        }
        public void DumpOverlayFromTile()
        {
            foreach (Tile t in TilesData)
            {
                if (t.GetObjects().Any(x => x.ObjectType == MapObjectType.Overlay))
                {
                    OverlayUnit o = t.GetObjects().First(x => x.ObjectType == MapObjectType.Overlay) as OverlayUnit;
                    Overlays[o.X, o.Y] = o;
                }
            }
        }
        #endregion


        #region Private Methods - Map

        #endregion 


        #region Public Calls - Map
        public IEnumerable<ICombatObject> AllCombatObjects { get { return Infantries.Union<ICombatObject>(Aircrafts).Union(Buildings).Union(Units); } }
        public WaypointCollection Waypoints { get; private set; } = new WaypointCollection();
        public CellTagCollection Celltags { get; private set; } = new CellTagCollection();
        public Lightning LightningCollection { get; private set; }
        public InfantryLayer Infantries { get; private set; } = new InfantryLayer();
        public AircraftLayer Aircrafts { get; private set; } = new AircraftLayer();
        public StructureLayer Buildings { get; private set; } = new StructureLayer();
        public UnitLayer Units { get; private set; } = new UnitLayer();
        public TerrainLayer Terrains { get; private set; } = new TerrainLayer();
        public SmudgeLayer Smudges { get; private set; } = new SmudgeLayer();
        public LightSourceCollection LightSources { get; private set; } = new LightSourceCollection();
        public AITriggerCollection AiTriggers { get; private set; } = new AITriggerCollection();
        public TeamScriptCollection Scripts { get; private set; } = new TeamScriptCollection();
        public TaskforceCollection Taskforces { get; private set; } = new TaskforceCollection();
        public HouseCollection Houses { get; private set; } = new HouseCollection();
        public LocalVarCollection LocalVariables { get; private set; } = new LocalVarCollection();
        public TeamCollection Teams { get; private set; } = new TeamCollection();
        public CountryCollection Countries { get; private set; }
        public TriggerCollection Triggers { get; private set; } = new TriggerCollection();
        public TagCollection Tags { get; private set; }
        public OverlayLayer Overlays { get; private set; }
        //public ActionCollection Actions { get; private set; }
        //public EventCollection Events { get; private set; }
        public MapInfo Info { get; private set; } 
        public INIEntity[] InfoEntity
        {
            get { return new INIEntity[3] { Info.Basic, Info.Map, Info.SpecialFlags }; }
        }
        public Dictionary<string, INIEntity> IniResidue
        {
            get { return residual; }
            set { residual = value; }
        }
        public Rectangle PreviewSize
        {
            get { return previewSize; }
            set { previewSize = value; }
        }
        public TileLayer TilesData
        {
            get { return Tiles; }
            set { Tiles = value; }
        }
        public string IsoMapPack5
        {
            get { return isomappack5String; }
            set { isomappack5String = value; }
        }
        public string OverlayDataPack
        {
            get { return overlaydataString; }
            set { overlaydataString = value; }
        }
        public string OverlayPack
        {
            get { return overlayString; }
            set { overlayString = value; }
        }
        public string PreviewPack
        {
            get { return previewString; }
            set { previewString = value; }
        }
        public string NewID
        {
            get
            {
                for (genID = 1000000; genID < 99999999; genID++)
                {
                    string id = string.Format("{0:D8}", genID);
                    if (!globalid.Contains(id))
                    {
                        globalid.Add(id);
                        return id;
                    }
                }
                return "";
            }
        }
        public I2dLocateable CenterPoint
        {
            get
            {
                return new Pnt(Info.Size.Width, Info.Size.Height);
            }
        }
        #endregion
    }
}
