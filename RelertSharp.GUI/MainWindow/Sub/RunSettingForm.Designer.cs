namespace RelertSharp.GUI
{
    partial class RunSettingForm
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
            this.rdbSingle = new System.Windows.Forms.RadioButton();
            this.rdbSkirmish = new System.Windows.Forms.RadioButton();
            this.gpbGameMode = new System.Windows.Forms.GroupBox();
            this.gpbSkirmishSetting = new System.Windows.Forms.GroupBox();
            this.trkbGameSpeed = new System.Windows.Forms.TrackBar();
            this.mtxbUnitNum = new System.Windows.Forms.MaskedTextBox();
            this.mtxbMoney = new System.Windows.Forms.MaskedTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ckbMcvRe = new System.Windows.Forms.CheckBox();
            this.ckbSuper = new System.Windows.Forms.CheckBox();
            this.ckbAlly = new System.Windows.Forms.CheckBox();
            this.ckbCrates = new System.Windows.Forms.CheckBox();
            this.ckbBudAlly = new System.Windows.Forms.CheckBox();
            this.ckbShortGame = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txbExeName = new System.Windows.Forms.TextBox();
            this.btnRun = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.trkbSingleDiff = new System.Windows.Forms.TrackBar();
            this.label5 = new System.Windows.Forms.Label();
            this.rdbSyr = new System.Windows.Forms.RadioButton();
            this.rdbCncYr = new System.Windows.Forms.RadioButton();
            this.gpbGameMode.SuspendLayout();
            this.gpbSkirmishSetting.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trkbGameSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkbSingleDiff)).BeginInit();
            this.SuspendLayout();
            // 
            // rdbSingle
            // 
            this.rdbSingle.AutoSize = true;
            this.rdbSingle.Location = new System.Drawing.Point(12, 24);
            this.rdbSingle.Name = "rdbSingle";
            this.rdbSingle.Size = new System.Drawing.Size(132, 19);
            this.rdbSingle.TabIndex = 0;
            this.rdbSingle.Tag = "0";
            this.rdbSingle.Text = "Single Player";
            this.rdbSingle.UseVisualStyleBackColor = true;
            this.rdbSingle.CheckedChanged += new System.EventHandler(this.rdbSingle_CheckedChanged);
            // 
            // rdbSkirmish
            // 
            this.rdbSkirmish.AutoSize = true;
            this.rdbSkirmish.Checked = true;
            this.rdbSkirmish.Location = new System.Drawing.Point(12, 49);
            this.rdbSkirmish.Name = "rdbSkirmish";
            this.rdbSkirmish.Size = new System.Drawing.Size(92, 19);
            this.rdbSkirmish.TabIndex = 0;
            this.rdbSkirmish.TabStop = true;
            this.rdbSkirmish.Tag = "1";
            this.rdbSkirmish.Text = "Skirmish";
            this.rdbSkirmish.UseVisualStyleBackColor = true;
            this.rdbSkirmish.CheckedChanged += new System.EventHandler(this.rdbSingle_CheckedChanged);
            // 
            // gpbGameMode
            // 
            this.gpbGameMode.Controls.Add(this.rdbSingle);
            this.gpbGameMode.Controls.Add(this.rdbSkirmish);
            this.gpbGameMode.Location = new System.Drawing.Point(26, 27);
            this.gpbGameMode.Name = "gpbGameMode";
            this.gpbGameMode.Size = new System.Drawing.Size(168, 72);
            this.gpbGameMode.TabIndex = 1;
            this.gpbGameMode.TabStop = false;
            this.gpbGameMode.Text = "Game mode";
            // 
            // gpbSkirmishSetting
            // 
            this.gpbSkirmishSetting.Controls.Add(this.trkbGameSpeed);
            this.gpbSkirmishSetting.Controls.Add(this.mtxbUnitNum);
            this.gpbSkirmishSetting.Controls.Add(this.mtxbMoney);
            this.gpbSkirmishSetting.Controls.Add(this.label4);
            this.gpbSkirmishSetting.Controls.Add(this.label3);
            this.gpbSkirmishSetting.Controls.Add(this.label2);
            this.gpbSkirmishSetting.Controls.Add(this.ckbMcvRe);
            this.gpbSkirmishSetting.Controls.Add(this.ckbSuper);
            this.gpbSkirmishSetting.Controls.Add(this.ckbAlly);
            this.gpbSkirmishSetting.Controls.Add(this.ckbCrates);
            this.gpbSkirmishSetting.Controls.Add(this.ckbBudAlly);
            this.gpbSkirmishSetting.Controls.Add(this.ckbShortGame);
            this.gpbSkirmishSetting.Location = new System.Drawing.Point(204, 27);
            this.gpbSkirmishSetting.Name = "gpbSkirmishSetting";
            this.gpbSkirmishSetting.Size = new System.Drawing.Size(351, 189);
            this.gpbSkirmishSetting.TabIndex = 2;
            this.gpbSkirmishSetting.TabStop = false;
            this.gpbSkirmishSetting.Text = "Skirmish Setting";
            // 
            // trkbGameSpeed
            // 
            this.trkbGameSpeed.AutoSize = false;
            this.trkbGameSpeed.LargeChange = 2;
            this.trkbGameSpeed.Location = new System.Drawing.Point(234, 153);
            this.trkbGameSpeed.Maximum = 6;
            this.trkbGameSpeed.Name = "trkbGameSpeed";
            this.trkbGameSpeed.Size = new System.Drawing.Size(100, 25);
            this.trkbGameSpeed.TabIndex = 3;
            this.trkbGameSpeed.Value = 4;
            this.trkbGameSpeed.Scroll += new System.EventHandler(this.trkbGameSpeed_Scroll);
            // 
            // mtxbUnitNum
            // 
            this.mtxbUnitNum.Location = new System.Drawing.Point(128, 153);
            this.mtxbUnitNum.Mask = "00";
            this.mtxbUnitNum.Name = "mtxbUnitNum";
            this.mtxbUnitNum.PromptChar = ' ';
            this.mtxbUnitNum.Size = new System.Drawing.Size(100, 25);
            this.mtxbUnitNum.TabIndex = 2;
            this.mtxbUnitNum.ValidatingType = typeof(int);
            // 
            // mtxbMoney
            // 
            this.mtxbMoney.Location = new System.Drawing.Point(22, 153);
            this.mtxbMoney.Mask = "000000";
            this.mtxbMoney.Name = "mtxbMoney";
            this.mtxbMoney.PromptChar = ' ';
            this.mtxbMoney.Size = new System.Drawing.Size(100, 25);
            this.mtxbMoney.TabIndex = 2;
            this.mtxbMoney.ValidatingType = typeof(int);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(125, 135);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 15);
            this.label4.TabIndex = 1;
            this.label4.Text = "Units begin";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(231, 135);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 15);
            this.label3.TabIndex = 1;
            this.label3.Text = "Game speed";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 135);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Money";
            // 
            // ckbMcvRe
            // 
            this.ckbMcvRe.AutoSize = true;
            this.ckbMcvRe.Checked = true;
            this.ckbMcvRe.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbMcvRe.Location = new System.Drawing.Point(22, 100);
            this.ckbMcvRe.Name = "ckbMcvRe";
            this.ckbMcvRe.Size = new System.Drawing.Size(125, 19);
            this.ckbMcvRe.TabIndex = 0;
            this.ckbMcvRe.Text = "MCV redeploy";
            this.ckbMcvRe.UseVisualStyleBackColor = true;
            // 
            // ckbSuper
            // 
            this.ckbSuper.AutoSize = true;
            this.ckbSuper.Checked = true;
            this.ckbSuper.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbSuper.Location = new System.Drawing.Point(22, 67);
            this.ckbSuper.Name = "ckbSuper";
            this.ckbSuper.Size = new System.Drawing.Size(125, 19);
            this.ckbSuper.TabIndex = 0;
            this.ckbSuper.Text = "Superweapons";
            this.ckbSuper.UseVisualStyleBackColor = true;
            // 
            // ckbAlly
            // 
            this.ckbAlly.AutoSize = true;
            this.ckbAlly.Checked = true;
            this.ckbAlly.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbAlly.Location = new System.Drawing.Point(185, 102);
            this.ckbAlly.Name = "ckbAlly";
            this.ckbAlly.Size = new System.Drawing.Size(141, 19);
            this.ckbAlly.TabIndex = 0;
            this.ckbAlly.Text = "Allies Allowed";
            this.ckbAlly.UseVisualStyleBackColor = true;
            // 
            // ckbCrates
            // 
            this.ckbCrates.AutoSize = true;
            this.ckbCrates.Location = new System.Drawing.Point(185, 67);
            this.ckbCrates.Name = "ckbCrates";
            this.ckbCrates.Size = new System.Drawing.Size(133, 19);
            this.ckbCrates.TabIndex = 0;
            this.ckbCrates.Text = "Crates appear";
            this.ckbCrates.UseVisualStyleBackColor = true;
            // 
            // ckbBudAlly
            // 
            this.ckbBudAlly.AutoSize = true;
            this.ckbBudAlly.Checked = true;
            this.ckbBudAlly.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbBudAlly.Location = new System.Drawing.Point(185, 34);
            this.ckbBudAlly.Name = "ckbBudAlly";
            this.ckbBudAlly.Size = new System.Drawing.Size(141, 19);
            this.ckbBudAlly.TabIndex = 0;
            this.ckbBudAlly.Text = "Build off ally";
            this.ckbBudAlly.UseVisualStyleBackColor = true;
            // 
            // ckbShortGame
            // 
            this.ckbShortGame.AutoSize = true;
            this.ckbShortGame.Checked = true;
            this.ckbShortGame.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbShortGame.Location = new System.Drawing.Point(22, 34);
            this.ckbShortGame.Name = "ckbShortGame";
            this.ckbShortGame.Size = new System.Drawing.Size(109, 19);
            this.ckbShortGame.TabIndex = 0;
            this.ckbShortGame.Text = "Short game";
            this.ckbShortGame.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 148);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "Game Exe Name";
            // 
            // txbExeName
            // 
            this.txbExeName.Location = new System.Drawing.Point(26, 166);
            this.txbExeName.Name = "txbExeName";
            this.txbExeName.Size = new System.Drawing.Size(168, 25);
            this.txbExeName.TabIndex = 4;
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(101, 236);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(97, 29);
            this.btnRun.TabIndex = 6;
            this.btnRun.Text = "Run Game";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(327, 236);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(97, 29);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // trkbSingleDiff
            // 
            this.trkbSingleDiff.AutoSize = false;
            this.trkbSingleDiff.Enabled = false;
            this.trkbSingleDiff.LargeChange = 1;
            this.trkbSingleDiff.Location = new System.Drawing.Point(26, 120);
            this.trkbSingleDiff.Maximum = 2;
            this.trkbSingleDiff.Name = "trkbSingleDiff";
            this.trkbSingleDiff.Size = new System.Drawing.Size(168, 25);
            this.trkbSingleDiff.TabIndex = 3;
            this.trkbSingleDiff.Value = 2;
            this.trkbSingleDiff.Scroll += new System.EventHandler(this.trkbSingleDiff_Scroll);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(23, 102);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(143, 15);
            this.label5.TabIndex = 1;
            this.label5.Text = "Single Difficulty";
            // 
            // rdbSyr
            // 
            this.rdbSyr.AutoSize = true;
            this.rdbSyr.Checked = true;
            this.rdbSyr.Location = new System.Drawing.Point(26, 197);
            this.rdbSyr.Name = "rdbSyr";
            this.rdbSyr.Size = new System.Drawing.Size(84, 19);
            this.rdbSyr.TabIndex = 0;
            this.rdbSyr.TabStop = true;
            this.rdbSyr.Tag = "0";
            this.rdbSyr.Text = "Syringe";
            this.rdbSyr.UseVisualStyleBackColor = true;
            // 
            // rdbCncYr
            // 
            this.rdbCncYr.AutoSize = true;
            this.rdbCncYr.Location = new System.Drawing.Point(116, 197);
            this.rdbCncYr.Name = "rdbCncYr";
            this.rdbCncYr.Size = new System.Drawing.Size(76, 19);
            this.rdbCncYr.TabIndex = 0;
            this.rdbCncYr.Tag = "0";
            this.rdbCncYr.Text = "YR Inj";
            this.rdbCncYr.UseVisualStyleBackColor = true;
            // 
            // RunSettingForm
            // 
            this.AcceptButton = this.btnRun;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(590, 279);
            this.Controls.Add(this.rdbCncYr);
            this.Controls.Add(this.rdbSyr);
            this.Controls.Add(this.trkbSingleDiff);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txbExeName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gpbSkirmishSetting);
            this.Controls.Add(this.gpbGameMode);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RunSettingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "RunSettingForm";
            this.gpbGameMode.ResumeLayout(false);
            this.gpbGameMode.PerformLayout();
            this.gpbSkirmishSetting.ResumeLayout(false);
            this.gpbSkirmishSetting.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trkbGameSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkbSingleDiff)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rdbSingle;
        private System.Windows.Forms.RadioButton rdbSkirmish;
        private System.Windows.Forms.GroupBox gpbGameMode;
        private System.Windows.Forms.GroupBox gpbSkirmishSetting;
        private System.Windows.Forms.CheckBox ckbMcvRe;
        private System.Windows.Forms.CheckBox ckbSuper;
        private System.Windows.Forms.CheckBox ckbShortGame;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txbExeName;
        private System.Windows.Forms.TrackBar trkbGameSpeed;
        private System.Windows.Forms.MaskedTextBox mtxbUnitNum;
        private System.Windows.Forms.MaskedTextBox mtxbMoney;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox ckbAlly;
        private System.Windows.Forms.CheckBox ckbCrates;
        private System.Windows.Forms.CheckBox ckbBudAlly;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.TrackBar trkbSingleDiff;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton rdbSyr;
        private System.Windows.Forms.RadioButton rdbCncYr;
    }
}