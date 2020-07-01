namespace RelertSharp.GUI.Controls
{
    partial class TilePanel
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
            this.tbpGeneral = new System.Windows.Forms.TabPage();
            this.splitGeneral = new System.Windows.Forms.SplitContainer();
            this.trvGeneral = new System.Windows.Forms.TreeView();
            this.lvGeneral = new System.Windows.Forms.ListView();
            this.tbpAll = new System.Windows.Forms.TabPage();
            this.imgAllTiles = new System.Windows.Forms.ImageList(this.components);
            this.cbbAllTiles = new System.Windows.Forms.ComboBox();
            this.flpAllTiles = new System.Windows.Forms.FlowLayoutPanel();
            this.tbcMain.SuspendLayout();
            this.tbpGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitGeneral)).BeginInit();
            this.splitGeneral.Panel1.SuspendLayout();
            this.splitGeneral.Panel2.SuspendLayout();
            this.splitGeneral.SuspendLayout();
            this.tbpAll.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbcMain
            // 
            this.tbcMain.Controls.Add(this.tbpGeneral);
            this.tbcMain.Controls.Add(this.tbpAll);
            this.tbcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbcMain.Location = new System.Drawing.Point(0, 0);
            this.tbcMain.Multiline = true;
            this.tbcMain.Name = "tbcMain";
            this.tbcMain.SelectedIndex = 0;
            this.tbcMain.Size = new System.Drawing.Size(276, 722);
            this.tbcMain.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
            this.tbcMain.TabIndex = 0;
            // 
            // tbpGeneral
            // 
            this.tbpGeneral.Controls.Add(this.splitGeneral);
            this.tbpGeneral.Location = new System.Drawing.Point(4, 46);
            this.tbpGeneral.Name = "tbpGeneral";
            this.tbpGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tbpGeneral.Size = new System.Drawing.Size(268, 672);
            this.tbpGeneral.TabIndex = 0;
            this.tbpGeneral.Text = "General Tiles";
            this.tbpGeneral.UseVisualStyleBackColor = true;
            // 
            // splitGeneral
            // 
            this.splitGeneral.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitGeneral.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitGeneral.Location = new System.Drawing.Point(3, 3);
            this.splitGeneral.Name = "splitGeneral";
            this.splitGeneral.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitGeneral.Panel1
            // 
            this.splitGeneral.Panel1.Controls.Add(this.trvGeneral);
            // 
            // splitGeneral.Panel2
            // 
            this.splitGeneral.Panel2.Controls.Add(this.lvGeneral);
            this.splitGeneral.Size = new System.Drawing.Size(262, 666);
            this.splitGeneral.SplitterDistance = 379;
            this.splitGeneral.TabIndex = 0;
            // 
            // trvGeneral
            // 
            this.trvGeneral.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvGeneral.Location = new System.Drawing.Point(0, 0);
            this.trvGeneral.Name = "trvGeneral";
            this.trvGeneral.Size = new System.Drawing.Size(262, 379);
            this.trvGeneral.TabIndex = 0;
            // 
            // lvGeneral
            // 
            this.lvGeneral.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvGeneral.HideSelection = false;
            this.lvGeneral.Location = new System.Drawing.Point(0, 0);
            this.lvGeneral.Name = "lvGeneral";
            this.lvGeneral.Size = new System.Drawing.Size(262, 283);
            this.lvGeneral.TabIndex = 0;
            this.lvGeneral.UseCompatibleStateImageBehavior = false;
            // 
            // tbpAll
            // 
            this.tbpAll.Controls.Add(this.flpAllTiles);
            this.tbpAll.Controls.Add(this.cbbAllTiles);
            this.tbpAll.Location = new System.Drawing.Point(4, 46);
            this.tbpAll.Name = "tbpAll";
            this.tbpAll.Padding = new System.Windows.Forms.Padding(3);
            this.tbpAll.Size = new System.Drawing.Size(268, 672);
            this.tbpAll.TabIndex = 1;
            this.tbpAll.Text = "All available Tiles";
            this.tbpAll.UseVisualStyleBackColor = true;
            // 
            // imgAllTiles
            // 
            this.imgAllTiles.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imgAllTiles.ImageSize = new System.Drawing.Size(16, 16);
            this.imgAllTiles.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // cbbAllTiles
            // 
            this.cbbAllTiles.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbbAllTiles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbAllTiles.FormattingEnabled = true;
            this.cbbAllTiles.Location = new System.Drawing.Point(3, 3);
            this.cbbAllTiles.Name = "cbbAllTiles";
            this.cbbAllTiles.Size = new System.Drawing.Size(262, 23);
            this.cbbAllTiles.Sorted = true;
            this.cbbAllTiles.TabIndex = 0;
            this.cbbAllTiles.SelectedIndexChanged += new System.EventHandler(this.cbbAllTiles_SelectedIndexChanged);
            // 
            // flpAllTiles
            // 
            this.flpAllTiles.AutoScroll = true;
            this.flpAllTiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpAllTiles.Location = new System.Drawing.Point(3, 26);
            this.flpAllTiles.Name = "flpAllTiles";
            this.flpAllTiles.Size = new System.Drawing.Size(262, 643);
            this.flpAllTiles.TabIndex = 1;
            // 
            // TilePanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbcMain);
            this.Name = "TilePanel";
            this.Size = new System.Drawing.Size(276, 722);
            this.tbcMain.ResumeLayout(false);
            this.tbpGeneral.ResumeLayout(false);
            this.splitGeneral.Panel1.ResumeLayout(false);
            this.splitGeneral.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitGeneral)).EndInit();
            this.splitGeneral.ResumeLayout(false);
            this.tbpAll.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tbcMain;
        private System.Windows.Forms.TabPage tbpGeneral;
        private System.Windows.Forms.TabPage tbpAll;
        private System.Windows.Forms.SplitContainer splitGeneral;
        private System.Windows.Forms.TreeView trvGeneral;
        private System.Windows.Forms.ListView lvGeneral;
        private System.Windows.Forms.ComboBox cbbAllTiles;
        private System.Windows.Forms.ImageList imgAllTiles;
        private System.Windows.Forms.FlowLayoutPanel flpAllTiles;
    }
}
