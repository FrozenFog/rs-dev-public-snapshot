using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using RelertSharp.Common;
using RelertSharp.IniSystem;

namespace RelertSharp.Wpf.Dialogs
{
    /// <summary>
    /// DlgIniInspector.xaml 的交互逻辑
    /// </summary>
    public partial class DlgIniInspector : Window
    {
        private const string NO_CONTENT = "No Entry Exist";
        public DlgIniInspector()
        {
            InitializeComponent();
        }

        public void SetContent(string regname)
        {
            string txtRules = NO_CONTENT, txtArt = NO_CONTENT;
            if (GlobalVar.GlobalRules != null)
            {
                INIEntity rules = GlobalVar.GlobalRules[regname];
                INIEntity art = GlobalVar.GlobalRules.Art[regname];
                if (rules != INIEntity.NullEntity) txtRules = rules.SaveString();
                if (art != INIEntity.NullEntity) txtArt = art.SaveString();
            }
            txbRules.Text = txtRules;
            txbArt.Text = txtArt;
        }
        public void SetContents(IEnumerable<string> regNames)
        {
            regNames = regNames.Distinct();
            StringBuilder sbRules = new StringBuilder();
            StringBuilder sbArts = new StringBuilder();
            if (GlobalVar.GlobalRules != null)
            {
                foreach (string regname in regNames)
                {
                    INIEntity rules = GlobalVar.GlobalRules[regname];
                    INIEntity art = GlobalVar.GlobalRules.Art[regname];
                    if (rules != INIEntity.NullEntity) sbRules.AppendLine(rules.SaveString());
                    if (art != INIEntity.NullEntity) sbArts.AppendLine(art.SaveString());
                }
            }
            if (sbRules.Length == 0) sbRules.AppendLine(NO_CONTENT);
            if (sbArts.Length == 0) sbArts.AppendLine(NO_CONTENT);
            txbRules.Text = sbRules.ToString();
            txbArt.Text = sbArts.ToString();
        }
    }
}
