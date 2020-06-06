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
            this.pnlParameter = new RelertSharp.GUI.SubWindows.LogicEditor.ParameterPanel();
            this.lbxEventList = new System.Windows.Forms.ListBox();
            this.label11 = new System.Windows.Forms.Label();
            this.btnCopyEvent = new System.Windows.Forms.Button();
            this.cmsCopyEvent = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiCopyEventAdv = new System.Windows.Forms.ToolStripMenuItem();
            this.btnDeleteEvent = new System.Windows.Forms.Button();
            this.btnNewEvent = new System.Windows.Forms.Button();
            this.gpbEvents.SuspendLayout();
            this.cmsCopyEvent.SuspendLayout();
            this.SuspendLayout();
            // 
            // gpbEvents
            // 
            this.gpbEvents.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.gpbEvents.Controls.Add(this.pnlParameter);
            this.gpbEvents.Controls.Add(this.lbxEventList);
            this.gpbEvents.Controls.Add(this.label11);
            this.gpbEvents.Controls.Add(this.btnCopyEvent);
            this.gpbEvents.Controls.Add(this.btnDeleteEvent);
            this.gpbEvents.Controls.Add(this.btnNewEvent);
            this.gpbEvents.Location = new System.Drawing.Point(0, 0);
            this.gpbEvents.Margin = new System.Windows.Forms.Padding(4);
            this.gpbEvents.Name = "gpbEvents";
            this.gpbEvents.Padding = new System.Windows.Forms.Padding(4);
            this.gpbEvents.Size = new System.Drawing.Size(604, 448);
            this.gpbEvents.TabIndex = 6;
            this.gpbEvents.TabStop = false;
            this.gpbEvents.Text = "LGCgpbTrgEvents";
            // 
            // pnlParameter
            // 
            this.pnlParameter.Location = new System.Drawing.Point(201, 29);
            this.pnlParameter.Name = "pnlParameter";
            this.pnlParameter.Size = new System.Drawing.Size(394, 411);
            this.pnlParameter.TabIndex = 10;
            // 
            // lbxEventList
            // 
            this.lbxEventList.FormattingEnabled = true;
            this.lbxEventList.ItemHeight = 15;
            this.lbxEventList.Location = new System.Drawing.Point(11, 48);
            this.lbxEventList.Margin = new System.Windows.Forms.Padding(4);
            this.lbxEventList.Name = "lbxEventList";
            this.lbxEventList.Size = new System.Drawing.Size(183, 289);
            this.lbxEventList.Sorted = true;
            this.lbxEventList.TabIndex = 9;
            this.lbxEventList.SelectedIndexChanged += new System.EventHandler(this.lbxEventList_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(8, 29);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(127, 15);
            this.label11.TabIndex = 7;
            this.label11.Text = "LGClblEventList";
            // 
            // btnCopyEvent
            // 
            this.btnCopyEvent.ContextMenuStrip = this.cmsCopyEvent;
            this.btnCopyEvent.Location = new System.Drawing.Point(11, 411);
            this.btnCopyEvent.Margin = new System.Windows.Forms.Padding(4);
            this.btnCopyEvent.Name = "btnCopyEvent";
            this.btnCopyEvent.Size = new System.Drawing.Size(184, 29);
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
            this.btnDeleteEvent.Location = new System.Drawing.Point(11, 375);
            this.btnDeleteEvent.Margin = new System.Windows.Forms.Padding(4);
            this.btnDeleteEvent.Name = "btnDeleteEvent";
            this.btnDeleteEvent.Size = new System.Drawing.Size(184, 29);
            this.btnDeleteEvent.TabIndex = 7;
            this.btnDeleteEvent.Text = "LGCbtnDelEvent";
            this.btnDeleteEvent.UseVisualStyleBackColor = true;
            this.btnDeleteEvent.Click += new System.EventHandler(this.btnDeleteEvent_Click);
            // 
            // btnNewEvent
            // 
            this.btnNewEvent.Location = new System.Drawing.Point(11, 339);
            this.btnNewEvent.Margin = new System.Windows.Forms.Padding(4);
            this.btnNewEvent.Name = "btnNewEvent";
            this.btnNewEvent.Size = new System.Drawing.Size(184, 29);
            this.btnNewEvent.TabIndex = 6;
            this.btnNewEvent.Text = "LGCbtnNewEvent";
            this.btnNewEvent.UseVisualStyleBackColor = true;
            this.btnNewEvent.Click += new System.EventHandler(this.btnNewEvent_Click);
            // 
            // PanelEvent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gpbEvents);
            this.Name = "PanelEvent";
            this.Size = new System.Drawing.Size(604, 448);
            this.gpbEvents.ResumeLayout(false);
            this.gpbEvents.PerformLayout();
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
    }
}
