using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.MapStructure;
using RelertSharp.MapStructure.Logic;
using RelertSharp.Common;

namespace RelertSharp.Wpf.ViewModel
{
    internal static class GlobalCollectionVm
    {
        static GlobalCollectionVm()
        {
            GlobalVar.MapDocumentLoaded += RefershCollections;
        }

        private static void RefershCollections(object sender, EventArgs e)
        {
            Teams = new GlobalTeamVm();
            Scripts = new GlobalScriptVm();
            Taskforces = new GlobalTaskforceVm();
            AiTriggers = new GlobalAiTriggerVm();
            StaticCollections = new StaticCollectionVm();
            Countries = new GlobalCountryVm();
            Houses = new GlobalHouseVm();
            Triggers = new GlobalTriggerVm();
            Tags = new GlobalTagVm();
        }

        public static GlobalTeamVm Teams { get; set; } = new GlobalTeamVm();
        public static GlobalScriptVm Scripts { get; set; } = new GlobalScriptVm();
        public static GlobalTaskforceVm Taskforces { get; set; } = new GlobalTaskforceVm();
        public static GlobalAiTriggerVm AiTriggers { get; set; } = new GlobalAiTriggerVm();
        public static StaticCollectionVm StaticCollections { get; set; } = new StaticCollectionVm();
        public static GlobalCountryVm Countries { get; set; } = new GlobalCountryVm();
        public static GlobalHouseVm Houses { get; set; } = new GlobalHouseVm();
        public static GlobalTriggerVm Triggers { get; set; } = new GlobalTriggerVm();
        public static GlobalTagVm Tags { get; set; } = new GlobalTagVm();
    }
}
