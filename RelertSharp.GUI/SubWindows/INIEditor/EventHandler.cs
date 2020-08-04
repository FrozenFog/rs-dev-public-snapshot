using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        #endregion

        #region Sections

        private bool isSectionsUpdating = false;
        private void lbxSections_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(!isSectionsUpdating)
            {
                if (lbxSections.SelectedItem == null) return;
                UpdateKeyList(_CurrentSection);
            }
        }
        private void tsmiSectionInsert_Click(object sender, EventArgs e)
        {

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
        #endregion

        #region Keys
        private void tsmiKeyInsert_Click(object sender, EventArgs e)
        {

        }
        private void tsmiKeyRemove_Click(object sender, EventArgs e)
        {

        }

        private void dgvKeys_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            switch(e.ColumnIndex)
            {
                case 0:
                    OnKeyChanged((sender as DataGridView)[e.ColumnIndex, e.RowIndex].Value as string);
                    break;
                case 1:
                    OnValueChanged((sender as DataGridView)[e.ColumnIndex, e.RowIndex].Value as string);
                    break;
                default:
                    // Notice! This shouldn't happen
                    break;
            }

            return;
        }
        #endregion

    }
}
