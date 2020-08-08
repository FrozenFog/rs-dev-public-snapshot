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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(INIEditor));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cmsSections = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiSectionInsert = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSectionRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSectionRename = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsKeys = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiKeyInsert = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiKeyRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.btnImport = new System.Windows.Forms.ToolStripButton();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.tbcINI = new System.Windows.Forms.TabControl();
            this.tbpStandard = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.gpbSections = new System.Windows.Forms.GroupBox();
            this.lbxSections = new System.Windows.Forms.ListBox();
            this.gpbKeys = new System.Windows.Forms.GroupBox();
            this.dgvKeys = new System.Windows.Forms.DataGridView();
            this.dgvColHeaderKey = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvColHeaderValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbpRaw = new System.Windows.Forms.TabPage();
            this.reHolder = new System.Windows.Forms.Integration.ElementHost();
            this.reMain = new RelertSharp.SubWindows.INIEditor.RawEditor();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.tspMenu = new System.Windows.Forms.ToolStrip();
            this.tsbReload = new System.Windows.Forms.ToolStripButton();
            this.stsINI = new System.Windows.Forms.StatusStrip();
            this.stsSectionInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.cmsSections.SuspendLayout();
            this.cmsKeys.SuspendLayout();
            this.tbcINI.SuspendLayout();
            this.tbpStandard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.gpbSections.SuspendLayout();
            this.gpbKeys.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKeys)).BeginInit();
            this.tbpRaw.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tspMenu.SuspendLayout();
            this.stsINI.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmsSections
            // 
            this.cmsSections.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiSectionInsert,
            this.tsmiSectionRemove,
            this.tsmiSectionRename});
            this.cmsSections.Name = "cmsSections";
            this.cmsSections.Size = new System.Drawing.Size(253, 70);
            // 
            // tsmiSectionInsert
            // 
            this.tsmiSectionInsert.Name = "tsmiSectionInsert";
            this.tsmiSectionInsert.ShortcutKeys = System.Windows.Forms.Keys.Insert;
            this.tsmiSectionInsert.Size = new System.Drawing.Size(252, 22);
            this.tsmiSectionInsert.Text = "INItsmiSectionInsert";
            this.tsmiSectionInsert.Click += new System.EventHandler(this.tsmiSectionInsert_Click);
            // 
            // tsmiSectionRemove
            // 
            this.tsmiSectionRemove.Name = "tsmiSectionRemove";
            this.tsmiSectionRemove.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.tsmiSectionRemove.Size = new System.Drawing.Size(252, 22);
            this.tsmiSectionRemove.Text = "INItsmiSectionRemove";
            this.tsmiSectionRemove.Click += new System.EventHandler(this.tsmiSectionRemove_Click);
            // 
            // tsmiSectionRename
            // 
            this.tsmiSectionRename.Name = "tsmiSectionRename";
            this.tsmiSectionRename.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.tsmiSectionRename.Size = new System.Drawing.Size(252, 22);
            this.tsmiSectionRename.Text = "INItsmiSectionRename";
            this.tsmiSectionRename.Click += new System.EventHandler(this.tsmiSectionRename_Click);
            // 
            // cmsKeys
            // 
            this.cmsKeys.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiKeyInsert,
            this.tsmiKeyRemove});
            this.cmsKeys.Name = "cmsKeys";
            this.cmsKeys.Size = new System.Drawing.Size(232, 48);
            // 
            // tsmiKeyInsert
            // 
            this.tsmiKeyInsert.Name = "tsmiKeyInsert";
            this.tsmiKeyInsert.ShortcutKeys = System.Windows.Forms.Keys.Insert;
            this.tsmiKeyInsert.Size = new System.Drawing.Size(231, 22);
            this.tsmiKeyInsert.Text = "INItsmiKeyInsert";
            this.tsmiKeyInsert.Click += new System.EventHandler(this.tsmiKeyInsert_Click);
            // 
            // tsmiKeyRemove
            // 
            this.tsmiKeyRemove.Name = "tsmiKeyRemove";
            this.tsmiKeyRemove.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.tsmiKeyRemove.Size = new System.Drawing.Size(231, 22);
            this.tsmiKeyRemove.Text = "INItsmiKeyRemove";
            this.tsmiKeyRemove.Click += new System.EventHandler(this.tsmiKeyRemove_Click);
            // 
            // btnImport
            // 
            this.btnImport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnImport.Image = ((System.Drawing.Image)(resources.GetObject("btnImport.Image")));
            this.btnImport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(23, 22);
            this.btnImport.Text = "INItsbImport";
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnSave
            // 
            this.btnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(23, 22);
            this.btnSave.Text = "INItsbSave";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // tbcINI
            // 
            this.tbcINI.Controls.Add(this.tbpStandard);
            this.tbcINI.Controls.Add(this.tbpRaw);
            this.tbcINI.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbcINI.Location = new System.Drawing.Point(0, 0);
            this.tbcINI.Name = "tbcINI";
            this.tbcINI.SelectedIndex = 0;
            this.tbcINI.Size = new System.Drawing.Size(800, 391);
            this.tbcINI.TabIndex = 0;
            this.tbcINI.SelectedIndexChanged += new System.EventHandler(this.tbcINI_SelectedIndexChanged);
            // 
            // tbpStandard
            // 
            this.tbpStandard.Controls.Add(this.splitContainer1);
            this.tbpStandard.Location = new System.Drawing.Point(4, 22);
            this.tbpStandard.Name = "tbpStandard";
            this.tbpStandard.Padding = new System.Windows.Forms.Padding(3);
            this.tbpStandard.Size = new System.Drawing.Size(792, 365);
            this.tbpStandard.TabIndex = 0;
            this.tbpStandard.Text = "INItbpStandard";
            this.tbpStandard.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.gpbSections);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.gpbKeys);
            this.splitContainer1.Size = new System.Drawing.Size(786, 359);
            this.splitContainer1.SplitterDistance = 127;
            this.splitContainer1.TabIndex = 0;
            // 
            // gpbSections
            // 
            this.gpbSections.AutoSize = true;
            this.gpbSections.Controls.Add(this.lbxSections);
            this.gpbSections.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gpbSections.Location = new System.Drawing.Point(0, 0);
            this.gpbSections.Name = "gpbSections";
            this.gpbSections.Size = new System.Drawing.Size(127, 359);
            this.gpbSections.TabIndex = 1;
            this.gpbSections.TabStop = false;
            this.gpbSections.Text = "INIgpbSections";
            // 
            // lbxSections
            // 
            this.lbxSections.ContextMenuStrip = this.cmsSections;
            this.lbxSections.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbxSections.FormattingEnabled = true;
            this.lbxSections.ItemHeight = 12;
            this.lbxSections.Location = new System.Drawing.Point(3, 17);
            this.lbxSections.Name = "lbxSections";
            this.lbxSections.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbxSections.Size = new System.Drawing.Size(121, 339);
            this.lbxSections.TabIndex = 0;
            this.lbxSections.SelectedIndexChanged += new System.EventHandler(this.lbxSections_SelectedIndexChanged);
            // 
            // gpbKeys
            // 
            this.gpbKeys.Controls.Add(this.dgvKeys);
            this.gpbKeys.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gpbKeys.Location = new System.Drawing.Point(0, 0);
            this.gpbKeys.Name = "gpbKeys";
            this.gpbKeys.Size = new System.Drawing.Size(655, 359);
            this.gpbKeys.TabIndex = 0;
            this.gpbKeys.TabStop = false;
            this.gpbKeys.Text = "INIgpbKeys";
            // 
            // dgvKeys
            // 
            this.dgvKeys.AllowUserToAddRows = false;
            this.dgvKeys.AllowUserToDeleteRows = false;
            this.dgvKeys.AllowUserToResizeRows = false;
            this.dgvKeys.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.dgvKeys.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.dgvKeys.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvKeys.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvKeys.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvKeys.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvColHeaderKey,
            this.dgvColHeaderValue});
            this.dgvKeys.ContextMenuStrip = this.cmsKeys;
            this.dgvKeys.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvKeys.Location = new System.Drawing.Point(3, 17);
            this.dgvKeys.Name = "dgvKeys";
            this.dgvKeys.RowHeadersVisible = false;
            this.dgvKeys.RowTemplate.Height = 23;
            this.dgvKeys.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvKeys.Size = new System.Drawing.Size(649, 339);
            this.dgvKeys.TabIndex = 0;
            this.dgvKeys.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvKeys_CellValueChanged);
            // 
            // dgvColHeaderKey
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvColHeaderKey.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvColHeaderKey.HeaderText = "INIdgvColHeaderKey";
            this.dgvColHeaderKey.Name = "dgvColHeaderKey";
            this.dgvColHeaderKey.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgvColHeaderKey.Width = 280;
            // 
            // dgvColHeaderValue
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvColHeaderValue.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvColHeaderValue.HeaderText = "INIdgvColHeaderValue";
            this.dgvColHeaderValue.Name = "dgvColHeaderValue";
            this.dgvColHeaderValue.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgvColHeaderValue.Width = 360;
            // 
            // tbpRaw
            // 
            this.tbpRaw.Controls.Add(this.reHolder);
            this.tbpRaw.Location = new System.Drawing.Point(4, 22);
            this.tbpRaw.Name = "tbpRaw";
            this.tbpRaw.Padding = new System.Windows.Forms.Padding(3);
            this.tbpRaw.Size = new System.Drawing.Size(792, 365);
            this.tbpRaw.TabIndex = 1;
            this.tbpRaw.Text = "INItbpRaw";
            this.tbpRaw.UseVisualStyleBackColor = true;
            // 
            // reHolder
            // 
            this.reHolder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reHolder.Location = new System.Drawing.Point(3, 3);
            this.reHolder.Name = "reHolder";
            this.reHolder.Size = new System.Drawing.Size(786, 359);
            this.reHolder.TabIndex = 0;
            this.reHolder.Text = "INIreHolder";
            this.reHolder.Child = this.reMain;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.tspMenu);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.tbcINI);
            this.splitContainer2.Size = new System.Drawing.Size(800, 420);
            this.splitContainer2.SplitterDistance = 25;
            this.splitContainer2.TabIndex = 2;
            this.splitContainer2.TabStop = false;
            // 
            // tspMenu
            // 
            this.tspMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tspMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tspMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnImport,
            this.btnSave,
            this.tsbReload});
            this.tspMenu.Location = new System.Drawing.Point(0, 0);
            this.tspMenu.Name = "tspMenu";
            this.tspMenu.Size = new System.Drawing.Size(800, 25);
            this.tspMenu.TabIndex = 1;
            this.tspMenu.Text = "toolStrip1";
            // 
            // tsbReload
            // 
            this.tsbReload.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbReload.Image = ((System.Drawing.Image)(resources.GetObject("tsbReload.Image")));
            this.tsbReload.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbReload.Name = "tsbReload";
            this.tsbReload.Size = new System.Drawing.Size(23, 22);
            this.tsbReload.Text = "INItsbReload";
            this.tsbReload.Click += new System.EventHandler(this.tsbReload_Click);
            // 
            // stsINI
            // 
            this.stsINI.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stsINI.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stsSectionInfo});
            this.stsINI.Location = new System.Drawing.Point(0, 0);
            this.stsINI.Name = "stsINI";
            this.stsINI.Size = new System.Drawing.Size(800, 26);
            this.stsINI.TabIndex = 3;
            this.stsINI.Text = "INIstsINI";
            // 
            // stsSectionInfo
            // 
            this.stsSectionInfo.Name = "stsSectionInfo";
            this.stsSectionInfo.Size = new System.Drawing.Size(0, 21);
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer3.IsSplitterFixed = true;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.stsINI);
            this.splitContainer3.Size = new System.Drawing.Size(800, 450);
            this.splitContainer3.SplitterDistance = 420;
            this.splitContainer3.TabIndex = 4;
            this.splitContainer3.TabStop = false;
            // 
            // INIEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.splitContainer3);
            this.Name = "INIEditor";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "INITitle";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.INIEditor_FormClosing);
            this.ResizeBegin += INIEditor_ResizeBegin;
            this.ResizeEnd += INIEditor_ResizeEnd;
            this.cmsSections.ResumeLayout(false);
            this.cmsKeys.ResumeLayout(false);
            this.tbcINI.ResumeLayout(false);
            this.tbpStandard.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.gpbSections.ResumeLayout(false);
            this.gpbKeys.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvKeys)).EndInit();
            this.tbpRaw.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.tspMenu.ResumeLayout(false);
            this.tspMenu.PerformLayout();
            this.stsINI.ResumeLayout(false);
            this.stsINI.PerformLayout();
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip cmsSections;
        private System.Windows.Forms.ToolStripMenuItem tsmiSectionInsert;
        private System.Windows.Forms.ToolStripMenuItem tsmiSectionRemove;
        private System.Windows.Forms.ContextMenuStrip cmsKeys;
        private System.Windows.Forms.ToolStripMenuItem tsmiKeyInsert;
        private System.Windows.Forms.ToolStripMenuItem tsmiKeyRemove;
        private System.Windows.Forms.ToolStripButton btnImport;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.TabControl tbcINI;
        private System.Windows.Forms.TabPage tbpStandard;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox gpbSections;
        private System.Windows.Forms.ListBox lbxSections;
        private System.Windows.Forms.GroupBox gpbKeys;
        private System.Windows.Forms.DataGridView dgvKeys;
        private System.Windows.Forms.TabPage tbpRaw;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ToolStrip tspMenu;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvColHeaderKey;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvColHeaderValue;
        private System.Windows.Forms.StatusStrip stsINI;
        private System.Windows.Forms.ToolStripStatusLabel stsSectionInfo;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.ToolStripMenuItem tsmiSectionRename;
        private System.Windows.Forms.ToolStripButton tsbReload;
        private RelertSharp.SubWindows.INIEditor.RawEditor codeEditor1;
        private System.Windows.Forms.Integration.ElementHost reHolder;
        private RawEditor reMain;
    }
}