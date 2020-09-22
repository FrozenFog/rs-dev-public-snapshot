using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using RelertSharp.FileSystem;
using RelertSharp.MapStructure;

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
                for (int i = 0; i < args.Length; i++)
                {
                    string a = args[i];
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
                    else if (a == "/trimOverlay")
                    {
                        string filename = args[++i];
                        MapFile m = new MapFile(filename);
                        int maxNum = int.Parse(args[++i]);
                        int max = m.Map.Overlays.Max(x => x.Index);
                        List<OverlayUnit> toRemove = new List<OverlayUnit>();
                        foreach (OverlayUnit o in m.Map.Overlays)
                        {
                            if (o.Index >= maxNum) toRemove.Add(o);
                            if (m.Map.IsOutOfSize(o, null)) toRemove.Add(o);
                        }
                        foreach (OverlayUnit o in toRemove) m.Map.RemoveOverlay(o);
                        m.SaveMapAs(Application.StartupPath, filename);
                    }
                }
            }
        }
    }
}
