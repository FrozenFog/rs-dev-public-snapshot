namespace relert_sharp
{
    partial class MainWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.pnlRightMenuContainer = new System.Windows.Forms.Panel();
            this.pnlMainContainer = new System.Windows.Forms.Panel();
            this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
            this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
            this.pnlInMain = new relert_sharp.DrawingEngine.DisplayingControl();
            this.pnlBottomMenuContainer = new System.Windows.Forms.Panel();
            this.pnlLeftMenuContainer = new System.Windows.Forms.Panel();
            this.tspMain = new System.Windows.Forms.ToolStrip();
            this.mnbtnPointer = new System.Windows.Forms.ToolStripButton();
            this.mnbtnTileBrush = new System.Windows.Forms.ToolStripButton();
            this.mnbtnUnitSelect = new System.Windows.Forms.ToolStripButton();
            this.mnsMain = new System.Windows.Forms.MenuStrip();
            this.menuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTools = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.statSp = new System.Windows.Forms.StatusStrip();
            this.statLbl = new System.Windows.Forms.ToolStripStatusLabel();
            this.statSelectedTarget = new System.Windows.Forms.ToolStripStatusLabel();
            this.statX = new System.Windows.Forms.ToolStripStatusLabel();
            this.statY = new System.Windows.Forms.ToolStripStatusLabel();
            this.statHeight = new System.Windows.Forms.ToolStripStatusLabel();
            this.statTileIndex = new System.Windows.Forms.ToolStripStatusLabel();
            this.statTileSubindex = new System.Windows.Forms.ToolStripStatusLabel();
            this.statOverlay = new System.Windows.Forms.ToolStripStatusLabel();
            this.statWarning = new System.Windows.Forms.ToolStripStatusLabel();
            this.statBar = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.pnlMainContainer.SuspendLayout();
            this.pnlLeftMenuContainer.SuspendLayout();
            this.tspMain.SuspendLayout();
            this.mnsMain.SuspendLayout();
            this.statSp.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlRightMenuContainer
            // 
            this.pnlRightMenuContainer.AutoScroll = true;
            this.pnlRightMenuContainer.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlRightMenuContainer.Location = new System.Drawing.Point(999, 25);
            this.pnlRightMenuContainer.Name = "pnlRightMenuContainer";
            this.pnlRightMenuContainer.Size = new System.Drawing.Size(173, 649);
            this.pnlRightMenuContainer.TabIndex = 0;
            // 
            // pnlMainContainer
            // 
            this.pnlMainContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlMainContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.pnlMainContainer.Controls.Add(this.vScrollBar1);
            this.pnlMainContainer.Controls.Add(this.hScrollBar1);
            this.pnlMainContainer.Controls.Add(this.pnlInMain);
            this.pnlMainContainer.Location = new System.Drawing.Point(189, 52);
            this.pnlMainContainer.Name = "pnlMainContainer";
            this.pnlMainContainer.Size = new System.Drawing.Size(810, 465);
            this.pnlMainContainer.TabIndex = 1;
            // 
            // vScrollBar1
            // 
            this.vScrollBar1.Dock = System.Windows.Forms.DockStyle.Right;
            this.vScrollBar1.Location = new System.Drawing.Point(790, 0);
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new System.Drawing.Size(20, 445);
            this.vScrollBar1.TabIndex = 1;
            // 
            // hScrollBar1
            // 
            this.hScrollBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.hScrollBar1.Location = new System.Drawing.Point(0, 445);
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.Size = new System.Drawing.Size(810, 20);
            this.hScrollBar1.TabIndex = 0;
            // 
            // pnlInMain
            // 
            this.pnlInMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.pnlInMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlInMain.Location = new System.Drawing.Point(0, 0);
            this.pnlInMain.Name = "pnlInMain";
            this.pnlInMain.Size = new System.Drawing.Size(810, 465);
            this.pnlInMain.TabIndex = 0;
            this.pnlInMain.VSync = false;
            this.pnlInMain.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlInMain_Paint);
            this.pnlInMain.Resize += new System.EventHandler(this.pnlInMain_Resize);
            // 
            // pnlBottomMenuContainer
            // 
            this.pnlBottomMenuContainer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlBottomMenuContainer.Location = new System.Drawing.Point(189, 520);
            this.pnlBottomMenuContainer.Name = "pnlBottomMenuContainer";
            this.pnlBottomMenuContainer.Size = new System.Drawing.Size(810, 129);
            this.pnlBottomMenuContainer.TabIndex = 2;
            // 
            // pnlLeftMenuContainer
            // 
            this.pnlLeftMenuContainer.Controls.Add(this.tspMain);
            this.pnlLeftMenuContainer.Location = new System.Drawing.Point(0, 52);
            this.pnlLeftMenuContainer.Name = "pnlLeftMenuContainer";
            this.pnlLeftMenuContainer.Size = new System.Drawing.Size(186, 597);
            this.pnlLeftMenuContainer.TabIndex = 3;
            // 
            // tspMain
            // 
            this.tspMain.AutoSize = false;
            this.tspMain.BackColor = System.Drawing.Color.LightGray;
            this.tspMain.Dock = System.Windows.Forms.DockStyle.Left;
            this.tspMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnbtnPointer,
            this.mnbtnTileBrush,
            this.mnbtnUnitSelect});
            this.tspMain.Location = new System.Drawing.Point(0, 0);
            this.tspMain.Name = "tspMain";
            this.tspMain.Size = new System.Drawing.Size(30, 597);
            this.tspMain.TabIndex = 5;
            this.tspMain.Text = "toolStrip1";
            // 
            // mnbtnPointer
            // 
            this.mnbtnPointer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mnbtnPointer.Image = ((System.Drawing.Image)(resources.GetObject("mnbtnPointer.Image")));
            this.mnbtnPointer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnbtnPointer.Name = "mnbtnPointer";
            this.mnbtnPointer.Size = new System.Drawing.Size(28, 20);
            this.mnbtnPointer.Text = "toolStripButton1";
            // 
            // mnbtnTileBrush
            // 
            this.mnbtnTileBrush.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mnbtnTileBrush.Image = ((System.Drawing.Image)(resources.GetObject("mnbtnTileBrush.Image")));
            this.mnbtnTileBrush.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnbtnTileBrush.Name = "mnbtnTileBrush";
            this.mnbtnTileBrush.Size = new System.Drawing.Size(28, 20);
            this.mnbtnTileBrush.Text = "toolStripButton1";
            // 
            // mnbtnUnitSelect
            // 
            this.mnbtnUnitSelect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mnbtnUnitSelect.Image = ((System.Drawing.Image)(resources.GetObject("mnbtnUnitSelect.Image")));
            this.mnbtnUnitSelect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnbtnUnitSelect.Name = "mnbtnUnitSelect";
            this.mnbtnUnitSelect.Size = new System.Drawing.Size(28, 20);
            this.mnbtnUnitSelect.Text = "toolStripButton1";
            // 
            // mnsMain
            // 
            this.mnsMain.BackColor = System.Drawing.Color.LightGray;
            this.mnsMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.mnsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFile,
            this.menuEdit,
            this.menuTools,
            this.menuHelp});
            this.mnsMain.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.mnsMain.Location = new System.Drawing.Point(0, 0);
            this.mnsMain.Name = "mnsMain";
            this.mnsMain.Size = new System.Drawing.Size(1172, 25);
            this.mnsMain.TabIndex = 4;
            this.mnsMain.Text = "menuStrip1";
            // 
            // menuFile
            // 
            this.menuFile.AutoSize = false;
            this.menuFile.Name = "menuFile";
            this.menuFile.Size = new System.Drawing.Size(50, 21);
            this.menuFile.Text = "&File";
            // 
            // menuEdit
            // 
            this.menuEdit.AutoSize = false;
            this.menuEdit.Name = "menuEdit";
            this.menuEdit.Size = new System.Drawing.Size(42, 21);
            this.menuEdit.Text = "&Edit";
            // 
            // menuTools
            // 
            this.menuTools.AutoSize = false;
            this.menuTools.Name = "menuTools";
            this.menuTools.Size = new System.Drawing.Size(50, 21);
            this.menuTools.Text = "&Tools";
            // 
            // menuHelp
            // 
            this.menuHelp.AutoSize = false;
            this.menuHelp.Name = "menuHelp";
            this.menuHelp.ShortcutKeyDisplayString = "";
            this.menuHelp.Size = new System.Drawing.Size(50, 21);
            this.menuHelp.Text = "&Help";
            // 
            // statSp
            // 
            this.statSp.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statLbl,
            this.statSelectedTarget,
            this.statX,
            this.statY,
            this.statHeight,
            this.statTileIndex,
            this.statTileSubindex,
            this.statOverlay,
            this.statWarning,
            this.statBar});
            this.statSp.Location = new System.Drawing.Point(0, 652);
            this.statSp.Name = "statSp";
            this.statSp.Size = new System.Drawing.Size(999, 22);
            this.statSp.TabIndex = 6;
            this.statSp.Text = "statusStrip1";
            // 
            // statLbl
            // 
            this.statLbl.AutoSize = false;
            this.statLbl.Name = "statLbl";
            this.statLbl.Size = new System.Drawing.Size(180, 17);
            this.statLbl.Text = "RSMWstatReady";
            // 
            // statSelectedTarget
            // 
            this.statSelectedTarget.AutoSize = false;
            this.statSelectedTarget.Name = "statSelectedTarget";
            this.statSelectedTarget.Size = new System.Drawing.Size(140, 17);
            this.statSelectedTarget.Text = "RSMWstatSelected";
            // 
            // statX
            // 
            this.statX.AutoSize = false;
            this.statX.Name = "statX";
            this.statX.Size = new System.Drawing.Size(50, 17);
            this.statX.Text = "1000";
            // 
            // statY
            // 
            this.statY.AutoSize = false;
            this.statY.Name = "statY";
            this.statY.Size = new System.Drawing.Size(50, 17);
            this.statY.Text = "1000";
            // 
            // statHeight
            // 
            this.statHeight.AutoSize = false;
            this.statHeight.Name = "statHeight";
            this.statHeight.Size = new System.Drawing.Size(30, 17);
            this.statHeight.Text = "10";
            // 
            // statTileIndex
            // 
            this.statTileIndex.AutoSize = false;
            this.statTileIndex.Name = "statTileIndex";
            this.statTileIndex.Size = new System.Drawing.Size(60, 17);
            this.statTileIndex.Text = "00(0x00)";
            // 
            // statTileSubindex
            // 
            this.statTileSubindex.AutoSize = false;
            this.statTileSubindex.Name = "statTileSubindex";
            this.statTileSubindex.Size = new System.Drawing.Size(60, 17);
            this.statTileSubindex.Text = "00(0x00)";
            // 
            // statOverlay
            // 
            this.statOverlay.AutoSize = false;
            this.statOverlay.Name = "statOverlay";
            this.statOverlay.Size = new System.Drawing.Size(140, 17);
            this.statOverlay.Text = "RSMWstatOverlay";
            // 
            // statWarning
            // 
            this.statWarning.AutoSize = false;
            this.statWarning.ForeColor = System.Drawing.Color.Maroon;
            this.statWarning.Name = "statWarning";
            this.statWarning.Size = new System.Drawing.Size(150, 17);
            this.statWarning.Text = "RSMWstatWarning";
            this.statWarning.Visible = false;
            // 
            // statBar
            // 
            this.statBar.AutoSize = false;
            this.statBar.Name = "statBar";
            this.statBar.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.statBar.Size = new System.Drawing.Size(160, 16);
            this.statBar.Visible = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.LightGray;
            this.toolStrip1.Location = new System.Drawing.Point(0, 25);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(999, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(1172, 674);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statSp);
            this.Controls.Add(this.pnlLeftMenuContainer);
            this.Controls.Add(this.pnlBottomMenuContainer);
            this.Controls.Add(this.pnlMainContainer);
            this.Controls.Add(this.pnlRightMenuContainer);
            this.Controls.Add(this.mnsMain);
            this.MainMenuStrip = this.mnsMain;
            this.Name = "MainWindow";
            this.Text = "RSMWTitle";
            this.pnlMainContainer.ResumeLayout(false);
            this.pnlLeftMenuContainer.ResumeLayout(false);
            this.tspMain.ResumeLayout(false);
            this.tspMain.PerformLayout();
            this.mnsMain.ResumeLayout(false);
            this.mnsMain.PerformLayout();
            this.statSp.ResumeLayout(false);
            this.statSp.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlRightMenuContainer;
        private System.Windows.Forms.Panel pnlMainContainer;
        private System.Windows.Forms.Panel pnlBottomMenuContainer;
        private relert_sharp.DrawingEngine.DisplayingControl pnlInMain;
        private System.Windows.Forms.VScrollBar vScrollBar1;
        private System.Windows.Forms.HScrollBar hScrollBar1;
        private System.Windows.Forms.Panel pnlLeftMenuContainer;
        private System.Windows.Forms.MenuStrip mnsMain;
        private System.Windows.Forms.ToolStrip tspMain;
        private System.Windows.Forms.StatusStrip statSp;
        private System.Windows.Forms.ToolStripStatusLabel statLbl;
        private System.Windows.Forms.ToolStripProgressBar statBar;
        private System.Windows.Forms.ToolStripStatusLabel statSelectedTarget;
        private System.Windows.Forms.ToolStripStatusLabel statX;
        private System.Windows.Forms.ToolStripStatusLabel statY;
        private System.Windows.Forms.ToolStripStatusLabel statHeight;
        private System.Windows.Forms.ToolStripStatusLabel statTileIndex;
        private System.Windows.Forms.ToolStripStatusLabel statTileSubindex;
        private System.Windows.Forms.ToolStripStatusLabel statOverlay;
        private System.Windows.Forms.ToolStripStatusLabel statWarning;
        private System.Windows.Forms.ToolStripMenuItem menuFile;
        private System.Windows.Forms.ToolStripMenuItem menuEdit;
        private System.Windows.Forms.ToolStripMenuItem menuTools;
        private System.Windows.Forms.ToolStripMenuItem menuHelp;
        private System.Windows.Forms.ToolStripButton mnbtnPointer;
        private System.Windows.Forms.ToolStripButton mnbtnTileBrush;
        private System.Windows.Forms.ToolStripButton mnbtnUnitSelect;
        private System.Windows.Forms.ToolStrip toolStrip1;
    }
}