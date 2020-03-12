using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using relert_sharp.IniSystem;
using relert_sharp.Common;
using System.Collections;

namespace relert_sharp.MapStructure.Logic
{
    public class ActionCollection : LogicCollection
    {
        #region Ctor - ActionCollection
        public ActionCollection(INIEntity entAction) : base(entAction, LogicType.ActionLogic)
        {

        }
        #endregion
    }
    //public class ActionGroup : LogicGroup
    //{
    //    #region Ctor - ActionGroup
    //    public ActionGroup(string _id, int _num, string[] _paramData) : base(_id, _num, _paramData, LogicType.ActionLogic)
    //    {

    //    }
    //    public ActionGroup() { }
    //    #endregion
    //}
    //public class ActionItem : LogicItem
    //{
    //    #region Ctor - ActionItem
    //    public ActionItem(int _id, string[] _param, string _comment = "") : base(_id, _param, LogicType.ActionLogic, _comment)
    //    {

    //    }
    //    #endregion


    //    #region Public Methods - ActionItem

    //    #endregion
    //}
}
