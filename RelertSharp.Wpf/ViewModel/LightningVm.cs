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
        public LightningVm()
        {
            data = new LightningItem();
        }
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
        public float Blue
        {
            get { return data.Blue; }
            set
            {
                data.Blue = value;
                SetProperty();
            }
        }
        public float Ambient
        {
            get { return data.Ambient; }
            set
            {
                data.Ambient = value;
                SetProperty();
            }
        }
        public float Ground
        {
            get { return data.Ground; }
            set
            {
                data.Ground = value;
                SetProperty();
            }
        }
        public float Level
        {
            get { return data.Level; }
            set
            {
                data.Level = value;
                SetProperty();
            }
        }
    }
}
