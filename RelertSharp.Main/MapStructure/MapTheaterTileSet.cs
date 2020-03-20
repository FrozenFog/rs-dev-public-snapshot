using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.Common;
using RelertSharp.IniSystem;
using static RelertSharp.Common.GlobalVar;

namespace RelertSharp.MapStructure
{
    public class MapTheaterTileSet
    {
        private List<string> tileNameIndex = new List<string>();


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
                default:
                    return;
            }
            INIFile _theaterIni = GlobalDir.GetFile(_theater, FileExtension.INI);
            int _cap = _theaterIni.IniData.Count;
            for (int i = 0; i < _cap; i++)
            {
                string _setName = "TileSet" + string.Format("{0:D4}", i);
                if (!_theaterIni.IniDict.Keys.Contains(_setName)) break;
                INIEntity _tileSet = _theaterIni.PopEnt(_setName);
                string _tileFileName = _tileSet["FileName"];
                int _numsInSet = _tileSet.GetPair("TilesInSet").ParseInt();
                if (_numsInSet == 0) continue;
                for (int j = 1;j<_numsInSet + 1; j++)
                {
                    tileNameIndex.Add(_tileFileName.ToLower() + string.Format("{0:D2}", j) + "." + TheaterSub);
                }
            }
        }
        #endregion


        #region Public Methods - MapTheaterTileSet
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
                if (_TileIndex == 65535) return tileNameIndex[0];
                return tileNameIndex[_TileIndex];
            }
        }
        #endregion
    }
}
