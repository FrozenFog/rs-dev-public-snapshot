using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


        public string GetName(int totalIndex)
        {
            int delta = totalIndex - Offset;
            return string.Format(FileName, delta + 1, data[delta].Suff);
        }
        public void AddTile(int varitycap)
        {
            TileVaries varies = new TileVaries()
            {
                VarityCap = varitycap
            };
            data.Add(varies);
        }


        public string SetName { get; set; }
        public int FrameworkSet { get; set; }
        public int OriginalSet { get; set; }
        public bool IsFramework { get; private set; }
        public bool AllowPlace { get; private set; }
        public string FileName { get; set; }
        /// <summary>
        /// Tile index from beginning
        /// </summary>
        public int Offset { get; set; }



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
