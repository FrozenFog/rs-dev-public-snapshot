using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.IniSystem;
using RelertSharp.Common;

namespace RelertSharp.MapStructure.Logic
{
    public class EventCollection : LogicCollection
    {
        public EventCollection(INIEntity entEvent) : base(entEvent, LogicType.EventLogic)
        {

        }
    }


    //public class EventGroup : LogicGroup
    //{
    //    #region Ctor - EventGroup
    //    public EventGroup(string _id, int _num, string[] _paramData) : base(_id, _num, _paramData, LogicType.EventLogic)
    //    {

    //    }
    //    public EventGroup() { }
    //    #endregion
    //}


    //public class EventItem : LogicItem
    //{
    //    #region Ctor - EventItem
    //    public EventItem(int _id, string[] _param, string _comment = "") : base(_id, _param, LogicType.EventLogic, _comment)
    //    {

    //    }
    //    #endregion


    //    #region Public Methods - EventItem
    //    public override string ToString()
    //    {
    //        if (string.IsNullOrEmpty(Comment)) return string.Format("Event{0:D2}", ID);
    //        else return Comment;
    //    }
    //    #endregion
    //}
}
