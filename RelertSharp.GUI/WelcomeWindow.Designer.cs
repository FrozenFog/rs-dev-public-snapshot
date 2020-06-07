namespace RelertSharp.GUI
{
    partial class WelcomeWindow
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
            this.rtxbDetail = new System.Windows.Forms.RichTextBox();
            this.btnLoadMap = new System.Windows.Forms.Button();
            this.btnChangeCfg = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "RSWLblDetail";
            // 
            // rtxbDetail
            // 
            this.rtxbDetail.Location = new System.Drawing.Point(37, 46);
            this.rtxbDetail.Name = "rtxbDetail";
            this.rtxbDetail.ReadOnly = true;
            this.rtxbDetail.Size = new System.Drawing.Size(402, 205);
            this.rtxbDetail.TabIndex = 1;
            this.rtxbDetail.Text = "";
            // 
            // btnLoadMap
            // 
            this.btnLoadMap.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnLoadMap.Location = new System.Drawing.Point(37, 257);
            this.btnLoadMap.Name = "btnLoadMap";
            this.btnLoadMap.Size = new System.Drawing.Size(130, 28);
            this.btnLoadMap.TabIndex = 2;
            this.btnLoadMap.Text = "RSWBtnLoad";
            this.btnLoadMap.UseVisualStyleBackColor = true;
            this.btnLoadMap.Click += new System.EventHandler(this.btnLoadMap_Click);
            // 
            // btnChangeCfg
            // 
            this.btnChangeCfg.Location = new System.Drawing.Point(173, 257);
            this.btnChangeCfg.Name = "btnChangeCfg";
            this.btnChangeCfg.Size = new System.Drawing.Size(130, 28);
            this.btnChangeCfg.TabIndex = 2;
            this.btnChangeCfg.Text = "RSWBtnChange";
            this.btnChangeCfg.UseVisualStyleBackColor = true;
            this.btnChangeCfg.Click += new System.EventHandler(this.btnChangeCfg_Click);
            // 
            // btnExit
            // 
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExit.Location = new System.Drawing.Point(309, 257);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(130, 28);
            this.btnExit.TabIndex = 2;
            this.btnExit.Text = "RSExit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // WelcomeWindow
            // 
            this.AcceptButton = this.btnLoadMap;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(487, 324);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnChangeCfg);
            this.Controls.Add(this.btnLoadMap);
            this.Controls.Add(this.rtxbDetail);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "WelcomeWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RSMainTitle";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox rtxbDetail;
        private System.Windows.Forms.Button btnLoadMap;
        private System.Windows.Forms.Button btnChangeCfg;
        private System.Windows.Forms.Button btnExit;
    }
}