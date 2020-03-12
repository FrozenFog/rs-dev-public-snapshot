using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using relert_sharp.FileSystem;
using relert_sharp.IniSystem;
using relert_sharp.Common;
using relert_sharp.MapStructure.Logic;
using relert_sharp.MapStructure.Objects;
using relert_sharp.MapStructure.Points;
using static relert_sharp.Utils.Misc;

namespace relert_sharp.MapStructure
{
    public class Map
    {
        private string mapFileName;
        private string mapPath;
        private string isomappack5String, overlayString, overlaydataString, previewString;
        private string digest;

        private MapInfo info;
        private RankInfo ranks;
        private HeaderInfo headers;
        private Lightning lightning;
        private Rectangle previewSize;

        private TaskforceCollection taskforces = new TaskforceCollection();
        private TeamScriptCollection scripts = new TeamScriptCollection();
        private AITriggerCollection aitriggers = new AITriggerCollection();
        private WaypointCollection waypoints = new WaypointCollection();
        private CellTagCollection celltags = new CellTagCollection();

        private UnitLayer units = new UnitLayer();
        private InfantryLayer infantries = new InfantryLayer();
        private StructureLayer structures = new StructureLayer();
        private AircraftLayer aircrafts = new AircraftLayer();
        private TerrainLayer terrains = new TerrainLayer();
        private SmudgeLayer smudges = new SmudgeLayer();

        private TileLayer Tiles;
        private OverlayLayer Overlays;

        private Dictionary<string, INIEntity> residual;


        #region Ctor - Map
        public Map(MapFile f)
        {
            mapFileName = f.FileName;
            mapPath = f.FilePath;
            isomappack5String = f.PopEnt("IsoMapPack5").JoinString();
            overlayString = f.PopEnt("OverlayPack").JoinString();
            overlaydataString = f.PopEnt("OverlayDataPack").JoinString();
            GetGeneralInfo(f);
            GetPreview(f);
            GetAbstractLogics(f);
            GetTeam(f);
            GetObjects(f);
            Tiles = new TileLayer(isomappack5String, info.Size);
            Overlays = new OverlayLayer(overlayString, overlaydataString);
            residual = new Dictionary<string, INIEntity>(f.IniDict);

            isomappack5String = "";
            overlayString = "";
            overlaydataString = "";
        }
        #endregion


        #region Public Methods - Map
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
        #endregion


        #region Private Methods - Map
        private void GetGeneralInfo(MapFile f)
        {
            info = new MapInfo(f.PopEnt("Basic"), f.PopEnt("Map"), f.PopEnt("SpecialFlags"));
            lightning = new Lightning(f.PopEnt("Lighting"));
            if (f.IniDict.Keys.Contains("Header")) headers = new HeaderInfo(f.PopEnt("Header"));
            if (f.IniDict.Keys.Contains("Ranking")) ranks = new RankInfo(f.PopEnt("Ranking"));
            digest = f.PopEnt("Digest").JoinString();
        }
        private void GetObjects(MapFile f)
        {
            INIEntity entUnit = f.PopEnt("Units");
            INIEntity entInf = f.PopEnt("Infantry");
            INIEntity entStructure = f.PopEnt("Structures");
            INIEntity entAircraft = f.PopEnt("Aircraft");
            INIEntity entTerrain = f.PopEnt("Terrain");
            INIEntity entSmudge = f.PopEnt("Smudge");

            foreach (INIPair p in entSmudge.DataList)
            {
                string[] tmp = p.ParseStringList();
                string coord = CoordString(int.Parse(tmp[1]), int.Parse(tmp[2]));
                smudges[coord] = new SmudgeItem(tmp[0], coord, ParseBool(tmp[3]));
            }
            foreach (INIPair p in entUnit.DataList)
            {
                units[p.Name] = new UnitItem(p.Name, p.ParseStringList());
            }
            foreach (INIPair p in entInf.DataList)
            {
                infantries[p.Name] = new InfantryItem(p.Name, p.ParseStringList());
            }
            foreach (INIPair p in entStructure.DataList)
            {
                structures[p.Name] = new StructureItem(p.Name, p.ParseStringList());
            }
            foreach (INIPair p in entAircraft.DataList)
            {
                aircrafts[p.Name] = new AircraftItem(p.Name, p.ParseStringList());
            }
            foreach (INIPair p in entTerrain.DataList)
            {
                terrains[p.Name] = new TerrainItem(p.Name, p.Value);
            }
        }
        private void GetTeam(MapFile f)
        {
            List<string> _teamList = f.PopEnt("TeamTypes").TakeValuesToList();
            List<string> _taskforceList = f.PopEnt("TaskForces").TakeValuesToList();
            List<string> _scriptList = f.PopEnt("ScriptTypes").TakeValuesToList();

            foreach (string teamID in _teamList)
            {
                Teams[teamID] = new TeamItem(f.PopEnt(teamID));
            }
            foreach (string tfID in _taskforceList)
            {
                taskforces[tfID] = new TaskforceItem(f.PopEnt(tfID));
            }
            foreach (string scptID in _scriptList)
            {
                scripts[scptID] = new TeamScriptGroup(f.PopEnt(scptID));
            }

            INIEntity _houseList = f.PopEnt("Houses");
            INIEntity _countryList = f.PopEnt("Countries");

            foreach (INIPair p in _houseList)
            {
                Houses[p.Name] = new HouseItem(f.PopEnt(p.Value));
            }
            Countries = new CountryCollection();
            foreach (INIPair p in _countryList)
            {
                CountryItem item = new CountryItem(f.PopEnt(p.Value));
                if (string.IsNullOrEmpty(item.Name)) item.Name = p.Value;
                item.Index = p.Name;
                Countries[p.Name] = item;
            }
        }
        private void GetAbstractLogics(MapFile f)
        {
            INIEntity entEvent = f.PopEnt("Events");
            INIEntity entAction = f.PopEnt("Actions");
            INIEntity entTrigger = f.PopEnt("Triggers");
            INIEntity entTag = f.PopEnt("Tags");
            INIEntity entVar = f.PopEnt("VariableNames");
            INIEntity entAITrigger = f.PopEnt("AITriggerTypes");
            INIEntity entAITriggerEnable = f.PopEnt("AITriggerTypesEnable");
            INIEntity entCelltags = f.PopEnt("CellTags");
            INIEntity entWaypoints = f.PopEnt("Waypoints");

            Triggers = new TriggerCollection(entTrigger);
            Actions = new ActionCollection(entAction);
            Events = new EventCollection(entEvent);
            Tags = new TagCollection(entTag);
            
            foreach (INIPair p in entVar.DataList)
            {
                string[] tmp = p.ParseStringList();
                LocalVariables[p.Name + "," + tmp[0]] = ParseBool(tmp[1]);
            }
            foreach (INIPair p in entAITrigger.DataList)
            {
                aitriggers[p.Name] = new AITriggerItem(p.Name, p.ParseStringList());
            }
            foreach (INIPair p in entAITriggerEnable.DataList)
            {
                if (aitriggers[p.Name] != null) aitriggers[p.Name].Enabled = ParseBool(p.Value);
                else aitriggers.GlobalEnables[p.Name] = ParseBool(p.Value);
            }
            foreach (INIPair p in entCelltags.DataList)
            {
                celltags[p.Name] = new CellTagItem(p.Name, p.Value);
            }
            foreach (INIPair p in entWaypoints.DataList)
            {
                waypoints[p.Value] = new WaypointItem(p.Value, p.Name);
            }
        }
        private void GetPreview(MapFile f)
        {
            INIEntity preview = f.PopEnt("Preview");
            if (preview.DataList.Count == 0)
            {
                f.PopEnt("PreviewPack");
                return;
            }
            int[] buf = preview.GetPair("Size").ParseIntList();
            previewSize = new Rectangle(buf[0], buf[1], buf[2], buf[3]);
            previewString = f.PopEnt("PreviewPack").JoinString();
        }
        #endregion 


        #region Public Calls - Map
        public HouseCollection Houses { get; private set; } = new HouseCollection();
        public LocalVarCollection LocalVariables { get; private set; } = new LocalVarCollection();
        public TeamCollection Teams { get; private set; } = new TeamCollection();
        public CountryCollection Countries { get; private set; }
        public TriggerCollection Triggers { get; private set; }
        public TagCollection Tags { get; private set; }
        public ActionCollection Actions { get; private set; }
        public EventCollection Events { get; private set; }
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
        #endregion
    }
}
