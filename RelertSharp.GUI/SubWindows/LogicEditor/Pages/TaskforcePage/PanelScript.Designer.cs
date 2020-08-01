namespace RelertSharp.GUI.SubWindows.LogicEditor
{
    partial class PanelScript
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
            this.gpbTeamScriptCur = new System.Windows.Forms.GroupBox();
            this.splitMain = new System.Windows.Forms.SplitContainer();
            this.lbxScriptList = new System.Windows.Forms.ListBox();
            this.label32 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tlpScriptBtn = new System.Windows.Forms.TableLayoutPanel();
            this.btnAddScriptMem = new System.Windows.Forms.Button();
            this.btnDelScriptMem = new System.Windows.Forms.Button();
            this.btnCopyScriptMem = new System.Windows.Forms.Button();
            this.txbScriptName = new System.Windows.Forms.TextBox();
            this.txbScriptID = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlParam = new RelertSharp.GUI.SubWindows.LogicEditor.PanelScriptParam();
            this.gpbTeamScriptCur.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            this.splitMain.Panel1.SuspendLayout();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tlpScriptBtn.SuspendLayout();
            this.SuspendLayout();
            // 
            // gpbTeamScriptCur
            // 
            this.gpbTeamScriptCur.Controls.Add(this.splitMain);
            this.gpbTeamScriptCur.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gpbTeamScriptCur.Location = new System.Drawing.Point(0, 0);
            this.gpbTeamScriptCur.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gpbTeamScriptCur.Name = "gpbTeamScriptCur";
            this.gpbTeamScriptCur.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gpbTeamScriptCur.Size = new System.Drawing.Size(480, 684);
            this.gpbTeamScriptCur.TabIndex = 3;
            this.gpbTeamScriptCur.TabStop = false;
            this.gpbTeamScriptCur.Text = "LGCgpbTeamScriptCur";
            // 
            // splitMain
            // 
            this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitMain.Location = new System.Drawing.Point(3, 20);
            this.splitMain.Name = "splitMain";
            this.splitMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitMain.Panel1
            // 
            this.splitMain.Panel1.Controls.Add(this.lbxScriptList);
            this.splitMain.Panel1.Controls.Add(this.label32);
            this.splitMain.Panel1.Controls.Add(this.tableLayoutPanel1);
            // 
            // splitMain.Panel2
            // 
            this.splitMain.Panel2.Controls.Add(this.pnlParam);
            this.splitMain.Size = new System.Drawing.Size(474, 662);
            this.splitMain.SplitterDistance = 320;
            this.splitMain.TabIndex = 27;
            // 
            // lbxScriptList
            // 
            this.lbxScriptList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbxScriptList.FormattingEnabled = true;
            this.lbxScriptList.ItemHeight = 15;
            this.lbxScriptList.Location = new System.Drawing.Point(0, 100);
            this.lbxScriptList.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lbxScriptList.Name = "lbxScriptList";
            this.lbxScriptList.Size = new System.Drawing.Size(474, 220);
            this.lbxScriptList.TabIndex = 0;
            this.lbxScriptList.SelectedValueChanged += new System.EventHandler(this.lbxScriptList_SelectedValueChanged);
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Dock = System.Windows.Forms.DockStyle.Top;
            this.label32.Location = new System.Drawing.Point(0, 85);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(183, 15);
            this.label32.TabIndex = 1;
            this.label32.Text = "LGClblScriptMemberList";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 115F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tlpScriptBtn, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txbScriptName, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.txbScriptID, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label29, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(474, 85);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // tlpScriptBtn
            // 
            this.tlpScriptBtn.ColumnCount = 3;
            this.tableLayoutPanel1.SetColumnSpan(this.tlpScriptBtn, 2);
            this.tlpScriptBtn.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpScriptBtn.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpScriptBtn.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpScriptBtn.Controls.Add(this.btnAddScriptMem, 0, 0);
            this.tlpScriptBtn.Controls.Add(this.btnDelScriptMem, 1, 0);
            this.tlpScriptBtn.Controls.Add(this.btnCopyScriptMem, 2, 0);
            this.tlpScriptBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpScriptBtn.Location = new System.Drawing.Point(3, 3);
            this.tlpScriptBtn.Name = "tlpScriptBtn";
            this.tlpScriptBtn.RowCount = 1;
            this.tlpScriptBtn.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpScriptBtn.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tlpScriptBtn.Size = new System.Drawing.Size(468, 29);
            this.tlpScriptBtn.TabIndex = 28;
            // 
            // btnAddScriptMem
            // 
            this.btnAddScriptMem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAddScriptMem.Location = new System.Drawing.Point(3, 2);
            this.btnAddScriptMem.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnAddScriptMem.Name = "btnAddScriptMem";
            this.btnAddScriptMem.Size = new System.Drawing.Size(150, 25);
            this.btnAddScriptMem.TabIndex = 2;
            this.btnAddScriptMem.Text = "LGCbtnNewScript";
            this.btnAddScriptMem.UseVisualStyleBackColor = true;
            this.btnAddScriptMem.Click += new System.EventHandler(this.btnAddScriptMem_Click);
            // 
            // btnDelScriptMem
            // 
            this.btnDelScriptMem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDelScriptMem.Location = new System.Drawing.Point(159, 2);
            this.btnDelScriptMem.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDelScriptMem.Name = "btnDelScriptMem";
            this.btnDelScriptMem.Size = new System.Drawing.Size(150, 25);
            this.btnDelScriptMem.TabIndex = 13;
            this.btnDelScriptMem.Text = "LGCbtnDelScript";
            this.btnDelScriptMem.UseVisualStyleBackColor = true;
            this.btnDelScriptMem.Click += new System.EventHandler(this.btnDelScriptMem_Click);
            // 
            // btnCopyScriptMem
            // 
            this.btnCopyScriptMem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCopyScriptMem.Location = new System.Drawing.Point(315, 2);
            this.btnCopyScriptMem.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCopyScriptMem.Name = "btnCopyScriptMem";
            this.btnCopyScriptMem.Size = new System.Drawing.Size(150, 25);
            this.btnCopyScriptMem.TabIndex = 12;
            this.btnCopyScriptMem.Text = "LGCbtnCopyScript";
            this.btnCopyScriptMem.UseVisualStyleBackColor = true;
            this.btnCopyScriptMem.Click += new System.EventHandler(this.btnCopyScriptMem_Click);
            // 
            // txbScriptName
            // 
            this.txbScriptName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txbScriptName.Location = new System.Drawing.Point(118, 57);
            this.txbScriptName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txbScriptName.Name = "txbScriptName";
            this.txbScriptName.Size = new System.Drawing.Size(353, 25);
            this.txbScriptName.TabIndex = 25;
            this.txbScriptName.Validated += new System.EventHandler(this.txbScriptName_Validated);
            // 
            // txbScriptID
            // 
            this.txbScriptID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txbScriptID.Enabled = false;
            this.txbScriptID.Location = new System.Drawing.Point(3, 57);
            this.txbScriptID.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txbScriptID.Name = "txbScriptID";
            this.txbScriptID.Size = new System.Drawing.Size(109, 25);
            this.txbScriptID.TabIndex = 24;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label29.Location = new System.Drawing.Point(118, 40);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(353, 15);
            this.label29.TabIndex = 23;
            this.label29.Text = "LGClblScriptName";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label2.Location = new System.Drawing.Point(3, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 20);
            this.label2.TabIndex = 22;
            this.label2.Text = "LGClblScriptID";
            // 
            // pnlParam
            // 
            this.pnlParam.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlParam.Location = new System.Drawing.Point(0, 0);
            this.pnlParam.Name = "pnlParam";
            this.pnlParam.Size = new System.Drawing.Size(474, 338);
            this.pnlParam.TabIndex = 26;
            // 
            // PanelScript
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gpbTeamScriptCur);
            this.Name = "PanelScript";
            this.Size = new System.Drawing.Size(480, 684);
            this.gpbTeamScriptCur.ResumeLayout(false);
            this.splitMain.Panel1.ResumeLayout(false);
            this.splitMain.Panel1.PerformLayout();
            this.splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
            this.splitMain.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tlpScriptBtn.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gpbTeamScriptCur;
        private System.Windows.Forms.TextBox txbScriptID;
        private System.Windows.Forms.TextBox txbScriptName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Button btnDelScriptMem;
        private System.Windows.Forms.Button btnCopyScriptMem;
        private System.Windows.Forms.Button btnAddScriptMem;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.ListBox lbxScriptList;
        private PanelScriptParam pnlParam;
        private System.Windows.Forms.SplitContainer splitMain;
        private System.Windows.Forms.TableLayoutPanel tlpScriptBtn;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}
