using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace RelertSharp.DrawingEngine
{
    internal static class CppExtern
    {
        public static class Scene
        {
            [DllImport("CncVxlRenderText.dll")]
            public static extern void ScenePositionToClientPosition(Vec3 pos, ref Pnt result);
            [DllImport("CncVxlRenderText.dll")]
            public static extern void ClientPositionToClientPosition(Pnt pos, ref Vec3 result);


            [DllImport("CncVxlRenderText.dll")]
            public static extern void MoveFocusOnScene(Vec3 Displacement);
            [DllImport("CncVxlRenderText.dll")]
            public static extern void SetFocusOnScreen(Vec3 pos);
            [DllImport("CncVxlRenderText.dll")]
            public static extern void MoveFocusOnScreen(float x, float y);


            [DllImport("CncVxlRenderText.dll")]
            public static extern bool ResetSceneView();
            [DllImport("CncVxlRenderText.dll")]
            public static extern void PresentAllObject();
            [DllImport("CncVxlRenderText.dll")]
            public static extern void ClearSceneObjects();
            [DllImport("CncVxlRenderText.dll")]
            public static extern bool SetUpScene(IntPtr hwnd);
            [DllImport("CncVxlRenderText.dll")]
            public static extern void SetBackgroundColor(byte r, byte g, byte b);
        }


        public static class Files
        {
            // pal
            [DllImport("CncVxlRenderText.dll")]
            public static extern int CreatePaletteFromFileInBuffer(IntPtr pBuffer);
            [DllImport("CncVxlRenderText.dll")]
            public static extern void RemovePalette(int id);

            // vxl
            [DllImport("CncVxlRenderText.dll")]
            public static extern int CreateTmpFileFromFileInMemory(IntPtr pVxl, uint szVxl, IntPtr pHva, uint szHva);
            [DllImport("CncVxlRenderText.dll")]
            public static extern bool RemoveVxlFile(int id);

            // shp
            [DllImport("CncVxlRenderText.dll")]
            public static extern int CreateShpFileFromFileInMemory(IntPtr pShp, uint szShp);
            [DllImport("CncVxlRenderText.dll")]
            public static extern bool RemoveShpFile(int id);
            [DllImport("CncVxlRenderText.dll")]
            public static extern bool LoadShpTextures(int id, int palId, uint dwRemapColor);

            // tmp
            [DllImport("CncVxlRenderText.dll")]
            public static extern int CreateTmpFileFromFilenMemory(IntPtr pTmp, uint szTmp);
            [DllImport("CncVxlRenderText.dll")]
            public static extern bool RemoveTmpFile(int id);
            [DllImport("CncVxlRenderText.dll")]
            public static extern bool LoadTmpTextures(int tmpId, int palId);
        }


        public static class ObjectUtils
        {
            [DllImport("CncVxlRenderText.dll")]
            public static extern int CreateVxlObjectAtScene(int idVxl, Vec3 pos, float rotateX, float rotateY, float rotateZ, int idPal, uint dwRemapColor);
            [DllImport("CncVxlRenderText.dll")]
            public static extern bool CreateTmpObjectAtScene(int idTmp, Vec3 pos, int tileIndex, ref int outTileIndex, ref int outExIndex);
            [DllImport("CncVxlRenderText.dll")]
            public static extern int CreateShpObjectAtScene(int idShp, Vec3 pos, int idFrame, int idPal, uint dwRemapColor, bool isFlat);
            [DllImport("CncVxlRenderText.dll")]
            public static extern void RemoveObjectAtScene(int id);
            [DllImport("CncVxlRenderText.dll")]
            public static extern void RotateObject(int id, float roX, float roY, float roZ);
            [DllImport("CncVxlRenderText.dll")]
            public static extern void MoveObject(int id, Vec3 Displacement);
            [DllImport("CncVxlRenderText.dll")]
            public static extern void SetObjectLocation(int id, Vec3 Pos);
        }
    }
}
