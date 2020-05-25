namespace RelertSharp.GUI
{
    internal partial class UnitAttributeForm
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
            this.gpnUnit = new System.Windows.Forms.GroupBox();
            this.lklTrace = new System.Windows.Forms.LinkLabel();
            this.lblUnitRegName = new System.Windows.Forms.Label();
            this.lblUnitID = new System.Windows.Forms.Label();
            this.txbFollows = new System.Windows.Forms.TextBox();
            this.ckbRebuild = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.ckbOnBridge = new System.Windows.Forms.CheckBox();
            this.ckbRecruit = new System.Windows.Forms.CheckBox();
            this.gpbGeneralAttribute.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pboxFacing)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkbVeteran)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkbHP)).BeginInit();
            this.gpnUnit.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // gpnUnit
            // 
            this.gpnUnit.Controls.Add(this.lklTrace);
            this.gpnUnit.Controls.Add(this.lblUnitRegName);
            this.gpnUnit.Controls.Add(this.lblUnitID);
            this.gpnUnit.Controls.Add(this.txbFollows);
            this.gpnUnit.Controls.Add(this.ckbRebuild);
            this.gpnUnit.Controls.Add(this.label8);
            this.gpnUnit.Controls.Add(this.ckbOnBridge);
            this.gpnUnit.Controls.Add(this.ckbRecruit);
            this.gpnUnit.Location = new System.Drawing.Point(413, 32);
            this.gpnUnit.Name = "gpnUnit";
            this.gpnUnit.Size = new System.Drawing.Size(315, 281);
            this.gpnUnit.TabIndex = 41;
            this.gpnUnit.TabStop = false;
            this.gpnUnit.Text = "Unit Attribute";
            // 
            // lklTrace
            // 
            this.lklTrace.AutoSize = true;
            this.lklTrace.Location = new System.Drawing.Point(24, 232);
            this.lklTrace.Name = "lklTrace";
            this.lklTrace.Size = new System.Drawing.Size(199, 15);
            this.lklTrace.TabIndex = 4;
            this.lklTrace.TabStop = true;
            this.lklTrace.Text = "Trace Tag in LogicEditor";
            // 
            // lblUnitRegName
            // 
            this.lblUnitRegName.AutoSize = true;
            this.lblUnitRegName.Location = new System.Drawing.Point(24, 203);
            this.lblUnitRegName.Name = "lblUnitRegName";
            this.lblUnitRegName.Size = new System.Drawing.Size(175, 15);
            this.lblUnitRegName.TabIndex = 3;
            this.lblUnitRegName.Text = "Unit Registion Name :";
            // 
            // lblUnitID
            // 
            this.lblUnitID.AutoSize = true;
            this.lblUnitID.Location = new System.Drawing.Point(24, 174);
            this.lblUnitID.Name = "lblUnitID";
            this.lblUnitID.Size = new System.Drawing.Size(79, 15);
            this.lblUnitID.TabIndex = 3;
            this.lblUnitID.Text = "Unit ID :";
            // 
            // txbFollows
            // 
            this.txbFollows.Location = new System.Drawing.Point(117, 24);
            this.txbFollows.Name = "txbFollows";
            this.txbFollows.Size = new System.Drawing.Size(48, 25);
            this.txbFollows.TabIndex = 1;
            this.txbFollows.Validated += new System.EventHandler(this.txbFollows_Validated);
            // 
            // ckbRebuild
            // 
            this.ckbRebuild.AutoSize = true;
            this.ckbRebuild.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckbRebuild.Location = new System.Drawing.Point(168, 80);
            this.ckbRebuild.Name = "ckbRebuild";
            this.ckbRebuild.Size = new System.Drawing.Size(117, 19);
            this.ckbRebuild.TabIndex = 2;
            this.ckbRebuild.Text = "Rebuildable";
            this.ckbRebuild.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckbRebuild.ThreeState = true;
            this.ckbRebuild.UseVisualStyleBackColor = true;
            this.ckbRebuild.CheckedChanged += new System.EventHandler(this.ckbRebuild_CheckedChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(24, 27);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(87, 15);
            this.label8.TabIndex = 0;
            this.label8.Text = "Follows ID";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ckbOnBridge
            // 
            this.ckbOnBridge.AutoSize = true;
            this.ckbOnBridge.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckbOnBridge.Location = new System.Drawing.Point(184, 26);
            this.ckbOnBridge.Name = "ckbOnBridge";
            this.ckbOnBridge.Size = new System.Drawing.Size(101, 19);
            this.ckbOnBridge.TabIndex = 2;
            this.ckbOnBridge.Text = "On Bridge";
            this.ckbOnBridge.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckbOnBridge.ThreeState = true;
            this.ckbOnBridge.UseVisualStyleBackColor = true;
            this.ckbOnBridge.CheckedChanged += new System.EventHandler(this.ckbOnBridge_CheckedChanged);
            // 
            // ckbRecruit
            // 
            this.ckbRecruit.AutoSize = true;
            this.ckbRecruit.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckbRecruit.Location = new System.Drawing.Point(18, 80);
            this.ckbRecruit.Name = "ckbRecruit";
            this.ckbRecruit.Size = new System.Drawing.Size(117, 19);
            this.ckbRecruit.TabIndex = 2;
            this.ckbRecruit.Text = "Recruitable";
            this.ckbRecruit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckbRecruit.ThreeState = true;
            this.ckbRecruit.UseVisualStyleBackColor = true;
            this.ckbRecruit.CheckedChanged += new System.EventHandler(this.ckbRecruit_CheckedChanged);
            // 
            // UnitAttributeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(762, 380);
            this.Controls.Add(this.gpnUnit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "UnitAttributeForm";
            this.Text = "Unit Attribute";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UnitAttributeForm_FormClosing);
            this.Controls.SetChildIndex(this.btnOK, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.gpbGeneralAttribute, 0);
            this.Controls.SetChildIndex(this.gpnUnit, 0);
            this.gpbGeneralAttribute.ResumeLayout(false);
            this.gpbGeneralAttribute.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pboxFacing)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkbVeteran)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkbHP)).EndInit();
            this.gpnUnit.ResumeLayout(false);
            this.gpnUnit.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gpnUnit;
        private System.Windows.Forms.TextBox txbFollows;
        private System.Windows.Forms.CheckBox ckbRebuild;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox ckbRecruit;
        private System.Windows.Forms.CheckBox ckbOnBridge;
        private System.Windows.Forms.Label lblUnitRegName;
        private System.Windows.Forms.Label lblUnitID;
        private System.Windows.Forms.LinkLabel lklTrace;
    }
}