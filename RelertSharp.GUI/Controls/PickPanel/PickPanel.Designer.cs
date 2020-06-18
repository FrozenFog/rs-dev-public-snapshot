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
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.cbbOwner = new System.Windows.Forms.ComboBox();
            this.mtxbHp = new System.Windows.Forms.MaskedTextBox();
            this.mtxbVeteran = new System.Windows.Forms.MaskedTextBox();
            this.mtxbFacing = new System.Windows.Forms.MaskedTextBox();
            this.cbbAttTag = new System.Windows.Forms.ComboBox();
            this.cbbStat = new System.Windows.Forms.ComboBox();
            this.txbGroup = new System.Windows.Forms.TextBox();
            this.ckbOnBrg = new System.Windows.Forms.CheckBox();
            this.ckbRecruit = new System.Windows.Forms.CheckBox();
            this.ckbUnitRebuild = new System.Windows.Forms.CheckBox();
            this.ckbSell = new System.Windows.Forms.CheckBox();
            this.txbFollow = new System.Windows.Forms.TextBox();
            this.ckbRepair = new System.Windows.Forms.CheckBox();
            this.ckbRebuild = new System.Windows.Forms.CheckBox();
            this.ckbShowName = new System.Windows.Forms.CheckBox();
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
            this.pnlObjectProp.Size = new System.Drawing.Size(310, 573);
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
            this.tlpObjectProps.Controls.Add(this.label7, 0, 7);
            this.tlpObjectProps.Controls.Add(this.label8, 0, 8);
            this.tlpObjectProps.Controls.Add(this.label10, 0, 9);
            this.tlpObjectProps.Controls.Add(this.label11, 0, 10);
            this.tlpObjectProps.Controls.Add(this.label12, 0, 11);
            this.tlpObjectProps.Controls.Add(this.label13, 0, 12);
            this.tlpObjectProps.Controls.Add(this.label14, 0, 13);
            this.tlpObjectProps.Controls.Add(this.label15, 0, 14);
            this.tlpObjectProps.Controls.Add(this.label16, 0, 15);
            this.tlpObjectProps.Controls.Add(this.label17, 0, 16);
            this.tlpObjectProps.Controls.Add(this.label18, 0, 17);
            this.tlpObjectProps.Controls.Add(this.label19, 0, 18);
            this.tlpObjectProps.Controls.Add(this.label20, 0, 19);
            this.tlpObjectProps.Controls.Add(this.label21, 0, 20);
            this.tlpObjectProps.Controls.Add(this.cbbOwner, 1, 0);
            this.tlpObjectProps.Controls.Add(this.mtxbHp, 1, 1);
            this.tlpObjectProps.Controls.Add(this.mtxbVeteran, 1, 2);
            this.tlpObjectProps.Controls.Add(this.mtxbFacing, 1, 3);
            this.tlpObjectProps.Controls.Add(this.cbbAttTag, 1, 4);
            this.tlpObjectProps.Controls.Add(this.cbbStat, 1, 5);
            this.tlpObjectProps.Controls.Add(this.txbGroup, 1, 6);
            this.tlpObjectProps.Controls.Add(this.ckbOnBrg, 1, 7);
            this.tlpObjectProps.Controls.Add(this.ckbRecruit, 1, 8);
            this.tlpObjectProps.Controls.Add(this.ckbUnitRebuild, 1, 9);
            this.tlpObjectProps.Controls.Add(this.ckbSell, 1, 10);
            this.tlpObjectProps.Controls.Add(this.txbFollow, 1, 11);
            this.tlpObjectProps.Controls.Add(this.ckbRepair, 1, 12);
            this.tlpObjectProps.Controls.Add(this.ckbRebuild, 1, 13);
            this.tlpObjectProps.Controls.Add(this.ckbShowName, 1, 14);
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
            this.tlpObjectProps.Size = new System.Drawing.Size(310, 573);
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
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(51, 215);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(143, 15);
            this.label7.TabIndex = 2;
            this.label7.Text = "RSMainObjLblOnBrg";
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(35, 238);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(159, 15);
            this.label8.TabIndex = 2;
            this.label8.Text = "RSMainObjLblRecruit";
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 261);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(191, 15);
            this.label10.TabIndex = 2;
            this.label10.Text = "RSMainObjLblUnitRebuild";
            // 
            // label11
            // 
            this.label11.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(59, 284);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(135, 15);
            this.label11.TabIndex = 2;
            this.label11.Text = "RSMainObjLblSell";
            // 
            // label12
            // 
            this.label12.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(43, 311);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(151, 15);
            this.label12.TabIndex = 2;
            this.label12.Text = "RSMainObjLblFollow";
            // 
            // label13
            // 
            this.label13.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(43, 338);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(151, 15);
            this.label13.TabIndex = 2;
            this.label13.Text = "RSMainObjLblRepair";
            // 
            // label14
            // 
            this.label14.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(35, 361);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(159, 15);
            this.label14.TabIndex = 2;
            this.label14.Text = "RSMainObjLblRebuild";
            // 
            // label15
            // 
            this.label15.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(59, 384);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(135, 15);
            this.label15.TabIndex = 2;
            this.label15.Text = "RSMainObjLblShow";
            // 
            // label16
            // 
            this.label16.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(51, 407);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(143, 15);
            this.label16.TabIndex = 2;
            this.label16.Text = "RSMainObjLblPower";
            // 
            // label17
            // 
            this.label17.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(19, 433);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(175, 15);
            this.label17.TabIndex = 2;
            this.label17.Text = "RSMainObjLblSpotlight";
            // 
            // label18
            // 
            this.label18.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(11, 463);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(183, 15);
            this.label18.TabIndex = 2;
            this.label18.Text = "RSMainObjLblUpgradeNum";
            // 
            // label19
            // 
            this.label19.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(67, 493);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(127, 15);
            this.label19.TabIndex = 2;
            this.label19.Text = "RSMainObjLblUp1";
            // 
            // label20
            // 
            this.label20.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(67, 522);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(127, 15);
            this.label20.TabIndex = 2;
            this.label20.Text = "RSMainObjLblUp2";
            // 
            // label21
            // 
            this.label21.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(67, 551);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(127, 15);
            this.label21.TabIndex = 2;
            this.label21.Text = "RSMainObjLblUp3";
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
            // 
            // mtxbHp
            // 
            this.mtxbHp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mtxbHp.Location = new System.Drawing.Point(200, 32);
            this.mtxbHp.Name = "mtxbHp";
            this.mtxbHp.Size = new System.Drawing.Size(107, 25);
            this.mtxbHp.TabIndex = 4;
            // 
            // mtxbVeteran
            // 
            this.mtxbVeteran.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mtxbVeteran.Location = new System.Drawing.Point(200, 63);
            this.mtxbVeteran.Name = "mtxbVeteran";
            this.mtxbVeteran.Size = new System.Drawing.Size(107, 25);
            this.mtxbVeteran.TabIndex = 5;
            // 
            // mtxbFacing
            // 
            this.mtxbFacing.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mtxbFacing.Location = new System.Drawing.Point(200, 94);
            this.mtxbFacing.Name = "mtxbFacing";
            this.mtxbFacing.Size = new System.Drawing.Size(107, 25);
            this.mtxbFacing.TabIndex = 5;
            // 
            // cbbAttTag
            // 
            this.cbbAttTag.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbbAttTag.FormattingEnabled = true;
            this.cbbAttTag.Location = new System.Drawing.Point(200, 125);
            this.cbbAttTag.Name = "cbbAttTag";
            this.cbbAttTag.Size = new System.Drawing.Size(107, 23);
            this.cbbAttTag.TabIndex = 3;
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
            // 
            // txbGroup
            // 
            this.txbGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txbGroup.Location = new System.Drawing.Point(200, 183);
            this.txbGroup.Name = "txbGroup";
            this.txbGroup.Size = new System.Drawing.Size(107, 25);
            this.txbGroup.TabIndex = 6;
            // 
            // ckbOnBrg
            // 
            this.ckbOnBrg.AutoSize = true;
            this.ckbOnBrg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ckbOnBrg.Location = new System.Drawing.Point(200, 214);
            this.ckbOnBrg.Name = "ckbOnBrg";
            this.ckbOnBrg.Size = new System.Drawing.Size(107, 17);
            this.ckbOnBrg.TabIndex = 7;
            this.ckbOnBrg.UseVisualStyleBackColor = true;
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
            // 
            // ckbUnitRebuild
            // 
            this.ckbUnitRebuild.AutoSize = true;
            this.ckbUnitRebuild.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ckbUnitRebuild.Location = new System.Drawing.Point(200, 260);
            this.ckbUnitRebuild.Name = "ckbUnitRebuild";
            this.ckbUnitRebuild.Size = new System.Drawing.Size(107, 17);
            this.ckbUnitRebuild.TabIndex = 7;
            this.ckbUnitRebuild.UseVisualStyleBackColor = true;
            // 
            // ckbSell
            // 
            this.ckbSell.AutoSize = true;
            this.ckbSell.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ckbSell.Location = new System.Drawing.Point(200, 283);
            this.ckbSell.Name = "ckbSell";
            this.ckbSell.Size = new System.Drawing.Size(107, 17);
            this.ckbSell.TabIndex = 7;
            this.ckbSell.UseVisualStyleBackColor = true;
            // 
            // txbFollow
            // 
            this.txbFollow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txbFollow.Location = new System.Drawing.Point(200, 306);
            this.txbFollow.Name = "txbFollow";
            this.txbFollow.Size = new System.Drawing.Size(107, 25);
            this.txbFollow.TabIndex = 6;
            // 
            // ckbRepair
            // 
            this.ckbRepair.AutoSize = true;
            this.ckbRepair.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ckbRepair.Location = new System.Drawing.Point(200, 337);
            this.ckbRepair.Name = "ckbRepair";
            this.ckbRepair.Size = new System.Drawing.Size(107, 17);
            this.ckbRepair.TabIndex = 7;
            this.ckbRepair.UseVisualStyleBackColor = true;
            // 
            // ckbRebuild
            // 
            this.ckbRebuild.AutoSize = true;
            this.ckbRebuild.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ckbRebuild.Location = new System.Drawing.Point(200, 360);
            this.ckbRebuild.Name = "ckbRebuild";
            this.ckbRebuild.Size = new System.Drawing.Size(107, 17);
            this.ckbRebuild.TabIndex = 7;
            this.ckbRebuild.UseVisualStyleBackColor = true;
            // 
            // ckbShowName
            // 
            this.ckbShowName.AutoSize = true;
            this.ckbShowName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ckbShowName.Location = new System.Drawing.Point(200, 383);
            this.ckbShowName.Name = "ckbShowName";
            this.ckbShowName.Size = new System.Drawing.Size(107, 17);
            this.ckbShowName.TabIndex = 7;
            this.ckbShowName.UseVisualStyleBackColor = true;
            // 
            // ckbPowered
            // 
            this.ckbPowered.AutoSize = true;
            this.ckbPowered.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ckbPowered.Location = new System.Drawing.Point(200, 406);
            this.ckbPowered.Name = "ckbPowered";
            this.ckbPowered.Size = new System.Drawing.Size(107, 17);
            this.ckbPowered.TabIndex = 7;
            this.ckbPowered.UseVisualStyleBackColor = true;
            // 
            // cbbSpotlight
            // 
            this.cbbSpotlight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbbSpotlight.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbSpotlight.FormattingEnabled = true;
            this.cbbSpotlight.Location = new System.Drawing.Point(200, 429);
            this.cbbSpotlight.Name = "cbbSpotlight";
            this.cbbSpotlight.Size = new System.Drawing.Size(107, 23);
            this.cbbSpotlight.TabIndex = 3;
            // 
            // mtxbUpgNum
            // 
            this.mtxbUpgNum.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mtxbUpgNum.Location = new System.Drawing.Point(200, 458);
            this.mtxbUpgNum.Name = "mtxbUpgNum";
            this.mtxbUpgNum.Size = new System.Drawing.Size(107, 25);
            this.mtxbUpgNum.TabIndex = 5;
            // 
            // cbbUpg1
            // 
            this.cbbUpg1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbbUpg1.FormattingEnabled = true;
            this.cbbUpg1.Location = new System.Drawing.Point(200, 489);
            this.cbbUpg1.Name = "cbbUpg1";
            this.cbbUpg1.Size = new System.Drawing.Size(107, 23);
            this.cbbUpg1.TabIndex = 3;
            // 
            // cbbUpg2
            // 
            this.cbbUpg2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbbUpg2.FormattingEnabled = true;
            this.cbbUpg2.Location = new System.Drawing.Point(200, 518);
            this.cbbUpg2.Name = "cbbUpg2";
            this.cbbUpg2.Size = new System.Drawing.Size(107, 23);
            this.cbbUpg2.TabIndex = 3;
            // 
            // cbbUpg3
            // 
            this.cbbUpg3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbbUpg3.FormattingEnabled = true;
            this.cbbUpg3.Location = new System.Drawing.Point(200, 547);
            this.cbbUpg3.Name = "cbbUpg3";
            this.cbbUpg3.Size = new System.Drawing.Size(107, 23);
            this.cbbUpg3.TabIndex = 3;
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
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.ComboBox cbbOwner;
        private System.Windows.Forms.MaskedTextBox mtxbHp;
        private System.Windows.Forms.MaskedTextBox mtxbVeteran;
        private System.Windows.Forms.MaskedTextBox mtxbFacing;
        private System.Windows.Forms.ComboBox cbbAttTag;
        private System.Windows.Forms.ComboBox cbbStat;
        private System.Windows.Forms.TextBox txbGroup;
        private System.Windows.Forms.CheckBox ckbOnBrg;
        private System.Windows.Forms.CheckBox ckbRecruit;
        private System.Windows.Forms.CheckBox ckbUnitRebuild;
        private System.Windows.Forms.CheckBox ckbSell;
        private System.Windows.Forms.TextBox txbFollow;
        private System.Windows.Forms.CheckBox ckbRepair;
        private System.Windows.Forms.CheckBox ckbRebuild;
        private System.Windows.Forms.CheckBox ckbShowName;
        private System.Windows.Forms.CheckBox ckbPowered;
        private System.Windows.Forms.ComboBox cbbSpotlight;
        private System.Windows.Forms.MaskedTextBox mtxbUpgNum;
        private System.Windows.Forms.ComboBox cbbUpg1;
        private System.Windows.Forms.ComboBox cbbUpg2;
        private System.Windows.Forms.ComboBox cbbUpg3;
    }
}
