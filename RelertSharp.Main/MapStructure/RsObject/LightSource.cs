﻿using System;
using System.Collections.Generic;
using System.Drawing;
using RelertSharp.Common;
using RelertSharp.IniSystem;
using static RelertSharp.Utils.Misc;
using RelertSharp.MapStructure.Objects;

namespace RelertSharp.MapStructure.Points
{
    public class LightSourceCollection : PointCollectionBase<LightSource>
    {
        private int _i = 0;
        public LightSourceCollection()
        {

        }


        #region Public Methods
        public override void AddObject(LightSource item, bool forceRenew = false)
        {
            item.index = _i++.ToString();
            data[item.index] = item;
        }
        public void Compile(out Dictionary<string, INIEntity> compiledLightPost, INIEntity toc, out List<StructureItem> entities)
        {
            int i = 0;
            compiledLightPost = new Dictionary<string, INIEntity>();
            entities = new List<StructureItem>();
            foreach (LightSource light in this)
            {
                if (light.IsEnable)
                {
                    string name = string.Format("{0}_{1}", Constant.EntName.RsLightCompileHead, i);
                    string index = string.Format("{0}{1}", Constant.EntName.RsLightTocIndex, i);
                    INIEntity template = new INIEntity(GlobalVar.GlobalConfig.LightPostTemplate, name);
                    template["LightVisibility"] = light.Visibility;
                    template["LightIntensity"] = light.Intensity;
                    template["LightRedTint"] = light.Red;
                    template["LightGreenTint"] = light.Green;
                    template["LightBlueTint"] = light.Blue * 2;
                    template["Name"] = string.Format("{0} {1} - {2}", Constant.EntName.RsLightCompileName, i, light.Name);
                    compiledLightPost[name] = template;
                    toc[index] = name;

                    StructureItem bud = new StructureItem(name)
                    {
                        X = light.X,
                        Y = light.Y
                    };
                    bud.Owner = GlobalVar.GlobalMap.GetCivilianHouse() ?? Constant.MapStructure.DefaultCivilianHouse;
                    entities.Add(bud);
                    i++;
                }
            }
        }
        #endregion

        #region Public Calls
        public LightSource this[string name]
        {
            get { return data[name]; }
        }
        #endregion
    }



    public class LightSource : PointItemBase, IMapObject
    {
        private object[] SaveData { get { return new object[] { Name, Visibility, Intensity, Red, Green, Blue, IsEnable ? 1 : 0 }; } }
        internal string index;


        #region Ctor
        public LightSource(Color color, string name)
        {
            FromColor(color);
            Name = name;
            X = -1000;
            Y = -1000;
        }
        public LightSource(float r, float g, float b, string name)
        {
            Red = r;
            Green = g;
            Blue = b;
            Name = name;
        }
        public LightSource(INIPair p)
        {
            int coord = p.ParseInt();
            X = CoordIntX(coord);
            Y = CoordIntY(coord);
            string[] tmp = p.ParseStringList();
            if (tmp.Length != Constant.MapStructure.ArgLenLightSource) throw new Exception();
            Name = tmp[0];
            Visibility = ParseInt(tmp[1], 5000);
            Intensity = ParseFloat(tmp[2], 0.2f);
            Red = ParseFloat(tmp[3], 0.05f);
            Green = ParseFloat(tmp[4], 0.05f);
            Blue = ParseFloat(tmp[5], 0.05f);
            IsEnable = IniParseBool(tmp[6], true);
        }
        public LightSource(LightSource src) : base(src)
        {
            Name = src.Name;
            Visibility = src.Visibility;
            Intensity = src.Intensity;
            Red = src.Red;
            Green = src.Green;
            Blue = src.Blue;
        }
        private LightSource()
        {

        }
        #endregion


        #region Public Methods
        public void FromColor(Color c)
        {
            Red = c.R / 255f;
            Green = c.G / 255f;
            Blue = c.B / 127f;
        }
        public Color ToColor()
        {
            float r = Red * 255f;
            float g = Green * 255f;
            float b = Blue * 127f;
            float max = MaxItem(r, g, b);
            float scale = 255 / max;
            return Color.FromArgb((byte)(r * scale), (byte)(g * scale), (byte)(b * scale));
        }
        public Vec4 ToVec4()
        {
            Vec4 color = new Vec4(Red + 1, Green + 1, Blue + 1, 1);
            float shift = LightSource.IntensityShift(Intensity);
            return color * Vec4.Unit3(shift);
        }
        public string FormatString()
        {
            return string.Format(
                "LightVisibility={0}\nLightIntensity={1}\nLightRedTint={2}\nLightGreenTint={3}\nLightBlueTint={4}",
                Visibility,
                Intensity,
                Red,
                Green,
                Blue);
        }
        public INIPair SaveToPair()
        {
            string value = SaveData.JoinBy();
            return new INIPair(Coord.ToString(), value);
        }
        public override string ToString()
        {
            if (!IsLocationSetted) return string.Format("{0}: Not set in map", Name);
            return string.Format("{0}: {1}, {2}", Name, X, Y);
        }
        public bool Equal(LightSource dest)
        {
            return Red == dest.Red &&
                Green == dest.Green &&
                Blue == dest.Blue &&
                Intensity == dest.Intensity &&
                Visibility == dest.Visibility;
        }

        public static float IntensityShift(float intensity)
        {
            return intensity >= 0 ? 1 + intensity * 1.5f : 0.9384f + 0.44f * intensity;
        }
        public string[] ExtractParameter()
        {
            return new string[]
            {
                Red.ToString(),
                Green.ToString(),
                Blue.ToString(),
                Intensity.ToString(),
                Visibility.ToString(),
                IsEnable.ZeroOne(),
                X.ToString(),
                Y.ToString()
            };
        }
        public IMapObject ConstructFromParameter(string[] command)
        {
            ParameterReader reader = new ParameterReader(command);
            LightSource source = new LightSource()
            {
                Red = reader.ReadFloat(0.05f),
                Green = reader.ReadFloat(0.05f),
                Blue = reader.ReadFloat(0.05f),
                Intensity = reader.ReadFloat(0.02f),
                Visibility = reader.ReadInt(5000),
                IsEnable = reader.ReadBool(true),
                X = reader.ReadInt(),
                Y = reader.ReadInt()
            };
            return source;
        }
        #endregion


        #region Private Methods
        private byte ToByte(float color, float scale)
        {
            return (byte)Math.Min(Math.Floor(color * scale), 255);
        }
        #endregion


        #region Public Calls
        public float Red { get; set; } = 0.05f;
        public float Green { get; set; } = 0.05f;
        public float Blue { get; set; } = 0.05f;
        public float Intensity { get; set; } = 0.02f;
        public int Visibility { get; set; } = 5000;
        public int Range { get { return (Visibility >> 8) + 1; } }
        public bool IsEnable { get; set; } = true;
        public bool IsLocationSetted { get { return X > 0 && Y > 0; } }
        public override MapObjectType ObjectType { get { return MapObjectType.LightSource; } }
        #endregion
    }
}
