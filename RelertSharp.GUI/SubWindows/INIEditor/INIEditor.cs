using RelertSharp.Common;
using RelertSharp.IniSystem;
using RelertSharp.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static RelertSharp.Language;

namespace RelertSharp.SubWindows.INIEditor
{
    public partial class INIEditor : Form
    {

        private RsLog Logger = GlobalVar.Log;
        private string stsHeader, stsEntityCount;

        private bool isSaved = false;

        #region Ctor - INIEditor
        public INIEditor()
        {
            InitializeComponent();
            Initialize();
        }
        #endregion

        #region Private Methods - INIEditor
        private void Initialize()
        {
            InitializeLanguage();

            // This INI Editor shouldn't save unless user called it
            // So we need a copy of origional Dictionary
            Data = GlobalVar.CurrentMapDocument.Map.IniResidue.Clone();

            UpdateSectionList();
        }

        private void InitializeLanguage()
        {
            foreach (Control c in Controls) SetControlLanguage(c);
            Text = DICT[Text];
            stsHeader = DICT["INIstsSectionInfoHeader"];
            stsEntityCount = DICT["INIstsSectionInfoEntityCount"];
        }

        /// <summary>
        /// If isUpdating is true, then isSectionUpdaing Property should be set and
        /// BeginUpdate & EndUpdate should be called in other functions manually
        /// </summary>
        /// <param name="isUpdating"></param>
        private void UpdateSectionList(bool isUpdating = false)
        {
            if(!isUpdating)
            {
                isSectionsUpdating = true;
                lbxSections.BeginUpdate();
            }

            string selectedSection = lbxSections.SelectedItem as string;
            lbxSections.Items.Clear();
            
            var keys = Data.Keys.ToList();
            keys.Sort();
            
            foreach (string section in keys)
                lbxSections.Items.Add(section);
            if (!string.IsNullOrEmpty(selectedSection) && Data.ContainsKey(selectedSection))
            {
                lbxSections.SelectedIndex = lbxSections.Items.IndexOf(selectedSection);
                UpdateKeyList(selectedSection);
            }
            else
            {
                lbxSections.SelectedIndex = -1;
                UpdateKeyList(null as INIEntity);
            }
            if (lbxSections.SelectedIndex == -1) stsSectionInfo.Text = "";

            if (!isUpdating)
            {
                lbxSections.EndUpdate();
                isSectionsUpdating = false;
            }
        }

        /// <summary>
        /// Do not use null string as this function's param
        /// </summary>
        /// <param name="section"></param>
        private void UpdateKeyList(string section) => UpdateKeyList(Data[section]);
        private void UpdateKeyList(INIEntity entity)
        {
            dgvKeys.BeginUpdate();
            dgvKeys.Rows.Clear();

            if (entity == null)
            {
                dgvKeys.EndUpdate(); // Don't forget it!
                return;
            }

            stsSectionInfo.Text = stsHeader + " " + entity.Name + " " + stsEntityCount + " " + entity.DictData.Count;

            foreach(INIPair pair in _CurrentSection)
            {
                DataGridViewRow row = new DataGridViewRow();
                DataGridViewTextBoxCell keyCell = new DataGridViewTextBoxCell();
                keyCell.Value = pair.Name;
                DataGridViewTextBoxCell valueCell = new DataGridViewTextBoxCell();
                valueCell.Value = pair.Value;
                row.Cells.Add(keyCell);
                row.Cells.Add(valueCell);
                dgvKeys.Rows.Add(row);
            }

            dgvKeys.EndUpdate();
        }

        private void OnValueChanged(string Key,string Value)
        {
            
        }
        private void OnKeyChanged(string Key,string Value)
        {

        }


        private void SaveINI()
        {
            MessageBox.Show("SAVE");
        }
        private void ImportINI()
        {
            MessageBox.Show("IMPORT");
        }



        #endregion

        #region Private Calls - INIEditor
        private INIEntity _CurrentSection { get { return Data[lbxSections.SelectedItem as string]; } }



        #endregion

        #region Public Calls - INIEditor
        public Dictionary<string,INIEntity> Data { get; private set; }




        #endregion
    }
}
