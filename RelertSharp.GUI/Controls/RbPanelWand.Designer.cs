namespace RelertSharp.GUI.Controls
{
    partial class RbPanelWand
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
            this.gpbMain = new System.Windows.Forms.GroupBox();
            this.ckbSameIndex = new System.Windows.Forms.CheckBox();
            this.ckbSameSet = new System.Windows.Forms.CheckBox();
            this.ckbSameZ = new System.Windows.Forms.CheckBox();
            this.gpbMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // gpbMain
            // 
            this.gpbMain.Controls.Add(this.ckbSameIndex);
            this.gpbMain.Controls.Add(this.ckbSameSet);
            this.gpbMain.Controls.Add(this.ckbSameZ);
            this.gpbMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gpbMain.Location = new System.Drawing.Point(0, 0);
            this.gpbMain.Name = "gpbMain";
            this.gpbMain.Size = new System.Drawing.Size(273, 190);
            this.gpbMain.TabIndex = 0;
            this.gpbMain.TabStop = false;
            this.gpbMain.Text = "Wand control";
            // 
            // ckbSameIndex
            // 
            this.ckbSameIndex.AutoSize = true;
            this.ckbSameIndex.Location = new System.Drawing.Point(33, 143);
            this.ckbSameIndex.Name = "ckbSameIndex";
            this.ckbSameIndex.Size = new System.Drawing.Size(133, 19);
            this.ckbSameIndex.TabIndex = 0;
            this.ckbSameIndex.Text = "Same Subindex";
            this.ckbSameIndex.UseVisualStyleBackColor = true;
            // 
            // ckbSameSet
            // 
            this.ckbSameSet.AutoSize = true;
            this.ckbSameSet.Checked = true;
            this.ckbSameSet.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbSameSet.Location = new System.Drawing.Point(33, 93);
            this.ckbSameSet.Name = "ckbSameSet";
            this.ckbSameSet.Size = new System.Drawing.Size(229, 19);
            this.ckbSameSet.TabIndex = 0;
            this.ckbSameSet.Text = "Same Tileset (Tile index)";
            this.ckbSameSet.UseVisualStyleBackColor = true;
            // 
            // ckbSameZ
            // 
            this.ckbSameZ.AutoSize = true;
            this.ckbSameZ.Checked = true;
            this.ckbSameZ.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbSameZ.Location = new System.Drawing.Point(33, 43);
            this.ckbSameZ.Name = "ckbSameZ";
            this.ckbSameZ.Size = new System.Drawing.Size(117, 19);
            this.ckbSameZ.TabIndex = 0;
            this.ckbSameZ.Text = "Same Height";
            this.ckbSameZ.UseVisualStyleBackColor = true;
            // 
            // RbPanelWand
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.gpbMain);
            this.Name = "RbPanelWand";
            this.Size = new System.Drawing.Size(273, 190);
            this.gpbMain.ResumeLayout(false);
            this.gpbMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gpbMain;
        private System.Windows.Forms.CheckBox ckbSameIndex;
        private System.Windows.Forms.CheckBox ckbSameSet;
        private System.Windows.Forms.CheckBox ckbSameZ;
    }
}
