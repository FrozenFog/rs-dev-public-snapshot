using System;
using System.Collections.Generic;
using System.Linq;

namespace RelertSharp.MapStructure
{
    public class TileSet
    {
        private List<TileVaries> data = new List<TileVaries>();


        public TileSet(bool isFramework, bool allowPlace, int framework)
        {
            IsFramework = isFramework;
            AllowPlace = allowPlace;
            FrameworkSet = framework;
        }
        public TileSet(TileSet src)
        {
            data = src.data;
            IsFramework = src.IsFramework;
            AllowPlace = src.AllowPlace;
            FrameworkSet = src.FrameworkSet;
            SetName = src.SetName;
            SetIndex = src.SetIndex;
            OriginalSet = src.OriginalSet;
            FileName = src.FileName;
            Offset = src.Offset;
        }


        #region Public Methods
        public string GetName(int totalIndex, bool fixOffset = true)
        {
            int delta = 0;
            if (fixOffset) delta = totalIndex - Offset;
            else delta = totalIndex;
            return string.Format(FileName, delta + 1, data[delta].Suff);
        }
        public string GetBaseHeightName(int height)
        {
            return string.Format(FileName, height, "");
        }
        public void SetMaxIndex(int maxCount)
        {
            data = data.Take(maxCount).ToList();
        }
        public void AddTile(int varitycap)
        {
            TileVaries varies = new TileVaries()
            {
                VarityCap = varitycap
            };
            data.Add(varies);
        }
        public List<string> GetNames()
        {
            List<string> result = new List<string>();
            for (int i = 0; i < data.Count; i++)
            {
                result.Add(GetName(i, false));
            }
            return result;
        }
        public bool ClassifyAs(params string[] guessTypes)
        {
            foreach (string guessType in guessTypes)
            {
                if (SetName.Contains(guessType) || SetName.Contains(guessType.ToLower())) return true;
            }
            return false;
        }
        public override string ToString()
        {
            return string.Format("{0} ({1:D4})", SetName, SetIndex);
        }
        #endregion


        #region Public Calls
        public int Count { get { return data.Count; } }
        public string SetName { get; set; }
        /// <summary>
        /// 0 if not specified
        /// </summary>
        public int FrameworkSet { get; set; }
        public int OriginalSet { get; set; }
        public bool IsFramework { get; private set; }
        public bool AllowPlace { get; private set; }
        public string FileName { get; set; }
        public int SetIndex { get; set; }
        /// <summary>
        /// Tile index from beginning
        /// </summary>
        public int Offset { get; set; }
        #endregion



        private class TileVaries
        {
            private Random r = new Random();


            public TileVaries() { }


            public string Suff
            {
                get
                {
                    if (VarityCap > 0)
                    {
                        int delta = r.Next(0, VarityCap);
                        if (delta > 0) return ((char)('a' + delta)).ToString();
                    }
                    return "";
                }
            }
            public int VarityCap { get; set; }

        }
    }
}
