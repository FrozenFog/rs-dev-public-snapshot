using RelertSharp.Wpf.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RelertSharp.Wpf.Common
{
    internal static class GuiConst
    {
        public const double PARAM_ROW_HEIGHT = 23d;
        public const double PARAM_ROW_DELTA = 28d;
    }
    public enum GuiViewSide
    {
        Top,
        Bottom,
        Left,
        Right,
        Center
    }
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
        HousePanel
    }
    public class RsViewComponentAttribute : Attribute
    {
        public RsViewComponentAttribute(GuiViewType type, GuiViewSide side, string title)
        {
            ViewType = type;
            Title = title;
            Side = side;
        }
        public GuiViewSide Side { get; set; }
        public GuiViewType ViewType { get; set; }
        public string Title { get; set; }


        public static string GetViewName(object view)
        {
            IRsView rsView = view as IRsView;
            return rsView.ViewType.ToString();
        }
        internal static string GetViewTitle(IRsView view)
        {
            FieldInfo[] fields = typeof(MainWindow).GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (FieldInfo info in fields)
            {
                RsViewComponentAttribute attr = info.GetCustomAttribute<RsViewComponentAttribute>();
                if (attr != null && attr.ViewType == view.ViewType)
                {
                    return attr.Title;
                }
            }
            return string.Empty;
        }
    }
}
