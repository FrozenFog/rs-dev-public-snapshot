namespace RelertSharp.GUI.Controls
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
            this.ckbToAircraft = new System.Windows.Forms.CheckBox();
            this.ckbToBuilding = new System.Windows.Forms.CheckBox();
            this.ckbToUnit = new System.Windows.Forms.CheckBox();
            this.ckbToInfantry = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pboxFacing)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkbVeteran)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkbHP)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txbGroup
            // 
            this.txbGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txbGroup.Location = new System.Drawing.Point(142, 296);
            this.txbGroup.Name = "txbGroup";
            this.txbGroup.Size = new System.Drawing.Size(113, 25);
            this.txbGroup.TabIndex = 22;
            this.txbGroup.TextChanged += new System.EventHandler(this.txbGroup_TextChanged);
            // 
            // pboxFacing
            // 
            this.pboxFacing.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pboxFacing.Image = global::RelertSharp.GUI.Properties.Resources.rotationBase;
            this.pboxFacing.Location = new System.Drawing.Point(142, 180);
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
            this.mtxbVeteran.Dock = System.Windows.Forms.DockStyle.Right;
            this.mtxbVeteran.Location = new System.Drawing.Point(73, 0);
            this.mtxbVeteran.Name = "mtxbVeteran";
            this.mtxbVeteran.Size = new System.Drawing.Size(40, 25);
            this.mtxbVeteran.TabIndex = 19;
            this.mtxbVeteran.Validated += new System.EventHandler(this.mtxbVeteran_Validated);
            // 
            // mtxbHP
            // 
            this.mtxbHP.Dock = System.Windows.Forms.DockStyle.Right;
            this.mtxbHP.Location = new System.Drawing.Point(73, 0);
            this.mtxbHP.Name = "mtxbHP";
            this.mtxbHP.Size = new System.Drawing.Size(40, 25);
            this.mtxbHP.TabIndex = 20;
            this.mtxbHP.Validated += new System.EventHandler(this.mtxbHP_Validated);
            // 
            // trkbVeteran
            // 
            this.trkbVeteran.AutoSize = false;
            this.trkbVeteran.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trkbVeteran.LargeChange = 100;
            this.trkbVeteran.Location = new System.Drawing.Point(0, 0);
            this.trkbVeteran.Maximum = 200;
            this.trkbVeteran.Name = "trkbVeteran";
            this.trkbVeteran.Size = new System.Drawing.Size(73, 27);
            this.trkbVeteran.TabIndex = 17;
            this.trkbVeteran.TickFrequency = 32;
            this.trkbVeteran.Scroll += new System.EventHandler(this.trkbVeteran_Scroll);
            // 
            // trkbHP
            // 
            this.trkbHP.AutoSize = false;
            this.trkbHP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trkbHP.LargeChange = 64;
            this.trkbHP.Location = new System.Drawing.Point(0, 0);
            this.trkbHP.Maximum = 256;
            this.trkbHP.Minimum = 1;
            this.trkbHP.Name = "trkbHP";
            this.trkbHP.Size = new System.Drawing.Size(73, 29);
            this.trkbHP.TabIndex = 18;
            this.trkbHP.TickFrequency = 32;
            this.trkbHP.Value = 256;
            this.trkbHP.Scroll += new System.EventHandler(this.trkbHP_Scroll);
            // 
            // cbbStatus
            // 
            this.cbbStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbbStatus.FormattingEnabled = true;
            this.cbbStatus.Location = new System.Drawing.Point(142, 267);
            this.cbbStatus.Name = "cbbStatus";
            this.cbbStatus.Size = new System.Drawing.Size(113, 23);
            this.cbbStatus.TabIndex = 14;
            this.cbbStatus.SelectedIndexChanged += new System.EventHandler(this.cbbStatus_SelectedIndexChanged);
            // 
            // cbbTags
            // 
            this.cbbTags.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbbTags.FormattingEnabled = true;
            this.cbbTags.Location = new System.Drawing.Point(142, 238);
            this.cbbTags.Name = "cbbTags";
            this.cbbTags.Size = new System.Drawing.Size(113, 23);
            this.cbbTags.TabIndex = 15;
            this.cbbTags.SelectedIndexChanged += new System.EventHandler(this.cbbTags_SelectedIndexChanged);
            // 
            // cbbOwnerHouse
            // 
            this.cbbOwnerHouse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbbOwnerHouse.FormattingEnabled = true;
            this.cbbOwnerHouse.Location = new System.Drawing.Point(142, 83);
            this.cbbOwnerHouse.Name = "cbbOwnerHouse";
            this.cbbOwnerHouse.Size = new System.Drawing.Size(113, 23);
            this.cbbOwnerHouse.TabIndex = 16;
            this.cbbOwnerHouse.SelectedIndexChanged += new System.EventHandler(this.cbbOwnerHouse_SelectedIndexChanged);
            // 
            // ckbFacing
            // 
            this.ckbFacing.AutoSize = true;
            this.ckbFacing.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckbFacing.Dock = System.Windows.Forms.DockStyle.Right;
            this.ckbFacing.Location = new System.Drawing.Point(59, 180);
            this.ckbFacing.Name = "ckbFacing";
            this.ckbFacing.Size = new System.Drawing.Size(77, 52);
            this.ckbFacing.TabIndex = 7;
            this.ckbFacing.Text = "Facing";
            this.ckbFacing.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckbFacing.UseVisualStyleBackColor = true;
            // 
            // ckbVeteran
            // 
            this.ckbVeteran.AutoSize = true;
            this.ckbVeteran.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckbVeteran.Dock = System.Windows.Forms.DockStyle.Right;
            this.ckbVeteran.Location = new System.Drawing.Point(35, 147);
            this.ckbVeteran.Name = "ckbVeteran";
            this.ckbVeteran.Size = new System.Drawing.Size(101, 27);
            this.ckbVeteran.TabIndex = 8;
            this.ckbVeteran.Text = "Veterancy";
            this.ckbVeteran.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckbVeteran.UseVisualStyleBackColor = true;
            // 
            // ckbHP
            // 
            this.ckbHP.AutoSize = true;
            this.ckbHP.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckbHP.Dock = System.Windows.Forms.DockStyle.Right;
            this.ckbHP.Location = new System.Drawing.Point(11, 112);
            this.ckbHP.Name = "ckbHP";
            this.ckbHP.Size = new System.Drawing.Size(125, 29);
            this.ckbHP.TabIndex = 9;
            this.ckbHP.Text = "Health Point";
            this.ckbHP.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckbHP.UseVisualStyleBackColor = true;
            // 
            // ckbGroup
            // 
            this.ckbGroup.AutoSize = true;
            this.ckbGroup.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckbGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ckbGroup.Location = new System.Drawing.Point(3, 296);
            this.ckbGroup.Name = "ckbGroup";
            this.ckbGroup.Size = new System.Drawing.Size(133, 25);
            this.ckbGroup.TabIndex = 10;
            this.ckbGroup.Text = "Group Number";
            this.ckbGroup.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckbGroup.UseVisualStyleBackColor = true;
            // 
            // ckbStat
            // 
            this.ckbStat.AutoSize = true;
            this.ckbStat.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckbStat.Dock = System.Windows.Forms.DockStyle.Right;
            this.ckbStat.Location = new System.Drawing.Point(19, 267);
            this.ckbStat.Name = "ckbStat";
            this.ckbStat.Size = new System.Drawing.Size(117, 23);
            this.ckbStat.TabIndex = 11;
            this.ckbStat.Text = "Unit Status";
            this.ckbStat.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckbStat.UseVisualStyleBackColor = true;
            // 
            // ckbTags
            // 
            this.ckbTags.AutoSize = true;
            this.ckbTags.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckbTags.Dock = System.Windows.Forms.DockStyle.Right;
            this.ckbTags.Location = new System.Drawing.Point(3, 238);
            this.ckbTags.Name = "ckbTags";
            this.ckbTags.Size = new System.Drawing.Size(133, 23);
            this.ckbTags.TabIndex = 12;
            this.ckbTags.Text = "Attatched Tag";
            this.ckbTags.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckbTags.UseVisualStyleBackColor = true;
            // 
            // ckbOwnerHouse
            // 
            this.ckbOwnerHouse.AutoSize = true;
            this.ckbOwnerHouse.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckbOwnerHouse.Dock = System.Windows.Forms.DockStyle.Right;
            this.ckbOwnerHouse.Location = new System.Drawing.Point(19, 83);
            this.ckbOwnerHouse.Name = "ckbOwnerHouse";
            this.ckbOwnerHouse.Size = new System.Drawing.Size(117, 23);
            this.ckbOwnerHouse.TabIndex = 13;
            this.ckbOwnerHouse.Text = "Owner House";
            this.ckbOwnerHouse.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckbOwnerHouse.UseVisualStyleBackColor = true;
            // 
            // ckbToAircraft
            // 
            this.ckbToAircraft.AutoSize = true;
            this.ckbToAircraft.Dock = System.Windows.Forms.DockStyle.Top;
            this.ckbToAircraft.Location = new System.Drawing.Point(0, 0);
            this.ckbToAircraft.Name = "ckbToAircraft";
            this.ckbToAircraft.Size = new System.Drawing.Size(111, 19);
            this.ckbToAircraft.TabIndex = 0;
            this.ckbToAircraft.Text = "Aircrafts";
            this.ckbToAircraft.UseVisualStyleBackColor = true;
            // 
            // ckbToBuilding
            // 
            this.ckbToBuilding.AutoSize = true;
            this.ckbToBuilding.Dock = System.Windows.Forms.DockStyle.Top;
            this.ckbToBuilding.Location = new System.Drawing.Point(0, 19);
            this.ckbToBuilding.Name = "ckbToBuilding";
            this.ckbToBuilding.Size = new System.Drawing.Size(111, 19);
            this.ckbToBuilding.TabIndex = 0;
            this.ckbToBuilding.Text = "Buildings";
            this.ckbToBuilding.UseVisualStyleBackColor = true;
            // 
            // ckbToUnit
            // 
            this.ckbToUnit.AutoSize = true;
            this.ckbToUnit.Dock = System.Windows.Forms.DockStyle.Top;
            this.ckbToUnit.Location = new System.Drawing.Point(0, 38);
            this.ckbToUnit.Name = "ckbToUnit";
            this.ckbToUnit.Size = new System.Drawing.Size(111, 19);
            this.ckbToUnit.TabIndex = 0;
            this.ckbToUnit.Text = "Units";
            this.ckbToUnit.UseVisualStyleBackColor = true;
            // 
            // ckbToInfantry
            // 
            this.ckbToInfantry.AutoSize = true;
            this.ckbToInfantry.Dock = System.Windows.Forms.DockStyle.Top;
            this.ckbToInfantry.Location = new System.Drawing.Point(0, 57);
            this.ckbToInfantry.Name = "ckbToInfantry";
            this.ckbToInfantry.Size = new System.Drawing.Size(111, 19);
            this.ckbToInfantry.TabIndex = 0;
            this.ckbToInfantry.Text = "Infantries";
            this.ckbToInfantry.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel3, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.ckbGroup, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.txbGroup, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.cbbStatus, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.pboxFacing, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.cbbTags, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.ckbStat, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.ckbOwnerHouse, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.cbbOwnerHouse, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.ckbTags, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.ckbFacing, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.ckbHP, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.ckbVeteran, 0, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 8;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(258, 323);
            this.tableLayoutPanel1.TabIndex = 24;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.trkbVeteran);
            this.panel3.Controls.Add(this.mtxbVeteran);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(142, 147);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(113, 27);
            this.panel3.TabIndex = 25;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.trkbHP);
            this.panel2.Controls.Add(this.mtxbHP);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(142, 112);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(113, 29);
            this.panel2.TabIndex = 25;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ckbToInfantry);
            this.panel1.Controls.Add(this.ckbToUnit);
            this.panel1.Controls.Add(this.ckbToBuilding);
            this.panel1.Controls.Add(this.ckbToAircraft);
            this.panel1.Location = new System.Drawing.Point(142, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(111, 74);
            this.panel1.TabIndex = 25;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Right;
            this.label1.Location = new System.Drawing.Point(41, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 80);
            this.label1.TabIndex = 17;
            this.label1.Text = "Apply To...";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // RbPanelAttribute
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = false;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "RbPanelAttribute";
            this.Size = new System.Drawing.Size(258, 323);
            ((System.ComponentModel.ISupportInitialize)(this.pboxFacing)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkbVeteran)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkbHP)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

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
        private System.Windows.Forms.CheckBox ckbToAircraft;
        private System.Windows.Forms.CheckBox ckbToBuilding;
        private System.Windows.Forms.CheckBox ckbToUnit;
        private System.Windows.Forms.CheckBox ckbToInfantry;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
    }
}
