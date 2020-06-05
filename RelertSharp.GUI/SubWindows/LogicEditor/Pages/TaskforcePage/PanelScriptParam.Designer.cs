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
            this.SuspendLayout();
            // 
            // mtxbScriptID
            // 
            this.mtxbScriptID.Location = new System.Drawing.Point(0, 20);
            this.mtxbScriptID.Mask = "00";
            this.mtxbScriptID.Name = "mtxbScriptID";
            this.mtxbScriptID.PromptChar = ' ';
            this.mtxbScriptID.Size = new System.Drawing.Size(41, 25);
            this.mtxbScriptID.TabIndex = 42;
            this.mtxbScriptID.ValidatingType = typeof(int);
            this.mtxbScriptID.Validated += new System.EventHandler(this.mtxbScriptID_Validated);
            // 
            // lblNa
            // 
            this.lblNa.AutoSize = true;
            this.lblNa.Enabled = false;
            this.lblNa.Location = new System.Drawing.Point(307, 24);
            this.lblNa.Name = "lblNa";
            this.lblNa.Size = new System.Drawing.Size(111, 15);
            this.lblNa.TabIndex = 41;
            this.lblNa.Text = "LGClblNoParam";
            this.lblNa.Visible = false;
            // 
            // txbParam
            // 
            this.txbParam.Location = new System.Drawing.Point(307, 20);
            this.txbParam.Name = "txbParam";
            this.txbParam.Size = new System.Drawing.Size(137, 25);
            this.txbParam.TabIndex = 39;
            this.txbParam.Visible = false;
            // 
            // btnNewScript
            // 
            this.btnNewScript.Location = new System.Drawing.Point(0, 54);
            this.btnNewScript.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnNewScript.Name = "btnNewScript";
            this.btnNewScript.Size = new System.Drawing.Size(137, 29);
            this.btnNewScript.TabIndex = 31;
            this.btnNewScript.Text = "LGCbtnNewScript";
            this.btnNewScript.UseVisualStyleBackColor = true;
            // 
            // btnDelScript
            // 
            this.btnDelScript.Location = new System.Drawing.Point(307, 54);
            this.btnDelScript.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDelScript.Name = "btnDelScript";
            this.btnDelScript.Size = new System.Drawing.Size(137, 29);
            this.btnDelScript.TabIndex = 32;
            this.btnDelScript.Text = "LGCbtnDelScript";
            this.btnDelScript.UseVisualStyleBackColor = true;
            // 
            // btnCopyScript
            // 
            this.btnCopyScript.Location = new System.Drawing.Point(155, 54);
            this.btnCopyScript.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCopyScript.Name = "btnCopyScript";
            this.btnCopyScript.Size = new System.Drawing.Size(137, 29);
            this.btnCopyScript.TabIndex = 30;
            this.btnCopyScript.Text = "LGCbtnCopyScript";
            this.btnCopyScript.UseVisualStyleBackColor = true;
            // 
            // cbbParam
            // 
            this.cbbParam.FormattingEnabled = true;
            this.cbbParam.Location = new System.Drawing.Point(307, 21);
            this.cbbParam.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbbParam.Name = "cbbParam";
            this.cbbParam.Size = new System.Drawing.Size(137, 23);
            this.cbbParam.TabIndex = 38;
            this.cbbParam.Visible = false;
            // 
            // lblParamName
            // 
            this.lblParamName.AutoSize = true;
            this.lblParamName.Location = new System.Drawing.Point(305, 1);
            this.lblParamName.Name = "lblParamName";
            this.lblParamName.Size = new System.Drawing.Size(159, 15);
            this.lblParamName.TabIndex = 37;
            this.lblParamName.Text = "LGClblScriptCurPara";
            // 
            // cbbScriptType
            // 
            this.cbbScriptType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbScriptType.FormattingEnabled = true;
            this.cbbScriptType.Location = new System.Drawing.Point(47, 21);
            this.cbbScriptType.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbbScriptType.Name = "cbbScriptType";
            this.cbbScriptType.Size = new System.Drawing.Size(245, 23);
            this.cbbScriptType.TabIndex = 36;
            this.cbbScriptType.SelectedValueChanged += new System.EventHandler(this.cbbScriptType_SelectedValueChanged);
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(0, 0);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(159, 15);
            this.label31.TabIndex = 35;
            this.label31.Text = "LGClblScriptCurType";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(0, 90);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(135, 15);
            this.label34.TabIndex = 34;
            this.label34.Text = "LGClblScriptDesc";
            // 
            // rtxbScriptDesc
            // 
            this.rtxbScriptDesc.Location = new System.Drawing.Point(3, 107);
            this.rtxbScriptDesc.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rtxbScriptDesc.Name = "rtxbScriptDesc";
            this.rtxbScriptDesc.ReadOnly = true;
            this.rtxbScriptDesc.Size = new System.Drawing.Size(445, 195);
            this.rtxbScriptDesc.TabIndex = 33;
            this.rtxbScriptDesc.Text = "";
            // 
            // lklParamName
            // 
            this.lklParamName.AutoSize = true;
            this.lklParamName.Location = new System.Drawing.Point(305, 1);
            this.lklParamName.Name = "lklParamName";
            this.lklParamName.Size = new System.Drawing.Size(87, 15);
            this.lklParamName.TabIndex = 43;
            this.lklParamName.TabStop = true;
            this.lklParamName.Text = "linkLabel1";
            // 
            // PanelScriptParam
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lklParamName);
            this.Controls.Add(this.mtxbScriptID);
            this.Controls.Add(this.lblNa);
            this.Controls.Add(this.txbParam);
            this.Controls.Add(this.btnNewScript);
            this.Controls.Add(this.btnDelScript);
            this.Controls.Add(this.btnCopyScript);
            this.Controls.Add(this.cbbParam);
            this.Controls.Add(this.lblParamName);
            this.Controls.Add(this.cbbScriptType);
            this.Controls.Add(this.label31);
            this.Controls.Add(this.label34);
            this.Controls.Add(this.rtxbScriptDesc);
            this.Name = "PanelScriptParam";
            this.Size = new System.Drawing.Size(451, 304);
            this.ResumeLayout(false);
            this.PerformLayout();

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
    }
}
