using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace RelertSharp.Wpf.Common
{
    internal class DelayedAction
    {
        private DispatcherTimer tmr;
        public DelayedAction(Action beforeInvoke, Action invoke, int intervalMs) 
        {
            BeforeInvoke = beforeInvoke;
            InvokeAction = invoke;
            Interval = intervalMs;
            tmr = new DispatcherTimer()
            {
                Interval = new TimeSpan(0, 0, 0, 0, intervalMs)
            };
            tmr.Tick += Tick;
        }

        private void Tick(object sender, EventArgs e)
        {
            tmr.Stop();
            InvokeAction?.Invoke();
        }

        #region Api
        public void Restart()
        {
            tmr.Stop();
            BeforeInvoke?.Invoke();
            tmr.Start();
        }
        public void Stop()
        {
            tmr.Stop();
        }
        #endregion



        public Action BeforeInvoke { get; private set; }
        public Action InvokeAction { get; private set; }
        public int Interval { get; private set; }
    }
}
