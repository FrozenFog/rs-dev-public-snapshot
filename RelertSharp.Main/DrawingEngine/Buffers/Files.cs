using System;
using System.Collections.Generic;

namespace RelertSharp.DrawingEngine
{
    internal partial class BufferCollection
    {

        public class CFile
        {
            #region Ctor - File
            public CFile() { }
            #endregion


            #region Public Methods - File
            public void Dispose()
            {
                foreach (int p in Shp.Values) CppExtern.Files.RemoveShpFile(p);
                foreach (int p in Vxl.Values) CppExtern.Files.RemoveVxlFile(p);
                foreach (int p in Tmp.Values) CppExtern.Files.RemoveTmpFile(p);
                foreach (int p in Pal.Values) CppExtern.Files.RemovePalette(p);
                Shp.Clear();
                Vxl.Clear();
                Tmp.Clear();
                Pal.Clear();
                GC.Collect();
            }
            #endregion


            #region Public Calls - File
            public Dictionary<string, int> Shp { get; private set; } = new Dictionary<string, int>();
            public Dictionary<string, int> Vxl { get; private set; } = new Dictionary<string, int>();
            public Dictionary<string, int> Tmp { get; private set; } = new Dictionary<string, int>();
            public Dictionary<string, int> Pal { get; private set; } = new Dictionary<string, int>();
            public int WaypointBase { get; set; }
            public int CelltagBase { get; set; }
            public int LightSourceBase { get; set; }
            #endregion
        }
    }
}
