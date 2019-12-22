using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using relert_sharp.FileSystem;
using relert_sharp.Common;
using relert_sharp.MapStructure.Logic;
using static relert_sharp.Utils.Misc;

namespace relert_sharp.MapStructure
{
    public class Map
    {
        private string mapFileName;
        private string mapPath;
        private string isomappack5String, overlayString, overlaydataString, previewString;
        private MapType maptype;

        private MapInfo info;
        private Rectangle previewSize;

        private TriggerCollection triggers;
        private ActionCollection actions;
        private EventCollection events;
        private TagCollection tags;

        private LocalVarCollection localvariables = new LocalVarCollection();
        private TeamCollection teams = new TeamCollection();
        private TaskforceCollection taskforces = new TaskforceCollection();
        private TeamScriptCollection scripts = new TeamScriptCollection();

        private TileLayer Tiles;
        private OverlayLayer Overlays;

        private Dictionary<string, INIEntity> residual;
        public Map(MapFile f)
        {
            mapFileName = f.FileName;
            mapPath = f.FilePath;
            isomappack5String = f.PopEnt("IsoMapPack5").JoinString();
            overlayString = f.PopEnt("OverlayPack").JoinString();
            overlaydataString = f.PopEnt("OverlayDataPack").JoinString();
            info = new MapInfo(f.PopEnt("Basic"), f.PopEnt("Map"), f.PopEnt("SpecialFlags"));
            GetPreview(f);
            GetAbstractLogics(f);
            GetTeam(f);
            Tiles = new TileLayer(isomappack5String, info.Size);
            Overlays = new OverlayLayer(overlayString, overlaydataString);
            residual = new Dictionary<string, INIEntity>(f.IniDict);

            isomappack5String = "";
            overlayString = "";
            overlaydataString = "";
        }


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
        private void GetTeam(MapFile f)
        {
            List<string> _teamList = f.PopEnt("TeamTypes").TakeValuesToList();
            List<string> _taskforceList = f.PopEnt("TaskForces").TakeValuesToList();
            List<string> _scriptList = f.PopEnt("ScriptTypes").TakeValuesToList();

            foreach (string teamID in _teamList)
            {
                teams[teamID] = new TeamItem(f.PopEnt(teamID));
            }
            foreach (string tfID in _taskforceList)
            {
                taskforces[tfID] = new TaskforceItem(f.PopEnt(tfID));
            }
            foreach (string scptID in _scriptList)
            {
                scripts[scptID] = new TeamScriptGroup(f.PopEnt(scptID));
            }
        }
        private void GetAbstractLogics(MapFile f)
        {
            INIEntity entEvent = f.PopEnt("Events");
            INIEntity entAction = f.PopEnt("Actions");
            INIEntity entTrigger = f.PopEnt("Triggers");
            INIEntity entTag = f.PopEnt("Tags");
            INIEntity entVar = f.PopEnt("VariableNames");

            triggers = new TriggerCollection(entTrigger);
            actions = new ActionCollection(entAction);
            events = new EventCollection(entEvent);
            tags = new TagCollection(entTag);
            
            foreach (INIPair p in entVar.DataList)
            {
                string[] tmp = p.ParseStringList();
                localvariables[tmp[0]] = ParseBool(tmp[1]);
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
