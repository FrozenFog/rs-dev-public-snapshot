using RelertSharp.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RelertSharp.Engine
{
    internal static class CppExtern
    {
        private const string name = "CncVxlRenderText.dll";
        private static readonly object cppLock = new object();
        private static MiniTimer timer = new MiniTimer();

        public static class Scene
        {
            [DllImport(name)]
            public static extern bool SetSceneFont(string fontName, int size);
            [DllImport(name)]
            private static extern void ScenePositionToClientPosition(Vec3 pos, ref Pnt result);
            public static void ScenePositionToClientPositionLock(Vec3 pos, ref Pnt result)
            {
                lock (cppLock)
                {
                    ScenePositionToClientPosition(pos, ref result);
                }
            }
            [DllImport(name)]
            private static extern void ClientPositionToScenePosition(Pnt pos, ref Vec3 result);
            public static void ClientPositionToScenePositionLock(Pnt pos, ref Vec3 result)
            {
                lock (cppLock)
                {
                    ClientPositionToScenePosition(pos, ref result);
                }
            }


            [DllImport(name)]
            public static extern void MoveFocusOnScene(Vec3 Displacement);
            [DllImport(name)]
            public static extern void SetFocusOnScene(Vec3 pos);
            [DllImport(name)]
            public static extern void MoveFocusOnScreen(float x, float y);


            [DllImport(name)]
            public static extern bool ResetSceneView();
            [DllImport(name)]
            private static extern void PresentAllObject();
            public static void PresentAllObjectLock()
            {
                lock (cppLock)
                {
                    timer.Start();
                    PresentAllObject();
                    timer.Stop();
                    long avg = timer.Average;
                }
            }
            [DllImport(name)]
            public static extern void ClearSceneObjects();
            [DllImport(name)]
            public static extern bool SetUpScene(int width, int height);
            [DllImport(name)]
            private static extern IntPtr SetSceneSize(int width, int height);
            public static IntPtr SetSceneSizeLock(int width, int height)
            {
                IntPtr p = IntPtr.Zero;
                lock (cppLock) p = SetSceneSize(width, height);
                return p;
                //CancellationTokenSource cts = new CancellationTokenSource();
                //IntPtr r = IntPtr.Zero;
                //bool done = false;
                //Task t = new Task(() =>
                //{
                //    cts.Token.ThrowIfCancellationRequested();
                //    r = SetSceneSize(width, height);
                //    done = true;
                //}, cts.Token);
                //Task.Run(() =>
                //{
                //    Thread.Sleep(1000);
                //    if (!done) cts.Cancel();
                //});
                //try
                //{
                //    t.RunSynchronously();
                //}
                //catch (OperationCanceledException e)
                //{
                //    return IntPtr.Zero;
                //}
                //return r;
            }
            [DllImport(name)]
            public static extern void SetBackgroundColor(byte r, byte g, byte b);
        }


        public static class Files
        {
            // pal
            [DllImport(name)]
            public static extern int CreatePaletteFromFileInBuffer(IntPtr pBuffer);
            [DllImport(name)]
            public static extern void RemovePalette(int id);

            // vxl
            [DllImport(name)]
            public static extern int CreateVxlFileFromFileInMemory(IntPtr pVxl, uint szVxl, IntPtr pHva, uint szHva);
            [DllImport(name)]
            public static extern bool RemoveVxlFile(int id);

            // shp
            [DllImport(name)]
            public static extern int CreateShpFileFromFileInMemory(IntPtr pShp, uint szShp);
            [DllImport(name)]
            public static extern bool RemoveShpFile(int id);
            [DllImport(name)]
            public static extern bool LoadShpTextures(int id, int frame);
            [DllImport(name)]
            public static extern bool IsShpFrameLoadedAsTexture(int id, int frame);

            // tmp
            [DllImport(name)]
            public static extern int CreateTmpFileFromFileInMemory(IntPtr pTmp, uint szTmp);
            [DllImport(name)]
            public static extern bool RemoveTmpFile(int id);
            [DllImport(name)]
            public static extern bool LoadTmpTextures(int tmpId);


            // common
            [DllImport(name)]
            public static extern int CreateCommonARGB32TextureFromColorBuffer(IntPtr pFile, uint width, uint height);
        }


        public static class ObjectUtils
        {
            [DllImport(name)]
            public static extern int CreateVxlObjectAtScene(int idVxl, Vec3 pos, float rotateX, float rotateY, float rotateZ, int idPal, uint dwRemapColor, int ofset);
            [DllImport(name)]
            public static extern bool CreateVxlObjectCached(int idVxl, Vec3 pos, Vec3 shadowPos, float rotateZ, int idPal, uint dwRemapColor, int offset, ref int outId, ref int outShadowId);
            [DllImport(name)]
            public static extern bool CreateTmpObjectAtScene(int idTmp, Vec3 pos, int pPal, int tileIndex, ref int outTileIndex, ref int outExIndex);
            [DllImport(name)]
            public static extern int CreateShpObjectAtScene(int idShp, Vec3 pos, int idFrame, int idPal, uint dwRemapColor, int flatType, int foundationX, int foundationY, int height, byte shaderType);
            [DllImport(name)]
            public static extern int CreateRectangleObjectAtScene(Vec3 pos, float width, float height, uint color);
            [DllImport(name)]
            public static extern int CreateLineObjectAtScene(Vec3 start, Vec3 end, uint startColor, uint endColor);
            [DllImport(name)]
            public static extern int CreateStringObjectAtScene(Vec3 pos, uint color, string content);
            [DllImport(name)]
            public static extern void RemoveTmpFromScene(int id);
            [DllImport(name)]
            public static extern void RemoveVxlFromScene(int id);
            [DllImport(name)]
            public static extern void RemoveShpFromScene(int id);
            [DllImport(name)]
            public static extern void RemoveCommonFromScene(int id);
            [DllImport(name)]
            public static extern void RemoveCommonTextureFromScene(int id);
            [DllImport(name)]
            public static extern void RotateObject(int id, float roX, float roY, float roZ);
            [DllImport(name)]
            public static extern void MoveObject(int id, Vec3 Displacement);
            [DllImport(name)]
            public static extern void SetObjectLocation(int id, Vec3 Pos);
            [DllImport(name)]
            public static extern void SetObjectColorCoefficient(int id, Vec4 color);
            [DllImport(name)]
            public static extern void SetObjectZAdjust(int id, float zAdjust);
        }
    }
}
