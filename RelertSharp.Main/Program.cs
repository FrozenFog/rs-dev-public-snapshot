using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using RelertSharp.CliFunction;
using RelertSharp.FileSystem;
using RelertSharp.MapStructure;

namespace RelertSharp
{
    static class Program
    {
        private static string StartupPath { get { return Application.StartupPath + "\\"; } }
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                BeginArgReading(args);
                while (!ArgEnd())
                {
                    string a = GetHeadArg();
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
                        string filename = GetNextArg();
                        MapFile m = new MapFile(filename);
                        int maxNum = int.Parse(GetNextArg());
                        int max = m.Map.Overlays.Max(x => x.Index);
                        List<OverlayUnit> toRemove = new List<OverlayUnit>();
                        foreach (OverlayUnit o in m.Map.Overlays)
                        {
                            if (o.Index >= maxNum) toRemove.Add(o);
                            if (m.Map.IsOutOfSize(o, null)) toRemove.Add(o);
                        }
                        foreach (OverlayUnit o in toRemove) m.Map.RemoveOverlay(o);
                        m.SaveMapAs(StartupPath, filename);
                    }
                    else if (a == "/locker")
                    {
                        string mode = GetNextArg();
                        string filename = GetNextArg();
                        string seed = GetNextArg();
                        MapFile m = new MapFile(filename);
                        MapLocker.RunLocker(m, mode, seed);
                        m.SaveMapAs(StartupPath, filename);
                    }
                }
            }
        }

        private static string[] pArgs;
        private static int iArg;
        static bool BeginArgReading(string[] args)
        {
            pArgs = args;
            iArg = 0;
            return true;
        }
        static string GetNextArg()
        {
            string s = pArgs[iArg];
            if (s.StartsWith("/")) return string.Empty;
            else return pArgs[iArg++];
        }
        static string GetHeadArg()
        {
            string s = pArgs[iArg];
            if (s.StartsWith("/")) return pArgs[iArg++];
            else return string.Empty;
        }
        static bool ArgEnd()
        {
            return iArg >= pArgs.Length;
        }
    }
}
