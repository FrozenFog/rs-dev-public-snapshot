using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.MapStructure.Logic;
using RelertSharp.IniSystem;
using RelertSharp.Common;

namespace RelertSharp.GUI
{
    internal delegate void TriggerUpdateHandler(object sender, TriggerItem trigger);
    internal delegate void TemplateStatChangedHandler(object sender, bool isTemplate);
    internal delegate void LogicItemUpdateHandler(object sender, LogicItem item);
    internal delegate void SoundPlayingHandler(object sender, TriggerParam param, TechnoPair p);
    internal delegate void TaskforceHandler(object sender, TaskforceItem taskforce);
    internal delegate void I2dLocateableHandler(object sender, I2dLocateable pos);
}
