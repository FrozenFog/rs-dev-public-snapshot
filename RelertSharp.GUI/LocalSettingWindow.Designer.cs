namespace RelertSharp.GUI
{
    partial class LocalSettingWindow
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txbGamePath = new System.Windows.Forms.TextBox();
            this.btnGamePath = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txbCfgPath = new System.Windows.Forms.TextBox();
            this.btnCfgPath = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.rtxbDetail = new System.Windows.Forms.RichTextBox();
            this.btnAccept = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "LcfgLbl00";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(127, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "LcfgLblGamePath";
            // 
            // txbGamePath
            // 
            this.txbGamePath.Location = new System.Drawing.Point(15, 111);
            this.txbGamePath.Name = "txbGamePath";
            this.txbGamePath.ReadOnly = true;
            this.txbGamePath.Size = new System.Drawing.Size(279, 25);
            this.txbGamePath.TabIndex = 2;
            // 
            // btnGamePath
            // 
            this.btnGamePath.Location = new System.Drawing.Point(300, 111);
            this.btnGamePath.Name = "btnGamePath";
            this.btnGamePath.Size = new System.Drawing.Size(75, 25);
            this.btnGamePath.TabIndex = 3;
            this.btnGamePath.Text = "LcfgBtnBrowse";
            this.btnGamePath.UseVisualStyleBackColor = true;
            this.btnGamePath.Click += new System.EventHandler(this.btnGamePath_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 139);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(119, 15);
            this.label3.TabIndex = 1;
            this.label3.Text = "LcfgLblCfgPath";
            // 
            // txbCfgPath
            // 
            this.txbCfgPath.Location = new System.Drawing.Point(15, 157);
            this.txbCfgPath.Name = "txbCfgPath";
            this.txbCfgPath.ReadOnly = true;
            this.txbCfgPath.Size = new System.Drawing.Size(279, 25);
            this.txbCfgPath.TabIndex = 2;
            // 
            // btnCfgPath
            // 
            this.btnCfgPath.Location = new System.Drawing.Point(300, 157);
            this.btnCfgPath.Name = "btnCfgPath";
            this.btnCfgPath.Size = new System.Drawing.Size(75, 25);
            this.btnCfgPath.TabIndex = 3;
            this.btnCfgPath.Text = "LcfgBtnBrowse";
            this.btnCfgPath.UseVisualStyleBackColor = true;
            this.btnCfgPath.Click += new System.EventHandler(this.btnCfgPath_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 185);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(135, 15);
            this.label4.TabIndex = 1;
            this.label4.Text = "LcfgLblCfgDetail";
            // 
            // rtxbDetail
            // 
            this.rtxbDetail.Location = new System.Drawing.Point(15, 203);
            this.rtxbDetail.Name = "rtxbDetail";
            this.rtxbDetail.ReadOnly = true;
            this.rtxbDetail.Size = new System.Drawing.Size(360, 149);
            this.rtxbDetail.TabIndex = 4;
            this.rtxbDetail.Text = "";
            // 
            // btnAccept
            // 
            this.btnAccept.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnAccept.Enabled = false;
            this.btnAccept.Location = new System.Drawing.Point(56, 367);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(91, 28);
            this.btnAccept.TabIndex = 5;
            this.btnAccept.Text = "DLGAcc";
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(242, 367);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(91, 28);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "DLGCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // LocalSettingWindow
            // 
            this.AcceptButton = this.btnAccept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(413, 409);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.rtxbDetail);
            this.Controls.Add(this.btnCfgPath);
            this.Controls.Add(this.btnGamePath);
            this.Controls.Add(this.txbCfgPath);
            this.Controls.Add(this.txbGamePath);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LocalSettingWindow";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "LcfgTitle";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txbGamePath;
        private System.Windows.Forms.Button btnGamePath;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txbCfgPath;
        private System.Windows.Forms.Button btnCfgPath;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RichTextBox rtxbDetail;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.Button btnCancel;
    }
}