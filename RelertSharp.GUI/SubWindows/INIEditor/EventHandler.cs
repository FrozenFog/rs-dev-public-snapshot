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
using System.Windows.Input;

namespace RelertSharp.SubWindows.INIEditor
{
    public partial class INIEditor : Form
    {

        #region Main Form
        private void INIEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }
        private void btnImport_Click(object sender, EventArgs e) => ImportINI();
        private void btnSave_Click(object sender, EventArgs e) => SaveINI();
        private void tsbReload_Click(object sender, EventArgs e) => ReloadINI();
        private void tbcINI_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tbcINI.SelectedIndex == 0) // Raw to Standard
            {
                INIFile file = new INIFile(System.Text.Encoding.UTF8.GetBytes(reMain.rawEditor.Text), "$rs.inieditor.name");
                Data = file.IniDict.Clone();
                UpdateSectionList();
                stsSectionInfo.Text =
                    "Successfully parsed " + reMain.rawEditor.LineCount + " lines into " + Data.Count + " sections";
            }
            else // Standard to Raw
                UpdateRaw();
        }
        #endregion

        #region Sections

        private bool isSectionsUpdating = false;
        private void lbxSections_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(!isSectionsUpdating)
            {
                if (lbxSections.SelectedItem == null) return;
                UpdateKeyList(_CurrentSection);
                if (lbxSections.SelectedIndex == -1) stsSectionInfo.Text = "";
            }
        }
        private void tsmiSectionInsert_Click(object sender, EventArgs e)
        {
            string origin = "NewSection";
            int i = 0;
            while (Data.ContainsKey(origin + i)) ++i;
            using (dlgEditSection editForm = new dlgEditSection(origin + i))
            {
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    string sectionName = editForm.txbSectionName.Text.Trim();
                    if (string.IsNullOrEmpty(sectionName)) return;
                    if (!Data.ContainsKey(sectionName))
                    {
                        INIEntity entity = new INIEntity(sectionName);
                        Data[sectionName] = entity;
                        UpdateSectionList(false, false);
                        lbxSections.SelectedItem = sectionName;
                        UpdateKeyList();
                    }
                }
            }
        }
        private void tsmiSectionRemove_Click(object sender, EventArgs e)
        {
            if (lbxSections.SelectedIndices.Count <= 0) return;

            isSectionsUpdating = true;
            lbxSections.BeginUpdate();

            var src = lbxSections.SelectedItems;
            foreach (string section in src)
                Data.Remove(section);

            UpdateSectionList(true);

            lbxSections.EndUpdate();
            isSectionsUpdating = false;
        }
        private void tsmiSectionRename_Click(object sender, EventArgs e)
        {
            string origin = lbxSections.SelectedItem as string;
            if (string.IsNullOrEmpty(origin)) return;
            using (dlgEditSection editForm = new dlgEditSection(origin))
            {
                if(editForm.ShowDialog()==DialogResult.OK)
                {
                    string sectionName = editForm.txbSectionName.Text.Trim();
                    if (string.IsNullOrEmpty(sectionName)) return;
                    if (!Data.ContainsKey(sectionName))
                    {
                        INIEntity entity = new INIEntity(Data[origin], sectionName);
                        Data[sectionName] = entity;
                        Data.Remove(origin);
                        UpdateSectionList();
                        lbxSections.SelectedIndex = lbxSections.Items.IndexOf(sectionName);
                    }
                }
            }
        }
        #endregion

        #region Keys
        private void tsmiKeyInsert_Click(object sender, EventArgs e)
        {
            if (_CurrentSection == null) return;
            string keyName = "NewKey";
            int i = 0;
            while (_CurrentSection.DictData.ContainsKey(keyName + i))  ++i;
            INIPair pair = new INIPair(keyName + i, "NULL");
            _CurrentSection.AddPair(pair);
            UpdateKeyList();
        }
        private void tsmiKeyRemove_Click(object sender, EventArgs e)
        {
            List<string> src = new List<string>();
            foreach (DataGridViewRow row in dgvKeys.SelectedRows)
                src.Add(row.Cells[0].Value as string);
            foreach(string key in src)
                _CurrentSection.RemovePair(key);
            UpdateKeyList();
        }
        private void dgvKeys_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (_CurrentSection == null) return;
            if (dgvKeys.SelectedCells.Count == 0) return;
            switch(e.ColumnIndex)
            {
                case 0:
                    OnKeyChanged(_CurrentSection.ElementAt(e.RowIndex).Name, dgvKeys[0, e.RowIndex].Value as string);
                    break;
                case 1:
                    OnValueChanged(dgvKeys[0, e.RowIndex].Value as string, dgvKeys[1, e.RowIndex].Value as string);
                    break;
                default:
                    Logger.Write("dgvKeys_CellValueChanged meet an invalid EventArgs!");
                    // Notice! This shouldn't happen
                    break;
            }

            return;
        }
        #endregion

        #region Raw
        // I don't know, maybe for further use?
        #endregion

    }
}
