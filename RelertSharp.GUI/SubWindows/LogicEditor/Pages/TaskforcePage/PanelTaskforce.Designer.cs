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
            this.cbbTaskType = new System.Windows.Forms.ComboBox();
            this.mtxbTaskNum = new System.Windows.Forms.MaskedTextBox();
            this.label42 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.lvTaskforceUnits = new System.Windows.Forms.ListView();
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
            this.imglstPcx = new System.Windows.Forms.ImageList(this.components);
            this.gpbTaskDetial.SuspendLayout();
            this.SuspendLayout();
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
            this.gpbTaskDetial.Location = new System.Drawing.Point(0, 0);
            this.gpbTaskDetial.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gpbTaskDetial.Name = "gpbTaskDetial";
            this.gpbTaskDetial.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gpbTaskDetial.Size = new System.Drawing.Size(453, 667);
            this.gpbTaskDetial.TabIndex = 3;
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
            this.cbbTaskType.SelectedValueChanged += new System.EventHandler(this.cbbTaskType_SelectedValueChanged);
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
            // mtxbTaskGroup
            // 
            this.mtxbTaskGroup.Location = new System.Drawing.Point(376, 74);
            this.mtxbTaskGroup.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.mtxbTaskGroup.Name = "mtxbTaskGroup";
            this.mtxbTaskGroup.Size = new System.Drawing.Size(60, 25);
            this.mtxbTaskGroup.TabIndex = 3;
            this.mtxbTaskGroup.ValidatingType = typeof(int);
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
            this.btnCopyTfUnit.Location = new System.Drawing.Point(304, 629);
            this.btnCopyTfUnit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCopyTfUnit.Name = "btnCopyTfUnit";
            this.btnCopyTfUnit.Size = new System.Drawing.Size(132, 29);
            this.btnCopyTfUnit.TabIndex = 0;
            this.btnCopyTfUnit.Text = "LGCbtnCopyTaskMem";
            this.btnCopyTfUnit.UseVisualStyleBackColor = true;
            // 
            // btnDelTfUnit
            // 
            this.btnDelTfUnit.Location = new System.Drawing.Point(159, 629);
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
            this.btnNewTfUnit.Location = new System.Drawing.Point(5, 629);
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
            // imglstPcx
            // 
            this.imglstPcx.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imglstPcx.ImageSize = new System.Drawing.Size(60, 48);
            this.imglstPcx.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // PanelTaskforce
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gpbTaskDetial);
            this.Name = "PanelTaskforce";
            this.Size = new System.Drawing.Size(453, 667);
            this.gpbTaskDetial.ResumeLayout(false);
            this.gpbTaskDetial.PerformLayout();
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
    }
}
