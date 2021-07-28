using RelertSharp.Common;
using RelertSharp.FileSystem;
using RelertSharp.IniSystem;
using System.Collections.Generic;
using System.Linq;
using static RelertSharp.Common.GlobalVar;
using static RelertSharp.Language;

namespace RelertSharp.MapStructure
{
    public class MapTheaterTileSet
    {
        private List<string> tileNameIndex = new List<string>();
        private Dictionary<int, TileSet> tileSets = new Dictionary<int, TileSet>();
        private Dictionary<int, int> tileIndexToTileSet = new Dictionary<int, int>();
        private Dictionary<string, int> general = new Dictionary<string, int>();
        private readonly List<string> _subs = new List<string>() { "tem", "des", "urb", "ubn", "sno", "lun" };


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
                for (int j = 1; j < _numsInSet + 1; j++)
                {
                    string filename = string.Format("{0}{1:D2}", _tileFileName, j);
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
                    tileIndexToTileSet[current] = i;
                    current++;
                }
                tileSets[i] = set;
            }
        }
        #endregion


        #region Private Methods - MapTheaterTileSet
        private void SetLatSystem()
        {
            Constant.LATSystem.idxClear = general[Constant.LATSystem.sClear];
            Constant.LATSystem.idxRough = general[Constant.LATSystem.sRough];
            Constant.LATSystem.idxGreen = general[Constant.LATSystem.sGreen];
            Constant.LATSystem.idxPave = general[Constant.LATSystem.sPave];
            Constant.LATSystem.idxSand = general[Constant.LATSystem.sSand];

            Constant.LATSystem.idxC2S = general[Constant.LATSystem.sClearToSand];
            Constant.LATSystem.idxC2R = general[Constant.LATSystem.sClearToRough];
            Constant.LATSystem.idxC2G = general[Constant.LATSystem.sClearToGreen];
            Constant.LATSystem.idxC2P = general[Constant.LATSystem.sClearToPave];

            Constant.LATSystem.SetLat();
        }
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
            SafeLoad(Constant.TileSetClass.Rough, "SandTile");
            SafeLoad(Constant.TileSetClass.Ramp, "RampBase");
            SafeLoad(Constant.TileSetClass.Pave, "PaveTile");
            SafeLoad(Constant.TileSetClass.Green, "RoughTile");
            SafeLoad(Constant.TileSetClass.Sand, "GreenTile");
            SafeLoad(Constant.TileSetClass.Water, "WaterSet");
            SetLatSystem();
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
                return tileSets[tileIndexToTileSet[tileIndex]];
                //for (int i = 0; i < tileSets.Count; i++)
                //{
                //    if (tileIndex < tileSets.ElementAt(i).Value.Offset)
                //    {
                //        return tileSets.ElementAt(i - 1).Value;
                //    }
                //}
                //return tileSets.Last().Value;
            }
            return tileSets[0];
        }
        private TileSet GetTileSet(int tileindex)
        {
            int i = tileindex;
            return GetTileSet(ref i);
        }
        #endregion


        #region Public Methods - MapTheaterTileSet
        public bool LatEqual(int setIndexA, int setIndexB)
        {
            if (Constant.LATSystem.LatFull.Contains(setIndexA))
            {
                if (Constant.LATSystem.LatFull.Contains(setIndexB)) return setIndexA == setIndexB;
                else return setIndexA == SwitchLatIndex(setIndexB);
            }
            else
            {
                if (Constant.LATSystem.LatFull.Contains(setIndexB)) return setIndexA == SwitchLatIndex(setIndexB);
                else return setIndexA == setIndexB;
            }
        }
        public bool IsLat(int tileIndex)
        {
            int set = GetTileSet(tileIndex).SetIndex;
            return Constant.LATSystem.LatSet.Contains(set);
        }
        public bool IsLat(Tile t)
        {
            int set = GetTileSetIndexFromTile(t);
            return Constant.LATSystem.LatSet.Contains(set);
        }
        public bool IsClearLat(Tile changer, Tile centerReferance)
        {
            int set = GetTileSetFromTile(changer).SetIndex;
            int center = GetTileSetFromTile(centerReferance).SetIndex;
            bool latEqual = LatEqual(set, center);
            if (LatEqual(center, Constant.LATSystem.idxGreen))
            {
                if (!Constant.LATSystem.LatSet.Contains(set))
                {
                    if (changer.TileTerrainType == Constant.DrawingEngine.Tiles.Shore || changer.TileTerrainType == Constant.DrawingEngine.Tiles.LAT_D) return false;
                }
            }
            if (set == center && center != Constant.LATSystem.idxClear) return false;
            if (set == Constant.LATSystem.idxClear) return true;
            //if (Constant.LATSystem.LatFull.Contains(set)) return false;
            if (latEqual) return false;
            if (Constant.LATSystem.LatSet.Contains(set) && !latEqual) return true;
            return true;
        }
        public bool IsConnLat(int setIndex)
        {
            return Constant.LATSystem.LatConnect.Contains(setIndex);
        }
        public int SwitchLatIndex(int setIndex)
        {
            if (setIndex == Constant.LATSystem.idxSand) return Constant.LATSystem.idxC2S;
            else if (setIndex == Constant.LATSystem.idxPave) return Constant.LATSystem.idxC2P;
            else if (setIndex == Constant.LATSystem.idxGreen) return Constant.LATSystem.idxC2G;
            else if (setIndex == Constant.LATSystem.idxRough) return Constant.LATSystem.idxC2R;
            else if (setIndex == Constant.LATSystem.idxC2S) return Constant.LATSystem.idxSand;
            else if (setIndex == Constant.LATSystem.idxC2P) return Constant.LATSystem.idxPave;
            else if (setIndex == Constant.LATSystem.idxC2R) return Constant.LATSystem.idxRough;
            else if (setIndex == Constant.LATSystem.idxC2G) return Constant.LATSystem.idxGreen;
            return setIndex;
        }
        public TileSet SwapSet(TileSet src)
        {
            int index = SwitchLatIndex(src.SetIndex);
            return GetTileSetFromIndex(index);
        }
        public TileSet GetTileSetFromIndex(int index)
        {
            if (tileSets.Keys.Contains(index)) return tileSets[index];
            return null;
        }
        public TileSet GetTileSetFromTile(Tile t)
        {
            return GetTileSet(t.TileIndex);
        }
        public int GetTileSetIndexFromTile(Tile t)
        {
            return GetTileSet(t.TileIndex).SetIndex;
        }
        public bool IsValidTile(int _tileindex)
        {
            return tileNameIndex.Count > _tileindex;
        }
        public string NameAsTheater(string name)
        {
            return string.Format("{0}.{1}", name.ToLower(), TheaterSub);
        }
        public string GetFrameworkNameSafe(string filename)
        {
            if (GlobalDir.HasFile(filename)) return filename;
            else
            {
                foreach (string sub in _subs)
                {
                    string name = filename.Substring(0, filename.Length - 3) + sub;
                    if (GlobalDir.HasFile(name)) return name;
                }
                Log.Write(string.Format("Framework name {0} not found!", filename));
                return string.Empty;
                //throw new RSException.InvalidFileException(filename);
            }
        }
        public string GetFrameworkFromTile(Tile t, out bool isHyte)
        {
            try
            {
                isHyte = false;
                TileSet set = GetTileSet(t.TileIndex);
                if (set.FrameworkSet != 0 && tileSets.ContainsKey(set.FrameworkSet))
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
        /// <summary>
        /// Return theater default ramp tileset
        /// </summary>
        /// <returns></returns>
        public TileSet GetRampSet()
        {
            return tileSets[general["RampBase"]];
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
