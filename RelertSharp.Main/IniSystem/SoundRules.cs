using RelertSharp.Common;
using System.Collections.Generic;

namespace RelertSharp.IniSystem
{
    public class SoundRules
    {
        private INIFile sound, eva, theme;
        //private string eva1key, eva2key, eva3key, eva4key, eva5key, eva6key;


        #region Ctor - SoundRules
        public SoundRules(string soundname, string evaname, string themename)
        {
            sound = GlobalVar.GlobalDir.GetFile(GlobalVar.GlobalConfig.SoundName, FileExtension.INI, true);
            eva = GlobalVar.GlobalDir.GetFile(GlobalVar.GlobalConfig.EvaName, FileExtension.INI, true);
            theme = GlobalVar.GlobalDir.GetFile(GlobalVar.GlobalConfig.ThemeName, FileExtension.INI, true);
            GetPairs();
        }
        #endregion


        #region Private Methods - SoundRules
        private void GetPairs()
        {
            INIEntity ent;
            ent = sound["SoundList"];
            int i = 0;
            foreach (INIPair p in ent)
            {
                SoundList.Add(sound[p.Value as string].ToTechno(p.Name));
                SoundList0.Add(new TechnoPair(i.ToString(), p.Value));
                i++;
            }
            i = 0;
            ent = eva["DialogList"];
            foreach (INIPair p in ent)
            {
                EvaList.Add(eva[p.Value as string].ToTechno(p.Name));
                EvaList0.Add(new TechnoPair(i.ToString(), p.Value));
                i++;
            }
            i = 0;
            ent = theme["Themes"];
            foreach (INIPair p in ent)
            {
                ThemeList.Add(theme[p.Value as string].ToTechno(p.Name));
                ThemeList0.Add(new TechnoPair(i.ToString(), p.Value));
                i++;
            }
        }
        #endregion


        #region Public Methods - SoundRules
        public INIEntity GetEva(string index)
        {
            string evaname = eva["DialogList"][index];
            return eva[evaname];
        }
        public INIEntity GetEva(int index)
        {
            string name = eva["DialogList"].GetPair(index).Value;
            return eva[name];
        }
        public INIEntity GetSound(string index)
        {
            string soundname = sound["SoundList"][index];
            return sound[soundname];
        }
        public INIEntity GetSound(int index)
        {
            string name = sound["SoundList"].GetPair(index).Value;
            return sound[name];
        }
        public INIEntity GetTheme(string index)
        {
            string themename = theme["Themes"][index];
            return theme[themename];
        }
        public INIEntity GetTheme(int index)
        {
            string name = theme["Themes"].GetPair(index).Value;
            return theme[name];
        }
        #endregion


        #region Public Calls - SoundRules
        public List<TechnoPair> SoundList { get; private set; } = new List<TechnoPair>();
        public List<TechnoPair> ThemeList { get; private set; } = new List<TechnoPair>();
        public List<TechnoPair> EvaList { get; private set; } = new List<TechnoPair>();
        public List<TechnoPair> EvaList0 { get; private set; } = new List<TechnoPair>();
        public List<TechnoPair> SoundList0 { get; private set; } = new List<TechnoPair>();
        public List<TechnoPair> ThemeList0 { get; private set; } = new List<TechnoPair>();
        #endregion
    }
}
