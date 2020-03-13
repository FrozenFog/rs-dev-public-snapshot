namespace RelertSharp.SubWindows
{
    partial class INIComparator
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
            this.txbINIAPath = new System.Windows.Forms.TextBox();
            this.txbINIBPath = new System.Windows.Forms.TextBox();
            this.btnSelectPath1 = new System.Windows.Forms.Button();
            this.btnSelectPath2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnRunCompare = new System.Windows.Forms.Button();
            this.ckbIgnoreRemoved = new System.Windows.Forms.CheckBox();
            this.ckbIgnoreNew = new System.Windows.Forms.CheckBox();
            this.ckbCalculateR = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // txbINIAPath
            // 
            this.txbINIAPath.AllowDrop = true;
            this.txbINIAPath.Location = new System.Drawing.Point(107, 44);
            this.txbINIAPath.Name = "txbINIAPath";
            this.txbINIAPath.Size = new System.Drawing.Size(344, 21);
            this.txbINIAPath.TabIndex = 0;
            // 
            // txbINIBPath
            // 
            this.txbINIBPath.Location = new System.Drawing.Point(107, 94);
            this.txbINIBPath.Name = "txbINIBPath";
            this.txbINIBPath.Size = new System.Drawing.Size(344, 21);
            this.txbINIBPath.TabIndex = 0;
            // 
            // btnSelectPath1
            // 
            this.btnSelectPath1.Location = new System.Drawing.Point(27, 43);
            this.btnSelectPath1.Name = "btnSelectPath1";
            this.btnSelectPath1.Size = new System.Drawing.Size(74, 21);
            this.btnSelectPath1.TabIndex = 1;
            this.btnSelectPath1.Text = "CMPbtnSetPath";
            this.btnSelectPath1.UseVisualStyleBackColor = true;
            this.btnSelectPath1.Click += new System.EventHandler(this.btnSelectPath1_Click);
            // 
            // btnSelectPath2
            // 
            this.btnSelectPath2.Location = new System.Drawing.Point(27, 93);
            this.btnSelectPath2.Name = "btnSelectPath2";
            this.btnSelectPath2.Size = new System.Drawing.Size(74, 21);
            this.btnSelectPath2.TabIndex = 1;
            this.btnSelectPath2.Text = "CMPbtnSetPath";
            this.btnSelectPath2.UseVisualStyleBackColor = true;
            this.btnSelectPath2.Click += new System.EventHandler(this.btnSelectPath2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(105, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "CMPlblPrevious";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(105, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "CMPlblUpdated";
            // 
            // btnRunCompare
            // 
            this.btnRunCompare.Location = new System.Drawing.Point(375, 174);
            this.btnRunCompare.Name = "btnRunCompare";
            this.btnRunCompare.Size = new System.Drawing.Size(76, 21);
            this.btnRunCompare.TabIndex = 3;
            this.btnRunCompare.Text = "CMPbtnRunCompare";
            this.btnRunCompare.UseVisualStyleBackColor = true;
            this.btnRunCompare.Click += new System.EventHandler(this.btnRunCompare_Click);
            // 
            // ckbIgnoreRemoved
            // 
            this.ckbIgnoreRemoved.AutoSize = true;
            this.ckbIgnoreRemoved.Location = new System.Drawing.Point(107, 121);
            this.ckbIgnoreRemoved.Name = "ckbIgnoreRemoved";
            this.ckbIgnoreRemoved.Size = new System.Drawing.Size(138, 16);
            this.ckbIgnoreRemoved.TabIndex = 4;
            this.ckbIgnoreRemoved.Text = "CMPckbIgnoreRemoved";
            this.ckbIgnoreRemoved.UseVisualStyleBackColor = true;
            // 
            // ckbIgnoreNew
            // 
            this.ckbIgnoreNew.AutoSize = true;
            this.ckbIgnoreNew.Location = new System.Drawing.Point(107, 143);
            this.ckbIgnoreNew.Name = "ckbIgnoreNew";
            this.ckbIgnoreNew.Size = new System.Drawing.Size(114, 16);
            this.ckbIgnoreNew.TabIndex = 4;
            this.ckbIgnoreNew.Text = "CMPckbIgnoreNew";
            this.ckbIgnoreNew.UseVisualStyleBackColor = true;
            // 
            // ckbCalculateR
            // 
            this.ckbCalculateR.AutoSize = true;
            this.ckbCalculateR.Location = new System.Drawing.Point(107, 165);
            this.ckbCalculateR.Name = "ckbCalculateR";
            this.ckbCalculateR.Size = new System.Drawing.Size(132, 16);
            this.ckbCalculateR.TabIndex = 4;
            this.ckbCalculateR.Text = "CMPckbCalcRelative";
            this.ckbCalculateR.UseVisualStyleBackColor = true;
            // 
            // INIComparator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(472, 218);
            this.Controls.Add(this.ckbIgnoreNew);
            this.Controls.Add(this.ckbCalculateR);
            this.Controls.Add(this.ckbIgnoreRemoved);
            this.Controls.Add(this.btnRunCompare);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSelectPath2);
            this.Controls.Add(this.btnSelectPath1);
            this.Controls.Add(this.txbINIBPath);
            this.Controls.Add(this.txbINIAPath);
            this.MaximizeBox = false;
            this.Name = "INIComparator";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "CMPTitle";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txbINIAPath;
        private System.Windows.Forms.TextBox txbINIBPath;
        private System.Windows.Forms.Button btnSelectPath1;
        private System.Windows.Forms.Button btnSelectPath2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnRunCompare;
        private System.Windows.Forms.CheckBox ckbIgnoreRemoved;
        private System.Windows.Forms.CheckBox ckbIgnoreNew;
        private System.Windows.Forms.CheckBox ckbCalculateR;
    }
}