using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.IniSystem;
using RelertSharp.MapStructure.Logic;
using RelertSharp.Common;
using System.Collections;
using System.Collections.ObjectModel;
using RelertSharp.Common.Config.Model;
using static RelertSharp.Common.Constant;

namespace RelertSharp.Wpf.ViewModel
{
    internal class ScriptVm : BaseVm<TeamScriptGroup>
    {
        public ScriptVm()
        {
            data = new TeamScriptGroup();
        }
        public ScriptVm(TeamScriptGroup obj) : base(obj)
        {

        }


        #region Curd
        public void AddItemAt(int pos)
        {
            data.AddItemAt(pos);
            SetProperty(nameof(Items));
        }
        public void RemoveItemAt(int pos)
        {
            data.RemoveItemAt(pos);
            SetProperty(nameof(Items));
        }
        public void MoveItemTo(int from, int to)
        {
            data.MoveItemTo(from, to);
            SetProperty(nameof(Items));
        }
        public void CopyItem(int sourceIndex)
        {
            data.CopyItemAt(sourceIndex);
            SetProperty(nameof(Items));
        }
        public void RemoveAllItem()
        {
            data.RemoveAll();
            SetProperty(nameof(Items));
        }
        #endregion


        #region Bind Call
        private ScriptItemVm selectedItem;
        public ScriptItemVm SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                SetProperty();
            }
        }
        public string Name
        {
            get { return data.Name; }
            set
            {
                data.Name = value;
                data.OnNameUpdated();
                SetProperty();
            }
        }
        public ScriptGroupVm Items
        {
            get { return new ScriptGroupVm(data.Data); }
        }
        public int Count { get { return data.Data.Count; } }
        #endregion
    }


    internal class ScriptGroupVm : BaseNotifyCollectionVm<TeamScriptGroup>, IEnumerable
    {
        private List<ScriptItemVm> data;
        public ScriptGroupVm()
        {
            data = new List<ScriptItemVm>();
        }
        public ScriptGroupVm(IEnumerable<TeamScriptItem> items)
        {
            data = new List<ScriptItemVm>();
            items.Foreach(x =>
            {
                data.Add(new ScriptItemVm(x));
            });
        }
        public IEnumerator GetEnumerator()
        {
            return data.GetEnumerator();
        }
        public List<ScriptItemVm> Items { get { return data; } }
    }


    internal class ScriptItemVm : BaseVm<TeamScriptItem>
    {
        public ScriptItemVm()
        {
            data = new TeamScriptItem(0, "0");
            data.InfoUpdated += UpdateTitle;
        }

        public ScriptItemVm(TeamScriptItem item) : base(item)
        {
            data.InfoUpdated += UpdateTitle;
        }

        private void UpdateTitle(object sender, EventArgs e)
        {
            SetProperty(nameof(Title));
            SetProperty(nameof(DetailInformation));
        }


        #region Param IO
        public string GetParameter(LogicInfoParameter param)
        {
            return data.GetParameter(param);
        }
        public void SetParameter(LogicInfoParameter param, bool value)
        {
            data.SetParameter(param, value);
        }
        public void SetParameter(LogicInfoParameter param, string value)
        {
            if (value.IsNullOrEmpty()) value = Config.PARAM_DEF;
            data.SetParameter(param, value);
        }
        public void SetParameter(LogicInfoParameter param, IIndexableItem value)
        {
            if (value != null) data.SetParameter(param, value.Id);
            else data.SetParameter(param, Config.PARAM_DEF);
        }
        #endregion


        #region Bind Call
        public string Title
        {
            get { return data.ToString(); }
        }
        public string DetailInformation
        {
            get { return data.Info.Description; }
        }
        public int ScriptIndex
        {
            get { return data.ScriptActionIndex; }
            set
            {
                data.SetScriptTypeTo(value);
                SetProperty();
            }
        }
        public LogicInfo Info
        {
            get { return data.Info; }
        }
        #endregion
    }
}
