namespace RelertSharp.GUI.RbPanel
{
    partial class RbPanelAttribute : RbPanelBase
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
            this.components = new System.ComponentModel.Container();
            this.txbGroup = new System.Windows.Forms.TextBox();
            this.pboxFacing = new System.Windows.Forms.PictureBox();
            this.mtxbVeteran = new System.Windows.Forms.MaskedTextBox();
            this.mtxbHP = new System.Windows.Forms.MaskedTextBox();
            this.trkbVeteran = new System.Windows.Forms.TrackBar();
            this.trkbHP = new System.Windows.Forms.TrackBar();
            this.cbbStatus = new System.Windows.Forms.ComboBox();
            this.cbbTags = new System.Windows.Forms.ComboBox();
            this.cbbOwnerHouse = new System.Windows.Forms.ComboBox();
            this.ckbFacing = new System.Windows.Forms.CheckBox();
            this.ckbVeteran = new System.Windows.Forms.CheckBox();
            this.ckbHP = new System.Windows.Forms.CheckBox();
            this.ckbGroup = new System.Windows.Forms.CheckBox();
            this.ckbStat = new System.Windows.Forms.CheckBox();
            this.ckbTags = new System.Windows.Forms.CheckBox();
            this.ckbOwnerHouse = new System.Windows.Forms.CheckBox();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pboxFacing)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkbVeteran)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkbHP)).BeginInit();
            this.SuspendLayout();
            // 
            // txbGroup
            // 
            this.txbGroup.Location = new System.Drawing.Point(205, 219);
            this.txbGroup.Name = "txbGroup";
            this.txbGroup.Size = new System.Drawing.Size(173, 25);
            this.txbGroup.TabIndex = 22;
            // 
            // pboxFacing
            // 
            this.pboxFacing.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pboxFacing.Image = global::RelertSharp.GUI.Properties.Resources.rotationBase;
            this.pboxFacing.Location = new System.Drawing.Point(205, 103);
            this.pboxFacing.Name = "pboxFacing";
            this.pboxFacing.Size = new System.Drawing.Size(52, 52);
            this.pboxFacing.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pboxFacing.TabIndex = 21;
            this.pboxFacing.TabStop = false;
            this.pboxFacing.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pboxFacing_MouseDown);
            this.pboxFacing.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pboxFacing_MouseMove);
            this.pboxFacing.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pboxFacing_MouseUp);
            // 
            // mtxbVeteran
            // 
            this.mtxbVeteran.Location = new System.Drawing.Point(309, 74);
            this.mtxbVeteran.Name = "mtxbVeteran";
            this.mtxbVeteran.Size = new System.Drawing.Size(69, 25);
            this.mtxbVeteran.TabIndex = 19;
            // 
            // mtxbHP
            // 
            this.mtxbHP.Location = new System.Drawing.Point(309, 43);
            this.mtxbHP.Name = "mtxbHP";
            this.mtxbHP.Size = new System.Drawing.Size(69, 25);
            this.mtxbHP.TabIndex = 20;
            // 
            // trkbVeteran
            // 
            this.trkbVeteran.AutoSize = false;
            this.trkbVeteran.LargeChange = 100;
            this.trkbVeteran.Location = new System.Drawing.Point(205, 74);
            this.trkbVeteran.Maximum = 200;
            this.trkbVeteran.Name = "trkbVeteran";
            this.trkbVeteran.Size = new System.Drawing.Size(98, 23);
            this.trkbVeteran.TabIndex = 17;
            this.trkbVeteran.TickFrequency = 32;
            // 
            // trkbHP
            // 
            this.trkbHP.AutoSize = false;
            this.trkbHP.LargeChange = 64;
            this.trkbHP.Location = new System.Drawing.Point(205, 43);
            this.trkbHP.Maximum = 256;
            this.trkbHP.Minimum = 1;
            this.trkbHP.Name = "trkbHP";
            this.trkbHP.Size = new System.Drawing.Size(98, 23);
            this.trkbHP.TabIndex = 18;
            this.trkbHP.TickFrequency = 32;
            this.trkbHP.Value = 1;
            // 
            // cbbStatus
            // 
            this.cbbStatus.FormattingEnabled = true;
            this.cbbStatus.Location = new System.Drawing.Point(205, 190);
            this.cbbStatus.Name = "cbbStatus";
            this.cbbStatus.Size = new System.Drawing.Size(173, 23);
            this.cbbStatus.TabIndex = 14;
            // 
            // cbbTags
            // 
            this.cbbTags.FormattingEnabled = true;
            this.cbbTags.Location = new System.Drawing.Point(205, 161);
            this.cbbTags.Name = "cbbTags";
            this.cbbTags.Size = new System.Drawing.Size(173, 23);
            this.cbbTags.TabIndex = 15;
            // 
            // cbbOwnerHouse
            // 
            this.cbbOwnerHouse.FormattingEnabled = true;
            this.cbbOwnerHouse.Location = new System.Drawing.Point(205, 14);
            this.cbbOwnerHouse.Name = "cbbOwnerHouse";
            this.cbbOwnerHouse.Size = new System.Drawing.Size(173, 23);
            this.cbbOwnerHouse.TabIndex = 16;
            // 
            // ckbFacing
            // 
            this.ckbFacing.AutoSize = true;
            this.ckbFacing.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckbFacing.Location = new System.Drawing.Point(122, 121);
            this.ckbFacing.Name = "ckbFacing";
            this.ckbFacing.Size = new System.Drawing.Size(77, 19);
            this.ckbFacing.TabIndex = 7;
            this.ckbFacing.Text = "Facing";
            this.ckbFacing.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckbFacing.UseVisualStyleBackColor = true;
            // 
            // ckbVeteran
            // 
            this.ckbVeteran.AutoSize = true;
            this.ckbVeteran.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckbVeteran.Location = new System.Drawing.Point(10, 76);
            this.ckbVeteran.Name = "ckbVeteran";
            this.ckbVeteran.Size = new System.Drawing.Size(189, 19);
            this.ckbVeteran.TabIndex = 8;
            this.ckbVeteran.Text = "Veterancy Percentage";
            this.ckbVeteran.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckbVeteran.UseVisualStyleBackColor = true;
            // 
            // ckbHP
            // 
            this.ckbHP.AutoSize = true;
            this.ckbHP.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckbHP.Location = new System.Drawing.Point(74, 45);
            this.ckbHP.Name = "ckbHP";
            this.ckbHP.Size = new System.Drawing.Size(125, 19);
            this.ckbHP.TabIndex = 9;
            this.ckbHP.Text = "Health Point";
            this.ckbHP.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckbHP.UseVisualStyleBackColor = true;
            // 
            // ckbGroup
            // 
            this.ckbGroup.AutoSize = true;
            this.ckbGroup.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckbGroup.Location = new System.Drawing.Point(74, 221);
            this.ckbGroup.Name = "ckbGroup";
            this.ckbGroup.Size = new System.Drawing.Size(125, 19);
            this.ckbGroup.TabIndex = 10;
            this.ckbGroup.Text = "Group Number";
            this.ckbGroup.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckbGroup.UseVisualStyleBackColor = true;
            // 
            // ckbStat
            // 
            this.ckbStat.AutoSize = true;
            this.ckbStat.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckbStat.Location = new System.Drawing.Point(82, 192);
            this.ckbStat.Name = "ckbStat";
            this.ckbStat.Size = new System.Drawing.Size(117, 19);
            this.ckbStat.TabIndex = 11;
            this.ckbStat.Text = "Unit Status";
            this.ckbStat.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckbStat.UseVisualStyleBackColor = true;
            // 
            // ckbTags
            // 
            this.ckbTags.AutoSize = true;
            this.ckbTags.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckbTags.Location = new System.Drawing.Point(66, 163);
            this.ckbTags.Name = "ckbTags";
            this.ckbTags.Size = new System.Drawing.Size(133, 19);
            this.ckbTags.TabIndex = 12;
            this.ckbTags.Text = "Attatched Tag";
            this.ckbTags.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckbTags.UseVisualStyleBackColor = true;
            // 
            // ckbOwnerHouse
            // 
            this.ckbOwnerHouse.AutoSize = true;
            this.ckbOwnerHouse.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckbOwnerHouse.Location = new System.Drawing.Point(82, 16);
            this.ckbOwnerHouse.Name = "ckbOwnerHouse";
            this.ckbOwnerHouse.Size = new System.Drawing.Size(117, 19);
            this.ckbOwnerHouse.TabIndex = 13;
            this.ckbOwnerHouse.Text = "Owner House";
            this.ckbOwnerHouse.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckbOwnerHouse.UseVisualStyleBackColor = true;
            // 
            // RbPanelAttribute
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.txbGroup);
            this.Controls.Add(this.pboxFacing);
            this.Controls.Add(this.mtxbVeteran);
            this.Controls.Add(this.mtxbHP);
            this.Controls.Add(this.trkbVeteran);
            this.Controls.Add(this.trkbHP);
            this.Controls.Add(this.cbbStatus);
            this.Controls.Add(this.cbbTags);
            this.Controls.Add(this.cbbOwnerHouse);
            this.Controls.Add(this.ckbFacing);
            this.Controls.Add(this.ckbVeteran);
            this.Controls.Add(this.ckbHP);
            this.Controls.Add(this.ckbGroup);
            this.Controls.Add(this.ckbStat);
            this.Controls.Add(this.ckbTags);
            this.Controls.Add(this.ckbOwnerHouse);
            this.Name = "RbPanelAttribute";
            this.Controls.SetChildIndex(this.ckbOwnerHouse, 0);
            this.Controls.SetChildIndex(this.ckbTags, 0);
            this.Controls.SetChildIndex(this.ckbStat, 0);
            this.Controls.SetChildIndex(this.ckbGroup, 0);
            this.Controls.SetChildIndex(this.ckbHP, 0);
            this.Controls.SetChildIndex(this.ckbVeteran, 0);
            this.Controls.SetChildIndex(this.ckbFacing, 0);
            this.Controls.SetChildIndex(this.cbbOwnerHouse, 0);
            this.Controls.SetChildIndex(this.cbbTags, 0);
            this.Controls.SetChildIndex(this.cbbStatus, 0);
            this.Controls.SetChildIndex(this.trkbHP, 0);
            this.Controls.SetChildIndex(this.trkbVeteran, 0);
            this.Controls.SetChildIndex(this.mtxbHP, 0);
            this.Controls.SetChildIndex(this.mtxbVeteran, 0);
            this.Controls.SetChildIndex(this.pboxFacing, 0);
            this.Controls.SetChildIndex(this.txbGroup, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pboxFacing)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkbVeteran)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkbHP)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txbGroup;
        private System.Windows.Forms.PictureBox pboxFacing;
        private System.Windows.Forms.MaskedTextBox mtxbVeteran;
        private System.Windows.Forms.MaskedTextBox mtxbHP;
        private System.Windows.Forms.TrackBar trkbVeteran;
        private System.Windows.Forms.TrackBar trkbHP;
        private System.Windows.Forms.ComboBox cbbStatus;
        private System.Windows.Forms.ComboBox cbbTags;
        private System.Windows.Forms.ComboBox cbbOwnerHouse;
        private System.Windows.Forms.CheckBox ckbFacing;
        private System.Windows.Forms.CheckBox ckbVeteran;
        private System.Windows.Forms.CheckBox ckbHP;
        private System.Windows.Forms.CheckBox ckbGroup;
        private System.Windows.Forms.CheckBox ckbStat;
        private System.Windows.Forms.CheckBox ckbTags;
        private System.Windows.Forms.CheckBox ckbOwnerHouse;
        private System.Windows.Forms.ToolTip toolTip;
    }
}
