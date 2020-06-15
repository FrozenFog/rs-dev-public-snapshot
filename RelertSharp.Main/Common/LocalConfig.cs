using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RelertSharp.IniSystem;

namespace RelertSharp.Common
{
    public class LocalConfig : INIFile
    {
        private static RsLog Log { get { return GlobalVar.Log; } }
        #region Ctor
        public LocalConfig(string name) : base(name, INIFileType.DefaultINI, true)
        {

        }
        public LocalConfig() : base(true)
        {

        }
        #endregion


        #region Public Methods
        public void SaveConfig()
        {
            string name = Application.StartupPath + "\\local.rsc";
            SaveIni(name, true);
            Log.Write("Local config saved!");
            Dispose();
        }
        public Dictionary<string, string[]> GetCustomObjectFolder()
        {
            Dictionary<string, string[]> result = new Dictionary<string, string[]>();
            foreach (INIPair p in this["UserCustomTree"])
            {
                result[p.Name] = p.ParseStringList();
            }
            return result;
        }
        #endregion


        #region Public Calls
        public bool IsValid { get { return !(string.IsNullOrEmpty(GamePath) || string.IsNullOrEmpty(PrimaryConfigName)); } }
        public string GamePath
        {
            get
            {
                return this["General"]["GamePath"];
            }
            set
            {
                this["General"]["GamePath"] = value;
            }
        }
        public string PrimaryConfigName
        {
            get { return this["General"]["PrimaryConfigPath"]; }
            set { this["General"]["PrimaryConfigPath"] = value; }
        }
        public string RecentPath
        {
            get { return this["General"]["RecentPath"]; }
            set { this["General"]["RecentPath"] = value; }
        }
        #endregion
    }
}
