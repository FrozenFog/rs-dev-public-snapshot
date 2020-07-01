using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using RelertSharp.Common;
using RelertSharp.IniSystem;
using RelertSharp.FileSystem;
using static RelertSharp.Common.GlobalVar;
using static RelertSharp.Language;

namespace RelertSharp.MapStructure
{
    public class MapTheaterTileSet
    {
        private List<string> tileNameIndex = new List<string>();
        private Dictionary<int, TileSet> tileSets = new Dictionary<int, TileSet>();
        private Dictionary<string, int> general = new Dictionary<string, int>();
        private readonly List<string> _subs = new List<string>(){ "tem", "des", "urb", "ubn", "sno", "lun" };


        #region Ctor - MapTheaterTileSet
        public MapTheaterTileSet(TheaterType _type)
        {
            string _theater = "";
            switch (_type)
            {
                case TheaterType.Temprate:
                    _theater = GlobalConfig["INI"]["TemperateTheme"];
                    TheaterSub = "tem";
                    break;
                case TheaterType.Desert:
                    _theater = GlobalConfig["INI"]["DesertTheme"];
                    TheaterSub = "des";
                    break;
                case TheaterType.Urban:
                    _theater = GlobalConfig["INI"]["UrbanTheme"];
                    TheaterSub = "urb";
                    break;
                case TheaterType.NewUrban:
                    _theater = GlobalConfig["INI"]["NewUrbanTheme"];
                    TheaterSub = "ubn";
                    break;
                case TheaterType.Snow:
                    _theater = GlobalConfig["INI"]["SnowTheme"];
                    TheaterSub = "sno";
                    break;
                case TheaterType.Lunar:
                    _theater = GlobalConfig["INI"]["LunarTheme"];
                    TheaterSub = "lun";
                    break;
                case TheaterType.Custom1:
                    _theater = GlobalConfig["INI"]["Custom1Theater"];
                    TheaterSub = GlobalConfig["CustomTheater"]["Custom1Sub"];
                    _subs.Add(TheaterSub);
                    break;
                case TheaterType.Custom2:
                    _theater = GlobalConfig["INI"]["Custom2Theater"];
                    TheaterSub = GlobalConfig["CustomTheater"]["Custom2Sub"];
                    _subs.Add(TheaterSub);
                    break;
                case TheaterType.Custom3:
                    _theater = GlobalConfig["INI"]["Custom3Theater"];
                    TheaterSub = GlobalConfig["CustomTheater"]["Custom3Sub"];
                    _subs.Add(TheaterSub);
                    break;
                default:
                    return;
            }
            SetGlobalPal();
            INIFile _theaterIni = GlobalDir.GetFile(_theater, FileExtension.INI);
            LoadGeneral(_theaterIni["General"]);
            int _cap = _theaterIni.IniData.Count;
            int current = 0;
            for (int i = 0; ; i++)
            {
                string _setName = "TileSet" + string.Format("{0:D4}", i);
                if (!_theaterIni.IniDict.Keys.Contains(_setName)) break;
                INIEntity ent = _theaterIni.PopEnt(_setName);
                int frameworkIndex = ent.ParseInt("MarbleMadness");
                int originalIndex = ent.ParseInt("NonMarbleMadness", -1);
                string _tileFileName = ent["FileName"];
                int _numsInSet = ent.ParseInt("TilesInSet");
                if (_numsInSet == 0) continue;
                TileSet set = new TileSet(originalIndex != -1, ent.ParseBool("AllowToPlace", true), frameworkIndex)
                {
                    Offset = current,
                    FileName = string.Format("{0}{1}{2}.{3}", _tileFileName, "{0:D2}", "{1}", TheaterSub),
                    SetName = ent["SetName"],
                    SetIndex = i
                };
                if (set.IsFramework) set.OriginalSet = originalIndex;
                for (int j = 1;j<_numsInSet + 1; j++)
                {
                    string filename = string.Format("{0}{1:D2}", _tileFileName, j);
                    current++;
                    int cap = 0;
                    for (char c = 'a'; ; c++)
                    {
                        string varyName = string.Format("{0}{1}.{2}", filename, c, TheaterSub);
                        if (GlobalDir.HasFile(varyName))
                        {
                            cap++;
                        }
                        else break;
                    }
                    set.AddTile(cap);
                    tileNameIndex.Add(_tileFileName.ToLower() + string.Format("{0:D2}", j) + "." + TheaterSub);
                }
                tileSets[i] = set;
            }
        }
        #endregion


        #region Private Methods - MapTheaterTileSet
        private void SetGlobalPal()
        {
            string palName = string.Format("iso{0}.pal", TheaterSub);
            PalFile pal = new PalFile(GlobalDir.GetRawByte(palName), palName);
            TilePalette = pal;
        }
        private void LoadGeneral(INIEntity entGeneral)
        {
            foreach (INIPair p in entGeneral)
            {
                general[p.Name] = p.ParseInt();
            }
            SafeLoad(Constant.TileSetClass.Clear, "ClearTile");
            SafeLoad(Constant.TileSetClass.Rough, "RoughTile");
            SafeLoad(Constant.TileSetClass.Ramp, "RampBase");
            SafeLoad(Constant.TileSetClass.Pave, "PaveTile");
            SafeLoad(Constant.TileSetClass.Green, "GreenTile");
            SafeLoad(Constant.TileSetClass.Sand, "SandTile");
            SafeLoad(Constant.TileSetClass.Water, "WaterSet");
        }
        private void SafeLoad(string DICTkey, string generalKey)
        {
            if (general.Keys.Contains(generalKey)) GeneralTilesets[DICT[DICTkey]] = general[generalKey];
        }
        private TileSet GetTileSet(ref int tileIndex)
        {
            if (tileIndex == 65535 || tileIndex >= tileNameIndex.Count) tileIndex = 0;
            if (tileIndex != 0)
            {
                for (int i = 0; i < tileSets.Count; i++)
                {
                    if (tileIndex < tileSets.ElementAt(i).Value.Offset)
                    {
                        return tileSets.ElementAt(i - 1).Value;
                    }
                }
                return tileSets.Last().Value;
            }
            return tileSets[0];
        }
        private TileSet GetTileSet(int tileindex)
        {
            int i = tileindex;
            return GetTileSet(ref i);
        }
        private string GetFrameworkNameSafe(string filename)
        {
            if (GlobalDir.HasFile(filename)) return filename;
            else
            {
                foreach (string sub in _subs)
                {
                    string name = filename.Substring(0, filename.Length - 3) + sub;
                    if (GlobalDir.HasFile(name)) return name;
                }
                throw new RSException.InvalidFileException(filename);
            }
        }
        #endregion


        #region Public Methods - MapTheaterTileSet
        public bool IsValidTile(int _tileindex)
        {
            return tileNameIndex.Count > _tileindex;
        }
        public string NameAsTheater(string name)
        {
            return string.Format("{0}.{1}", name.ToLower(), TheaterSub);
        }
        public string GetFrameworkFromTile(Tile t, out bool isHyte)
        {
            try
            {
                isHyte = false;
                TileSet set = GetTileSet(t.TileIndex);
                if (set.FrameworkSet != 0)
                {
                    string name = tileSets[set.FrameworkSet].GetName(t.TileIndex - set.Offset, false);
                    return GetFrameworkNameSafe(name);
                }
                isHyte = true;
                string hyte = tileSets[general["HeightBase"]].GetBaseHeightName(t.Height + 1);
                return GetFrameworkNameSafe(hyte);
            }
            catch (RSException.InvalidFileException e)
            {
                Log.Write("Framework {0} has not found!", e.FileName);
                isHyte = true;
                string hyte = tileSets[general["HeightBase"]].GetBaseHeightName(t.Height + 1);
                return GetFrameworkNameSafe(hyte);
            }
        }
        /// <summary>
        /// Tiles that exceed source set will be trimmed
        /// </summary>
        /// <param name="src"></param>
        /// <param name="isHyte"></param>
        /// <returns></returns>
        public TileSet GetFrameworkFromSet(TileSet src, out bool isHyte)
        {
            isHyte = false;
            if (src.FrameworkSet != 0)
            {
                TileSet framework = new TileSet(tileSets[src.FrameworkSet]);
                framework.SetMaxIndex(src.Count);
                return framework;
            }
            else
            {
                isHyte = true;
                return src;
            }
        }
        #endregion


        #region Public Calls - MapTheaterTileSet
        public string TheaterSub { get; private set; }
        public string this[int _TileIndex]
        {
            get
            {
                int i = _TileIndex;
                TileSet set = GetTileSet(ref i);
                return set.GetName(i);
            }
        }
        public IEnumerable<TileSet> TileSets { get { return tileSets.Values; } }
        public Dictionary<string, int> GeneralTilesets { get; private set; } = new Dictionary<string, int>();
        #endregion
    }
}
