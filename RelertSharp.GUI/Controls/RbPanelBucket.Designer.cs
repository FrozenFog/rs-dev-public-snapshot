namespace RelertSharp.GUI.Controls
{
    partial class RbPanelBucket
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.cbbFillBy = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gpbMain = new System.Windows.Forms.GroupBox();
            this.gpbMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbbFillBy
            // 
            this.cbbFillBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbFillBy.FormattingEnabled = true;
            this.cbbFillBy.Location = new System.Drawing.Point(120, 38);
            this.cbbFillBy.Name = "cbbFillBy";
            this.cbbFillBy.Size = new System.Drawing.Size(121, 23);
            this.cbbFillBy.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Fill With:";
            // 
            // gpbMain
            // 
            this.gpbMain.Controls.Add(this.label1);
            this.gpbMain.Controls.Add(this.cbbFillBy);
            this.gpbMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gpbMain.Location = new System.Drawing.Point(0, 0);
            this.gpbMain.Name = "gpbMain";
            this.gpbMain.Size = new System.Drawing.Size(268, 86);
            this.gpbMain.TabIndex = 2;
            this.gpbMain.TabStop = false;
            this.gpbMain.Text = "Bucket Control";
            // 
            // RbPanelBucket
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.gpbMain);
            this.Name = "RbPanelBucket";
            this.Size = new System.Drawing.Size(268, 86);
            this.gpbMain.ResumeLayout(false);
            this.gpbMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbbFillBy;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gpbMain;
    }
}
