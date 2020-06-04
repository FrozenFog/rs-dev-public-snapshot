﻿namespace RelertSharp.GUI
{
    partial class AircraftAttributeForm
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
            this.ckbRebuild = new System.Windows.Forms.CheckBox();
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
            this.gpnUnit.Controls.Add(this.ckbRebuild);
            this.gpnUnit.Controls.Add(this.ckbRecruit);
            this.gpnUnit.Location = new System.Drawing.Point(413, 32);
            this.gpnUnit.Name = "gpnUnit";
            this.gpnUnit.Size = new System.Drawing.Size(315, 281);
            this.gpnUnit.TabIndex = 42;
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
            this.lblUnitRegName.Size = new System.Drawing.Size(207, 15);
            this.lblUnitRegName.TabIndex = 3;
            this.lblUnitRegName.Text = "Aircraft Registion Name :";
            // 
            // lblUnitID
            // 
            this.lblUnitID.AutoSize = true;
            this.lblUnitID.Location = new System.Drawing.Point(24, 174);
            this.lblUnitID.Name = "lblUnitID";
            this.lblUnitID.Size = new System.Drawing.Size(111, 15);
            this.lblUnitID.TabIndex = 3;
            this.lblUnitID.Text = "Aircraft ID :";
            // 
            // ckbRebuild
            // 
            this.ckbRebuild.AutoSize = true;
            this.ckbRebuild.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckbRebuild.Location = new System.Drawing.Point(177, 55);
            this.ckbRebuild.Name = "ckbRebuild";
            this.ckbRebuild.Size = new System.Drawing.Size(117, 19);
            this.ckbRebuild.TabIndex = 2;
            this.ckbRebuild.Text = "Rebuildable";
            this.ckbRebuild.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckbRebuild.ThreeState = true;
            this.ckbRebuild.UseVisualStyleBackColor = true;
            this.ckbRebuild.CheckedChanged += new System.EventHandler(this.ckbRebuild_CheckedChanged);
            // 
            // ckbRecruit
            // 
            this.ckbRecruit.AutoSize = true;
            this.ckbRecruit.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckbRecruit.Location = new System.Drawing.Point(27, 55);
            this.ckbRecruit.Name = "ckbRecruit";
            this.ckbRecruit.Size = new System.Drawing.Size(117, 19);
            this.ckbRecruit.TabIndex = 2;
            this.ckbRecruit.Text = "Recruitable";
            this.ckbRecruit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckbRecruit.ThreeState = true;
            this.ckbRecruit.UseVisualStyleBackColor = true;
            this.ckbRecruit.CheckedChanged += new System.EventHandler(this.ckbRecruit_CheckedChanged);
            // 
            // AircraftAttributeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(762, 380);
            this.Controls.Add(this.gpnUnit);
            this.Name = "AircraftAttributeForm";
            this.Text = "AircraftAttributeForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UnitAttributeForm_FormClosing);
            this.Controls.SetChildIndex(this.gpbGeneralAttribute, 0);
            this.Controls.SetChildIndex(this.btnOK, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
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
        private System.Windows.Forms.LinkLabel lklTrace;
        private System.Windows.Forms.Label lblUnitRegName;
        private System.Windows.Forms.Label lblUnitID;
        private System.Windows.Forms.CheckBox ckbRebuild;
        private System.Windows.Forms.CheckBox ckbRecruit;
    }
}