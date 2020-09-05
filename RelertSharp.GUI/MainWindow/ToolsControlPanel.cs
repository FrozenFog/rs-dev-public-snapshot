using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RelertSharp.GUI.Model;

namespace RelertSharp.GUI
{
    public partial class MainWindowTest
    {
        private void SwitchToolPanel(MainWindowDataModel.MouseActionType actionType)
        {
            HideControlPanel();
            switch (actionType)
            {
                case MainWindowDataModel.MouseActionType.AddingObject:
                    rbPanelBrush.Visible = true;
                    break;
                case MainWindowDataModel.MouseActionType.AttributeBrush:
                    rbPanelAttribute.Visible = true;
                    break;
                case MainWindowDataModel.MouseActionType.TileBucket:
                    rbPanelBucket.Visible = true;
                    break;
                case MainWindowDataModel.MouseActionType.TileWand:
                    rbPanelWand.Visible = true;
                    break;
            }
        }
        private void HideControlPanel()
        {
            rbPanelAttribute.Visible = false;
            rbPanelBrush.Visible = false;
            rbPanelBucket.Visible = false;
            rbPanelWand.Visible = false;
        }
    }
}
