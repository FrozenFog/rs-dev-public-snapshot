namespace RelertSharp.GUI.SubWindows.LogicEditor
{
    partial class PanelTrgTag
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
            this.gpbTag = new System.Windows.Forms.GroupBox();
            this.splitMain = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.txbTagName = new System.Windows.Forms.TextBox();
            this.txbTrgName = new System.Windows.Forms.TextBox();
            this.tlpCkb = new System.Windows.Forms.TableLayoutPanel();
            this.ckbEasy = new System.Windows.Forms.CheckBox();
            this.ckbNormal = new System.Windows.Forms.CheckBox();
            this.ckbHard = new System.Windows.Forms.CheckBox();
            this.ckbDisabled = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cbbTagID = new System.Windows.Forms.ComboBox();
            this.txbTrgID = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tlpBtn = new System.Windows.Forms.TableLayoutPanel();
            this.btnNewTrigger = new System.Windows.Forms.Button();
            this.cmsEditTemplate = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiEditTemp = new System.Windows.Forms.ToolStripMenuItem();
            this.btnDelTrigger = new System.Windows.Forms.Button();
            this.btnCopyTrigger = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.gpbRepeat = new System.Windows.Forms.GroupBox();
            this.tlpRepeat = new System.Windows.Forms.TableLayoutPanel();
            this.rdbRepeat0 = new System.Windows.Forms.RadioButton();
            this.rdbRepeat2 = new System.Windows.Forms.RadioButton();
            this.rdbRepeat1 = new System.Windows.Forms.RadioButton();
            this.tlpRight = new System.Windows.Forms.TableLayoutPanel();
            this.cbbCustomGroup = new System.Windows.Forms.ComboBox();
            this.cbbAttatchedTrg = new System.Windows.Forms.ComboBox();
            this.btnSaveTemp = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lklTraceTrigger = new System.Windows.Forms.LinkLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbxTriggerHouses = new System.Windows.Forms.ListBox();
            this.label8 = new System.Windows.Forms.Label();
            this.gpbTag.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            this.splitMain.Panel1.SuspendLayout();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tlpCkb.SuspendLayout();
            this.tlpBtn.SuspendLayout();
            this.cmsEditTemplate.SuspendLayout();
            this.gpbRepeat.SuspendLayout();
            this.tlpRepeat.SuspendLayout();
            this.tlpRight.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gpbTag
            // 
            this.gpbTag.Controls.Add(this.splitMain);
            this.gpbTag.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gpbTag.Location = new System.Drawing.Point(0, 0);
            this.gpbTag.Margin = new System.Windows.Forms.Padding(4);
            this.gpbTag.Name = "gpbTag";
            this.gpbTag.Padding = new System.Windows.Forms.Padding(4);
            this.gpbTag.Size = new System.Drawing.Size(1546, 248);
            this.gpbTag.TabIndex = 5;
            this.gpbTag.TabStop = false;
            this.gpbTag.Text = "LGCgpbTrgTag";
            // 
            // splitMain
            // 
            this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitMain.Location = new System.Drawing.Point(4, 22);
            this.splitMain.Name = "splitMain";
            // 
            // splitMain.Panel1
            // 
            this.splitMain.Panel1.Controls.Add(this.tableLayoutPanel1);
            this.splitMain.Panel1.Controls.Add(this.gpbRepeat);
            // 
            // splitMain.Panel2
            // 
            this.splitMain.Panel2.Controls.Add(this.tlpRight);
            this.splitMain.Panel2.Controls.Add(this.panel1);
            this.splitMain.Size = new System.Drawing.Size(1538, 222);
            this.splitMain.SplitterDistance = 996;
            this.splitMain.TabIndex = 18;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.txbTagName, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.txbTrgName, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.tlpCkb, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.label6, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.cbbTagID, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.txbTrgID, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label7, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.label4, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.tlpBtn, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 2, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(844, 222);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // txbTagName
            // 
            this.txbTagName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txbTagName.Location = new System.Drawing.Point(134, 114);
            this.txbTagName.Margin = new System.Windows.Forms.Padding(4);
            this.txbTagName.Name = "txbTagName";
            this.txbTagName.ReadOnly = true;
            this.txbTagName.Size = new System.Drawing.Size(706, 25);
            this.txbTagName.TabIndex = 5;
            // 
            // txbTrgName
            // 
            this.txbTrgName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txbTrgName.Location = new System.Drawing.Point(134, 64);
            this.txbTrgName.Margin = new System.Windows.Forms.Padding(4);
            this.txbTrgName.Name = "txbTrgName";
            this.txbTrgName.Size = new System.Drawing.Size(706, 25);
            this.txbTrgName.TabIndex = 5;
            this.txbTrgName.Validated += new System.EventHandler(this.txbTrgName_Validated);
            // 
            // tlpCkb
            // 
            this.tlpCkb.ColumnCount = 4;
            this.tableLayoutPanel1.SetColumnSpan(this.tlpCkb, 3);
            this.tlpCkb.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22F));
            this.tlpCkb.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22F));
            this.tlpCkb.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22F));
            this.tlpCkb.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34F));
            this.tlpCkb.Controls.Add(this.ckbEasy, 0, 0);
            this.tlpCkb.Controls.Add(this.ckbNormal, 1, 0);
            this.tlpCkb.Controls.Add(this.ckbHard, 2, 0);
            this.tlpCkb.Controls.Add(this.ckbDisabled, 3, 0);
            this.tlpCkb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpCkb.Location = new System.Drawing.Point(3, 143);
            this.tlpCkb.Name = "tlpCkb";
            this.tlpCkb.RowCount = 1;
            this.tlpCkb.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpCkb.Size = new System.Drawing.Size(838, 76);
            this.tlpCkb.TabIndex = 1;
            // 
            // ckbEasy
            // 
            this.ckbEasy.AutoSize = true;
            this.ckbEasy.Dock = System.Windows.Forms.DockStyle.Top;
            this.ckbEasy.Location = new System.Drawing.Point(4, 4);
            this.ckbEasy.Margin = new System.Windows.Forms.Padding(4);
            this.ckbEasy.Name = "ckbEasy";
            this.ckbEasy.Size = new System.Drawing.Size(176, 19);
            this.ckbEasy.TabIndex = 15;
            this.ckbEasy.Tag = "e";
            this.ckbEasy.Text = "LGCckbEasy";
            this.ckbEasy.UseVisualStyleBackColor = true;
            this.ckbEasy.CheckedChanged += new System.EventHandler(this.DiffRepeatCheckChanged);
            // 
            // ckbNormal
            // 
            this.ckbNormal.AutoSize = true;
            this.ckbNormal.Dock = System.Windows.Forms.DockStyle.Top;
            this.ckbNormal.Location = new System.Drawing.Point(188, 4);
            this.ckbNormal.Margin = new System.Windows.Forms.Padding(4);
            this.ckbNormal.Name = "ckbNormal";
            this.ckbNormal.Size = new System.Drawing.Size(176, 19);
            this.ckbNormal.TabIndex = 15;
            this.ckbNormal.Tag = "n";
            this.ckbNormal.Text = "LGCckbNormal";
            this.ckbNormal.UseVisualStyleBackColor = true;
            this.ckbNormal.CheckedChanged += new System.EventHandler(this.DiffRepeatCheckChanged);
            // 
            // ckbHard
            // 
            this.ckbHard.AutoSize = true;
            this.ckbHard.Dock = System.Windows.Forms.DockStyle.Top;
            this.ckbHard.Location = new System.Drawing.Point(372, 4);
            this.ckbHard.Margin = new System.Windows.Forms.Padding(4);
            this.ckbHard.Name = "ckbHard";
            this.ckbHard.Size = new System.Drawing.Size(176, 19);
            this.ckbHard.TabIndex = 15;
            this.ckbHard.Tag = "h";
            this.ckbHard.Text = "LGCckbHard";
            this.ckbHard.UseVisualStyleBackColor = true;
            this.ckbHard.CheckedChanged += new System.EventHandler(this.DiffRepeatCheckChanged);
            // 
            // ckbDisabled
            // 
            this.ckbDisabled.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ckbDisabled.AutoSize = true;
            this.ckbDisabled.Location = new System.Drawing.Point(693, 4);
            this.ckbDisabled.Margin = new System.Windows.Forms.Padding(4);
            this.ckbDisabled.Name = "ckbDisabled";
            this.ckbDisabled.Size = new System.Drawing.Size(141, 19);
            this.ckbDisabled.TabIndex = 12;
            this.ckbDisabled.Tag = "d";
            this.ckbDisabled.Text = "LGCckbDisabled";
            this.ckbDisabled.UseVisualStyleBackColor = true;
            this.ckbDisabled.CheckedChanged += new System.EventHandler(this.DiffRepeatCheckChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(114, 110);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.label6.Size = new System.Drawing.Size(12, 25);
            this.label6.TabIndex = 8;
            this.label6.Text = "-";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label5.Location = new System.Drawing.Point(4, 95);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(102, 15);
            this.label5.TabIndex = 7;
            this.label5.Text = "LGClblTagID";
            // 
            // cbbTagID
            // 
            this.cbbTagID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbbTagID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbTagID.FormattingEnabled = true;
            this.cbbTagID.Location = new System.Drawing.Point(4, 114);
            this.cbbTagID.Margin = new System.Windows.Forms.Padding(4);
            this.cbbTagID.Name = "cbbTagID";
            this.cbbTagID.Size = new System.Drawing.Size(102, 23);
            this.cbbTagID.TabIndex = 17;
            this.cbbTagID.SelectedIndexChanged += new System.EventHandler(this.cbbTagID_SelectedIndexChanged);
            // 
            // txbTrgID
            // 
            this.txbTrgID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txbTrgID.Location = new System.Drawing.Point(4, 64);
            this.txbTrgID.Margin = new System.Windows.Forms.Padding(4);
            this.txbTrgID.Name = "txbTrgID";
            this.txbTrgID.ReadOnly = true;
            this.txbTrgID.Size = new System.Drawing.Size(102, 25);
            this.txbTrgID.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label7.Location = new System.Drawing.Point(134, 95);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(706, 15);
            this.label7.TabIndex = 7;
            this.label7.Text = "LGClblTagName";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(114, 60);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.label4.Size = new System.Drawing.Size(12, 25);
            this.label4.TabIndex = 8;
            this.label4.Text = "-";
            // 
            // tlpBtn
            // 
            this.tlpBtn.ColumnCount = 3;
            this.tableLayoutPanel1.SetColumnSpan(this.tlpBtn, 3);
            this.tlpBtn.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpBtn.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpBtn.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpBtn.Controls.Add(this.btnNewTrigger, 0, 0);
            this.tlpBtn.Controls.Add(this.btnDelTrigger, 1, 0);
            this.tlpBtn.Controls.Add(this.btnCopyTrigger, 2, 0);
            this.tlpBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpBtn.Location = new System.Drawing.Point(3, 3);
            this.tlpBtn.Name = "tlpBtn";
            this.tlpBtn.RowCount = 1;
            this.tlpBtn.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpBtn.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tlpBtn.Size = new System.Drawing.Size(838, 34);
            this.tlpBtn.TabIndex = 0;
            // 
            // btnNewTrigger
            // 
            this.btnNewTrigger.ContextMenuStrip = this.cmsEditTemplate;
            this.btnNewTrigger.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnNewTrigger.Location = new System.Drawing.Point(4, 4);
            this.btnNewTrigger.Margin = new System.Windows.Forms.Padding(4);
            this.btnNewTrigger.Name = "btnNewTrigger";
            this.btnNewTrigger.Size = new System.Drawing.Size(271, 26);
            this.btnNewTrigger.TabIndex = 3;
            this.btnNewTrigger.Text = "LGCbtnNewTrg";
            this.btnNewTrigger.UseVisualStyleBackColor = true;
            this.btnNewTrigger.Click += new System.EventHandler(this.btnNewTrigger_Click);
            // 
            // cmsEditTemplate
            // 
            this.cmsEditTemplate.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmsEditTemplate.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiEditTemp});
            this.cmsEditTemplate.Name = "cmsEditTemplate";
            this.cmsEditTemplate.Size = new System.Drawing.Size(209, 28);
            // 
            // tsmiEditTemp
            // 
            this.tsmiEditTemp.Name = "tsmiEditTemp";
            this.tsmiEditTemp.Size = new System.Drawing.Size(208, 24);
            this.tsmiEditTemp.Text = "LGCtsmiEditTemp";
            this.tsmiEditTemp.Click += new System.EventHandler(this.tsmiEditTemp_Click);
            // 
            // btnDelTrigger
            // 
            this.btnDelTrigger.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDelTrigger.Location = new System.Drawing.Point(283, 4);
            this.btnDelTrigger.Margin = new System.Windows.Forms.Padding(4);
            this.btnDelTrigger.Name = "btnDelTrigger";
            this.btnDelTrigger.Size = new System.Drawing.Size(271, 26);
            this.btnDelTrigger.TabIndex = 3;
            this.btnDelTrigger.Text = "LGCbtnDelTrg";
            this.btnDelTrigger.UseVisualStyleBackColor = true;
            this.btnDelTrigger.Click += new System.EventHandler(this.btnDelTrigger_Click);
            // 
            // btnCopyTrigger
            // 
            this.btnCopyTrigger.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCopyTrigger.Location = new System.Drawing.Point(562, 4);
            this.btnCopyTrigger.Margin = new System.Windows.Forms.Padding(4);
            this.btnCopyTrigger.Name = "btnCopyTrigger";
            this.btnCopyTrigger.Size = new System.Drawing.Size(272, 26);
            this.btnCopyTrigger.TabIndex = 3;
            this.btnCopyTrigger.Text = "LGCbtnCopyTrg";
            this.btnCopyTrigger.UseVisualStyleBackColor = true;
            this.btnCopyTrigger.Click += new System.EventHandler(this.btnCopyTrigger_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label2.Location = new System.Drawing.Point(4, 45);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 15);
            this.label2.TabIndex = 7;
            this.label2.Text = "LGClblTrgID";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label3.Location = new System.Drawing.Point(134, 45);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(706, 15);
            this.label3.TabIndex = 7;
            this.label3.Text = "LGClblTrgName";
            // 
            // gpbRepeat
            // 
            this.gpbRepeat.Controls.Add(this.tlpRepeat);
            this.gpbRepeat.Dock = System.Windows.Forms.DockStyle.Right;
            this.gpbRepeat.Location = new System.Drawing.Point(844, 0);
            this.gpbRepeat.Margin = new System.Windows.Forms.Padding(4);
            this.gpbRepeat.Name = "gpbRepeat";
            this.gpbRepeat.Padding = new System.Windows.Forms.Padding(4);
            this.gpbRepeat.Size = new System.Drawing.Size(152, 222);
            this.gpbRepeat.TabIndex = 9;
            this.gpbRepeat.TabStop = false;
            this.gpbRepeat.Text = "LGCgpbRepeat";
            // 
            // tlpRepeat
            // 
            this.tlpRepeat.ColumnCount = 1;
            this.tlpRepeat.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpRepeat.Controls.Add(this.rdbRepeat0, 0, 0);
            this.tlpRepeat.Controls.Add(this.rdbRepeat2, 0, 2);
            this.tlpRepeat.Controls.Add(this.rdbRepeat1, 0, 1);
            this.tlpRepeat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpRepeat.Location = new System.Drawing.Point(4, 22);
            this.tlpRepeat.Name = "tlpRepeat";
            this.tlpRepeat.RowCount = 3;
            this.tlpRepeat.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpRepeat.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpRepeat.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpRepeat.Size = new System.Drawing.Size(144, 196);
            this.tlpRepeat.TabIndex = 1;
            // 
            // rdbRepeat0
            // 
            this.rdbRepeat0.AutoSize = true;
            this.rdbRepeat0.Checked = true;
            this.rdbRepeat0.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rdbRepeat0.Location = new System.Drawing.Point(4, 4);
            this.rdbRepeat0.Margin = new System.Windows.Forms.Padding(4);
            this.rdbRepeat0.Name = "rdbRepeat0";
            this.rdbRepeat0.Size = new System.Drawing.Size(136, 19);
            this.rdbRepeat0.TabIndex = 0;
            this.rdbRepeat0.TabStop = true;
            this.rdbRepeat0.Tag = "0";
            this.rdbRepeat0.Text = "LGCrdbRep0";
            this.rdbRepeat0.UseVisualStyleBackColor = true;
            this.rdbRepeat0.CheckedChanged += new System.EventHandler(this.DiffRepeatCheckChanged);
            // 
            // rdbRepeat2
            // 
            this.rdbRepeat2.AutoSize = true;
            this.rdbRepeat2.Dock = System.Windows.Forms.DockStyle.Top;
            this.rdbRepeat2.Location = new System.Drawing.Point(4, 58);
            this.rdbRepeat2.Margin = new System.Windows.Forms.Padding(4);
            this.rdbRepeat2.Name = "rdbRepeat2";
            this.rdbRepeat2.Size = new System.Drawing.Size(136, 19);
            this.rdbRepeat2.TabIndex = 0;
            this.rdbRepeat2.Tag = "2";
            this.rdbRepeat2.Text = "LGCrdbRep2";
            this.rdbRepeat2.UseVisualStyleBackColor = true;
            this.rdbRepeat2.CheckedChanged += new System.EventHandler(this.DiffRepeatCheckChanged);
            // 
            // rdbRepeat1
            // 
            this.rdbRepeat1.AutoSize = true;
            this.rdbRepeat1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rdbRepeat1.Location = new System.Drawing.Point(4, 31);
            this.rdbRepeat1.Margin = new System.Windows.Forms.Padding(4);
            this.rdbRepeat1.Name = "rdbRepeat1";
            this.rdbRepeat1.Size = new System.Drawing.Size(136, 19);
            this.rdbRepeat1.TabIndex = 0;
            this.rdbRepeat1.Tag = "1";
            this.rdbRepeat1.Text = "LGCrdbRep1";
            this.rdbRepeat1.UseVisualStyleBackColor = true;
            this.rdbRepeat1.CheckedChanged += new System.EventHandler(this.DiffRepeatCheckChanged);
            // 
            // tlpRight
            // 
            this.tlpRight.AutoSize = true;
            this.tlpRight.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlpRight.ColumnCount = 2;
            this.tlpRight.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpRight.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpRight.Controls.Add(this.cbbCustomGroup, 0, 3);
            this.tlpRight.Controls.Add(this.cbbAttatchedTrg, 0, 1);
            this.tlpRight.Controls.Add(this.btnSaveTemp, 0, 4);
            this.tlpRight.Controls.Add(this.label10, 0, 2);
            this.tlpRight.Controls.Add(this.label9, 0, 0);
            this.tlpRight.Controls.Add(this.lklTraceTrigger, 1, 0);
            this.tlpRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpRight.Location = new System.Drawing.Point(170, 0);
            this.tlpRight.Name = "tlpRight";
            this.tlpRight.RowCount = 5;
            this.tlpRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tlpRight.Size = new System.Drawing.Size(368, 222);
            this.tlpRight.TabIndex = 6;
            // 
            // cbbCustomGroup
            // 
            this.tlpRight.SetColumnSpan(this.cbbCustomGroup, 2);
            this.cbbCustomGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbbCustomGroup.FormattingEnabled = true;
            this.cbbCustomGroup.Location = new System.Drawing.Point(4, 74);
            this.cbbCustomGroup.Margin = new System.Windows.Forms.Padding(4);
            this.cbbCustomGroup.Name = "cbbCustomGroup";
            this.cbbCustomGroup.Size = new System.Drawing.Size(360, 23);
            this.cbbCustomGroup.TabIndex = 14;
            // 
            // cbbAttatchedTrg
            // 
            this.tlpRight.SetColumnSpan(this.cbbAttatchedTrg, 2);
            this.cbbAttatchedTrg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbbAttatchedTrg.FormattingEnabled = true;
            this.cbbAttatchedTrg.Location = new System.Drawing.Point(4, 24);
            this.cbbAttatchedTrg.Margin = new System.Windows.Forms.Padding(4);
            this.cbbAttatchedTrg.Name = "cbbAttatchedTrg";
            this.cbbAttatchedTrg.Size = new System.Drawing.Size(360, 23);
            this.cbbAttatchedTrg.TabIndex = 13;
            this.cbbAttatchedTrg.SelectedIndexChanged += new System.EventHandler(this.cbbAttatchedTrg_SelectedIndexChanged);
            // 
            // btnSaveTemp
            // 
            this.btnSaveTemp.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnSaveTemp.Location = new System.Drawing.Point(4, 104);
            this.btnSaveTemp.Margin = new System.Windows.Forms.Padding(4);
            this.btnSaveTemp.Name = "btnSaveTemp";
            this.btnSaveTemp.Size = new System.Drawing.Size(143, 29);
            this.btnSaveTemp.TabIndex = 3;
            this.btnSaveTemp.Text = "LGCbtnSaveTemplate";
            this.btnSaveTemp.UseVisualStyleBackColor = true;
            this.btnSaveTemp.Visible = false;
            this.btnSaveTemp.Click += new System.EventHandler(this.btnSaveTemp_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label10.Location = new System.Drawing.Point(4, 55);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(143, 15);
            this.label10.TabIndex = 10;
            this.label10.Text = "LGClblCustomGroup";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label9.Location = new System.Drawing.Point(4, 5);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(143, 15);
            this.label9.TabIndex = 10;
            this.label9.Text = "LGClblAttTrg";
            // 
            // lklTraceTrigger
            // 
            this.lklTraceTrigger.AutoSize = true;
            this.lklTraceTrigger.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lklTraceTrigger.Enabled = false;
            this.lklTraceTrigger.Location = new System.Drawing.Point(155, 5);
            this.lklTraceTrigger.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lklTraceTrigger.Name = "lklTraceTrigger";
            this.lklTraceTrigger.Size = new System.Drawing.Size(209, 15);
            this.lklTraceTrigger.TabIndex = 16;
            this.lklTraceTrigger.TabStop = true;
            this.lklTraceTrigger.Text = "LGClklTrace";
            this.lklTraceTrigger.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lklTraceTrigger.Click += new System.EventHandler(this.lklTraceTrigger_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbxTriggerHouses);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(170, 222);
            this.panel1.TabIndex = 12;
            // 
            // lbxTriggerHouses
            // 
            this.lbxTriggerHouses.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbxTriggerHouses.FormattingEnabled = true;
            this.lbxTriggerHouses.HorizontalScrollbar = true;
            this.lbxTriggerHouses.ItemHeight = 15;
            this.lbxTriggerHouses.Location = new System.Drawing.Point(0, 15);
            this.lbxTriggerHouses.Margin = new System.Windows.Forms.Padding(4);
            this.lbxTriggerHouses.Name = "lbxTriggerHouses";
            this.lbxTriggerHouses.Size = new System.Drawing.Size(170, 207);
            this.lbxTriggerHouses.TabIndex = 11;
            this.lbxTriggerHouses.SelectedIndexChanged += new System.EventHandler(this.lbxTriggerHouses_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Top;
            this.label8.Location = new System.Drawing.Point(0, 0);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(127, 15);
            this.label8.TabIndex = 10;
            this.label8.Text = "LGClblTrgHouses";
            // 
            // PanelTrgTag
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gpbTag);
            this.Name = "PanelTrgTag";
            this.Size = new System.Drawing.Size(1546, 248);
            this.gpbTag.ResumeLayout(false);
            this.splitMain.Panel1.ResumeLayout(false);
            this.splitMain.Panel2.ResumeLayout(false);
            this.splitMain.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
            this.splitMain.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tlpCkb.ResumeLayout(false);
            this.tlpCkb.PerformLayout();
            this.tlpBtn.ResumeLayout(false);
            this.cmsEditTemplate.ResumeLayout(false);
            this.gpbRepeat.ResumeLayout(false);
            this.tlpRepeat.ResumeLayout(false);
            this.tlpRepeat.PerformLayout();
            this.tlpRight.ResumeLayout(false);
            this.tlpRight.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gpbTag;
        private System.Windows.Forms.ComboBox cbbTagID;
        private System.Windows.Forms.LinkLabel lklTraceTrigger;
        private System.Windows.Forms.CheckBox ckbHard;
        private System.Windows.Forms.CheckBox ckbNormal;
        private System.Windows.Forms.CheckBox ckbEasy;
        private System.Windows.Forms.ComboBox cbbCustomGroup;
        private System.Windows.Forms.ComboBox cbbAttatchedTrg;
        private System.Windows.Forms.CheckBox ckbDisabled;
        private System.Windows.Forms.ListBox lbxTriggerHouses;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox gpbRepeat;
        private System.Windows.Forms.RadioButton rdbRepeat2;
        private System.Windows.Forms.RadioButton rdbRepeat1;
        private System.Windows.Forms.RadioButton rdbRepeat0;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txbTrgID;
        private System.Windows.Forms.TextBox txbTagName;
        private System.Windows.Forms.TextBox txbTrgName;
        private System.Windows.Forms.Button btnNewTrigger;
        private System.Windows.Forms.Button btnSaveTemp;
        private System.Windows.Forms.Button btnCopyTrigger;
        private System.Windows.Forms.Button btnDelTrigger;
        private System.Windows.Forms.ContextMenuStrip cmsEditTemplate;
        private System.Windows.Forms.ToolStripMenuItem tsmiEditTemp;
        private System.Windows.Forms.SplitContainer splitMain;
        private System.Windows.Forms.TableLayoutPanel tlpBtn;
        private System.Windows.Forms.TableLayoutPanel tlpCkb;
        private System.Windows.Forms.TableLayoutPanel tlpRepeat;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tlpRight;
    }
}
