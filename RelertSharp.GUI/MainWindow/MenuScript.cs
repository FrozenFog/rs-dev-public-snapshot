using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using RelertSharp.FileSystem;
using RelertSharp.IniSystem;
using RelertSharp.Common;
using RelertSharp.MapStructure;
using static RelertSharp.Common.GlobalVar;
using static RelertSharp.GUI.GuiUtils;

namespace RelertSharp.GUI
{
    public partial class MainWindowTest
    {
        private async void tsmiMainSaveMapAs_Click(object sender, EventArgs e)
        {
            verifyForm.CheckMap();
            verifyForm.Show();
            requireFocus = false;
            await Task.Run(() =>
            {
                while (verifyForm.Wait) ;
            });
            if (verifyForm.DialogResult == DialogResult.OK)
            {
                SaveFileDialog dlg = new SaveFileDialog()
                {
                    InitialDirectory = GlobalConfig.LastPath,
                    Filter = "Red Alert 2 Mission Map|*.map|Red Alert 2 YR Map File|*.yrm|Red Alert 2 General Map|*.mpr",
                    AddExtension = true,
                    Title = "Save",
                    FileName = CurrentMapDocument.FileName
                };
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    string path = dlg.FileName.Substring(0, dlg.FileName.LastIndexOf('\\') + 1);
                    CurrentMapDocument.SaveMapAs(path, dlg.FileName.Split('\\').Last());
                    Complete("Saving Complete!");
                    saved = true;
                    logicEditor.ChangeSaved = true;
                }
            }
        }
        private void tsmiMainRunMap_Click(object sender, EventArgs e)
        {
            RunSettingForm setting = new RunSettingForm();
            setting.ReadSettingFromLocal();
            if (setting.ShowDialog() == DialogResult.OK)
            {
                INIFile spawn = setting.GetSpawn();
                spawn.SaveIni(GlobalConfig.GamePath + "spawn.ini");
                CurrentMapDocument.SaveMapAs(GlobalConfig.GamePath, "spawnmap.ini");
                string injectorName = setting.UseSyringe ? "syringe.exe" : "CncnetYRInject.exe";
                RunGame(setting.GameName, injectorName, !setting.UseSyringe);
            }
            setting.SaveSettingToLocal();
        }
        private void tsmiMainDevMode_Click(object sender, EventArgs e)
        {
            GlobalConfig.Local.DevMode = tsmiMainDevMode.Checked;
            if (YesNoWarning("Dev mode changed! Program must restart to reload virtual directory.\nRestart Now?") == DialogResult.Yes)
            {
                Process.Start("rsdata.exe", "/reboot");
                Close();
            }
        }
        private void tsmiCheckMap_Click(object sender, EventArgs e)
        {
            verifyForm.CheckMap();
            verifyForm.Show();
            requireFocus = false;
        }
        private void tsmiOpenMap_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog()
            {
                Title = Language.DICT["OpenMapDlgTitle"],
                InitialDirectory = string.IsNullOrEmpty(GlobalConfig.Local.RecentPath) ? Application.StartupPath : GlobalConfig.Local.RecentPath,
                Filter = "Red Alert 2 Map File|*.map;*.yrm;*.mpr",
                AddExtension = true,
                CheckFileExists = true,
            };
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                drew = false;
                pnlPick.DisableDrawing();
                Engine?.Dispose();
                string mapname = dlg.FileName;
                GlobalConfig.Local.RecentPath = mapname.Substring(0, mapname.Length - mapname.LastIndexOf('\\'));
                MapFile map = new MapFile(mapname);
                GlobalRules.MapIniData = map.Map.IniResidue;
                CurrentMapDocument = map;
                CurrentMapDocument.Map.DumpStructure();
                if (Log.HasCritical)
                {
                    Log.Critical("These object will not show in scene(or in logic editor)");
                    Warning(Log.ShowCritical());
                }
                if (EngineInitialize(panel1.Handle, pnlMiniMap))
                {
                    bgwDraw.RunWorkerAsync();
                }
                else
                {
                    Fatal("Engine Initialize Failed!!");
                }
            }
        }
    }
}
