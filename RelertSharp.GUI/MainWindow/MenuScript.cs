using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RelertSharp.Common;
using static RelertSharp.GUI.GuiUtils;

namespace RelertSharp.GUI
{
    public partial class MainWindowTest
    {
        private void tsmiMainSaveMapAs_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog()
            {
                InitialDirectory = GlobalVar.GlobalConfig.LastPath,
                Filter = "Red Alert 2 Mission Map|*.map|Red Alert 2 YR Map File|*.yrm|Red Alert 2 General Map|*.mpr",
                AddExtension = true,
                Title = "Save",
                FileName = GlobalVar.CurrentMapDocument.FileName
            };
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                GlobalVar.CurrentMapDocument.SaveMap(dlg.FileName);
                Complete("Saving Complete!");
            }
        }
    }
}
