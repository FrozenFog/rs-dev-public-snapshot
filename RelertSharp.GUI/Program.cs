using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using RelertSharp.FileSystem;
using RelertSharp.IniSystem;
using RelertSharp.Common;
using RelertSharp.SubWindows.LogicEditor;
using RelertSharp.SubWindows.INIEditor;

namespace RelertSharp.GUI
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
#if DEBUG
            Initialization();
            _Test.Run();
#else
            if (args.Length < 1) return;
            try
            {
                Initialization();
            }
            catch (Exception e)
            {
                MessageBox.Show("Initialization failed!\nTrace:\n" + e.StackTrace, "RelertSharp", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                MapFile map = new MapFile(args[0]);
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new LogicEditor(map.Map));
            }
            catch (Exception e)
            {
                MessageBox.Show("Unhandled error!\nTrace:\n" + e.StackTrace, "Fatal", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
#endif
        }
        static void Initialization()
        {
            Utils.Misc.Init_Language();
            SetGlobalVar();
        }
        static void SetGlobalVar()
        {
            GlobalVar.GlobalConfig = new RSConfig();
            INIFile local = new INIFile("local.rsc");
            GlobalVar.GlobalConfig.Override(local.IniData);
            GlobalVar.GlobalDir = new VirtualDir();
            GlobalVar.GlobalRules = new Rules(GlobalVar.GlobalDir.GetRawByte(GlobalVar.GlobalConfig.RulesName + ".ini"), GlobalVar.GlobalConfig.RulesName + ".ini");
            GlobalVar.GlobalRules.LoadArt(GlobalVar.GlobalDir.GetFile(GlobalVar.GlobalConfig.ArtName, FileExtension.INI));
            GlobalVar.GlobalSound = new SoundRules(GlobalVar.GlobalConfig.SoundName, GlobalVar.GlobalConfig.EvaName, GlobalVar.GlobalConfig.ThemeName);
            GlobalVar.GlobalSoundBank = new SoundBank(GlobalVar.GlobalConfig.BagNameList);

            //csf
            if (GlobalVar.GlobalConfig.StringtableList.Count > 0)
            {
                GlobalVar.GlobalCsf = GlobalVar.GlobalDir.GetFile(GlobalVar.GlobalConfig.StringtableList[0], FileExtension.CSF);
                if (GlobalVar.GlobalConfig.StringtableList.Count > 1)
                {
                    foreach (string name in GlobalVar.GlobalConfig.StringtableList.Skip(1))
                    {
                        GlobalVar.GlobalCsf.AddCsfLib(GlobalVar.GlobalDir.GetFile(name, FileExtension.CSF));
                    }
                }
                GlobalVar.GlobalCsf.ToTechno();
            }
            else GlobalVar.GlobalCsf = new CsfFile();
        }
    }
}
