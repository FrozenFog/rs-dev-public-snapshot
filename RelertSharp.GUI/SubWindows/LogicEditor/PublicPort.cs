using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using RelertSharp.MapStructure.Logic;
using RelertSharp.Common;
using static RelertSharp.GUI.GuiUtils;

namespace RelertSharp.GUI.SubWindows.LogicEditor
{
    internal partial class LogicEditor
    {
        public void TraceTag(TagItem tag)
        {
            TriggerItem trigger = lbxTriggerList.Items.Cast<TriggerItem>().Where(x => x.ID == tag.AssoTrigger).First();
            if (trigger != null)
            {
                tbcMain.SelectedIndex = 0;
                lbxTriggerList.SelectedItem = trigger;
            }
        }
    }
}
