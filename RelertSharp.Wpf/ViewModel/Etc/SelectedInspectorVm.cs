using RelertSharp.Common;
using RelertSharp.MapStructure.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelertSharp.Wpf.ViewModel
{
    internal class SelectedInspectorVm : BaseVm<IMapObject>
    {
        public SelectedInspectorVm()
        {
            data = new VirtualMapObject();
        }
    }
}
