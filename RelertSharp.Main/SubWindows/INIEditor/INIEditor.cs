using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RelertSharp.FileSystem;
using RelertSharp.MapStructure;
using RelertSharp.MapStructure.Logic;
using RelertSharp.Common;
using RelertSharp.IniSystem;
using static RelertSharp.Language;

namespace RelertSharp.SubWindows.INIEditor
{
    public partial class INIEditor : Form
    {
        private Dictionary<string,INIEntity> sections,osections;
        private List<string> bdsSectionL, searchList;
        private List<INIPair> iniPairs = new List<INIPair>();
        private string SectionName;
        private BindingSource bdsKey = new BindingSource();
        private BindingSource bdsSection = new BindingSource();
        private Map file;


        #region Ctor - INIEditor
        public INIEditor(Map m)
        {
            InitializeComponent();
            SetLanguage();
            file = m;
            sections = m.IniResidue;
            sections.Add("Basic", m.Info.BasicResidue);
            sections.Add("SpecialFlags", m.Info.SpecialFlagsResidue);
            osections = DeepCopy(sections) as Dictionary<string, INIEntity>;
            
            bdsSectionL = sections.Keys.ToList();
            bdsSectionL.Sort();
            bdsSection.DataSource = bdsSectionL;
            dgvKeys.DataSource = bdsKey;
            lbxSectionList.DataSource = bdsSection;
        }
        #endregion


        #region Private Methods - INIEditor
        private void SetLanguage()
        {
            foreach (Control c in Controls) SetControlLanguage(c);
            Text = DICT[Text];

        }

        private object DeepCopy(object obj)
        {
            MemoryStream memoryStream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(memoryStream, obj);
            memoryStream.Position = 0;
            return formatter.Deserialize(memoryStream);
        }
        #endregion


        #region Private Comparer - INIEditor
        private class INIPairNameComparer : IComparer<INIPair>
        {
            public int Compare(INIPair x, INIPair y)
            {
                if (x == null && y == null) return 0;
                if (x == null) return -1;
                if (y == null) return 1;
                return x.Name.CompareTo(y.Name);
            }
        }
        #endregion
    }
}
