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
            this.ckbSameIndex = new System.Windows.Forms.CheckBox();
            this.ckbSameSet = new System.Windows.Forms.CheckBox();
            this.ckbSameZ = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ckbSameIndex
            // 
            this.ckbSameIndex.AutoSize = true;
            this.ckbSameIndex.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ckbSameIndex.Location = new System.Drawing.Point(126, 49);
            this.ckbSameIndex.Name = "ckbSameIndex";
            this.ckbSameIndex.Size = new System.Drawing.Size(18, 18);
            this.ckbSameIndex.TabIndex = 0;
            this.ckbSameIndex.UseVisualStyleBackColor = true;
            // 
            // ckbSameSet
            // 
            this.ckbSameSet.AutoSize = true;
            this.ckbSameSet.Checked = true;
            this.ckbSameSet.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbSameSet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ckbSameSet.Location = new System.Drawing.Point(126, 26);
            this.ckbSameSet.Name = "ckbSameSet";
            this.ckbSameSet.Size = new System.Drawing.Size(18, 17);
            this.ckbSameSet.TabIndex = 0;
            this.ckbSameSet.UseVisualStyleBackColor = true;
            // 
            // ckbSameZ
            // 
            this.ckbSameZ.AutoSize = true;
            this.ckbSameZ.Checked = true;
            this.ckbSameZ.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbSameZ.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ckbSameZ.Location = new System.Drawing.Point(126, 3);
            this.ckbSameZ.Name = "ckbSameZ";
            this.ckbSameZ.Size = new System.Drawing.Size(18, 17);
            this.ckbSameZ.TabIndex = 0;
            this.ckbSameZ.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.ckbSameIndex, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.ckbSameSet, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.ckbSameZ, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(147, 70);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Right;
            this.label1.Location = new System.Drawing.Point(25, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 23);
            this.label1.TabIndex = 2;
            this.label1.Text = "Same Height";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Right;
            this.label2.Location = new System.Drawing.Point(17, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 23);
            this.label2.TabIndex = 2;
            this.label2.Text = "Same Tileset";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Right;
            this.label3.Location = new System.Drawing.Point(9, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 24);
            this.label3.TabIndex = 2;
            this.label3.Text = "Same Subindex";
            // 
            // RbPanelWand
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = false;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "RbPanelWand";
            this.Size = new System.Drawing.Size(147, 70);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.CheckBox ckbSameIndex;
        private System.Windows.Forms.CheckBox ckbSameSet;
        private System.Windows.Forms.CheckBox ckbSameZ;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}
