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
        private Dictionary<int, TileAbstract> tileabstract;


        #region Ctor - MapTheaterTileSet
        public MapTheaterTileSet(TheaterType _type)
        {
            tileabstract = new Dictionary<int, TileAbstract>();
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
        public TileAbstract GetTileAbstract(int tileindex)
        {
            TileAbstract abs;
            if (!tileabstract.Keys.Contains(tileindex))
            {
                abs = new TileAbstract();
                string name = this[tileindex];
                TmpFile tmp = new TmpFile(GlobalDir.GetRawByte(name), name);
                for (int i = 0; i < tmp.Images.Count; i++)
                {
                    TileAbstract.SubTileAbstract sub = new TileAbstract.SubTileAbstract();
                    sub.ColorLeft = tmp[i].ColorRadarLeft;
                    sub.ColorRight = tmp[i].ColorRadarRight;
                    abs[i] = sub;
                }
                tileabstract[tileindex] = abs;
            }
            else abs = tileabstract[tileindex];
            return abs;
        }
        #endregion


        #region Public Calls - MapTheaterTileSet
        public string TheaterSub { get; private set; }
        public string this[int _TileIndex]
        {
            get
            {
                if (_TileIndex == 65535) return tileNameIndex[0];
                if (_TileIndex > tileNameIndex.Count) return tileNameIndex[0];
                return tileNameIndex[_TileIndex];
            }
        }
        #endregion
    }


    public class TileAbstract
    {
        private Dictionary<int, SubTileAbstract> subtiles = new Dictionary<int, SubTileAbstract>();
        public TileAbstract() { }


        public SubTileAbstract this[int subindex]
        {
            get
            {
                if (subtiles.Keys.Contains(subindex)) return subtiles[subindex];
                return subtiles[0];
            }
            set
            {
                subtiles[subindex] = value;
            }
        }


        public class SubTileAbstract
        {
            public SubTileAbstract() { }

            
            public Color ColorLeft { get; set; }
            public Color ColorRight { get; set; }
        }
    }
}
