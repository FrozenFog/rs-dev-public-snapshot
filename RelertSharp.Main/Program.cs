﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using relert_sharp.FileSystem;
using relert_sharp.Common;
using relert_sharp.SubWindows.LogicEditor;

namespace relert_sharp
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
            _run.M();
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
            GlobalVar.GlobalDir = new VirtualDir();
            GlobalVar.GlobalRules = new IniSystem.Rules(GlobalVar.GlobalDir.GetRawByte(GlobalVar.GlobalConfig.RulesName + ".ini"), GlobalVar.GlobalConfig.RulesName + ".ini");
            GlobalVar.GlobalRules.LoadArt(GlobalVar.GlobalDir.GetFile(GlobalVar.GlobalConfig.ArtName, FileExtension.INI));
            GlobalVar.GlobalSound = new IniSystem.SoundRules(GlobalVar.GlobalConfig.SoundName, GlobalVar.GlobalConfig.EvaName, GlobalVar.GlobalConfig.ThemeName);
            GlobalVar.GlobalSoundBank = new SoundBank(GlobalVar.GlobalConfig.BagNameList);

            //csf
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
    }
}
