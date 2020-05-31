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
            this.pnlEvent = new RelertSharp.GUI.SubWindows.LogicEditor.PanelEvent();
            this.pnlTriggerTag = new RelertSharp.GUI.SubWindows.LogicEditor.PanelTrgTag();
            this.cmsCopyAction = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiCopyActionAdv = new System.Windows.Forms.ToolStripMenuItem();
            this.lbxTriggerList = new System.Windows.Forms.ListBox();
            this.cmsTriggerList = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiTrgLstAscending = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTrgLstDecending = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiTrgLstID = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTrgLstName = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTrgLstIDName = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.tbpTaskScriptPage = new System.Windows.Forms.TabPage();
            this.gpbTeamScript = new System.Windows.Forms.GroupBox();
            this.btnDelScript = new System.Windows.Forms.Button();
            this.btnNewScript = new System.Windows.Forms.Button();
            this.btnCopyScript = new System.Windows.Forms.Button();
            this.gpbTeamScriptCur = new System.Windows.Forms.GroupBox();
            this.label34 = new System.Windows.Forms.Label();
            this.ckbInsert = new System.Windows.Forms.CheckBox();
            this.rtxbScriptDesc = new System.Windows.Forms.RichTextBox();
            this.cbbScriptCurPara = new System.Windows.Forms.ComboBox();
            this.btnDelScriptMem = new System.Windows.Forms.Button();
            this.btnCopyScriptMem = new System.Windows.Forms.Button();
            this.txbScriptName = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.cbbScriptCurType = new System.Windows.Forms.ComboBox();
            this.label31 = new System.Windows.Forms.Label();
            this.btnAddScriptMem = new System.Windows.Forms.Button();
            this.label32 = new System.Windows.Forms.Label();
            this.lbxScriptMemList = new System.Windows.Forms.ListBox();
            this.label33 = new System.Windows.Forms.Label();
            this.lbxScriptList = new System.Windows.Forms.ListBox();
            this.gpbTeamTask = new System.Windows.Forms.GroupBox();
            this.label36 = new System.Windows.Forms.Label();
            this.gpbTaskDetial = new System.Windows.Forms.GroupBox();
            this.cbbTaskType = new System.Windows.Forms.ComboBox();
            this.mtxbTaskNum = new System.Windows.Forms.MaskedTextBox();
            this.label42 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.lvTaskforceUnits = new System.Windows.Forms.ListView();
            this.imglstPcx = new System.Windows.Forms.ImageList(this.components);
            this.mtxbTaskGroup = new System.Windows.Forms.MaskedTextBox();
            this.txbTaskName = new System.Windows.Forms.TextBox();
            this.txbTaskID = new System.Windows.Forms.TextBox();
            this.label38 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.btnCopyTaskforce = new System.Windows.Forms.Button();
            this.btnDelTaskforce = new System.Windows.Forms.Button();
            this.btnCopyTfUnit = new System.Windows.Forms.Button();
            this.btnDelTfUnit = new System.Windows.Forms.Button();
            this.btnNewTfUnit = new System.Windows.Forms.Button();
            this.btnNewTaskforce = new System.Windows.Forms.Button();
            this.lbxTaskList = new System.Windows.Forms.ListBox();
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
            this.gpbSearch = new System.Windows.Forms.GroupBox();
            this.ckbSuper = new System.Windows.Forms.CheckBox();
            this.ckbAnim = new System.Windows.Forms.CheckBox();
            this.ckbTheme = new System.Windows.Forms.CheckBox();
            this.ckbEva = new System.Windows.Forms.CheckBox();
            this.ckbSound = new System.Windows.Forms.CheckBox();
            this.ckbTechno = new System.Windows.Forms.CheckBox();
            this.ckbCsf = new System.Windows.Forms.CheckBox();
            this.ckbScript = new System.Windows.Forms.CheckBox();
            this.ckbHouse = new System.Windows.Forms.CheckBox();
            this.ckbGlobal = new System.Windows.Forms.CheckBox();
            this.ckbAiTrigger = new System.Windows.Forms.CheckBox();
            this.ckbTaskForce = new System.Windows.Forms.CheckBox();
            this.ckbTeam = new System.Windows.Forms.CheckBox();
            this.ckbLocal = new System.Windows.Forms.CheckBox();
            this.ckbTag = new System.Windows.Forms.CheckBox();
            this.ckbTrigger = new System.Windows.Forms.CheckBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txbSearchName = new System.Windows.Forms.TextBox();
            this.lblSearchResult = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.lvSearchResult = new System.Windows.Forms.ListView();
            this.hdRegID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.hdType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.hdValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.hdExtraValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.rtxbSearchInspector = new System.Windows.Forms.RichTextBox();
            this.pnlAction = new RelertSharp.GUI.SubWindows.LogicEditor.PanelEvent();
            this.tbcMain.SuspendLayout();
            this.tbpTriggers.SuspendLayout();
            this.cmsCopyAction.SuspendLayout();
            this.cmsTriggerList.SuspendLayout();
            this.tbpTaskScriptPage.SuspendLayout();
            this.gpbTeamScript.SuspendLayout();
            this.gpbTeamScriptCur.SuspendLayout();
            this.gpbTeamTask.SuspendLayout();
            this.gpbTaskDetial.SuspendLayout();
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
            this.gpbSearch.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbcMain
            // 
            this.tbcMain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tbcMain.Controls.Add(this.tbpTriggers);
            this.tbcMain.Controls.Add(this.tbpTaskScriptPage);
            this.tbcMain.Controls.Add(this.tbpTeams);
            this.tbcMain.Controls.Add(this.tbpMiscs);
            this.tbcMain.Location = new System.Drawing.Point(16, 41);
            this.tbcMain.Margin = new System.Windows.Forms.Padding(4);
            this.tbcMain.Name = "tbcMain";
            this.tbcMain.SelectedIndex = 0;
            this.tbcMain.Size = new System.Drawing.Size(1533, 739);
            this.tbcMain.TabIndex = 0;
            // 
            // tbpTriggers
            // 
            this.tbpTriggers.BackColor = System.Drawing.Color.Transparent;
            this.tbpTriggers.Controls.Add(this.pnlAction);
            this.tbpTriggers.Controls.Add(this.pnlEvent);
            this.tbpTriggers.Controls.Add(this.pnlTriggerTag);
            this.tbpTriggers.Controls.Add(this.lbxTriggerList);
            this.tbpTriggers.Controls.Add(this.label1);
            this.tbpTriggers.Location = new System.Drawing.Point(4, 25);
            this.tbpTriggers.Margin = new System.Windows.Forms.Padding(4);
            this.tbpTriggers.Name = "tbpTriggers";
            this.tbpTriggers.Padding = new System.Windows.Forms.Padding(4);
            this.tbpTriggers.Size = new System.Drawing.Size(1525, 710);
            this.tbpTriggers.TabIndex = 0;
            this.tbpTriggers.Text = "LGCtbpTrgPage";
            // 
            // pnlEvent
            // 
            this.pnlEvent.Location = new System.Drawing.Point(288, 250);
            this.pnlEvent.Name = "pnlEvent";
            this.pnlEvent.Size = new System.Drawing.Size(604, 448);
            this.pnlEvent.TabIndex = 8;
            // 
            // pnlTriggerTag
            // 
            this.pnlTriggerTag.Location = new System.Drawing.Point(288, 14);
            this.pnlTriggerTag.Name = "pnlTriggerTag";
            this.pnlTriggerTag.Size = new System.Drawing.Size(1223, 229);
            this.pnlTriggerTag.TabIndex = 7;
            // 
            // cmsCopyAction
            // 
            this.cmsCopyAction.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmsCopyAction.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiCopyActionAdv});
            this.cmsCopyAction.Name = "cmsCopyAction";
            this.cmsCopyAction.Size = new System.Drawing.Size(206, 28);
            // 
            // tsmiCopyActionAdv
            // 
            this.tsmiCopyActionAdv.Name = "tsmiCopyActionAdv";
            this.tsmiCopyActionAdv.Size = new System.Drawing.Size(205, 24);
            this.tsmiCopyActionAdv.Text = "LGCtsmiCopyAdv";
            this.tsmiCopyActionAdv.Click += new System.EventHandler(this.tsmiCopyActionAdv_Click);
            // 
            // lbxTriggerList
            // 
            this.lbxTriggerList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lbxTriggerList.ContextMenuStrip = this.cmsTriggerList;
            this.lbxTriggerList.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbxTriggerList.FormattingEnabled = true;
            this.lbxTriggerList.HorizontalScrollbar = true;
            this.lbxTriggerList.ItemHeight = 15;
            this.lbxTriggerList.Location = new System.Drawing.Point(11, 32);
            this.lbxTriggerList.Margin = new System.Windows.Forms.Padding(4);
            this.lbxTriggerList.Name = "lbxTriggerList";
            this.lbxTriggerList.Size = new System.Drawing.Size(269, 649);
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
            this.label1.Location = new System.Drawing.Point(8, 14);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "LGClblTrgList";
            // 
            // tbpTaskScriptPage
            // 
            this.tbpTaskScriptPage.BackColor = System.Drawing.Color.Transparent;
            this.tbpTaskScriptPage.Controls.Add(this.gpbTeamScript);
            this.tbpTaskScriptPage.Controls.Add(this.gpbTeamTask);
            this.tbpTaskScriptPage.Location = new System.Drawing.Point(4, 25);
            this.tbpTaskScriptPage.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbpTaskScriptPage.Name = "tbpTaskScriptPage";
            this.tbpTaskScriptPage.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbpTaskScriptPage.Size = new System.Drawing.Size(1525, 710);
            this.tbpTaskScriptPage.TabIndex = 3;
            this.tbpTaskScriptPage.Text = "LGCtbpTaskScriptPage";
            // 
            // gpbTeamScript
            // 
            this.gpbTeamScript.Controls.Add(this.btnDelScript);
            this.gpbTeamScript.Controls.Add(this.btnNewScript);
            this.gpbTeamScript.Controls.Add(this.btnCopyScript);
            this.gpbTeamScript.Controls.Add(this.gpbTeamScriptCur);
            this.gpbTeamScript.Controls.Add(this.label33);
            this.gpbTeamScript.Controls.Add(this.lbxScriptList);
            this.gpbTeamScript.Location = new System.Drawing.Point(773, 16);
            this.gpbTeamScript.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gpbTeamScript.Name = "gpbTeamScript";
            this.gpbTeamScript.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gpbTeamScript.Size = new System.Drawing.Size(747, 688);
            this.gpbTeamScript.TabIndex = 4;
            this.gpbTeamScript.TabStop = false;
            this.gpbTeamScript.Text = "LGCgpbTeamScript";
            // 
            // btnDelScript
            // 
            this.btnDelScript.Location = new System.Drawing.Point(128, 331);
            this.btnDelScript.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDelScript.Name = "btnDelScript";
            this.btnDelScript.Size = new System.Drawing.Size(117, 29);
            this.btnDelScript.TabIndex = 8;
            this.btnDelScript.Text = "LGCbtnDelScript";
            this.btnDelScript.UseVisualStyleBackColor = true;
            // 
            // btnNewScript
            // 
            this.btnNewScript.Location = new System.Drawing.Point(8, 331);
            this.btnNewScript.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnNewScript.Name = "btnNewScript";
            this.btnNewScript.Size = new System.Drawing.Size(115, 29);
            this.btnNewScript.TabIndex = 7;
            this.btnNewScript.Text = "LGCbtnNewScript";
            this.btnNewScript.UseVisualStyleBackColor = true;
            // 
            // btnCopyScript
            // 
            this.btnCopyScript.Location = new System.Drawing.Point(251, 331);
            this.btnCopyScript.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCopyScript.Name = "btnCopyScript";
            this.btnCopyScript.Size = new System.Drawing.Size(115, 29);
            this.btnCopyScript.TabIndex = 6;
            this.btnCopyScript.Text = "LGCbtnCopyScript";
            this.btnCopyScript.UseVisualStyleBackColor = true;
            // 
            // gpbTeamScriptCur
            // 
            this.gpbTeamScriptCur.Controls.Add(this.label34);
            this.gpbTeamScriptCur.Controls.Add(this.ckbInsert);
            this.gpbTeamScriptCur.Controls.Add(this.rtxbScriptDesc);
            this.gpbTeamScriptCur.Controls.Add(this.cbbScriptCurPara);
            this.gpbTeamScriptCur.Controls.Add(this.btnDelScriptMem);
            this.gpbTeamScriptCur.Controls.Add(this.btnCopyScriptMem);
            this.gpbTeamScriptCur.Controls.Add(this.txbScriptName);
            this.gpbTeamScriptCur.Controls.Add(this.label29);
            this.gpbTeamScriptCur.Controls.Add(this.label30);
            this.gpbTeamScriptCur.Controls.Add(this.cbbScriptCurType);
            this.gpbTeamScriptCur.Controls.Add(this.label31);
            this.gpbTeamScriptCur.Controls.Add(this.btnAddScriptMem);
            this.gpbTeamScriptCur.Controls.Add(this.label32);
            this.gpbTeamScriptCur.Controls.Add(this.lbxScriptMemList);
            this.gpbTeamScriptCur.Location = new System.Drawing.Point(371, 18);
            this.gpbTeamScriptCur.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gpbTeamScriptCur.Name = "gpbTeamScriptCur";
            this.gpbTeamScriptCur.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gpbTeamScriptCur.Size = new System.Drawing.Size(371, 338);
            this.gpbTeamScriptCur.TabIndex = 2;
            this.gpbTeamScriptCur.TabStop = false;
            this.gpbTeamScriptCur.Text = "LGCgpbTeamScriptCur";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(5, 162);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(135, 15);
            this.label34.TabIndex = 17;
            this.label34.Text = "LGClblScriptDesc";
            // 
            // ckbInsert
            // 
            this.ckbInsert.AutoSize = true;
            this.ckbInsert.Location = new System.Drawing.Point(237, 49);
            this.ckbInsert.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ckbInsert.Name = "ckbInsert";
            this.ckbInsert.Size = new System.Drawing.Size(125, 19);
            this.ckbInsert.TabIndex = 16;
            this.ckbInsert.Text = "LGCckbInsert";
            this.ckbInsert.UseVisualStyleBackColor = true;
            // 
            // rtxbScriptDesc
            // 
            this.rtxbScriptDesc.Location = new System.Drawing.Point(5, 180);
            this.rtxbScriptDesc.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rtxbScriptDesc.Name = "rtxbScriptDesc";
            this.rtxbScriptDesc.ReadOnly = true;
            this.rtxbScriptDesc.Size = new System.Drawing.Size(359, 56);
            this.rtxbScriptDesc.TabIndex = 15;
            this.rtxbScriptDesc.Text = "";
            // 
            // cbbScriptCurPara
            // 
            this.cbbScriptCurPara.FormattingEnabled = true;
            this.cbbScriptCurPara.Location = new System.Drawing.Point(177, 272);
            this.cbbScriptCurPara.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbbScriptCurPara.Name = "cbbScriptCurPara";
            this.cbbScriptCurPara.Size = new System.Drawing.Size(187, 23);
            this.cbbScriptCurPara.TabIndex = 14;
            // 
            // btnDelScriptMem
            // 
            this.btnDelScriptMem.Location = new System.Drawing.Point(124, 302);
            this.btnDelScriptMem.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDelScriptMem.Name = "btnDelScriptMem";
            this.btnDelScriptMem.Size = new System.Drawing.Size(115, 30);
            this.btnDelScriptMem.TabIndex = 13;
            this.btnDelScriptMem.Text = "LGCbtnDelScriptMem";
            this.btnDelScriptMem.UseVisualStyleBackColor = true;
            // 
            // btnCopyScriptMem
            // 
            this.btnCopyScriptMem.Location = new System.Drawing.Point(244, 302);
            this.btnCopyScriptMem.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCopyScriptMem.Name = "btnCopyScriptMem";
            this.btnCopyScriptMem.Size = new System.Drawing.Size(121, 30);
            this.btnCopyScriptMem.TabIndex = 12;
            this.btnCopyScriptMem.Text = "LGCbtnCopyScriptMem";
            this.btnCopyScriptMem.UseVisualStyleBackColor = true;
            // 
            // txbScriptName
            // 
            this.txbScriptName.Location = new System.Drawing.Point(177, 18);
            this.txbScriptName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txbScriptName.Name = "txbScriptName";
            this.txbScriptName.Size = new System.Drawing.Size(187, 25);
            this.txbScriptName.TabIndex = 9;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(5, 21);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(135, 15);
            this.label29.TabIndex = 8;
            this.label29.Text = "LGClblScriptName";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(5, 276);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(159, 15);
            this.label30.TabIndex = 7;
            this.label30.Text = "LGClblScriptCurPara";
            // 
            // cbbScriptCurType
            // 
            this.cbbScriptCurType.FormattingEnabled = true;
            this.cbbScriptCurType.Location = new System.Drawing.Point(177, 242);
            this.cbbScriptCurType.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbbScriptCurType.Name = "cbbScriptCurType";
            this.cbbScriptCurType.Size = new System.Drawing.Size(187, 23);
            this.cbbScriptCurType.TabIndex = 5;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(5, 246);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(159, 15);
            this.label31.TabIndex = 4;
            this.label31.Text = "LGClblScriptCurType";
            // 
            // btnAddScriptMem
            // 
            this.btnAddScriptMem.Location = new System.Drawing.Point(5, 302);
            this.btnAddScriptMem.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnAddScriptMem.Name = "btnAddScriptMem";
            this.btnAddScriptMem.Size = new System.Drawing.Size(113, 30);
            this.btnAddScriptMem.TabIndex = 2;
            this.btnAddScriptMem.Text = "LGCbtnAddScriptMem";
            this.btnAddScriptMem.UseVisualStyleBackColor = true;
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(5, 49);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(183, 15);
            this.label32.TabIndex = 1;
            this.label32.Text = "LGClblScriptMemberList";
            // 
            // lbxScriptMemList
            // 
            this.lbxScriptMemList.FormattingEnabled = true;
            this.lbxScriptMemList.ItemHeight = 15;
            this.lbxScriptMemList.Location = new System.Drawing.Point(5, 74);
            this.lbxScriptMemList.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lbxScriptMemList.Name = "lbxScriptMemList";
            this.lbxScriptMemList.Size = new System.Drawing.Size(359, 79);
            this.lbxScriptMemList.TabIndex = 0;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(5, 19);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(135, 15);
            this.label33.TabIndex = 1;
            this.label33.Text = "LGClblScriptList";
            // 
            // lbxScriptList
            // 
            this.lbxScriptList.FormattingEnabled = true;
            this.lbxScriptList.HorizontalScrollbar = true;
            this.lbxScriptList.ItemHeight = 15;
            this.lbxScriptList.Location = new System.Drawing.Point(5, 32);
            this.lbxScriptList.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lbxScriptList.Name = "lbxScriptList";
            this.lbxScriptList.Size = new System.Drawing.Size(359, 289);
            this.lbxScriptList.TabIndex = 0;
            // 
            // gpbTeamTask
            // 
            this.gpbTeamTask.Controls.Add(this.label36);
            this.gpbTeamTask.Controls.Add(this.gpbTaskDetial);
            this.gpbTeamTask.Controls.Add(this.lbxTaskList);
            this.gpbTeamTask.Location = new System.Drawing.Point(5, 5);
            this.gpbTeamTask.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gpbTeamTask.Name = "gpbTeamTask";
            this.gpbTeamTask.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gpbTeamTask.Size = new System.Drawing.Size(757, 709);
            this.gpbTeamTask.TabIndex = 3;
            this.gpbTeamTask.TabStop = false;
            this.gpbTeamTask.Text = "LGCgpbTeamTask";
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(5, 22);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(119, 15);
            this.label36.TabIndex = 1;
            this.label36.Text = "LGClblTaskList";
            // 
            // gpbTaskDetial
            // 
            this.gpbTaskDetial.Controls.Add(this.cbbTaskType);
            this.gpbTaskDetial.Controls.Add(this.mtxbTaskNum);
            this.gpbTaskDetial.Controls.Add(this.label42);
            this.gpbTaskDetial.Controls.Add(this.label41);
            this.gpbTaskDetial.Controls.Add(this.lvTaskforceUnits);
            this.gpbTaskDetial.Controls.Add(this.mtxbTaskGroup);
            this.gpbTaskDetial.Controls.Add(this.txbTaskName);
            this.gpbTaskDetial.Controls.Add(this.txbTaskID);
            this.gpbTaskDetial.Controls.Add(this.label38);
            this.gpbTaskDetial.Controls.Add(this.label39);
            this.gpbTaskDetial.Controls.Add(this.label40);
            this.gpbTaskDetial.Controls.Add(this.label37);
            this.gpbTaskDetial.Controls.Add(this.btnCopyTaskforce);
            this.gpbTaskDetial.Controls.Add(this.btnDelTaskforce);
            this.gpbTaskDetial.Controls.Add(this.btnCopyTfUnit);
            this.gpbTaskDetial.Controls.Add(this.btnDelTfUnit);
            this.gpbTaskDetial.Controls.Add(this.btnNewTfUnit);
            this.gpbTaskDetial.Controls.Add(this.btnNewTaskforce);
            this.gpbTaskDetial.Location = new System.Drawing.Point(284, 22);
            this.gpbTaskDetial.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gpbTaskDetial.Name = "gpbTaskDetial";
            this.gpbTaskDetial.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gpbTaskDetial.Size = new System.Drawing.Size(453, 682);
            this.gpbTaskDetial.TabIndex = 2;
            this.gpbTaskDetial.TabStop = false;
            this.gpbTaskDetial.Text = "LGCgpbTeamTask";
            // 
            // cbbTaskType
            // 
            this.cbbTaskType.FormattingEnabled = true;
            this.cbbTaskType.Location = new System.Drawing.Point(5, 602);
            this.cbbTaskType.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbbTaskType.Name = "cbbTaskType";
            this.cbbTaskType.Size = new System.Drawing.Size(364, 23);
            this.cbbTaskType.TabIndex = 13;
            this.cbbTaskType.SelectedIndexChanged += new System.EventHandler(this.cbbTaskCurType_SelectedIndexChanged);
            // 
            // mtxbTaskNum
            // 
            this.mtxbTaskNum.Location = new System.Drawing.Point(376, 602);
            this.mtxbTaskNum.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.mtxbTaskNum.Mask = "000";
            this.mtxbTaskNum.Name = "mtxbTaskNum";
            this.mtxbTaskNum.PromptChar = ' ';
            this.mtxbTaskNum.Size = new System.Drawing.Size(60, 25);
            this.mtxbTaskNum.TabIndex = 12;
            this.mtxbTaskNum.ValidatingType = typeof(int);
            this.mtxbTaskNum.Validated += new System.EventHandler(this.mtxbTaskNum_Validated);
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(373, 585);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(135, 15);
            this.label42.TabIndex = 10;
            this.label42.Text = "LGClblTaskCurNum";
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(0, 585);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(143, 15);
            this.label41.TabIndex = 11;
            this.label41.Text = "LGClblTaskCurType";
            // 
            // lvTaskforceUnits
            // 
            this.lvTaskforceUnits.HideSelection = false;
            this.lvTaskforceUnits.LargeImageList = this.imglstPcx;
            this.lvTaskforceUnits.Location = new System.Drawing.Point(5, 122);
            this.lvTaskforceUnits.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lvTaskforceUnits.MultiSelect = false;
            this.lvTaskforceUnits.Name = "lvTaskforceUnits";
            this.lvTaskforceUnits.Size = new System.Drawing.Size(431, 460);
            this.lvTaskforceUnits.TabIndex = 3;
            this.lvTaskforceUnits.UseCompatibleStateImageBehavior = false;
            this.lvTaskforceUnits.SelectedIndexChanged += new System.EventHandler(this.lvTaskforceUnits_SelectedIndexChanged);
            // 
            // imglstPcx
            // 
            this.imglstPcx.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imglstPcx.ImageSize = new System.Drawing.Size(60, 48);
            this.imglstPcx.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // mtxbTaskGroup
            // 
            this.mtxbTaskGroup.Location = new System.Drawing.Point(376, 74);
            this.mtxbTaskGroup.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.mtxbTaskGroup.Name = "mtxbTaskGroup";
            this.mtxbTaskGroup.Size = new System.Drawing.Size(60, 25);
            this.mtxbTaskGroup.TabIndex = 3;
            this.mtxbTaskGroup.Validated += new System.EventHandler(this.mtxbTaskGroup_Validated);
            // 
            // txbTaskName
            // 
            this.txbTaskName.Location = new System.Drawing.Point(127, 74);
            this.txbTaskName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txbTaskName.Name = "txbTaskName";
            this.txbTaskName.Size = new System.Drawing.Size(243, 25);
            this.txbTaskName.TabIndex = 2;
            this.txbTaskName.Validated += new System.EventHandler(this.txbTaskName_Validated);
            // 
            // txbTaskID
            // 
            this.txbTaskID.Location = new System.Drawing.Point(5, 74);
            this.txbTaskID.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txbTaskID.Name = "txbTaskID";
            this.txbTaskID.ReadOnly = true;
            this.txbTaskID.Size = new System.Drawing.Size(115, 25);
            this.txbTaskID.TabIndex = 2;
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(124, 56);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(119, 15);
            this.label38.TabIndex = 1;
            this.label38.Text = "LGClblTaskName";
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Location = new System.Drawing.Point(373, 56);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(127, 15);
            this.label39.TabIndex = 1;
            this.label39.Text = "LGClblTaskGroup";
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(3, 104);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(167, 15);
            this.label40.TabIndex = 1;
            this.label40.Text = "LGClblTaskMemberList";
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(3, 56);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(103, 15);
            this.label37.TabIndex = 1;
            this.label37.Text = "LGClblTaskID";
            // 
            // btnCopyTaskforce
            // 
            this.btnCopyTaskforce.Location = new System.Drawing.Point(304, 24);
            this.btnCopyTaskforce.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCopyTaskforce.Name = "btnCopyTaskforce";
            this.btnCopyTaskforce.Size = new System.Drawing.Size(132, 29);
            this.btnCopyTaskforce.TabIndex = 0;
            this.btnCopyTaskforce.Text = "LGCbtnCopyTask";
            this.btnCopyTaskforce.UseVisualStyleBackColor = true;
            this.btnCopyTaskforce.Click += new System.EventHandler(this.btnCopyTask_Click);
            // 
            // btnDelTaskforce
            // 
            this.btnDelTaskforce.Location = new System.Drawing.Point(159, 24);
            this.btnDelTaskforce.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDelTaskforce.Name = "btnDelTaskforce";
            this.btnDelTaskforce.Size = new System.Drawing.Size(132, 29);
            this.btnDelTaskforce.TabIndex = 0;
            this.btnDelTaskforce.Text = "LGCbtnDelTask";
            this.btnDelTaskforce.UseVisualStyleBackColor = true;
            this.btnDelTaskforce.Click += new System.EventHandler(this.btnDelTask_Click);
            // 
            // btnCopyTfUnit
            // 
            this.btnCopyTfUnit.Location = new System.Drawing.Point(304, 638);
            this.btnCopyTfUnit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCopyTfUnit.Name = "btnCopyTfUnit";
            this.btnCopyTfUnit.Size = new System.Drawing.Size(132, 29);
            this.btnCopyTfUnit.TabIndex = 0;
            this.btnCopyTfUnit.Text = "LGCbtnCopyTaskMem";
            this.btnCopyTfUnit.UseVisualStyleBackColor = true;
            this.btnCopyTfUnit.Click += new System.EventHandler(this.btnCopyTaskMem_Click);
            // 
            // btnDelTfUnit
            // 
            this.btnDelTfUnit.Location = new System.Drawing.Point(159, 638);
            this.btnDelTfUnit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDelTfUnit.Name = "btnDelTfUnit";
            this.btnDelTfUnit.Size = new System.Drawing.Size(132, 29);
            this.btnDelTfUnit.TabIndex = 0;
            this.btnDelTfUnit.Text = "LGCbtnDelTaskMem";
            this.btnDelTfUnit.UseVisualStyleBackColor = true;
            this.btnDelTfUnit.Click += new System.EventHandler(this.btnDelTaskMem_Click);
            // 
            // btnNewTfUnit
            // 
            this.btnNewTfUnit.Location = new System.Drawing.Point(5, 638);
            this.btnNewTfUnit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnNewTfUnit.Name = "btnNewTfUnit";
            this.btnNewTfUnit.Size = new System.Drawing.Size(137, 29);
            this.btnNewTfUnit.TabIndex = 0;
            this.btnNewTfUnit.Text = "LGCbtnAddTaskMem";
            this.btnNewTfUnit.UseVisualStyleBackColor = true;
            this.btnNewTfUnit.Click += new System.EventHandler(this.btnAddTaskMem_Click);
            // 
            // btnNewTaskforce
            // 
            this.btnNewTaskforce.Location = new System.Drawing.Point(5, 24);
            this.btnNewTaskforce.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnNewTaskforce.Name = "btnNewTaskforce";
            this.btnNewTaskforce.Size = new System.Drawing.Size(137, 29);
            this.btnNewTaskforce.TabIndex = 0;
            this.btnNewTaskforce.Text = "LGCbtnNewTask";
            this.btnNewTaskforce.UseVisualStyleBackColor = true;
            this.btnNewTaskforce.Click += new System.EventHandler(this.btnNewTask_Click);
            // 
            // lbxTaskList
            // 
            this.lbxTaskList.FormattingEnabled = true;
            this.lbxTaskList.HorizontalScrollbar = true;
            this.lbxTaskList.ItemHeight = 15;
            this.lbxTaskList.Location = new System.Drawing.Point(9, 40);
            this.lbxTaskList.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lbxTaskList.Name = "lbxTaskList";
            this.lbxTaskList.Size = new System.Drawing.Size(269, 649);
            this.lbxTaskList.Sorted = true;
            this.lbxTaskList.TabIndex = 0;
            this.lbxTaskList.SelectedIndexChanged += new System.EventHandler(this.lbxTaskList_SelectedIndexChanged);
            // 
            // tbpTeams
            // 
            this.tbpTeams.BackColor = System.Drawing.Color.Transparent;
            this.tbpTeams.Controls.Add(this.gpbTeamAI);
            this.tbpTeams.Controls.Add(this.gpbTeamTeam);
            this.tbpTeams.Location = new System.Drawing.Point(4, 25);
            this.tbpTeams.Margin = new System.Windows.Forms.Padding(4);
            this.tbpTeams.Name = "tbpTeams";
            this.tbpTeams.Padding = new System.Windows.Forms.Padding(4);
            this.tbpTeams.Size = new System.Drawing.Size(1525, 710);
            this.tbpTeams.TabIndex = 1;
            this.tbpTeams.Text = "LGCtbpTeamPage";
            // 
            // gpbTeamAI
            // 
            this.gpbTeamAI.Controls.Add(this.olvAIConfig);
            this.gpbTeamAI.Controls.Add(this.lbxAIList);
            this.gpbTeamAI.Controls.Add(this.btnCopyAI);
            this.gpbTeamAI.Controls.Add(this.btnNewAI);
            this.gpbTeamAI.Controls.Add(this.btnDelAI);
            this.gpbTeamAI.Controls.Add(this.label22);
            this.gpbTeamAI.Location = new System.Drawing.Point(761, 10);
            this.gpbTeamAI.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gpbTeamAI.Name = "gpbTeamAI";
            this.gpbTeamAI.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gpbTeamAI.Size = new System.Drawing.Size(755, 696);
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
            this.olvAIConfig.FullRowSelect = true;
            this.olvAIConfig.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.olvAIConfig.HideSelection = false;
            this.olvAIConfig.Location = new System.Drawing.Point(337, 18);
            this.olvAIConfig.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.olvAIConfig.MultiSelect = false;
            this.olvAIConfig.Name = "olvAIConfig";
            this.olvAIConfig.ShowGroups = false;
            this.olvAIConfig.Size = new System.Drawing.Size(411, 673);
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
            this.lbxAIList.FormattingEnabled = true;
            this.lbxAIList.HorizontalScrollbar = true;
            this.lbxAIList.ItemHeight = 15;
            this.lbxAIList.Location = new System.Drawing.Point(1, 36);
            this.lbxAIList.Margin = new System.Windows.Forms.Padding(4);
            this.lbxAIList.Name = "lbxAIList";
            this.lbxAIList.Size = new System.Drawing.Size(328, 619);
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
            this.label22.Location = new System.Drawing.Point(5, 18);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(103, 15);
            this.label22.TabIndex = 1;
            this.label22.Text = "LGClblAIList";
            // 
            // gpbTeamTeam
            // 
            this.gpbTeamTeam.Controls.Add(this.olvTeamConfig);
            this.gpbTeamTeam.Controls.Add(this.lbxTeamList);
            this.gpbTeamTeam.Controls.Add(this.btnCopyTeam);
            this.gpbTeamTeam.Controls.Add(this.btnNewTeam);
            this.gpbTeamTeam.Controls.Add(this.btnDelTeam);
            this.gpbTeamTeam.Controls.Add(this.label28);
            this.gpbTeamTeam.Location = new System.Drawing.Point(7, 6);
            this.gpbTeamTeam.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gpbTeamTeam.Name = "gpbTeamTeam";
            this.gpbTeamTeam.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gpbTeamTeam.Size = new System.Drawing.Size(749, 696);
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
            this.olvTeamConfig.FullRowSelect = true;
            this.olvTeamConfig.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.olvTeamConfig.HideSelection = false;
            this.olvTeamConfig.Location = new System.Drawing.Point(337, 18);
            this.olvTeamConfig.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.olvTeamConfig.MultiSelect = false;
            this.olvTeamConfig.Name = "olvTeamConfig";
            this.olvTeamConfig.ShowGroups = false;
            this.olvTeamConfig.Size = new System.Drawing.Size(405, 673);
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
            this.lbxTeamList.FormattingEnabled = true;
            this.lbxTeamList.HorizontalScrollbar = true;
            this.lbxTeamList.ItemHeight = 15;
            this.lbxTeamList.Location = new System.Drawing.Point(1, 36);
            this.lbxTeamList.Margin = new System.Windows.Forms.Padding(4);
            this.lbxTeamList.Name = "lbxTeamList";
            this.lbxTeamList.Size = new System.Drawing.Size(328, 619);
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
            this.label28.Location = new System.Drawing.Point(5, 18);
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
            this.tbpMiscs.Margin = new System.Windows.Forms.Padding(4);
            this.tbpMiscs.Name = "tbpMiscs";
            this.tbpMiscs.Padding = new System.Windows.Forms.Padding(4);
            this.tbpMiscs.Size = new System.Drawing.Size(1525, 710);
            this.tbpMiscs.TabIndex = 2;
            this.tbpMiscs.Text = "LGCtbpMiscPage";
            // 
            // gpbMap
            // 
            this.gpbMap.Controls.Add(this.splitContainer1);
            this.gpbMap.Location = new System.Drawing.Point(789, 9);
            this.gpbMap.Margin = new System.Windows.Forms.Padding(4);
            this.gpbMap.Name = "gpbMap";
            this.gpbMap.Padding = new System.Windows.Forms.Padding(4);
            this.gpbMap.Size = new System.Drawing.Size(725, 690);
            this.gpbMap.TabIndex = 4;
            this.gpbMap.TabStop = false;
            this.gpbMap.Text = "LGCgpbMap";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(4, 22);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4);
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
            this.splitContainer1.SplitterDistance = 325;
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
            this.olvBasic.Margin = new System.Windows.Forms.Padding(4);
            this.olvBasic.Name = "olvBasic";
            this.olvBasic.Size = new System.Drawing.Size(717, 325);
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
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(717, 334);
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
            this.chklbxSpecialFlag.Margin = new System.Windows.Forms.Padding(4);
            this.chklbxSpecialFlag.Name = "chklbxSpecialFlag";
            this.chklbxSpecialFlag.Size = new System.Drawing.Size(349, 304);
            this.chklbxSpecialFlag.TabIndex = 5;
            // 
            // chklbxBasic
            // 
            this.chklbxBasic.FormattingEnabled = true;
            this.chklbxBasic.Location = new System.Drawing.Point(4, 22);
            this.chklbxBasic.Margin = new System.Windows.Forms.Padding(4);
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
            this.gpbHouses.Margin = new System.Windows.Forms.Padding(4);
            this.gpbHouses.Name = "gpbHouses";
            this.gpbHouses.Padding = new System.Windows.Forms.Padding(4);
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
            this.olvHouse.Margin = new System.Windows.Forms.Padding(4);
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
            this.LGCgpbHouseAllies.Margin = new System.Windows.Forms.Padding(4);
            this.LGCgpbHouseAllies.Name = "LGCgpbHouseAllies";
            this.LGCgpbHouseAllies.Padding = new System.Windows.Forms.Padding(4);
            this.LGCgpbHouseAllies.Size = new System.Drawing.Size(516, 224);
            this.LGCgpbHouseAllies.TabIndex = 3;
            this.LGCgpbHouseAllies.TabStop = false;
            // 
            // btnGoAllie
            // 
            this.btnGoAllie.Location = new System.Drawing.Point(209, 129);
            this.btnGoAllie.Margin = new System.Windows.Forms.Padding(4);
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
            this.btnGoEnemy.Margin = new System.Windows.Forms.Padding(4);
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
            this.lbxHouseEnemy.Margin = new System.Windows.Forms.Padding(4);
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
            this.lbxHouseAllie.Margin = new System.Windows.Forms.Padding(4);
            this.lbxHouseAllie.Name = "lbxHouseAllie";
            this.lbxHouseAllie.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbxHouseAllie.Size = new System.Drawing.Size(192, 139);
            this.lbxHouseAllie.TabIndex = 1;
            this.lbxHouseAllie.Enter += new System.EventHandler(this.lbxHouseAllie_Enter);
            // 
            // txbHouseAllies
            // 
            this.txbHouseAllies.Location = new System.Drawing.Point(8, 190);
            this.txbHouseAllies.Margin = new System.Windows.Forms.Padding(4);
            this.txbHouseAllies.Name = "txbHouseAllies";
            this.txbHouseAllies.Size = new System.Drawing.Size(499, 25);
            this.txbHouseAllies.TabIndex = 0;
            this.txbHouseAllies.Validating += new System.ComponentModel.CancelEventHandler(this.txbHouseAllies_Validating);
            this.txbHouseAllies.Validated += new System.EventHandler(this.txbHouseAllies_Validated);
            // 
            // btnDelHouse
            // 
            this.btnDelHouse.Location = new System.Drawing.Point(132, 438);
            this.btnDelHouse.Margin = new System.Windows.Forms.Padding(4);
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
            this.btnNewHouse.Margin = new System.Windows.Forms.Padding(4);
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
            this.lbxHouses.Margin = new System.Windows.Forms.Padding(4);
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
            this.gpbLocalVar.Margin = new System.Windows.Forms.Padding(4);
            this.gpbLocalVar.Name = "gpbLocalVar";
            this.gpbLocalVar.Padding = new System.Windows.Forms.Padding(4);
            this.gpbLocalVar.Size = new System.Drawing.Size(773, 210);
            this.gpbLocalVar.TabIndex = 1;
            this.gpbLocalVar.TabStop = false;
            this.gpbLocalVar.Text = "LGCgpbLocalVar";
            // 
            // txbLocalName
            // 
            this.txbLocalName.Location = new System.Drawing.Point(339, 178);
            this.txbLocalName.Margin = new System.Windows.Forms.Padding(4);
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
            this.btnNewLocalVar.Margin = new System.Windows.Forms.Padding(4);
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
            this.chklbxLocalVar.Margin = new System.Windows.Forms.Padding(4);
            this.chklbxLocalVar.Name = "chklbxLocalVar";
            this.chklbxLocalVar.Size = new System.Drawing.Size(756, 144);
            this.chklbxLocalVar.TabIndex = 0;
            this.chklbxLocalVar.SelectedIndexChanged += new System.EventHandler(this.chklbxLocalVar_SelectedIndexChanged);
            this.chklbxLocalVar.Leave += new System.EventHandler(this.chklbxLocalVar_Leave);
            // 
            // gpbSearch
            // 
            this.gpbSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.gpbSearch.Controls.Add(this.ckbSuper);
            this.gpbSearch.Controls.Add(this.ckbAnim);
            this.gpbSearch.Controls.Add(this.ckbTheme);
            this.gpbSearch.Controls.Add(this.ckbEva);
            this.gpbSearch.Controls.Add(this.ckbSound);
            this.gpbSearch.Controls.Add(this.ckbTechno);
            this.gpbSearch.Controls.Add(this.ckbCsf);
            this.gpbSearch.Controls.Add(this.ckbScript);
            this.gpbSearch.Controls.Add(this.ckbHouse);
            this.gpbSearch.Controls.Add(this.ckbGlobal);
            this.gpbSearch.Controls.Add(this.ckbAiTrigger);
            this.gpbSearch.Controls.Add(this.ckbTaskForce);
            this.gpbSearch.Controls.Add(this.ckbTeam);
            this.gpbSearch.Controls.Add(this.ckbLocal);
            this.gpbSearch.Controls.Add(this.ckbTag);
            this.gpbSearch.Controls.Add(this.ckbTrigger);
            this.gpbSearch.Controls.Add(this.btnSearch);
            this.gpbSearch.Controls.Add(this.txbSearchName);
            this.gpbSearch.Controls.Add(this.lblSearchResult);
            this.gpbSearch.Controls.Add(this.label16);
            this.gpbSearch.Controls.Add(this.lvSearchResult);
            this.gpbSearch.Controls.Add(this.rtxbSearchInspector);
            this.gpbSearch.Location = new System.Drawing.Point(1557, 41);
            this.gpbSearch.Margin = new System.Windows.Forms.Padding(4);
            this.gpbSearch.Name = "gpbSearch";
            this.gpbSearch.Padding = new System.Windows.Forms.Padding(4);
            this.gpbSearch.Size = new System.Drawing.Size(353, 739);
            this.gpbSearch.TabIndex = 4;
            this.gpbSearch.TabStop = false;
            this.gpbSearch.Text = "LGCgpbSearch";
            // 
            // ckbSuper
            // 
            this.ckbSuper.AutoSize = true;
            this.ckbSuper.Location = new System.Drawing.Point(192, 224);
            this.ckbSuper.Margin = new System.Windows.Forms.Padding(4);
            this.ckbSuper.Name = "ckbSuper";
            this.ckbSuper.Size = new System.Drawing.Size(117, 19);
            this.ckbSuper.TabIndex = 7;
            this.ckbSuper.Text = "LGCckbSuper";
            this.ckbSuper.UseVisualStyleBackColor = true;
            // 
            // ckbAnim
            // 
            this.ckbAnim.AutoSize = true;
            this.ckbAnim.Location = new System.Drawing.Point(192, 196);
            this.ckbAnim.Margin = new System.Windows.Forms.Padding(4);
            this.ckbAnim.Name = "ckbAnim";
            this.ckbAnim.Size = new System.Drawing.Size(109, 19);
            this.ckbAnim.TabIndex = 7;
            this.ckbAnim.Text = "LGCckbAnim";
            this.ckbAnim.UseVisualStyleBackColor = true;
            // 
            // ckbTheme
            // 
            this.ckbTheme.AutoSize = true;
            this.ckbTheme.Location = new System.Drawing.Point(192, 169);
            this.ckbTheme.Margin = new System.Windows.Forms.Padding(4);
            this.ckbTheme.Name = "ckbTheme";
            this.ckbTheme.Size = new System.Drawing.Size(101, 19);
            this.ckbTheme.TabIndex = 7;
            this.ckbTheme.Text = "LGCckbMus";
            this.ckbTheme.UseVisualStyleBackColor = true;
            // 
            // ckbEva
            // 
            this.ckbEva.AutoSize = true;
            this.ckbEva.Location = new System.Drawing.Point(192, 141);
            this.ckbEva.Margin = new System.Windows.Forms.Padding(4);
            this.ckbEva.Name = "ckbEva";
            this.ckbEva.Size = new System.Drawing.Size(101, 19);
            this.ckbEva.TabIndex = 7;
            this.ckbEva.Text = "LGCckbEva";
            this.ckbEva.UseVisualStyleBackColor = true;
            // 
            // ckbSound
            // 
            this.ckbSound.AutoSize = true;
            this.ckbSound.Location = new System.Drawing.Point(192, 114);
            this.ckbSound.Margin = new System.Windows.Forms.Padding(4);
            this.ckbSound.Name = "ckbSound";
            this.ckbSound.Size = new System.Drawing.Size(101, 19);
            this.ckbSound.TabIndex = 7;
            this.ckbSound.Text = "LGCckbSnd";
            this.ckbSound.UseVisualStyleBackColor = true;
            // 
            // ckbTechno
            // 
            this.ckbTechno.AutoSize = true;
            this.ckbTechno.Location = new System.Drawing.Point(192, 86);
            this.ckbTechno.Margin = new System.Windows.Forms.Padding(4);
            this.ckbTechno.Name = "ckbTechno";
            this.ckbTechno.Size = new System.Drawing.Size(125, 19);
            this.ckbTechno.TabIndex = 7;
            this.ckbTechno.Text = "LGCckbTechno";
            this.ckbTechno.UseVisualStyleBackColor = true;
            // 
            // ckbCsf
            // 
            this.ckbCsf.AutoSize = true;
            this.ckbCsf.Location = new System.Drawing.Point(192, 59);
            this.ckbCsf.Margin = new System.Windows.Forms.Padding(4);
            this.ckbCsf.Name = "ckbCsf";
            this.ckbCsf.Size = new System.Drawing.Size(101, 19);
            this.ckbCsf.TabIndex = 7;
            this.ckbCsf.Text = "LGCckbCsf";
            this.ckbCsf.UseVisualStyleBackColor = true;
            // 
            // ckbScript
            // 
            this.ckbScript.AutoSize = true;
            this.ckbScript.Location = new System.Drawing.Point(11, 196);
            this.ckbScript.Margin = new System.Windows.Forms.Padding(4);
            this.ckbScript.Name = "ckbScript";
            this.ckbScript.Size = new System.Drawing.Size(109, 19);
            this.ckbScript.TabIndex = 6;
            this.ckbScript.Text = "LGCckbTScp";
            this.ckbScript.UseVisualStyleBackColor = true;
            // 
            // ckbHouse
            // 
            this.ckbHouse.AutoSize = true;
            this.ckbHouse.Location = new System.Drawing.Point(11, 251);
            this.ckbHouse.Margin = new System.Windows.Forms.Padding(4);
            this.ckbHouse.Name = "ckbHouse";
            this.ckbHouse.Size = new System.Drawing.Size(117, 19);
            this.ckbHouse.TabIndex = 6;
            this.ckbHouse.Text = "LGCckbHouse";
            this.ckbHouse.UseVisualStyleBackColor = true;
            // 
            // ckbGlobal
            // 
            this.ckbGlobal.AutoSize = true;
            this.ckbGlobal.Location = new System.Drawing.Point(192, 251);
            this.ckbGlobal.Margin = new System.Windows.Forms.Padding(4);
            this.ckbGlobal.Name = "ckbGlobal";
            this.ckbGlobal.Size = new System.Drawing.Size(125, 19);
            this.ckbGlobal.TabIndex = 6;
            this.ckbGlobal.Text = "LGCckbGlobal";
            this.ckbGlobal.UseVisualStyleBackColor = true;
            // 
            // ckbAiTrigger
            // 
            this.ckbAiTrigger.AutoSize = true;
            this.ckbAiTrigger.Location = new System.Drawing.Point(11, 224);
            this.ckbAiTrigger.Margin = new System.Windows.Forms.Padding(4);
            this.ckbAiTrigger.Name = "ckbAiTrigger";
            this.ckbAiTrigger.Size = new System.Drawing.Size(117, 19);
            this.ckbAiTrigger.TabIndex = 6;
            this.ckbAiTrigger.Text = "LGCckbAiTrg";
            this.ckbAiTrigger.UseVisualStyleBackColor = true;
            // 
            // ckbTaskForce
            // 
            this.ckbTaskForce.AutoSize = true;
            this.ckbTaskForce.Location = new System.Drawing.Point(11, 169);
            this.ckbTaskForce.Margin = new System.Windows.Forms.Padding(4);
            this.ckbTaskForce.Name = "ckbTaskForce";
            this.ckbTaskForce.Size = new System.Drawing.Size(93, 19);
            this.ckbTaskForce.TabIndex = 6;
            this.ckbTaskForce.Text = "LGCckbTF";
            this.ckbTaskForce.UseVisualStyleBackColor = true;
            // 
            // ckbTeam
            // 
            this.ckbTeam.AutoSize = true;
            this.ckbTeam.Location = new System.Drawing.Point(11, 141);
            this.ckbTeam.Margin = new System.Windows.Forms.Padding(4);
            this.ckbTeam.Name = "ckbTeam";
            this.ckbTeam.Size = new System.Drawing.Size(109, 19);
            this.ckbTeam.TabIndex = 6;
            this.ckbTeam.Text = "LGCckbTeam";
            this.ckbTeam.UseVisualStyleBackColor = true;
            // 
            // ckbLocal
            // 
            this.ckbLocal.AutoSize = true;
            this.ckbLocal.Location = new System.Drawing.Point(11, 114);
            this.ckbLocal.Margin = new System.Windows.Forms.Padding(4);
            this.ckbLocal.Name = "ckbLocal";
            this.ckbLocal.Size = new System.Drawing.Size(117, 19);
            this.ckbLocal.TabIndex = 6;
            this.ckbLocal.Text = "LGCckbLocal";
            this.ckbLocal.UseVisualStyleBackColor = true;
            // 
            // ckbTag
            // 
            this.ckbTag.AutoSize = true;
            this.ckbTag.Location = new System.Drawing.Point(11, 86);
            this.ckbTag.Margin = new System.Windows.Forms.Padding(4);
            this.ckbTag.Name = "ckbTag";
            this.ckbTag.Size = new System.Drawing.Size(101, 19);
            this.ckbTag.TabIndex = 6;
            this.ckbTag.Text = "LGCckbTag";
            this.ckbTag.UseVisualStyleBackColor = true;
            // 
            // ckbTrigger
            // 
            this.ckbTrigger.AutoSize = true;
            this.ckbTrigger.Location = new System.Drawing.Point(11, 59);
            this.ckbTrigger.Margin = new System.Windows.Forms.Padding(4);
            this.ckbTrigger.Name = "ckbTrigger";
            this.ckbTrigger.Size = new System.Drawing.Size(109, 19);
            this.ckbTrigger.TabIndex = 6;
            this.ckbTrigger.Text = "LGCckbTrig";
            this.ckbTrigger.UseVisualStyleBackColor = true;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(220, 22);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(111, 26);
            this.btnSearch.TabIndex = 5;
            this.btnSearch.Text = "LGCbtnSearch";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txbSearchName
            // 
            this.txbSearchName.ForeColor = System.Drawing.SystemColors.GrayText;
            this.txbSearchName.Location = new System.Drawing.Point(8, 25);
            this.txbSearchName.Margin = new System.Windows.Forms.Padding(4);
            this.txbSearchName.Name = "txbSearchName";
            this.txbSearchName.Size = new System.Drawing.Size(204, 25);
            this.txbSearchName.TabIndex = 4;
            this.txbSearchName.Text = "LGClblFakeSearch";
            this.txbSearchName.Enter += new System.EventHandler(this.txbSearchName_Enter);
            this.txbSearchName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txbSearchName_KeyDown);
            this.txbSearchName.Leave += new System.EventHandler(this.txbSearchName_Leave);
            // 
            // lblSearchResult
            // 
            this.lblSearchResult.AutoSize = true;
            this.lblSearchResult.Location = new System.Drawing.Point(8, 278);
            this.lblSearchResult.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSearchResult.Name = "lblSearchResult";
            this.lblSearchResult.Size = new System.Drawing.Size(151, 15);
            this.lblSearchResult.TabIndex = 3;
            this.lblSearchResult.Text = "LGClblSearchResult";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(8, 529);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(127, 15);
            this.label16.TabIndex = 2;
            this.label16.Text = "LGClblInspector";
            // 
            // lvSearchResult
            // 
            this.lvSearchResult.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.hdRegID,
            this.hdType,
            this.hdValue,
            this.hdExtraValue});
            this.lvSearchResult.FullRowSelect = true;
            this.lvSearchResult.HideSelection = false;
            this.lvSearchResult.Location = new System.Drawing.Point(8, 296);
            this.lvSearchResult.Margin = new System.Windows.Forms.Padding(4);
            this.lvSearchResult.MultiSelect = false;
            this.lvSearchResult.Name = "lvSearchResult";
            this.lvSearchResult.Size = new System.Drawing.Size(328, 228);
            this.lvSearchResult.TabIndex = 1;
            this.lvSearchResult.UseCompatibleStateImageBehavior = false;
            this.lvSearchResult.View = System.Windows.Forms.View.Details;
            this.lvSearchResult.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvSearchResult_ColumnClick);
            // 
            // hdRegID
            // 
            this.hdRegID.Text = "LGCclhRegID";
            // 
            // hdType
            // 
            this.hdType.Text = "LGCclhType";
            // 
            // hdValue
            // 
            this.hdValue.Text = "LGCclhValue";
            // 
            // hdExtraValue
            // 
            this.hdExtraValue.Text = "LGCclhExVal";
            // 
            // rtxbSearchInspector
            // 
            this.rtxbSearchInspector.Font = new System.Drawing.Font("Verdana", 9F);
            this.rtxbSearchInspector.Location = new System.Drawing.Point(8, 551);
            this.rtxbSearchInspector.Margin = new System.Windows.Forms.Padding(4);
            this.rtxbSearchInspector.Name = "rtxbSearchInspector";
            this.rtxbSearchInspector.ReadOnly = true;
            this.rtxbSearchInspector.Size = new System.Drawing.Size(328, 179);
            this.rtxbSearchInspector.TabIndex = 0;
            this.rtxbSearchInspector.Text = "";
            // 
            // pnlAction
            // 
            this.pnlAction.Location = new System.Drawing.Point(907, 250);
            this.pnlAction.Name = "pnlAction";
            this.pnlAction.Size = new System.Drawing.Size(604, 448);
            this.pnlAction.TabIndex = 9;
            // 
            // LogicEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1920, 791);
            this.Controls.Add(this.gpbSearch);
            this.Controls.Add(this.tbcMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LogicEditor";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LGCTitle";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LogicEditor_FormClosing);
            this.tbcMain.ResumeLayout(false);
            this.tbpTriggers.ResumeLayout(false);
            this.tbpTriggers.PerformLayout();
            this.cmsCopyAction.ResumeLayout(false);
            this.cmsTriggerList.ResumeLayout(false);
            this.tbpTaskScriptPage.ResumeLayout(false);
            this.gpbTeamScript.ResumeLayout(false);
            this.gpbTeamScript.PerformLayout();
            this.gpbTeamScriptCur.ResumeLayout(false);
            this.gpbTeamScriptCur.PerformLayout();
            this.gpbTeamTask.ResumeLayout(false);
            this.gpbTeamTask.PerformLayout();
            this.gpbTaskDetial.ResumeLayout(false);
            this.gpbTaskDetial.PerformLayout();
            this.tbpTeams.ResumeLayout(false);
            this.gpbTeamAI.ResumeLayout(false);
            this.gpbTeamAI.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olvAIConfig)).EndInit();
            this.gpbTeamTeam.ResumeLayout(false);
            this.gpbTeamTeam.PerformLayout();
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
            this.gpbSearch.ResumeLayout(false);
            this.gpbSearch.PerformLayout();
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
        private System.Windows.Forms.ContextMenuStrip cmsCopyAction;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopyActionAdv;
        private System.Windows.Forms.GroupBox gpbSearch;
        private System.Windows.Forms.RichTextBox rtxbSearchInspector;
        private System.Windows.Forms.TextBox txbSearchName;
        private System.Windows.Forms.Label lblSearchResult;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ListView lvSearchResult;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.CheckBox ckbAiTrigger;
        private System.Windows.Forms.CheckBox ckbTaskForce;
        private System.Windows.Forms.CheckBox ckbTeam;
        private System.Windows.Forms.CheckBox ckbLocal;
        private System.Windows.Forms.CheckBox ckbTag;
        private System.Windows.Forms.CheckBox ckbTrigger;
        private System.Windows.Forms.CheckBox ckbAnim;
        private System.Windows.Forms.CheckBox ckbTheme;
        private System.Windows.Forms.CheckBox ckbEva;
        private System.Windows.Forms.CheckBox ckbSound;
        private System.Windows.Forms.CheckBox ckbTechno;
        private System.Windows.Forms.CheckBox ckbCsf;
        private System.Windows.Forms.CheckBox ckbScript;
        private System.Windows.Forms.CheckBox ckbSuper;
        private System.Windows.Forms.ColumnHeader hdRegID;
        private System.Windows.Forms.ColumnHeader hdType;
        private System.Windows.Forms.ColumnHeader hdValue;
        private System.Windows.Forms.ColumnHeader hdExtraValue;
        private System.Windows.Forms.CheckBox ckbGlobal;
        private System.Windows.Forms.CheckBox ckbHouse;
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
        private System.Windows.Forms.GroupBox gpbTaskDetial;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Button btnCopyTaskforce;
        private System.Windows.Forms.Button btnDelTaskforce;
        private System.Windows.Forms.Button btnNewTaskforce;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.ListBox lbxTaskList;
        private System.Windows.Forms.MaskedTextBox mtxbTaskGroup;
        private System.Windows.Forms.TextBox txbTaskName;
        private System.Windows.Forms.TextBox txbTaskID;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.ListView lvTaskforceUnits;
        private System.Windows.Forms.GroupBox gpbTeamTask;
        private System.Windows.Forms.Button btnCopyTfUnit;
        private System.Windows.Forms.Button btnDelTfUnit;
        private System.Windows.Forms.Button btnNewTfUnit;
        private System.Windows.Forms.ImageList imglstPcx;
        private System.Windows.Forms.ComboBox cbbTaskType;
        private System.Windows.Forms.MaskedTextBox mtxbTaskNum;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.GroupBox gpbTeamScript;
        private System.Windows.Forms.Button btnDelScript;
        private System.Windows.Forms.Button btnNewScript;
        private System.Windows.Forms.Button btnCopyScript;
        private System.Windows.Forms.GroupBox gpbTeamScriptCur;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.CheckBox ckbInsert;
        private System.Windows.Forms.RichTextBox rtxbScriptDesc;
        private System.Windows.Forms.ComboBox cbbScriptCurPara;
        private System.Windows.Forms.Button btnDelScriptMem;
        private System.Windows.Forms.Button btnCopyScriptMem;
        private System.Windows.Forms.TextBox txbScriptName;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.ComboBox cbbScriptCurType;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Button btnAddScriptMem;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.ListBox lbxScriptMemList;
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
    }
}