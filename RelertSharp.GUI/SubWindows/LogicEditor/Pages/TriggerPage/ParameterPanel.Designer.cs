namespace RelertSharp.GUI.SubWindows.LogicEditor
{
    partial class ParameterPanel
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
            this.mtxbEventID = new System.Windows.Forms.MaskedTextBox();
            this.gpbEventParam = new System.Windows.Forms.GroupBox();
            this.lblNoParamE = new System.Windows.Forms.Label();
            this.tlpParam = new System.Windows.Forms.TableLayoutPanel();
            this.pnlR4 = new System.Windows.Forms.Panel();
            this.txbEP4 = new System.Windows.Forms.TextBox();
            this.cbbEP4 = new System.Windows.Forms.ComboBox();
            this.ckbEP4 = new System.Windows.Forms.CheckBox();
            this.lklEP4 = new System.Windows.Forms.LinkLabel();
            this.pnlR3 = new System.Windows.Forms.Panel();
            this.txbEP3 = new System.Windows.Forms.TextBox();
            this.cbbEP3 = new System.Windows.Forms.ComboBox();
            this.ckbEP3 = new System.Windows.Forms.CheckBox();
            this.lklEP3 = new System.Windows.Forms.LinkLabel();
            this.pnlR2 = new System.Windows.Forms.Panel();
            this.txbEP2 = new System.Windows.Forms.TextBox();
            this.cbbEP2 = new System.Windows.Forms.ComboBox();
            this.ckbEP2 = new System.Windows.Forms.CheckBox();
            this.lklEP2 = new System.Windows.Forms.LinkLabel();
            this.pnlR1 = new System.Windows.Forms.Panel();
            this.txbEP1 = new System.Windows.Forms.TextBox();
            this.ckbEP1 = new System.Windows.Forms.CheckBox();
            this.cbbEP1 = new System.Windows.Forms.ComboBox();
            this.lklEP1 = new System.Windows.Forms.LinkLabel();
            this.cbbEventAbst = new System.Windows.Forms.ComboBox();
            this.txbEventAnno = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.rtxbEventDetail = new System.Windows.Forms.RichTextBox();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.gpbEventParam.SuspendLayout();
            this.tlpParam.SuspendLayout();
            this.pnlR4.SuspendLayout();
            this.pnlR3.SuspendLayout();
            this.pnlR2.SuspendLayout();
            this.pnlR1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlTop.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // mtxbEventID
            // 
            this.mtxbEventID.Location = new System.Drawing.Point(4, 24);
            this.mtxbEventID.Margin = new System.Windows.Forms.Padding(4);
            this.mtxbEventID.Mask = "00";
            this.mtxbEventID.Name = "mtxbEventID";
            this.mtxbEventID.PromptChar = ' ';
            this.mtxbEventID.Size = new System.Drawing.Size(37, 25);
            this.mtxbEventID.TabIndex = 12;
            this.mtxbEventID.ValidatingType = typeof(int);
            this.mtxbEventID.TextChanged += new System.EventHandler(this.mtxbEventID_Validated);
            // 
            // gpbEventParam
            // 
            this.gpbEventParam.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gpbEventParam.Controls.Add(this.lblNoParamE);
            this.gpbEventParam.Controls.Add(this.tlpParam);
            this.gpbEventParam.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gpbEventParam.Location = new System.Drawing.Point(0, 252);
            this.gpbEventParam.Margin = new System.Windows.Forms.Padding(4);
            this.gpbEventParam.Name = "gpbEventParam";
            this.gpbEventParam.Padding = new System.Windows.Forms.Padding(4);
            this.gpbEventParam.Size = new System.Drawing.Size(423, 150);
            this.gpbEventParam.TabIndex = 13;
            this.gpbEventParam.TabStop = false;
            this.gpbEventParam.Text = "LGCgpbEventParam";
            this.gpbEventParam.Resize += new System.EventHandler(this.gpbEventParam_Resize);
            // 
            // lblNoParamE
            // 
            this.lblNoParamE.AutoSize = true;
            this.lblNoParamE.Location = new System.Drawing.Point(146, 77);
            this.lblNoParamE.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNoParamE.Name = "lblNoParamE";
            this.lblNoParamE.Size = new System.Drawing.Size(111, 15);
            this.lblNoParamE.TabIndex = 0;
            this.lblNoParamE.Text = "LGClblNoParam";
            this.lblNoParamE.Visible = false;
            // 
            // tlpParam
            // 
            this.tlpParam.AutoSize = true;
            this.tlpParam.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlpParam.ColumnCount = 2;
            this.tlpParam.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpParam.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpParam.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpParam.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpParam.Controls.Add(this.pnlR4, 1, 3);
            this.tlpParam.Controls.Add(this.lklEP4, 0, 3);
            this.tlpParam.Controls.Add(this.pnlR3, 1, 2);
            this.tlpParam.Controls.Add(this.lklEP3, 0, 2);
            this.tlpParam.Controls.Add(this.pnlR2, 1, 1);
            this.tlpParam.Controls.Add(this.lklEP2, 0, 1);
            this.tlpParam.Controls.Add(this.pnlR1, 1, 0);
            this.tlpParam.Controls.Add(this.lklEP1, 0, 0);
            this.tlpParam.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpParam.Location = new System.Drawing.Point(4, 22);
            this.tlpParam.Name = "tlpParam";
            this.tlpParam.RowCount = 4;
            this.tlpParam.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpParam.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpParam.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpParam.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpParam.Size = new System.Drawing.Size(415, 124);
            this.tlpParam.TabIndex = 25;
            // 
            // pnlR4
            // 
            this.pnlR4.Controls.Add(this.txbEP4);
            this.pnlR4.Controls.Add(this.cbbEP4);
            this.pnlR4.Controls.Add(this.ckbEP4);
            this.pnlR4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlR4.Location = new System.Drawing.Point(42, 96);
            this.pnlR4.Name = "pnlR4";
            this.pnlR4.Size = new System.Drawing.Size(370, 25);
            this.pnlR4.TabIndex = 26;
            // 
            // txbEP4
            // 
            this.txbEP4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txbEP4.Location = new System.Drawing.Point(0, 0);
            this.txbEP4.Margin = new System.Windows.Forms.Padding(4);
            this.txbEP4.Name = "txbEP4";
            this.txbEP4.Size = new System.Drawing.Size(352, 25);
            this.txbEP4.TabIndex = 4;
            this.txbEP4.Tag = "3";
            this.txbEP4.Visible = false;
            this.txbEP4.Validated += new System.EventHandler(this.ParamChanged);
            // 
            // cbbEP4
            // 
            this.cbbEP4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbbEP4.FormattingEnabled = true;
            this.cbbEP4.Location = new System.Drawing.Point(0, 0);
            this.cbbEP4.Margin = new System.Windows.Forms.Padding(4);
            this.cbbEP4.Name = "cbbEP4";
            this.cbbEP4.Size = new System.Drawing.Size(352, 23);
            this.cbbEP4.TabIndex = 4;
            this.cbbEP4.Tag = "3";
            this.cbbEP4.Visible = false;
            this.cbbEP4.Validated += new System.EventHandler(this.ParamChanged);
            // 
            // ckbEP4
            // 
            this.ckbEP4.AutoSize = true;
            this.ckbEP4.Dock = System.Windows.Forms.DockStyle.Right;
            this.ckbEP4.Location = new System.Drawing.Point(352, 0);
            this.ckbEP4.Margin = new System.Windows.Forms.Padding(4);
            this.ckbEP4.Name = "ckbEP4";
            this.ckbEP4.Size = new System.Drawing.Size(18, 25);
            this.ckbEP4.TabIndex = 4;
            this.ckbEP4.Tag = "3";
            this.ckbEP4.UseVisualStyleBackColor = true;
            this.ckbEP4.Visible = false;
            this.ckbEP4.CheckedChanged += new System.EventHandler(this.ParamChanged);
            // 
            // lklEP4
            // 
            this.lklEP4.AutoSize = true;
            this.lklEP4.Dock = System.Windows.Forms.DockStyle.Left;
            this.lklEP4.Location = new System.Drawing.Point(4, 93);
            this.lklEP4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lklEP4.Name = "lklEP4";
            this.lklEP4.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.lklEP4.Size = new System.Drawing.Size(31, 31);
            this.lklEP4.TabIndex = 9;
            this.lklEP4.TabStop = true;
            this.lklEP4.Tag = "3";
            this.lklEP4.Text = "EP4";
            this.lklEP4.Visible = false;
            this.lklEP4.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lkl_LinkClicked);
            // 
            // pnlR3
            // 
            this.pnlR3.Controls.Add(this.txbEP3);
            this.pnlR3.Controls.Add(this.cbbEP3);
            this.pnlR3.Controls.Add(this.ckbEP3);
            this.pnlR3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlR3.Location = new System.Drawing.Point(42, 65);
            this.pnlR3.Name = "pnlR3";
            this.pnlR3.Size = new System.Drawing.Size(370, 25);
            this.pnlR3.TabIndex = 26;
            // 
            // txbEP3
            // 
            this.txbEP3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txbEP3.Location = new System.Drawing.Point(0, 0);
            this.txbEP3.Margin = new System.Windows.Forms.Padding(4);
            this.txbEP3.Name = "txbEP3";
            this.txbEP3.Size = new System.Drawing.Size(352, 25);
            this.txbEP3.TabIndex = 3;
            this.txbEP3.Tag = "2";
            this.txbEP3.Visible = false;
            this.txbEP3.Validated += new System.EventHandler(this.ParamChanged);
            // 
            // cbbEP3
            // 
            this.cbbEP3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbbEP3.FormattingEnabled = true;
            this.cbbEP3.Location = new System.Drawing.Point(0, 0);
            this.cbbEP3.Margin = new System.Windows.Forms.Padding(4);
            this.cbbEP3.Name = "cbbEP3";
            this.cbbEP3.Size = new System.Drawing.Size(352, 23);
            this.cbbEP3.TabIndex = 3;
            this.cbbEP3.Tag = "2";
            this.cbbEP3.Visible = false;
            this.cbbEP3.Validated += new System.EventHandler(this.ParamChanged);
            // 
            // ckbEP3
            // 
            this.ckbEP3.AutoSize = true;
            this.ckbEP3.Dock = System.Windows.Forms.DockStyle.Right;
            this.ckbEP3.Location = new System.Drawing.Point(352, 0);
            this.ckbEP3.Margin = new System.Windows.Forms.Padding(4);
            this.ckbEP3.Name = "ckbEP3";
            this.ckbEP3.Size = new System.Drawing.Size(18, 25);
            this.ckbEP3.TabIndex = 3;
            this.ckbEP3.Tag = "2";
            this.ckbEP3.UseVisualStyleBackColor = true;
            this.ckbEP3.Visible = false;
            this.ckbEP3.CheckedChanged += new System.EventHandler(this.ParamChanged);
            // 
            // lklEP3
            // 
            this.lklEP3.AutoSize = true;
            this.lklEP3.Dock = System.Windows.Forms.DockStyle.Left;
            this.lklEP3.Location = new System.Drawing.Point(4, 62);
            this.lklEP3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lklEP3.Name = "lklEP3";
            this.lklEP3.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.lklEP3.Size = new System.Drawing.Size(31, 31);
            this.lklEP3.TabIndex = 9;
            this.lklEP3.TabStop = true;
            this.lklEP3.Tag = "2";
            this.lklEP3.Text = "EP3";
            this.lklEP3.Visible = false;
            this.lklEP3.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lkl_LinkClicked);
            // 
            // pnlR2
            // 
            this.pnlR2.Controls.Add(this.txbEP2);
            this.pnlR2.Controls.Add(this.cbbEP2);
            this.pnlR2.Controls.Add(this.ckbEP2);
            this.pnlR2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlR2.Location = new System.Drawing.Point(42, 34);
            this.pnlR2.Name = "pnlR2";
            this.pnlR2.Size = new System.Drawing.Size(370, 25);
            this.pnlR2.TabIndex = 26;
            // 
            // txbEP2
            // 
            this.txbEP2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txbEP2.Location = new System.Drawing.Point(0, 0);
            this.txbEP2.Margin = new System.Windows.Forms.Padding(4);
            this.txbEP2.Name = "txbEP2";
            this.txbEP2.Size = new System.Drawing.Size(352, 25);
            this.txbEP2.TabIndex = 2;
            this.txbEP2.Tag = "1";
            this.txbEP2.Visible = false;
            this.txbEP2.Validated += new System.EventHandler(this.ParamChanged);
            // 
            // cbbEP2
            // 
            this.cbbEP2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbbEP2.FormattingEnabled = true;
            this.cbbEP2.Location = new System.Drawing.Point(0, 0);
            this.cbbEP2.Margin = new System.Windows.Forms.Padding(4);
            this.cbbEP2.Name = "cbbEP2";
            this.cbbEP2.Size = new System.Drawing.Size(352, 23);
            this.cbbEP2.TabIndex = 2;
            this.cbbEP2.Tag = "1";
            this.cbbEP2.Visible = false;
            this.cbbEP2.Validated += new System.EventHandler(this.ParamChanged);
            // 
            // ckbEP2
            // 
            this.ckbEP2.AutoSize = true;
            this.ckbEP2.Dock = System.Windows.Forms.DockStyle.Right;
            this.ckbEP2.Location = new System.Drawing.Point(352, 0);
            this.ckbEP2.Margin = new System.Windows.Forms.Padding(4);
            this.ckbEP2.Name = "ckbEP2";
            this.ckbEP2.Size = new System.Drawing.Size(18, 25);
            this.ckbEP2.TabIndex = 2;
            this.ckbEP2.Tag = "1";
            this.ckbEP2.UseVisualStyleBackColor = true;
            this.ckbEP2.Visible = false;
            this.ckbEP2.CheckedChanged += new System.EventHandler(this.ParamChanged);
            // 
            // lklEP2
            // 
            this.lklEP2.AutoSize = true;
            this.lklEP2.Dock = System.Windows.Forms.DockStyle.Left;
            this.lklEP2.Location = new System.Drawing.Point(4, 31);
            this.lklEP2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lklEP2.Name = "lklEP2";
            this.lklEP2.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.lklEP2.Size = new System.Drawing.Size(31, 31);
            this.lklEP2.TabIndex = 9;
            this.lklEP2.TabStop = true;
            this.lklEP2.Tag = "1";
            this.lklEP2.Text = "EP2";
            this.lklEP2.Visible = false;
            this.lklEP2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lkl_LinkClicked);
            // 
            // pnlR1
            // 
            this.pnlR1.Controls.Add(this.txbEP1);
            this.pnlR1.Controls.Add(this.ckbEP1);
            this.pnlR1.Controls.Add(this.cbbEP1);
            this.pnlR1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlR1.Location = new System.Drawing.Point(42, 3);
            this.pnlR1.Name = "pnlR1";
            this.pnlR1.Size = new System.Drawing.Size(370, 25);
            this.pnlR1.TabIndex = 25;
            // 
            // txbEP1
            // 
            this.txbEP1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txbEP1.Location = new System.Drawing.Point(0, 0);
            this.txbEP1.Margin = new System.Windows.Forms.Padding(4);
            this.txbEP1.Name = "txbEP1";
            this.txbEP1.Size = new System.Drawing.Size(352, 25);
            this.txbEP1.TabIndex = 1;
            this.txbEP1.Tag = "0";
            this.txbEP1.Visible = false;
            this.txbEP1.Validated += new System.EventHandler(this.ParamChanged);
            // 
            // ckbEP1
            // 
            this.ckbEP1.AutoSize = true;
            this.ckbEP1.Dock = System.Windows.Forms.DockStyle.Right;
            this.ckbEP1.Location = new System.Drawing.Point(352, 0);
            this.ckbEP1.Margin = new System.Windows.Forms.Padding(4);
            this.ckbEP1.Name = "ckbEP1";
            this.ckbEP1.Size = new System.Drawing.Size(18, 25);
            this.ckbEP1.TabIndex = 1;
            this.ckbEP1.Tag = "0";
            this.ckbEP1.UseVisualStyleBackColor = true;
            this.ckbEP1.Visible = false;
            this.ckbEP1.CheckedChanged += new System.EventHandler(this.ParamChanged);
            // 
            // cbbEP1
            // 
            this.cbbEP1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbbEP1.FormattingEnabled = true;
            this.cbbEP1.Location = new System.Drawing.Point(0, 0);
            this.cbbEP1.Margin = new System.Windows.Forms.Padding(4);
            this.cbbEP1.Name = "cbbEP1";
            this.cbbEP1.Size = new System.Drawing.Size(370, 23);
            this.cbbEP1.TabIndex = 1;
            this.cbbEP1.Tag = "0";
            this.cbbEP1.Visible = false;
            this.cbbEP1.Validated += new System.EventHandler(this.ParamChanged);
            // 
            // lklEP1
            // 
            this.lklEP1.AutoSize = true;
            this.lklEP1.Dock = System.Windows.Forms.DockStyle.Left;
            this.lklEP1.Location = new System.Drawing.Point(4, 0);
            this.lklEP1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lklEP1.Name = "lklEP1";
            this.lklEP1.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.lklEP1.Size = new System.Drawing.Size(31, 31);
            this.lklEP1.TabIndex = 9;
            this.lklEP1.TabStop = true;
            this.lklEP1.Tag = "0";
            this.lklEP1.Text = "EP1";
            this.lklEP1.Visible = false;
            this.lklEP1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lkl_LinkClicked);
            // 
            // cbbEventAbst
            // 
            this.cbbEventAbst.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbbEventAbst.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbEventAbst.DropDownWidth = 300;
            this.cbbEventAbst.Font = new System.Drawing.Font("Verdana", 9F);
            this.cbbEventAbst.FormattingEnabled = true;
            this.cbbEventAbst.Location = new System.Drawing.Point(49, 24);
            this.cbbEventAbst.Margin = new System.Windows.Forms.Padding(4);
            this.cbbEventAbst.Name = "cbbEventAbst";
            this.cbbEventAbst.Size = new System.Drawing.Size(370, 26);
            this.cbbEventAbst.TabIndex = 19;
            this.cbbEventAbst.SelectedIndexChanged += new System.EventHandler(this.cbbEventAbst_SelectedIndexChanged);
            // 
            // txbEventAnno
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.txbEventAnno, 2);
            this.txbEventAnno.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txbEventAnno.Location = new System.Drawing.Point(4, 74);
            this.txbEventAnno.Margin = new System.Windows.Forms.Padding(4);
            this.txbEventAnno.Name = "txbEventAnno";
            this.txbEventAnno.Size = new System.Drawing.Size(415, 25);
            this.txbEventAnno.TabIndex = 14;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label14.Location = new System.Drawing.Point(49, 5);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(370, 15);
            this.label14.TabIndex = 15;
            this.label14.Text = "LGClblEventAbst";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Dock = System.Windows.Forms.DockStyle.Top;
            this.label15.Location = new System.Drawing.Point(0, 0);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(143, 15);
            this.label15.TabIndex = 16;
            this.label15.Text = "LGClblEventDetail";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.label18, 2);
            this.label18.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label18.Location = new System.Drawing.Point(4, 55);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(415, 15);
            this.label18.TabIndex = 17;
            this.label18.Text = "LGClblEventAnno";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label13.Location = new System.Drawing.Point(4, 0);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(37, 20);
            this.label13.TabIndex = 18;
            this.label13.Text = "LGClblEventID";
            // 
            // rtxbEventDetail
            // 
            this.rtxbEventDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxbEventDetail.Font = new System.Drawing.Font("Verdana", 9F);
            this.rtxbEventDetail.Location = new System.Drawing.Point(0, 15);
            this.rtxbEventDetail.Margin = new System.Windows.Forms.Padding(4);
            this.rtxbEventDetail.Name = "rtxbEventDetail";
            this.rtxbEventDetail.ReadOnly = true;
            this.rtxbEventDetail.Size = new System.Drawing.Size(423, 137);
            this.rtxbEventDetail.TabIndex = 20;
            this.rtxbEventDetail.Text = "";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.label13, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.mtxbEventID, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.txbEventAnno, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.cbbEventAbst, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label14, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label18, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(423, 100);
            this.tableLayoutPanel1.TabIndex = 21;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rtxbEventDetail);
            this.panel1.Controls.Add(this.label15);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 100);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(423, 152);
            this.panel1.TabIndex = 22;
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.panel1);
            this.pnlTop.Controls.Add(this.tableLayoutPanel1);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(423, 252);
            this.pnlTop.TabIndex = 23;
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.pnlTop);
            this.pnlMain.Controls.Add(this.gpbEventParam);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(423, 402);
            this.pnlMain.TabIndex = 24;
            // 
            // ParameterPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlMain);
            this.Name = "ParameterPanel";
            this.Size = new System.Drawing.Size(423, 402);
            this.gpbEventParam.ResumeLayout(false);
            this.gpbEventParam.PerformLayout();
            this.tlpParam.ResumeLayout(false);
            this.tlpParam.PerformLayout();
            this.pnlR4.ResumeLayout(false);
            this.pnlR4.PerformLayout();
            this.pnlR3.ResumeLayout(false);
            this.pnlR3.PerformLayout();
            this.pnlR2.ResumeLayout(false);
            this.pnlR2.PerformLayout();
            this.pnlR1.ResumeLayout(false);
            this.pnlR1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MaskedTextBox mtxbEventID;
        private System.Windows.Forms.GroupBox gpbEventParam;
        private System.Windows.Forms.LinkLabel lklEP4;
        private System.Windows.Forms.LinkLabel lklEP3;
        private System.Windows.Forms.LinkLabel lklEP2;
        private System.Windows.Forms.LinkLabel lklEP1;
        private System.Windows.Forms.TextBox txbEP4;
        private System.Windows.Forms.TextBox txbEP3;
        private System.Windows.Forms.TextBox txbEP1;
        private System.Windows.Forms.TextBox txbEP2;
        private System.Windows.Forms.CheckBox ckbEP4;
        private System.Windows.Forms.CheckBox ckbEP3;
        private System.Windows.Forms.CheckBox ckbEP2;
        private System.Windows.Forms.CheckBox ckbEP1;
        private System.Windows.Forms.ComboBox cbbEP4;
        private System.Windows.Forms.ComboBox cbbEP3;
        private System.Windows.Forms.ComboBox cbbEP2;
        private System.Windows.Forms.ComboBox cbbEP1;
        private System.Windows.Forms.Label lblNoParamE;
        private System.Windows.Forms.ComboBox cbbEventAbst;
        private System.Windows.Forms.TextBox txbEventAnno;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.RichTextBox rtxbEventDetail;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.TableLayoutPanel tlpParam;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlR4;
        private System.Windows.Forms.Panel pnlR3;
        private System.Windows.Forms.Panel pnlR2;
        private System.Windows.Forms.Panel pnlR1;
    }
}
