using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelertSharp.GUI
{
    public partial class MainWindowTest
    {
        private void SwitchToFramework(bool enable)
        {
            if (drew)
            {
                Map.SwitchToFramework(enable);
            }
        }
        private void SwitchToFlatGround(bool enable)
        {
            if (drew)
            {
                Map.SwitchFlatGround(enable);
            }
        }
    }
}
