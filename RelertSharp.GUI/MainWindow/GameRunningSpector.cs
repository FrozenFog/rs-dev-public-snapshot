using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.ComponentModel;
using static RelertSharp.Common.GlobalVar;
using static RelertSharp.GUI.GuiUtils;

namespace RelertSharp.GUI
{
    public partial class MainWindowTest
    {
        private string gameProcessName;
        private bool replaceCncnet5, useRsCncnet5;
        private void RunGame(string gameexeName, string injectorName, bool reserveCncnet5)
        {
            Process[] ps = Process.GetProcesses();
            gameProcessName = gameexeName.Substring(0, gameexeName.Length - 4);
            if (ps.Where(x => x.ProcessName.ToLower() == gameProcessName).Count() == 0)
            {
                ProcessStartInfo info = new ProcessStartInfo(injectorName, string.Format("\"{0}\" -SPAWN", gameexeName));
                info.WorkingDirectory = GlobalConfig.GamePath;
                info.UseShellExecute = false;
#if RELEASE
                try
                {
#endif
                    if (reserveCncnet5) EnsureComponents();
                    Process.Start(info);
                    WindowState = FormWindowState.Minimized;
                    bgwMonitor.RunWorkerAsync();
#if RELEASE
            }
                catch (Exception e)
                {
                    Fatal("Failed to run game!\nTrace:" + e.StackTrace);
                }
#endif
            }
            else
            {
                Warning("Game is currently running!");
            }
        }
        private void EnsureComponents()
        {
            useRsCncnet5 = true;
            string pathCncnet5 = GlobalConfig.GamePath + "cncnet5.dll";
            string pathReserve = Application.StartupPath + "\\reserve.dll";
            if (File.Exists(pathCncnet5))
            {
                if (File.Exists(pathReserve)) File.Delete(pathReserve);
                File.Copy(pathCncnet5, pathReserve);
                File.Delete(pathCncnet5);
                replaceCncnet5 = true;
            }
            File.Copy(Application.StartupPath + "\\cncnet5.dll", pathCncnet5);
        }
        private void bgwMonitor_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(5000);
            if (Process.GetProcesses().Where(x=>x.ProcessName.ToLower() == gameProcessName).Count() == 0)
            {
                Fatal("Game initialize may failed!\nPlease check your game path and locate cncnet5.dll");
            }
            while (true)
            {
                Process[] ps = Process.GetProcesses();
                if (ps.Where(x => x.ProcessName.ToLower() == gameProcessName).Count() == 0) break;
                Thread.Sleep(2000);
            }
            (sender as BackgroundWorker).ReportProgress(1);
        }
        private void bgwMonitor_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == 1)
            {
                WindowState = FormWindowState.Normal;
                if (drew) Engine.Refresh();
                if (useRsCncnet5)
                {
                    string pathCncnet5 = GlobalConfig.GamePath + "cncnet5.dll";
                    string pathReserve = Application.StartupPath + "\\reserve.dll";
                    File.Delete(pathCncnet5);
                    if (replaceCncnet5)
                    {
                        File.Copy(pathReserve, pathCncnet5);
                        File.Delete(pathReserve);
                        replaceCncnet5 = false;
                    }
                }
            }
        }
    }
}
