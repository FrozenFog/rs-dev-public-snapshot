namespace RelertSharp.GUI.Controls
{
    partial class RbPanelLightRoom
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
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.txbName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.nmbxRed = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.ckbEnabled = new System.Windows.Forms.CheckBox();
            this.nmbxGreen = new System.Windows.Forms.NumericUpDown();
            this.nmbxBlue = new System.Windows.Forms.NumericUpDown();
            this.nmbxIntensity = new System.Windows.Forms.NumericUpDown();
            this.nmbxVisibility = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.btnColor = new System.Windows.Forms.Button();
            this.btnAddNew = new System.Windows.Forms.Button();
            this.lbxAllLight = new System.Windows.Forms.ListBox();
            this.cmsLightItem = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiSetSourcePos = new System.Windows.Forms.ToolStripMenuItem();
            this.label9 = new System.Windows.Forms.Label();
            this.tkbSaturation = new System.Windows.Forms.TrackBar();
            this.label10 = new System.Windows.Forms.Label();
            this.txbHex = new System.Windows.Forms.TextBox();
            this.tmrUpdater = new System.Windows.Forms.Timer(this.components);
            this.tlpMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmbxRed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmbxGreen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmbxBlue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmbxIntensity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmbxVisibility)).BeginInit();
            this.cmsLightItem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tkbSaturation)).BeginInit();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 2;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpMain.Controls.Add(this.label1, 0, 0);
            this.tlpMain.Controls.Add(this.txbName, 1, 0);
            this.tlpMain.Controls.Add(this.label2, 0, 1);
            this.tlpMain.Controls.Add(this.label3, 0, 2);
            this.tlpMain.Controls.Add(this.label4, 0, 3);
            this.tlpMain.Controls.Add(this.label5, 0, 5);
            this.tlpMain.Controls.Add(this.label6, 0, 6);
            this.tlpMain.Controls.Add(this.nmbxRed, 1, 1);
            this.tlpMain.Controls.Add(this.label7, 0, 7);
            this.tlpMain.Controls.Add(this.ckbEnabled, 1, 7);
            this.tlpMain.Controls.Add(this.nmbxGreen, 1, 2);
            this.tlpMain.Controls.Add(this.nmbxBlue, 1, 3);
            this.tlpMain.Controls.Add(this.nmbxIntensity, 1, 5);
            this.tlpMain.Controls.Add(this.nmbxVisibility, 1, 6);
            this.tlpMain.Controls.Add(this.label8, 0, 8);
            this.tlpMain.Controls.Add(this.btnColor, 1, 8);
            this.tlpMain.Controls.Add(this.btnAddNew, 0, 10);
            this.tlpMain.Controls.Add(this.lbxAllLight, 0, 11);
            this.tlpMain.Controls.Add(this.label9, 0, 4);
            this.tlpMain.Controls.Add(this.tkbSaturation, 1, 4);
            this.tlpMain.Controls.Add(this.label10, 0, 9);
            this.tlpMain.Controls.Add(this.txbHex, 1, 9);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 13;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpMain.Size = new System.Drawing.Size(250, 439);
            this.tlpMain.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(82, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name";
            // 
            // txbName
            // 
            this.txbName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txbName.Location = new System.Drawing.Point(127, 3);
            this.txbName.Name = "txbName";
            this.txbName.Size = new System.Drawing.Size(120, 25);
            this.txbName.TabIndex = 1;
            this.txbName.TextChanged += new System.EventHandler(this.EndEdit);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(90, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "Red";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(74, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 15);
            this.label3.TabIndex = 0;
            this.label3.Text = "Green";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(82, 101);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 15);
            this.label4.TabIndex = 0;
            this.label4.Text = "Blue";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(42, 162);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 15);
            this.label5.TabIndex = 0;
            this.label5.Text = "Intensity";
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(34, 193);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 15);
            this.label6.TabIndex = 0;
            this.label6.Text = "Visibility";
            // 
            // nmbxRed
            // 
            this.nmbxRed.DecimalPlaces = 4;
            this.nmbxRed.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nmbxRed.Location = new System.Drawing.Point(127, 34);
            this.nmbxRed.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.nmbxRed.Name = "nmbxRed";
            this.nmbxRed.Size = new System.Drawing.Size(120, 25);
            this.nmbxRed.TabIndex = 2;
            this.nmbxRed.Value = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.nmbxRed.ValueChanged += new System.EventHandler(this.EndEdit);
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(58, 220);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(63, 15);
            this.label7.TabIndex = 0;
            this.label7.Text = "Enabled";
            // 
            // ckbEnabled
            // 
            this.ckbEnabled.AutoSize = true;
            this.ckbEnabled.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ckbEnabled.Location = new System.Drawing.Point(127, 219);
            this.ckbEnabled.Name = "ckbEnabled";
            this.ckbEnabled.Size = new System.Drawing.Size(120, 17);
            this.ckbEnabled.TabIndex = 3;
            this.ckbEnabled.UseVisualStyleBackColor = true;
            this.ckbEnabled.CheckedChanged += new System.EventHandler(this.EndEdit);
            // 
            // nmbxGreen
            // 
            this.nmbxGreen.DecimalPlaces = 4;
            this.nmbxGreen.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nmbxGreen.Location = new System.Drawing.Point(127, 65);
            this.nmbxGreen.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.nmbxGreen.Name = "nmbxGreen";
            this.nmbxGreen.Size = new System.Drawing.Size(120, 25);
            this.nmbxGreen.TabIndex = 2;
            this.nmbxGreen.Value = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.nmbxGreen.ValueChanged += new System.EventHandler(this.EndEdit);
            // 
            // nmbxBlue
            // 
            this.nmbxBlue.DecimalPlaces = 4;
            this.nmbxBlue.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nmbxBlue.Location = new System.Drawing.Point(127, 96);
            this.nmbxBlue.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.nmbxBlue.Name = "nmbxBlue";
            this.nmbxBlue.Size = new System.Drawing.Size(120, 25);
            this.nmbxBlue.TabIndex = 2;
            this.nmbxBlue.Value = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.nmbxBlue.ValueChanged += new System.EventHandler(this.EndEdit);
            // 
            // nmbxIntensity
            // 
            this.nmbxIntensity.DecimalPlaces = 2;
            this.nmbxIntensity.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nmbxIntensity.Location = new System.Drawing.Point(127, 157);
            this.nmbxIntensity.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nmbxIntensity.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            this.nmbxIntensity.Name = "nmbxIntensity";
            this.nmbxIntensity.Size = new System.Drawing.Size(120, 25);
            this.nmbxIntensity.TabIndex = 2;
            this.nmbxIntensity.Value = new decimal(new int[] {
            2,
            0,
            0,
            65536});
            this.nmbxIntensity.ValueChanged += new System.EventHandler(this.EndEdit);
            // 
            // nmbxVisibility
            // 
            this.nmbxVisibility.Increment = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.nmbxVisibility.Location = new System.Drawing.Point(127, 188);
            this.nmbxVisibility.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nmbxVisibility.Name = "nmbxVisibility";
            this.nmbxVisibility.Size = new System.Drawing.Size(120, 25);
            this.nmbxVisibility.TabIndex = 2;
            this.nmbxVisibility.Value = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.nmbxVisibility.ValueChanged += new System.EventHandler(this.EndEdit);
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 247);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(111, 15);
            this.label8.TabIndex = 0;
            this.label8.Text = "Color Preview";
            // 
            // btnColor
            // 
            this.btnColor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnColor.Location = new System.Drawing.Point(127, 242);
            this.btnColor.Name = "btnColor";
            this.btnColor.Size = new System.Drawing.Size(120, 25);
            this.btnColor.TabIndex = 4;
            this.btnColor.UseVisualStyleBackColor = true;
            this.btnColor.Click += new System.EventHandler(this.btnColor_Click);
            // 
            // btnAddNew
            // 
            this.tlpMain.SetColumnSpan(this.btnAddNew, 2);
            this.btnAddNew.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAddNew.Location = new System.Drawing.Point(3, 304);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Size = new System.Drawing.Size(244, 30);
            this.btnAddNew.TabIndex = 5;
            this.btnAddNew.Text = "Add new light source";
            this.btnAddNew.UseVisualStyleBackColor = true;
            this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);
            // 
            // lbxAllLight
            // 
            this.tlpMain.SetColumnSpan(this.lbxAllLight, 2);
            this.lbxAllLight.ContextMenuStrip = this.cmsLightItem;
            this.lbxAllLight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbxAllLight.FormattingEnabled = true;
            this.lbxAllLight.HorizontalScrollbar = true;
            this.lbxAllLight.ItemHeight = 15;
            this.lbxAllLight.Location = new System.Drawing.Point(3, 340);
            this.lbxAllLight.Name = "lbxAllLight";
            this.lbxAllLight.Size = new System.Drawing.Size(244, 76);
            this.lbxAllLight.TabIndex = 6;
            this.lbxAllLight.SelectedValueChanged += new System.EventHandler(this.lbxAllLight_SelectedValueChanged);
            this.lbxAllLight.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lbxAllLight_MouseDoubleClick);
            // 
            // cmsLightItem
            // 
            this.cmsLightItem.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmsLightItem.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiSetSourcePos});
            this.cmsLightItem.Name = "cmsLightItem";
            this.cmsLightItem.Size = new System.Drawing.Size(256, 28);
            // 
            // tsmiSetSourcePos
            // 
            this.tsmiSetSourcePos.Name = "tsmiSetSourcePos";
            this.tsmiSetSourcePos.Size = new System.Drawing.Size(255, 24);
            this.tsmiSetSourcePos.Text = "Set light source location";
            this.tsmiSetSourcePos.Click += new System.EventHandler(this.tsmiSetSourcePos_Click);
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(34, 131);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(87, 15);
            this.label9.TabIndex = 0;
            this.label9.Text = "Saturation";
            // 
            // tkbSaturation
            // 
            this.tkbSaturation.AutoSize = false;
            this.tkbSaturation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tkbSaturation.LargeChange = 10;
            this.tkbSaturation.Location = new System.Drawing.Point(127, 127);
            this.tkbSaturation.Maximum = 100;
            this.tkbSaturation.Name = "tkbSaturation";
            this.tkbSaturation.Size = new System.Drawing.Size(120, 24);
            this.tkbSaturation.TabIndex = 7;
            this.tkbSaturation.TickFrequency = 10;
            this.tkbSaturation.Scroll += new System.EventHandler(this.tkbSaturation_Scroll);
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(50, 278);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(71, 15);
            this.label10.TabIndex = 0;
            this.label10.Text = "Hex Code";
            // 
            // txbHex
            // 
            this.txbHex.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txbHex.Location = new System.Drawing.Point(127, 273);
            this.txbHex.Name = "txbHex";
            this.txbHex.Size = new System.Drawing.Size(120, 25);
            this.txbHex.TabIndex = 8;
            this.txbHex.Validated += new System.EventHandler(this.txbHex_Validated);
            // 
            // tmrUpdater
            // 
            this.tmrUpdater.Interval = 500;
            this.tmrUpdater.Tick += new System.EventHandler(this.tmrUpdater_Tick);
            // 
            // RbPanelLightRoom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = false;
            this.Controls.Add(this.tlpMain);
            this.Name = "RbPanelLightRoom";
            this.Size = new System.Drawing.Size(250, 439);
            this.Leave += new System.EventHandler(this.RbPanelLightRoom_Leave);
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmbxRed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmbxGreen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmbxBlue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmbxIntensity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmbxVisibility)).EndInit();
            this.cmsLightItem.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tkbSaturation)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txbName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown nmbxRed;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox ckbEnabled;
        private System.Windows.Forms.NumericUpDown nmbxGreen;
        private System.Windows.Forms.NumericUpDown nmbxBlue;
        private System.Windows.Forms.NumericUpDown nmbxIntensity;
        private System.Windows.Forms.NumericUpDown nmbxVisibility;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnColor;
        private System.Windows.Forms.Button btnAddNew;
        private System.Windows.Forms.ListBox lbxAllLight;
        private System.Windows.Forms.ContextMenuStrip cmsLightItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiSetSourcePos;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TrackBar tkbSaturation;
        private System.Windows.Forms.Timer tmrUpdater;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txbHex;
    }
}
