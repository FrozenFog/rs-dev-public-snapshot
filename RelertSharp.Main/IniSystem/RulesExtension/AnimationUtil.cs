using RelertSharp.Common;
using RelertSharp.FileSystem;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RelertSharp.Common.Constant;

namespace RelertSharp.IniSystem
{
    public static partial class RulesExtension
    {
        public static AnimationComponent GetAnimByRegName(this Rules r, string regName)
        {
            return GetComponent(r, regName);
        }

        private static AnimationComponent GetComponent(Rules r, string regName)
        {
            INIFile art = r.Art;
            if (!art.HasIniEnt(regName)) return null;
            INIEntity ent = art[regName];
            AnimationComponent component = new AnimationComponent()
            {
                RegName = regName
            };
            string shpName = GetShpName(art, ent, regName) + EX_SHP;
            string alphaName = ent[KEY_ALPHA] + EX_SHP;
            string customPal = ent[KEY_CUSPAL];
            component.Shp = GetShpFile(shpName);
            component.Pal = GetPalFile(customPal);
            component.Alpha = GetShpFile(alphaName);

            if (ent.HasPair(KEY_TRAILER)) component.TrailerAnim = GetComponent(r, ent[KEY_TRAILER]);
            if (ent.HasPair(KEY_NEXTANIM)) component.NextAnim = GetComponent(r, ent[KEY_NEXTANIM]);

            return component;
        }

        private static string GetShpName(INIFile art, INIEntity ent, string regname)
        {
            if (ent.HasPair(KEY_IMAGE))
            {
                string img = ent[KEY_IMAGE];
                INIEntity imgEnt = art.GetEnt(img);
                if (imgEnt == null) return img;
                else return GetShpName(art, imgEnt, img);
            }
            return regname;
        }

        private static ShpFile GetShpFile(string name)
        {
            if (name.IsNullOrEmpty()) return null;
            if (GlobalVar.GlobalDir.TryGetRawByte(name, out byte[] bytes))
            {
                return new ShpFile(bytes, name);
            }
            return null;
        }
        private static PalFile GetPalFile(string name)
        {
            if (name.IsNullOrEmpty()) return null;
            if (GlobalVar.GlobalDir.TryGetRawByte(name, out byte[] bytes))
            {
                return new PalFile(bytes, name);
            }
            return null;
        }
    }

    public class AnimationComponent : IRegistable, IDisposable
    {
        private int frameCount = -1;
        internal ShpFile Shp;
        internal ShpFile Alpha;
        internal PalFile Pal;
        #region Ctor
        public AnimationComponent()
        {

        }
        #endregion

        #region Api
        public void Dispose()
        {
            Shp?.Dispose();
            Alpha?.Dispose();
            Pal?.Dispose();
            NextAnim?.Dispose();
            TrailerAnim?.Dispose();
        }
        public void ResetBitmap()
        {
            Shp.ResetPal();
            NextAnim?.ResetBitmap();
            TrailerAnim?.ResetBitmap();
        }
        public void SetFrame(int frame, Color transparentColor, PalFile pal = null)
        {
            if (frame >= Shp.Count)
            {
                NextAnim?.SetFrame(frame - Shp.Count, transparentColor, pal);
            }
            else
            {
                if (Pal == null) Shp.Frames[frame].SetBitmap(pal, transparentColor);
                else Shp.Frames[frame].SetBitmap(Pal, transparentColor);
                if (Alpha != null) Shp.ApplyAlphaShp(Alpha, frame, transparentColor);
            }
            TrailerAnim?.SetFrame(GetTrailerFrame(frame), transparentColor, pal);
        }
        public List<Bitmap> GetImageByFrame(int frame)
        {
            List<Bitmap> bmps = new List<Bitmap>();
            if (frame >= Shp.Count)
            {
                return NextAnim.GetImageByFrame(frame - Shp.Count);
            }
            else
            {
                bmps.Add(Shp.Frames[frame].Image);
                if (TrailerAnim != null) bmps.AddRange(TrailerAnim.GetImageByFrame(GetTrailerFrame(frame)));
            }
            return bmps;
        }
        public void FrameXY(int frame, out int x, out int y)
        {
            if (frame >= Shp.Count)
            {
                NextAnim.FrameXY(frame - Shp.Count, out int xNext, out int yNext);
                x = xNext;
                y = yNext;
            }
            else
            {
                x = Shp.Frames[frame].X;
                y = Shp.Frames[frame].Y;
            }
        }
        public void FrameWH(int frame, out int width, out int height)
        {
            if (frame >= Shp.Count)
            {
                NextAnim.FrameWH(frame - Shp.Count, out int wNext, out int hNext);
                width = wNext;
                height = hNext;
            }
            else
            {
                width = Shp.Width;
                height = Shp.Height;
            }
        }
        #endregion


        #region Private
        private int GetTrailerFrame(int frame)
        {
            if (TrailerAnim == null) return 0;
            return frame % TrailerAnim.FrameCount;
        }
        #endregion


        #region Public Calls
        public string RegName { get; set; }
        public int FrameCount
        {
            get
            {
                if (frameCount < 0)
                {
                    frameCount = 0;
                    if (Shp != null) frameCount += Shp.Count;
                    if (NextAnim != null) frameCount += NextAnim.FrameCount;
                }
                return frameCount;
            }
        }
        public AnimationComponent NextAnim { get; set; }
        public AnimationComponent TrailerAnim { get; set; }
        #endregion
    }
}
