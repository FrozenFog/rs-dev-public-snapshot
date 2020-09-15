using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace RelertSharp
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                foreach (string a in args)
                {
                    if (a == "/reboot")
                    {
                        while (true)
                        {
                            Process[] ps = Process.GetProcesses();
                            if (ps.Count(x => x.ProcessName == "RelertSharp") == 0)
                            {
                                Process.Start("RelertSharp.exe");
                                return;
                            }
                            else
                            {
                                Thread.Sleep(200);
                            }
                        }
                    }
                }
            }
        }
    }
}
