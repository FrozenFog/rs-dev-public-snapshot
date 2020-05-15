namespace RelertSharp.SubWindows.INIEditor
{
    partial class INIEditor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.gpbSections = new System.Windows.Forms.GroupBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.lbxSectionList = new System.Windows.Forms.ListBox();
            this.txbSectionSearch = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnDelSection = new System.Windows.Forms.Button();
            this.btnNewSection = new System.Windows.Forms.Button();
            this.gpbKeys = new System.Windows.Forms.GroupBox();
            this.dgvKeys = new System.Windows.Forms.DataGridView();
            this.lbxKeyList = new System.Windows.Forms.ListBox();
            this.rtxbKeyDesc = new System.Windows.Forms.RichTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnNewKey = new System.Windows.Forms.Button();
            this.btnDelKey = new System.Windows.Forms.Button();
            this.txbKey = new System.Windows.Forms.TextBox();
            this.txbValue = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.gpbSections.SuspendLayout();
            this.gpbKeys.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKeys)).BeginInit();
            this.SuspendLayout();
            // 
            // gpbSections
            // 
            this.gpbSections.BackColor = System.Drawing.Color.Transparent;
            this.gpbSections.Controls.Add(this.btnSearch);
            this.gpbSections.Controls.Add(this.lbxSectionList);
            this.gpbSections.Controls.Add(this.txbSectionSearch);
            this.gpbSections.Controls.Add(this.label1);
            this.gpbSections.Controls.Add(this.btnDelSection);
            this.gpbSections.Controls.Add(this.btnNewSection);
            this.gpbSections.Location = new System.Drawing.Point(12, 12);
            this.gpbSections.Name = "gpbSections";
            this.gpbSections.Size = new System.Drawing.Size(255, 657);
            this.gpbSections.TabIndex = 0;
            this.gpbSections.TabStop = false;
            this.gpbSections.Text = "INIgpbSections";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(185, 61);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(64, 21);
            this.btnSearch.TabIndex = 5;
            this.btnSearch.Text = "INIbtnSearch";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // lbxSectionList
            // 
            this.lbxSectionList.FormattingEnabled = true;
            this.lbxSectionList.ItemHeight = 12;
            this.lbxSectionList.Location = new System.Drawing.Point(5, 88);
            this.lbxSectionList.Name = "lbxSectionList";
            this.lbxSectionList.Size = new System.Drawing.Size(244, 556);
            this.lbxSectionList.TabIndex = 4;
            this.lbxSectionList.SelectedIndexChanged += new System.EventHandler(this.lbxSectionList_SelectedIndexChanged);
            // 
            // txbSectionSearch
            // 
            this.txbSectionSearch.ForeColor = System.Drawing.SystemColors.GrayText;
            this.txbSectionSearch.Location = new System.Drawing.Point(6, 61);
            this.txbSectionSearch.Name = "txbSectionSearch";
            this.txbSectionSearch.Size = new System.Drawing.Size(173, 21);
            this.txbSectionSearch.TabIndex = 3;
            this.txbSectionSearch.Text = "INIlblFakeSearch";
            this.txbSectionSearch.Enter += new System.EventHandler(this.txbSectionSearch_Enter);
            this.txbSectionSearch.Leave += new System.EventHandler(this.txbSectionSearch_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "INIlblSectionSearch";
            // 
            // btnDelSection
            // 
            this.btnDelSection.Location = new System.Drawing.Point(136, 20);
            this.btnDelSection.Name = "btnDelSection";
            this.btnDelSection.Size = new System.Drawing.Size(113, 23);
            this.btnDelSection.TabIndex = 1;
            this.btnDelSection.Text = "INIbtnDelSection";
            this.btnDelSection.UseVisualStyleBackColor = true;
            this.btnDelSection.Click += new System.EventHandler(this.btnDelSection_Click);
            // 
            // btnNewSection
            // 
            this.btnNewSection.Location = new System.Drawing.Point(6, 20);
            this.btnNewSection.Name = "btnNewSection";
            this.btnNewSection.Size = new System.Drawing.Size(113, 23);
            this.btnNewSection.TabIndex = 0;
            this.btnNewSection.Text = "INIbtnNewSection";
            this.btnNewSection.UseVisualStyleBackColor = true;
            this.btnNewSection.Click += new System.EventHandler(this.btnNewSection_Click);
            // 
            // gpbKeys
            // 
            this.gpbKeys.BackColor = System.Drawing.Color.Transparent;
            this.gpbKeys.Controls.Add(this.dgvKeys);
            this.gpbKeys.Controls.Add(this.lbxKeyList);
            this.gpbKeys.Controls.Add(this.rtxbKeyDesc);
            this.gpbKeys.Controls.Add(this.label4);
            this.gpbKeys.Controls.Add(this.btnNewKey);
            this.gpbKeys.Controls.Add(this.btnDelKey);
            this.gpbKeys.Controls.Add(this.txbKey);
            this.gpbKeys.Controls.Add(this.txbValue);
            this.gpbKeys.Controls.Add(this.label3);
            this.gpbKeys.Controls.Add(this.label2);
            this.gpbKeys.Location = new System.Drawing.Point(273, 12);
            this.gpbKeys.Name = "gpbKeys";
            this.gpbKeys.Size = new System.Drawing.Size(979, 657);
            this.gpbKeys.TabIndex = 1;
            this.gpbKeys.TabStop = false;
            this.gpbKeys.Text = "INIgpbKeys";
            // 
            // dgvKeys
            // 
            this.dgvKeys.AllowUserToAddRows = false;
            this.dgvKeys.AllowUserToDeleteRows = false;
            this.dgvKeys.AllowUserToResizeColumns = false;
            this.dgvKeys.AllowUserToResizeRows = false;
            this.dgvKeys.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvKeys.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvKeys.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvKeys.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvKeys.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.dgvKeys.Location = new System.Drawing.Point(14, 20);
            this.dgvKeys.MultiSelect = false;
            this.dgvKeys.Name = "dgvKeys";
            this.dgvKeys.RowHeadersVisible = false;
            this.dgvKeys.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvKeys.RowTemplate.Height = 23;
            this.dgvKeys.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvKeys.Size = new System.Drawing.Size(959, 434);
            this.dgvKeys.TabIndex = 25;
            this.dgvKeys.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(this.dgvKeys_RowStateChanged);
            // 
            // lbxKeyList
            // 
            this.lbxKeyList.FormattingEnabled = true;
            this.lbxKeyList.ItemHeight = 12;
            this.lbxKeyList.Location = new System.Drawing.Point(14, 508);
            this.lbxKeyList.Name = "lbxKeyList";
            this.lbxKeyList.Size = new System.Drawing.Size(228, 136);
            this.lbxKeyList.TabIndex = 23;
            // 
            // rtxbKeyDesc
            // 
            this.rtxbKeyDesc.Enabled = false;
            this.rtxbKeyDesc.Font = new System.Drawing.Font("Verdana", 9F);
            this.rtxbKeyDesc.Location = new System.Drawing.Point(254, 508);
            this.rtxbKeyDesc.Name = "rtxbKeyDesc";
            this.rtxbKeyDesc.ReadOnly = true;
            this.rtxbKeyDesc.Size = new System.Drawing.Size(719, 136);
            this.rtxbKeyDesc.TabIndex = 24;
            this.rtxbKeyDesc.Text = "";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 489);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 12);
            this.label4.TabIndex = 21;
            this.label4.Text = "INIlblKeyUseable";
            // 
            // btnNewKey
            // 
            this.btnNewKey.Location = new System.Drawing.Point(14, 463);
            this.btnNewKey.Name = "btnNewKey";
            this.btnNewKey.Size = new System.Drawing.Size(111, 23);
            this.btnNewKey.TabIndex = 16;
            this.btnNewKey.Text = "INIbtnNewKey";
            this.btnNewKey.UseVisualStyleBackColor = true;
            this.btnNewKey.Click += new System.EventHandler(this.btnNewKey_Click);
            // 
            // btnDelKey
            // 
            this.btnDelKey.Location = new System.Drawing.Point(131, 463);
            this.btnDelKey.Name = "btnDelKey";
            this.btnDelKey.Size = new System.Drawing.Size(111, 23);
            this.btnDelKey.TabIndex = 20;
            this.btnDelKey.Text = "INIbtnDelKey";
            this.btnDelKey.UseVisualStyleBackColor = true;
            this.btnDelKey.Click += new System.EventHandler(this.btnDelKey_Click);
            // 
            // txbKey
            // 
            this.txbKey.Location = new System.Drawing.Point(254, 463);
            this.txbKey.Name = "txbKey";
            this.txbKey.Size = new System.Drawing.Size(263, 21);
            this.txbKey.TabIndex = 17;
            // 
            // txbValue
            // 
            this.txbValue.Location = new System.Drawing.Point(540, 463);
            this.txbValue.Name = "txbValue";
            this.txbValue.Size = new System.Drawing.Size(433, 21);
            this.txbValue.TabIndex = 19;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(523, 466);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(11, 12);
            this.label3.TabIndex = 18;
            this.label3.Text = "=";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(255, 493);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 12);
            this.label2.TabIndex = 22;
            this.label2.Text = "INIlblKeyDesc";
            // 
            // INIEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.gpbKeys);
            this.Controls.Add(this.gpbSections);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "INIEditor";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "INITitle";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.INIEditor_FormClosing);
            this.gpbSections.ResumeLayout(false);
            this.gpbSections.PerformLayout();
            this.gpbKeys.ResumeLayout(false);
            this.gpbKeys.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKeys)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gpbSections;
        private System.Windows.Forms.Button btnDelSection;
        private System.Windows.Forms.Button btnNewSection;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txbSectionSearch;
        private System.Windows.Forms.ListBox lbxSectionList;
        private System.Windows.Forms.GroupBox gpbKeys;
        private System.Windows.Forms.ListBox lbxKeyList;
        private System.Windows.Forms.RichTextBox rtxbKeyDesc;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnNewKey;
        private System.Windows.Forms.Button btnDelKey;
        private System.Windows.Forms.TextBox txbKey;
        private System.Windows.Forms.TextBox txbValue;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridView dgvKeys;
    }
}