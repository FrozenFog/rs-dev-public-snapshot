namespace RelertSharp.GUI.SubWindows.LogicEditor
{
    partial class PanelScriptParam
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
            this.mtxbScriptID = new System.Windows.Forms.MaskedTextBox();
            this.lblNa = new System.Windows.Forms.Label();
            this.txbParam = new System.Windows.Forms.TextBox();
            this.btnNewScript = new System.Windows.Forms.Button();
            this.btnDelScript = new System.Windows.Forms.Button();
            this.btnCopyScript = new System.Windows.Forms.Button();
            this.cbbParam = new System.Windows.Forms.ComboBox();
            this.lblParamName = new System.Windows.Forms.Label();
            this.cbbScriptType = new System.Windows.Forms.ComboBox();
            this.label31 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.rtxbScriptDesc = new System.Windows.Forms.RichTextBox();
            this.lklParamName = new System.Windows.Forms.LinkLabel();
            this.tlpScriptBtn = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.tlpScriptBtn.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // mtxbScriptID
            // 
            this.mtxbScriptID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mtxbScriptID.Location = new System.Drawing.Point(3, 23);
            this.mtxbScriptID.Mask = "00";
            this.mtxbScriptID.Name = "mtxbScriptID";
            this.mtxbScriptID.PromptChar = ' ';
            this.mtxbScriptID.Size = new System.Drawing.Size(39, 25);
            this.mtxbScriptID.TabIndex = 42;
            this.mtxbScriptID.ValidatingType = typeof(int);
            this.mtxbScriptID.TextChanged += new System.EventHandler(this.mtxbScriptID_Validated);
            // 
            // lblNa
            // 
            this.lblNa.AutoSize = true;
            this.lblNa.Enabled = false;
            this.lblNa.Location = new System.Drawing.Point(3, 3);
            this.lblNa.Name = "lblNa";
            this.lblNa.Size = new System.Drawing.Size(111, 15);
            this.lblNa.TabIndex = 41;
            this.lblNa.Text = "LGClblNoParam";
            this.lblNa.Visible = false;
            // 
            // txbParam
            // 
            this.txbParam.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txbParam.Location = new System.Drawing.Point(0, 0);
            this.txbParam.Name = "txbParam";
            this.txbParam.Size = new System.Drawing.Size(361, 25);
            this.txbParam.TabIndex = 39;
            this.txbParam.Visible = false;
            this.txbParam.Validated += new System.EventHandler(this.ParamChanged);
            // 
            // btnNewScript
            // 
            this.btnNewScript.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnNewScript.Location = new System.Drawing.Point(3, 2);
            this.btnNewScript.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnNewScript.Name = "btnNewScript";
            this.btnNewScript.Size = new System.Drawing.Size(114, 30);
            this.btnNewScript.TabIndex = 31;
            this.btnNewScript.Text = "LGCbtnAddScriptMem";
            this.btnNewScript.UseVisualStyleBackColor = true;
            this.btnNewScript.Click += new System.EventHandler(this.btnNewScript_Click);
            // 
            // btnDelScript
            // 
            this.btnDelScript.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDelScript.Location = new System.Drawing.Point(123, 2);
            this.btnDelScript.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDelScript.Name = "btnDelScript";
            this.btnDelScript.Size = new System.Drawing.Size(114, 30);
            this.btnDelScript.TabIndex = 32;
            this.btnDelScript.Text = "LGCbtnDelScriptMem";
            this.btnDelScript.UseVisualStyleBackColor = true;
            this.btnDelScript.Click += new System.EventHandler(this.btnDelScript_Click);
            // 
            // btnCopyScript
            // 
            this.btnCopyScript.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCopyScript.Location = new System.Drawing.Point(243, 2);
            this.btnCopyScript.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCopyScript.Name = "btnCopyScript";
            this.btnCopyScript.Size = new System.Drawing.Size(115, 30);
            this.btnCopyScript.TabIndex = 30;
            this.btnCopyScript.Text = "LGCbtnCopyScriptMem";
            this.btnCopyScript.UseVisualStyleBackColor = true;
            this.btnCopyScript.Click += new System.EventHandler(this.btnCopyScript_Click);
            // 
            // cbbParam
            // 
            this.cbbParam.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbbParam.FormattingEnabled = true;
            this.cbbParam.Location = new System.Drawing.Point(0, 0);
            this.cbbParam.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbbParam.Name = "cbbParam";
            this.cbbParam.Size = new System.Drawing.Size(361, 23);
            this.cbbParam.TabIndex = 38;
            this.cbbParam.Visible = false;
            this.cbbParam.SelectedValueChanged += new System.EventHandler(this.ParamChanged);
            // 
            // lblParamName
            // 
            this.lblParamName.AutoSize = true;
            this.lblParamName.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblParamName.Location = new System.Drawing.Point(0, -1);
            this.lblParamName.Name = "lblParamName";
            this.lblParamName.Size = new System.Drawing.Size(159, 15);
            this.lblParamName.TabIndex = 37;
            this.lblParamName.Text = "LGClblScriptCurPara";
            // 
            // cbbScriptType
            // 
            this.cbbScriptType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbbScriptType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbScriptType.FormattingEnabled = true;
            this.cbbScriptType.Location = new System.Drawing.Point(48, 22);
            this.cbbScriptType.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbbScriptType.Name = "cbbScriptType";
            this.cbbScriptType.Size = new System.Drawing.Size(316, 23);
            this.cbbScriptType.TabIndex = 36;
            this.cbbScriptType.SelectedValueChanged += new System.EventHandler(this.cbbScriptType_SelectedValueChanged);
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.label31, 2);
            this.label31.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label31.Location = new System.Drawing.Point(3, 5);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(361, 15);
            this.label31.TabIndex = 35;
            this.label31.Text = "LGClblScriptCurType";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Dock = System.Windows.Forms.DockStyle.Top;
            this.label34.Location = new System.Drawing.Point(0, 140);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(135, 15);
            this.label34.TabIndex = 34;
            this.label34.Text = "LGClblScriptDesc";
            // 
            // rtxbScriptDesc
            // 
            this.rtxbScriptDesc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxbScriptDesc.Location = new System.Drawing.Point(0, 155);
            this.rtxbScriptDesc.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rtxbScriptDesc.Name = "rtxbScriptDesc";
            this.rtxbScriptDesc.ReadOnly = true;
            this.rtxbScriptDesc.Size = new System.Drawing.Size(367, 212);
            this.rtxbScriptDesc.TabIndex = 33;
            this.rtxbScriptDesc.Text = "";
            // 
            // lklParamName
            // 
            this.lklParamName.AutoSize = true;
            this.lklParamName.Location = new System.Drawing.Point(3, -1);
            this.lklParamName.Name = "lklParamName";
            this.lklParamName.Size = new System.Drawing.Size(87, 15);
            this.lklParamName.TabIndex = 43;
            this.lklParamName.TabStop = true;
            this.lklParamName.Text = "linkLabel1";
            this.lklParamName.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lklParamName_LinkClicked);
            // 
            // tlpScriptBtn
            // 
            this.tlpScriptBtn.ColumnCount = 3;
            this.tableLayoutPanel1.SetColumnSpan(this.tlpScriptBtn, 2);
            this.tlpScriptBtn.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpScriptBtn.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpScriptBtn.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpScriptBtn.Controls.Add(this.btnNewScript, 0, 0);
            this.tlpScriptBtn.Controls.Add(this.btnDelScript, 1, 0);
            this.tlpScriptBtn.Controls.Add(this.btnCopyScript, 2, 0);
            this.tlpScriptBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpScriptBtn.Location = new System.Drawing.Point(3, 103);
            this.tlpScriptBtn.Name = "tlpScriptBtn";
            this.tlpScriptBtn.RowCount = 1;
            this.tlpScriptBtn.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpScriptBtn.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tlpScriptBtn.Size = new System.Drawing.Size(361, 34);
            this.tlpScriptBtn.TabIndex = 44;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.tlpScriptBtn, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label31, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.mtxbScriptID, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.cbbScriptType, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(367, 140);
            this.tableLayoutPanel1.TabIndex = 45;
            // 
            // panel2
            // 
            this.panel2.AutoSize = true;
            this.panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.SetColumnSpan(this.panel2, 2);
            this.panel2.Controls.Add(this.lblNa);
            this.panel2.Controls.Add(this.cbbParam);
            this.panel2.Controls.Add(this.txbParam);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 73);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(361, 24);
            this.panel2.TabIndex = 46;
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.SetColumnSpan(this.panel1, 2);
            this.panel1.Controls.Add(this.lklParamName);
            this.panel1.Controls.Add(this.lblParamName);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 53);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(361, 14);
            this.panel1.TabIndex = 46;
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.rtxbScriptDesc);
            this.pnlMain.Controls.Add(this.label34);
            this.pnlMain.Controls.Add(this.tableLayoutPanel1);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(367, 367);
            this.pnlMain.TabIndex = 46;
            // 
            // PanelScriptParam
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlMain);
            this.Name = "PanelScriptParam";
            this.Size = new System.Drawing.Size(367, 367);
            this.tlpScriptBtn.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MaskedTextBox mtxbScriptID;
        private System.Windows.Forms.Label lblNa;
        private System.Windows.Forms.TextBox txbParam;
        private System.Windows.Forms.Button btnNewScript;
        private System.Windows.Forms.Button btnDelScript;
        private System.Windows.Forms.Button btnCopyScript;
        private System.Windows.Forms.ComboBox cbbParam;
        private System.Windows.Forms.Label lblParamName;
        private System.Windows.Forms.ComboBox cbbScriptType;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.RichTextBox rtxbScriptDesc;
        private System.Windows.Forms.LinkLabel lklParamName;
        private System.Windows.Forms.TableLayoutPanel tlpScriptBtn;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pnlMain;
    }
}
