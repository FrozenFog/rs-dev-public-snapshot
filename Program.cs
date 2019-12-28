using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using relert_sharp.Utils;

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
            bool debug = true;
            if (!debug)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Misc.Init_Language();
                Application.Run(new Main());
            }
            else
            {
                _run.M();
            }
        }
    }
}
