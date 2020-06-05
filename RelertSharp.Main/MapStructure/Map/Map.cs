using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using RelertSharp.FileSystem;
using RelertSharp.IniSystem;
using RelertSharp.Common;
using RelertSharp.MapStructure.Logic;
using RelertSharp.MapStructure.Objects;
using RelertSharp.MapStructure.Points;
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
        private List<string> globalid = new List<string>();
        private int genID = 1000000;

        private MapInfo info;
        private RankInfo ranks;
        private HeaderInfo headers;
        private Rectangle previewSize;

        private TileLayer Tiles;

        private Dictionary<string, INIEntity> residual;

        #region Ctor - Map
        public Map(MapFile f)
        {
            ReadFromMapFile(f);
        }
        #endregion


        #region Public Methods - Map


        #region Collection Utils
        public TriggerItem NewTrigger(out TagItem tag)
        {
            TriggerItem t = Triggers.NewTrigger(NewID);
            tag = new TagItem(t, NewID);
            Tags[tag.ID] = tag;
            return t;
        }
        public void RemoveTrigger(TriggerItem item)
        {
            Triggers.RemoveTrigger(item);
            DelID(item.ID);
        }
        public TaskforceItem NewTaskforce()
        {
            TaskforceItem t = TaskForces.NewTaskforce(NewID);
            return t;
        }
        public void RemoveTaskforce(TaskforceItem item)
        {
            TaskForces.Remove(item.ID);
            DelID(item.ID);
        }
        public TeamItem NewTeam()
        {
            TeamItem t = Teams.NewTeam(NewID);
            return t;
        }
        public void RemoveTeam(TeamItem item)
        {
            Teams.Remove(item.ID);
            DelID(item.ID);
        }
        public AITriggerItem NewAITrigger()
        {
            AITriggerItem t = AiTriggers.NewAITrigger(NewID);
            return t;
        }
        public void RemoveAITrigger(AITriggerItem item)
        {
            AiTriggers.Remove(item.ID);
            DelID(item.ID);
        }
        public IEnumerable<object> GetComboCollections(TriggerParam param)
        {
            switch (param.ComboType)
            {
                case TriggerParam.ComboContent.Aircrafts:
                    return GlobalRules.AircraftList;
                case TriggerParam.ComboContent.Buildings:
                    return GlobalRules.BuildingList;
                case TriggerParam.ComboContent.Infantries:
                    return GlobalRules.InfantryList;
                case TriggerParam.ComboContent.Units:
                    return GlobalRules.VehicleList;
                case TriggerParam.ComboContent.SoundNames:
                    return GlobalSound.SoundList;
                case TriggerParam.ComboContent.EvaNames:
                    return GlobalSound.EvaList;
                case TriggerParam.ComboContent.ThemeNames:
                    return GlobalSound.ThemeList;
                case TriggerParam.ComboContent.LocalVar:
                    return LocalVariables.ToTechno();
                case TriggerParam.ComboContent.SuperWeapons:
                    return GlobalRules.SuperWeaponList;
                case TriggerParam.ComboContent.CsfLabel:
                    return GlobalCsf.TechnoPairs;
                case TriggerParam.ComboContent.Triggers:
                    return Triggers.ToTechno();
                case TriggerParam.ComboContent.Tags:
                    return Tags.ToTechno();
                case TriggerParam.ComboContent.TechnoType:
                    return GlobalRules.TechnoList;
                case TriggerParam.ComboContent.GlobalVar:
                    return GlobalRules.GlobalVar;
                case TriggerParam.ComboContent.Teams:
                    return Teams.ToTechno();
                case TriggerParam.ComboContent.Houses:
                    return Countries.ToTechno();
                case TriggerParam.ComboContent.Animations:
                    return GlobalRules.AnimationList;
                case TriggerParam.ComboContent.ParticalAnim:
                    return GlobalRules.ParticalList;
                case TriggerParam.ComboContent.VoxelAnim:
                    return GlobalRules.VoxAnimList;
                case TriggerParam.ComboContent.BuildingID:
                    return GlobalRules.BuildingIDList;
                case TriggerParam.ComboContent.Movies:
                    return GlobalRules.MovieList;
                case TriggerParam.ComboContent.Warhead:
                    return GlobalRules.WarheadList;
                case TriggerParam.ComboContent.Animations0:
                    return GlobalRules.AnimationList0;
                case TriggerParam.ComboContent.Buildings0:
                    return GlobalRules.BuildingList0;
                case TriggerParam.ComboContent.AttackTargetType:
                    return GlobalVar.Scripts.AttackTargetType;
                case TriggerParam.ComboContent.UnloadBehavior:
                    return GlobalVar.Scripts.UnloadBehavior;
                case TriggerParam.ComboContent.ScriptMission:
                    return GlobalVar.Scripts.Missions;
                case TriggerParam.ComboContent.ScriptFrom0:
                    return Scripts.ToTechnoFrom0();
                case TriggerParam.ComboContent.EvaNames0:
                    return GlobalSound.EvaList0;
                case TriggerParam.ComboContent.SoundNames0:
                    return GlobalSound.SoundList0;
                case TriggerParam.ComboContent.ThemeNames0:
                    return GlobalSound.ThemeList0;
                case TriggerParam.ComboContent.Facing:
                    return GlobalVar.Scripts.FacingDirections;
                case TriggerParam.ComboContent.TalkBubble:
                    return GlobalVar.Scripts.TalkBubbles;
                default:
                    return null;
            }
        }
        #endregion


        public I3dLocateable ReferanceDeltaCell(I2dLocateable originalLocation, I2dLocateable groundDelta)
        {
            Pnt3 result = new Pnt3(groundDelta.X, groundDelta.Y, 0);
            Pnt newLocation = new Pnt(originalLocation.X + groundDelta.X, originalLocation.Y + groundDelta.Y);
            result.Z = GetHeightFromTile(newLocation) - GetHeightFromTile(originalLocation);
            return result;
        }
        public bool IsOutOfSize(I2dLocateable objectLocation, I2dLocateable delta)
        {
            Pnt tmp = new Pnt(objectLocation.X + delta.X, objectLocation.Y + delta.Y);
            return !TilesData.HasTileOn(tmp);
        }
        public uint GetHouseColor(string housename)
        {
            HouseItem house = Houses.GetHouse(housename);
            if (house == null) return 0;
            return 0xFF000000 | (uint)(house.DrawingColor.B << 16 | house.DrawingColor.G << 8 | house.DrawingColor.R);
        }
        public void CompressTile()
        {
            foreach (Tile t in Tiles.Data.Values)
            {
                t.Height -= Tiles.BottomLevel;
            }
            Tiles.RemoveEmptyTiles();
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
        #endregion


        #region Private Methods - Map

        #endregion 


        #region Public Calls - Map
        public WaypointCollection Waypoints { get; private set; } = new WaypointCollection();
        public CellTagCollection Celltags { get; private set; } = new CellTagCollection();
        public Lightning LightningCollection { get; private set; }
        public InfantryLayer Infantries { get; private set; } = new InfantryLayer();
        public AircraftLayer Aircrafts { get; private set; } = new AircraftLayer();
        public StructureLayer Buildings { get; private set; } = new StructureLayer();
        public UnitLayer Units { get; private set; } = new UnitLayer();
        public TerrainLayer Terrains { get; private set; } = new TerrainLayer();
        public SmudgeLayer Smudges { get; private set; } = new SmudgeLayer();
        public AITriggerCollection AiTriggers { get; private set; } = new AITriggerCollection();
        public TeamScriptCollection Scripts { get; private set; } = new TeamScriptCollection();
        public TaskforceCollection TaskForces { get; private set; } = new TaskforceCollection();
        public HouseCollection Houses { get; private set; } = new HouseCollection();
        public LocalVarCollection LocalVariables { get; private set; } = new LocalVarCollection();
        public TeamCollection Teams { get; private set; } = new TeamCollection();
        public CountryCollection Countries { get; private set; }
        public TriggerCollection Triggers { get; private set; } = new TriggerCollection();
        public TagCollection Tags { get; private set; }
        public OverlayLayer Overlays { get; private set; }
        //public ActionCollection Actions { get; private set; }
        //public EventCollection Events { get; private set; }
        public MapInfo Info
        {
            get { return info; }
        }
        public INIEntity[] InfoEntity
        {
            get { return new INIEntity[3] { info.Basic, info.Map, info.SpecialFlags }; }
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
                return new Pnt(info.Size.Width, info.Size.Height);
            }
        }
        #endregion
    }
}
