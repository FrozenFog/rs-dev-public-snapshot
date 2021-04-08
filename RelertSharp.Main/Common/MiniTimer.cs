using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelertSharp.Common
{
    public class MiniTimer
    {
        private Stopwatch watch = new Stopwatch();
        private long avg;


        public MiniTimer()
        {

        }

        public void Start()
        {
            watch.Restart();
        }
        public void Stop()
        {
            watch.Stop();
            if (avg == 0) avg = Elapsed();
            else avg = (avg + Elapsed()) / 2;
        }
        public long Elapsed()
        {
            return watch.ElapsedMilliseconds;
        }
        public long Average { get { return avg; } }
    }
}
