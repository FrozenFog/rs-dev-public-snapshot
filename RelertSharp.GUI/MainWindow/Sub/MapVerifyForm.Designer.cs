namespace RelertSharp.GUI
{
    partial class MapVerifyForm
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
            this.lvVerifyResult = new System.Windows.Forms.ListView();
            this.colLevel = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colMessage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colLocation = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imgErrors = new System.Windows.Forms.ImageList(this.components);
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tlpMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvVerifyResult
            // 
            this.lvVerifyResult.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colLevel,
            this.colMessage,
            this.colType,
            this.colLocation});
            this.tlpMain.SetColumnSpan(this.lvVerifyResult, 3);
            this.lvVerifyResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvVerifyResult.FullRowSelect = true;
            this.lvVerifyResult.HideSelection = false;
            this.lvVerifyResult.Location = new System.Drawing.Point(3, 2);
            this.lvVerifyResult.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lvVerifyResult.MultiSelect = false;
            this.lvVerifyResult.Name = "lvVerifyResult";
            this.lvVerifyResult.Size = new System.Drawing.Size(759, 372);
            this.lvVerifyResult.SmallImageList = this.imgErrors;
            this.lvVerifyResult.TabIndex = 0;
            this.lvVerifyResult.UseCompatibleStateImageBehavior = false;
            this.lvVerifyResult.View = System.Windows.Forms.View.Details;
            this.lvVerifyResult.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvVerifyResult_ColumnClick);
            this.lvVerifyResult.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvVerifyResult_MouseDoubleClick);
            // 
            // colLevel
            // 
            this.colLevel.Text = "VERColType";
            this.colLevel.Width = 90;
            // 
            // colMessage
            // 
            this.colMessage.Text = "VERColMsg";
            this.colMessage.Width = 54;
            // 
            // colType
            // 
            this.colType.Text = "VERColLevel";
            this.colType.Width = 72;
            // 
            // colLocation
            // 
            this.colLocation.Text = "VERColTrace";
            this.colLocation.Width = 350;
            // 
            // imgErrors
            // 
            this.imgErrors.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imgErrors.ImageSize = new System.Drawing.Size(16, 16);
            this.imgErrors.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // tlpMain
            // 
            this.tlpMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tlpMain.ColumnCount = 3;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpMain.Controls.Add(this.lvVerifyResult, 0, 0);
            this.tlpMain.Controls.Add(this.btnOK, 0, 1);
            this.tlpMain.Controls.Add(this.btnCancel, 2, 1);
            this.tlpMain.Location = new System.Drawing.Point(25, 12);
            this.tlpMain.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 2;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpMain.Size = new System.Drawing.Size(765, 416);
            this.tlpMain.TabIndex = 1;
            // 
            // btnOK
            // 
            this.btnOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnOK.Location = new System.Drawing.Point(4, 380);
            this.btnOK.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(183, 32);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "DLGContinue";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCancel.Location = new System.Drawing.Point(577, 380);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(184, 32);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "DLGCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // MapVerifyForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(811, 441);
            this.Controls.Add(this.tlpMain);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "MapVerifyForm";
            this.ShowIcon = false;
            this.Text = "VERTitle";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MapVerifyForm_FormClosing);
            this.tlpMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvVerifyResult;
        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.ColumnHeader colLevel;
        private System.Windows.Forms.ColumnHeader colMessage;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ColumnHeader colType;
        private System.Windows.Forms.ColumnHeader colLocation;
        private System.Windows.Forms.ImageList imgErrors;
    }
}