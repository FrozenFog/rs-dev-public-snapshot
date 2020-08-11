namespace RelertSharp.GUI.SubWindows.LogicEditor
{
    internal partial class LogicEditor
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
            this.tbcMain = new System.Windows.Forms.TabControl();
            this.tbpTriggers = new System.Windows.Forms.TabPage();
            this.splitTriggerMain = new System.Windows.Forms.SplitContainer();
            this.lbxTriggerList = new System.Windows.Forms.ListBox();
            this.cmsTriggerList = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiTrgLstAscending = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTrgLstDecending = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiTrgLstID = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTrgLstName = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTrgLstIDName = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.splitTagEA = new System.Windows.Forms.SplitContainer();
            this.splitEA = new System.Windows.Forms.SplitContainer();
            this.tbpTaskScriptPage = new System.Windows.Forms.TabPage();
            this.splitTaskScript = new System.Windows.Forms.SplitContainer();
            this.gpbTeamTask = new System.Windows.Forms.GroupBox();
            this.splitTaskforce = new System.Windows.Forms.SplitContainer();
            this.lbxTaskList = new System.Windows.Forms.ListBox();
            this.label36 = new System.Windows.Forms.Label();
            this.gpbTeamScript = new System.Windows.Forms.GroupBox();
            this.splitScript = new System.Windows.Forms.SplitContainer();
            this.lbxScriptList = new System.Windows.Forms.ListBox();
            this.label33 = new System.Windows.Forms.Label();
            this.tbpTeams = new System.Windows.Forms.TabPage();
            this.gpbTeamAI = new System.Windows.Forms.GroupBox();
            this.olvAIConfig = new BrightIdeasSoftware.ObjectListView();
            this.olvColAIKey = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColAIValue = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.lbxAIList = new System.Windows.Forms.ListBox();
            this.btnCopyAI = new System.Windows.Forms.Button();
            this.btnNewAI = new System.Windows.Forms.Button();
            this.btnDelAI = new System.Windows.Forms.Button();
            this.label22 = new System.Windows.Forms.Label();
            this.gpbTeamTeam = new System.Windows.Forms.GroupBox();
            this.olvTeamConfig = new BrightIdeasSoftware.ObjectListView();
            this.olvColTeamKey = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColTeamValue = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.lbxTeamList = new System.Windows.Forms.ListBox();
            this.btnCopyTeam = new System.Windows.Forms.Button();
            this.btnNewTeam = new System.Windows.Forms.Button();
            this.btnDelTeam = new System.Windows.Forms.Button();
            this.label28 = new System.Windows.Forms.Label();
            this.tbpMiscs = new System.Windows.Forms.TabPage();
            this.gpbMap = new System.Windows.Forms.GroupBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.olvBasic = new BrightIdeasSoftware.ObjectListView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label26 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.chklbxSpecialFlag = new System.Windows.Forms.CheckedListBox();
            this.chklbxBasic = new System.Windows.Forms.CheckedListBox();
            this.gpbHouses = new System.Windows.Forms.GroupBox();
            this.olvHouse = new BrightIdeasSoftware.ObjectListView();
            this.olvColHouseKey = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColHouseValue = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.LGCgpbHouseAllies = new System.Windows.Forms.GroupBox();
            this.btnGoAllie = new System.Windows.Forms.Button();
            this.btnGoEnemy = new System.Windows.Forms.Button();
            this.lbxHouseEnemy = new System.Windows.Forms.ListBox();
            this.label24 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.lbxHouseAllie = new System.Windows.Forms.ListBox();
            this.txbHouseAllies = new System.Windows.Forms.TextBox();
            this.btnDelHouse = new System.Windows.Forms.Button();
            this.btnNewHouse = new System.Windows.Forms.Button();
            this.lbxHouses = new System.Windows.Forms.ListBox();
            this.gpbLocalVar = new System.Windows.Forms.GroupBox();
            this.txbLocalName = new System.Windows.Forms.TextBox();
            this.label35 = new System.Windows.Forms.Label();
            this.btnNewLocalVar = new System.Windows.Forms.Button();
            this.chklbxLocalVar = new System.Windows.Forms.CheckedListBox();
            this.ttTrg = new System.Windows.Forms.ToolTip(this.components);
            this.pnlSearch = new RelertSharp.GUI.SubWindows.LogicEditor.PanelGlobalSearch();
            this.pnlTriggerTag = new RelertSharp.GUI.SubWindows.LogicEditor.PanelTrgTag();
            this.pnlEvent = new RelertSharp.GUI.SubWindows.LogicEditor.PanelEvent();
            this.pnlAction = new RelertSharp.GUI.SubWindows.LogicEditor.PanelEvent();
            this.pnlTaskforce = new RelertSharp.GUI.SubWindows.LogicEditor.PanelTaskforce();
            this.pnlScript = new RelertSharp.GUI.SubWindows.LogicEditor.PanelScript();
            this.splitTeamAI = new System.Windows.Forms.SplitContainer();
            this.splitTeam = new System.Windows.Forms.SplitContainer();
            this.splitAI = new System.Windows.Forms.SplitContainer();
            this.tbcMain.SuspendLayout();
            this.tbpTriggers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitTriggerMain)).BeginInit();
            this.splitTriggerMain.Panel1.SuspendLayout();
            this.splitTriggerMain.Panel2.SuspendLayout();
            this.splitTriggerMain.SuspendLayout();
            this.cmsTriggerList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitTagEA)).BeginInit();
            this.splitTagEA.Panel1.SuspendLayout();
            this.splitTagEA.Panel2.SuspendLayout();
            this.splitTagEA.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitEA)).BeginInit();
            this.splitEA.Panel1.SuspendLayout();
            this.splitEA.Panel2.SuspendLayout();
            this.splitEA.SuspendLayout();
            this.tbpTaskScriptPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitTaskScript)).BeginInit();
            this.splitTaskScript.Panel1.SuspendLayout();
            this.splitTaskScript.Panel2.SuspendLayout();
            this.splitTaskScript.SuspendLayout();
            this.gpbTeamTask.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitTaskforce)).BeginInit();
            this.splitTaskforce.Panel1.SuspendLayout();
            this.splitTaskforce.Panel2.SuspendLayout();
            this.splitTaskforce.SuspendLayout();
            this.gpbTeamScript.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitScript)).BeginInit();
            this.splitScript.Panel1.SuspendLayout();
            this.splitScript.Panel2.SuspendLayout();
            this.splitScript.SuspendLayout();
            this.tbpTeams.SuspendLayout();
            this.gpbTeamAI.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olvAIConfig)).BeginInit();
            this.gpbTeamTeam.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olvTeamConfig)).BeginInit();
            this.tbpMiscs.SuspendLayout();
            this.gpbMap.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olvBasic)).BeginInit();
            this.panel1.SuspendLayout();
            this.gpbHouses.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olvHouse)).BeginInit();
            this.LGCgpbHouseAllies.SuspendLayout();
            this.gpbLocalVar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitTeamAI)).BeginInit();
            this.splitTeamAI.Panel1.SuspendLayout();
            this.splitTeamAI.Panel2.SuspendLayout();
            this.splitTeamAI.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitTeam)).BeginInit();
            this.splitTeam.Panel1.SuspendLayout();
            this.splitTeam.Panel2.SuspendLayout();
            this.splitTeam.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitAI)).BeginInit();
            this.splitAI.Panel1.SuspendLayout();
            this.splitAI.Panel2.SuspendLayout();
            this.splitAI.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbcMain
            // 
            this.tbcMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbcMain.Controls.Add(this.tbpTriggers);
            this.tbcMain.Controls.Add(this.tbpTaskScriptPage);
            this.tbcMain.Controls.Add(this.tbpTeams);
            this.tbcMain.Controls.Add(this.tbpMiscs);
            this.tbcMain.Location = new System.Drawing.Point(16, 41);
            this.tbcMain.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbcMain.Name = "tbcMain";
            this.tbcMain.SelectedIndex = 0;
            this.tbcMain.Size = new System.Drawing.Size(979, 739);
            this.tbcMain.TabIndex = 0;
            // 
            // tbpTriggers
            // 
            this.tbpTriggers.BackColor = System.Drawing.Color.Transparent;
            this.tbpTriggers.Controls.Add(this.splitTriggerMain);
            this.tbpTriggers.Location = new System.Drawing.Point(4, 25);
            this.tbpTriggers.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbpTriggers.Name = "tbpTriggers";
            this.tbpTriggers.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbpTriggers.Size = new System.Drawing.Size(971, 710);
            this.tbpTriggers.TabIndex = 0;
            this.tbpTriggers.Text = "LGCtbpTrgPage";
            // 
            // splitTriggerMain
            // 
            this.splitTriggerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitTriggerMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitTriggerMain.Location = new System.Drawing.Point(4, 4);
            this.splitTriggerMain.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.splitTriggerMain.Name = "splitTriggerMain";
            // 
            // splitTriggerMain.Panel1
            // 
            this.splitTriggerMain.Panel1.Controls.Add(this.lbxTriggerList);
            this.splitTriggerMain.Panel1.Controls.Add(this.label1);
            // 
            // splitTriggerMain.Panel2
            // 
            this.splitTriggerMain.Panel2.Controls.Add(this.splitTagEA);
            this.splitTriggerMain.Size = new System.Drawing.Size(963, 702);
            this.splitTriggerMain.SplitterDistance = 242;
            this.splitTriggerMain.TabIndex = 10;
            // 
            // lbxTriggerList
            // 
            this.lbxTriggerList.ContextMenuStrip = this.cmsTriggerList;
            this.lbxTriggerList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbxTriggerList.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbxTriggerList.FormattingEnabled = true;
            this.lbxTriggerList.HorizontalScrollbar = true;
            this.lbxTriggerList.ItemHeight = 15;
            this.lbxTriggerList.Location = new System.Drawing.Point(0, 15);
            this.lbxTriggerList.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lbxTriggerList.Name = "lbxTriggerList";
            this.lbxTriggerList.Size = new System.Drawing.Size(242, 687);
            this.lbxTriggerList.Sorted = true;
            this.lbxTriggerList.TabIndex = 2;
            this.lbxTriggerList.SelectedValueChanged += new System.EventHandler(this.lbxTriggerList_SelectedValueChanged);
            // 
            // cmsTriggerList
            // 
            this.cmsTriggerList.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmsTriggerList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiTrgLstAscending,
            this.tsmiTrgLstDecending,
            this.toolStripSeparator1,
            this.tsmiTrgLstID,
            this.tsmiTrgLstName,
            this.tsmiTrgLstIDName});
            this.cmsTriggerList.Name = "cmsTriggerList";
            this.cmsTriggerList.Size = new System.Drawing.Size(264, 130);
            // 
            // tsmiTrgLstAscending
            // 
            this.tsmiTrgLstAscending.Name = "tsmiTrgLstAscending";
            this.tsmiTrgLstAscending.Size = new System.Drawing.Size(263, 24);
            this.tsmiTrgLstAscending.Text = "LGCtsmiAscend";
            this.tsmiTrgLstAscending.Click += new System.EventHandler(this.tsmiTrgLstAscending_Click);
            // 
            // tsmiTrgLstDecending
            // 
            this.tsmiTrgLstDecending.Name = "tsmiTrgLstDecending";
            this.tsmiTrgLstDecending.Size = new System.Drawing.Size(263, 24);
            this.tsmiTrgLstDecending.Text = "LGCtsmiDecend";
            this.tsmiTrgLstDecending.Click += new System.EventHandler(this.tsmiTrgLstDecending_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(260, 6);
            // 
            // tsmiTrgLstID
            // 
            this.tsmiTrgLstID.Name = "tsmiTrgLstID";
            this.tsmiTrgLstID.Size = new System.Drawing.Size(263, 24);
            this.tsmiTrgLstID.Text = "LGCtsmiShowID";
            this.tsmiTrgLstID.Click += new System.EventHandler(this.tsmiTrgLstID_Click);
            // 
            // tsmiTrgLstName
            // 
            this.tsmiTrgLstName.Name = "tsmiTrgLstName";
            this.tsmiTrgLstName.Size = new System.Drawing.Size(263, 24);
            this.tsmiTrgLstName.Text = "LGCtsmiShowName";
            this.tsmiTrgLstName.Click += new System.EventHandler(this.tsmiTrgLstName_Click);
            // 
            // tsmiTrgLstIDName
            // 
            this.tsmiTrgLstIDName.Name = "tsmiTrgLstIDName";
            this.tsmiTrgLstIDName.Size = new System.Drawing.Size(263, 24);
            this.tsmiTrgLstIDName.Text = "LGCtsmiShowIDandName";
            this.tsmiTrgLstIDName.Click += new System.EventHandler(this.tsmiTrgLstIDName_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "LGClblTrgList";
            // 
            // splitTagEA
            // 
            this.splitTagEA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitTagEA.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitTagEA.Location = new System.Drawing.Point(0, 0);
            this.splitTagEA.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.splitTagEA.Name = "splitTagEA";
            this.splitTagEA.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitTagEA.Panel1
            // 
            this.splitTagEA.Panel1.Controls.Add(this.pnlTriggerTag);
            // 
            // splitTagEA.Panel2
            // 
            this.splitTagEA.Panel2.Controls.Add(this.splitEA);
            this.splitTagEA.Size = new System.Drawing.Size(717, 702);
            this.splitTagEA.SplitterDistance = 236;
            this.splitTagEA.TabIndex = 0;
            // 
            // splitEA
            // 
            this.splitEA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitEA.Location = new System.Drawing.Point(0, 0);
            this.splitEA.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.splitEA.Name = "splitEA";
            // 
            // splitEA.Panel1
            // 
            this.splitEA.Panel1.Controls.Add(this.pnlEvent);
            // 
            // splitEA.Panel2
            // 
            this.splitEA.Panel2.Controls.Add(this.pnlAction);
            this.splitEA.Size = new System.Drawing.Size(717, 462);
            this.splitEA.SplitterDistance = 374;
            this.splitEA.TabIndex = 0;
            this.splitEA.Resize += new System.EventHandler(this.splitEA_Resize);
            // 
            // tbpTaskScriptPage
            // 
            this.tbpTaskScriptPage.BackColor = System.Drawing.Color.Transparent;
            this.tbpTaskScriptPage.Controls.Add(this.splitTaskScript);
            this.tbpTaskScriptPage.Location = new System.Drawing.Point(4, 25);
            this.tbpTaskScriptPage.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbpTaskScriptPage.Name = "tbpTaskScriptPage";
            this.tbpTaskScriptPage.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbpTaskScriptPage.Size = new System.Drawing.Size(971, 710);
            this.tbpTaskScriptPage.TabIndex = 3;
            this.tbpTaskScriptPage.Text = "LGCtbpTaskScriptPage";
            // 
            // splitTaskScript
            // 
            this.splitTaskScript.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitTaskScript.Location = new System.Drawing.Point(3, 2);
            this.splitTaskScript.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.splitTaskScript.Name = "splitTaskScript";
            // 
            // splitTaskScript.Panel1
            // 
            this.splitTaskScript.Panel1.Controls.Add(this.gpbTeamTask);
            // 
            // splitTaskScript.Panel2
            // 
            this.splitTaskScript.Panel2.Controls.Add(this.gpbTeamScript);
            this.splitTaskScript.Size = new System.Drawing.Size(965, 706);
            this.splitTaskScript.SplitterDistance = 467;
            this.splitTaskScript.TabIndex = 6;
            this.splitTaskScript.Resize += new System.EventHandler(this.splitTaskScript_Resize);
            // 
            // gpbTeamTask
            // 
            this.gpbTeamTask.Controls.Add(this.splitTaskforce);
            this.gpbTeamTask.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gpbTeamTask.Location = new System.Drawing.Point(0, 0);
            this.gpbTeamTask.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gpbTeamTask.Name = "gpbTeamTask";
            this.gpbTeamTask.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gpbTeamTask.Size = new System.Drawing.Size(467, 706);
            this.gpbTeamTask.TabIndex = 5;
            this.gpbTeamTask.TabStop = false;
            this.gpbTeamTask.Text = "LGCgpbTeamTask";
            // 
            // splitTaskforce
            // 
            this.splitTaskforce.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitTaskforce.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitTaskforce.Location = new System.Drawing.Point(3, 20);
            this.splitTaskforce.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.splitTaskforce.Name = "splitTaskforce";
            // 
            // splitTaskforce.Panel1
            // 
            this.splitTaskforce.Panel1.Controls.Add(this.lbxTaskList);
            this.splitTaskforce.Panel1.Controls.Add(this.label36);
            // 
            // splitTaskforce.Panel2
            // 
            this.splitTaskforce.Panel2.Controls.Add(this.pnlTaskforce);
            this.splitTaskforce.Size = new System.Drawing.Size(461, 684);
            this.splitTaskforce.SplitterDistance = 244;
            this.splitTaskforce.TabIndex = 3;
            // 
            // lbxTaskList
            // 
            this.lbxTaskList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbxTaskList.FormattingEnabled = true;
            this.lbxTaskList.HorizontalScrollbar = true;
            this.lbxTaskList.ItemHeight = 15;
            this.lbxTaskList.Location = new System.Drawing.Point(0, 15);
            this.lbxTaskList.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lbxTaskList.Name = "lbxTaskList";
            this.lbxTaskList.Size = new System.Drawing.Size(244, 669);
            this.lbxTaskList.Sorted = true;
            this.lbxTaskList.TabIndex = 0;
            this.lbxTaskList.SelectedValueChanged += new System.EventHandler(this.lbxTaskList_SelectedValueChanged);
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Dock = System.Windows.Forms.DockStyle.Top;
            this.label36.Location = new System.Drawing.Point(0, 0);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(119, 15);
            this.label36.TabIndex = 1;
            this.label36.Text = "LGClblTaskList";
            // 
            // gpbTeamScript
            // 
            this.gpbTeamScript.Controls.Add(this.splitScript);
            this.gpbTeamScript.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gpbTeamScript.Location = new System.Drawing.Point(0, 0);
            this.gpbTeamScript.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gpbTeamScript.Name = "gpbTeamScript";
            this.gpbTeamScript.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gpbTeamScript.Size = new System.Drawing.Size(494, 706);
            this.gpbTeamScript.TabIndex = 4;
            this.gpbTeamScript.TabStop = false;
            this.gpbTeamScript.Text = "LGCgpbTeamScript";
            // 
            // splitScript
            // 
            this.splitScript.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitScript.Location = new System.Drawing.Point(3, 20);
            this.splitScript.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.splitScript.Name = "splitScript";
            // 
            // splitScript.Panel1
            // 
            this.splitScript.Panel1.Controls.Add(this.lbxScriptList);
            this.splitScript.Panel1.Controls.Add(this.label33);
            // 
            // splitScript.Panel2
            // 
            this.splitScript.Panel2.Controls.Add(this.pnlScript);
            this.splitScript.Size = new System.Drawing.Size(488, 684);
            this.splitScript.SplitterDistance = 179;
            this.splitScript.TabIndex = 3;
            // 
            // lbxScriptList
            // 
            this.lbxScriptList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbxScriptList.FormattingEnabled = true;
            this.lbxScriptList.HorizontalScrollbar = true;
            this.lbxScriptList.ItemHeight = 15;
            this.lbxScriptList.Location = new System.Drawing.Point(0, 15);
            this.lbxScriptList.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lbxScriptList.Name = "lbxScriptList";
            this.lbxScriptList.Size = new System.Drawing.Size(179, 669);
            this.lbxScriptList.Sorted = true;
            this.lbxScriptList.TabIndex = 0;
            this.lbxScriptList.SelectedValueChanged += new System.EventHandler(this.lbxScriptList_SelectedValueChanged);
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Dock = System.Windows.Forms.DockStyle.Top;
            this.label33.Location = new System.Drawing.Point(0, 0);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(135, 15);
            this.label33.TabIndex = 1;
            this.label33.Text = "LGClblScriptList";
            // 
            // tbpTeams
            // 
            this.tbpTeams.BackColor = System.Drawing.Color.Transparent;
            this.tbpTeams.Controls.Add(this.splitTeamAI);
            this.tbpTeams.Location = new System.Drawing.Point(4, 25);
            this.tbpTeams.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbpTeams.Name = "tbpTeams";
            this.tbpTeams.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbpTeams.Size = new System.Drawing.Size(971, 710);
            this.tbpTeams.TabIndex = 1;
            this.tbpTeams.Text = "LGCtbpTeamPage";
            // 
            // gpbTeamAI
            // 
            this.gpbTeamAI.Controls.Add(this.splitAI);
            this.gpbTeamAI.Controls.Add(this.btnCopyAI);
            this.gpbTeamAI.Controls.Add(this.btnNewAI);
            this.gpbTeamAI.Controls.Add(this.btnDelAI);
            this.gpbTeamAI.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gpbTeamAI.Location = new System.Drawing.Point(0, 0);
            this.gpbTeamAI.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gpbTeamAI.Name = "gpbTeamAI";
            this.gpbTeamAI.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gpbTeamAI.Size = new System.Drawing.Size(471, 702);
            this.gpbTeamAI.TabIndex = 3;
            this.gpbTeamAI.TabStop = false;
            this.gpbTeamAI.Text = "LGCgpbTeamAI";
            // 
            // olvAIConfig
            // 
            this.olvAIConfig.AllColumns.Add(this.olvColAIKey);
            this.olvAIConfig.AllColumns.Add(this.olvColAIValue);
            this.olvAIConfig.CellEditActivation = BrightIdeasSoftware.ObjectListView.CellEditActivateMode.SingleClickAlways;
            this.olvAIConfig.CellEditUseWholeCell = false;
            this.olvAIConfig.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColAIKey,
            this.olvColAIValue});
            this.olvAIConfig.Cursor = System.Windows.Forms.Cursors.Default;
            this.olvAIConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.olvAIConfig.FullRowSelect = true;
            this.olvAIConfig.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.olvAIConfig.HideSelection = false;
            this.olvAIConfig.Location = new System.Drawing.Point(0, 0);
            this.olvAIConfig.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.olvAIConfig.MultiSelect = false;
            this.olvAIConfig.Name = "olvAIConfig";
            this.olvAIConfig.ShowGroups = false;
            this.olvAIConfig.Size = new System.Drawing.Size(306, 680);
            this.olvAIConfig.TabIndex = 0;
            this.olvAIConfig.UseCompatibleStateImageBehavior = false;
            this.olvAIConfig.View = System.Windows.Forms.View.Details;
            this.olvAIConfig.CellEditFinished += new BrightIdeasSoftware.CellEditEventHandler(this.olvAIConfig_CellEditFinished);
            this.olvAIConfig.CellEditFinishing += new BrightIdeasSoftware.CellEditEventHandler(this.olvAIConfig_CellEditFinishing);
            this.olvAIConfig.CellEditStarting += new BrightIdeasSoftware.CellEditEventHandler(this.olvAIConfig_CellEditStarting);
            // 
            // olvColAIKey
            // 
            this.olvColAIKey.AspectName = "Value.ShowName";
            this.olvColAIKey.IsEditable = false;
            this.olvColAIKey.Text = "LGColvColAIKey";
            this.olvColAIKey.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColAIKey.Width = 133;
            // 
            // olvColAIValue
            // 
            this.olvColAIValue.AspectName = "Value.Value";
            this.olvColAIValue.Text = "LGColvColAIValue";
            this.olvColAIValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColAIValue.Width = 152;
            // 
            // lbxAIList
            // 
            this.lbxAIList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbxAIList.FormattingEnabled = true;
            this.lbxAIList.HorizontalScrollbar = true;
            this.lbxAIList.ItemHeight = 15;
            this.lbxAIList.Location = new System.Drawing.Point(0, 15);
            this.lbxAIList.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lbxAIList.Name = "lbxAIList";
            this.lbxAIList.Size = new System.Drawing.Size(155, 665);
            this.lbxAIList.TabIndex = 6;
            this.lbxAIList.SelectedIndexChanged += new System.EventHandler(this.lbxAIList_SelectedIndexChanged);
            // 
            // btnCopyAI
            // 
            this.btnCopyAI.Location = new System.Drawing.Point(228, 662);
            this.btnCopyAI.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCopyAI.Name = "btnCopyAI";
            this.btnCopyAI.Size = new System.Drawing.Size(104, 30);
            this.btnCopyAI.TabIndex = 5;
            this.btnCopyAI.Text = "LGCbtnCopyAI";
            this.btnCopyAI.UseVisualStyleBackColor = true;
            this.btnCopyAI.Click += new System.EventHandler(this.btnCopyAI_Click);
            // 
            // btnNewAI
            // 
            this.btnNewAI.Location = new System.Drawing.Point(8, 664);
            this.btnNewAI.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnNewAI.Name = "btnNewAI";
            this.btnNewAI.Size = new System.Drawing.Size(103, 29);
            this.btnNewAI.TabIndex = 3;
            this.btnNewAI.Text = "LGCbtnNewAI";
            this.btnNewAI.UseVisualStyleBackColor = true;
            this.btnNewAI.Click += new System.EventHandler(this.btnNewAI_Click);
            // 
            // btnDelAI
            // 
            this.btnDelAI.Location = new System.Drawing.Point(116, 662);
            this.btnDelAI.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDelAI.Name = "btnDelAI";
            this.btnDelAI.Size = new System.Drawing.Size(107, 30);
            this.btnDelAI.TabIndex = 4;
            this.btnDelAI.Text = "LGCbtnDelAI";
            this.btnDelAI.UseVisualStyleBackColor = true;
            this.btnDelAI.Click += new System.EventHandler(this.btnDelAI_Click);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Dock = System.Windows.Forms.DockStyle.Top;
            this.label22.Location = new System.Drawing.Point(0, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(103, 15);
            this.label22.TabIndex = 1;
            this.label22.Text = "LGClblAIList";
            // 
            // gpbTeamTeam
            // 
            this.gpbTeamTeam.Controls.Add(this.splitTeam);
            this.gpbTeamTeam.Controls.Add(this.btnCopyTeam);
            this.gpbTeamTeam.Controls.Add(this.btnNewTeam);
            this.gpbTeamTeam.Controls.Add(this.btnDelTeam);
            this.gpbTeamTeam.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gpbTeamTeam.Location = new System.Drawing.Point(0, 0);
            this.gpbTeamTeam.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gpbTeamTeam.Name = "gpbTeamTeam";
            this.gpbTeamTeam.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gpbTeamTeam.Size = new System.Drawing.Size(488, 702);
            this.gpbTeamTeam.TabIndex = 2;
            this.gpbTeamTeam.TabStop = false;
            this.gpbTeamTeam.Text = "LGCgpbTeamTeam";
            // 
            // olvTeamConfig
            // 
            this.olvTeamConfig.AllColumns.Add(this.olvColTeamKey);
            this.olvTeamConfig.AllColumns.Add(this.olvColTeamValue);
            this.olvTeamConfig.CellEditActivation = BrightIdeasSoftware.ObjectListView.CellEditActivateMode.SingleClickAlways;
            this.olvTeamConfig.CellEditUseWholeCell = false;
            this.olvTeamConfig.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColTeamKey,
            this.olvColTeamValue});
            this.olvTeamConfig.Cursor = System.Windows.Forms.Cursors.Default;
            this.olvTeamConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.olvTeamConfig.FullRowSelect = true;
            this.olvTeamConfig.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.olvTeamConfig.HideSelection = false;
            this.olvTeamConfig.Location = new System.Drawing.Point(0, 0);
            this.olvTeamConfig.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.olvTeamConfig.MultiSelect = false;
            this.olvTeamConfig.Name = "olvTeamConfig";
            this.olvTeamConfig.ShowGroups = false;
            this.olvTeamConfig.Size = new System.Drawing.Size(318, 680);
            this.olvTeamConfig.TabIndex = 0;
            this.olvTeamConfig.UseCompatibleStateImageBehavior = false;
            this.olvTeamConfig.View = System.Windows.Forms.View.Details;
            this.olvTeamConfig.CellEditFinished += new BrightIdeasSoftware.CellEditEventHandler(this.olvTeamConfig_CellEditFinished);
            this.olvTeamConfig.CellEditFinishing += new BrightIdeasSoftware.CellEditEventHandler(this.olvTeamConfig_CellEditFinishing);
            this.olvTeamConfig.CellEditStarting += new BrightIdeasSoftware.CellEditEventHandler(this.olvTeamConfig_CellEditStarting);
            // 
            // olvColTeamKey
            // 
            this.olvColTeamKey.AspectName = "Value.ShowName";
            this.olvColTeamKey.IsEditable = false;
            this.olvColTeamKey.Text = "LGColvColTeamKey";
            this.olvColTeamKey.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColTeamKey.Width = 116;
            // 
            // olvColTeamValue
            // 
            this.olvColTeamValue.AspectName = "Value.Value";
            this.olvColTeamValue.Text = "LGColvColTeamValue";
            this.olvColTeamValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColTeamValue.Width = 165;
            // 
            // lbxTeamList
            // 
            this.lbxTeamList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbxTeamList.FormattingEnabled = true;
            this.lbxTeamList.HorizontalScrollbar = true;
            this.lbxTeamList.ItemHeight = 15;
            this.lbxTeamList.Location = new System.Drawing.Point(0, 15);
            this.lbxTeamList.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lbxTeamList.Name = "lbxTeamList";
            this.lbxTeamList.Size = new System.Drawing.Size(160, 665);
            this.lbxTeamList.Sorted = true;
            this.lbxTeamList.TabIndex = 6;
            this.lbxTeamList.SelectedIndexChanged += new System.EventHandler(this.lbxTeamList_SelectedIndexChanged);
            // 
            // btnCopyTeam
            // 
            this.btnCopyTeam.Location = new System.Drawing.Point(228, 662);
            this.btnCopyTeam.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCopyTeam.Name = "btnCopyTeam";
            this.btnCopyTeam.Size = new System.Drawing.Size(104, 30);
            this.btnCopyTeam.TabIndex = 5;
            this.btnCopyTeam.Text = "LGCbtnCopyTeam";
            this.btnCopyTeam.UseVisualStyleBackColor = true;
            this.btnCopyTeam.Click += new System.EventHandler(this.btnCopyTeam_Click);
            // 
            // btnNewTeam
            // 
            this.btnNewTeam.Location = new System.Drawing.Point(8, 662);
            this.btnNewTeam.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnNewTeam.Name = "btnNewTeam";
            this.btnNewTeam.Size = new System.Drawing.Size(103, 29);
            this.btnNewTeam.TabIndex = 3;
            this.btnNewTeam.Text = "LGCbtnNewTeam";
            this.btnNewTeam.UseVisualStyleBackColor = true;
            this.btnNewTeam.Click += new System.EventHandler(this.btnNewTeam_Click);
            // 
            // btnDelTeam
            // 
            this.btnDelTeam.Location = new System.Drawing.Point(116, 662);
            this.btnDelTeam.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDelTeam.Name = "btnDelTeam";
            this.btnDelTeam.Size = new System.Drawing.Size(107, 30);
            this.btnDelTeam.TabIndex = 4;
            this.btnDelTeam.Text = "LGCbtnDelTeam";
            this.btnDelTeam.UseVisualStyleBackColor = true;
            this.btnDelTeam.Click += new System.EventHandler(this.btnDelTeam_Click);
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Dock = System.Windows.Forms.DockStyle.Top;
            this.label28.Location = new System.Drawing.Point(0, 0);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(119, 15);
            this.label28.TabIndex = 1;
            this.label28.Text = "LGClblTeamList";
            // 
            // tbpMiscs
            // 
            this.tbpMiscs.BackColor = System.Drawing.Color.Transparent;
            this.tbpMiscs.Controls.Add(this.gpbMap);
            this.tbpMiscs.Controls.Add(this.gpbHouses);
            this.tbpMiscs.Controls.Add(this.gpbLocalVar);
            this.tbpMiscs.Location = new System.Drawing.Point(4, 25);
            this.tbpMiscs.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbpMiscs.Name = "tbpMiscs";
            this.tbpMiscs.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbpMiscs.Size = new System.Drawing.Size(971, 710);
            this.tbpMiscs.TabIndex = 2;
            this.tbpMiscs.Text = "LGCtbpMiscPage";
            // 
            // gpbMap
            // 
            this.gpbMap.Controls.Add(this.splitContainer1);
            this.gpbMap.Location = new System.Drawing.Point(789, 9);
            this.gpbMap.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gpbMap.Name = "gpbMap";
            this.gpbMap.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gpbMap.Size = new System.Drawing.Size(725, 690);
            this.gpbMap.TabIndex = 4;
            this.gpbMap.TabStop = false;
            this.gpbMap.Text = "LGCgpbMap";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(4, 22);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.olvBasic);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Size = new System.Drawing.Size(717, 664);
            this.splitContainer1.SplitterDistance = 320;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 0;
            // 
            // olvBasic
            // 
            this.olvBasic.CellEditUseWholeCell = false;
            this.olvBasic.Cursor = System.Windows.Forms.Cursors.Default;
            this.olvBasic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.olvBasic.HideSelection = false;
            this.olvBasic.Location = new System.Drawing.Point(0, 0);
            this.olvBasic.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.olvBasic.Name = "olvBasic";
            this.olvBasic.Size = new System.Drawing.Size(717, 320);
            this.olvBasic.TabIndex = 0;
            this.olvBasic.UseCompatibleStateImageBehavior = false;
            this.olvBasic.View = System.Windows.Forms.View.Details;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label26);
            this.panel1.Controls.Add(this.label25);
            this.panel1.Controls.Add(this.chklbxSpecialFlag);
            this.panel1.Controls.Add(this.chklbxBasic);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(717, 339);
            this.panel1.TabIndex = 0;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Dock = System.Windows.Forms.DockStyle.Right;
            this.label26.Location = new System.Drawing.Point(566, 0);
            this.label26.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(151, 15);
            this.label26.TabIndex = 7;
            this.label26.Text = "LGClblSpecialFlags";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Dock = System.Windows.Forms.DockStyle.Left;
            this.label25.Location = new System.Drawing.Point(0, 0);
            this.label25.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(143, 15);
            this.label25.TabIndex = 6;
            this.label25.Text = "LGClblBasicChecks";
            // 
            // chklbxSpecialFlag
            // 
            this.chklbxSpecialFlag.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chklbxSpecialFlag.FormattingEnabled = true;
            this.chklbxSpecialFlag.Location = new System.Drawing.Point(363, 22);
            this.chklbxSpecialFlag.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chklbxSpecialFlag.Name = "chklbxSpecialFlag";
            this.chklbxSpecialFlag.Size = new System.Drawing.Size(349, 304);
            this.chklbxSpecialFlag.TabIndex = 5;
            // 
            // chklbxBasic
            // 
            this.chklbxBasic.FormattingEnabled = true;
            this.chklbxBasic.Location = new System.Drawing.Point(4, 22);
            this.chklbxBasic.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chklbxBasic.Name = "chklbxBasic";
            this.chklbxBasic.Size = new System.Drawing.Size(349, 304);
            this.chklbxBasic.TabIndex = 4;
            // 
            // gpbHouses
            // 
            this.gpbHouses.Controls.Add(this.olvHouse);
            this.gpbHouses.Controls.Add(this.LGCgpbHouseAllies);
            this.gpbHouses.Controls.Add(this.btnDelHouse);
            this.gpbHouses.Controls.Add(this.btnNewHouse);
            this.gpbHouses.Controls.Add(this.lbxHouses);
            this.gpbHouses.Location = new System.Drawing.Point(8, 8);
            this.gpbHouses.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gpbHouses.Name = "gpbHouses";
            this.gpbHouses.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gpbHouses.Size = new System.Drawing.Size(773, 474);
            this.gpbHouses.TabIndex = 3;
            this.gpbHouses.TabStop = false;
            this.gpbHouses.Text = "LGCgpbHouses";
            // 
            // olvHouse
            // 
            this.olvHouse.AllColumns.Add(this.olvColHouseKey);
            this.olvHouse.AllColumns.Add(this.olvColHouseValue);
            this.olvHouse.CellEditActivation = BrightIdeasSoftware.ObjectListView.CellEditActivateMode.SingleClickAlways;
            this.olvHouse.CellEditUseWholeCell = false;
            this.olvHouse.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColHouseKey,
            this.olvColHouseValue});
            this.olvHouse.Cursor = System.Windows.Forms.Cursors.Default;
            this.olvHouse.FullRowSelect = true;
            this.olvHouse.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.olvHouse.HideSelection = false;
            this.olvHouse.Location = new System.Drawing.Point(249, 24);
            this.olvHouse.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.olvHouse.MultiSelect = false;
            this.olvHouse.Name = "olvHouse";
            this.olvHouse.ShowGroups = false;
            this.olvHouse.Size = new System.Drawing.Size(511, 222);
            this.olvHouse.TabIndex = 4;
            this.olvHouse.UseCompatibleStateImageBehavior = false;
            this.olvHouse.View = System.Windows.Forms.View.Details;
            this.olvHouse.CellEditFinished += new BrightIdeasSoftware.CellEditEventHandler(this.olvHouse_CellEditFinished);
            this.olvHouse.CellEditFinishing += new BrightIdeasSoftware.CellEditEventHandler(this.olvHouse_CellEditFinishing);
            this.olvHouse.CellEditStarting += new BrightIdeasSoftware.CellEditEventHandler(this.olvHouse_CellEditStarting);
            // 
            // olvColHouseKey
            // 
            this.olvColHouseKey.AspectName = "Value.ShowName";
            this.olvColHouseKey.IsEditable = false;
            this.olvColHouseKey.Sortable = false;
            this.olvColHouseKey.Text = "LGColvColHouseKey";
            this.olvColHouseKey.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColHouseKey.Width = 168;
            // 
            // olvColHouseValue
            // 
            this.olvColHouseValue.AspectName = "Value.Value";
            this.olvColHouseValue.Sortable = false;
            this.olvColHouseValue.Text = "LGColvColHouseValue";
            this.olvColHouseValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColHouseValue.Width = 177;
            // 
            // LGCgpbHouseAllies
            // 
            this.LGCgpbHouseAllies.Controls.Add(this.btnGoAllie);
            this.LGCgpbHouseAllies.Controls.Add(this.btnGoEnemy);
            this.LGCgpbHouseAllies.Controls.Add(this.lbxHouseEnemy);
            this.LGCgpbHouseAllies.Controls.Add(this.label24);
            this.LGCgpbHouseAllies.Controls.Add(this.label23);
            this.LGCgpbHouseAllies.Controls.Add(this.lbxHouseAllie);
            this.LGCgpbHouseAllies.Controls.Add(this.txbHouseAllies);
            this.LGCgpbHouseAllies.Location = new System.Drawing.Point(249, 242);
            this.LGCgpbHouseAllies.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.LGCgpbHouseAllies.Name = "LGCgpbHouseAllies";
            this.LGCgpbHouseAllies.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.LGCgpbHouseAllies.Size = new System.Drawing.Size(516, 224);
            this.LGCgpbHouseAllies.TabIndex = 3;
            this.LGCgpbHouseAllies.TabStop = false;
            // 
            // btnGoAllie
            // 
            this.btnGoAllie.Location = new System.Drawing.Point(209, 129);
            this.btnGoAllie.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnGoAllie.Name = "btnGoAllie";
            this.btnGoAllie.Size = new System.Drawing.Size(81, 29);
            this.btnGoAllie.TabIndex = 6;
            this.btnGoAllie.Text = "<-";
            this.btnGoAllie.UseVisualStyleBackColor = true;
            this.btnGoAllie.Click += new System.EventHandler(this.btnGoAllie_Click);
            // 
            // btnGoEnemy
            // 
            this.btnGoEnemy.Location = new System.Drawing.Point(209, 92);
            this.btnGoEnemy.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnGoEnemy.Name = "btnGoEnemy";
            this.btnGoEnemy.Size = new System.Drawing.Size(81, 29);
            this.btnGoEnemy.TabIndex = 5;
            this.btnGoEnemy.Text = "->";
            this.btnGoEnemy.UseVisualStyleBackColor = true;
            this.btnGoEnemy.Click += new System.EventHandler(this.btnGoEnemy_Click);
            // 
            // lbxHouseEnemy
            // 
            this.lbxHouseEnemy.FormattingEnabled = true;
            this.lbxHouseEnemy.HorizontalScrollbar = true;
            this.lbxHouseEnemy.ItemHeight = 15;
            this.lbxHouseEnemy.Location = new System.Drawing.Point(299, 40);
            this.lbxHouseEnemy.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lbxHouseEnemy.Name = "lbxHouseEnemy";
            this.lbxHouseEnemy.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbxHouseEnemy.Size = new System.Drawing.Size(208, 139);
            this.lbxHouseEnemy.TabIndex = 4;
            this.lbxHouseEnemy.Enter += new System.EventHandler(this.lbxHouseEnemy_Enter);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Dock = System.Windows.Forms.DockStyle.Right;
            this.label24.Location = new System.Drawing.Point(417, 22);
            this.label24.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(95, 15);
            this.label24.TabIndex = 3;
            this.label24.Text = "LGClblEnemy";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Dock = System.Windows.Forms.DockStyle.Left;
            this.label23.Location = new System.Drawing.Point(4, 22);
            this.label23.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(95, 15);
            this.label23.TabIndex = 2;
            this.label23.Text = "LGClblAllie";
            // 
            // lbxHouseAllie
            // 
            this.lbxHouseAllie.FormattingEnabled = true;
            this.lbxHouseAllie.HorizontalScrollbar = true;
            this.lbxHouseAllie.ItemHeight = 15;
            this.lbxHouseAllie.Location = new System.Drawing.Point(8, 40);
            this.lbxHouseAllie.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lbxHouseAllie.Name = "lbxHouseAllie";
            this.lbxHouseAllie.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbxHouseAllie.Size = new System.Drawing.Size(192, 139);
            this.lbxHouseAllie.TabIndex = 1;
            this.lbxHouseAllie.Enter += new System.EventHandler(this.lbxHouseAllie_Enter);
            // 
            // txbHouseAllies
            // 
            this.txbHouseAllies.Location = new System.Drawing.Point(8, 190);
            this.txbHouseAllies.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txbHouseAllies.Name = "txbHouseAllies";
            this.txbHouseAllies.Size = new System.Drawing.Size(499, 25);
            this.txbHouseAllies.TabIndex = 0;
            this.txbHouseAllies.Validating += new System.ComponentModel.CancelEventHandler(this.txbHouseAllies_Validating);
            this.txbHouseAllies.Validated += new System.EventHandler(this.txbHouseAllies_Validated);
            // 
            // btnDelHouse
            // 
            this.btnDelHouse.Location = new System.Drawing.Point(132, 438);
            this.btnDelHouse.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnDelHouse.Name = "btnDelHouse";
            this.btnDelHouse.Size = new System.Drawing.Size(109, 29);
            this.btnDelHouse.TabIndex = 2;
            this.btnDelHouse.Text = "LGCbtnDelHouse";
            this.btnDelHouse.UseVisualStyleBackColor = true;
            this.btnDelHouse.Click += new System.EventHandler(this.btnDelHouse_Click);
            // 
            // btnNewHouse
            // 
            this.btnNewHouse.Location = new System.Drawing.Point(12, 438);
            this.btnNewHouse.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnNewHouse.Name = "btnNewHouse";
            this.btnNewHouse.Size = new System.Drawing.Size(109, 29);
            this.btnNewHouse.TabIndex = 1;
            this.btnNewHouse.Text = "LGCbtnNewHouse";
            this.btnNewHouse.UseVisualStyleBackColor = true;
            this.btnNewHouse.Click += new System.EventHandler(this.btnNewHouse_Click);
            // 
            // lbxHouses
            // 
            this.lbxHouses.FormattingEnabled = true;
            this.lbxHouses.HorizontalScrollbar = true;
            this.lbxHouses.ItemHeight = 15;
            this.lbxHouses.Location = new System.Drawing.Point(12, 25);
            this.lbxHouses.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lbxHouses.Name = "lbxHouses";
            this.lbxHouses.Size = new System.Drawing.Size(228, 409);
            this.lbxHouses.TabIndex = 0;
            this.lbxHouses.SelectedIndexChanged += new System.EventHandler(this.lbxHouses_SelectedIndexChanged);
            // 
            // gpbLocalVar
            // 
            this.gpbLocalVar.Controls.Add(this.txbLocalName);
            this.gpbLocalVar.Controls.Add(this.label35);
            this.gpbLocalVar.Controls.Add(this.btnNewLocalVar);
            this.gpbLocalVar.Controls.Add(this.chklbxLocalVar);
            this.gpbLocalVar.Location = new System.Drawing.Point(8, 489);
            this.gpbLocalVar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gpbLocalVar.Name = "gpbLocalVar";
            this.gpbLocalVar.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gpbLocalVar.Size = new System.Drawing.Size(773, 210);
            this.gpbLocalVar.TabIndex = 1;
            this.gpbLocalVar.TabStop = false;
            this.gpbLocalVar.Text = "LGCgpbLocalVar";
            // 
            // txbLocalName
            // 
            this.txbLocalName.Location = new System.Drawing.Point(339, 178);
            this.txbLocalName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txbLocalName.Name = "txbLocalName";
            this.txbLocalName.Size = new System.Drawing.Size(425, 25);
            this.txbLocalName.TabIndex = 4;
            this.txbLocalName.TextChanged += new System.EventHandler(this.txbLocalName_TextChanged);
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(180, 184);
            this.label35.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(151, 15);
            this.label35.TabIndex = 3;
            this.label35.Text = "LGClblLocalVarName";
            // 
            // btnNewLocalVar
            // 
            this.btnNewLocalVar.Location = new System.Drawing.Point(8, 178);
            this.btnNewLocalVar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnNewLocalVar.Name = "btnNewLocalVar";
            this.btnNewLocalVar.Size = new System.Drawing.Size(164, 29);
            this.btnNewLocalVar.TabIndex = 1;
            this.btnNewLocalVar.Text = "LGCbtnNewLocalVar";
            this.btnNewLocalVar.UseVisualStyleBackColor = true;
            this.btnNewLocalVar.Click += new System.EventHandler(this.btnNewLocalVar_Click);
            // 
            // chklbxLocalVar
            // 
            this.chklbxLocalVar.FormattingEnabled = true;
            this.chklbxLocalVar.Location = new System.Drawing.Point(8, 25);
            this.chklbxLocalVar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chklbxLocalVar.Name = "chklbxLocalVar";
            this.chklbxLocalVar.Size = new System.Drawing.Size(756, 144);
            this.chklbxLocalVar.TabIndex = 0;
            this.chklbxLocalVar.SelectedIndexChanged += new System.EventHandler(this.chklbxLocalVar_SelectedIndexChanged);
            this.chklbxLocalVar.Leave += new System.EventHandler(this.chklbxLocalVar_Leave);
            // 
            // pnlSearch
            // 
            this.pnlSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlSearch.Location = new System.Drawing.Point(1001, 66);
            this.pnlSearch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Size = new System.Drawing.Size(353, 710);
            this.pnlSearch.TabIndex = 1;
            // 
            // pnlTriggerTag
            // 
            this.pnlTriggerTag.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTriggerTag.Location = new System.Drawing.Point(0, 0);
            this.pnlTriggerTag.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnlTriggerTag.Name = "pnlTriggerTag";
            this.pnlTriggerTag.Size = new System.Drawing.Size(717, 236);
            this.pnlTriggerTag.TabIndex = 7;
            // 
            // pnlEvent
            // 
            this.pnlEvent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlEvent.Location = new System.Drawing.Point(0, 0);
            this.pnlEvent.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnlEvent.Name = "pnlEvent";
            this.pnlEvent.Size = new System.Drawing.Size(374, 462);
            this.pnlEvent.TabIndex = 8;
            // 
            // pnlAction
            // 
            this.pnlAction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlAction.Location = new System.Drawing.Point(0, 0);
            this.pnlAction.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnlAction.Name = "pnlAction";
            this.pnlAction.Size = new System.Drawing.Size(339, 462);
            this.pnlAction.TabIndex = 9;
            // 
            // pnlTaskforce
            // 
            this.pnlTaskforce.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTaskforce.Location = new System.Drawing.Point(0, 0);
            this.pnlTaskforce.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnlTaskforce.Name = "pnlTaskforce";
            this.pnlTaskforce.Size = new System.Drawing.Size(213, 684);
            this.pnlTaskforce.TabIndex = 2;
            // 
            // pnlScript
            // 
            this.pnlScript.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlScript.Location = new System.Drawing.Point(0, 0);
            this.pnlScript.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnlScript.Name = "pnlScript";
            this.pnlScript.Size = new System.Drawing.Size(305, 684);
            this.pnlScript.TabIndex = 2;
            // 
            // splitTeamAI
            // 
            this.splitTeamAI.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitTeamAI.Location = new System.Drawing.Point(4, 4);
            this.splitTeamAI.Name = "splitTeamAI";
            // 
            // splitTeamAI.Panel1
            // 
            this.splitTeamAI.Panel1.Controls.Add(this.gpbTeamTeam);
            // 
            // splitTeamAI.Panel2
            // 
            this.splitTeamAI.Panel2.Controls.Add(this.gpbTeamAI);
            this.splitTeamAI.Size = new System.Drawing.Size(963, 702);
            this.splitTeamAI.SplitterDistance = 488;
            this.splitTeamAI.TabIndex = 4;
            // 
            // splitTeam
            // 
            this.splitTeam.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitTeam.Location = new System.Drawing.Point(3, 20);
            this.splitTeam.Name = "splitTeam";
            // 
            // splitTeam.Panel1
            // 
            this.splitTeam.Panel1.Controls.Add(this.lbxTeamList);
            this.splitTeam.Panel1.Controls.Add(this.label28);
            // 
            // splitTeam.Panel2
            // 
            this.splitTeam.Panel2.Controls.Add(this.olvTeamConfig);
            this.splitTeam.Size = new System.Drawing.Size(482, 680);
            this.splitTeam.SplitterDistance = 160;
            this.splitTeam.TabIndex = 3;
            // 
            // splitAI
            // 
            this.splitAI.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitAI.Location = new System.Drawing.Point(3, 20);
            this.splitAI.Name = "splitAI";
            // 
            // splitAI.Panel1
            // 
            this.splitAI.Panel1.Controls.Add(this.lbxAIList);
            this.splitAI.Panel1.Controls.Add(this.label22);
            // 
            // splitAI.Panel2
            // 
            this.splitAI.Panel2.Controls.Add(this.olvAIConfig);
            this.splitAI.Size = new System.Drawing.Size(465, 680);
            this.splitAI.SplitterDistance = 155;
            this.splitAI.TabIndex = 4;
            // 
            // LogicEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1379, 791);
            this.Controls.Add(this.pnlSearch);
            this.Controls.Add(this.tbcMain);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "LogicEditor";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LGCTitle";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LogicEditor_FormClosing);
            this.ResizeBegin += new System.EventHandler(this.LogicEditor_ResizeBegin);
            this.ResizeEnd += new System.EventHandler(this.LogicEditor_ResizeEnd);
            this.tbcMain.ResumeLayout(false);
            this.tbpTriggers.ResumeLayout(false);
            this.splitTriggerMain.Panel1.ResumeLayout(false);
            this.splitTriggerMain.Panel1.PerformLayout();
            this.splitTriggerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitTriggerMain)).EndInit();
            this.splitTriggerMain.ResumeLayout(false);
            this.cmsTriggerList.ResumeLayout(false);
            this.splitTagEA.Panel1.ResumeLayout(false);
            this.splitTagEA.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitTagEA)).EndInit();
            this.splitTagEA.ResumeLayout(false);
            this.splitEA.Panel1.ResumeLayout(false);
            this.splitEA.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitEA)).EndInit();
            this.splitEA.ResumeLayout(false);
            this.tbpTaskScriptPage.ResumeLayout(false);
            this.splitTaskScript.Panel1.ResumeLayout(false);
            this.splitTaskScript.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitTaskScript)).EndInit();
            this.splitTaskScript.ResumeLayout(false);
            this.gpbTeamTask.ResumeLayout(false);
            this.splitTaskforce.Panel1.ResumeLayout(false);
            this.splitTaskforce.Panel1.PerformLayout();
            this.splitTaskforce.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitTaskforce)).EndInit();
            this.splitTaskforce.ResumeLayout(false);
            this.gpbTeamScript.ResumeLayout(false);
            this.splitScript.Panel1.ResumeLayout(false);
            this.splitScript.Panel1.PerformLayout();
            this.splitScript.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitScript)).EndInit();
            this.splitScript.ResumeLayout(false);
            this.tbpTeams.ResumeLayout(false);
            this.gpbTeamAI.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.olvAIConfig)).EndInit();
            this.gpbTeamTeam.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.olvTeamConfig)).EndInit();
            this.tbpMiscs.ResumeLayout(false);
            this.gpbMap.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.olvBasic)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.gpbHouses.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.olvHouse)).EndInit();
            this.LGCgpbHouseAllies.ResumeLayout(false);
            this.LGCgpbHouseAllies.PerformLayout();
            this.gpbLocalVar.ResumeLayout(false);
            this.gpbLocalVar.PerformLayout();
            this.splitTeamAI.Panel1.ResumeLayout(false);
            this.splitTeamAI.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitTeamAI)).EndInit();
            this.splitTeamAI.ResumeLayout(false);
            this.splitTeam.Panel1.ResumeLayout(false);
            this.splitTeam.Panel1.PerformLayout();
            this.splitTeam.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitTeam)).EndInit();
            this.splitTeam.ResumeLayout(false);
            this.splitAI.Panel1.ResumeLayout(false);
            this.splitAI.Panel1.PerformLayout();
            this.splitAI.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitAI)).EndInit();
            this.splitAI.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tbcMain;
        private System.Windows.Forms.TabPage tbpTriggers;
        private System.Windows.Forms.TabPage tbpTeams;
        private System.Windows.Forms.TabPage tbpMiscs;
        private System.Windows.Forms.ListBox lbxTriggerList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ContextMenuStrip cmsTriggerList;
        private System.Windows.Forms.ToolStripMenuItem tsmiTrgLstAscending;
        private System.Windows.Forms.ToolStripMenuItem tsmiTrgLstDecending;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmiTrgLstID;
        private System.Windows.Forms.ToolStripMenuItem tsmiTrgLstName;
        private System.Windows.Forms.ToolTip ttTrg;
        private System.Windows.Forms.ToolStripMenuItem tsmiTrgLstIDName;
        private System.Windows.Forms.GroupBox gpbTeamTeam;
        private BrightIdeasSoftware.ObjectListView olvTeamConfig;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Button btnCopyTeam;
        private System.Windows.Forms.Button btnDelTeam;
        private System.Windows.Forms.Button btnNewTeam;
        private System.Windows.Forms.GroupBox gpbLocalVar;
        private System.Windows.Forms.TextBox txbLocalName;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Button btnNewLocalVar;
        private System.Windows.Forms.CheckedListBox chklbxLocalVar;
        private System.Windows.Forms.ListBox lbxTeamList;
        private System.Windows.Forms.TabPage tbpTaskScriptPage;
        private System.Windows.Forms.GroupBox gpbTeamScript;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.ListBox lbxScriptList;
        private System.Windows.Forms.GroupBox gpbTeamAI;
        private BrightIdeasSoftware.ObjectListView olvAIConfig;
        private System.Windows.Forms.ListBox lbxAIList;
        private System.Windows.Forms.Button btnCopyAI;
        private System.Windows.Forms.Button btnNewAI;
        private System.Windows.Forms.Button btnDelAI;
        private System.Windows.Forms.Label label22;
        private BrightIdeasSoftware.OLVColumn olvColTeamKey;
        private BrightIdeasSoftware.OLVColumn olvColTeamValue;
        private System.Windows.Forms.GroupBox gpbHouses;
        private System.Windows.Forms.ListBox lbxHouses;
        private BrightIdeasSoftware.ObjectListView olvHouse;
        private BrightIdeasSoftware.OLVColumn olvColHouseKey;
        private BrightIdeasSoftware.OLVColumn olvColHouseValue;
        private System.Windows.Forms.GroupBox LGCgpbHouseAllies;
        private System.Windows.Forms.Button btnDelHouse;
        private System.Windows.Forms.Button btnNewHouse;
        private System.Windows.Forms.Button btnGoAllie;
        private System.Windows.Forms.Button btnGoEnemy;
        private System.Windows.Forms.ListBox lbxHouseEnemy;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.ListBox lbxHouseAllie;
        private System.Windows.Forms.TextBox txbHouseAllies;
        private System.Windows.Forms.GroupBox gpbMap;
        private BrightIdeasSoftware.OLVColumn olvColAIKey;
        private BrightIdeasSoftware.OLVColumn olvColAIValue;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.CheckedListBox chklbxSpecialFlag;
        private System.Windows.Forms.CheckedListBox chklbxBasic;
        private BrightIdeasSoftware.ObjectListView olvBasic;
        private PanelTrgTag pnlTriggerTag;
        private PanelEvent pnlEvent;
        private PanelEvent pnlAction;
        private PanelGlobalSearch pnlSearch;
        private System.Windows.Forms.GroupBox gpbTeamTask;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.ListBox lbxTaskList;
        private PanelTaskforce pnlTaskforce;
        private PanelScript pnlScript;
        private System.Windows.Forms.SplitContainer splitTriggerMain;
        private System.Windows.Forms.SplitContainer splitTagEA;
        private System.Windows.Forms.SplitContainer splitEA;
        private System.Windows.Forms.SplitContainer splitTaskScript;
        private System.Windows.Forms.SplitContainer splitTaskforce;
        private System.Windows.Forms.SplitContainer splitScript;
        private System.Windows.Forms.SplitContainer splitTeamAI;
        private System.Windows.Forms.SplitContainer splitTeam;
        private System.Windows.Forms.SplitContainer splitAI;
    }
}