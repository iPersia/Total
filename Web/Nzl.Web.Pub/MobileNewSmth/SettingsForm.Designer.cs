namespace Nzl.Web.Pub.MobileNewSmth
{
    partial class SettingsForm
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
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.nudUpdateInterval = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnTp1Save = new System.Windows.Forms.Button();
            this.ckbSavedPassword = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            this.txtPasswd = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.rtxtItemValue = new System.Windows.Forms.RichTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbItems = new System.Windows.Forms.ComboBox();
            this.btnTp2Save = new System.Windows.Forms.Button();
            this.txtHint = new System.Windows.Forms.TextBox();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudUpdateInterval)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.nudUpdateInterval);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.btnTp1Save);
            this.tabPage1.Controls.Add(this.ckbSavedPassword);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.txtID);
            this.tabPage1.Controls.Add(this.txtPasswd);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(306, 276);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "General";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // nudUpdateInterval
            // 
            this.nudUpdateInterval.Location = new System.Drawing.Point(79, 96);
            this.nudUpdateInterval.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudUpdateInterval.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.nudUpdateInterval.Name = "nudUpdateInterval";
            this.nudUpdateInterval.Size = new System.Drawing.Size(43, 21);
            this.nudUpdateInterval.TabIndex = 12;
            this.nudUpdateInterval.Tag = "UpdateInterval";
            this.nudUpdateInterval.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(128, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 11;
            this.label4.Text = "minutes.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 10;
            this.label3.Text = "Update Per";
            // 
            // btnTp1Save
            // 
            this.btnTp1Save.Location = new System.Drawing.Point(112, 240);
            this.btnTp1Save.Name = "btnTp1Save";
            this.btnTp1Save.Size = new System.Drawing.Size(75, 23);
            this.btnTp1Save.TabIndex = 8;
            this.btnTp1Save.Text = "Save";
            this.btnTp1Save.UseVisualStyleBackColor = true;
            this.btnTp1Save.Click += new System.EventHandler(this.btnTp1Save_Click);
            // 
            // ckbSavedPassword
            // 
            this.ckbSavedPassword.AutoSize = true;
            this.ckbSavedPassword.Checked = true;
            this.ckbSavedPassword.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbSavedPassword.Location = new System.Drawing.Point(248, 61);
            this.ckbSavedPassword.Name = "ckbSavedPassword";
            this.ckbSavedPassword.Size = new System.Drawing.Size(48, 16);
            this.ckbSavedPassword.TabIndex = 7;
            this.ckbSavedPassword.Text = "Save";
            this.ckbSavedPassword.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "Password";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "User Name";
            // 
            // txtID
            // 
            this.txtID.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtID.Font = new System.Drawing.Font("SimSun", 9F);
            this.txtID.Location = new System.Drawing.Point(79, 23);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(160, 21);
            this.txtID.TabIndex = 3;
            this.txtID.Tag = "User";
            // 
            // txtPasswd
            // 
            this.txtPasswd.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtPasswd.Location = new System.Drawing.Point(79, 58);
            this.txtPasswd.Name = "txtPasswd";
            this.txtPasswd.PasswordChar = '*';
            this.txtPasswd.Size = new System.Drawing.Size(160, 21);
            this.txtPasswd.TabIndex = 4;
            this.txtPasswd.Tag = "Password";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(314, 302);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.txtHint);
            this.tabPage2.Controls.Add(this.rtxtItemValue);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.cmbItems);
            this.tabPage2.Controls.Add(this.btnTp2Save);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(306, 276);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Misc";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // rtxtItemValue
            // 
            this.rtxtItemValue.Location = new System.Drawing.Point(8, 42);
            this.rtxtItemValue.Name = "rtxtItemValue";
            this.rtxtItemValue.Size = new System.Drawing.Size(290, 140);
            this.rtxtItemValue.TabIndex = 12;
            this.rtxtItemValue.Text = "";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 12);
            this.label6.TabIndex = 11;
            this.label6.Text = "Items";
            // 
            // cmbItems
            // 
            this.cmbItems.FormattingEnabled = true;
            this.cmbItems.Location = new System.Drawing.Point(60, 16);
            this.cmbItems.Name = "cmbItems";
            this.cmbItems.Size = new System.Drawing.Size(238, 20);
            this.cmbItems.TabIndex = 10;
            this.cmbItems.SelectedIndexChanged += new System.EventHandler(this.cmbItems_SelectedIndexChanged);
            // 
            // btnTp2Save
            // 
            this.btnTp2Save.Location = new System.Drawing.Point(112, 240);
            this.btnTp2Save.Name = "btnTp2Save";
            this.btnTp2Save.Size = new System.Drawing.Size(75, 23);
            this.btnTp2Save.TabIndex = 9;
            this.btnTp2Save.Text = "Save";
            this.btnTp2Save.UseVisualStyleBackColor = true;
            this.btnTp2Save.Click += new System.EventHandler(this.btnTp2Save_Click);
            // 
            // txtHint
            // 
            this.txtHint.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtHint.Location = new System.Drawing.Point(8, 188);
            this.txtHint.Multiline = true;
            this.txtHint.Name = "txtHint";
            this.txtHint.Size = new System.Drawing.Size(288, 46);
            this.txtHint.TabIndex = 13;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(314, 302);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "SettingsForm";
            this.ShowInTaskbar = false;
            this.Text = "Settings";
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudUpdateInterval)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.TextBox txtPasswd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox ckbSavedPassword;
        private System.Windows.Forms.Button btnTp1Save;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nudUpdateInterval;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnTp2Save;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbItems;
        private System.Windows.Forms.RichTextBox rtxtItemValue;
        private System.Windows.Forms.TextBox txtHint;

    }
}