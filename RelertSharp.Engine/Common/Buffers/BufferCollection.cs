using System;
using System.Collections.Generic;

namespace RelertSharp.Engine
{
    internal partial class BufferCollection
    {
        #region Ctor - BufferCollection
        public BufferCollection()
        {

        }
        #endregion


        #region Public Methods - BufferCollection
        public void ReleaseFiles()
        {
            Files.Dispose();
        }
        #endregion


        #region Public Calls - BufferCollection
        public CScene Scenes { get; private set; } = new CScene();
        public CBuffer Buffers { get; private set; } = new CBuffer();
        public CFile Files { get; private set; } = new CFile();
        public List<int> WaypointNum { get; private set; } = new List<int>();
        #endregion

    }
}
