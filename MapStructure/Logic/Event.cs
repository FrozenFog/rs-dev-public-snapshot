using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using relert_sharp.FileSystem;
using relert_sharp.Common;

namespace relert_sharp.MapStructure.Logic
{
    public class EventCollection : LogicCollection
    {
        public EventCollection(INIEntity entEvent) : base(entEvent, LogicType.EventLogic)
        {

        }
    }
    public class EventGroup : LogicGroup
    {
        public EventGroup(string _id, int _num, string[] _paramData) : base(_id, _num, _paramData, LogicType.EventLogic)
        {

        }
    }
    public class EventItem : LogicItem
    {
        public EventItem(int _id, string[] _param, string _comment = "") : base(_id, _param, LogicType.EventLogic, _comment)
        {

        }
    }
}
