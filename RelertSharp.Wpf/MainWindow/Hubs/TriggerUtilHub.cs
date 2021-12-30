using RelertSharp.MapStructure.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelertSharp.Wpf
{
    internal static class TriggerUtilHub
    {
        public static event IndexableHandler NewTriggerPushed;
        public static void PushNewTriggerToList(TriggerItem src)
        {
            NewTriggerPushed?.Invoke(src);
        }
    }
}
