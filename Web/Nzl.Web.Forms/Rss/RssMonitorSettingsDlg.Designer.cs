namespace Nzl.Web.Forms.Rss
{
    partial class RssMonitorSettingsDlg
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
            this.tcSettings = new System.Windows.Forms.TabControl();
            this.tpRssProvider = new System.Windows.Forms.TabPage();
            this.clbRssReaders = new System.Windows.Forms.CheckedListBox();
            this.tpRssReaderSetting = new System.Windows.Forms.TabPage();
            this.cmbTotalCount = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnEnter = new System.Windows.Forms.Button();
            this.panelContainer = new System.Windows.Forms.Panel();
            this.cmbWindowSize = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tcSettings.SuspendLayout();
            this.tpRssProvider.SuspendLayout();
            this.tpRssReaderSetting.SuspendLayout();
            this.panelContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcSettings
            // 
            this.tcSettings.Controls.Add(this.tpRssProvider);
            this.tcSettings.Controls.Add(this.tpRssReaderSetting);
            this.tcSettings.Dock = System.Windows.Forms.DockStyle.Top;
            this.tcSettings.Location = new System.Drawing.Point(0, 0);
            this.tcSettings.Name = "tcSettings";
            this.tcSettings.SelectedIndex = 0;
            this.tcSettings.Size = new System.Drawing.Size(354, 326);
            this.tcSettings.TabIndex = 0;
            // 
            // tpRssProvider
            // 
            this.tpRssProvider.Controls.Add(this.clbRssReaders);
            this.tpRssProvider.Location = new System.Drawing.Point(4, 22);
            this.tpRssProvider.Name = "tpRssProvider";
            this.tpRssProvider.Padding = new System.Windows.Forms.Padding(3);
            this.tpRssProvider.Size = new System.Drawing.Size(346, 300);
            this.tpRssProvider.TabIndex = 0;
            this.tpRssProvider.Text = "Rss Provider";
            this.tpRssProvider.UseVisualStyleBackColor = true;
            // 
            // clbRssReaders
            // 
            this.clbRssReaders.CheckOnClick = true;
            this.clbRssReaders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clbRssReaders.FormattingEnabled = true;
            this.clbRssReaders.Location = new System.Drawing.Point(3, 3);
            this.clbRssReaders.Name = "clbRssReaders";
            this.clbRssReaders.Size = new System.Drawing.Size(340, 294);
            this.clbRssReaders.TabIndex = 5;
            // 
            // tpRssReaderSetting
            // 
            this.tpRssReaderSetting.Controls.Add(this.cmbWindowSize);
            this.tpRssReaderSetting.Controls.Add(this.label2);
            this.tpRssReaderSetting.Controls.Add(this.cmbTotalCount);
            this.tpRssReaderSetting.Controls.Add(this.label1);
            this.tpRssReaderSetting.Location = new System.Drawing.Point(4, 22);
            this.tpRssReaderSetting.Name = "tpRssReaderSetting";
            this.tpRssReaderSetting.Padding = new System.Windows.Forms.Padding(3);
            this.tpRssReaderSetting.Size = new System.Drawing.Size(346, 300);
            this.tpRssReaderSetting.TabIndex = 1;
            this.tpRssReaderSetting.Text = "Misc Setting";
            this.tpRssReaderSetting.UseVisualStyleBackColor = true;
            // 
            // cmbTotalCount
            // 
            this.cmbTotalCount.FormattingEnabled = true;
            this.cmbTotalCount.Location = new System.Drawing.Point(208, 31);
            this.cmbTotalCount.Name = "cmbTotalCount";
            this.cmbTotalCount.Size = new System.Drawing.Size(121, 20);
            this.cmbTotalCount.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(161, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Total Displayed Item Count";
            // 
            // btnEnter
            // 
            this.btnEnter.Location = new System.Drawing.Point(137, 337);
            this.btnEnter.Name = "btnEnter";
            this.btnEnter.Size = new System.Drawing.Size(80, 23);
            this.btnEnter.TabIndex = 1;
            this.btnEnter.Text = "OK";
            this.btnEnter.UseVisualStyleBackColor = true;
            this.btnEnter.Click += new System.EventHandler(this.btnEnter_Click);
            // 
            // panelContainer
            // 
            this.panelContainer.BackColor = System.Drawing.SystemColors.Window;
            this.panelContainer.Controls.Add(this.tcSettings);
            this.panelContainer.Controls.Add(this.btnEnter);
            this.panelContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContainer.Location = new System.Drawing.Point(0, 0);
            this.panelContainer.Name = "panelContainer";
            this.panelContainer.Size = new System.Drawing.Size(354, 374);
            this.panelContainer.TabIndex = 2;
            // 
            // cmbWindowSize
            // 
            this.cmbWindowSize.FormattingEnabled = true;
            this.cmbWindowSize.Location = new System.Drawing.Point(208, 68);
            this.cmbWindowSize.Name = "cmbWindowSize";
            this.cmbWindowSize.Size = new System.Drawing.Size(121, 20);
            this.cmbWindowSize.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "Window Size";
            // 
            // RssMonitorSettingsDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(354, 374);
            this.Controls.Add(this.panelContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RssMonitorSettingsDlg";
            this.Text = "Rss Monitor Settings";
            this.Load += new System.EventHandler(this.RssMonitorSettingsDlg_Load);
            this.tcSettings.ResumeLayout(false);
            this.tpRssProvider.ResumeLayout(false);
            this.tpRssReaderSetting.ResumeLayout(false);
            this.tpRssReaderSetting.PerformLayout();
            this.panelContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcSettings;
        private System.Windows.Forms.TabPage tpRssProvider;
        private System.Windows.Forms.TabPage tpRssReaderSetting;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbTotalCount;
        private System.Windows.Forms.Button btnEnter;
        private System.Windows.Forms.CheckedListBox clbRssReaders;
        private System.Windows.Forms.Panel panelContainer;
        private System.Windows.Forms.ComboBox cmbWindowSize;
        private System.Windows.Forms.Label label2;
    }
}