namespace RelertSharp.GUI.Controls
{
    partial class PickPanel
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
            this.tbcMain = new System.Windows.Forms.TabControl();
            this.tbpObject = new System.Windows.Forms.TabPage();
            this.splitObjects = new System.Windows.Forms.SplitContainer();
            this.trvObject = new System.Windows.Forms.TreeView();
            this.imgMain = new System.Windows.Forms.ImageList(this.components);
            this.pnlObjectProp = new System.Windows.Forms.Panel();
            this.tlpObjectProps = new System.Windows.Forms.TableLayoutPanel();
            this.label9 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblBridge = new System.Windows.Forms.Label();
            this.lblRecruit = new System.Windows.Forms.Label();
            this.lblUnitRebuild = new System.Windows.Forms.Label();
            this.lblSell = new System.Windows.Forms.Label();
            this.lblFollow = new System.Windows.Forms.Label();
            this.lblRepair = new System.Windows.Forms.Label();
            this.lblPower = new System.Windows.Forms.Label();
            this.lblSpotlight = new System.Windows.Forms.Label();
            this.lblUpNum = new System.Windows.Forms.Label();
            this.lblUp1 = new System.Windows.Forms.Label();
            this.lblUp2 = new System.Windows.Forms.Label();
            this.lblUp3 = new System.Windows.Forms.Label();
            this.cbbOwner = new System.Windows.Forms.ComboBox();
            this.mtxbHp = new System.Windows.Forms.MaskedTextBox();
            this.mtxbVeteran = new System.Windows.Forms.MaskedTextBox();
            this.mtxbFacing = new System.Windows.Forms.MaskedTextBox();
            this.cbbAttTag = new System.Windows.Forms.ComboBox();
            this.cbbStat = new System.Windows.Forms.ComboBox();
            this.txbGroup = new System.Windows.Forms.TextBox();
            this.ckbBridge = new System.Windows.Forms.CheckBox();
            this.ckbRecruit = new System.Windows.Forms.CheckBox();
            this.ckbUnitRebuild = new System.Windows.Forms.CheckBox();
            this.ckbSell = new System.Windows.Forms.CheckBox();
            this.txbFollow = new System.Windows.Forms.TextBox();
            this.ckbRepair = new System.Windows.Forms.CheckBox();
            this.ckbPowered = new System.Windows.Forms.CheckBox();
            this.cbbSpotlight = new System.Windows.Forms.ComboBox();
            this.mtxbUpgNum = new System.Windows.Forms.MaskedTextBox();
            this.cbbUpg1 = new System.Windows.Forms.ComboBox();
            this.cbbUpg2 = new System.Windows.Forms.ComboBox();
            this.cbbUpg3 = new System.Windows.Forms.ComboBox();
            this.tbpTerrain = new System.Windows.Forms.TabPage();
            this.tbpSmudge = new System.Windows.Forms.TabPage();
            this.tbpOverlay = new System.Windows.Forms.TabPage();
            this.tbpWaypoint = new System.Windows.Forms.TabPage();
            this.tbpCelltag = new System.Windows.Forms.TabPage();
            this.tbpBaseNode = new System.Windows.Forms.TabPage();
            this.tbcMain.SuspendLayout();
            this.tbpObject.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitObjects)).BeginInit();
            this.splitObjects.Panel1.SuspendLayout();
            this.splitObjects.Panel2.SuspendLayout();
            this.splitObjects.SuspendLayout();
            this.pnlObjectProp.SuspendLayout();
            this.tlpObjectProps.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbcMain
            // 
            this.tbcMain.Controls.Add(this.tbpObject);
            this.tbcMain.Controls.Add(this.tbpTerrain);
            this.tbcMain.Controls.Add(this.tbpSmudge);
            this.tbcMain.Controls.Add(this.tbpOverlay);
            this.tbcMain.Controls.Add(this.tbpWaypoint);
            this.tbcMain.Controls.Add(this.tbpCelltag);
            this.tbcMain.Controls.Add(this.tbpBaseNode);
            this.tbcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbcMain.Location = new System.Drawing.Point(0, 0);
            this.tbcMain.Multiline = true;
            this.tbcMain.Name = "tbcMain";
            this.tbcMain.SelectedIndex = 0;
            this.tbcMain.Size = new System.Drawing.Size(345, 750);
            this.tbcMain.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
            this.tbcMain.TabIndex = 0;
            // 
            // tbpObject
            // 
            this.tbpObject.Controls.Add(this.splitObjects);
            this.tbpObject.Location = new System.Drawing.Point(4, 46);
            this.tbpObject.Name = "tbpObject";
            this.tbpObject.Padding = new System.Windows.Forms.Padding(3);
            this.tbpObject.Size = new System.Drawing.Size(337, 700);
            this.tbpObject.TabIndex = 0;
            this.tbpObject.Text = "General Objects";
            this.tbpObject.UseVisualStyleBackColor = true;
            // 
            // splitObjects
            // 
            this.splitObjects.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitObjects.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitObjects.Location = new System.Drawing.Point(3, 3);
            this.splitObjects.Name = "splitObjects";
            this.splitObjects.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitObjects.Panel1
            // 
            this.splitObjects.Panel1.Controls.Add(this.trvObject);
            // 
            // splitObjects.Panel2
            // 
            this.splitObjects.Panel2.AutoScroll = true;
            this.splitObjects.Panel2.Controls.Add(this.pnlObjectProp);
            this.splitObjects.Size = new System.Drawing.Size(331, 694);
            this.splitObjects.SplitterDistance = 506;
            this.splitObjects.TabIndex = 1;
            // 
            // trvObject
            // 
            this.trvObject.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvObject.HideSelection = false;
            this.trvObject.ImageIndex = 0;
            this.trvObject.ImageList = this.imgMain;
            this.trvObject.Location = new System.Drawing.Point(0, 0);
            this.trvObject.Name = "trvObject";
            this.trvObject.SelectedImageIndex = 0;
            this.trvObject.ShowNodeToolTips = true;
            this.trvObject.Size = new System.Drawing.Size(331, 506);
            this.trvObject.TabIndex = 0;
            this.trvObject.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.trvObject_NodeMouseClick);
            // 
            // imgMain
            // 
            this.imgMain.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imgMain.ImageSize = new System.Drawing.Size(16, 16);
            this.imgMain.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // pnlObjectProp
            // 
            this.pnlObjectProp.AutoSize = true;
            this.pnlObjectProp.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlObjectProp.Controls.Add(this.tlpObjectProps);
            this.pnlObjectProp.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlObjectProp.Location = new System.Drawing.Point(0, 0);
            this.pnlObjectProp.Name = "pnlObjectProp";
            this.pnlObjectProp.Size = new System.Drawing.Size(310, 527);
            this.pnlObjectProp.TabIndex = 1;
            // 
            // tlpObjectProps
            // 
            this.tlpObjectProps.AutoSize = true;
            this.tlpObjectProps.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlpObjectProps.ColumnCount = 2;
            this.tlpObjectProps.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpObjectProps.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpObjectProps.Controls.Add(this.label9, 0, 0);
            this.tlpObjectProps.Controls.Add(this.label1, 0, 1);
            this.tlpObjectProps.Controls.Add(this.label2, 0, 2);
            this.tlpObjectProps.Controls.Add(this.label3, 0, 3);
            this.tlpObjectProps.Controls.Add(this.label4, 0, 4);
            this.tlpObjectProps.Controls.Add(this.label5, 0, 5);
            this.tlpObjectProps.Controls.Add(this.label6, 0, 6);
            this.tlpObjectProps.Controls.Add(this.lblBridge, 0, 7);
            this.tlpObjectProps.Controls.Add(this.lblRecruit, 0, 8);
            this.tlpObjectProps.Controls.Add(this.lblUnitRebuild, 0, 9);
            this.tlpObjectProps.Controls.Add(this.lblSell, 0, 10);
            this.tlpObjectProps.Controls.Add(this.lblFollow, 0, 11);
            this.tlpObjectProps.Controls.Add(this.lblRepair, 0, 12);
            this.tlpObjectProps.Controls.Add(this.lblPower, 0, 15);
            this.tlpObjectProps.Controls.Add(this.lblSpotlight, 0, 16);
            this.tlpObjectProps.Controls.Add(this.lblUpNum, 0, 17);
            this.tlpObjectProps.Controls.Add(this.lblUp1, 0, 18);
            this.tlpObjectProps.Controls.Add(this.lblUp2, 0, 19);
            this.tlpObjectProps.Controls.Add(this.lblUp3, 0, 20);
            this.tlpObjectProps.Controls.Add(this.cbbOwner, 1, 0);
            this.tlpObjectProps.Controls.Add(this.mtxbHp, 1, 1);
            this.tlpObjectProps.Controls.Add(this.mtxbVeteran, 1, 2);
            this.tlpObjectProps.Controls.Add(this.mtxbFacing, 1, 3);
            this.tlpObjectProps.Controls.Add(this.cbbAttTag, 1, 4);
            this.tlpObjectProps.Controls.Add(this.cbbStat, 1, 5);
            this.tlpObjectProps.Controls.Add(this.txbGroup, 1, 6);
            this.tlpObjectProps.Controls.Add(this.ckbBridge, 1, 7);
            this.tlpObjectProps.Controls.Add(this.ckbRecruit, 1, 8);
            this.tlpObjectProps.Controls.Add(this.ckbUnitRebuild, 1, 9);
            this.tlpObjectProps.Controls.Add(this.ckbSell, 1, 10);
            this.tlpObjectProps.Controls.Add(this.txbFollow, 1, 11);
            this.tlpObjectProps.Controls.Add(this.ckbRepair, 1, 12);
            this.tlpObjectProps.Controls.Add(this.ckbPowered, 1, 15);
            this.tlpObjectProps.Controls.Add(this.cbbSpotlight, 1, 16);
            this.tlpObjectProps.Controls.Add(this.mtxbUpgNum, 1, 17);
            this.tlpObjectProps.Controls.Add(this.cbbUpg1, 1, 18);
            this.tlpObjectProps.Controls.Add(this.cbbUpg2, 1, 19);
            this.tlpObjectProps.Controls.Add(this.cbbUpg3, 1, 20);
            this.tlpObjectProps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpObjectProps.Location = new System.Drawing.Point(0, 0);
            this.tlpObjectProps.Name = "tlpObjectProps";
            this.tlpObjectProps.RowCount = 21;
            this.tlpObjectProps.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpObjectProps.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpObjectProps.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpObjectProps.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpObjectProps.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpObjectProps.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpObjectProps.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpObjectProps.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpObjectProps.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpObjectProps.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpObjectProps.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpObjectProps.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpObjectProps.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpObjectProps.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpObjectProps.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpObjectProps.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpObjectProps.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpObjectProps.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpObjectProps.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpObjectProps.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpObjectProps.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpObjectProps.Size = new System.Drawing.Size(310, 527);
            this.tlpObjectProps.TabIndex = 0;
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(51, 7);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(143, 15);
            this.label9.TabIndex = 2;
            this.label9.Text = "RSMainObjLblOwner";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(75, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "RSMainObjLblHp";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(35, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(159, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "RSMainObjLblVeteran";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 99);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(167, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "RSMainObjLblRotation";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(67, 129);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(127, 15);
            this.label4.TabIndex = 2;
            this.label4.Text = "RSMainObjLblTag";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(59, 158);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(135, 15);
            this.label5.TabIndex = 2;
            this.label5.Text = "RSMainObjLblStat";
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(51, 188);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(143, 15);
            this.label6.TabIndex = 2;
            this.label6.Text = "RSMainObjLblGroup";
            // 
            // lblBridge
            // 
            this.lblBridge.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblBridge.AutoSize = true;
            this.lblBridge.Location = new System.Drawing.Point(51, 215);
            this.lblBridge.Name = "lblBridge";
            this.lblBridge.Size = new System.Drawing.Size(143, 15);
            this.lblBridge.TabIndex = 2;
            this.lblBridge.Text = "RSMainObjLblOnBrg";
            // 
            // lblRecruit
            // 
            this.lblRecruit.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblRecruit.AutoSize = true;
            this.lblRecruit.Location = new System.Drawing.Point(35, 238);
            this.lblRecruit.Name = "lblRecruit";
            this.lblRecruit.Size = new System.Drawing.Size(159, 15);
            this.lblRecruit.TabIndex = 2;
            this.lblRecruit.Text = "RSMainObjLblRecruit";
            // 
            // lblUnitRebuild
            // 
            this.lblUnitRebuild.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblUnitRebuild.AutoSize = true;
            this.lblUnitRebuild.Location = new System.Drawing.Point(3, 261);
            this.lblUnitRebuild.Name = "lblUnitRebuild";
            this.lblUnitRebuild.Size = new System.Drawing.Size(191, 15);
            this.lblUnitRebuild.TabIndex = 2;
            this.lblUnitRebuild.Text = "RSMainObjLblUnitRebuild";
            // 
            // lblSell
            // 
            this.lblSell.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblSell.AutoSize = true;
            this.lblSell.Location = new System.Drawing.Point(59, 284);
            this.lblSell.Name = "lblSell";
            this.lblSell.Size = new System.Drawing.Size(135, 15);
            this.lblSell.TabIndex = 2;
            this.lblSell.Text = "RSMainObjLblSell";
            // 
            // lblFollow
            // 
            this.lblFollow.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblFollow.AutoSize = true;
            this.lblFollow.Location = new System.Drawing.Point(43, 311);
            this.lblFollow.Name = "lblFollow";
            this.lblFollow.Size = new System.Drawing.Size(151, 15);
            this.lblFollow.TabIndex = 2;
            this.lblFollow.Text = "RSMainObjLblFollow";
            // 
            // lblRepair
            // 
            this.lblRepair.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblRepair.AutoSize = true;
            this.lblRepair.Location = new System.Drawing.Point(43, 338);
            this.lblRepair.Name = "lblRepair";
            this.lblRepair.Size = new System.Drawing.Size(151, 15);
            this.lblRepair.TabIndex = 2;
            this.lblRepair.Text = "RSMainObjLblRepair";
            // 
            // lblPower
            // 
            this.lblPower.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblPower.AutoSize = true;
            this.lblPower.Location = new System.Drawing.Point(51, 361);
            this.lblPower.Name = "lblPower";
            this.lblPower.Size = new System.Drawing.Size(143, 15);
            this.lblPower.TabIndex = 2;
            this.lblPower.Text = "RSMainObjLblPower";
            // 
            // lblSpotlight
            // 
            this.lblSpotlight.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblSpotlight.AutoSize = true;
            this.lblSpotlight.Location = new System.Drawing.Point(19, 387);
            this.lblSpotlight.Name = "lblSpotlight";
            this.lblSpotlight.Size = new System.Drawing.Size(175, 15);
            this.lblSpotlight.TabIndex = 2;
            this.lblSpotlight.Text = "RSMainObjLblSpotlight";
            // 
            // lblUpNum
            // 
            this.lblUpNum.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblUpNum.AutoSize = true;
            this.lblUpNum.Location = new System.Drawing.Point(11, 417);
            this.lblUpNum.Name = "lblUpNum";
            this.lblUpNum.Size = new System.Drawing.Size(183, 15);
            this.lblUpNum.TabIndex = 2;
            this.lblUpNum.Text = "RSMainObjLblUpgradeNum";
            // 
            // lblUp1
            // 
            this.lblUp1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblUp1.AutoSize = true;
            this.lblUp1.Location = new System.Drawing.Point(67, 447);
            this.lblUp1.Name = "lblUp1";
            this.lblUp1.Size = new System.Drawing.Size(127, 15);
            this.lblUp1.TabIndex = 2;
            this.lblUp1.Text = "RSMainObjLblUp1";
            // 
            // lblUp2
            // 
            this.lblUp2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblUp2.AutoSize = true;
            this.lblUp2.Location = new System.Drawing.Point(67, 476);
            this.lblUp2.Name = "lblUp2";
            this.lblUp2.Size = new System.Drawing.Size(127, 15);
            this.lblUp2.TabIndex = 2;
            this.lblUp2.Text = "RSMainObjLblUp2";
            // 
            // lblUp3
            // 
            this.lblUp3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblUp3.AutoSize = true;
            this.lblUp3.Location = new System.Drawing.Point(67, 505);
            this.lblUp3.Name = "lblUp3";
            this.lblUp3.Size = new System.Drawing.Size(127, 15);
            this.lblUp3.TabIndex = 2;
            this.lblUp3.Text = "RSMainObjLblUp3";
            // 
            // cbbOwner
            // 
            this.cbbOwner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbbOwner.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbOwner.FormattingEnabled = true;
            this.cbbOwner.Location = new System.Drawing.Point(200, 3);
            this.cbbOwner.Name = "cbbOwner";
            this.cbbOwner.Size = new System.Drawing.Size(107, 23);
            this.cbbOwner.TabIndex = 3;
            this.cbbOwner.SelectedIndexChanged += new System.EventHandler(this.ParamChangedOnRedraw);
            // 
            // mtxbHp
            // 
            this.mtxbHp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mtxbHp.Location = new System.Drawing.Point(200, 32);
            this.mtxbHp.Mask = "000";
            this.mtxbHp.Name = "mtxbHp";
            this.mtxbHp.PromptChar = ' ';
            this.mtxbHp.Size = new System.Drawing.Size(107, 25);
            this.mtxbHp.TabIndex = 4;
            this.mtxbHp.Text = "256";
            this.mtxbHp.ValidatingType = typeof(int);
            this.mtxbHp.Validated += new System.EventHandler(this.ParamChangedOnRedraw);
            // 
            // mtxbVeteran
            // 
            this.mtxbVeteran.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mtxbVeteran.Location = new System.Drawing.Point(200, 63);
            this.mtxbVeteran.Mask = "000";
            this.mtxbVeteran.Name = "mtxbVeteran";
            this.mtxbVeteran.PromptChar = ' ';
            this.mtxbVeteran.Size = new System.Drawing.Size(107, 25);
            this.mtxbVeteran.TabIndex = 5;
            this.mtxbVeteran.Text = "0";
            this.mtxbVeteran.ValidatingType = typeof(int);
            this.mtxbVeteran.Validated += new System.EventHandler(this.ParamChangedOnRedraw);
            // 
            // mtxbFacing
            // 
            this.mtxbFacing.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mtxbFacing.Location = new System.Drawing.Point(200, 94);
            this.mtxbFacing.Name = "mtxbFacing";
            this.mtxbFacing.Size = new System.Drawing.Size(107, 25);
            this.mtxbFacing.TabIndex = 5;
            this.mtxbFacing.Text = "0";
            this.mtxbFacing.Validated += new System.EventHandler(this.ParamChangedOnRedraw);
            // 
            // cbbAttTag
            // 
            this.cbbAttTag.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbbAttTag.FormattingEnabled = true;
            this.cbbAttTag.Location = new System.Drawing.Point(200, 125);
            this.cbbAttTag.Name = "cbbAttTag";
            this.cbbAttTag.Size = new System.Drawing.Size(107, 23);
            this.cbbAttTag.TabIndex = 3;
            this.cbbAttTag.SelectedIndexChanged += new System.EventHandler(this.ParamChangedOnRedraw);
            // 
            // cbbStat
            // 
            this.cbbStat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbbStat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbStat.FormattingEnabled = true;
            this.cbbStat.Location = new System.Drawing.Point(200, 154);
            this.cbbStat.Name = "cbbStat";
            this.cbbStat.Size = new System.Drawing.Size(107, 23);
            this.cbbStat.TabIndex = 3;
            this.cbbStat.SelectedIndexChanged += new System.EventHandler(this.ParamChangedOnRedraw);
            // 
            // txbGroup
            // 
            this.txbGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txbGroup.Location = new System.Drawing.Point(200, 183);
            this.txbGroup.Name = "txbGroup";
            this.txbGroup.Size = new System.Drawing.Size(107, 25);
            this.txbGroup.TabIndex = 6;
            this.txbGroup.Text = "-1";
            this.txbGroup.Validated += new System.EventHandler(this.ParamChangedOnRedraw);
            // 
            // ckbBridge
            // 
            this.ckbBridge.AutoSize = true;
            this.ckbBridge.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ckbBridge.Location = new System.Drawing.Point(200, 214);
            this.ckbBridge.Name = "ckbBridge";
            this.ckbBridge.Size = new System.Drawing.Size(107, 17);
            this.ckbBridge.TabIndex = 7;
            this.ckbBridge.UseVisualStyleBackColor = true;
            this.ckbBridge.CheckedChanged += new System.EventHandler(this.ParamChangedOnRedraw);
            // 
            // ckbRecruit
            // 
            this.ckbRecruit.AutoSize = true;
            this.ckbRecruit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ckbRecruit.Location = new System.Drawing.Point(200, 237);
            this.ckbRecruit.Name = "ckbRecruit";
            this.ckbRecruit.Size = new System.Drawing.Size(107, 17);
            this.ckbRecruit.TabIndex = 7;
            this.ckbRecruit.UseVisualStyleBackColor = true;
            this.ckbRecruit.CheckedChanged += new System.EventHandler(this.ParamChangedOnRedraw);
            // 
            // ckbUnitRebuild
            // 
            this.ckbUnitRebuild.AutoSize = true;
            this.ckbUnitRebuild.Checked = true;
            this.ckbUnitRebuild.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbUnitRebuild.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ckbUnitRebuild.Location = new System.Drawing.Point(200, 260);
            this.ckbUnitRebuild.Name = "ckbUnitRebuild";
            this.ckbUnitRebuild.Size = new System.Drawing.Size(107, 17);
            this.ckbUnitRebuild.TabIndex = 7;
            this.ckbUnitRebuild.UseVisualStyleBackColor = true;
            this.ckbUnitRebuild.CheckedChanged += new System.EventHandler(this.ParamChangedOnRedraw);
            // 
            // ckbSell
            // 
            this.ckbSell.AutoSize = true;
            this.ckbSell.Checked = true;
            this.ckbSell.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbSell.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ckbSell.Location = new System.Drawing.Point(200, 283);
            this.ckbSell.Name = "ckbSell";
            this.ckbSell.Size = new System.Drawing.Size(107, 17);
            this.ckbSell.TabIndex = 7;
            this.ckbSell.UseVisualStyleBackColor = true;
            this.ckbSell.CheckedChanged += new System.EventHandler(this.ParamChangedOnRedraw);
            // 
            // txbFollow
            // 
            this.txbFollow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txbFollow.Location = new System.Drawing.Point(200, 306);
            this.txbFollow.Name = "txbFollow";
            this.txbFollow.Size = new System.Drawing.Size(107, 25);
            this.txbFollow.TabIndex = 6;
            this.txbFollow.Text = "-1";
            this.txbFollow.Validated += new System.EventHandler(this.ParamChangedOnRedraw);
            // 
            // ckbRepair
            // 
            this.ckbRepair.AutoSize = true;
            this.ckbRepair.Checked = true;
            this.ckbRepair.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbRepair.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ckbRepair.Location = new System.Drawing.Point(200, 337);
            this.ckbRepair.Name = "ckbRepair";
            this.ckbRepair.Size = new System.Drawing.Size(107, 17);
            this.ckbRepair.TabIndex = 7;
            this.ckbRepair.UseVisualStyleBackColor = true;
            this.ckbRepair.CheckedChanged += new System.EventHandler(this.ParamChangedOnRedraw);
            // 
            // ckbPowered
            // 
            this.ckbPowered.AutoSize = true;
            this.ckbPowered.Checked = true;
            this.ckbPowered.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbPowered.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ckbPowered.Location = new System.Drawing.Point(200, 360);
            this.ckbPowered.Name = "ckbPowered";
            this.ckbPowered.Size = new System.Drawing.Size(107, 17);
            this.ckbPowered.TabIndex = 7;
            this.ckbPowered.UseVisualStyleBackColor = true;
            this.ckbPowered.CheckedChanged += new System.EventHandler(this.ParamChangedOnRedraw);
            // 
            // cbbSpotlight
            // 
            this.cbbSpotlight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbbSpotlight.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbSpotlight.FormattingEnabled = true;
            this.cbbSpotlight.Location = new System.Drawing.Point(200, 383);
            this.cbbSpotlight.Name = "cbbSpotlight";
            this.cbbSpotlight.Size = new System.Drawing.Size(107, 23);
            this.cbbSpotlight.TabIndex = 3;
            this.cbbSpotlight.SelectedIndexChanged += new System.EventHandler(this.ParamChangedOnRedraw);
            // 
            // mtxbUpgNum
            // 
            this.mtxbUpgNum.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mtxbUpgNum.Location = new System.Drawing.Point(200, 412);
            this.mtxbUpgNum.Mask = "0";
            this.mtxbUpgNum.Name = "mtxbUpgNum";
            this.mtxbUpgNum.PromptChar = ' ';
            this.mtxbUpgNum.Size = new System.Drawing.Size(107, 25);
            this.mtxbUpgNum.TabIndex = 5;
            this.mtxbUpgNum.Text = "0";
            this.mtxbUpgNum.ValidatingType = typeof(int);
            this.mtxbUpgNum.TextChanged += new System.EventHandler(this.mtxbUpgNum_TextChanged);
            this.mtxbUpgNum.Validated += new System.EventHandler(this.ParamChangedOnRedraw);
            // 
            // cbbUpg1
            // 
            this.cbbUpg1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbbUpg1.FormattingEnabled = true;
            this.cbbUpg1.Location = new System.Drawing.Point(200, 443);
            this.cbbUpg1.Name = "cbbUpg1";
            this.cbbUpg1.Size = new System.Drawing.Size(107, 23);
            this.cbbUpg1.TabIndex = 3;
            this.cbbUpg1.Text = "None";
            this.cbbUpg1.SelectedIndexChanged += new System.EventHandler(this.ParamChangedOnRedraw);
            // 
            // cbbUpg2
            // 
            this.cbbUpg2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbbUpg2.FormattingEnabled = true;
            this.cbbUpg2.Location = new System.Drawing.Point(200, 472);
            this.cbbUpg2.Name = "cbbUpg2";
            this.cbbUpg2.Size = new System.Drawing.Size(107, 23);
            this.cbbUpg2.TabIndex = 3;
            this.cbbUpg2.Text = "None";
            this.cbbUpg2.SelectedIndexChanged += new System.EventHandler(this.ParamChangedOnRedraw);
            // 
            // cbbUpg3
            // 
            this.cbbUpg3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbbUpg3.FormattingEnabled = true;
            this.cbbUpg3.Location = new System.Drawing.Point(200, 501);
            this.cbbUpg3.Name = "cbbUpg3";
            this.cbbUpg3.Size = new System.Drawing.Size(107, 23);
            this.cbbUpg3.TabIndex = 3;
            this.cbbUpg3.Text = "None";
            this.cbbUpg3.SelectedIndexChanged += new System.EventHandler(this.ParamChangedOnRedraw);
            // 
            // tbpTerrain
            // 
            this.tbpTerrain.Location = new System.Drawing.Point(4, 46);
            this.tbpTerrain.Name = "tbpTerrain";
            this.tbpTerrain.Size = new System.Drawing.Size(337, 700);
            this.tbpTerrain.TabIndex = 4;
            this.tbpTerrain.Text = "Terrains";
            this.tbpTerrain.UseVisualStyleBackColor = true;
            // 
            // tbpSmudge
            // 
            this.tbpSmudge.Location = new System.Drawing.Point(4, 46);
            this.tbpSmudge.Name = "tbpSmudge";
            this.tbpSmudge.Size = new System.Drawing.Size(337, 700);
            this.tbpSmudge.TabIndex = 5;
            this.tbpSmudge.Text = "Smudges";
            this.tbpSmudge.UseVisualStyleBackColor = true;
            // 
            // tbpOverlay
            // 
            this.tbpOverlay.Location = new System.Drawing.Point(4, 46);
            this.tbpOverlay.Name = "tbpOverlay";
            this.tbpOverlay.Size = new System.Drawing.Size(337, 700);
            this.tbpOverlay.TabIndex = 6;
            this.tbpOverlay.Text = "Overlays";
            this.tbpOverlay.UseVisualStyleBackColor = true;
            // 
            // tbpWaypoint
            // 
            this.tbpWaypoint.Location = new System.Drawing.Point(4, 46);
            this.tbpWaypoint.Name = "tbpWaypoint";
            this.tbpWaypoint.Size = new System.Drawing.Size(337, 700);
            this.tbpWaypoint.TabIndex = 7;
            this.tbpWaypoint.Text = "Waypoints";
            this.tbpWaypoint.UseVisualStyleBackColor = true;
            // 
            // tbpCelltag
            // 
            this.tbpCelltag.Location = new System.Drawing.Point(4, 46);
            this.tbpCelltag.Name = "tbpCelltag";
            this.tbpCelltag.Size = new System.Drawing.Size(337, 700);
            this.tbpCelltag.TabIndex = 8;
            this.tbpCelltag.Text = "Celltags";
            this.tbpCelltag.UseVisualStyleBackColor = true;
            // 
            // tbpBaseNode
            // 
            this.tbpBaseNode.Location = new System.Drawing.Point(4, 46);
            this.tbpBaseNode.Name = "tbpBaseNode";
            this.tbpBaseNode.Size = new System.Drawing.Size(337, 700);
            this.tbpBaseNode.TabIndex = 9;
            this.tbpBaseNode.Text = "Base Nodes";
            this.tbpBaseNode.UseVisualStyleBackColor = true;
            // 
            // PickPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbcMain);
            this.Name = "PickPanel";
            this.Size = new System.Drawing.Size(345, 750);
            this.tbcMain.ResumeLayout(false);
            this.tbpObject.ResumeLayout(false);
            this.splitObjects.Panel1.ResumeLayout(false);
            this.splitObjects.Panel2.ResumeLayout(false);
            this.splitObjects.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitObjects)).EndInit();
            this.splitObjects.ResumeLayout(false);
            this.pnlObjectProp.ResumeLayout(false);
            this.pnlObjectProp.PerformLayout();
            this.tlpObjectProps.ResumeLayout(false);
            this.tlpObjectProps.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tbcMain;
        private System.Windows.Forms.TabPage tbpObject;
        private System.Windows.Forms.TabPage tbpTerrain;
        private System.Windows.Forms.TabPage tbpSmudge;
        private System.Windows.Forms.TabPage tbpOverlay;
        private System.Windows.Forms.TabPage tbpWaypoint;
        private System.Windows.Forms.TabPage tbpCelltag;
        private System.Windows.Forms.TabPage tbpBaseNode;
        private System.Windows.Forms.TreeView trvObject;
        private System.Windows.Forms.ImageList imgMain;
        private System.Windows.Forms.SplitContainer splitObjects;
        private System.Windows.Forms.TableLayoutPanel tlpObjectProps;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlObjectProp;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblBridge;
        private System.Windows.Forms.Label lblRecruit;
        private System.Windows.Forms.Label lblUnitRebuild;
        private System.Windows.Forms.Label lblSell;
        private System.Windows.Forms.Label lblFollow;
        private System.Windows.Forms.Label lblRepair;
        private System.Windows.Forms.Label lblPower;
        private System.Windows.Forms.Label lblSpotlight;
        private System.Windows.Forms.Label lblUpNum;
        private System.Windows.Forms.Label lblUp1;
        private System.Windows.Forms.Label lblUp2;
        private System.Windows.Forms.Label lblUp3;
        private System.Windows.Forms.ComboBox cbbOwner;
        private System.Windows.Forms.MaskedTextBox mtxbHp;
        private System.Windows.Forms.MaskedTextBox mtxbVeteran;
        private System.Windows.Forms.MaskedTextBox mtxbFacing;
        private System.Windows.Forms.ComboBox cbbAttTag;
        private System.Windows.Forms.ComboBox cbbStat;
        private System.Windows.Forms.TextBox txbGroup;
        private System.Windows.Forms.CheckBox ckbBridge;
        private System.Windows.Forms.CheckBox ckbRecruit;
        private System.Windows.Forms.CheckBox ckbUnitRebuild;
        private System.Windows.Forms.CheckBox ckbSell;
        private System.Windows.Forms.TextBox txbFollow;
        private System.Windows.Forms.CheckBox ckbRepair;
        private System.Windows.Forms.CheckBox ckbPowered;
        private System.Windows.Forms.ComboBox cbbSpotlight;
        private System.Windows.Forms.MaskedTextBox mtxbUpgNum;
        private System.Windows.Forms.ComboBox cbbUpg1;
        private System.Windows.Forms.ComboBox cbbUpg2;
        private System.Windows.Forms.ComboBox cbbUpg3;
    }
}
