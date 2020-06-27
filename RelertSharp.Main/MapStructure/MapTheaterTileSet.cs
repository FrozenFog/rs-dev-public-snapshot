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

namespace RelertSharp.MapStructure
{
    public class MapTheaterTileSet
    {
        private List<string> tileNameIndex = new List<string>();
        private List<TileSet> tileSets = new List<TileSet>();


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
                    break;
                case TheaterType.Custom2:
                    _theater = GlobalConfig["INI"]["Custom2Theater"];
                    TheaterSub = GlobalConfig["CustomTheater"]["Custom2Sub"];
                    break;
                case TheaterType.Custom3:
                    _theater = GlobalConfig["INI"]["Custom3Theater"];
                    TheaterSub = GlobalConfig["CustomTheater"]["Custom3Sub"];
                    break;
                default:
                    return;
            }
            INIFile _theaterIni = GlobalDir.GetFile(_theater, FileExtension.INI);
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
                TileSet set = new TileSet(originalIndex != -1, ent.ParseBool("AllowToPlace", true), frameworkIndex);
                set.Offset = current;
                set.FileName = string.Format("{0}{1}{2}.{3}", _tileFileName, "{0:D2}", "{1}", TheaterSub);
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
                tileSets.Add(set);
            }
        }
        #endregion


        #region Private Methods - MapTheaterTileSet
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
        #endregion


        #region Public Calls - MapTheaterTileSet
        public string TheaterSub { get; private set; }
        public string this[int _TileIndex]
        {
            get
            {
                if (_TileIndex == 65535 || _TileIndex >= tileNameIndex.Count) _TileIndex = 0;
                if (_TileIndex == 0) return tileSets[0].GetName(_TileIndex);
                for (int i = 0; i < tileSets.Count; i++)
                {
                    if (_TileIndex < tileSets[i].Offset)
                    {
                        return tileSets[i - 1].GetName(_TileIndex);
                    }
                }
                return tileNameIndex[0];
            }
        }
        #endregion
    }
}
