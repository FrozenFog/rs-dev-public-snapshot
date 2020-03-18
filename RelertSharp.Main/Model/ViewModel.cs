using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.MapStructure;
using RelertSharp.MapStructure.Logic;

namespace RelertSharp.Model
{
    public sealed class MapViewModel : BindableBase
    {
        private Map map;
        private TriggerItem trigger;
        private TriggerItem[] triggers;
        private LogicItem @event, @action;
        private TagItem tag;
        private int i = 0;


        #region Ctor - MapViewModel
        public MapViewModel(Map m)
        {
            map = m;
            triggers = map.Triggers.ToArray();
            trigger = triggers[i];
        }
        #endregion


        #region Public Methods - MapViewModel
        public void NextTrigger()
        {
            trigger = triggers[i++];
        }
        #endregion


        #region Public Calls - MapViewModel
        public TriggerItem Trigger
        {
            get { return trigger; }
            set { SetProperty(ref trigger, value); }
        }
        public LogicItem Event
        {
            get { return @event; }
            set { SetProperty(ref @event, value); }
        }
        public LogicItem Action
        {
            get { return action; }
            set { SetProperty(ref action, value); }
        }
        public TagItem Tag
        {
            get { return tag; }
            set { SetProperty(ref tag, value); }
        }
        #endregion
    }
}
