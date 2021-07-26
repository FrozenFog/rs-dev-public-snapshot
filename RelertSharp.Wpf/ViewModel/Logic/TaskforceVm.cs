using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using RelertSharp.Common;
using RelertSharp.MapStructure.Logic;

namespace RelertSharp.Wpf.ViewModel
{
    internal class TaskforceVm : BaseVm<TaskforceItem>
    {
        public TaskforceVm()
        {
            data = new TaskforceItem();
        }
        public TaskforceVm(object obj) : base(obj)
        {

        }


        #region Curd
        public void AddItemAt(int pos, string regname)
        {
            TaskforceUnit u = data.AddItemAt(pos);
            u.RegName = regname;
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
            TaskforceUnit u = data.CopyItemAt(sourceIndex);
            SetProperty(nameof(Items));
        }
        public void RemoveAllItem()
        {
            data.RemoveAll();
            SetProperty(nameof(Items));
        }
        #endregion


        #region Bind Call
        private TaskforceItemVm selectedItem;
        public TaskforceItemVm SelectedItem
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
        public string Group
        {
            get { return data.Group; }
            set
            {
                data.Group = value;
                SetProperty();
            }
        }
        public TaskforceGroupVm Items
        {
            get { return new TaskforceGroupVm(data.Members); }
        }
        public int Count { get { return data.Members.Count; } }
        #endregion
    }


    internal class TaskforceGroupVm : BaseNotifyCollectionVm<TaskforceItemVm>, IEnumerable
    {
        private List<TaskforceItemVm> data;
        public TaskforceGroupVm()
        {
            data = new List<TaskforceItemVm>();
        }
        public TaskforceGroupVm(IEnumerable<TaskforceUnit> src)
        {
            data = new List<TaskforceItemVm>();
            src.Foreach(x =>
            {
                data.Add(new TaskforceItemVm(x));
            });
        }
        public IEnumerator GetEnumerator()
        {
            return data.GetEnumerator();
        }
        public List<TaskforceItemVm> Items { get { return data; } }
    }

    internal class TaskforceItemVm : BaseVm<TaskforceUnit>
    {
        public TaskforceItemVm()
        {
            data = new TaskforceUnit(Constant.ITEM_NONE, 0);
            data.InfoUpdated += UpdateInfo;
        }
        public TaskforceItemVm(TaskforceUnit item) : base(item)
        {
            item.InfoUpdated += UpdateInfo;
        }

        private void UpdateInfo(object sender, EventArgs e)
        {
            SetProperty(nameof(Title));
            SetProperty(nameof(CameoImage));
        }


        #region Bind Call
        public IIndexableItem CurrentUnit
        {
            get
            {
                return new ComboItem(data.RegName, data.UiName);
            }
            set
            {
                if (value == null) return;
                data.RegName = value.Id;
                SetProperty();
            }
        }
        public string Title
        {
            get
            {
                return string.Format("{0}\n{1}\n{2}", data.UiName, data.RegName, data.Count);
            }
        }
        public ImageSource CameoImage
        {
            get
            {
                if (data.RegName == Constant.ITEM_NONE) return null;
                return GlobalVar.GlobalDir.GetPcxImage(data.RegName).ToWpfImage();
            }
        }
        public int UnitNum
        {
            get { return data.Count; }
            set
            {
                data.Count = value;
                SetProperty();
            }
        }
        #endregion
    }
}
