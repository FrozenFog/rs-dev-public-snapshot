namespace RelertSharp.GUI.SubWindows.LogicEditor
{
    partial class ParameterPanel
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
            this.mtxbEventID = new System.Windows.Forms.MaskedTextBox();
            this.gpbEventParam = new System.Windows.Forms.GroupBox();
            this.lklEP4 = new System.Windows.Forms.LinkLabel();
            this.lklEP3 = new System.Windows.Forms.LinkLabel();
            this.lklEP2 = new System.Windows.Forms.LinkLabel();
            this.lklEP1 = new System.Windows.Forms.LinkLabel();
            this.txbEP4 = new System.Windows.Forms.TextBox();
            this.txbEP3 = new System.Windows.Forms.TextBox();
            this.txbEP1 = new System.Windows.Forms.TextBox();
            this.txbEP2 = new System.Windows.Forms.TextBox();
            this.ckbEP4 = new System.Windows.Forms.CheckBox();
            this.ckbEP3 = new System.Windows.Forms.CheckBox();
            this.ckbEP2 = new System.Windows.Forms.CheckBox();
            this.ckbEP1 = new System.Windows.Forms.CheckBox();
            this.cbbEP4 = new System.Windows.Forms.ComboBox();
            this.cbbEP3 = new System.Windows.Forms.ComboBox();
            this.cbbEP2 = new System.Windows.Forms.ComboBox();
            this.cbbEP1 = new System.Windows.Forms.ComboBox();
            this.lblNoParamE = new System.Windows.Forms.Label();
            this.cbbEventAbst = new System.Windows.Forms.ComboBox();
            this.txbEventAnno = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.rtxbEventDetail = new System.Windows.Forms.RichTextBox();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.gpbEventParam.SuspendLayout();
            this.SuspendLayout();
            // 
            // mtxbEventID
            // 
            this.mtxbEventID.Location = new System.Drawing.Point(2, 16);
            this.mtxbEventID.Margin = new System.Windows.Forms.Padding(4);
            this.mtxbEventID.Mask = "00";
            this.mtxbEventID.Name = "mtxbEventID";
            this.mtxbEventID.PromptChar = ' ';
            this.mtxbEventID.Size = new System.Drawing.Size(41, 25);
            this.mtxbEventID.TabIndex = 12;
            this.mtxbEventID.ValidatingType = typeof(int);
            this.mtxbEventID.TextChanged += new System.EventHandler(this.mtxbEventID_Validated);
            // 
            // gpbEventParam
            // 
            this.gpbEventParam.Controls.Add(this.lklEP4);
            this.gpbEventParam.Controls.Add(this.lklEP3);
            this.gpbEventParam.Controls.Add(this.lklEP2);
            this.gpbEventParam.Controls.Add(this.lklEP1);
            this.gpbEventParam.Controls.Add(this.txbEP4);
            this.gpbEventParam.Controls.Add(this.txbEP3);
            this.gpbEventParam.Controls.Add(this.txbEP1);
            this.gpbEventParam.Controls.Add(this.txbEP2);
            this.gpbEventParam.Controls.Add(this.ckbEP4);
            this.gpbEventParam.Controls.Add(this.ckbEP3);
            this.gpbEventParam.Controls.Add(this.ckbEP2);
            this.gpbEventParam.Controls.Add(this.ckbEP1);
            this.gpbEventParam.Controls.Add(this.cbbEP4);
            this.gpbEventParam.Controls.Add(this.cbbEP3);
            this.gpbEventParam.Controls.Add(this.cbbEP2);
            this.gpbEventParam.Controls.Add(this.cbbEP1);
            this.gpbEventParam.Controls.Add(this.lblNoParamE);
            this.gpbEventParam.Location = new System.Drawing.Point(2, 245);
            this.gpbEventParam.Margin = new System.Windows.Forms.Padding(4);
            this.gpbEventParam.Name = "gpbEventParam";
            this.gpbEventParam.Padding = new System.Windows.Forms.Padding(4);
            this.gpbEventParam.Size = new System.Drawing.Size(393, 166);
            this.gpbEventParam.TabIndex = 13;
            this.gpbEventParam.TabStop = false;
            this.gpbEventParam.Text = "LGCgpbEventParam";
            // 
            // lklEP4
            // 
            this.lklEP4.AutoSize = true;
            this.lklEP4.Location = new System.Drawing.Point(16, 125);
            this.lklEP4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lklEP4.Name = "lklEP4";
            this.lklEP4.Size = new System.Drawing.Size(31, 15);
            this.lklEP4.TabIndex = 9;
            this.lklEP4.TabStop = true;
            this.lklEP4.Tag = "3";
            this.lklEP4.Text = "EP4";
            this.lklEP4.Visible = false;
            this.lklEP4.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lkl_LinkClicked);
            // 
            // lklEP3
            // 
            this.lklEP3.AutoSize = true;
            this.lklEP3.Location = new System.Drawing.Point(16, 92);
            this.lklEP3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lklEP3.Name = "lklEP3";
            this.lklEP3.Size = new System.Drawing.Size(31, 15);
            this.lklEP3.TabIndex = 9;
            this.lklEP3.TabStop = true;
            this.lklEP3.Tag = "2";
            this.lklEP3.Text = "EP3";
            this.lklEP3.Visible = false;
            this.lklEP3.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lkl_LinkClicked);
            // 
            // lklEP2
            // 
            this.lklEP2.AutoSize = true;
            this.lklEP2.Location = new System.Drawing.Point(16, 60);
            this.lklEP2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lklEP2.Name = "lklEP2";
            this.lklEP2.Size = new System.Drawing.Size(31, 15);
            this.lklEP2.TabIndex = 9;
            this.lklEP2.TabStop = true;
            this.lklEP2.Tag = "1";
            this.lklEP2.Text = "EP2";
            this.lklEP2.Visible = false;
            this.lklEP2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lkl_LinkClicked);
            // 
            // lklEP1
            // 
            this.lklEP1.AutoSize = true;
            this.lklEP1.Location = new System.Drawing.Point(16, 28);
            this.lklEP1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lklEP1.Name = "lklEP1";
            this.lklEP1.Size = new System.Drawing.Size(31, 15);
            this.lklEP1.TabIndex = 9;
            this.lklEP1.TabStop = true;
            this.lklEP1.Tag = "0";
            this.lklEP1.Text = "EP1";
            this.lklEP1.Visible = false;
            this.lklEP1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lkl_LinkClicked);
            // 
            // txbEP4
            // 
            this.txbEP4.Location = new System.Drawing.Point(181, 120);
            this.txbEP4.Margin = new System.Windows.Forms.Padding(4);
            this.txbEP4.Name = "txbEP4";
            this.txbEP4.Size = new System.Drawing.Size(203, 25);
            this.txbEP4.TabIndex = 4;
            this.txbEP4.Tag = "3";
            this.txbEP4.Visible = false;
            this.txbEP4.Validated += new System.EventHandler(this.ParamChanged);
            // 
            // txbEP3
            // 
            this.txbEP3.Location = new System.Drawing.Point(181, 89);
            this.txbEP3.Margin = new System.Windows.Forms.Padding(4);
            this.txbEP3.Name = "txbEP3";
            this.txbEP3.Size = new System.Drawing.Size(203, 25);
            this.txbEP3.TabIndex = 3;
            this.txbEP3.Tag = "2";
            this.txbEP3.Visible = false;
            this.txbEP3.Validated += new System.EventHandler(this.ParamChanged);
            // 
            // txbEP1
            // 
            this.txbEP1.Location = new System.Drawing.Point(181, 23);
            this.txbEP1.Margin = new System.Windows.Forms.Padding(4);
            this.txbEP1.Name = "txbEP1";
            this.txbEP1.Size = new System.Drawing.Size(203, 25);
            this.txbEP1.TabIndex = 1;
            this.txbEP1.Tag = "0";
            this.txbEP1.Visible = false;
            this.txbEP1.Validated += new System.EventHandler(this.ParamChanged);
            // 
            // txbEP2
            // 
            this.txbEP2.Location = new System.Drawing.Point(181, 55);
            this.txbEP2.Margin = new System.Windows.Forms.Padding(4);
            this.txbEP2.Name = "txbEP2";
            this.txbEP2.Size = new System.Drawing.Size(203, 25);
            this.txbEP2.TabIndex = 2;
            this.txbEP2.Tag = "1";
            this.txbEP2.Visible = false;
            this.txbEP2.Validated += new System.EventHandler(this.ParamChanged);
            // 
            // ckbEP4
            // 
            this.ckbEP4.AutoSize = true;
            this.ckbEP4.Location = new System.Drawing.Point(367, 125);
            this.ckbEP4.Margin = new System.Windows.Forms.Padding(4);
            this.ckbEP4.Name = "ckbEP4";
            this.ckbEP4.Size = new System.Drawing.Size(18, 17);
            this.ckbEP4.TabIndex = 4;
            this.ckbEP4.Tag = "3";
            this.ckbEP4.UseVisualStyleBackColor = true;
            this.ckbEP4.Visible = false;
            this.ckbEP4.CheckedChanged += new System.EventHandler(this.ParamChanged);
            // 
            // ckbEP3
            // 
            this.ckbEP3.AutoSize = true;
            this.ckbEP3.Location = new System.Drawing.Point(367, 92);
            this.ckbEP3.Margin = new System.Windows.Forms.Padding(4);
            this.ckbEP3.Name = "ckbEP3";
            this.ckbEP3.Size = new System.Drawing.Size(18, 17);
            this.ckbEP3.TabIndex = 3;
            this.ckbEP3.Tag = "2";
            this.ckbEP3.UseVisualStyleBackColor = true;
            this.ckbEP3.Visible = false;
            this.ckbEP3.CheckedChanged += new System.EventHandler(this.ParamChanged);
            // 
            // ckbEP2
            // 
            this.ckbEP2.AutoSize = true;
            this.ckbEP2.Location = new System.Drawing.Point(367, 60);
            this.ckbEP2.Margin = new System.Windows.Forms.Padding(4);
            this.ckbEP2.Name = "ckbEP2";
            this.ckbEP2.Size = new System.Drawing.Size(18, 17);
            this.ckbEP2.TabIndex = 2;
            this.ckbEP2.Tag = "1";
            this.ckbEP2.UseVisualStyleBackColor = true;
            this.ckbEP2.Visible = false;
            this.ckbEP2.CheckedChanged += new System.EventHandler(this.ParamChanged);
            // 
            // ckbEP1
            // 
            this.ckbEP1.AutoSize = true;
            this.ckbEP1.Location = new System.Drawing.Point(367, 28);
            this.ckbEP1.Margin = new System.Windows.Forms.Padding(4);
            this.ckbEP1.Name = "ckbEP1";
            this.ckbEP1.Size = new System.Drawing.Size(18, 17);
            this.ckbEP1.TabIndex = 1;
            this.ckbEP1.Tag = "0";
            this.ckbEP1.UseVisualStyleBackColor = true;
            this.ckbEP1.Visible = false;
            this.ckbEP1.CheckedChanged += new System.EventHandler(this.ParamChanged);
            // 
            // cbbEP4
            // 
            this.cbbEP4.FormattingEnabled = true;
            this.cbbEP4.Location = new System.Drawing.Point(181, 121);
            this.cbbEP4.Margin = new System.Windows.Forms.Padding(4);
            this.cbbEP4.Name = "cbbEP4";
            this.cbbEP4.Size = new System.Drawing.Size(203, 23);
            this.cbbEP4.TabIndex = 4;
            this.cbbEP4.Tag = "3";
            this.cbbEP4.Visible = false;
            this.cbbEP4.Validated += new System.EventHandler(this.ParamChanged);
            // 
            // cbbEP3
            // 
            this.cbbEP3.FormattingEnabled = true;
            this.cbbEP3.Location = new System.Drawing.Point(181, 89);
            this.cbbEP3.Margin = new System.Windows.Forms.Padding(4);
            this.cbbEP3.Name = "cbbEP3";
            this.cbbEP3.Size = new System.Drawing.Size(203, 23);
            this.cbbEP3.TabIndex = 3;
            this.cbbEP3.Tag = "2";
            this.cbbEP3.Visible = false;
            this.cbbEP3.Validated += new System.EventHandler(this.ParamChanged);
            // 
            // cbbEP2
            // 
            this.cbbEP2.FormattingEnabled = true;
            this.cbbEP2.Location = new System.Drawing.Point(181, 56);
            this.cbbEP2.Margin = new System.Windows.Forms.Padding(4);
            this.cbbEP2.Name = "cbbEP2";
            this.cbbEP2.Size = new System.Drawing.Size(203, 23);
            this.cbbEP2.TabIndex = 2;
            this.cbbEP2.Tag = "1";
            this.cbbEP2.Visible = false;
            this.cbbEP2.Validated += new System.EventHandler(this.ParamChanged);
            // 
            // cbbEP1
            // 
            this.cbbEP1.FormattingEnabled = true;
            this.cbbEP1.Location = new System.Drawing.Point(181, 24);
            this.cbbEP1.Margin = new System.Windows.Forms.Padding(4);
            this.cbbEP1.Name = "cbbEP1";
            this.cbbEP1.Size = new System.Drawing.Size(203, 23);
            this.cbbEP1.TabIndex = 1;
            this.cbbEP1.Tag = "0";
            this.cbbEP1.Visible = false;
            this.cbbEP1.Validated += new System.EventHandler(this.ParamChanged);
            // 
            // lblNoParamE
            // 
            this.lblNoParamE.AutoSize = true;
            this.lblNoParamE.Location = new System.Drawing.Point(95, 78);
            this.lblNoParamE.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNoParamE.Name = "lblNoParamE";
            this.lblNoParamE.Size = new System.Drawing.Size(111, 15);
            this.lblNoParamE.TabIndex = 0;
            this.lblNoParamE.Text = "LGClblNoParam";
            this.lblNoParamE.Visible = false;
            // 
            // cbbEventAbst
            // 
            this.cbbEventAbst.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbEventAbst.DropDownWidth = 300;
            this.cbbEventAbst.Font = new System.Drawing.Font("Verdana", 9F);
            this.cbbEventAbst.FormattingEnabled = true;
            this.cbbEventAbst.Location = new System.Drawing.Point(52, 16);
            this.cbbEventAbst.Margin = new System.Windows.Forms.Padding(4);
            this.cbbEventAbst.Name = "cbbEventAbst";
            this.cbbEventAbst.Size = new System.Drawing.Size(341, 26);
            this.cbbEventAbst.TabIndex = 19;
            this.cbbEventAbst.SelectedIndexChanged += new System.EventHandler(this.cbbEventAbst_SelectedIndexChanged);
            // 
            // txbEventAnno
            // 
            this.txbEventAnno.Location = new System.Drawing.Point(2, 67);
            this.txbEventAnno.Margin = new System.Windows.Forms.Padding(4);
            this.txbEventAnno.Name = "txbEventAnno";
            this.txbEventAnno.Size = new System.Drawing.Size(391, 25);
            this.txbEventAnno.TabIndex = 14;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(50, 0);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(127, 15);
            this.label14.TabIndex = 15;
            this.label14.Text = "LGClblEventAbst";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(2, 97);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(143, 15);
            this.label15.TabIndex = 16;
            this.label15.Text = "LGClblEventDetail";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(-1, 49);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(127, 15);
            this.label18.TabIndex = 17;
            this.label18.Text = "LGClblEventAnno";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(-1, 0);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(111, 15);
            this.label13.TabIndex = 18;
            this.label13.Text = "LGClblEventID";
            // 
            // rtxbEventDetail
            // 
            this.rtxbEventDetail.Font = new System.Drawing.Font("Verdana", 9F);
            this.rtxbEventDetail.Location = new System.Drawing.Point(2, 116);
            this.rtxbEventDetail.Margin = new System.Windows.Forms.Padding(4);
            this.rtxbEventDetail.Name = "rtxbEventDetail";
            this.rtxbEventDetail.ReadOnly = true;
            this.rtxbEventDetail.Size = new System.Drawing.Size(392, 120);
            this.rtxbEventDetail.TabIndex = 20;
            this.rtxbEventDetail.Text = "";
            // 
            // ParameterPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mtxbEventID);
            this.Controls.Add(this.gpbEventParam);
            this.Controls.Add(this.cbbEventAbst);
            this.Controls.Add(this.txbEventAnno);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.rtxbEventDetail);
            this.Name = "ParameterPanel";
            this.Size = new System.Drawing.Size(394, 411);
            this.gpbEventParam.ResumeLayout(false);
            this.gpbEventParam.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MaskedTextBox mtxbEventID;
        private System.Windows.Forms.GroupBox gpbEventParam;
        private System.Windows.Forms.LinkLabel lklEP4;
        private System.Windows.Forms.LinkLabel lklEP3;
        private System.Windows.Forms.LinkLabel lklEP2;
        private System.Windows.Forms.LinkLabel lklEP1;
        private System.Windows.Forms.TextBox txbEP4;
        private System.Windows.Forms.TextBox txbEP3;
        private System.Windows.Forms.TextBox txbEP1;
        private System.Windows.Forms.TextBox txbEP2;
        private System.Windows.Forms.CheckBox ckbEP4;
        private System.Windows.Forms.CheckBox ckbEP3;
        private System.Windows.Forms.CheckBox ckbEP2;
        private System.Windows.Forms.CheckBox ckbEP1;
        private System.Windows.Forms.ComboBox cbbEP4;
        private System.Windows.Forms.ComboBox cbbEP3;
        private System.Windows.Forms.ComboBox cbbEP2;
        private System.Windows.Forms.ComboBox cbbEP1;
        private System.Windows.Forms.Label lblNoParamE;
        private System.Windows.Forms.ComboBox cbbEventAbst;
        private System.Windows.Forms.TextBox txbEventAnno;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.RichTextBox rtxbEventDetail;
        private System.Windows.Forms.ToolTip toolTip;
    }
}
