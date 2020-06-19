namespace RelertSharp.GUI
{
    partial class MainWindowTest
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindowTest));
            this.button1 = new System.Windows.Forms.Button();
            this.panelHost = new System.Windows.Forms.Panel();
            this.splitMain = new System.Windows.Forms.SplitContainer();
            this.pnlMainAreaContainer = new System.Windows.Forms.Panel();
            this.splitPickerMain = new System.Windows.Forms.SplitContainer();
            this.pnlPick = new RelertSharp.GUI.Controls.PickPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbPanelBrush = new RelertSharp.GUI.Controls.RbBrushPanel();
            this.rbPanelAttribute = new RelertSharp.GUI.Controls.RbPanelAttribute();
            this.txbCommand = new System.Windows.Forms.TextBox();
            this.toolsMain = new System.Windows.Forms.ToolStrip();
            this.toolBtnMoving = new System.Windows.Forms.ToolStripButton();
            this.toolBtnSelecting = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolBtnBrush = new System.Windows.Forms.ToolStripButton();
            this.toolBtnAttributeBrush = new System.Windows.Forms.ToolStripButton();
            this.splitSide = new System.Windows.Forms.SplitContainer();
            this.pnlMiniMap = new RelertSharp.GUI.Controls.MinimapPanel();
            this.pnlSide = new System.Windows.Forms.Panel();
            this.pnlSideRank = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.maskedTextBox1 = new System.Windows.Forms.MaskedTextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.maskedTextBox2 = new System.Windows.Forms.MaskedTextBox();
            this.maskedTextBox3 = new System.Windows.Forms.MaskedTextBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.comboBox4 = new System.Windows.Forms.ComboBox();
            this.ckbRankPanel = new System.Windows.Forms.CheckBox();
            this.pnlSideDebug = new System.Windows.Forms.Panel();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.ckbDebugPanel = new System.Windows.Forms.CheckBox();
            this.pnlSideLightning = new System.Windows.Forms.Panel();
            this.tlpLightning = new System.Windows.Forms.TableLayoutPanel();
            this.btnLightningRefresh = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.ckbLightningEnable = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbbLightningType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.nmbxLightningRed = new System.Windows.Forms.NumericUpDown();
            this.nmbxLightningGreen = new System.Windows.Forms.NumericUpDown();
            this.nmbxLightningBlue = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.nmbxLightningAmbient = new System.Windows.Forms.NumericUpDown();
            this.nmbxLightningGround = new System.Windows.Forms.NumericUpDown();
            this.nmbxLightningLevel = new System.Windows.Forms.NumericUpDown();
            this.ckbLightningPanel = new System.Windows.Forms.CheckBox();
            this.pnlSideChecking = new System.Windows.Forms.Panel();
            this.tlpChecking = new System.Windows.Forms.TableLayoutPanel();
            this.label17 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.ckbBuildableTiles = new System.Windows.Forms.CheckBox();
            this.ckbGroundPassableTiles = new System.Windows.Forms.CheckBox();
            this.cbbCheckingPanel = new System.Windows.Forms.CheckBox();
            this.pnlSideObjects = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.ckbObjectsPanel = new System.Windows.Forms.CheckBox();
            this.lblx = new System.Windows.Forms.Label();
            this.lbly = new System.Windows.Forms.Label();
            this.lblz = new System.Windows.Forms.Label();
            this.lblMouseX = new System.Windows.Forms.Label();
            this.lblMouseY = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.itemRectangle = new System.Windows.Forms.ToolStripMenuItem();
            this.itemIsometric = new System.Windows.Forms.ToolStripMenuItem();
            this.itemPrecise = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsToolSelect = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiRectSelect = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiIsoSelect = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiPreciseSelect = new System.Windows.Forms.ToolStripMenuItem();
            this.bgwDraw = new System.ComponentModel.BackgroundWorker();
            this.bgwRmbMoving = new System.ComponentModel.BackgroundWorker();
            this.lblSubcell = new System.Windows.Forms.Label();
            this.panelHost.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            this.splitMain.Panel1.SuspendLayout();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();
            this.pnlMainAreaContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitPickerMain)).BeginInit();
            this.splitPickerMain.Panel1.SuspendLayout();
            this.splitPickerMain.Panel2.SuspendLayout();
            this.splitPickerMain.SuspendLayout();
            this.panel1.SuspendLayout();
            this.toolsMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitSide)).BeginInit();
            this.splitSide.Panel1.SuspendLayout();
            this.splitSide.Panel2.SuspendLayout();
            this.splitSide.SuspendLayout();
            this.pnlSide.SuspendLayout();
            this.pnlSideRank.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.pnlSideDebug.SuspendLayout();
            this.pnlSideLightning.SuspendLayout();
            this.tlpLightning.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmbxLightningRed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmbxLightningGreen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmbxLightningBlue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmbxLightningAmbient)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmbxLightningGround)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmbxLightningLevel)).BeginInit();
            this.pnlSideChecking.SuspendLayout();
            this.tlpChecking.SuspendLayout();
            this.pnlSideObjects.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.cmsToolSelect.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(16, 15);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 29);
            this.button1.TabIndex = 1;
            this.button1.Text = "Render";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panelHost
            // 
            this.panelHost.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelHost.Controls.Add(this.splitMain);
            this.panelHost.Location = new System.Drawing.Point(10, 50);
            this.panelHost.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelHost.Name = "panelHost";
            this.panelHost.Size = new System.Drawing.Size(1243, 796);
            this.panelHost.TabIndex = 3;
            // 
            // splitMain
            // 
            this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitMain.Location = new System.Drawing.Point(0, 0);
            this.splitMain.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.splitMain.Name = "splitMain";
            // 
            // splitMain.Panel1
            // 
            this.splitMain.Panel1.Controls.Add(this.pnlMainAreaContainer);
            this.splitMain.Panel1.Controls.Add(this.toolsMain);
            // 
            // splitMain.Panel2
            // 
            this.splitMain.Panel2.Controls.Add(this.splitSide);
            this.splitMain.Size = new System.Drawing.Size(1243, 796);
            this.splitMain.SplitterDistance = 1035;
            this.splitMain.TabIndex = 5;
            // 
            // pnlMainAreaContainer
            // 
            this.pnlMainAreaContainer.Controls.Add(this.splitPickerMain);
            this.pnlMainAreaContainer.Controls.Add(this.txbCommand);
            this.pnlMainAreaContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMainAreaContainer.Location = new System.Drawing.Point(25, 0);
            this.pnlMainAreaContainer.Name = "pnlMainAreaContainer";
            this.pnlMainAreaContainer.Size = new System.Drawing.Size(1010, 796);
            this.pnlMainAreaContainer.TabIndex = 4;
            // 
            // splitPickerMain
            // 
            this.splitPickerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitPickerMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitPickerMain.Location = new System.Drawing.Point(0, 0);
            this.splitPickerMain.Name = "splitPickerMain";
            // 
            // splitPickerMain.Panel1
            // 
            this.splitPickerMain.Panel1.Controls.Add(this.pnlPick);
            // 
            // splitPickerMain.Panel2
            // 
            this.splitPickerMain.Panel2.Controls.Add(this.panel1);
            this.splitPickerMain.Size = new System.Drawing.Size(1010, 771);
            this.splitPickerMain.SplitterDistance = 238;
            this.splitPickerMain.TabIndex = 4;
            // 
            // pnlPick
            // 
            this.pnlPick.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPick.Location = new System.Drawing.Point(0, 0);
            this.pnlPick.Name = "pnlPick";
            this.pnlPick.Size = new System.Drawing.Size(238, 771);
            this.pnlPick.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rbPanelBrush);
            this.panel1.Controls.Add(this.rbPanelAttribute);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(768, 771);
            this.panel1.TabIndex = 2;
            this.panel1.SizeChanged += new System.EventHandler(this.panel1_SizeChanged);
            this.panel1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseClick);
            this.panel1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDoubleClick);
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.panel1.MouseEnter += new System.EventHandler(this.panel1_MouseEnter_1);
            this.panel1.MouseLeave += new System.EventHandler(this.panel1_MouseLeave);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            this.panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp);
            this.panel1.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.panel1_PreviewKeyDown);
            this.panel1.Resize += new System.EventHandler(this.panel1_Resize);
            // 
            // rbPanelBrush
            // 
            this.rbPanelBrush.BackColor = System.Drawing.SystemColors.Control;
            this.rbPanelBrush.Location = new System.Drawing.Point(3, 265);
            this.rbPanelBrush.Name = "rbPanelBrush";
            this.rbPanelBrush.Size = new System.Drawing.Size(260, 257);
            this.rbPanelBrush.TabIndex = 1;
            this.rbPanelBrush.Visible = false;
            // 
            // rbPanelAttribute
            // 
            this.rbPanelAttribute.BackColor = System.Drawing.SystemColors.Control;
            this.rbPanelAttribute.Location = new System.Drawing.Point(3, 2);
            this.rbPanelAttribute.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rbPanelAttribute.Name = "rbPanelAttribute";
            this.rbPanelAttribute.Size = new System.Drawing.Size(540, 258);
            this.rbPanelAttribute.TabIndex = 0;
            this.rbPanelAttribute.Visible = false;
            // 
            // txbCommand
            // 
            this.txbCommand.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txbCommand.Location = new System.Drawing.Point(0, 771);
            this.txbCommand.Name = "txbCommand";
            this.txbCommand.Size = new System.Drawing.Size(1010, 25);
            this.txbCommand.TabIndex = 3;
            this.txbCommand.Text = "/";
            this.txbCommand.Visible = false;
            this.txbCommand.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txbCommand_KeyDown);
            // 
            // toolsMain
            // 
            this.toolsMain.Dock = System.Windows.Forms.DockStyle.Left;
            this.toolsMain.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolBtnMoving,
            this.toolBtnSelecting,
            this.toolStripSeparator1,
            this.toolBtnBrush,
            this.toolBtnAttributeBrush});
            this.toolsMain.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.toolsMain.Location = new System.Drawing.Point(0, 0);
            this.toolsMain.Name = "toolsMain";
            this.toolsMain.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolsMain.Size = new System.Drawing.Size(25, 796);
            this.toolsMain.TabIndex = 3;
            this.toolsMain.Text = "toolStrip1";
            // 
            // toolBtnMoving
            // 
            this.toolBtnMoving.Checked = true;
            this.toolBtnMoving.CheckOnClick = true;
            this.toolBtnMoving.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolBtnMoving.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBtnMoving.Image = global::RelertSharp.GUI.Properties.Resources.btnMoving;
            this.toolBtnMoving.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnMoving.Name = "toolBtnMoving";
            this.toolBtnMoving.Size = new System.Drawing.Size(22, 24);
            this.toolBtnMoving.Tag = "moving";
            this.toolBtnMoving.Text = "RSMainToolBtnMoving";
            this.toolBtnMoving.Click += new System.EventHandler(this.ToolBoxClickHandler);
            // 
            // toolBtnSelecting
            // 
            this.toolBtnSelecting.CheckOnClick = true;
            this.toolBtnSelecting.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBtnSelecting.Image = global::RelertSharp.GUI.Properties.Resources.btnRectSelecting;
            this.toolBtnSelecting.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnSelecting.Name = "toolBtnSelecting";
            this.toolBtnSelecting.Size = new System.Drawing.Size(22, 24);
            this.toolBtnSelecting.Tag = "selecting";
            this.toolBtnSelecting.Text = "RSMainToolBtnSelecting";
            this.toolBtnSelecting.Click += new System.EventHandler(this.ToolBoxClickHandler);
            this.toolBtnSelecting.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ToolBoxRightClickHandler);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(22, 6);
            // 
            // toolBtnBrush
            // 
            this.toolBtnBrush.CheckOnClick = true;
            this.toolBtnBrush.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBtnBrush.Image = global::RelertSharp.GUI.Properties.Resources.btnBrush;
            this.toolBtnBrush.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnBrush.Name = "toolBtnBrush";
            this.toolBtnBrush.Size = new System.Drawing.Size(22, 24);
            this.toolBtnBrush.Tag = "brush";
            this.toolBtnBrush.Text = "RSMainToolBrush";
            this.toolBtnBrush.Click += new System.EventHandler(this.ToolBoxClickHandler);
            // 
            // toolBtnAttributeBrush
            // 
            this.toolBtnAttributeBrush.CheckOnClick = true;
            this.toolBtnAttributeBrush.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBtnAttributeBrush.Image = ((System.Drawing.Image)(resources.GetObject("toolBtnAttributeBrush.Image")));
            this.toolBtnAttributeBrush.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnAttributeBrush.Name = "toolBtnAttributeBrush";
            this.toolBtnAttributeBrush.Size = new System.Drawing.Size(22, 24);
            this.toolBtnAttributeBrush.Tag = "attribute";
            this.toolBtnAttributeBrush.Text = "RSMainToolAttBrush";
            this.toolBtnAttributeBrush.Click += new System.EventHandler(this.ToolBoxClickHandler);
            // 
            // splitSide
            // 
            this.splitSide.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitSide.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitSide.Location = new System.Drawing.Point(0, 0);
            this.splitSide.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.splitSide.Name = "splitSide";
            this.splitSide.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitSide.Panel1
            // 
            this.splitSide.Panel1.Controls.Add(this.pnlMiniMap);
            // 
            // splitSide.Panel2
            // 
            this.splitSide.Panel2.Controls.Add(this.pnlSide);
            this.splitSide.Size = new System.Drawing.Size(204, 796);
            this.splitSide.SplitterDistance = 176;
            this.splitSide.TabIndex = 1;
            // 
            // pnlMiniMap
            // 
            this.pnlMiniMap.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.pnlMiniMap.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pnlMiniMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMiniMap.Location = new System.Drawing.Point(0, 0);
            this.pnlMiniMap.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnlMiniMap.Name = "pnlMiniMap";
            this.pnlMiniMap.Size = new System.Drawing.Size(204, 176);
            this.pnlMiniMap.TabIndex = 4;
            this.pnlMiniMap.SizeChanged += new System.EventHandler(this.pnlMiniMap_SizeChanged);
            this.pnlMiniMap.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pnlMiniMap_MouseClick);
            this.pnlMiniMap.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlMiniMap_MouseDown);
            this.pnlMiniMap.MouseLeave += new System.EventHandler(this.pnlMiniMap_MouseLeave);
            this.pnlMiniMap.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlMiniMap_MouseMove);
            this.pnlMiniMap.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnlMiniMap_MouseUp);
            // 
            // pnlSide
            // 
            this.pnlSide.AutoScroll = true;
            this.pnlSide.Controls.Add(this.pnlSideRank);
            this.pnlSide.Controls.Add(this.pnlSideDebug);
            this.pnlSide.Controls.Add(this.pnlSideLightning);
            this.pnlSide.Controls.Add(this.pnlSideChecking);
            this.pnlSide.Controls.Add(this.pnlSideObjects);
            this.pnlSide.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSide.Location = new System.Drawing.Point(0, 0);
            this.pnlSide.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnlSide.Name = "pnlSide";
            this.pnlSide.Size = new System.Drawing.Size(204, 616);
            this.pnlSide.TabIndex = 5;
            // 
            // pnlSideRank
            // 
            this.pnlSideRank.AutoSize = true;
            this.pnlSideRank.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlSideRank.Controls.Add(this.tableLayoutPanel1);
            this.pnlSideRank.Controls.Add(this.ckbRankPanel);
            this.pnlSideRank.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSideRank.Location = new System.Drawing.Point(0, 905);
            this.pnlSideRank.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnlSideRank.Name = "pnlSideRank";
            this.pnlSideRank.Size = new System.Drawing.Size(183, 220);
            this.pnlSideRank.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.comboBox1, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.maskedTextBox1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label10, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label11, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label12, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label13, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label14, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label15, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.label16, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.maskedTextBox2, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.maskedTextBox3, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.comboBox2, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.comboBox3, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.comboBox4, 1, 6);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 25);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 7;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(183, 195);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // comboBox1
            // 
            this.comboBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(184, 89);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(1, 23);
            this.comboBox1.TabIndex = 1;
            // 
            // maskedTextBox1
            // 
            this.maskedTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.maskedTextBox1.Location = new System.Drawing.Point(184, 2);
            this.maskedTextBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.maskedTextBox1.Mask = "00:00:00";
            this.maskedTextBox1.Name = "maskedTextBox1";
            this.maskedTextBox1.PromptChar = '0';
            this.maskedTextBox1.Size = new System.Drawing.Size(1, 25);
            this.maskedTextBox1.TabIndex = 1;
            this.maskedTextBox1.ValidatingType = typeof(System.DateTime);
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(35, 7);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(143, 15);
            this.label10.TabIndex = 0;
            this.label10.Text = "RSMainSideLblParE";
            // 
            // label11
            // 
            this.label11.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(35, 36);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(143, 15);
            this.label11.TabIndex = 2;
            this.label11.Text = "RSMainSideLblParM";
            // 
            // label12
            // 
            this.label12.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(35, 65);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(143, 15);
            this.label12.TabIndex = 4;
            this.label12.Text = "RSMainSideLblParH";
            // 
            // label13
            // 
            this.label13.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(11, 93);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(167, 15);
            this.label13.TabIndex = 4;
            this.label13.Text = "RSMainSideLblOverTtl";
            // 
            // label14
            // 
            this.label14.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(11, 120);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(167, 15);
            this.label14.TabIndex = 4;
            this.label14.Text = "RSMainSideLblOverMsg";
            // 
            // label15
            // 
            this.label15.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(3, 147);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(175, 15);
            this.label15.TabIndex = 4;
            this.label15.Text = "RSMainSideLblUnderTtl";
            // 
            // label16
            // 
            this.label16.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(3, 174);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(175, 15);
            this.label16.TabIndex = 4;
            this.label16.Text = "RSMainSideLblUnderMsg";
            // 
            // maskedTextBox2
            // 
            this.maskedTextBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.maskedTextBox2.Location = new System.Drawing.Point(184, 31);
            this.maskedTextBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.maskedTextBox2.Mask = "00:00:00";
            this.maskedTextBox2.Name = "maskedTextBox2";
            this.maskedTextBox2.PromptChar = '0';
            this.maskedTextBox2.Size = new System.Drawing.Size(1, 25);
            this.maskedTextBox2.TabIndex = 1;
            // 
            // maskedTextBox3
            // 
            this.maskedTextBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.maskedTextBox3.Location = new System.Drawing.Point(184, 60);
            this.maskedTextBox3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.maskedTextBox3.Mask = "00:00:00";
            this.maskedTextBox3.Name = "maskedTextBox3";
            this.maskedTextBox3.PromptChar = '0';
            this.maskedTextBox3.Size = new System.Drawing.Size(1, 25);
            this.maskedTextBox3.TabIndex = 1;
            // 
            // comboBox2
            // 
            this.comboBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(184, 116);
            this.comboBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(1, 23);
            this.comboBox2.TabIndex = 1;
            // 
            // comboBox3
            // 
            this.comboBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Location = new System.Drawing.Point(184, 143);
            this.comboBox3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(1, 23);
            this.comboBox3.TabIndex = 1;
            // 
            // comboBox4
            // 
            this.comboBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBox4.FormattingEnabled = true;
            this.comboBox4.Location = new System.Drawing.Point(184, 170);
            this.comboBox4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBox4.Name = "comboBox4";
            this.comboBox4.Size = new System.Drawing.Size(1, 23);
            this.comboBox4.TabIndex = 1;
            // 
            // ckbRankPanel
            // 
            this.ckbRankPanel.Appearance = System.Windows.Forms.Appearance.Button;
            this.ckbRankPanel.AutoSize = true;
            this.ckbRankPanel.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ckbRankPanel.Checked = true;
            this.ckbRankPanel.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbRankPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.ckbRankPanel.Location = new System.Drawing.Point(0, 0);
            this.ckbRankPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ckbRankPanel.Name = "ckbRankPanel";
            this.ckbRankPanel.Size = new System.Drawing.Size(183, 25);
            this.ckbRankPanel.TabIndex = 1;
            this.ckbRankPanel.Text = "RSMainSideCkbRank";
            this.ckbRankPanel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ckbRankPanel.UseVisualStyleBackColor = true;
            this.ckbRankPanel.CheckedChanged += new System.EventHandler(this.Panelchecker_CheckedChanged0);
            // 
            // pnlSideDebug
            // 
            this.pnlSideDebug.AutoSize = true;
            this.pnlSideDebug.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlSideDebug.Controls.Add(this.listBox1);
            this.pnlSideDebug.Controls.Add(this.ckbDebugPanel);
            this.pnlSideDebug.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSideDebug.Location = new System.Drawing.Point(0, 651);
            this.pnlSideDebug.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnlSideDebug.Name = "pnlSideDebug";
            this.pnlSideDebug.Size = new System.Drawing.Size(183, 254);
            this.pnlSideDebug.TabIndex = 5;
            // 
            // listBox1
            // 
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.HorizontalScrollbar = true;
            this.listBox1.ItemHeight = 15;
            this.listBox1.Location = new System.Drawing.Point(0, 25);
            this.listBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(183, 229);
            this.listBox1.TabIndex = 3;
            // 
            // ckbDebugPanel
            // 
            this.ckbDebugPanel.Appearance = System.Windows.Forms.Appearance.Button;
            this.ckbDebugPanel.AutoSize = true;
            this.ckbDebugPanel.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ckbDebugPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.ckbDebugPanel.Location = new System.Drawing.Point(0, 0);
            this.ckbDebugPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ckbDebugPanel.Name = "ckbDebugPanel";
            this.ckbDebugPanel.Size = new System.Drawing.Size(183, 25);
            this.ckbDebugPanel.TabIndex = 4;
            this.ckbDebugPanel.Text = "RSMainSideCkbDebug";
            this.ckbDebugPanel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ckbDebugPanel.UseVisualStyleBackColor = true;
            this.ckbDebugPanel.CheckedChanged += new System.EventHandler(this.Panelchecker_CheckedChanged0);
            // 
            // pnlSideLightning
            // 
            this.pnlSideLightning.AutoSize = true;
            this.pnlSideLightning.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlSideLightning.Controls.Add(this.tlpLightning);
            this.pnlSideLightning.Controls.Add(this.ckbLightningPanel);
            this.pnlSideLightning.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSideLightning.Location = new System.Drawing.Point(0, 374);
            this.pnlSideLightning.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnlSideLightning.Name = "pnlSideLightning";
            this.pnlSideLightning.Size = new System.Drawing.Size(183, 277);
            this.pnlSideLightning.TabIndex = 2;
            // 
            // tlpLightning
            // 
            this.tlpLightning.AutoSize = true;
            this.tlpLightning.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlpLightning.ColumnCount = 2;
            this.tlpLightning.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpLightning.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpLightning.Controls.Add(this.btnLightningRefresh, 0, 8);
            this.tlpLightning.Controls.Add(this.label1, 0, 0);
            this.tlpLightning.Controls.Add(this.ckbLightningEnable, 1, 0);
            this.tlpLightning.Controls.Add(this.label2, 0, 1);
            this.tlpLightning.Controls.Add(this.cbbLightningType, 1, 1);
            this.tlpLightning.Controls.Add(this.label3, 0, 2);
            this.tlpLightning.Controls.Add(this.label4, 0, 3);
            this.tlpLightning.Controls.Add(this.label5, 0, 4);
            this.tlpLightning.Controls.Add(this.nmbxLightningRed, 1, 2);
            this.tlpLightning.Controls.Add(this.nmbxLightningGreen, 1, 3);
            this.tlpLightning.Controls.Add(this.nmbxLightningBlue, 1, 4);
            this.tlpLightning.Controls.Add(this.label6, 0, 5);
            this.tlpLightning.Controls.Add(this.label7, 0, 6);
            this.tlpLightning.Controls.Add(this.label8, 0, 7);
            this.tlpLightning.Controls.Add(this.nmbxLightningAmbient, 1, 5);
            this.tlpLightning.Controls.Add(this.nmbxLightningGround, 1, 6);
            this.tlpLightning.Controls.Add(this.nmbxLightningLevel, 1, 7);
            this.tlpLightning.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpLightning.Location = new System.Drawing.Point(0, 25);
            this.tlpLightning.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tlpLightning.Name = "tlpLightning";
            this.tlpLightning.RowCount = 9;
            this.tlpLightning.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpLightning.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpLightning.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpLightning.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpLightning.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpLightning.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpLightning.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpLightning.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpLightning.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpLightning.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpLightning.Size = new System.Drawing.Size(183, 252);
            this.tlpLightning.TabIndex = 1;
            // 
            // btnLightningRefresh
            // 
            this.tlpLightning.SetColumnSpan(this.btnLightningRefresh, 2);
            this.btnLightningRefresh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnLightningRefresh.Enabled = false;
            this.btnLightningRefresh.Location = new System.Drawing.Point(3, 225);
            this.btnLightningRefresh.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnLightningRefresh.Name = "btnLightningRefresh";
            this.btnLightningRefresh.Size = new System.Drawing.Size(177, 25);
            this.btnLightningRefresh.TabIndex = 6;
            this.btnLightningRefresh.Text = "RSMainSideLblRefreshLight";
            this.btnLightningRefresh.UseVisualStyleBackColor = true;
            this.btnLightningRefresh.Click += new System.EventHandler(this.btnLightningRefresh_Click);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(159, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "RSMainSideLblEnable";
            // 
            // ckbLightningEnable
            // 
            this.ckbLightningEnable.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ckbLightningEnable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ckbLightningEnable.Location = new System.Drawing.Point(168, 2);
            this.ckbLightningEnable.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ckbLightningEnable.Name = "ckbLightningEnable";
            this.ckbLightningEnable.Size = new System.Drawing.Size(12, 18);
            this.ckbLightningEnable.TabIndex = 1;
            this.ckbLightningEnable.UseVisualStyleBackColor = true;
            this.ckbLightningEnable.CheckedChanged += new System.EventHandler(this.ckbLight_CheckedChanged);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(143, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "RSMainSideLblType";
            // 
            // cbbLightningType
            // 
            this.cbbLightningType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbbLightningType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbLightningType.FormattingEnabled = true;
            this.cbbLightningType.Items.AddRange(new object[] {
            "Normal",
            "LightningStorm",
            "Dominator"});
            this.cbbLightningType.Location = new System.Drawing.Point(168, 24);
            this.cbbLightningType.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbbLightningType.Name = "cbbLightningType";
            this.cbbLightningType.Size = new System.Drawing.Size(12, 23);
            this.cbbLightningType.TabIndex = 3;
            this.cbbLightningType.SelectedIndexChanged += new System.EventHandler(this.ckbLightningType_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(43, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(119, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "RSMainSideLblR";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(43, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(119, 15);
            this.label4.TabIndex = 4;
            this.label4.Text = "RSMainSideLblG";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(43, 114);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(119, 15);
            this.label5.TabIndex = 4;
            this.label5.Text = "RSMainSideLblB";
            // 
            // nmbxLightningRed
            // 
            this.nmbxLightningRed.DecimalPlaces = 6;
            this.nmbxLightningRed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nmbxLightningRed.Enabled = false;
            this.nmbxLightningRed.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nmbxLightningRed.Location = new System.Drawing.Point(168, 51);
            this.nmbxLightningRed.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.nmbxLightningRed.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nmbxLightningRed.Name = "nmbxLightningRed";
            this.nmbxLightningRed.Size = new System.Drawing.Size(12, 25);
            this.nmbxLightningRed.TabIndex = 5;
            this.nmbxLightningRed.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nmbxLightningRed.ValueChanged += new System.EventHandler(this.nmbxLightningRed_ValueChanged);
            // 
            // nmbxLightningGreen
            // 
            this.nmbxLightningGreen.DecimalPlaces = 6;
            this.nmbxLightningGreen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nmbxLightningGreen.Enabled = false;
            this.nmbxLightningGreen.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nmbxLightningGreen.Location = new System.Drawing.Point(168, 80);
            this.nmbxLightningGreen.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.nmbxLightningGreen.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nmbxLightningGreen.Name = "nmbxLightningGreen";
            this.nmbxLightningGreen.Size = new System.Drawing.Size(12, 25);
            this.nmbxLightningGreen.TabIndex = 5;
            this.nmbxLightningGreen.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nmbxLightningGreen.ValueChanged += new System.EventHandler(this.nmbxLightningRed_ValueChanged);
            // 
            // nmbxLightningBlue
            // 
            this.nmbxLightningBlue.DecimalPlaces = 6;
            this.nmbxLightningBlue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nmbxLightningBlue.Enabled = false;
            this.nmbxLightningBlue.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nmbxLightningBlue.Location = new System.Drawing.Point(168, 109);
            this.nmbxLightningBlue.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.nmbxLightningBlue.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nmbxLightningBlue.Name = "nmbxLightningBlue";
            this.nmbxLightningBlue.Size = new System.Drawing.Size(12, 25);
            this.nmbxLightningBlue.TabIndex = 5;
            this.nmbxLightningBlue.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nmbxLightningBlue.ValueChanged += new System.EventHandler(this.nmbxLightningRed_ValueChanged);
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(27, 143);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(135, 15);
            this.label6.TabIndex = 4;
            this.label6.Text = "RSMainSideLblAmb";
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(27, 172);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(135, 15);
            this.label7.TabIndex = 4;
            this.label7.Text = "RSMainSideLblGnd";
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(27, 201);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(135, 15);
            this.label8.TabIndex = 4;
            this.label8.Text = "RSMainSideLblLvl";
            // 
            // nmbxLightningAmbient
            // 
            this.nmbxLightningAmbient.DecimalPlaces = 6;
            this.nmbxLightningAmbient.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nmbxLightningAmbient.Enabled = false;
            this.nmbxLightningAmbient.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nmbxLightningAmbient.Location = new System.Drawing.Point(168, 138);
            this.nmbxLightningAmbient.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.nmbxLightningAmbient.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nmbxLightningAmbient.Name = "nmbxLightningAmbient";
            this.nmbxLightningAmbient.Size = new System.Drawing.Size(12, 25);
            this.nmbxLightningAmbient.TabIndex = 5;
            this.nmbxLightningAmbient.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nmbxLightningAmbient.ValueChanged += new System.EventHandler(this.nmbxLightningRed_ValueChanged);
            // 
            // nmbxLightningGround
            // 
            this.nmbxLightningGround.DecimalPlaces = 6;
            this.nmbxLightningGround.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nmbxLightningGround.Enabled = false;
            this.nmbxLightningGround.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nmbxLightningGround.Location = new System.Drawing.Point(168, 167);
            this.nmbxLightningGround.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.nmbxLightningGround.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nmbxLightningGround.Name = "nmbxLightningGround";
            this.nmbxLightningGround.Size = new System.Drawing.Size(12, 25);
            this.nmbxLightningGround.TabIndex = 5;
            this.nmbxLightningGround.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nmbxLightningGround.ValueChanged += new System.EventHandler(this.nmbxLightningRed_ValueChanged);
            // 
            // nmbxLightningLevel
            // 
            this.nmbxLightningLevel.DecimalPlaces = 6;
            this.nmbxLightningLevel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nmbxLightningLevel.Enabled = false;
            this.nmbxLightningLevel.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.nmbxLightningLevel.Location = new System.Drawing.Point(168, 196);
            this.nmbxLightningLevel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.nmbxLightningLevel.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nmbxLightningLevel.Name = "nmbxLightningLevel";
            this.nmbxLightningLevel.Size = new System.Drawing.Size(12, 25);
            this.nmbxLightningLevel.TabIndex = 5;
            this.nmbxLightningLevel.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nmbxLightningLevel.ValueChanged += new System.EventHandler(this.nmbxLightningRed_ValueChanged);
            // 
            // ckbLightningPanel
            // 
            this.ckbLightningPanel.Appearance = System.Windows.Forms.Appearance.Button;
            this.ckbLightningPanel.AutoSize = true;
            this.ckbLightningPanel.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ckbLightningPanel.Checked = true;
            this.ckbLightningPanel.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbLightningPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.ckbLightningPanel.Location = new System.Drawing.Point(0, 0);
            this.ckbLightningPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ckbLightningPanel.Name = "ckbLightningPanel";
            this.ckbLightningPanel.Size = new System.Drawing.Size(183, 25);
            this.ckbLightningPanel.TabIndex = 0;
            this.ckbLightningPanel.Text = "RSMainSideCkbLight";
            this.ckbLightningPanel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ckbLightningPanel.UseVisualStyleBackColor = true;
            this.ckbLightningPanel.CheckedChanged += new System.EventHandler(this.Panelchecker_CheckedChanged0);
            // 
            // pnlSideChecking
            // 
            this.pnlSideChecking.AutoSize = true;
            this.pnlSideChecking.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlSideChecking.Controls.Add(this.tlpChecking);
            this.pnlSideChecking.Controls.Add(this.cbbCheckingPanel);
            this.pnlSideChecking.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSideChecking.Location = new System.Drawing.Point(0, 305);
            this.pnlSideChecking.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnlSideChecking.Name = "pnlSideChecking";
            this.pnlSideChecking.Size = new System.Drawing.Size(183, 69);
            this.pnlSideChecking.TabIndex = 3;
            // 
            // tlpChecking
            // 
            this.tlpChecking.AutoSize = true;
            this.tlpChecking.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlpChecking.ColumnCount = 2;
            this.tlpChecking.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpChecking.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpChecking.Controls.Add(this.label17, 0, 1);
            this.tlpChecking.Controls.Add(this.label9, 0, 0);
            this.tlpChecking.Controls.Add(this.ckbBuildableTiles, 1, 0);
            this.tlpChecking.Controls.Add(this.ckbGroundPassableTiles, 1, 1);
            this.tlpChecking.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpChecking.Location = new System.Drawing.Point(0, 25);
            this.tlpChecking.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tlpChecking.Name = "tlpChecking";
            this.tlpChecking.RowCount = 2;
            this.tlpChecking.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpChecking.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpChecking.Size = new System.Drawing.Size(183, 44);
            this.tlpChecking.TabIndex = 1;
            // 
            // label17
            // 
            this.label17.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(11, 25);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(175, 15);
            this.label17.TabIndex = 3;
            this.label17.Text = "RSMainSideLblPassable";
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 3);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(183, 15);
            this.label9.TabIndex = 1;
            this.label9.Text = "RSMainSideLblBuildable";
            // 
            // ckbBuildableTiles
            // 
            this.ckbBuildableTiles.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ckbBuildableTiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ckbBuildableTiles.Location = new System.Drawing.Point(192, 2);
            this.ckbBuildableTiles.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ckbBuildableTiles.Name = "ckbBuildableTiles";
            this.ckbBuildableTiles.Size = new System.Drawing.Size(1, 18);
            this.ckbBuildableTiles.TabIndex = 2;
            this.ckbBuildableTiles.UseVisualStyleBackColor = true;
            this.ckbBuildableTiles.CheckedChanged += new System.EventHandler(this.ckbBuildableTiles_CheckedChanged);
            // 
            // ckbGroundPassableTiles
            // 
            this.ckbGroundPassableTiles.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ckbGroundPassableTiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ckbGroundPassableTiles.Location = new System.Drawing.Point(192, 24);
            this.ckbGroundPassableTiles.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ckbGroundPassableTiles.Name = "ckbGroundPassableTiles";
            this.ckbGroundPassableTiles.Size = new System.Drawing.Size(1, 18);
            this.ckbGroundPassableTiles.TabIndex = 2;
            this.ckbGroundPassableTiles.UseVisualStyleBackColor = true;
            this.ckbGroundPassableTiles.CheckedChanged += new System.EventHandler(this.ckbGroundPassableTiles_CheckedChanged);
            // 
            // cbbCheckingPanel
            // 
            this.cbbCheckingPanel.Appearance = System.Windows.Forms.Appearance.Button;
            this.cbbCheckingPanel.AutoSize = true;
            this.cbbCheckingPanel.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cbbCheckingPanel.Checked = true;
            this.cbbCheckingPanel.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbbCheckingPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbbCheckingPanel.Location = new System.Drawing.Point(0, 0);
            this.cbbCheckingPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbbCheckingPanel.Name = "cbbCheckingPanel";
            this.cbbCheckingPanel.Size = new System.Drawing.Size(183, 25);
            this.cbbCheckingPanel.TabIndex = 0;
            this.cbbCheckingPanel.Text = "RSMainSideCkbCheck";
            this.cbbCheckingPanel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cbbCheckingPanel.UseVisualStyleBackColor = true;
            this.cbbCheckingPanel.CheckedChanged += new System.EventHandler(this.Panelchecker_CheckedChanged0);
            // 
            // pnlSideObjects
            // 
            this.pnlSideObjects.AutoSize = true;
            this.pnlSideObjects.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlSideObjects.Controls.Add(this.tabControl1);
            this.pnlSideObjects.Controls.Add(this.ckbObjectsPanel);
            this.pnlSideObjects.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSideObjects.Location = new System.Drawing.Point(0, 0);
            this.pnlSideObjects.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnlSideObjects.Name = "pnlSideObjects";
            this.pnlSideObjects.Size = new System.Drawing.Size(183, 305);
            this.pnlSideObjects.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl1.Location = new System.Drawing.Point(0, 25);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(183, 280);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage1.Size = new System.Drawing.Size(175, 251);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage2.Size = new System.Drawing.Size(196, 251);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // ckbObjectsPanel
            // 
            this.ckbObjectsPanel.Appearance = System.Windows.Forms.Appearance.Button;
            this.ckbObjectsPanel.AutoSize = true;
            this.ckbObjectsPanel.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ckbObjectsPanel.Checked = true;
            this.ckbObjectsPanel.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbObjectsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.ckbObjectsPanel.Location = new System.Drawing.Point(0, 0);
            this.ckbObjectsPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ckbObjectsPanel.Name = "ckbObjectsPanel";
            this.ckbObjectsPanel.Size = new System.Drawing.Size(183, 25);
            this.ckbObjectsPanel.TabIndex = 1;
            this.ckbObjectsPanel.Text = "RSMainSideCkbMapObj";
            this.ckbObjectsPanel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ckbObjectsPanel.UseVisualStyleBackColor = true;
            this.ckbObjectsPanel.CheckedChanged += new System.EventHandler(this.Panelchecker_CheckedChanged0);
            // 
            // lblx
            // 
            this.lblx.AutoSize = true;
            this.lblx.Location = new System.Drawing.Point(123, 22);
            this.lblx.Name = "lblx";
            this.lblx.Size = new System.Drawing.Size(39, 15);
            this.lblx.TabIndex = 4;
            this.lblx.Text = "X : ";
            // 
            // lbly
            // 
            this.lbly.AutoSize = true;
            this.lbly.Location = new System.Drawing.Point(221, 22);
            this.lbly.Name = "lbly";
            this.lbly.Size = new System.Drawing.Size(39, 15);
            this.lbly.TabIndex = 4;
            this.lbly.Text = "Y : ";
            // 
            // lblz
            // 
            this.lblz.AutoSize = true;
            this.lblz.Location = new System.Drawing.Point(321, 22);
            this.lblz.Name = "lblz";
            this.lblz.Size = new System.Drawing.Size(39, 15);
            this.lblz.TabIndex = 4;
            this.lblz.Text = "Z : ";
            // 
            // lblMouseX
            // 
            this.lblMouseX.AutoSize = true;
            this.lblMouseX.Location = new System.Drawing.Point(655, 22);
            this.lblMouseX.Name = "lblMouseX";
            this.lblMouseX.Size = new System.Drawing.Size(71, 15);
            this.lblMouseX.TabIndex = 4;
            this.lblMouseX.Text = "MouseX :";
            // 
            // lblMouseY
            // 
            this.lblMouseY.AutoSize = true;
            this.lblMouseY.Location = new System.Drawing.Point(813, 22);
            this.lblMouseY.Name = "lblMouseY";
            this.lblMouseY.Size = new System.Drawing.Size(71, 15);
            this.lblMouseY.TabIndex = 4;
            this.lblMouseY.Text = "MouseY :";
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(1045, 15);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(100, 29);
            this.button2.TabIndex = 5;
            this.button2.Text = "Logic";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Location = new System.Drawing.Point(1153, 15);
            this.button3.Margin = new System.Windows.Forms.Padding(4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(100, 29);
            this.button3.TabIndex = 6;
            this.button3.Text = "INI";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // itemRectangle
            // 
            this.itemRectangle.Name = "itemRectangle";
            this.itemRectangle.Size = new System.Drawing.Size(276, 26);
            this.itemRectangle.Text = "RSMainTooltsmiRectangle";
            // 
            // itemIsometric
            // 
            this.itemIsometric.Name = "itemIsometric";
            this.itemIsometric.Size = new System.Drawing.Size(276, 26);
            this.itemIsometric.Text = "RSMainTooltsmiIsometric";
            // 
            // itemPrecise
            // 
            this.itemPrecise.Name = "itemPrecise";
            this.itemPrecise.Size = new System.Drawing.Size(276, 26);
            this.itemPrecise.Text = "RSMainTooltsmiPrecise";
            // 
            // cmsToolSelect
            // 
            this.cmsToolSelect.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmsToolSelect.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiRectSelect,
            this.tsmiIsoSelect,
            this.tsmiPreciseSelect});
            this.cmsToolSelect.Name = "cmsToolSelect";
            this.cmsToolSelect.Size = new System.Drawing.Size(275, 82);
            // 
            // tsmiRectSelect
            // 
            this.tsmiRectSelect.Image = global::RelertSharp.GUI.Properties.Resources.btnRectSelecting;
            this.tsmiRectSelect.Name = "tsmiRectSelect";
            this.tsmiRectSelect.Size = new System.Drawing.Size(274, 26);
            this.tsmiRectSelect.Text = "RSMainTooltsmiRectangle";
            this.tsmiRectSelect.Click += new System.EventHandler(this.tsmiRectSelect_Click);
            // 
            // tsmiIsoSelect
            // 
            this.tsmiIsoSelect.Image = global::RelertSharp.GUI.Properties.Resources.btnIsoSelecting;
            this.tsmiIsoSelect.Name = "tsmiIsoSelect";
            this.tsmiIsoSelect.Size = new System.Drawing.Size(274, 26);
            this.tsmiIsoSelect.Text = "RSMainTooltsmiIsometric";
            this.tsmiIsoSelect.Click += new System.EventHandler(this.tsmiIsoSelect_Click);
            // 
            // tsmiPreciseSelect
            // 
            this.tsmiPreciseSelect.Image = global::RelertSharp.GUI.Properties.Resources.btnPrecSelecting;
            this.tsmiPreciseSelect.Name = "tsmiPreciseSelect";
            this.tsmiPreciseSelect.Size = new System.Drawing.Size(274, 26);
            this.tsmiPreciseSelect.Text = "RSMainTooltsmiPrecise";
            this.tsmiPreciseSelect.Click += new System.EventHandler(this.tsmiPreciseSelect_Click);
            // 
            // bgwDraw
            // 
            this.bgwDraw.WorkerReportsProgress = true;
            this.bgwDraw.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwDraw_DoWork);
            this.bgwDraw.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgwDraw_ProgressChanged);
            // 
            // bgwRmbMoving
            // 
            this.bgwRmbMoving.WorkerSupportsCancellation = true;
            this.bgwRmbMoving.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwRmbMoving_DoWork);
            // 
            // lblSubcell
            // 
            this.lblSubcell.AutoSize = true;
            this.lblSubcell.Location = new System.Drawing.Point(419, 22);
            this.lblSubcell.Name = "lblSubcell";
            this.lblSubcell.Size = new System.Drawing.Size(71, 15);
            this.lblSubcell.TabIndex = 4;
            this.lblSubcell.Text = "Subcell:";
            // 
            // MainWindowTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1269, 859);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.lblMouseY);
            this.Controls.Add(this.lblMouseX);
            this.Controls.Add(this.lblSubcell);
            this.Controls.Add(this.lblz);
            this.Controls.Add(this.lbly);
            this.Controls.Add(this.lblx);
            this.Controls.Add(this.panelHost);
            this.Controls.Add(this.button1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainWindowTest";
            this.Text = "RSMainTitle";
            this.panelHost.ResumeLayout(false);
            this.splitMain.Panel1.ResumeLayout(false);
            this.splitMain.Panel1.PerformLayout();
            this.splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
            this.splitMain.ResumeLayout(false);
            this.pnlMainAreaContainer.ResumeLayout(false);
            this.pnlMainAreaContainer.PerformLayout();
            this.splitPickerMain.Panel1.ResumeLayout(false);
            this.splitPickerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitPickerMain)).EndInit();
            this.splitPickerMain.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.toolsMain.ResumeLayout(false);
            this.toolsMain.PerformLayout();
            this.splitSide.Panel1.ResumeLayout(false);
            this.splitSide.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitSide)).EndInit();
            this.splitSide.ResumeLayout(false);
            this.pnlSide.ResumeLayout(false);
            this.pnlSide.PerformLayout();
            this.pnlSideRank.ResumeLayout(false);
            this.pnlSideRank.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.pnlSideDebug.ResumeLayout(false);
            this.pnlSideDebug.PerformLayout();
            this.pnlSideLightning.ResumeLayout(false);
            this.pnlSideLightning.PerformLayout();
            this.tlpLightning.ResumeLayout(false);
            this.tlpLightning.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmbxLightningRed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmbxLightningGreen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmbxLightningBlue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmbxLightningAmbient)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmbxLightningGround)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmbxLightningLevel)).EndInit();
            this.pnlSideChecking.ResumeLayout(false);
            this.pnlSideChecking.PerformLayout();
            this.tlpChecking.ResumeLayout(false);
            this.tlpChecking.PerformLayout();
            this.pnlSideObjects.ResumeLayout(false);
            this.pnlSideObjects.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.cmsToolSelect.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panelHost;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label lblx;
        private System.Windows.Forms.Label lbly;
        private System.Windows.Forms.Label lblz;
        private System.Windows.Forms.Label lblMouseX;
        private System.Windows.Forms.Label lblMouseY;
        private RelertSharp.GUI.Controls.MinimapPanel pnlMiniMap;
        private System.Windows.Forms.Panel pnlSideDebug;
        private System.Windows.Forms.Panel pnlSideLightning;
        private System.Windows.Forms.CheckBox ckbLightningPanel;
        private System.Windows.Forms.TableLayoutPanel tlpLightning;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox ckbLightningEnable;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbbLightningType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nmbxLightningRed;
        private System.Windows.Forms.NumericUpDown nmbxLightningGreen;
        private System.Windows.Forms.NumericUpDown nmbxLightningBlue;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown nmbxLightningAmbient;
        private System.Windows.Forms.NumericUpDown nmbxLightningGround;
        private System.Windows.Forms.NumericUpDown nmbxLightningLevel;
        private System.Windows.Forms.Panel pnlSide;
        private System.Windows.Forms.CheckBox ckbDebugPanel;
        private System.Windows.Forms.SplitContainer splitMain;
        private System.Windows.Forms.Panel pnlSideRank;
        private System.Windows.Forms.CheckBox ckbRankPanel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.MaskedTextBox maskedTextBox1;
        private System.Windows.Forms.MaskedTextBox maskedTextBox2;
        private System.Windows.Forms.MaskedTextBox maskedTextBox3;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.ComboBox comboBox4;
        private System.Windows.Forms.Button btnLightningRefresh;
        private System.Windows.Forms.Panel pnlSideChecking;
        private System.Windows.Forms.TableLayoutPanel tlpChecking;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox ckbBuildableTiles;
        private System.Windows.Forms.CheckBox cbbCheckingPanel;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.CheckBox ckbGroundPassableTiles;
        private System.Windows.Forms.Panel pnlSideObjects;
        private System.Windows.Forms.CheckBox ckbObjectsPanel;
        private System.Windows.Forms.SplitContainer splitSide;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private Controls.RbPanelAttribute rbPanelAttribute;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ToolStrip toolsMain;
        private System.Windows.Forms.ToolStripButton toolBtnMoving;
        private System.Windows.Forms.ToolStripButton toolBtnAttributeBrush;
        private System.Windows.Forms.ToolStripButton toolBtnBrush;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem itemRectangle;
        private System.Windows.Forms.ToolStripMenuItem itemIsometric;
        private System.Windows.Forms.ToolStripMenuItem itemPrecise;
        private System.Windows.Forms.ToolStripButton toolBtnSelecting;
        private System.Windows.Forms.ContextMenuStrip cmsToolSelect;
        private System.Windows.Forms.ToolStripMenuItem tsmiRectSelect;
        private System.Windows.Forms.ToolStripMenuItem tsmiIsoSelect;
        private System.Windows.Forms.ToolStripMenuItem tsmiPreciseSelect;
        private System.Windows.Forms.Panel pnlMainAreaContainer;
        private System.Windows.Forms.TextBox txbCommand;
        private System.ComponentModel.BackgroundWorker bgwDraw;
        private System.ComponentModel.BackgroundWorker bgwRmbMoving;
        private RelertSharp.GUI.Controls.PickPanel pnlPick;
        private System.Windows.Forms.SplitContainer splitPickerMain;
        private System.Windows.Forms.Label lblSubcell;
        private Controls.RbBrushPanel rbPanelBrush;
    }
}