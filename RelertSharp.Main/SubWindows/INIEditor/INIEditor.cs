﻿using System;
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
        public string NewSectionName;
        private BindingSource bdsKey = new BindingSource();
        private BindingSource bdsSection = new BindingSource();
        private MapFile file;


        #region Ctor - INIEditor
        public INIEditor(MapFile m)
        {
            InitializeComponent();
            SetLanguage();
            file = m;
            sections = m.Map.IniResidue;
            osections = DeepCopy(sections) as Dictionary<string, INIEntity>;
            
            bdsSectionL = sections.Keys.ToList();
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
    }
}
