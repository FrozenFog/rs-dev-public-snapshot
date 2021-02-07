using RelertSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RelertSharp.Utils.Misc;
using static RelertSharp.Common.GlobalVar;

namespace RelertSharp.IniSystem
{
    public static partial class RulesExtension
    {
        private static Dictionary<string, Vec3> bufferedBuildingShape = new Dictionary<string, Vec3>();

        public static List<bool> GetBuildingCustomShape(this Rules r, string regname, int sizeX, int sizeY)
        {
            List<bool> shape = InitializeListWithCap<bool>(sizeX * sizeY);
            string artname = r.GetArtEntityName(regname);
            INIEntity art = r.Art[artname];
            string foundation = (string)art["Foundation"].ToLower();
            if (foundation == "custom")
            {
                for (int i = 0; i < sizeX * sizeY; i++)
                {
                    string found = string.Format("Foundation.{0}", i);
                    if (art.HasPair(found))
                    {
                        int[] tmp = art.ParseIntList(found);
                        if (tmp.Length != 2)
                        {
                            Log.Critical("Building foundation error! Item {0} has unreadable foundation!", regname);
                            shape.SetValueAll(true);
                            return shape;
                        }
                        try
                        {
                            shape[tmp[0] + tmp[1] * sizeX] = true;
                        }
                        catch
                        {
                            Log.Critical("Building foundation error! Item {0}: size {1},{2} has unreadable foundation!", regname, sizeX, sizeY);
                            shape.SetValueAll(true);
                        }
                    }
                }
            }
            else shape.SetValueAll(true);
            return shape;
        }
        public static void GetBuildingShapeData(this Rules r, string nameid, out int height, out int foundX, out int foundY)
        {
            Vec3 sz;
            if (!bufferedBuildingShape.Keys.Contains(nameid))
            {
                sz = new Vec3();
                string artname = r.GetArtEntityName(nameid);
                INIEntity art = r.Art[artname];

                string foundation = (string)art["Foundation"].ToLower();
                if (!string.IsNullOrEmpty(foundation))
                {
                    if (foundation == "custom")
                    {
                        sz.X = art.ParseInt("Foundation.X", 1);
                        sz.Y = art.ParseInt("Foundation.Y", 1);
                    }
                    else
                    {
                        string[] tmp = foundation.Split('x');
                        sz.X = int.Parse(tmp[0]);
                        sz.Y = int.Parse(tmp[1]);
                    }
                }
                else
                {
                    sz.X = 1;
                    sz.Y = 1;
                }
                sz.Z = art.ParseInt("Height", 5) + 5;
                bufferedBuildingShape[nameid] = sz;
            }
            else sz = bufferedBuildingShape[nameid];
            foundX = (int)sz.X == 0 ? 1 : (int)sz.X;
            foundY = (int)sz.Y == 0 ? 1 : (int)sz.Y;
            height = (int)sz.Z;
        }
        public static void GetSmudgeSizeData(this Rules r, string nameid, out int foundx, out int foundy)
        {
            INIEntity ent = r[nameid];
            foundx = ent.ParseInt("Width", 1);
            foundy = ent.ParseInt("Height", 1);
        }
        public static Vec4 GetBuildingLampData(this Rules rules, string nameid, out float intensity, out int visibility)
        {
            if (!rules.HasIniEnt(nameid) || string.IsNullOrEmpty(rules[nameid].Name))
            {
                intensity = 0;
                visibility = 0;
                return Vec4.Unit3(0);
            }
            else
            {
                INIEntity item = rules[nameid];
                visibility = item.ParseInt("LightVisibility", 5000);
                intensity = item.ParseFloat("LightIntensity");
                float r = item.ParseFloat("LightRedTint") + 1;
                float g = item.ParseFloat("LightGreenTint") + 1;
                float b = item.ParseFloat("LightBlueTint") + 1;
                return new Vec4(r, g, b, 1);
            }
        }
    }
}
