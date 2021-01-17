using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.MapStructure;
using RelertSharp.MapStructure.Logic;

namespace RelertSharp.Wpf.ViewModel
{
    internal static class GlobalCollectionVm
    {
        public static GlobalTeamVm Teams { get; set; } = new GlobalTeamVm();
        public static void UpdateTeamsAll()
        {
            Teams.UpdateAll();
        }
        public static GlobalScriptVm Scripts { get; set; } = new GlobalScriptVm();
        public static GlobalTaskforceVm Taskforces { get; set; } = new GlobalTaskforceVm();
        public static GlobalAiTriggerVm AiTriggers { get; set; } = new GlobalAiTriggerVm();
    }
}
