using RelertSharp.Common;
using RelertSharp.IniSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static RelertSharp.Language;

namespace RelertSharp.SubWindows.INIEditor
{
    public partial class dlgImportFile : Form
    {
        private INIFile file;

        public dlgImportFile()
        {
            InitializeComponent();
            InitializeLanguage();
        }

        private void InitializeLanguage()
        {
            foreach (Control c in Controls) SetControlLanguage(c);
            Text = DICT[Text];
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            List<INIEntity> selectedItems = new List<INIEntity>(lbxAvailable.SelectedItems.Count);
            foreach (INIEntity entity in lbxAvailable.SelectedItems)
                selectedItems.Add(entity);
            lbxAvailable.BeginUpdate();
            lbxSelected.BeginUpdate();
            foreach (INIEntity entity in selectedItems)
            {
                lbxSelected.Items.Add(entity);
                lbxAvailable.Items.Remove(entity);
            }
            lbxAvailable.EndUpdate();
            lbxSelected.EndUpdate();
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            List<INIEntity> selectedItems = new List<INIEntity>(lbxSelected.SelectedItems.Count);
            foreach (INIEntity entity in lbxSelected.SelectedItems)
                selectedItems.Add(entity);
            lbxAvailable.BeginUpdate();
            lbxSelected.BeginUpdate();
            foreach (INIEntity entity in selectedItems)
            {
                lbxAvailable.Items.Add(entity);
                lbxSelected.Items.Remove(entity);
            }
            lbxAvailable.EndUpdate();
            lbxSelected.EndUpdate();
        }

        private void btnLoadFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "RA2/TS INI file|*.ini|All files|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
                if (File.Exists(openFileDialog.FileName))
                {
                    try
                    {
                        file = new INIFile(openFileDialog.FileName);

                        lbxAvailable.Items.Clear();
                        lbxSelected.Items.Clear();

                        lbxAvailable.BeginUpdate();
                        lbxAvailable.Items.AddRange(file.IniData.ToArray());
                        lbxAvailable.EndUpdate();

                        file.Dispose();

                        txbCurrentFile.Text = openFileDialog.FileName;
                    }
                    catch(Exception ex)
                    {
                        GlobalVar.Log.Write(DICT["INIErrorMessageImport"] + ex.Message);
                    }
                }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
