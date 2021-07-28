﻿using RelertSharp.Common;
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
using static RelertSharp.Utils.Misc;

namespace RelertSharp.MapStructure
{
    public partial class Map
    {
        private void ReadFromMapFile(MapFile f)
        {
            mapFileName = f.FileName;
            mapPath = f.FilePath;
            isomappack5String = f.PopEnt("IsoMapPack5").JoinString();
            overlayString = f.PopEnt("OverlayPack").JoinString();
            overlaydataString = f.PopEnt("OverlayDataPack").JoinString();

            GetGeneralInfo(f);
            Tiles = new TileLayer(isomappack5String, Info.Size);
            Overlays = new OverlayLayer(overlayString, overlaydataString);
            FixEmptyTiles();
            DumpOverlayToTile();

            void func()
            {
                residual = new Dictionary<string, INIEntity>(f.IniDict);
                GlobalVar.GlobalRules?.SetLocalRules(residual);
            }

            GetPreview(f);
            GetAbstractLogics(f);
            GetTeam(f);
            GetObjects(f, func);
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
            Info = new MapInfo(f.PopEnt("Basic"), f.PopEnt("Map"), f.PopEnt("SpecialFlags"));
            LightningCollection = new Lightning(f.PopEnt("Lighting"));
            if (f.IniDict.Keys.Contains("Header")) headers = new HeaderInfo(f.PopEnt("Header"));
            if (f.IniDict.Keys.Contains("Ranking")) ranks = new RankInfo(f.PopEnt("Ranking"));
            digest = f.PopEnt("Digest").JoinString();
        }
        private void GetObjects(MapFile f, Action dumpFunc = null)
        {
            INIEntity entUnit = f.PopEnt("Units");
            INIEntity entInf = f.PopEnt("Infantry");
            INIEntity entStructure = f.PopEnt("Structures");
            INIEntity entAircraft = f.PopEnt("Aircraft");
            INIEntity entTerrain = f.PopEnt("Terrain");
            INIEntity entSmudge = f.PopEnt("Smudge");
            INIEntity entLight = f.PopEnt(Constant.MapStructure.CustomComponents.LightsourceTitle);

            dumpFunc?.Invoke();

            foreach (INIPair p in entSmudge.DataList)
            {
                string[] tmp = p.ParseStringList();
                int x = int.Parse(tmp[1]);
                int y = int.Parse(tmp[2]);
                AddSmudge(tmp[0], x, y, IniParseBool(tmp[3]));
            }
            foreach (INIPair p in entUnit.DataList)
            {
                AddUnit(p.Name, p.ParseStringList());
            }
            foreach (INIPair p in entInf.DataList)
            {
                AddInfantry(p.Name, p.ParseStringList());
            }
            foreach (INIPair p in entStructure.DataList)
            {
                AddStructure(p.Name, p.ParseStringList());
            }
            foreach (INIPair p in entAircraft.DataList)
            {
                AddAircraft(p.Name, p.ParseStringList());
            }
            foreach (INIPair p in entTerrain.DataList)
            {
                AddTerrain(p.Name, p.Value);
            }
            foreach (INIPair p in entLight.DataList)
            {
                LightSources.AddObject(new LightSource(p));
            }
        }
        private void GetTeam(MapFile f)
        {
            List<string> _teamList = f.PopEnt("TeamTypes").TakeValuesToList();
            List<string> _taskforceList = f.PopEnt("TaskForces").TakeValuesToList();
            List<string> _scriptList = f.PopEnt("ScriptTypes").TakeValuesToList();

            IniEntitySerializer serTeam = new IniEntitySerializer(typeof(TeamItem));
            foreach (string teamID in _teamList)
            {
                TeamItem team = new TeamItem();
                serTeam.Deserialize(f.PopEnt(teamID), team);
                Teams[teamID] = team;
            }
            foreach (string tfID in _taskforceList)
            {
                Taskforces[tfID] = new TaskforceItem(f.PopEnt(tfID));
            }
            foreach (string scptID in _scriptList)
            {
                Scripts[scptID] = new TeamScriptGroup(f.PopEnt(scptID));
            }

            INIEntity _houseList = f.PopEnt("Houses");
            INIEntity _countryList = f.PopEnt("Countries");

            Countries = new CountryCollection();
            IniEntitySerializer serCon = new IniEntitySerializer(typeof(CountryItem));
            foreach (INIPair p in _countryList)
            {
                CountryItem con = new CountryItem();
                serCon.Deserialize(f.PopEnt(p.Value), con);
                con.CountryNameChanged += CountryNameChanged;
                Countries[p.Name] = con;
                //CountryItem item = new CountryItem(f.PopEnt(p.Value));
                //item.CountryNameChanged += CountryNameChanged;
                ////if (string.IsNullOrEmpty(item.Name)) item.Name = p.Value;
                //Countries[p.Name] = item;
            }
            foreach (INIPair p in _houseList)
            {
                HouseItem item = new HouseItem(f.PopEnt(p.Value));
                item.HouseNameChanged += HouseNameChanged;
                foreach (BaseNode node in item.BaseNodes) AddObjectToTile(node);
                Houses[p.Name] = item;

                if (item.PlayerControl)
                {
                    CountryItem c = Countries.GetCountry(item.Country);
                    if (c != null)
                    {
                        GlobalVar.PlayerSide = c.Side;
                    }
                }
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
                Triggers.ReadTriggerFromIni(p);
                if (entEvent.DictData.Keys.Contains(p.Name))
                {
                    Triggers[p.Name].Events = new LogicGroup(entEvent.GetPair(p.Name), TriggerSubType.EventLogic);
                }
                if (entAction.DictData.Keys.Contains(p.Name))
                {
                    Triggers[p.Name].Actions = new LogicGroup(entAction.GetPair(p.Name), TriggerSubType.ActionLogic);
                }
            }
            foreach (INIPair p in entVar.DataList)
            {
                string[] tmp = p.ParseStringList();
                LocalVariables[p.Name] = new LocalVarItem(tmp[0], IniParseBool(tmp[1]), p.Name);
            }
            foreach (INIPair p in entAITrigger.DataList)
            {
                AiTriggers[p.Name] = new AITriggerItem(p.Name, p.ParseStringList());
            }
            foreach (INIPair p in entAITriggerEnable.DataList)
            {
                if (AiTriggers[p.Name] != null) AiTriggers[p.Name].Enabled = IniParseBool(p.Value);
                AiTriggers.GlobalEnables[p.Name] = IniParseBool(p.Value);
            }
            foreach (INIPair p in entCelltags.DataList)
            {
                CellTagItem cell = new CellTagItem(p.Name, p.Value);
                Celltags.AddObject(cell);
                AddObjectToTile(cell);
            }
            foreach (INIPair p in entWaypoints.DataList)
            {
                WaypointItem w = new WaypointItem(p.Value, p.Name);
                Waypoints.AddObject(w);
                AddObjectToTile(w);
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
            int[] buf = preview.ParseIntList("Size");
            previewSize = new Rectangle(buf[0], buf[1], buf[2], buf[3]);
            previewString = f.PopEnt("PreviewPack").JoinString();
        }
    }
}
