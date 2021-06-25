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

namespace RelertSharp.Wpf.ViewModel
{
    internal class ScriptVm : BaseVm<TeamScriptGroup>
    {
        public ScriptVm()
        {
            data = new TeamScriptGroup();
        }
        public ScriptVm(object obj) : base(obj) { }

        public string Name
        {
            get { return data.Name; }
            set
            {
                data.Name = value;
                SetProperty();
            }
        }
        public ObservableCollection<ScriptItemVm> Items
        {
            get { return new ObservableCollection<ScriptItemVm>(data.Data.Cast<ScriptItemVm>()); }
        }
    }

    internal class ScriptItemVm : BaseNotifyCollectionVm<TeamScriptItem>, IEnumerable
    {
        private List<TeamScriptItem> data;
        public ScriptItemVm()
        {
            data = new List<TeamScriptItem>();
        }
        public ScriptItemVm(List<TeamScriptItem> items)
        {
            data = items;
        }
        public IEnumerator GetEnumerator()
        {
            return data.GetEnumerator();
        }
        public List<TeamScriptItem> Items { get { return data; } }
    }
}
