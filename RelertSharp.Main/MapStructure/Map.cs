﻿using System;
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
using static RelertSharp.Utils.Misc;

namespace RelertSharp.MapStructure
{
    public class Map
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
        private Lightning lightning;
        private Rectangle previewSize;

        private WaypointCollection waypoints = new WaypointCollection();
        private CellTagCollection celltags = new CellTagCollection();

        private TileLayer Tiles;

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
            LoadHouseColor();

            Tiles = new TileLayer(isomappack5String, info.Size);
            Overlays = new OverlayLayer(overlayString, overlaydataString);
            residual = new Dictionary<string, INIEntity>(f.IniDict);

            globalid.AddRange(Triggers.Keys);
            globalid.AddRange(Teams.Keys);
            globalid.AddRange(TaskForces.Keys);
            globalid.AddRange(Tags.Keys);

            isomappack5String = "";
            overlayString = "";
            overlaydataString = "";
        }
        #endregion


        #region Public Methods - Map
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
        public TriggerItem NewTrigger()
        {
            TriggerItem t = Triggers.NewTrigger(NewID);
            TagItem tag = new TagItem(t, NewID);
            Tags[tag.ID] = tag;
            return t;
        }
        public int GetHeightFromTile(I2dLocateable obj)
        {
            Tile t = Tiles[CoordInt(obj.X, obj.Y)];
            return t == null ? 0 : t.Height;
        }
        #endregion


        #region Private Methods - Map
        private void LoadHouseColor()
        {
            foreach (HouseItem house in Houses)
            {
                if (string.IsNullOrEmpty(house.ColorName)) house.DrawingColor = Color.Red;
                else
                {
                    INIPair p = GlobalVar.GlobalRules["Colors"].GetPair(house.ColorName);
                    if (p.Name == "") house.DrawingColor = Color.Red;
                    else
                    {
                        string[] hsb = p.ParseStringList();
                        house.DrawingColor = Utils.HSBColor.FromHSB(hsb);
                    }
                }
            }
        }
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
                int x = int.Parse(tmp[1]);
                int y = int.Parse(tmp[2]);
                string coord = CoordString(x, y);
                Smudges[coord] = new SmudgeItem(tmp[0], x, y, ParseBool(tmp[3]));
            }
            foreach (INIPair p in entUnit.DataList)
            {
                Units[p.Name] = new UnitItem(p.Name, p.ParseStringList());
            }
            foreach (INIPair p in entInf.DataList)
            {
                Infantries[p.Name] = new InfantryItem(p.Name, p.ParseStringList());
            }
            foreach (INIPair p in entStructure.DataList)
            {
                Buildings[p.Name] = new StructureItem(p.Name, p.ParseStringList());
            }
            foreach (INIPair p in entAircraft.DataList)
            {
                Aircrafts[p.Name] = new AircraftItem(p.Name, p.ParseStringList());
            }
            foreach (INIPair p in entTerrain.DataList)
            {
                Terrains[p.Name] = new TerrainItem(p.Name, p.Value);
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
                TaskForces[tfID] = new TaskforceItem(f.PopEnt(tfID));
            }
            foreach (string scptID in _scriptList)
            {
                Scripts[scptID] = new TeamScriptGroup(f.PopEnt(scptID));
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

            Tags = new TagCollection(entTag);

            foreach (INIPair p in entTrigger.DataList)
            {
                Triggers.LoadTriggerCommand(p);
                if (entEvent.DictData.Keys.Contains(p.Name))
                {
                    Triggers[p.Name].Events = new LogicGroup(entEvent.GetPair(p.Name), LogicType.EventLogic);
                }
                if (entAction.DictData.Keys.Contains(p.Name))
                {
                    Triggers[p.Name].Actions = new LogicGroup(entAction.GetPair(p.Name), LogicType.ActionLogic);
                }
            }
            foreach (INIPair p in entVar.DataList)
            {
                string[] tmp = p.ParseStringList();
                LocalVariables[tmp[0]] = new LocalVarItem(tmp[0], ParseBool(tmp[1]), p.Name);
            }
            foreach (INIPair p in entAITrigger.DataList)
            {
                AiTriggers[p.Name] = new AITriggerItem(p.Name, p.ParseStringList());
            }
            foreach (INIPair p in entAITriggerEnable.DataList)
            {
                if (AiTriggers[p.Name] != null) AiTriggers[p.Name].Enabled = ParseBool(p.Value);
                else AiTriggers.GlobalEnables[p.Name] = ParseBool(p.Value);
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
                for (; genID < 99999999; genID++)
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
        #endregion
    }
}
