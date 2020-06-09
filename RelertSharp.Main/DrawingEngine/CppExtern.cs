using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using RelertSharp.Common;

namespace RelertSharp.DrawingEngine
{
    internal static class CppExtern
    {
        public static class Scene
        {
            [DllImport("\\bin\\CncVxlRenderText.dll")]
            public static extern bool SetSceneFont(string fontName, int size);
            [DllImport("\\bin\\CncVxlRenderText.dll")]
            public static extern void ScenePositionToClientPosition(Vec3 pos, ref Pnt result);
            [DllImport("\\bin\\CncVxlRenderText.dll")]
            public static extern void ClientPositionToScenePosition(Pnt pos, ref Vec3 result);


            [DllImport("\\bin\\CncVxlRenderText.dll")]
            public static extern void MoveFocusOnScene(Vec3 Displacement);
            [DllImport("\\bin\\CncVxlRenderText.dll")]
            public static extern void SetFocusOnScene(Vec3 pos);
            [DllImport("\\bin\\CncVxlRenderText.dll")]
            public static extern void MoveFocusOnScreen(float x, float y);


            [DllImport("\\bin\\CncVxlRenderText.dll")]
            public static extern bool ResetSceneView();
            [DllImport("\\bin\\CncVxlRenderText.dll")]
            public static extern void PresentAllObject();
            [DllImport("\\bin\\CncVxlRenderText.dll")]
            public static extern void ClearSceneObjects();
            [DllImport("\\bin\\CncVxlRenderText.dll")]
            public static extern bool SetUpScene(IntPtr hwnd);
            [DllImport("\\bin\\CncVxlRenderText.dll")]
            public static extern void SetBackgroundColor(byte r, byte g, byte b);
        }


        public static class Files
        {
            // pal
            [DllImport("\\bin\\CncVxlRenderText.dll")]
            public static extern int CreatePaletteFromFileInBuffer(IntPtr pBuffer);
            [DllImport("\\bin\\CncVxlRenderText.dll")]
            public static extern void RemovePalette(int id);

            // vxl
            [DllImport("\\bin\\CncVxlRenderText.dll")]
            public static extern int CreateVxlFileFromFileInMemory(IntPtr pVxl, uint szVxl, IntPtr pHva, uint szHva);
            [DllImport("\\bin\\CncVxlRenderText.dll")]
            public static extern bool RemoveVxlFile(int id);

            // shp
            [DllImport("\\bin\\CncVxlRenderText.dll")]
            public static extern int CreateShpFileFromFileInMemory(IntPtr pShp, uint szShp);
            [DllImport("\\bin\\CncVxlRenderText.dll")]
            public static extern bool RemoveShpFile(int id);
            [DllImport("\\bin\\CncVxlRenderText.dll")]
            public static extern bool LoadShpTextures(int id, int frame);
            [DllImport("\\bin\\CncVxlRenderText.dll")]
            public static extern bool IsShpFrameLoadedAsTexture(int id, int frame);

            // tmp
            [DllImport("\\bin\\CncVxlRenderText.dll")]
            public static extern int CreateTmpFileFromFileInMemory(IntPtr pTmp, uint szTmp);
            [DllImport("\\bin\\CncVxlRenderText.dll")]
            public static extern bool RemoveTmpFile(int id);
            [DllImport("\\bin\\CncVxlRenderText.dll")]
            public static extern bool LoadTmpTextures(int tmpId);
        }


        public static class ObjectUtils
        {
            [DllImport("\\bin\\CncVxlRenderText.dll")]
            public static extern int CreateVxlObjectAtScene(int idVxl, Vec3 pos, float rotateX, float rotateY, float rotateZ, int idPal, uint dwRemapColor);
            [DllImport("\\bin\\CncVxlRenderText.dll")]
            public static extern bool CreateTmpObjectAtScene(int idTmp, Vec3 pos, int pPal, int tileIndex, ref int outTileIndex, ref int outExIndex);
            [DllImport("\\bin\\CncVxlRenderText.dll")]
            public static extern int CreateShpObjectAtScene(int idShp, Vec3 pos, int idFrame, int idPal, uint dwRemapColor, int flatType, int foundationX, int foundationY, int height, bool isShadow);
            [DllImport("\\bin\\CncVxlRenderText.dll")]
            public static extern int CreateRectangleObjectAtScene(Vec3 pos, float width, float height, uint color);
            [DllImport("\\bin\\CncVxlRenderText.dll")]
            public static extern int CreateLineObjectAtScene(Vec3 start, Vec3 end, uint startColor, uint endColor);
            [DllImport("\\bin\\CncVxlRenderText.dll")]
            public static extern int CreateStringObjectAtScene(Vec3 pos, uint color, string content);
            [DllImport("\\bin\\CncVxlRenderText.dll")]
            public static extern void RemoveObjectFromScene(int id);
            [DllImport("\\bin\\CncVxlRenderText.dll")]
            public static extern void RotateObject(int id, float roX, float roY, float roZ);
            [DllImport("\\bin\\CncVxlRenderText.dll")]
            public static extern void MoveObject(int id, Vec3 Displacement);
            [DllImport("\\bin\\CncVxlRenderText.dll")]
            public static extern void SetObjectLocation(int id, Vec3 Pos);
            [DllImport("\\bin\\CncVxlRenderText.dll")]
            public static extern void SetObjectColorCoefficient(int id, Vec4 color);
        }
    }
}
