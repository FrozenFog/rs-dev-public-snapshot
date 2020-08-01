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
using static RelertSharp.Common.GlobalVar;
using static RelertSharp.GUI.GuiUtils;

namespace RelertSharp.GUI
{
    public partial class MainWindowTest
    {
        private void tsmiMainSaveMapAs_Click(object sender, EventArgs e)
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
                CurrentMapDocument.SaveMap(dlg.FileName);
                Complete("Saving Complete!");
                saved = true;
                logicEditor.ChangeSaved = true;
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
                CurrentMapDocument.SaveMapInfoInto(GlobalConfig.GamePath, "spawnmap.ini");
                string injectorName = setting.UseSyringe ? "syringe.exe" : "CncnetYRInject.exe";
                RunGame(setting.GameName, injectorName, !setting.UseSyringe);
            }
            setting.SaveSettingToLocal();
        }
    }
}
