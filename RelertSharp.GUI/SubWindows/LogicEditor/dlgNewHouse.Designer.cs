namespace RelertSharp.GUI.SubWindows.LogicEditor
{
    partial class dlgNewHouse
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
            this.cbbParent = new System.Windows.Forms.ComboBox();
            this.txbName = new System.Windows.Forms.TextBox();
            this.btnHouseNewConfirm = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "LGClblHouseNewName";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "LGClblHouseNewParent";
            // 
            // cbbParent
            // 
            this.cbbParent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbParent.FormattingEnabled = true;
            this.cbbParent.Location = new System.Drawing.Point(12, 63);
            this.cbbParent.Name = "cbbParent";
            this.cbbParent.Size = new System.Drawing.Size(190, 20);
            this.cbbParent.TabIndex = 2;
            // 
            // txbName
            // 
            this.txbName.Location = new System.Drawing.Point(12, 24);
            this.txbName.Name = "txbName";
            this.txbName.Size = new System.Drawing.Size(190, 21);
            this.txbName.TabIndex = 3;
            // 
            // btnHouseNewConfirm
            // 
            this.btnHouseNewConfirm.Location = new System.Drawing.Point(14, 89);
            this.btnHouseNewConfirm.Name = "btnHouseNewConfirm";
            this.btnHouseNewConfirm.Size = new System.Drawing.Size(188, 23);
            this.btnHouseNewConfirm.TabIndex = 4;
            this.btnHouseNewConfirm.Text = "LGCbtnHouseNewConfirm";
            this.btnHouseNewConfirm.UseVisualStyleBackColor = true;
            this.btnHouseNewConfirm.Click += new System.EventHandler(this.btnHouseNewConfirm_Click);
            // 
            // dlgNewHouse
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(215, 116);
            this.Controls.Add(this.btnHouseNewConfirm);
            this.Controls.Add(this.txbName);
            this.Controls.Add(this.cbbParent);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgNewHouse";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "dlgNewHouse";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbbParent;
        private System.Windows.Forms.TextBox txbName;
        private System.Windows.Forms.Button btnHouseNewConfirm;
    }
}