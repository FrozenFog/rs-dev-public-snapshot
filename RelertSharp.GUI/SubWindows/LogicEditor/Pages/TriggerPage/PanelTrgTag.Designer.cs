namespace RelertSharp.GUI.SubWindows.LogicEditor
{
    partial class PanelTrgTag
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
            this.gpbTag = new System.Windows.Forms.GroupBox();
            this.cbbTagID = new System.Windows.Forms.ComboBox();
            this.lklTraceTrigger = new System.Windows.Forms.LinkLabel();
            this.ckbHard = new System.Windows.Forms.CheckBox();
            this.ckbNormal = new System.Windows.Forms.CheckBox();
            this.ckbEasy = new System.Windows.Forms.CheckBox();
            this.cbbCustomGroup = new System.Windows.Forms.ComboBox();
            this.cbbAttatchedTrg = new System.Windows.Forms.ComboBox();
            this.ckbDisabled = new System.Windows.Forms.CheckBox();
            this.lbxTriggerHouses = new System.Windows.Forms.ListBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.gpbRepeat = new System.Windows.Forms.GroupBox();
            this.rdbRepeat2 = new System.Windows.Forms.RadioButton();
            this.rdbRepeat1 = new System.Windows.Forms.RadioButton();
            this.rdbRepeat0 = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txbTrgID = new System.Windows.Forms.TextBox();
            this.txbTagName = new System.Windows.Forms.TextBox();
            this.txbTrgName = new System.Windows.Forms.TextBox();
            this.btnNewTrigger = new System.Windows.Forms.Button();
            this.cmsEditTemplate = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiEditTemp = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSaveTemp = new System.Windows.Forms.Button();
            this.btnCopyTrigger = new System.Windows.Forms.Button();
            this.btnDelTrigger = new System.Windows.Forms.Button();
            this.gpbTag.SuspendLayout();
            this.gpbRepeat.SuspendLayout();
            this.cmsEditTemplate.SuspendLayout();
            this.SuspendLayout();
            // 
            // gpbTag
            // 
            this.gpbTag.Controls.Add(this.cbbTagID);
            this.gpbTag.Controls.Add(this.lklTraceTrigger);
            this.gpbTag.Controls.Add(this.ckbHard);
            this.gpbTag.Controls.Add(this.ckbNormal);
            this.gpbTag.Controls.Add(this.ckbEasy);
            this.gpbTag.Controls.Add(this.cbbCustomGroup);
            this.gpbTag.Controls.Add(this.cbbAttatchedTrg);
            this.gpbTag.Controls.Add(this.ckbDisabled);
            this.gpbTag.Controls.Add(this.lbxTriggerHouses);
            this.gpbTag.Controls.Add(this.label10);
            this.gpbTag.Controls.Add(this.label9);
            this.gpbTag.Controls.Add(this.label8);
            this.gpbTag.Controls.Add(this.gpbRepeat);
            this.gpbTag.Controls.Add(this.label6);
            this.gpbTag.Controls.Add(this.label4);
            this.gpbTag.Controls.Add(this.label7);
            this.gpbTag.Controls.Add(this.label3);
            this.gpbTag.Controls.Add(this.label5);
            this.gpbTag.Controls.Add(this.label2);
            this.gpbTag.Controls.Add(this.txbTrgID);
            this.gpbTag.Controls.Add(this.txbTagName);
            this.gpbTag.Controls.Add(this.txbTrgName);
            this.gpbTag.Controls.Add(this.btnNewTrigger);
            this.gpbTag.Controls.Add(this.btnSaveTemp);
            this.gpbTag.Controls.Add(this.btnCopyTrigger);
            this.gpbTag.Controls.Add(this.btnDelTrigger);
            this.gpbTag.Location = new System.Drawing.Point(0, 0);
            this.gpbTag.Margin = new System.Windows.Forms.Padding(4);
            this.gpbTag.Name = "gpbTag";
            this.gpbTag.Padding = new System.Windows.Forms.Padding(4);
            this.gpbTag.Size = new System.Drawing.Size(1223, 229);
            this.gpbTag.TabIndex = 5;
            this.gpbTag.TabStop = false;
            this.gpbTag.Text = "LGCgpbTrgTag";
            // 
            // cbbTagID
            // 
            this.cbbTagID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbTagID.FormattingEnabled = true;
            this.cbbTagID.Location = new System.Drawing.Point(17, 151);
            this.cbbTagID.Margin = new System.Windows.Forms.Padding(4);
            this.cbbTagID.Name = "cbbTagID";
            this.cbbTagID.Size = new System.Drawing.Size(115, 23);
            this.cbbTagID.TabIndex = 17;
            this.cbbTagID.SelectedIndexChanged += new System.EventHandler(this.cbbTagID_SelectedIndexChanged);
            // 
            // lklTraceTrigger
            // 
            this.lklTraceTrigger.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lklTraceTrigger.AutoSize = true;
            this.lklTraceTrigger.Enabled = false;
            this.lklTraceTrigger.Location = new System.Drawing.Point(1119, 25);
            this.lklTraceTrigger.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lklTraceTrigger.Name = "lklTraceTrigger";
            this.lklTraceTrigger.Size = new System.Drawing.Size(95, 15);
            this.lklTraceTrigger.TabIndex = 16;
            this.lklTraceTrigger.TabStop = true;
            this.lklTraceTrigger.Text = "LGClklTrace";
            this.lklTraceTrigger.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lklTraceTrigger.Click += new System.EventHandler(this.lklTraceTrigger_Click);
            // 
            // ckbHard
            // 
            this.ckbHard.AutoSize = true;
            this.ckbHard.Location = new System.Drawing.Point(204, 194);
            this.ckbHard.Margin = new System.Windows.Forms.Padding(4);
            this.ckbHard.Name = "ckbHard";
            this.ckbHard.Size = new System.Drawing.Size(109, 19);
            this.ckbHard.TabIndex = 15;
            this.ckbHard.Tag = "h";
            this.ckbHard.Text = "LGCckbHard";
            this.ckbHard.UseVisualStyleBackColor = true;
            this.ckbHard.CheckedChanged += new System.EventHandler(this.DiffRepeatCheckChanged);
            // 
            // ckbNormal
            // 
            this.ckbNormal.AutoSize = true;
            this.ckbNormal.Location = new System.Drawing.Point(111, 194);
            this.ckbNormal.Margin = new System.Windows.Forms.Padding(4);
            this.ckbNormal.Name = "ckbNormal";
            this.ckbNormal.Size = new System.Drawing.Size(125, 19);
            this.ckbNormal.TabIndex = 15;
            this.ckbNormal.Tag = "n";
            this.ckbNormal.Text = "LGCckbNormal";
            this.ckbNormal.UseVisualStyleBackColor = true;
            this.ckbNormal.CheckedChanged += new System.EventHandler(this.DiffRepeatCheckChanged);
            // 
            // ckbEasy
            // 
            this.ckbEasy.AutoSize = true;
            this.ckbEasy.Location = new System.Drawing.Point(17, 194);
            this.ckbEasy.Margin = new System.Windows.Forms.Padding(4);
            this.ckbEasy.Name = "ckbEasy";
            this.ckbEasy.Size = new System.Drawing.Size(109, 19);
            this.ckbEasy.TabIndex = 15;
            this.ckbEasy.Tag = "e";
            this.ckbEasy.Text = "LGCckbEasy";
            this.ckbEasy.UseVisualStyleBackColor = true;
            this.ckbEasy.CheckedChanged += new System.EventHandler(this.DiffRepeatCheckChanged);
            // 
            // cbbCustomGroup
            // 
            this.cbbCustomGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbbCustomGroup.FormattingEnabled = true;
            this.cbbCustomGroup.Location = new System.Drawing.Point(872, 112);
            this.cbbCustomGroup.Margin = new System.Windows.Forms.Padding(4);
            this.cbbCustomGroup.Name = "cbbCustomGroup";
            this.cbbCustomGroup.Size = new System.Drawing.Size(340, 23);
            this.cbbCustomGroup.TabIndex = 14;
            // 
            // cbbAttatchedTrg
            // 
            this.cbbAttatchedTrg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbbAttatchedTrg.FormattingEnabled = true;
            this.cbbAttatchedTrg.Location = new System.Drawing.Point(872, 45);
            this.cbbAttatchedTrg.Margin = new System.Windows.Forms.Padding(4);
            this.cbbAttatchedTrg.Name = "cbbAttatchedTrg";
            this.cbbAttatchedTrg.Size = new System.Drawing.Size(341, 23);
            this.cbbAttatchedTrg.TabIndex = 13;
            this.cbbAttatchedTrg.SelectedIndexChanged += new System.EventHandler(this.cbbAttatchedTrg_SelectedIndexChanged);
            // 
            // ckbDisabled
            // 
            this.ckbDisabled.AutoSize = true;
            this.ckbDisabled.Location = new System.Drawing.Point(384, 194);
            this.ckbDisabled.Margin = new System.Windows.Forms.Padding(4);
            this.ckbDisabled.Name = "ckbDisabled";
            this.ckbDisabled.Size = new System.Drawing.Size(141, 19);
            this.ckbDisabled.TabIndex = 12;
            this.ckbDisabled.Tag = "d";
            this.ckbDisabled.Text = "LGCckbDisabled";
            this.ckbDisabled.UseVisualStyleBackColor = true;
            this.ckbDisabled.CheckedChanged += new System.EventHandler(this.DiffRepeatCheckChanged);
            // 
            // lbxTriggerHouses
            // 
            this.lbxTriggerHouses.FormattingEnabled = true;
            this.lbxTriggerHouses.ItemHeight = 15;
            this.lbxTriggerHouses.Location = new System.Drawing.Point(656, 44);
            this.lbxTriggerHouses.Margin = new System.Windows.Forms.Padding(4);
            this.lbxTriggerHouses.Name = "lbxTriggerHouses";
            this.lbxTriggerHouses.Size = new System.Drawing.Size(207, 169);
            this.lbxTriggerHouses.TabIndex = 11;
            this.lbxTriggerHouses.SelectedIndexChanged += new System.EventHandler(this.lbxTriggerHouses_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(869, 90);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(143, 15);
            this.label10.TabIndex = 10;
            this.label10.Text = "LGClblCustomGroup";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(869, 25);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(103, 15);
            this.label9.TabIndex = 10;
            this.label9.Text = "LGClblAttTrg";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(653, 25);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(127, 15);
            this.label8.TabIndex = 10;
            this.label8.Text = "LGClblTrgHouses";
            // 
            // gpbRepeat
            // 
            this.gpbRepeat.Controls.Add(this.rdbRepeat2);
            this.gpbRepeat.Controls.Add(this.rdbRepeat1);
            this.gpbRepeat.Controls.Add(this.rdbRepeat0);
            this.gpbRepeat.Location = new System.Drawing.Point(495, 25);
            this.gpbRepeat.Margin = new System.Windows.Forms.Padding(4);
            this.gpbRepeat.Name = "gpbRepeat";
            this.gpbRepeat.Padding = new System.Windows.Forms.Padding(4);
            this.gpbRepeat.Size = new System.Drawing.Size(153, 189);
            this.gpbRepeat.TabIndex = 9;
            this.gpbRepeat.TabStop = false;
            this.gpbRepeat.Text = "LGCgpbRepeat";
            // 
            // rdbRepeat2
            // 
            this.rdbRepeat2.AutoSize = true;
            this.rdbRepeat2.Location = new System.Drawing.Point(11, 140);
            this.rdbRepeat2.Margin = new System.Windows.Forms.Padding(4);
            this.rdbRepeat2.Name = "rdbRepeat2";
            this.rdbRepeat2.Size = new System.Drawing.Size(108, 19);
            this.rdbRepeat2.TabIndex = 0;
            this.rdbRepeat2.Tag = "2";
            this.rdbRepeat2.Text = "LGCrdbRep2";
            this.rdbRepeat2.UseVisualStyleBackColor = true;
            this.rdbRepeat2.CheckedChanged += new System.EventHandler(this.DiffRepeatCheckChanged);
            // 
            // rdbRepeat1
            // 
            this.rdbRepeat1.AutoSize = true;
            this.rdbRepeat1.Location = new System.Drawing.Point(11, 89);
            this.rdbRepeat1.Margin = new System.Windows.Forms.Padding(4);
            this.rdbRepeat1.Name = "rdbRepeat1";
            this.rdbRepeat1.Size = new System.Drawing.Size(108, 19);
            this.rdbRepeat1.TabIndex = 0;
            this.rdbRepeat1.Tag = "1";
            this.rdbRepeat1.Text = "LGCrdbRep1";
            this.rdbRepeat1.UseVisualStyleBackColor = true;
            this.rdbRepeat1.CheckedChanged += new System.EventHandler(this.DiffRepeatCheckChanged);
            // 
            // rdbRepeat0
            // 
            this.rdbRepeat0.AutoSize = true;
            this.rdbRepeat0.Checked = true;
            this.rdbRepeat0.Location = new System.Drawing.Point(11, 38);
            this.rdbRepeat0.Margin = new System.Windows.Forms.Padding(4);
            this.rdbRepeat0.Name = "rdbRepeat0";
            this.rdbRepeat0.Size = new System.Drawing.Size(108, 19);
            this.rdbRepeat0.TabIndex = 0;
            this.rdbRepeat0.TabStop = true;
            this.rdbRepeat0.Tag = "0";
            this.rdbRepeat0.Text = "LGCrdbRep0";
            this.rdbRepeat0.UseVisualStyleBackColor = true;
            this.rdbRepeat0.CheckedChanged += new System.EventHandler(this.DiffRepeatCheckChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(141, 155);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(15, 15);
            this.label6.TabIndex = 8;
            this.label6.Text = "-";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(141, 90);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(15, 15);
            this.label4.TabIndex = 8;
            this.label4.Text = "-";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(161, 130);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(111, 15);
            this.label7.TabIndex = 7;
            this.label7.Text = "LGClblTagName";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(161, 68);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 15);
            this.label3.TabIndex = 7;
            this.label3.Text = "LGClblTrgName";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 130);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(95, 15);
            this.label5.TabIndex = 7;
            this.label5.Text = "LGClblTagID";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 68);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 15);
            this.label2.TabIndex = 7;
            this.label2.Text = "LGClblTrgID";
            // 
            // txbTrgID
            // 
            this.txbTrgID.Location = new System.Drawing.Point(17, 86);
            this.txbTrgID.Margin = new System.Windows.Forms.Padding(4);
            this.txbTrgID.Name = "txbTrgID";
            this.txbTrgID.ReadOnly = true;
            this.txbTrgID.Size = new System.Drawing.Size(115, 25);
            this.txbTrgID.TabIndex = 6;
            // 
            // txbTagName
            // 
            this.txbTagName.Location = new System.Drawing.Point(164, 151);
            this.txbTagName.Margin = new System.Windows.Forms.Padding(4);
            this.txbTagName.Name = "txbTagName";
            this.txbTagName.ReadOnly = true;
            this.txbTagName.Size = new System.Drawing.Size(321, 25);
            this.txbTagName.TabIndex = 5;
            // 
            // txbTrgName
            // 
            this.txbTrgName.Location = new System.Drawing.Point(164, 86);
            this.txbTrgName.Margin = new System.Windows.Forms.Padding(4);
            this.txbTrgName.Name = "txbTrgName";
            this.txbTrgName.Size = new System.Drawing.Size(321, 25);
            this.txbTrgName.TabIndex = 5;
            this.txbTrgName.Validated += new System.EventHandler(this.txbTrgName_Validated);
            // 
            // btnNewTrigger
            // 
            this.btnNewTrigger.ContextMenuStrip = this.cmsEditTemplate;
            this.btnNewTrigger.Location = new System.Drawing.Point(17, 25);
            this.btnNewTrigger.Margin = new System.Windows.Forms.Padding(4);
            this.btnNewTrigger.Name = "btnNewTrigger";
            this.btnNewTrigger.Size = new System.Drawing.Size(143, 29);
            this.btnNewTrigger.TabIndex = 3;
            this.btnNewTrigger.Text = "LGCbtnNewTrg";
            this.btnNewTrigger.UseVisualStyleBackColor = true;
            this.btnNewTrigger.Click += new System.EventHandler(this.btnNewTrigger_Click);
            // 
            // cmsEditTemplate
            // 
            this.cmsEditTemplate.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmsEditTemplate.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiEditTemp});
            this.cmsEditTemplate.Name = "cmsEditTemplate";
            this.cmsEditTemplate.Size = new System.Drawing.Size(209, 28);
            // 
            // tsmiEditTemp
            // 
            this.tsmiEditTemp.Name = "tsmiEditTemp";
            this.tsmiEditTemp.Size = new System.Drawing.Size(208, 24);
            this.tsmiEditTemp.Text = "LGCtsmiEditTemp";
            this.tsmiEditTemp.Click += new System.EventHandler(this.tsmiEditTemp_Click);
            // 
            // btnSaveTemp
            // 
            this.btnSaveTemp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveTemp.Location = new System.Drawing.Point(872, 165);
            this.btnSaveTemp.Margin = new System.Windows.Forms.Padding(4);
            this.btnSaveTemp.Name = "btnSaveTemp";
            this.btnSaveTemp.Size = new System.Drawing.Size(133, 29);
            this.btnSaveTemp.TabIndex = 3;
            this.btnSaveTemp.Text = "LGCbtnSaveTemplate";
            this.btnSaveTemp.UseVisualStyleBackColor = true;
            this.btnSaveTemp.Visible = false;
            this.btnSaveTemp.Click += new System.EventHandler(this.btnSaveTemp_Click);
            // 
            // btnCopyTrigger
            // 
            this.btnCopyTrigger.Location = new System.Drawing.Point(344, 25);
            this.btnCopyTrigger.Margin = new System.Windows.Forms.Padding(4);
            this.btnCopyTrigger.Name = "btnCopyTrigger";
            this.btnCopyTrigger.Size = new System.Drawing.Size(143, 29);
            this.btnCopyTrigger.TabIndex = 3;
            this.btnCopyTrigger.Text = "LGCbtnCopyTrg";
            this.btnCopyTrigger.UseVisualStyleBackColor = true;
            this.btnCopyTrigger.Click += new System.EventHandler(this.btnCopyTrigger_Click);
            // 
            // btnDelTrigger
            // 
            this.btnDelTrigger.Location = new System.Drawing.Point(184, 25);
            this.btnDelTrigger.Margin = new System.Windows.Forms.Padding(4);
            this.btnDelTrigger.Name = "btnDelTrigger";
            this.btnDelTrigger.Size = new System.Drawing.Size(143, 29);
            this.btnDelTrigger.TabIndex = 3;
            this.btnDelTrigger.Text = "LGCbtnDelTrg";
            this.btnDelTrigger.UseVisualStyleBackColor = true;
            this.btnDelTrigger.Click += new System.EventHandler(this.btnDelTrigger_Click);
            // 
            // PanelTrgTag
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gpbTag);
            this.Name = "PanelTrgTag";
            this.Size = new System.Drawing.Size(1223, 229);
            this.gpbTag.ResumeLayout(false);
            this.gpbTag.PerformLayout();
            this.gpbRepeat.ResumeLayout(false);
            this.gpbRepeat.PerformLayout();
            this.cmsEditTemplate.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gpbTag;
        private System.Windows.Forms.ComboBox cbbTagID;
        private System.Windows.Forms.LinkLabel lklTraceTrigger;
        private System.Windows.Forms.CheckBox ckbHard;
        private System.Windows.Forms.CheckBox ckbNormal;
        private System.Windows.Forms.CheckBox ckbEasy;
        private System.Windows.Forms.ComboBox cbbCustomGroup;
        private System.Windows.Forms.ComboBox cbbAttatchedTrg;
        private System.Windows.Forms.CheckBox ckbDisabled;
        private System.Windows.Forms.ListBox lbxTriggerHouses;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox gpbRepeat;
        private System.Windows.Forms.RadioButton rdbRepeat2;
        private System.Windows.Forms.RadioButton rdbRepeat1;
        private System.Windows.Forms.RadioButton rdbRepeat0;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txbTrgID;
        private System.Windows.Forms.TextBox txbTagName;
        private System.Windows.Forms.TextBox txbTrgName;
        private System.Windows.Forms.Button btnNewTrigger;
        private System.Windows.Forms.Button btnSaveTemp;
        private System.Windows.Forms.Button btnCopyTrigger;
        private System.Windows.Forms.Button btnDelTrigger;
        private System.Windows.Forms.ContextMenuStrip cmsEditTemplate;
        private System.Windows.Forms.ToolStripMenuItem tsmiEditTemp;
    }
}
