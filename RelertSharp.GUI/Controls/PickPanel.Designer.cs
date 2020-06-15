namespace RelertSharp.GUI
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
            this.trvObject = new System.Windows.Forms.TreeView();
            this.imgMain = new System.Windows.Forms.ImageList(this.components);
            this.tbpTerrain = new System.Windows.Forms.TabPage();
            this.tbpSmudge = new System.Windows.Forms.TabPage();
            this.tbpOverlay = new System.Windows.Forms.TabPage();
            this.tbpWaypoint = new System.Windows.Forms.TabPage();
            this.tbpCelltag = new System.Windows.Forms.TabPage();
            this.tbpBaseNode = new System.Windows.Forms.TabPage();
            this.tbcMain.SuspendLayout();
            this.tbpObject.SuspendLayout();
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
            this.tbcMain.Size = new System.Drawing.Size(311, 595);
            this.tbcMain.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
            this.tbcMain.TabIndex = 0;
            // 
            // tbpObject
            // 
            this.tbpObject.Controls.Add(this.trvObject);
            this.tbpObject.Location = new System.Drawing.Point(4, 67);
            this.tbpObject.Name = "tbpObject";
            this.tbpObject.Padding = new System.Windows.Forms.Padding(3);
            this.tbpObject.Size = new System.Drawing.Size(303, 524);
            this.tbpObject.TabIndex = 0;
            this.tbpObject.Text = "General Objects";
            this.tbpObject.UseVisualStyleBackColor = true;
            // 
            // trvObject
            // 
            this.trvObject.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvObject.ImageIndex = 0;
            this.trvObject.ImageList = this.imgMain;
            this.trvObject.Location = new System.Drawing.Point(3, 3);
            this.trvObject.Name = "trvObject";
            this.trvObject.SelectedImageIndex = 0;
            this.trvObject.ShowNodeToolTips = true;
            this.trvObject.Size = new System.Drawing.Size(297, 518);
            this.trvObject.TabIndex = 0;
            // 
            // imgMain
            // 
            this.imgMain.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imgMain.ImageSize = new System.Drawing.Size(16, 16);
            this.imgMain.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // tbpTerrain
            // 
            this.tbpTerrain.Location = new System.Drawing.Point(4, 67);
            this.tbpTerrain.Name = "tbpTerrain";
            this.tbpTerrain.Size = new System.Drawing.Size(303, 524);
            this.tbpTerrain.TabIndex = 4;
            this.tbpTerrain.Text = "Terrains";
            this.tbpTerrain.UseVisualStyleBackColor = true;
            // 
            // tbpSmudge
            // 
            this.tbpSmudge.Location = new System.Drawing.Point(4, 67);
            this.tbpSmudge.Name = "tbpSmudge";
            this.tbpSmudge.Size = new System.Drawing.Size(303, 524);
            this.tbpSmudge.TabIndex = 5;
            this.tbpSmudge.Text = "Smudges";
            this.tbpSmudge.UseVisualStyleBackColor = true;
            // 
            // tbpOverlay
            // 
            this.tbpOverlay.Location = new System.Drawing.Point(4, 67);
            this.tbpOverlay.Name = "tbpOverlay";
            this.tbpOverlay.Size = new System.Drawing.Size(303, 524);
            this.tbpOverlay.TabIndex = 6;
            this.tbpOverlay.Text = "Overlays";
            this.tbpOverlay.UseVisualStyleBackColor = true;
            // 
            // tbpWaypoint
            // 
            this.tbpWaypoint.Location = new System.Drawing.Point(4, 67);
            this.tbpWaypoint.Name = "tbpWaypoint";
            this.tbpWaypoint.Size = new System.Drawing.Size(303, 524);
            this.tbpWaypoint.TabIndex = 7;
            this.tbpWaypoint.Text = "Waypoints";
            this.tbpWaypoint.UseVisualStyleBackColor = true;
            // 
            // tbpCelltag
            // 
            this.tbpCelltag.Location = new System.Drawing.Point(4, 67);
            this.tbpCelltag.Name = "tbpCelltag";
            this.tbpCelltag.Size = new System.Drawing.Size(303, 524);
            this.tbpCelltag.TabIndex = 8;
            this.tbpCelltag.Text = "Celltags";
            this.tbpCelltag.UseVisualStyleBackColor = true;
            // 
            // tbpBaseNode
            // 
            this.tbpBaseNode.Location = new System.Drawing.Point(4, 67);
            this.tbpBaseNode.Name = "tbpBaseNode";
            this.tbpBaseNode.Size = new System.Drawing.Size(303, 524);
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
            this.Size = new System.Drawing.Size(311, 595);
            this.tbcMain.ResumeLayout(false);
            this.tbpObject.ResumeLayout(false);
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
    }
}
