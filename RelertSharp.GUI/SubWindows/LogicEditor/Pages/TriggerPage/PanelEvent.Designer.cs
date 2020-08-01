namespace RelertSharp.GUI.SubWindows.LogicEditor
{
    partial class PanelEvent
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
            this.gpbEvents = new System.Windows.Forms.GroupBox();
            this.splitMain = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbxEventList = new System.Windows.Forms.ListBox();
            this.tlpBtn = new System.Windows.Forms.TableLayoutPanel();
            this.btnNewEvent = new System.Windows.Forms.Button();
            this.btnCopyEvent = new System.Windows.Forms.Button();
            this.cmsCopyEvent = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiCopyEventAdv = new System.Windows.Forms.ToolStripMenuItem();
            this.btnDeleteEvent = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.pnlParameter = new RelertSharp.GUI.SubWindows.LogicEditor.ParameterPanel();
            this.gpbEvents.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            this.splitMain.Panel1.SuspendLayout();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tlpBtn.SuspendLayout();
            this.cmsCopyEvent.SuspendLayout();
            this.SuspendLayout();
            // 
            // gpbEvents
            // 
            this.gpbEvents.Controls.Add(this.splitMain);
            this.gpbEvents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gpbEvents.Location = new System.Drawing.Point(0, 0);
            this.gpbEvents.Margin = new System.Windows.Forms.Padding(4);
            this.gpbEvents.Name = "gpbEvents";
            this.gpbEvents.Padding = new System.Windows.Forms.Padding(4);
            this.gpbEvents.Size = new System.Drawing.Size(573, 455);
            this.gpbEvents.TabIndex = 6;
            this.gpbEvents.TabStop = false;
            this.gpbEvents.Text = "LGCgpbTrgEvents";
            // 
            // splitMain
            // 
            this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitMain.Location = new System.Drawing.Point(4, 22);
            this.splitMain.Name = "splitMain";
            // 
            // splitMain.Panel1
            // 
            this.splitMain.Panel1.Controls.Add(this.panel1);
            // 
            // splitMain.Panel2
            // 
            this.splitMain.Panel2.Controls.Add(this.pnlParameter);
            this.splitMain.Size = new System.Drawing.Size(565, 429);
            this.splitMain.SplitterDistance = 161;
            this.splitMain.TabIndex = 11;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbxEventList);
            this.panel1.Controls.Add(this.tlpBtn);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(161, 429);
            this.panel1.TabIndex = 11;
            // 
            // lbxEventList
            // 
            this.lbxEventList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbxEventList.FormattingEnabled = true;
            this.lbxEventList.ItemHeight = 15;
            this.lbxEventList.Location = new System.Drawing.Point(0, 15);
            this.lbxEventList.Margin = new System.Windows.Forms.Padding(4);
            this.lbxEventList.Name = "lbxEventList";
            this.lbxEventList.Size = new System.Drawing.Size(161, 296);
            this.lbxEventList.Sorted = true;
            this.lbxEventList.TabIndex = 9;
            this.lbxEventList.SelectedIndexChanged += new System.EventHandler(this.lbxEventList_SelectedIndexChanged);
            // 
            // tlpBtn
            // 
            this.tlpBtn.ColumnCount = 1;
            this.tlpBtn.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpBtn.Controls.Add(this.btnNewEvent, 0, 0);
            this.tlpBtn.Controls.Add(this.btnCopyEvent, 0, 2);
            this.tlpBtn.Controls.Add(this.btnDeleteEvent, 0, 1);
            this.tlpBtn.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tlpBtn.Location = new System.Drawing.Point(0, 311);
            this.tlpBtn.Name = "tlpBtn";
            this.tlpBtn.RowCount = 3;
            this.tlpBtn.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpBtn.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpBtn.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpBtn.Size = new System.Drawing.Size(161, 118);
            this.tlpBtn.TabIndex = 10;
            // 
            // btnNewEvent
            // 
            this.btnNewEvent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnNewEvent.Location = new System.Drawing.Point(4, 4);
            this.btnNewEvent.Margin = new System.Windows.Forms.Padding(4);
            this.btnNewEvent.Name = "btnNewEvent";
            this.btnNewEvent.Size = new System.Drawing.Size(153, 31);
            this.btnNewEvent.TabIndex = 6;
            this.btnNewEvent.Text = "LGCbtnNewEvent";
            this.btnNewEvent.UseVisualStyleBackColor = true;
            this.btnNewEvent.Click += new System.EventHandler(this.btnNewEvent_Click);
            // 
            // btnCopyEvent
            // 
            this.btnCopyEvent.ContextMenuStrip = this.cmsCopyEvent;
            this.btnCopyEvent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCopyEvent.Location = new System.Drawing.Point(4, 82);
            this.btnCopyEvent.Margin = new System.Windows.Forms.Padding(4);
            this.btnCopyEvent.Name = "btnCopyEvent";
            this.btnCopyEvent.Size = new System.Drawing.Size(153, 32);
            this.btnCopyEvent.TabIndex = 8;
            this.btnCopyEvent.Text = "LGCbtnCopyEvent";
            this.btnCopyEvent.UseVisualStyleBackColor = true;
            this.btnCopyEvent.Click += new System.EventHandler(this.btnCopyEvent_Click);
            // 
            // cmsCopyEvent
            // 
            this.cmsCopyEvent.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmsCopyEvent.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiCopyEventAdv});
            this.cmsCopyEvent.Name = "cmsCopyEvent";
            this.cmsCopyEvent.Size = new System.Drawing.Size(206, 28);
            // 
            // tsmiCopyEventAdv
            // 
            this.tsmiCopyEventAdv.Name = "tsmiCopyEventAdv";
            this.tsmiCopyEventAdv.Size = new System.Drawing.Size(205, 24);
            this.tsmiCopyEventAdv.Text = "LGCtsmiCopyAdv";
            this.tsmiCopyEventAdv.Click += new System.EventHandler(this.tsmiCopyEventAdv_Click);
            // 
            // btnDeleteEvent
            // 
            this.btnDeleteEvent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDeleteEvent.Location = new System.Drawing.Point(4, 43);
            this.btnDeleteEvent.Margin = new System.Windows.Forms.Padding(4);
            this.btnDeleteEvent.Name = "btnDeleteEvent";
            this.btnDeleteEvent.Size = new System.Drawing.Size(153, 31);
            this.btnDeleteEvent.TabIndex = 7;
            this.btnDeleteEvent.Text = "LGCbtnDelEvent";
            this.btnDeleteEvent.UseVisualStyleBackColor = true;
            this.btnDeleteEvent.Click += new System.EventHandler(this.btnDeleteEvent_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Dock = System.Windows.Forms.DockStyle.Top;
            this.label11.Location = new System.Drawing.Point(0, 0);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(127, 15);
            this.label11.TabIndex = 7;
            this.label11.Text = "LGClblEventList";
            // 
            // pnlParameter
            // 
            this.pnlParameter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlParameter.Location = new System.Drawing.Point(0, 0);
            this.pnlParameter.Name = "pnlParameter";
            this.pnlParameter.Size = new System.Drawing.Size(400, 429);
            this.pnlParameter.TabIndex = 10;
            // 
            // PanelEvent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gpbEvents);
            this.Name = "PanelEvent";
            this.Size = new System.Drawing.Size(573, 455);
            this.gpbEvents.ResumeLayout(false);
            this.splitMain.Panel1.ResumeLayout(false);
            this.splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
            this.splitMain.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tlpBtn.ResumeLayout(false);
            this.cmsCopyEvent.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gpbEvents;
        private System.Windows.Forms.ListBox lbxEventList;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnCopyEvent;
        private System.Windows.Forms.Button btnDeleteEvent;
        private System.Windows.Forms.Button btnNewEvent;
        private System.Windows.Forms.ContextMenuStrip cmsCopyEvent;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopyEventAdv;
        private ParameterPanel pnlParameter;
        private System.Windows.Forms.SplitContainer splitMain;
        private System.Windows.Forms.TableLayoutPanel tlpBtn;
        private System.Windows.Forms.Panel panel1;
    }
}
