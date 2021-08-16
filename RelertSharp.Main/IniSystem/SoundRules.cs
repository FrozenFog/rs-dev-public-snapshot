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
            sound = GlobalVar.GlobalDir.GetFile(soundname, FileExtension.INI, true);
            eva = GlobalVar.GlobalDir.GetFile(evaname, FileExtension.INI, true);
            theme = GlobalVar.GlobalDir.GetFile(themename, FileExtension.INI, true);
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
                //SoundList.Add(sound[p.Value as string].ToTechno(p.Value));
                SoundList.Add(new ComboItem(p.Value, ""));
                SoundList0.Add(new ComboItem(i.ToString(), p.Value));
                i++;
            }
            i = 0;
            ent = eva["DialogList"];
            foreach (INIPair p in ent)
            {
                //EvaList.Add(eva[p.Value as string].ToTechno(p.Value));
                EvaList.Add(new ComboItem(p.Value, ""));
                EvaList0.Add(new ComboItem(i.ToString(), p.Value));
                i++;
            }
            i = 0;
            ent = theme["Themes"];
            foreach (INIPair p in ent)
            {
                //ThemeList.Add(theme[p.Value as string].ToTechno(p.Value));
                ThemeList.Add(new ComboItem(p.Value, ""));
                ThemeList0.Add(new ComboItem(i.ToString(), p.Value));
                i++;
            }
        }
        #endregion


        #region Public Methods - SoundRules
        public INIEntity GetEva(string regName)
        {
            string evaname = eva["DialogList"].Find(x => x.Value == regName).Value;
            return eva[evaname];
        }
        public INIEntity GetSound(string regName)
        {
            string soundname = sound["SoundList"].Find(x => x.Value == regName).Value;
            return sound[soundname];
        }
        public INIEntity GetTheme(string regName)
        {
            string themename = theme["Themes"].Find(x => x.Value == regName).Value;
            return theme[themename];
        }
        #endregion


        #region Public Calls - SoundRules
        public List<ComboItem> SoundList { get; private set; } = new List<ComboItem>();
        public List<ComboItem> ThemeList { get; private set; } = new List<ComboItem>();
        public List<ComboItem> EvaList { get; private set; } = new List<ComboItem>();
        public List<ComboItem> EvaList0 { get; private set; } = new List<ComboItem>();
        public List<ComboItem> SoundList0 { get; private set; } = new List<ComboItem>();
        public List<ComboItem> ThemeList0 { get; private set; } = new List<ComboItem>();
        #endregion
    }
}
