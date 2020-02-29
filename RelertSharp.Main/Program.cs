using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using relert_sharp.FileSystem;
using relert_sharp.Common;
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
            GlobalVar.GlobalConfig = new RSConfig();
            GlobalVar.GlobalDir = new VirtualDir();
            GlobalVar.GlobalRules = new IniSystem.Rules(GlobalVar.GlobalDir.GetRawByte(GlobalVar.GlobalConfig.RulesName), GlobalVar.GlobalConfig.RulesName);
            GlobalVar.GlobalSound = new IniSystem.SoundRules(GlobalVar.GlobalConfig.SoundName, GlobalVar.GlobalConfig.EvaName, GlobalVar.GlobalConfig.ThemeName);
            GlobalVar.GlobalSoundBank = new SoundBank(GlobalVar.GlobalConfig.BagNameList);
        }
    }
}
