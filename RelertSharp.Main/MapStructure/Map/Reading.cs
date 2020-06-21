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
            Tiles = new TileLayer(isomappack5String, info.Size);
            Overlays = new OverlayLayer(overlayString, overlaydataString);
            Tiles.FixEmptyTiles(Info.Size.Width, info.Size.Height);
            DumpOverlayToTile();

            GetPreview(f);
            GetAbstractLogics(f);
            GetTeam(f);
            GetObjects(f);
            LoadHouseColor();

            globalid.AddRange(Triggers.Keys);
            globalid.AddRange(Teams.Keys);
            globalid.AddRange(TaskForces.Keys);
            globalid.AddRange(Tags.Keys);

            residual = new Dictionary<string, INIEntity>(f.IniDict);

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
            LightningCollection = new Lightning(f.PopEnt("Lighting"));
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
                AddSmudge(tmp[0], x, y, ParseBool(tmp[3]));
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
                HouseItem item = new HouseItem(f.PopEnt(p.Value));
                item.Index = p.Name;
                Houses[p.Name] = item;
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
            Tags.TemplateTag = new TagItem(Triggers.TemplateTrigger, "TGMPLATE");

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
                LocalVariables[p.Name] = new LocalVarItem(tmp[0], ParseBool(tmp[1]), p.Name);
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
                Celltags.AddObject(new CellTagItem(p.Name, p.Value));
            }
            foreach (INIPair p in entWaypoints.DataList)
            {
                Waypoints.AddObject(new WaypointItem(p.Value, p.Name));
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
