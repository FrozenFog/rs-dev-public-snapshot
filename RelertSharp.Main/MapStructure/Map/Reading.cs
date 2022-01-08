using RelertSharp.Common;
using RelertSharp.FileSystem;
using RelertSharp.IniSystem;
using RelertSharp.IniSystem.Serialization;
using RelertSharp.MapStructure.Logic;
using RelertSharp.MapStructure.Objects;
using RelertSharp.MapStructure.Points;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using static RelertSharp.Common.Constant.MapStructure;
using static RelertSharp.Utils.Misc;

namespace RelertSharp.MapStructure
{
    public partial class Map
    {
        private MapReadingMonitor Monitor { get { return GlobalVar.Monitor; } }
        private void ReadFromMapFile(MapFile f)
        {
            isomappack5String = f.PopEnt("IsoMapPack5").JoinString();
            overlayString = f.PopEnt("OverlayPack").JoinString();
            overlaydataString = f.PopEnt("OverlayDataPack").JoinString();

            GetGeneralInfo(f);
            Tiles = new TileLayer(isomappack5String, Info.Size);
            Overlays = new OverlayLayer(overlayString, overlaydataString);
            FixEmptyTiles();
            DumpOverlayToTile();

            GetPreview(f);
            GetAbstractLogics(f);
            GetTeam(f);
            GetObjects(f);
            if (GlobalVar.GlobalRules != null) LoadHouseColor();

            globalid.AddRange(Triggers.AllId);
            globalid.AddRange(Teams.AllId);
            globalid.AddRange(Taskforces.AllId);
            globalid.AddRange(Tags.AllId);
            globalid.AddRange(Scripts.AllId);

            

            isomappack5String = "";
            overlayString = "";
            overlaydataString = "";
        }
        private void DumpOverlayToTile()
        {
            foreach (OverlayUnit o in Overlays)
            {
                Tile t = Tiles[o];
                if (t != null) t.AddObject(o);
            }
        }
        private void LoadHouseColor()
        {
            foreach (HouseItem house in Houses)
            {
                if (string.IsNullOrEmpty(house.ColorName)) house.DrawingColor = Color.Red;
                else
                {
                    INIPair p = GlobalVar.GlobalRules.GetColorInfo(house.ColorName);
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
            GlobalVar.Log.Info("Read Map general info");
            Info = new MapInfo(f.PopEnt("Basic"), f.PopEnt("Map"), f.PopEnt("SpecialFlags"));
            LightningCollection = new Lightning(f.PopEnt("Lighting"));
            if (f.IniDict.Keys.Contains("Header")) Header = new HeaderInfo(f.PopEnt("Header"));
            if (f.IniDict.Keys.Contains("Ranking")) Rank = new RankInfo(f.PopEnt("Ranking"));
            digest = f.PopEnt("Digest").JoinString();
        }
        private void GetObjects(MapFile f)
        {
            INIEntity entUnit = f.PopEnt(ENT_UNIT);
            INIEntity entInf = f.PopEnt(ENT_INF);
            INIEntity entStructure = f.PopEnt(ENT_STR);
            INIEntity entAircraft = f.PopEnt(ENT_AIR);
            INIEntity entTerrain = f.PopEnt(ENT_TERR);
            INIEntity entSmudge = f.PopEnt(ENT_SMG);
            INIEntity entTube = f.PopEnt(ENT_TUBE);
            INIEntity entLight = f.PopEnt(ENT_RS_LIGHT);

            residual = new Dictionary<string, INIEntity>(f.IniDict);
            GlobalVar.GlobalRules?.SetLocalRules(residual);

            GlobalVar.Log.Info("Read Map Smudge");
            Smudges.ReadFromIni(entSmudge);
            Smudges.Foreach(x => AddObjectToTile(x));
            GlobalVar.Log.Info("Read Map Units");
            Units.ReadFromIni(entUnit);
            Units.Foreach(x => AddObjectToTile(x));
            GlobalVar.Log.Info("Read Map Infantries");
            Infantries.ReadFromIni(entInf);
            Infantries.Foreach(x => AddObjectToTile(x));
            GlobalVar.Log.Info("Read Map Buildings");
            Buildings.ReadFromIni(entStructure);
            Buildings.Foreach(x => AddObjectToTile(x));
            GlobalVar.Log.Info("Read Map Aircrafts");
            Aircrafts.ReadFromIni(entAircraft);
            Aircrafts.Foreach(x => AddObjectToTile(x));
            GlobalVar.Log.Info("Read Map Terrains");
            Terrains.ReadFromIni(entTerrain);
            Terrains.Foreach(x => AddObjectToTile(x));
            GlobalVar.Log.Info("Read Map Tunnels");
            Tubes.ReadFromIni(entTube);
            GlobalVar.Log.Info("Read Map Lightsources");
            LightSources.ReadFromIni(entLight);
        }
        private void GetTeam(MapFile f)
        {
            List<string> _teamList = f.PopEnt("TeamTypes").TakeValuesToList();
            List<string> _taskforceList = f.PopEnt("TaskForces").TakeValuesToList();
            List<string> _scriptList = f.PopEnt("ScriptTypes").TakeValuesToList();

            IniEntitySerializer serTeam = new IniEntitySerializer(typeof(TeamItem));
            GlobalVar.Log.Info("Read Map Teams");
            foreach (string teamID in _teamList)
            {
                try
                {
                    TeamItem team = new TeamItem();
                    INIEntity ent = f.PopEnt(teamID);
                    serTeam.Deserialize(ent, team, false);
                    team.Residue = ent;
                    Teams[teamID] = team;
                }
                catch (Exception e)
                {
                    Monitor.LogFatal(teamID, string.Empty, LogicType.Team, e);
                }
            }
            GlobalVar.Log.Info("Read Map Taskforces");
            foreach (string tfID in _taskforceList)
            {
                try
                {
                    Taskforces[tfID] = new TaskforceItem(f.PopEnt(tfID));
                }
                catch (Exception e)
                {
                    Monitor.LogFatal(tfID, string.Empty, LogicType.Team, e);
                }
            }
            GlobalVar.Log.Info("Read Map Scripts");
            foreach (string scptID in _scriptList)
            {
                try
                {
                    Scripts[scptID] = new TeamScriptGroup(f.PopEnt(scptID));
                }
                catch (Exception e)
                {
                    Monitor.LogFatal(scptID, string.Empty, LogicType.Script, e);
                }
            }

            INIEntity _houseList = f.PopEnt("Houses");
            INIEntity _countryList = f.PopEnt("Countries");
            int iHouseMax = 0;
            void initDefaultHouse()
            {
                var countries = GlobalVar.GlobalRules[Constant.RulesHead.HEAD_COUNTRY];
                foreach (INIPair p in countries)
                {
                    var entCon = GlobalVar.GlobalRules[p.Value];
                    CountryItem con = CountryItem.ParseFromRules(entCon);
                    HouseItem house = HouseItem.FromCountry(con);
                    Countries[iHouseMax.ToString()] = con;
                    Houses[iHouseMax.ToString()] = house;
                    iHouseMax++;
                }
            }

            Countries = new CountryCollection();
            initDefaultHouse();
            GlobalVar.Log.Info("Read Map Countries");
            int idx = iHouseMax;
            foreach (INIPair p in _countryList)
            {
                try
                {
                    CountryItem con;
                    INIEntity ent = f.PopEnt(p.Value);
                    bool alreadyExist = false;
                    if (Countries.GetCountry(p.Value) is CountryItem c)
                    {
                        con = c;
                        con.OverwriteBy(ent);
                        alreadyExist = true;
                    }
                    else con = new CountryItem(ent);
                    con.Residual = ent;
                    con.CountryNameChanged += CountryNameChanged;
                    if (!alreadyExist) Countries[idx.ToString()] = con;
                }
                catch (Exception e)
                {
                    Monitor.LogFatal(p.Name, p.Value, LogicType.Country, e);
                }
                finally { idx++; }
            }
            GlobalVar.Log.Info("Read Map Houses");
            idx = iHouseMax;
            foreach (INIPair p in _houseList)
            {
                try
                {
                    INIEntity ent = f.PopEnt(p.Value);
                    HouseItem item;
                    bool alreadyExist = false;
                    if (Houses.GetHouse(p.Value) is HouseItem h)
                    {
                        item = h;
                        item.OverwriteBy(ent);
                        alreadyExist = true;
                    }
                    else item = new HouseItem(ent);
                    item.HouseNameChanged += HouseNameChanged;
                    foreach (BaseNode node in item.BaseNodes) AddObjectToTile(node);
                    if (!alreadyExist) Houses[idx.ToString()] = item;

                    if (item.PlayerControl)
                    {
                        CountryItem c = Countries.GetCountry(item.Country);
                        if (c != null)
                        {
                            GlobalVar.PlayerSide = c.Side;
                        }
                    }
                }
                catch (Exception e)
                {
                    Monitor.LogFatal(p.Name, p.Value, LogicType.House, e);
                }
                finally { idx++; }
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

            Tags.ReadFromIni(entTag);
            GlobalVar.Log.Info("Read Map Triggers");
            foreach (INIPair p in entTrigger.DataList)
            {
                try
                {
                    Triggers.ReadTriggerFromIni(p);
                    if (entEvent.DictData.Keys.Contains(p.Name))
                    {
                        Triggers[p.Name].Events = new LogicGroup(entEvent.GetPair(p.Name), TriggerSubType.Event);
                    }
                    if (entAction.DictData.Keys.Contains(p.Name))
                    {
                        Triggers[p.Name].Actions = new LogicGroup(entAction.GetPair(p.Name), TriggerSubType.Action);
                    }
                }
                catch (Exception e)
                {
                    Monitor.LogFatal(p.Name, string.Empty, LogicType.Trigger, e);
                    var item = Triggers[p.Name];
                    Triggers.RemoveItem(item);
                }
            }
            GlobalVar.Log.Info("Read Map Local");
            LocalVariables.ReadFromIni(entVar);
            GlobalVar.Log.Info("Read Map AiTrigger");
            AiTriggers.ReadFromIni(entAITrigger);
            AiTriggers.SetEnables(entAITriggerEnable);
            GlobalVar.Log.Info("Read Map Celltags");
            Celltags.ReadFromIni(entCelltags);
            Celltags.Foreach(x => AddObjectToTile(x));
            GlobalVar.Log.Info("Read Map Waypoints");
            Waypoints.ReadFromIni(entWaypoints);
            Waypoints.Foreach(x => AddObjectToTile(x));
        }
        private void GetPreview(MapFile f)
        {
            GlobalVar.Log.Info("Read Map Preview");
            INIEntity preview = f.PopEnt(ENT_PREV);
            if (preview.DataList.Count == 0)
            {
                f.PopEnt("PreviewPack");
                return;
            }
            int[] buf = preview.ParseIntList("Size");
            previewSize = new Rectangle(buf[0], buf[1], buf[2], buf[3]);
            previewString = f.PopEnt(ENT_PREV_PACK).JoinString();
        }
        private void AddObjectToTile(IMapObject obj)
        {
            if (obj.GetType() == typeof(StructureItem))
            {
                StructureItem bud = obj as StructureItem;
                foreach (I2dLocateable pos in new Foundation2D(bud))
                {
                    Tiles.AddObjectOnTile(pos, obj);
                }
            }
            else if (obj.GetType() == typeof(SmudgeItem))
            {
                SmudgeItem smg = obj as SmudgeItem;
                foreach (I2dLocateable pos in new Square2D(smg, smg.SizeX, smg.SizeY))
                {
                    Tiles.AddObjectOnTile(pos, obj);
                }
            }
            else Tiles.AddObjectOnTile(obj);
        }
    }
}
