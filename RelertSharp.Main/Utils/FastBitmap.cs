using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace RelertSharp.Utils
{
    unsafe class FastBitmap : IDisposable
    {
        #region FastBitmap - Ctor
        public FastBitmap(Image img)
        {
            theBitmap = new Bitmap(img);
            LockBitmap();
        }

        public FastBitmap(Bitmap bmp)
        {
            if (bmp.PixelFormat == PixelFormat.Format32bppArgb)
                theBitmap = bmp;
            else
                theBitmap = bmp.Clone(new Rectangle(Point.Empty, bmp.Size), PixelFormat.Format32bppArgb);
            LockBitmap();
        }

        public FastBitmap(int width,int height)
        {
            theBitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            LockBitmap();
        }
        #endregion

        #region FastBitmap - Private Methods
        private void LockBitmap()
        {
            Width = theBitmap.Width;
            Height = theBitmap.Height;
            bytesPerRow = Image.GetPixelFormatSize(theBitmap.PixelFormat) / 8 * Width;
            if (bytesPerRow % 4 != 0)
                bytesPerRow = 4 * ((bytesPerRow / 4) + 1);
            bitmapData = theBitmap.LockBits(new Rectangle(Point.Empty, theBitmap.Size), ImageLockMode.ReadWrite, theBitmap.PixelFormat);
            pixelBase = (byte*)bitmapData.Scan0.ToPointer();

            IsDisposed = false;
        }
        #endregion

        #region FastBitmap - Public Methods
        public void UnlockBitmap()
        {
            theBitmap.UnlockBits(bitmapData);
            bitmapData = null;
            pixelBase = null;

        }

        public int GetPixel(int x, int y)
        {
            return *(int*)(pixelBase + (y * bytesPerRow) + (x * 4));
        }

        public void SetPixel(int x,int y,int pColor)
        {
            *(int*)(pixelBase + (y * bytesPerRow) + (x * 4)) = pColor;
        }

        public void SetPixel(int x,int y,Color pColor)
        {
            *(int*)(pixelBase + (y * bytesPerRow) + (x * 4)) = pColor.ToArgb();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        #region FastBitmap - Override Methods
        protected virtual void Dispose(bool disposing)
        {
            if (IsDisposed) return;
            if (disposing) UnlockBitmap();
            IsDisposed = true;
        }
        public override bool Equals(object obj) => obj is FastBitmap fastBitmap && theBitmap.Equals(fastBitmap.theBitmap);
        public override int GetHashCode() => theBitmap.GetHashCode();
        #endregion

        #region FastBitmap - Private Members
        private readonly Bitmap theBitmap;
        private BitmapData bitmapData;
        private int bytesPerRow;
        private byte* pixelBase;
        #endregion

        #region FastBitmap - Public Calls
        public int Width { get; private set; }
        public int Height { get; private set; }
        public bool IsDisposed { get; private set; }

        public static implicit operator Bitmap(FastBitmap fastBitmap) => fastBitmap.theBitmap;
        public static implicit operator Image(FastBitmap fastBitmap) => fastBitmap.theBitmap;
        #endregion
    }
}