namespace RelertSharp.SubWindows.INIEditor
{
    partial class dlgNewSection
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
            this.lblNewSection = new System.Windows.Forms.Label();
            this.txbNewSection = new System.Windows.Forms.TextBox();
            this.btnNewSectionC = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblNewSection
            // 
            this.lblNewSection.AutoSize = true;
            this.lblNewSection.Location = new System.Drawing.Point(12, 9);
            this.lblNewSection.Name = "lblNewSection";
            this.lblNewSection.Size = new System.Drawing.Size(101, 12);
            this.lblNewSection.TabIndex = 0;
            this.lblNewSection.Text = "INIlblNewSection";
            // 
            // txbNewSection
            // 
            this.txbNewSection.Location = new System.Drawing.Point(14, 24);
            this.txbNewSection.Name = "txbNewSection";
            this.txbNewSection.Size = new System.Drawing.Size(262, 21);
            this.txbNewSection.TabIndex = 1;
            // 
            // btnNewSectionC
            // 
            this.btnNewSectionC.Location = new System.Drawing.Point(282, 24);
            this.btnNewSectionC.Name = "btnNewSectionC";
            this.btnNewSectionC.Size = new System.Drawing.Size(75, 23);
            this.btnNewSectionC.TabIndex = 2;
            this.btnNewSectionC.Text = "INIbtnNewSectionC";
            this.btnNewSectionC.UseVisualStyleBackColor = true;
            this.btnNewSectionC.Click += new System.EventHandler(this.btnNewSectionC_Click);
            // 
            // dlgNewSection
            // 
            this.AcceptButton = this.btnNewSectionC;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(369, 56);
            this.Controls.Add(this.btnNewSectionC);
            this.Controls.Add(this.txbNewSection);
            this.Controls.Add(this.lblNewSection);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgNewSection";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "INIdlgNewSection";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblNewSection;
        private System.Windows.Forms.TextBox txbNewSection;
        private System.Windows.Forms.Button btnNewSectionC;
    }
}