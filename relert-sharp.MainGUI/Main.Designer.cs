namespace relert_sharp
{
    partial class Main
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnOpenINIComp = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnOpenINIComp
            // 
            this.btnOpenINIComp.Location = new System.Drawing.Point(42, 26);
            this.btnOpenINIComp.Name = "btnOpenINIComp";
            this.btnOpenINIComp.Size = new System.Drawing.Size(110, 24);
            this.btnOpenINIComp.TabIndex = 0;
            this.btnOpenINIComp.Text = "INI Comparator";
            this.btnOpenINIComp.UseVisualStyleBackColor = true;
            this.btnOpenINIComp.Click += new System.EventHandler(this.btnOpenINIComp_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(563, 455);
            this.Controls.Add(this.btnOpenINIComp);
            this.Name = "Main";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOpenINIComp;
    }
}

