using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RelertSharp.GUI.Controls
{
    public partial class PickPanel
    {
        private void InitializeLightRoom()
        {
            lbxLightSource.LoadAs(Map.LightSources);
        }
    }
}
