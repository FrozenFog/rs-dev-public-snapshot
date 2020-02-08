using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using relert_sharp.FileSystem;
using relert_sharp.SubWindows;

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
#if DEBUG
            _run.M();
#else
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new LogicEditor());
#endif
        }
        static void Initialization()
        {
            Utils.Misc.Init_Language();
            Common.GlobalVar.GlobalConfig = new Common.RSConfig();
            Common.GlobalVar.GlobalDir = new FileSystem.VirtualDir();
        }
    }
}
