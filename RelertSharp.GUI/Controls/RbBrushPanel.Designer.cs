namespace RelertSharp.GUI.Controls
{
    partial class RbBrushPanel
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
            this.ckbSimBud = new System.Windows.Forms.CheckBox();
            this.ckbIgnoreBuilding = new System.Windows.Forms.CheckBox();
            this.ckbNode = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tlpContain = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tlpContain.SuspendLayout();
            this.SuspendLayout();
            // 
            // ckbSimBud
            // 
            this.ckbSimBud.AutoSize = true;
            this.ckbSimBud.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ckbSimBud.Location = new System.Drawing.Point(191, 3);
            this.ckbSimBud.Name = "ckbSimBud";
            this.ckbSimBud.Size = new System.Drawing.Size(18, 17);
            this.ckbSimBud.TabIndex = 0;
            this.ckbSimBud.UseVisualStyleBackColor = true;
            // 
            // ckbIgnoreBuilding
            // 
            this.ckbIgnoreBuilding.AutoSize = true;
            this.ckbIgnoreBuilding.Enabled = false;
            this.ckbIgnoreBuilding.Location = new System.Drawing.Point(191, 49);
            this.ckbIgnoreBuilding.Name = "ckbIgnoreBuilding";
            this.ckbIgnoreBuilding.Size = new System.Drawing.Size(18, 17);
            this.ckbIgnoreBuilding.TabIndex = 0;
            this.ckbIgnoreBuilding.UseVisualStyleBackColor = true;
            // 
            // ckbNode
            // 
            this.ckbNode.AutoSize = true;
            this.ckbNode.Location = new System.Drawing.Point(191, 26);
            this.ckbNode.Name = "ckbNode";
            this.ckbNode.Size = new System.Drawing.Size(18, 17);
            this.ckbNode.TabIndex = 0;
            this.ckbNode.UseVisualStyleBackColor = true;
            this.ckbNode.CheckedChanged += new System.EventHandler(this.ckbNode_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Right;
            this.label1.Location = new System.Drawing.Point(10, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(175, 23);
            this.label1.TabIndex = 2;
            this.label1.Text = "Simulate Constructing";
            // 
            // tlpContain
            // 
            this.tlpContain.AutoSize = true;
            this.tlpContain.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlpContain.ColumnCount = 2;
            this.tlpContain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpContain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpContain.Controls.Add(this.ckbIgnoreBuilding, 1, 3);
            this.tlpContain.Controls.Add(this.label2, 0, 2);
            this.tlpContain.Controls.Add(this.ckbNode, 1, 2);
            this.tlpContain.Controls.Add(this.label1, 0, 1);
            this.tlpContain.Controls.Add(this.ckbSimBud, 1, 1);
            this.tlpContain.Controls.Add(this.label3, 0, 3);
            this.tlpContain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpContain.Location = new System.Drawing.Point(0, 0);
            this.tlpContain.Name = "tlpContain";
            this.tlpContain.RowCount = 4;
            this.tlpContain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpContain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpContain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpContain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpContain.Size = new System.Drawing.Size(212, 68);
            this.tlpContain.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Right;
            this.label2.Location = new System.Drawing.Point(82, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 23);
            this.label2.TabIndex = 2;
            this.label2.Text = "Add Basenode";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Right;
            this.label3.Location = new System.Drawing.Point(58, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(127, 23);
            this.label3.TabIndex = 2;
            this.label3.Text = "Ignore Building";
            // 
            // RbBrushPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = false;
            this.Controls.Add(this.tlpContain);
            this.Name = "RbBrushPanel";
            this.Size = new System.Drawing.Size(212, 68);
            this.tlpContain.ResumeLayout(false);
            this.tlpContain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox ckbSimBud;
        private System.Windows.Forms.CheckBox ckbIgnoreBuilding;
        private System.Windows.Forms.CheckBox ckbNode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tlpContain;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}
