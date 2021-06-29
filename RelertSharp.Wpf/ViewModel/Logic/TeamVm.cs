using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.IniSystem;
using RelertSharp.MapStructure.Logic;
using RelertSharp.Common;
using System.Windows.Data;

namespace RelertSharp.Wpf.ViewModel
{
    internal class TeamVm : BaseVm<TeamItem>
    {
        public TeamVm()
        {
            data = new TeamItem();
        }
        public TeamVm(object obj) : base(obj) { }
        public TeamItem Data { get { return data; } }
    }
}
