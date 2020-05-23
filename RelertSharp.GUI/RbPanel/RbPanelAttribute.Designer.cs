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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ckbToAircraft = new System.Windows.Forms.CheckBox();
            this.ckbToBuilding = new System.Windows.Forms.CheckBox();
            this.ckbToUnit = new System.Windows.Forms.CheckBox();
            this.ckbToInfantry = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pboxFacing)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkbVeteran)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkbHP)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txbGroup
            // 
            this.txbGroup.Location = new System.Drawing.Point(205, 219);
            this.txbGroup.Name = "txbGroup";
            this.txbGroup.Size = new System.Drawing.Size(173, 25);
            this.txbGroup.TabIndex = 22;
            this.txbGroup.TextChanged += new System.EventHandler(this.txbGroup_TextChanged);
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
            this.mtxbVeteran.Validated += new System.EventHandler(this.mtxbVeteran_Validated);
            // 
            // mtxbHP
            // 
            this.mtxbHP.Location = new System.Drawing.Point(309, 43);
            this.mtxbHP.Name = "mtxbHP";
            this.mtxbHP.Size = new System.Drawing.Size(69, 25);
            this.mtxbHP.TabIndex = 20;
            this.mtxbHP.Validated += new System.EventHandler(this.mtxbHP_Validated);
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
            this.trkbVeteran.Scroll += new System.EventHandler(this.trkbVeteran_Scroll);
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
            this.trkbHP.Value = 256;
            this.trkbHP.Scroll += new System.EventHandler(this.trkbHP_Scroll);
            // 
            // cbbStatus
            // 
            this.cbbStatus.FormattingEnabled = true;
            this.cbbStatus.Location = new System.Drawing.Point(205, 190);
            this.cbbStatus.Name = "cbbStatus";
            this.cbbStatus.Size = new System.Drawing.Size(173, 23);
            this.cbbStatus.TabIndex = 14;
            this.cbbStatus.SelectedIndexChanged += new System.EventHandler(this.cbbStatus_SelectedIndexChanged);
            // 
            // cbbTags
            // 
            this.cbbTags.FormattingEnabled = true;
            this.cbbTags.Location = new System.Drawing.Point(205, 161);
            this.cbbTags.Name = "cbbTags";
            this.cbbTags.Size = new System.Drawing.Size(173, 23);
            this.cbbTags.TabIndex = 15;
            this.cbbTags.SelectedIndexChanged += new System.EventHandler(this.cbbTags_SelectedIndexChanged);
            // 
            // cbbOwnerHouse
            // 
            this.cbbOwnerHouse.FormattingEnabled = true;
            this.cbbOwnerHouse.Location = new System.Drawing.Point(205, 14);
            this.cbbOwnerHouse.Name = "cbbOwnerHouse";
            this.cbbOwnerHouse.Size = new System.Drawing.Size(173, 23);
            this.cbbOwnerHouse.TabIndex = 16;
            this.cbbOwnerHouse.SelectedIndexChanged += new System.EventHandler(this.cbbOwnerHouse_SelectedIndexChanged);
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ckbToAircraft);
            this.groupBox1.Controls.Add(this.ckbToBuilding);
            this.groupBox1.Controls.Add(this.ckbToUnit);
            this.groupBox1.Controls.Add(this.ckbToInfantry);
            this.groupBox1.Location = new System.Drawing.Point(384, 14);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(146, 230);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Apply To";
            // 
            // ckbToAircraft
            // 
            this.ckbToAircraft.AutoSize = true;
            this.ckbToAircraft.Location = new System.Drawing.Point(6, 189);
            this.ckbToAircraft.Name = "ckbToAircraft";
            this.ckbToAircraft.Size = new System.Drawing.Size(101, 19);
            this.ckbToAircraft.TabIndex = 0;
            this.ckbToAircraft.Text = "Aircrafts";
            this.ckbToAircraft.UseVisualStyleBackColor = true;
            // 
            // ckbToBuilding
            // 
            this.ckbToBuilding.AutoSize = true;
            this.ckbToBuilding.Location = new System.Drawing.Point(6, 134);
            this.ckbToBuilding.Name = "ckbToBuilding";
            this.ckbToBuilding.Size = new System.Drawing.Size(101, 19);
            this.ckbToBuilding.TabIndex = 0;
            this.ckbToBuilding.Text = "Buildings";
            this.ckbToBuilding.UseVisualStyleBackColor = true;
            // 
            // ckbToUnit
            // 
            this.ckbToUnit.AutoSize = true;
            this.ckbToUnit.Location = new System.Drawing.Point(6, 79);
            this.ckbToUnit.Name = "ckbToUnit";
            this.ckbToUnit.Size = new System.Drawing.Size(69, 19);
            this.ckbToUnit.TabIndex = 0;
            this.ckbToUnit.Text = "Units";
            this.ckbToUnit.UseVisualStyleBackColor = true;
            // 
            // ckbToInfantry
            // 
            this.ckbToInfantry.AutoSize = true;
            this.ckbToInfantry.Location = new System.Drawing.Point(6, 24);
            this.ckbToInfantry.Name = "ckbToInfantry";
            this.ckbToInfantry.Size = new System.Drawing.Size(109, 19);
            this.ckbToInfantry.TabIndex = 0;
            this.ckbToInfantry.Text = "Infantries";
            this.ckbToInfantry.UseVisualStyleBackColor = true;
            // 
            // RbPanelAttribute
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.groupBox1);
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
            this.Size = new System.Drawing.Size(551, 300);
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
            this.Controls.SetChildIndex(this.groupBox1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pboxFacing)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkbVeteran)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkbHP)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
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
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox ckbToAircraft;
        private System.Windows.Forms.CheckBox ckbToBuilding;
        private System.Windows.Forms.CheckBox ckbToUnit;
        private System.Windows.Forms.CheckBox ckbToInfantry;
    }
}
