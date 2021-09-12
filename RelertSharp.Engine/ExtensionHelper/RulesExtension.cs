using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.Common;
using RelertSharp.IniSystem;
using RelertSharp.MapStructure.Objects;
using static RelertSharp.Common.GlobalVar;
using static RelertSharp.Utils.Misc;
using static RelertSharp.Common.Constant;
using RelertSharp.Engine.Common;

namespace RelertSharp.Engine
{
    internal static class RulesExtension
    {
        #region Public
        internal static BuildingData GetBuildingData(this Rules r, string name)
        {
            var adjust = GlobalConfig.DrawingAdjust;
            BuildingData data = new BuildingData();
            string artname = r.GetArtEntityName(name);
            INIEntity art = r.Art[artname];
            if (string.IsNullOrEmpty(art.Name)) data.SelfId = name + EX_SHP;
            else data.SelfId = GuessStructureName(art);

            data.AlphaImage = r[name]["AlphaImage"];
            if (!string.IsNullOrEmpty(data.AlphaImage)) data.AlphaImage += EX_SHP;

            if (!adjust.DeactivateAnim.Any(x => x.Name == artname))
            {
                data.ActivateAnim = GuessStructureName(r.Art[art["ActiveAnim"]]);
                data.ActivateAnimTwo = GuessStructureName(r.Art[art["ActiveAnimTwo"]]);
                data.ActivateAnimThree = GuessStructureName(r.Art[art["ActiveAnimThree"]]);
            }

            data.IdleAnim = GuessStructureName(r.Art[art["IdleAnim"]]);
            data.SuperAnim = GuessStructureName(r.Art[art["SuperAnim"]]);
            if (!adjust.DeactivateBib.Any(x => x.Name == artname))
            {
                data.BibAnim = GuessStructureName(art["BibShape"]);
            }

            data.TurretAnimIsVoxel = r[name].ParseBool("TurretAnimIsVoxel");
            if (data.TurretAnimIsVoxel)
            {
                data.TurretAnim = r[name]["TurretAnim"] + EX_VXL;
                if (data.TurretAnim.ToLower().Contains("tur"))
                {
                    data.TurretBarrel = data.TurretAnim.ToLower().Replace("tur", "barl");
                }
            }
            else data.TurretAnim = GuessStructureName(r.Art[r[name]["TurretAnim"]]);

            data.nSelf = GlobalDir.GetShpFrameCount(data.SelfId, out bool bSelf);
            bool bTurret = true;
            if (!data.TurretAnimIsVoxel)
            {
                data.nTurretAnim = GlobalDir.GetShpFrameCount(data.TurretAnim, out bool turrEmpty);
                bTurret = turrEmpty;
            }
            else
            {
                data.nTurretAnim = 0;
                data.TurretOffset = r[name].ParseInt("TurretAnimZAdjust");
                bTurret = false;
            }

            data.TurretAnimZAdjust = r[name].ParseInt("TurretAnimZAdjust");
            data.ActiveAnimZAdjust = art.ParseInt("ActiveAnimZAdjust");
            data.ActiveAnimTwoZAdjust = art.ParseInt("ActiveAnimTwoZAdjust");
            data.ActiveAnimThreeZAdjust = art.ParseInt("ActiveAnimThreeZAdjust");
            data.ActiveAnimFourZAdjust = art.ParseInt("ActiveAnimZFourAdjust");
            data.IdleAnimZAdjust = art.ParseInt("IdleAnimZAdjust");
            data.SuperAnimZAdjust = art.ParseInt("SuperAnimZAdjust");

            data.Plug1ZAdjust = art.ParseInt("PowerUp1LocZZ");
            data.Plug2ZAdjust = art.ParseInt("PowerUp2LocZZ");
            data.Plug3ZAdjust = art.ParseInt("PowerUp3LocZZ");

            data.nActivateAnim = GlobalDir.GetShpFrameCount(data.ActivateAnim, out bool bAnim);
            data.nActivateAnimTwo = GlobalDir.GetShpFrameCount(data.ActivateAnimTwo, out bool bAnim2);
            data.nActivateAnimThree = GlobalDir.GetShpFrameCount(data.ActivateAnimThree, out bool bAnim3);
            data.nIdleAnim = GlobalDir.GetShpFrameCount(data.IdleAnim, out bool bIdle);
            data.nSuperAnim = GlobalDir.GetShpFrameCount(data.SuperAnim, out bool bSuper);
            data.nBibAnim = GlobalDir.GetShpFrameCount(data.BibAnim, out bool bBib);
            data.IsEmpty = bSelf && bAnim && bAnim2 && bAnim3 && bIdle && bSuper && bBib && bTurret;
            return data;
        }
        public static string GetOverlayName(this Rules r, byte overlayid)
        {
            INIEntity ov = r[RulesHead.HEAD_OVERLAY];
            return ov[overlayid.ToString()];
        }
        public static byte GetOverlayIndex(this Rules r, string regName)
        {
            INIEntity lst = r["OverlayTypes"];
            int index = lst.IndexOfValue(regName);
            if (index > -1 && index < 256) return (byte)index;
            else return 0;
        }
        public static int GetFrameFromDirection(this Rules r, int direction, string nameID)
        {
            string art = r.GetArtEntityName(nameID);
            INIEntity sequence = r.Art[r.Art[art]["Sequence"]];
            direction >>= 5;
            if (sequence.Name == "") return direction;
            int[] ready = sequence.ParseIntList("Ready");
            //int result = ready[0];
            int result = 7 - direction;
            return result < 0 ? 0 : result;
        }
        public static string GetCustomPaletteName(this Rules r, string nameid)
        {
            string artname = r.GetArtEntityName(nameid);
            INIEntity art = r.Art[artname];

            string pal = art["Palette"];
            if (string.IsNullOrEmpty(pal)) return pal;
            else return string.Format("{0}{1}.{2}", pal, TileDictionary.TheaterSub, "pal");
        }
        public static Pnt GetVoxTurOffset(this Rules r, string id)
        {
            string x = r[id]["TurretAnimX"];
            string y = r[id]["TurretAnimY"];
            return new Pnt()
            {
                X = string.IsNullOrEmpty(x) ? 0 : int.Parse(x),
                Y = string.IsNullOrEmpty(y) ? 0 : int.Parse(y)
            };
        }
        public static Pnt GetPlugOffset(this Rules r, string parent, int plugNum)
        {
            string xKey = string.Format("PowerUp{0}LocXX", plugNum);
            string yKey = string.Format("PowerUp{0}LocYY", plugNum);
            INIEntity art = r.Art[parent];
            int x = art.ParseInt(xKey);
            int y = art.ParseInt(yKey);
            return new Pnt(x, y);
        }
        public static INIEntity GetBuildingTurret(this Rules r, string nameid)
        {
            string turanim = r[nameid]["TurretAnim"];
            return r.Art[turanim];
        }
        public static string GetObjectImgName(this Rules r, ObjectItemBase inf, out short frame)
        {
            frame = 0;
            string img = r.GetArtEntityName(inf.RegName) + ".shp";
            frame = GlobalDir.GetShpFrameCount(img, out bool b);
            return img;
        }
        public static string GetUnitInfo(this Rules r, string id, ref string tur, ref string barl, ref bool vxl, out int turretOffset)
        {
            string artname = r.GetArtEntityName(id);
            INIEntity art = r.Art[artname];
            turretOffset = art.ParseInt("TurretOffset");
            vxl = IsVxl(r, artname);
            VxlFormating(artname, vxl, ref artname, ref tur, ref barl);
            return artname;
        }
        #endregion




        #region Private
        private static char _suff
        {
            get
            {
                switch (CurrentTheater)
                {
                    case TheaterType.Desert:
                        return 'D';
                    case TheaterType.Snow:
                        return 'A';
                    case TheaterType.Lunar:
                        return 'L';
                    case TheaterType.NewUrban:
                        return 'N';
                    case TheaterType.Temprate:
                        return 'T';
                    case TheaterType.Urban:
                        return 'U';
                    default:
                        return 'G';
                }
            }
        }
        private static string GuessStructureName(string id)
        {
            if (string.IsNullOrEmpty(id)) return "";
            string name = id + ".shp";
            if (GlobalConfig.DrawingAdjust.NoBudAltArt)
            {
                name = id.Replace(1, 'G') + ".shp";
            }
            else
            {
                name = id.Replace(1, _suff) + ".shp";
            }
            if (GlobalDir.HasFile(name)) return name;
            if (GlobalDir.HasFile(name.Replace(1, 'G'))) return name.Replace(1, 'G');
            if (GlobalDir.HasFile(id + ".shp")) return id + ".shp";
            return "";
        }
        private static string GuessStructureName(INIEntity ent)
        {
            string img = ent[KEY_IMAGE];
            if (string.IsNullOrEmpty(img)) return GuessStructureName(ent.Name);
            else return GuessStructureName(img);
        }
        //private static string GetArtName(string regname)
        //{
        //    string img = Rules[regname][KEY_IMAGE];
        //    if (string.IsNullOrEmpty(img)) return regname;
        //    if (Art.HasIniEnt(img)) return img;
        //    else return regname;
        //}
        private static void VxlFormating(string id, bool vxl, ref string name, ref string tur, ref string barl)
        {
            if (vxl)
            {
                tur = id + "tur.vxl";
                barl = id + "barl.vxl";
                name = id + ".vxl";
            }
            else name = id + ".shp";
        }
        private static bool IsVxl(Rules rules, string id)
        {
            return IniParseBool(rules.Art[id]["Voxel"]);
        }
        #endregion
    }
}
