using RelertSharp.MapStructure.Logic;
using RelertSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelertSharp.Wpf.ViewModel
{
    internal class TaskforceListVm : BaseVm<TaskforceItem>, IIndexableItem
    {
        #region Ctor
        public TaskforceListVm()
        {
            data = new TaskforceItem();
        }
        public TaskforceListVm(TaskforceItem src) : base(src)
        {
            src.NameUpdated += SetName;
        }

        private void SetName(object sender, EventArgs e)
        {
            SetProperty(nameof(Title));
        }
        #endregion


        #region Public
        public void ChangeDisplay(IndexableDisplayType type)
        {
            data.ChangeDisplay(type);
            SetProperty(nameof(Title));
        }
        #endregion


        #region Bind Call
        public string Title
        {
            get { return data.ToString(); }
        }
        public TaskforceItem Data { get { return data; } }

        public string Id { get => ((IIndexableItem)Data).Id; set => ((IIndexableItem)Data).Id = value; }
        public string Name { get => ((IIndexableItem)Data).Name; set => ((IIndexableItem)Data).Name = value; }

        public string Value => ((IIndexableItem)Data).Value;
        #endregion
    }
}
