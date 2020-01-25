using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace relert_sharp
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Initialization();
            bool debug = true;
            if (!debug)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainWindow());
            }
            else
            {
                _run.M();
            }
        }
        static void Initialization()
        {
            Utils.Misc.Init_Language();
            Common.GlobalVar.GlobalConfig = new Common.RSConfig();
            Common.GlobalVar.GlobalDir = new FileSystem.VirtualDir();
        }
    }
}
