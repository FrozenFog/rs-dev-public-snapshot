namespace RelertSharp.GUI
{
    partial class BaseAttributeForm
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
            this.gpbGeneralAttribute = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txbGroup = new System.Windows.Forms.TextBox();
            this.pboxFacing = new System.Windows.Forms.PictureBox();
            this.mtxbVeteran = new System.Windows.Forms.MaskedTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.mtxbHP = new System.Windows.Forms.MaskedTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.trkbVeteran = new System.Windows.Forms.TrackBar();
            this.label4 = new System.Windows.Forms.Label();
            this.trkbHP = new System.Windows.Forms.TrackBar();
            this.label5 = new System.Windows.Forms.Label();
            this.cbbStatus = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbbTags = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbbOwnerHouse = new System.Windows.Forms.ComboBox();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.gpbGeneralAttribute.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pboxFacing)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkbVeteran)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkbHP)).BeginInit();
            this.SuspendLayout();
            // 
            // gpbGeneralAttribute
            // 
            this.gpbGeneralAttribute.Controls.Add(this.label2);
            this.gpbGeneralAttribute.Controls.Add(this.txbGroup);
            this.gpbGeneralAttribute.Controls.Add(this.pboxFacing);
            this.gpbGeneralAttribute.Controls.Add(this.mtxbVeteran);
            this.gpbGeneralAttribute.Controls.Add(this.label1);
            this.gpbGeneralAttribute.Controls.Add(this.mtxbHP);
            this.gpbGeneralAttribute.Controls.Add(this.label3);
            this.gpbGeneralAttribute.Controls.Add(this.trkbVeteran);
            this.gpbGeneralAttribute.Controls.Add(this.label4);
            this.gpbGeneralAttribute.Controls.Add(this.trkbHP);
            this.gpbGeneralAttribute.Controls.Add(this.label5);
            this.gpbGeneralAttribute.Controls.Add(this.cbbStatus);
            this.gpbGeneralAttribute.Controls.Add(this.label6);
            this.gpbGeneralAttribute.Controls.Add(this.cbbTags);
            this.gpbGeneralAttribute.Controls.Add(this.label7);
            this.gpbGeneralAttribute.Controls.Add(this.cbbOwnerHouse);
            this.gpbGeneralAttribute.Location = new System.Drawing.Point(27, 32);
            this.gpbGeneralAttribute.Name = "gpbGeneralAttribute";
            this.gpbGeneralAttribute.Size = new System.Drawing.Size(380, 281);
            this.gpbGeneralAttribute.TabIndex = 39;
            this.gpbGeneralAttribute.TabStop = false;
            this.gpbGeneralAttribute.Text = "General Attribute";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(79, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 15);
            this.label2.TabIndex = 23;
            this.label2.Text = "Owner House";
            // 
            // txbGroup
            // 
            this.txbGroup.Location = new System.Drawing.Point(180, 229);
            this.txbGroup.Name = "txbGroup";
            this.txbGroup.Size = new System.Drawing.Size(173, 25);
            this.txbGroup.TabIndex = 37;
            this.txbGroup.TextChanged += new System.EventHandler(this.txbGroup_TextChanged);
            // 
            // pboxFacing
            // 
            this.pboxFacing.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pboxFacing.Image = global::RelertSharp.GUI.Properties.Resources.rotationBase;
            this.pboxFacing.Location = new System.Drawing.Point(180, 113);
            this.pboxFacing.Name = "pboxFacing";
            this.pboxFacing.Size = new System.Drawing.Size(52, 52);
            this.pboxFacing.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pboxFacing.TabIndex = 22;
            this.pboxFacing.TabStop = false;
            this.pboxFacing.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pboxFacing_MouseDown);
            this.pboxFacing.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pboxFacing_MouseMove);
            this.pboxFacing.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pboxFacing_MouseUp);
            // 
            // mtxbVeteran
            // 
            this.mtxbVeteran.Location = new System.Drawing.Point(284, 84);
            this.mtxbVeteran.Name = "mtxbVeteran";
            this.mtxbVeteran.Size = new System.Drawing.Size(69, 25);
            this.mtxbVeteran.TabIndex = 35;
            this.mtxbVeteran.Validated += new System.EventHandler(this.mtxbVeteran_Validated);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(119, 132);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 15);
            this.label1.TabIndex = 23;
            this.label1.Text = "Facing";
            // 
            // mtxbHP
            // 
            this.mtxbHP.Location = new System.Drawing.Point(284, 53);
            this.mtxbHP.Name = "mtxbHP";
            this.mtxbHP.Size = new System.Drawing.Size(69, 25);
            this.mtxbHP.TabIndex = 36;
            this.mtxbHP.Validated += new System.EventHandler(this.mtxbHP_Validated);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(71, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 15);
            this.label3.TabIndex = 23;
            this.label3.Text = "Health Point";
            // 
            // trkbVeteran
            // 
            this.trkbVeteran.AutoSize = false;
            this.trkbVeteran.LargeChange = 100;
            this.trkbVeteran.Location = new System.Drawing.Point(180, 84);
            this.trkbVeteran.Maximum = 200;
            this.trkbVeteran.Name = "trkbVeteran";
            this.trkbVeteran.Size = new System.Drawing.Size(98, 23);
            this.trkbVeteran.TabIndex = 33;
            this.trkbVeteran.TickFrequency = 32;
            this.trkbVeteran.Scroll += new System.EventHandler(this.trkbVeteran_Scroll);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 84);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(167, 15);
            this.label4.TabIndex = 23;
            this.label4.Text = "Veterancy Percentage";
            // 
            // trkbHP
            // 
            this.trkbHP.AutoSize = false;
            this.trkbHP.LargeChange = 64;
            this.trkbHP.Location = new System.Drawing.Point(180, 53);
            this.trkbHP.Maximum = 256;
            this.trkbHP.Minimum = 1;
            this.trkbHP.Name = "trkbHP";
            this.trkbHP.Size = new System.Drawing.Size(98, 23);
            this.trkbHP.TabIndex = 34;
            this.trkbHP.TickFrequency = 32;
            this.trkbHP.Value = 256;
            this.trkbHP.Scroll += new System.EventHandler(this.trkbHP_Scroll);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(63, 174);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(111, 15);
            this.label5.TabIndex = 23;
            this.label5.Text = "Attatched Tag";
            // 
            // cbbStatus
            // 
            this.cbbStatus.FormattingEnabled = true;
            this.cbbStatus.Location = new System.Drawing.Point(180, 200);
            this.cbbStatus.Name = "cbbStatus";
            this.cbbStatus.Size = new System.Drawing.Size(173, 23);
            this.cbbStatus.TabIndex = 30;
            this.cbbStatus.SelectedIndexChanged += new System.EventHandler(this.cbbStatus_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(79, 203);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(95, 15);
            this.label6.TabIndex = 23;
            this.label6.Text = "Unit Status";
            // 
            // cbbTags
            // 
            this.cbbTags.FormattingEnabled = true;
            this.cbbTags.Location = new System.Drawing.Point(180, 171);
            this.cbbTags.Name = "cbbTags";
            this.cbbTags.Size = new System.Drawing.Size(173, 23);
            this.cbbTags.TabIndex = 31;
            this.cbbTags.SelectedIndexChanged += new System.EventHandler(this.cbbTags_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(127, 232);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 15);
            this.label7.TabIndex = 23;
            this.label7.Text = "Group";
            // 
            // cbbOwnerHouse
            // 
            this.cbbOwnerHouse.FormattingEnabled = true;
            this.cbbOwnerHouse.Location = new System.Drawing.Point(180, 24);
            this.cbbOwnerHouse.Name = "cbbOwnerHouse";
            this.cbbOwnerHouse.Size = new System.Drawing.Size(173, 23);
            this.cbbOwnerHouse.TabIndex = 32;
            this.cbbOwnerHouse.SelectedIndexChanged += new System.EventHandler(this.cbbOwnerHouse_SelectedIndexChanged);
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(136, 336);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(112, 29);
            this.btnOK.TabIndex = 40;
            this.btnOK.Text = "Apply";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(481, 336);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(112, 29);
            this.btnCancel.TabIndex = 40;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // BaseAttributeForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(762, 380);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.gpbGeneralAttribute);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BaseAttributeForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "BaseAttributeForm";
            this.TopMost = true;
            this.gpbGeneralAttribute.ResumeLayout(false);
            this.gpbGeneralAttribute.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pboxFacing)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkbVeteran)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkbHP)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        protected System.Windows.Forms.ToolTip toolTip;
        protected System.Windows.Forms.GroupBox gpbGeneralAttribute;
        protected System.Windows.Forms.Label label2;
        protected System.Windows.Forms.TextBox txbGroup;
        protected System.Windows.Forms.PictureBox pboxFacing;
        protected System.Windows.Forms.MaskedTextBox mtxbVeteran;
        protected System.Windows.Forms.Label label1;
        protected System.Windows.Forms.MaskedTextBox mtxbHP;
        protected System.Windows.Forms.Label label3;
        protected System.Windows.Forms.TrackBar trkbVeteran;
        protected System.Windows.Forms.Label label4;
        protected System.Windows.Forms.TrackBar trkbHP;
        protected System.Windows.Forms.Label label5;
        protected System.Windows.Forms.ComboBox cbbStatus;
        protected System.Windows.Forms.Label label6;
        protected System.Windows.Forms.Label label7;
        protected System.Windows.Forms.ComboBox cbbOwnerHouse;
        protected System.Windows.Forms.Button btnOK;
        protected System.Windows.Forms.Button btnCancel;
        protected internal System.Windows.Forms.ComboBox cbbTags;
    }
}