using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using RelertSharp.Common;
using RelertSharp.IniSystem;
using static RelertSharp.Utils.Misc;
using RelertSharp.DrawingEngine.Presenting;

namespace RelertSharp.MapStructure.Points
{
    public class LightSourceCollection : PointCollectionBase<LightSource>
    {
        public LightSourceCollection()
        {

        }



        public override void AddObject(LightSource item)
        {
            data[item.Name] = item;
        }

        public LightSource this[string name]
        {
            get { return data[name]; }
        }
    }



    public class LightSource : PointItemBase, IMapObject
    {
        private object[] SaveData { get { return new object[] { Visibility, Intensity, Red, Green, Blue, IsEnable ? 1 : 0 }; } }


        #region Ctor
        public LightSource(Color color, string name)
        {
            Red = color.R / 256f;
            Green = color.G / 256f;
            Blue = color.B / 256f;
            Name = name;
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
            Name = p.Name;
            string[] tmp = p.ParseStringList();
            if (tmp.Length != Constant.MapStructure.ArgLenLightSource) throw new Exception();
            Visibility = ParseInt(tmp[0], 5000);
            Intensity = ParseFloat(tmp[1], 0.2f);
            Red = ParseFloat(tmp[2], 0.05f);
            Green = ParseFloat(tmp[3], 0.05f);
            Blue = ParseFloat(tmp[4], 0.05f);
            IsEnable = ParseBool(tmp[5], true);
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
        #endregion


        #region Public Methods
        public void FromColor(Color c)
        {
            Red = c.R / 256f;
            Green = c.G / 256f;
            Blue = c.B / 256f;
        }
        public Color ToColor()
        {
            float max = MaxItem(Red, Green, Blue);
            float scale = 255 / max;
            return Color.FromArgb(ToByte(Red, scale), ToByte(Green, scale), ToByte(Blue, scale));
        }
        public Vec4 ToVec4()
        {
            Vec4 color = new Vec4(Red + 1, Green + 1, Blue + 1, 1);
            return color * Vec4.Unit3(1f + Intensity * 1.5f);
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
            return new INIPair(Name, value);
        }
        public override string ToString()
        {
            return string.Format("{0}: {1}, {2}", Name, X, Y);
        }
        #endregion


        #region Private Methods
        private byte ToByte(float color, float scale)
        {
            return (byte)Math.Min(Math.Floor(color * scale), 255);
        }
        #endregion


        #region Public Calls
        public string Name { get; set; }
        public float Red { get; set; } = 0.05f;
        public float Green { get; set; } = 0.05f;
        public float Blue { get; set; } = 0.05f;
        public float Intensity { get; set; } = 0.2f;
        public int Visibility { get; set; } = 5000;
        public int Range { get { return (Visibility >> 8) + 1; } }
        public bool IsEnable { get; set; } = true;
        public new PresentMisc SceneObject { get; set; }
        IPresentBase IMapScenePresentable.SceneObject { get { return SceneObject; } set { SceneObject = (PresentMisc)value; } }
        #endregion
    }
}
