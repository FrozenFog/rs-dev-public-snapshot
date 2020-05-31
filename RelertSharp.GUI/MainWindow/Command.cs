using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RelertSharp.Common;
using static RelertSharp.Common.GlobalVar;
using static RelertSharp.Language;

namespace RelertSharp.GUI
{
    public partial class MainWindowTest
    {
        private void ExecuteCommand(string cmd)
        {
            if (cmd.StartsWith(@"/"))
            {
                string[] tmp = cmd.Substring(1).Split(' ');
                string cmdType = tmp[0];
                string args = tmp[1];
                switch (cmdType)
                {
                    case "goto":
                    case "g":
                        CmdMoveTo(args.Split(','));
                        break;
                }
            }

        }

        private void CmdMoveTo(string[] args)
        {
            try
            {
                I2dLocateable pos = new Pnt(int.Parse(args[0]), int.Parse(args[1]));
                Engine.MoveTo(pos);
            }
            catch
            {
                MessageBox.Show(DICT["RSCmdMoveError"], DICT["RSMainTitle"], MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
