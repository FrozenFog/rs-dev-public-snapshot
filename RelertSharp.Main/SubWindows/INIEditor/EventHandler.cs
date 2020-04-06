using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using RelertSharp.MapStructure.Logic;
using RelertSharp.FileSystem;
using RelertSharp.Common;
using RelertSharp.IniSystem;
using static RelertSharp.Language;

namespace RelertSharp.SubWindows.INIEditor
{
    public partial class INIEditor : Form
    {
        #region TextBox
        private void txbSectionSearch_Enter(object sender, EventArgs e)
        {
            if (txbSectionSearch.Text == DICT["INIlblFakeSearch"])
            {
                txbSectionSearch.Text = "";
                txbSectionSearch.ForeColor = Color.Black;
            }
        }

        private void txbSectionSearch_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txbSectionSearch.Text))
            {
                txbSectionSearch.Text = DICT["INIlblFakeSearch"];
                txbSectionSearch.ForeColor = SystemColors.GrayText;
            }
        }
        #endregion


        #region Button
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txbSectionSearch.Text.Trim()) || txbSectionSearch.Text.Trim() == DICT["INIlblFakeSearch"])
            {
                bdsSection.DataSource = null;
                bdsSection.DataSource = bdsSectionL;
                return;
            }
            searchList = bdsSectionL.FindAll(s => s.Contains(txbSectionSearch.Text.Trim()));
            bdsSection.DataSource = null;
            bdsSection.DataSource = searchList;
        }
        
        private void btnNewSection_Click(object sender, EventArgs e)
        {
            dlgNewSection dlgNew = new dlgNewSection();
            if (dlgNew.ShowDialog() == DialogResult.OK)
            {
                NewSectionName = dlgNew.retSectionName;
                if (!string.IsNullOrEmpty(NewSectionName)){
                    int findIndex = bdsSectionL.FindIndex(s => s.Equals(NewSectionName));
                    if (findIndex >= 0)
                    {
                        lbxSectionList.SelectedIndex = findIndex;
                        return;
                    }
                    bdsSectionL.Add(NewSectionName);
                    sections[NewSectionName] = new INIEntity();
                    bdsSection.DataSource = null;
                    bdsSection.DataSource = bdsSectionL;
                }
            }
        }

        private void btnDelSection_Click(object sender, EventArgs e)
        {
            if (lbxSectionList.SelectedItem != null)
            {
                sections.Remove(lbxSectionList.Text);
                bdsSectionL = sections.Keys.ToList();
                bdsSection.DataSource = null;
                bdsSection.DataSource = bdsSectionL;
            }
        }

        private void btnNewKey_Click(object sender, EventArgs e)
        {
            if (lbxSectionList.SelectedItem != null) 
                if (!string.IsNullOrEmpty(txbKey.Text))
                {
                    foreach(DataGridViewRow row in dgvKeys.Rows)
                    {
                        if (row.Cells[0].Value.ToString() == txbKey.Text)
                        {
                            row.Cells[1].Value = txbValue.Text;
                            return;
                        }
                    }
                    iniPairs.Add(new INIPair(txbKey.Text, txbValue.Text, string.Empty, string.Empty));
                    bdsKey.DataSource = null;
                    bdsKey.DataSource = iniPairs;
                    dgvKeys.Columns[0].HeaderText = DICT["INIdgvKeyKey"];
                    dgvKeys.Columns[0].FillWeight = 1;
                    dgvKeys.Columns[0].ReadOnly = true;
                    dgvKeys.Columns[1].HeaderText = DICT["INIdgvKeyValue"];
                    dgvKeys.Columns[1].FillWeight = 2;
                    dgvKeys.Columns[2].Visible = false;
                    dgvKeys.Columns[3].Visible = false;
                    dgvKeys.Columns[4].Visible = false;
                    dgvKeys.Columns[5].Visible = false;
                }
        }

        private void btnDelKey_Click(object sender, EventArgs e)
        {
            if (lbxSectionList.SelectedItem != null)
                if (dgvKeys.SelectedRows.Count > 0)
                {
                    foreach(INIPair inipair in iniPairs)
                    {
                        if (inipair.Name == dgvKeys.SelectedRows[0].Cells[0].Value.ToString())
                        {
                            iniPairs.Remove(inipair);
                            sections[SectionName].RemovePair(new INIPair(inipair.Name, "", "", ""));
                            bdsKey.DataSource = null;
                            bdsKey.DataSource = iniPairs;
                            dgvKeys.Columns[0].HeaderText = DICT["INIdgvKeyKey"];
                            dgvKeys.Columns[0].FillWeight = 1;
                            dgvKeys.Columns[0].ReadOnly = true;
                            dgvKeys.Columns[1].HeaderText = DICT["INIdgvKeyValue"];
                            dgvKeys.Columns[1].FillWeight = 2;
                            dgvKeys.Columns[2].Visible = false;
                            dgvKeys.Columns[3].Visible = false;
                            dgvKeys.Columns[4].Visible = false;
                            dgvKeys.Columns[5].Visible = false;
                            return;
                        }
                    }
                }
        }
        #endregion


        #region ListBox & DataGridView
        private void lbxSectionList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SectionName != null)
            {
                INIEntity tmp = new INIEntity();
                if (sections.TryGetValue(SectionName, out tmp))
                    foreach(DataGridViewRow row in dgvKeys.Rows)
                    {
                        INIPair iNIPair = new INIPair(
                            row.Cells[0].Value.ToString(), row.Cells[1].Value.ToString(),
                            row.Cells[2].Value.ToString(), row.Cells[3].Value.ToString());
                        sections[SectionName].AddPair(iNIPair);
                    }
            }
            if (string.IsNullOrEmpty(lbxSectionList.Text)) dgvKeys.Rows.Clear();
            SectionName = lbxSectionList.Text;
            iniPairs = sections[SectionName].DataList;
            bdsKey.DataSource = null;
            bdsKey.DataSource = iniPairs;
            dgvKeys.Columns[0].HeaderText = DICT["INIdgvKeyKey"];
            dgvKeys.Columns[0].FillWeight = 1;
            dgvKeys.Columns[0].ReadOnly = true;
            dgvKeys.Columns[1].HeaderText = DICT["INIdgvKeyValue"];
            dgvKeys.Columns[1].FillWeight = 2;
            dgvKeys.Columns[2].Visible = false;
            dgvKeys.Columns[3].Visible = false;
            dgvKeys.Columns[4].Visible = false;
            dgvKeys.Columns[5].Visible = false;

        }

        private void dgvKeys_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (dgvKeys.SelectedRows.Count > 0)
                if (dgvKeys.SelectedRows[0].Cells.Count > 0)
                {
                    string curDesc = DICT[dgvKeys.SelectedRows[0].Cells[0].Value.ToString()];
                    if (!string.IsNullOrEmpty(curDesc))
                    {
                        rtxbKeyDesc.Text = curDesc;
                        return;
                    }
                }
            rtxbKeyDesc.Text = DICT["INIrtxbNodesc"];        
        }
        #endregion


        #region Form
        private void INIEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            switch (MessageBox.Show(this.Owner, DICT["INImsgSave"], DICT["INITitle"], MessageBoxButtons.YesNoCancel))
            {
                case DialogResult.Yes:
                    {
                        break;
                    }
                case DialogResult.No:
                    {
                        file.IniResidue = DeepCopy(osections) as Dictionary<string, INIEntity>;
                        break;
                    }
                case DialogResult.Cancel:
                default:
                    e.Cancel = true;
                    break;
            }
            return;
        }
        #endregion
    }
}