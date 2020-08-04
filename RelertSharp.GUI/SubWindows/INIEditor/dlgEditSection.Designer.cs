namespace RelertSharp.SubWindows.INIEditor
{
    partial class dlgEditSection
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
            this.lblEditDesc = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txbSectionName = new System.Windows.Forms.TextBox();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblEditDesc
            // 
            this.lblEditDesc.AutoSize = true;
            this.lblEditDesc.Location = new System.Drawing.Point(12, 9);
            this.lblEditDesc.Name = "lblEditDesc";
            this.lblEditDesc.Size = new System.Drawing.Size(89, 12);
            this.lblEditDesc.TabIndex = 0;
            this.lblEditDesc.Text = "INIlblEditDesc";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(12, 51);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(103, 27);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "INIbtnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txbSectionName
            // 
            this.txbSectionName.Location = new System.Drawing.Point(12, 24);
            this.txbSectionName.Name = "txbSectionName";
            this.txbSectionName.Size = new System.Drawing.Size(225, 21);
            this.txbSectionName.TabIndex = 1;
            this.txbSectionName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txbSectionName_KeyDown);
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(134, 51);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(103, 27);
            this.btnConfirm.TabIndex = 3;
            this.btnConfirm.Text = "INIbtnConfirm";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // dlgEditSection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(249, 90);
            this.ControlBox = false;
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.txbSectionName);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblEditDesc);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgEditSection";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "INIdlgEditSection";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblEditDesc;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnConfirm;
        public System.Windows.Forms.TextBox txbSectionName;
    }
}