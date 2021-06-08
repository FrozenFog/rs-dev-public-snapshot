using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RelertSharp.Wpf.Common
{
    public enum GuiViewType
    {
        Undefined = -1,
        Minimap,
        MainPanel,
        AiTrigger,
        AiTriggerList,
        LightningPanel,
        Script,
        ScriptList,
        Taskforce,
        TaskforceList,
        Team,
        TeamList,
        AnimationPreview,
        TriggerList,
        Trigger,
        Event, Action,
        ObjctPanel,
    }
    public class RsViewComponentAttribute : Attribute
    {
        public RsViewComponentAttribute(GuiViewType type, [CallerMemberName]string name = null)
        {
            ViewType = type;
            Name = name;
        }
        public GuiViewType ViewType { get; set; }
        public string Name { get; set; }
    }
}
