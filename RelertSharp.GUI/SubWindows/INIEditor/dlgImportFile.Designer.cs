namespace RelertSharp.SubWindows.INIEditor
{
    partial class dlgImportFile
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
            this.lbxAvailable = new System.Windows.Forms.ListBox();
            this.lbxSelected = new System.Windows.Forms.ListBox();
            this.btnSelect = new System.Windows.Forms.Button();
            this.btnRelease = new System.Windows.Forms.Button();
            this.btnLoadFile = new System.Windows.Forms.Button();
            this.lblCurrentFile = new System.Windows.Forms.Label();
            this.txbCurrentFile = new System.Windows.Forms.TextBox();
            this.lblImportDesc = new System.Windows.Forms.Label();
            this.btnImport = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbxAvailable
            // 
            this.lbxAvailable.FormattingEnabled = true;
            this.lbxAvailable.ItemHeight = 12;
            this.lbxAvailable.Location = new System.Drawing.Point(12, 41);
            this.lbxAvailable.Name = "lbxAvailable";
            this.lbxAvailable.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbxAvailable.Size = new System.Drawing.Size(239, 328);
            this.lbxAvailable.Sorted = true;
            this.lbxAvailable.TabIndex = 0;
            // 
            // lbxSelected
            // 
            this.lbxSelected.FormattingEnabled = true;
            this.lbxSelected.ItemHeight = 12;
            this.lbxSelected.Location = new System.Drawing.Point(300, 41);
            this.lbxSelected.Name = "lbxSelected";
            this.lbxSelected.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbxSelected.Size = new System.Drawing.Size(239, 328);
            this.lbxSelected.Sorted = true;
            this.lbxSelected.TabIndex = 1;
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(257, 162);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(37, 23);
            this.btnSelect.TabIndex = 2;
            this.btnSelect.Text = "->";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // btnRelease
            // 
            this.btnRelease.Location = new System.Drawing.Point(257, 191);
            this.btnRelease.Name = "btnRelease";
            this.btnRelease.Size = new System.Drawing.Size(37, 23);
            this.btnRelease.TabIndex = 3;
            this.btnRelease.Text = "<-";
            this.btnRelease.UseVisualStyleBackColor = true;
            this.btnRelease.Click += new System.EventHandler(this.btnRelease_Click);
            // 
            // btnLoadFile
            // 
            this.btnLoadFile.Location = new System.Drawing.Point(508, 12);
            this.btnLoadFile.Name = "btnLoadFile";
            this.btnLoadFile.Size = new System.Drawing.Size(31, 23);
            this.btnLoadFile.TabIndex = 4;
            this.btnLoadFile.Text = "...";
            this.btnLoadFile.UseVisualStyleBackColor = true;
            this.btnLoadFile.Click += new System.EventHandler(this.btnLoadFile_Click);
            // 
            // lblCurrentFile
            // 
            this.lblCurrentFile.AutoSize = true;
            this.lblCurrentFile.Location = new System.Drawing.Point(12, 17);
            this.lblCurrentFile.Name = "lblCurrentFile";
            this.lblCurrentFile.Size = new System.Drawing.Size(107, 12);
            this.lblCurrentFile.TabIndex = 5;
            this.lblCurrentFile.Text = "INIlblCurrentFile";
            // 
            // txbCurrentFile
            // 
            this.txbCurrentFile.Location = new System.Drawing.Point(125, 12);
            this.txbCurrentFile.Name = "txbCurrentFile";
            this.txbCurrentFile.ReadOnly = true;
            this.txbCurrentFile.Size = new System.Drawing.Size(377, 21);
            this.txbCurrentFile.TabIndex = 6;
            // 
            // lblImportDesc
            // 
            this.lblImportDesc.AutoSize = true;
            this.lblImportDesc.Location = new System.Drawing.Point(12, 372);
            this.lblImportDesc.Name = "lblImportDesc";
            this.lblImportDesc.Size = new System.Drawing.Size(101, 12);
            this.lblImportDesc.TabIndex = 7;
            this.lblImportDesc.Text = "INIlblImportDesc";
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(464, 375);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(75, 23);
            this.btnImport.TabIndex = 8;
            this.btnImport.Text = "INIbtnImport";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // dlgImportFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(551, 403);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.lblImportDesc);
            this.Controls.Add(this.txbCurrentFile);
            this.Controls.Add(this.lblCurrentFile);
            this.Controls.Add(this.btnLoadFile);
            this.Controls.Add(this.btnRelease);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.lbxSelected);
            this.Controls.Add(this.lbxAvailable);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgImportFile";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "INIdlgImportFile";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbxAvailable;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Button btnRelease;
        private System.Windows.Forms.Button btnLoadFile;
        private System.Windows.Forms.Label lblCurrentFile;
        private System.Windows.Forms.TextBox txbCurrentFile;
        private System.Windows.Forms.Label lblImportDesc;
        private System.Windows.Forms.Button btnImport;
        public System.Windows.Forms.ListBox lbxSelected;
    }
}