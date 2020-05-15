namespace RelertSharp.GUI
{
    partial class LoadingWindow
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
            this.lblCurrentStatus = new System.Windows.Forms.Label();
            this.progMain = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // lblCurrentStatus
            // 
            this.lblCurrentStatus.AutoSize = true;
            this.lblCurrentStatus.Location = new System.Drawing.Point(41, 166);
            this.lblCurrentStatus.Name = "lblCurrentStatus";
            this.lblCurrentStatus.Size = new System.Drawing.Size(63, 15);
            this.lblCurrentStatus.TabIndex = 0;
            this.lblCurrentStatus.Text = "Current";
            // 
            // progMain
            // 
            this.progMain.Location = new System.Drawing.Point(44, 184);
            this.progMain.Name = "progMain";
            this.progMain.Size = new System.Drawing.Size(541, 26);
            this.progMain.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progMain.TabIndex = 1;
            // 
            // LoadingWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(617, 232);
            this.ControlBox = false;
            this.Controls.Add(this.progMain);
            this.Controls.Add(this.lblCurrentStatus);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoadingWindow";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Loading";
            this.TopMost = true;
            this.UseWaitCursor = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCurrentStatus;
        private System.Windows.Forms.ProgressBar progMain;
    }
}