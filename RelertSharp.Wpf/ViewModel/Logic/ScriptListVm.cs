using RelertSharp.Common;
using RelertSharp.MapStructure.Logic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelertSharp.Wpf.ViewModel
{
    internal class ScriptListVm : BaseListVm<TeamScriptGroup>, IIndexableItem
    {
        #region Ctor
        public ScriptListVm()
        {
            data = new TeamScriptGroup();
        }
        public ScriptListVm(TeamScriptGroup src) : base(src)
        {
            src.NameUpdated += SetName;
        }
        #endregion


        #region Public
        public override void ChangeDisplay(IndexableDisplayType type)
        {
            data.ChangeDisplay(type);
            base.ChangeDisplay(type);
        }
        #endregion


        #region Bind Call
        public override string Title
        {
            get { return data.ToString(); }
        }
        #endregion

        public string Id { get => ((IIndexableItem)Data).Id; set => ((IIndexableItem)Data).Id = value; }
        public string Name { get => ((IIndexableItem)Data).Name; set => ((IIndexableItem)Data).Name = value; }

        public string Value => ((IIndexableItem)Data).Value;
    }
}
