using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.MapStructure;

namespace RelertSharp.Wpf.ViewModel
{
    internal class LightningVm : BaseVm<LightningItem>
    {
        public float Red
        {
            get { return data.Red; }
            set
            {
                data.Red = value;
                SetProperty();
            }
        }
        public float Green
        {
            get { return data.Green; }
            set
            {
                data.Green = value;
                SetProperty();
            }
        }
    }
}
