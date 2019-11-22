namespace relert_sharp.SubWindows
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
            this.SuspendLayout();
            // 
            // txbINIAPath
            // 
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
            this.btnSelectPath1.Text = "Set Path";
            this.btnSelectPath1.UseVisualStyleBackColor = true;
            this.btnSelectPath1.Click += new System.EventHandler(this.btnSelectPath1_Click);
            // 
            // btnSelectPath2
            // 
            this.btnSelectPath2.Location = new System.Drawing.Point(27, 93);
            this.btnSelectPath2.Name = "btnSelectPath2";
            this.btnSelectPath2.Size = new System.Drawing.Size(74, 21);
            this.btnSelectPath2.TabIndex = 1;
            this.btnSelectPath2.Text = "Set Path";
            this.btnSelectPath2.UseVisualStyleBackColor = true;
            this.btnSelectPath2.Click += new System.EventHandler(this.btnSelectPath2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(105, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "INI Path A (Previous)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(105, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "INI Path B (Updated)";
            // 
            // btnRunCompare
            // 
            this.btnRunCompare.Location = new System.Drawing.Point(84, 143);
            this.btnRunCompare.Name = "btnRunCompare";
            this.btnRunCompare.Size = new System.Drawing.Size(76, 21);
            this.btnRunCompare.TabIndex = 3;
            this.btnRunCompare.Text = "Compare";
            this.btnRunCompare.UseVisualStyleBackColor = true;
            this.btnRunCompare.Click += new System.EventHandler(this.btnRunCompare_Click);
            // 
            // INIComparator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(472, 218);
            this.Controls.Add(this.btnRunCompare);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSelectPath2);
            this.Controls.Add(this.btnSelectPath1);
            this.Controls.Add(this.txbINIBPath);
            this.Controls.Add(this.txbINIAPath);
            this.MaximizeBox = false;
            this.Name = "INIComparator";
            this.ShowInTaskbar = false;
            this.Text = "INI Comparator";
            this.TopMost = true;
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
    }
}