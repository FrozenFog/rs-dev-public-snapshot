using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using relert_sharp.FileSystem;
using relert_sharp.Common;

namespace relert_sharp.MapStructure.Logic
{
    public class ActionCollection : LogicCollection
    {
        public ActionCollection(INIEntity entAction) : base(entAction, LogicType.ActionLogic)
        {

        }
    }
    public class ActionGroup : LogicGroup
    {
        public ActionGroup(string _id, int _num, string[] _paramData) : base(_id, _num, _paramData, LogicType.ActionLogic)
        {

        }
    }
    public class ActionItem : LogicItem
    {
        public ActionItem(int _id, string[] _param, string _comment = "") : base(_id, _param, LogicType.ActionLogic, _comment)
        {

        }
    }
}
