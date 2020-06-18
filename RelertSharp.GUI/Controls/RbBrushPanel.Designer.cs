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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ckbNode = new System.Windows.Forms.CheckBox();
            this.ckbIgnoreBuilding = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ckbSimBud
            // 
            this.ckbSimBud.AutoSize = true;
            this.ckbSimBud.Location = new System.Drawing.Point(26, 42);
            this.ckbSimBud.Name = "ckbSimBud";
            this.ckbSimBud.Size = new System.Drawing.Size(197, 19);
            this.ckbSimBud.TabIndex = 0;
            this.ckbSimBud.Text = "Simulate Constructing";
            this.ckbSimBud.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox1.Controls.Add(this.ckbIgnoreBuilding);
            this.groupBox1.Controls.Add(this.ckbNode);
            this.groupBox1.Controls.Add(this.ckbSimBud);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(254, 262);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Brush Control";
            // 
            // ckbNode
            // 
            this.ckbNode.AutoSize = true;
            this.ckbNode.Location = new System.Drawing.Point(26, 80);
            this.ckbNode.Name = "ckbNode";
            this.ckbNode.Size = new System.Drawing.Size(125, 19);
            this.ckbNode.TabIndex = 0;
            this.ckbNode.Text = "Add Basenode";
            this.ckbNode.UseVisualStyleBackColor = true;
            // 
            // ckbIgnoreBuilding
            // 
            this.ckbIgnoreBuilding.AutoSize = true;
            this.ckbIgnoreBuilding.Location = new System.Drawing.Point(26, 116);
            this.ckbIgnoreBuilding.Name = "ckbIgnoreBuilding";
            this.ckbIgnoreBuilding.Size = new System.Drawing.Size(149, 19);
            this.ckbIgnoreBuilding.TabIndex = 0;
            this.ckbIgnoreBuilding.Text = "Ignore Building";
            this.ckbIgnoreBuilding.UseVisualStyleBackColor = true;
            // 
            // RbBrushPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "RbBrushPanel";
            this.Size = new System.Drawing.Size(260, 268);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox ckbSimBud;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox ckbIgnoreBuilding;
        private System.Windows.Forms.CheckBox ckbNode;
    }
}
