namespace Nzl.Smth.Forms
{
    partial class TopicBrowserSettingsForm
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
            this.rbLatestReply = new System.Windows.Forms.RadioButton();
            this.rbFirstReply = new System.Windows.Forms.RadioButton();
            this.gpBrowseMode = new System.Windows.Forms.GroupBox();
            this.ckbAutoUpdating = new System.Windows.Forms.CheckBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.gpAutoUpdating = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbInterval = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gpBrowseMode.SuspendLayout();
            this.gpAutoUpdating.SuspendLayout();
            this.SuspendLayout();
            // 
            // rbLatestReply
            // 
            this.rbLatestReply.AutoSize = true;
            this.rbLatestReply.Location = new System.Drawing.Point(17, 26);
            this.rbLatestReply.Name = "rbLatestReply";
            this.rbLatestReply.Size = new System.Drawing.Size(95, 16);
            this.rbLatestReply.TabIndex = 0;
            this.rbLatestReply.Text = "Latest Reply";
            this.rbLatestReply.UseVisualStyleBackColor = true;
            this.rbLatestReply.CheckedChanged += new System.EventHandler(this.rbLatestReply_CheckedChanged);
            // 
            // rbFirstReply
            // 
            this.rbFirstReply.AutoSize = true;
            this.rbFirstReply.Location = new System.Drawing.Point(161, 26);
            this.rbFirstReply.Name = "rbFirstReply";
            this.rbFirstReply.Size = new System.Drawing.Size(89, 16);
            this.rbFirstReply.TabIndex = 1;
            this.rbFirstReply.Text = "First Reply";
            this.rbFirstReply.UseVisualStyleBackColor = true;
            // 
            // gpBrowseMode
            // 
            this.gpBrowseMode.Controls.Add(this.rbFirstReply);
            this.gpBrowseMode.Controls.Add(this.rbLatestReply);
            this.gpBrowseMode.Location = new System.Drawing.Point(12, 12);
            this.gpBrowseMode.Name = "gpBrowseMode";
            this.gpBrowseMode.Size = new System.Drawing.Size(277, 64);
            this.gpBrowseMode.TabIndex = 2;
            this.gpBrowseMode.TabStop = false;
            this.gpBrowseMode.Text = "Browse Mode";
            // 
            // ckbAutoUpdating
            // 
            this.ckbAutoUpdating.AutoSize = true;
            this.ckbAutoUpdating.Location = new System.Drawing.Point(17, 37);
            this.ckbAutoUpdating.Name = "ckbAutoUpdating";
            this.ckbAutoUpdating.Size = new System.Drawing.Size(102, 16);
            this.ckbAutoUpdating.TabIndex = 3;
            this.ckbAutoUpdating.TabStop = false;
            this.ckbAutoUpdating.Text = "Auto updating";
            this.ckbAutoUpdating.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(105, 240);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 4;
            this.btnOK.TabStop = false;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // gpAutoUpdating
            // 
            this.gpAutoUpdating.Controls.Add(this.label2);
            this.gpAutoUpdating.Controls.Add(this.cmbInterval);
            this.gpAutoUpdating.Controls.Add(this.label1);
            this.gpAutoUpdating.Controls.Add(this.ckbAutoUpdating);
            this.gpAutoUpdating.Location = new System.Drawing.Point(12, 95);
            this.gpAutoUpdating.Name = "gpAutoUpdating";
            this.gpAutoUpdating.Size = new System.Drawing.Size(277, 120);
            this.gpAutoUpdating.TabIndex = 5;
            this.gpAutoUpdating.TabStop = false;
            this.gpAutoUpdating.Text = "Auto Updating in Latest Reply";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(173, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "s.";
            // 
            // cmbInterval
            // 
            this.cmbInterval.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbInterval.FormattingEnabled = true;
            this.cmbInterval.Items.AddRange(new object[] {
            "5",
            "10",
            "15",
            "30",
            "60",
            "180",
            "300",
            "600"});
            this.cmbInterval.Location = new System.Drawing.Point(93, 76);
            this.cmbInterval.Name = "cmbInterval";
            this.cmbInterval.Size = new System.Drawing.Size(75, 20);
            this.cmbInterval.TabIndex = 5;
            this.cmbInterval.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "Interval is";
            // 
            // TopicBrowserSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(301, 301);
            this.Controls.Add(this.gpAutoUpdating);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.gpBrowseMode);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "TopicBrowserSettingsForm";
            this.Text = "Settings";
            this.gpBrowseMode.ResumeLayout(false);
            this.gpBrowseMode.PerformLayout();
            this.gpAutoUpdating.ResumeLayout(false);
            this.gpAutoUpdating.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton rbLatestReply;
        private System.Windows.Forms.RadioButton rbFirstReply;
        private System.Windows.Forms.GroupBox gpBrowseMode;
        private System.Windows.Forms.CheckBox ckbAutoUpdating;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.GroupBox gpAutoUpdating;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbInterval;
    }
}