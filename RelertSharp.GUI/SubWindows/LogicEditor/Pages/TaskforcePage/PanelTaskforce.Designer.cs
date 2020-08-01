namespace RelertSharp.GUI.SubWindows.LogicEditor
{
    partial class PanelTaskforce
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
            this.gpbTaskDetial = new System.Windows.Forms.GroupBox();
            this.lvTaskforceUnits = new System.Windows.Forms.ListView();
            this.imglstPcx = new System.Windows.Forms.ImageList(this.components);
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tlpBtnMember = new System.Windows.Forms.TableLayoutPanel();
            this.btnNewTfUnit = new System.Windows.Forms.Button();
            this.btnDelTfUnit = new System.Windows.Forms.Button();
            this.btnCopyTfUnit = new System.Windows.Forms.Button();
            this.label41 = new System.Windows.Forms.Label();
            this.mtxbTaskNum = new System.Windows.Forms.MaskedTextBox();
            this.cbbTaskType = new System.Windows.Forms.ComboBox();
            this.label42 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tlpBtnTask = new System.Windows.Forms.TableLayoutPanel();
            this.btnNewTaskforce = new System.Windows.Forms.Button();
            this.btnDelTaskforce = new System.Windows.Forms.Button();
            this.btnCopyTaskforce = new System.Windows.Forms.Button();
            this.label37 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.mtxbTaskGroup = new System.Windows.Forms.MaskedTextBox();
            this.txbTaskID = new System.Windows.Forms.TextBox();
            this.txbTaskName = new System.Windows.Forms.TextBox();
            this.gpbTaskDetial.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tlpBtnMember.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tlpBtnTask.SuspendLayout();
            this.SuspendLayout();
            // 
            // gpbTaskDetial
            // 
            this.gpbTaskDetial.Controls.Add(this.lvTaskforceUnits);
            this.gpbTaskDetial.Controls.Add(this.tableLayoutPanel2);
            this.gpbTaskDetial.Controls.Add(this.label40);
            this.gpbTaskDetial.Controls.Add(this.tableLayoutPanel1);
            this.gpbTaskDetial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gpbTaskDetial.Location = new System.Drawing.Point(0, 0);
            this.gpbTaskDetial.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gpbTaskDetial.Name = "gpbTaskDetial";
            this.gpbTaskDetial.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gpbTaskDetial.Size = new System.Drawing.Size(537, 508);
            this.gpbTaskDetial.TabIndex = 3;
            this.gpbTaskDetial.TabStop = false;
            this.gpbTaskDetial.Text = "LGCgpbTeamTask";
            // 
            // lvTaskforceUnits
            // 
            this.lvTaskforceUnits.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvTaskforceUnits.HideSelection = false;
            this.lvTaskforceUnits.LargeImageList = this.imglstPcx;
            this.lvTaskforceUnits.Location = new System.Drawing.Point(3, 120);
            this.lvTaskforceUnits.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lvTaskforceUnits.MultiSelect = false;
            this.lvTaskforceUnits.Name = "lvTaskforceUnits";
            this.lvTaskforceUnits.Size = new System.Drawing.Size(531, 301);
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
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel2.Controls.Add(this.tlpBtnMember, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.label41, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.mtxbTaskNum, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.cbbTaskType, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label42, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 421);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(531, 85);
            this.tableLayoutPanel2.TabIndex = 5;
            // 
            // tlpBtnMember
            // 
            this.tlpBtnMember.ColumnCount = 3;
            this.tableLayoutPanel2.SetColumnSpan(this.tlpBtnMember, 2);
            this.tlpBtnMember.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpBtnMember.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpBtnMember.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpBtnMember.Controls.Add(this.btnNewTfUnit, 0, 0);
            this.tlpBtnMember.Controls.Add(this.btnDelTfUnit, 1, 0);
            this.tlpBtnMember.Controls.Add(this.btnCopyTfUnit, 2, 0);
            this.tlpBtnMember.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpBtnMember.Location = new System.Drawing.Point(3, 53);
            this.tlpBtnMember.Name = "tlpBtnMember";
            this.tlpBtnMember.RowCount = 1;
            this.tlpBtnMember.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpBtnMember.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tlpBtnMember.Size = new System.Drawing.Size(525, 29);
            this.tlpBtnMember.TabIndex = 14;
            // 
            // btnNewTfUnit
            // 
            this.btnNewTfUnit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnNewTfUnit.Location = new System.Drawing.Point(3, 2);
            this.btnNewTfUnit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnNewTfUnit.Name = "btnNewTfUnit";
            this.btnNewTfUnit.Size = new System.Drawing.Size(169, 25);
            this.btnNewTfUnit.TabIndex = 0;
            this.btnNewTfUnit.Text = "LGCbtnAddTaskMem";
            this.btnNewTfUnit.UseVisualStyleBackColor = true;
            this.btnNewTfUnit.Click += new System.EventHandler(this.btnAddTaskMem_Click);
            // 
            // btnDelTfUnit
            // 
            this.btnDelTfUnit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDelTfUnit.Location = new System.Drawing.Point(178, 2);
            this.btnDelTfUnit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDelTfUnit.Name = "btnDelTfUnit";
            this.btnDelTfUnit.Size = new System.Drawing.Size(169, 25);
            this.btnDelTfUnit.TabIndex = 0;
            this.btnDelTfUnit.Text = "LGCbtnDelTaskMem";
            this.btnDelTfUnit.UseVisualStyleBackColor = true;
            this.btnDelTfUnit.Click += new System.EventHandler(this.btnDelTaskMem_Click);
            // 
            // btnCopyTfUnit
            // 
            this.btnCopyTfUnit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCopyTfUnit.Location = new System.Drawing.Point(353, 2);
            this.btnCopyTfUnit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCopyTfUnit.Name = "btnCopyTfUnit";
            this.btnCopyTfUnit.Size = new System.Drawing.Size(169, 25);
            this.btnCopyTfUnit.TabIndex = 0;
            this.btnCopyTfUnit.Text = "LGCbtnCopyTaskMem";
            this.btnCopyTfUnit.UseVisualStyleBackColor = true;
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label41.Location = new System.Drawing.Point(3, 5);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(465, 15);
            this.label41.TabIndex = 11;
            this.label41.Text = "LGClblTaskCurType";
            // 
            // mtxbTaskNum
            // 
            this.mtxbTaskNum.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mtxbTaskNum.Location = new System.Drawing.Point(474, 22);
            this.mtxbTaskNum.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.mtxbTaskNum.Mask = "000";
            this.mtxbTaskNum.Name = "mtxbTaskNum";
            this.mtxbTaskNum.PromptChar = ' ';
            this.mtxbTaskNum.Size = new System.Drawing.Size(54, 25);
            this.mtxbTaskNum.TabIndex = 12;
            this.mtxbTaskNum.ValidatingType = typeof(int);
            this.mtxbTaskNum.Validated += new System.EventHandler(this.mtxbTaskNum_Validated);
            // 
            // cbbTaskType
            // 
            this.cbbTaskType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbbTaskType.FormattingEnabled = true;
            this.cbbTaskType.Location = new System.Drawing.Point(3, 25);
            this.cbbTaskType.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbbTaskType.Name = "cbbTaskType";
            this.cbbTaskType.Size = new System.Drawing.Size(465, 23);
            this.cbbTaskType.TabIndex = 13;
            this.cbbTaskType.SelectedValueChanged += new System.EventHandler(this.cbbTaskType_SelectedValueChanged);
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label42.Location = new System.Drawing.Point(474, 0);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(54, 20);
            this.label42.TabIndex = 10;
            this.label42.Text = "LGClblTaskCurNum";
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Dock = System.Windows.Forms.DockStyle.Top;
            this.label40.Location = new System.Drawing.Point(3, 105);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(167, 15);
            this.label40.TabIndex = 1;
            this.label40.Text = "LGClblTaskMemberList";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 115F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.Controls.Add(this.tlpBtnTask, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label37, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label38, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label39, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.mtxbTaskGroup, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.txbTaskID, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.txbTaskName, 1, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 20);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(531, 85);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // tlpBtnTask
            // 
            this.tlpBtnTask.ColumnCount = 3;
            this.tableLayoutPanel1.SetColumnSpan(this.tlpBtnTask, 3);
            this.tlpBtnTask.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpBtnTask.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpBtnTask.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpBtnTask.Controls.Add(this.btnNewTaskforce, 0, 0);
            this.tlpBtnTask.Controls.Add(this.btnDelTaskforce, 1, 0);
            this.tlpBtnTask.Controls.Add(this.btnCopyTaskforce, 2, 0);
            this.tlpBtnTask.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpBtnTask.Location = new System.Drawing.Point(3, 3);
            this.tlpBtnTask.Name = "tlpBtnTask";
            this.tlpBtnTask.RowCount = 1;
            this.tlpBtnTask.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpBtnTask.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tlpBtnTask.Size = new System.Drawing.Size(525, 29);
            this.tlpBtnTask.TabIndex = 14;
            // 
            // btnNewTaskforce
            // 
            this.btnNewTaskforce.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnNewTaskforce.Location = new System.Drawing.Point(3, 2);
            this.btnNewTaskforce.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnNewTaskforce.Name = "btnNewTaskforce";
            this.btnNewTaskforce.Size = new System.Drawing.Size(169, 25);
            this.btnNewTaskforce.TabIndex = 0;
            this.btnNewTaskforce.Text = "LGCbtnNewTask";
            this.btnNewTaskforce.UseVisualStyleBackColor = true;
            this.btnNewTaskforce.Click += new System.EventHandler(this.btnNewTask_Click);
            // 
            // btnDelTaskforce
            // 
            this.btnDelTaskforce.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDelTaskforce.Location = new System.Drawing.Point(178, 2);
            this.btnDelTaskforce.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDelTaskforce.Name = "btnDelTaskforce";
            this.btnDelTaskforce.Size = new System.Drawing.Size(169, 25);
            this.btnDelTaskforce.TabIndex = 0;
            this.btnDelTaskforce.Text = "LGCbtnDelTask";
            this.btnDelTaskforce.UseVisualStyleBackColor = true;
            this.btnDelTaskforce.Click += new System.EventHandler(this.btnDelTask_Click);
            // 
            // btnCopyTaskforce
            // 
            this.btnCopyTaskforce.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCopyTaskforce.Location = new System.Drawing.Point(353, 2);
            this.btnCopyTaskforce.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCopyTaskforce.Name = "btnCopyTaskforce";
            this.btnCopyTaskforce.Size = new System.Drawing.Size(169, 25);
            this.btnCopyTaskforce.TabIndex = 0;
            this.btnCopyTaskforce.Text = "LGCbtnCopyTask";
            this.btnCopyTaskforce.UseVisualStyleBackColor = true;
            this.btnCopyTaskforce.Click += new System.EventHandler(this.btnCopyTask_Click);
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label37.Location = new System.Drawing.Point(3, 40);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(109, 15);
            this.label37.TabIndex = 1;
            this.label37.Text = "LGClblTaskID";
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label38.Location = new System.Drawing.Point(118, 40);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(350, 15);
            this.label38.TabIndex = 1;
            this.label38.Text = "LGClblTaskName";
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label39.Location = new System.Drawing.Point(474, 35);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(54, 20);
            this.label39.TabIndex = 1;
            this.label39.Text = "LGClblTaskGroup";
            // 
            // mtxbTaskGroup
            // 
            this.mtxbTaskGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mtxbTaskGroup.Location = new System.Drawing.Point(474, 57);
            this.mtxbTaskGroup.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.mtxbTaskGroup.Name = "mtxbTaskGroup";
            this.mtxbTaskGroup.Size = new System.Drawing.Size(54, 25);
            this.mtxbTaskGroup.TabIndex = 3;
            this.mtxbTaskGroup.ValidatingType = typeof(int);
            this.mtxbTaskGroup.Validated += new System.EventHandler(this.mtxbTaskGroup_Validated);
            // 
            // txbTaskID
            // 
            this.txbTaskID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txbTaskID.Location = new System.Drawing.Point(3, 57);
            this.txbTaskID.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txbTaskID.Name = "txbTaskID";
            this.txbTaskID.ReadOnly = true;
            this.txbTaskID.Size = new System.Drawing.Size(109, 25);
            this.txbTaskID.TabIndex = 2;
            // 
            // txbTaskName
            // 
            this.txbTaskName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txbTaskName.Location = new System.Drawing.Point(118, 57);
            this.txbTaskName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txbTaskName.Name = "txbTaskName";
            this.txbTaskName.Size = new System.Drawing.Size(350, 25);
            this.txbTaskName.TabIndex = 2;
            this.txbTaskName.Validated += new System.EventHandler(this.txbTaskName_Validated);
            // 
            // PanelTaskforce
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gpbTaskDetial);
            this.Name = "PanelTaskforce";
            this.Size = new System.Drawing.Size(537, 508);
            this.gpbTaskDetial.ResumeLayout(false);
            this.gpbTaskDetial.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tlpBtnMember.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tlpBtnTask.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gpbTaskDetial;
        private System.Windows.Forms.ComboBox cbbTaskType;
        private System.Windows.Forms.MaskedTextBox mtxbTaskNum;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.ListView lvTaskforceUnits;
        private System.Windows.Forms.MaskedTextBox mtxbTaskGroup;
        private System.Windows.Forms.TextBox txbTaskName;
        private System.Windows.Forms.TextBox txbTaskID;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Button btnCopyTaskforce;
        private System.Windows.Forms.Button btnDelTaskforce;
        private System.Windows.Forms.Button btnCopyTfUnit;
        private System.Windows.Forms.Button btnDelTfUnit;
        private System.Windows.Forms.Button btnNewTfUnit;
        private System.Windows.Forms.Button btnNewTaskforce;
        private System.Windows.Forms.ImageList imglstPcx;
        private System.Windows.Forms.TableLayoutPanel tlpBtnMember;
        private System.Windows.Forms.TableLayoutPanel tlpBtnTask;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
    }
}
