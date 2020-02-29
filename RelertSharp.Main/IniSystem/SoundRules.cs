using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using relert_sharp.Common;

namespace relert_sharp.IniSystem
{
    public class SoundRules
    {
        private INIFile sound, eva, theme;
        //private string eva1key, eva2key, eva3key, eva4key, eva5key, eva6key;


        #region Constructor - SoundRules
        public SoundRules(string soundname, string evaname, string themename)
        {
            sound = new INIFile(GlobalVar.GlobalDir.GetRawByte(GlobalVar.GlobalConfig.SoundName), GlobalVar.GlobalConfig.SoundName, INIFileType.SoundINI);
            eva = new INIFile(GlobalVar.GlobalDir.GetRawByte(GlobalVar.GlobalConfig.EvaName), GlobalVar.GlobalConfig.EvaName, INIFileType.EvaINI);
            theme = new INIFile(GlobalVar.GlobalDir.GetRawByte(GlobalVar.GlobalConfig.ThemeName), GlobalVar.GlobalConfig.ThemeName, INIFileType.ThemeINI);
        }
        #endregion


        #region Public Methods - SoundRules
        public INIEntity GetEva(string index)
        {
            string evaname = eva["DialogList"][index];
            return eva[evaname];
        }
        public INIEntity GetSound(string index)
        {
            string soundname = sound["SoundList"][index];
            return sound[soundname];
        }
        public INIEntity GetTheme(string index)
        {
            string themename = theme["Themes"][index];
            return theme[themename];
        }
        #endregion


        #region Public Calls - SoundRules
        public List<TechnoPair> SoundList { get; private set; }
        public List<TechnoPair> ThemeList { get; private set; }
        public List<TechnoPair> EvaList { get; private set; }
        #endregion
    }
}
