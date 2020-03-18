using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.Common;

namespace RelertSharp.IniSystem
{
    public class SoundRules
    {
        private INIFile sound, eva, theme;
        //private string eva1key, eva2key, eva3key, eva4key, eva5key, eva6key;


        #region Ctor - SoundRules
        public SoundRules(string soundname, string evaname, string themename)
        {
            sound = GlobalVar.GlobalDir.GetFile(GlobalVar.GlobalConfig.SoundName, FileExtension.INI);
            eva = GlobalVar.GlobalDir.GetFile(GlobalVar.GlobalConfig.EvaName, FileExtension.INI);
            theme = GlobalVar.GlobalDir.GetFile(GlobalVar.GlobalConfig.ThemeName, FileExtension.INI);
            GetPairs();
        }
        #endregion


        #region Private Methods - SoundRules
        private void GetPairs()
        {
            INIEntity ent;
            ent = sound["SoundList"];
            SoundList = new List<TechnoPair>();
            foreach (INIPair p in ent)
            {
                SoundList.Add(sound[p.Value as string].ToTechno(p.Name));
            }
            ent = eva["DialogList"];
            EvaList = new List<TechnoPair>();
            foreach (INIPair p in ent)
            {
                EvaList.Add(eva[p.Value as string].ToTechno(p.Name));
            }
            ent = theme["Themes"];
            ThemeList = new List<TechnoPair>();
            foreach (INIPair p in ent)
            {
                ThemeList.Add(theme[p.Value as string].ToTechno(p.Name));
            }
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
