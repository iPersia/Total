namespace Nzl.Test.Hook
{
    partial class UserActivitySupervisorClientForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserActivitySupervisorClientForm));
            this.uiTimer = new System.Windows.Forms.Timer(this.components);
            this.txtBox = new System.Windows.Forms.RichTextBox();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.cmsNotifyIcon = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.tsSeparator03 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiClientConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEditMonitorConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.tsSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiUpdateMonitorConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.tsSeparator02 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiServerConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEditSupervisorConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.tsSeparator20 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiUpdateSupervisorConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.tsSeparator01 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiLog = new System.Windows.Forms.ToolStripMenuItem();
            this.tsSeparator00 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiExit = new System.Windows.Forms.ToolStripMenuItem();
            this.logTimer = new System.Windows.Forms.Timer(this.components);
            this.checkServerTimer = new System.Windows.Forms.Timer(this.components);
            this.bwRequestMessage = new System.ComponentModel.BackgroundWorker();
            this.cmsNotifyIcon.SuspendLayout();
            this.SuspendLayout();
            // 
            // uiTimer
            // 
            this.uiTimer.Tick += new System.EventHandler(this.uiTimer_Tick);
            // 
            // txtBox
            // 
            this.txtBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBox.Enabled = false;
            this.txtBox.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtBox.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtBox.Location = new System.Drawing.Point(0, 0);
            this.txtBox.Name = "txtBox";
            this.txtBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.txtBox.ShortcutsEnabled = false;
            this.txtBox.Size = new System.Drawing.Size(352, 192);
            this.txtBox.TabIndex = 0;
            this.txtBox.Text = "";
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.cmsNotifyIcon;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "User Activity Logger";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
            // 
            // cmsNotifyIcon
            // 
            this.cmsNotifyIcon.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAbout,
            this.tsSeparator03,
            this.tsmiClientConfig,
            this.tsSeparator02,
            this.tsmiServerConfig,
            this.tsSeparator01,
            this.tsmiLog,
            this.tsSeparator00,
            this.tsmiExit});
            this.cmsNotifyIcon.Name = "cmsNotifyIcon";
            this.cmsNotifyIcon.Size = new System.Drawing.Size(181, 138);
            // 
            // tsmiAbout
            // 
            this.tsmiAbout.Name = "tsmiAbout";
            this.tsmiAbout.Size = new System.Drawing.Size(180, 22);
            this.tsmiAbout.Text = "About";
            this.tsmiAbout.Click += new System.EventHandler(this.tsmiAbout_Click);
            // 
            // tsSeparator03
            // 
            this.tsSeparator03.Name = "tsSeparator03";
            this.tsSeparator03.Size = new System.Drawing.Size(177, 6);
            // 
            // tsmiClientConfig
            // 
            this.tsmiClientConfig.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiEditMonitorConfig,
            this.tsSeparator10,
            this.tsmiUpdateMonitorConfig});
            this.tsmiClientConfig.Name = "tsmiClientConfig";
            this.tsmiClientConfig.Size = new System.Drawing.Size(180, 22);
            this.tsmiClientConfig.Text = "Monitor Config";
            // 
            // tsmiEditMonitorConfig
            // 
            this.tsmiEditMonitorConfig.Name = "tsmiEditMonitorConfig";
            this.tsmiEditMonitorConfig.Size = new System.Drawing.Size(119, 22);
            this.tsmiEditMonitorConfig.Text = "Edit";
            this.tsmiEditMonitorConfig.Click += new System.EventHandler(this.tsmiEditMonitorConfig_Click);
            // 
            // tsSeparator10
            // 
            this.tsSeparator10.Name = "tsSeparator10";
            this.tsSeparator10.Size = new System.Drawing.Size(116, 6);
            // 
            // tsmiUpdateMonitorConfig
            // 
            this.tsmiUpdateMonitorConfig.Name = "tsmiUpdateMonitorConfig";
            this.tsmiUpdateMonitorConfig.Size = new System.Drawing.Size(119, 22);
            this.tsmiUpdateMonitorConfig.Text = "Update";
            this.tsmiUpdateMonitorConfig.Click += new System.EventHandler(this.tsmiUpdateClientConfig_Click);
            // 
            // tsSeparator02
            // 
            this.tsSeparator02.Name = "tsSeparator02";
            this.tsSeparator02.Size = new System.Drawing.Size(177, 6);
            // 
            // tsmiServerConfig
            // 
            this.tsmiServerConfig.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiEditSupervisorConfig,
            this.tsSeparator20,
            this.tsmiUpdateSupervisorConfig});
            this.tsmiServerConfig.Name = "tsmiServerConfig";
            this.tsmiServerConfig.Size = new System.Drawing.Size(180, 22);
            this.tsmiServerConfig.Text = "Supervisor Config";
            // 
            // tsmiEditSupervisorConfig
            // 
            this.tsmiEditSupervisorConfig.Name = "tsmiEditSupervisorConfig";
            this.tsmiEditSupervisorConfig.Size = new System.Drawing.Size(119, 22);
            this.tsmiEditSupervisorConfig.Text = "Edit";
            this.tsmiEditSupervisorConfig.Click += new System.EventHandler(this.tsmiEditSupervisorConfig_Click);
            // 
            // tsSeparator20
            // 
            this.tsSeparator20.Name = "tsSeparator20";
            this.tsSeparator20.Size = new System.Drawing.Size(116, 6);
            // 
            // tsmiUpdateSupervisorConfig
            // 
            this.tsmiUpdateSupervisorConfig.Name = "tsmiUpdateSupervisorConfig";
            this.tsmiUpdateSupervisorConfig.Size = new System.Drawing.Size(119, 22);
            this.tsmiUpdateSupervisorConfig.Text = "Update";
            this.tsmiUpdateSupervisorConfig.Click += new System.EventHandler(this.tsmiUpdateServerConfig_Click);
            // 
            // tsSeparator01
            // 
            this.tsSeparator01.Name = "tsSeparator01";
            this.tsSeparator01.Size = new System.Drawing.Size(177, 6);
            // 
            // tsmiLog
            // 
            this.tsmiLog.Name = "tsmiLog";
            this.tsmiLog.Size = new System.Drawing.Size(180, 22);
            this.tsmiLog.Text = "Log";
            this.tsmiLog.Click += new System.EventHandler(this.tsmiLog_Click);
            // 
            // tsSeparator00
            // 
            this.tsSeparator00.Name = "tsSeparator00";
            this.tsSeparator00.Size = new System.Drawing.Size(177, 6);
            // 
            // tsmiExit
            // 
            this.tsmiExit.Name = "tsmiExit";
            this.tsmiExit.Size = new System.Drawing.Size(180, 22);
            this.tsmiExit.Text = "Exit";
            this.tsmiExit.Click += new System.EventHandler(this.tsmiExit_Click);
            // 
            // logTimer
            // 
            this.logTimer.Tick += new System.EventHandler(this.logTimer_Tick);
            // 
            // checkServerTimer
            // 
            this.checkServerTimer.Tick += new System.EventHandler(this.checkServerTimer_Tick);
            
            // 
            // UserActivitySupervisorClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(352, 192);
            this.Controls.Add(this.txtBox);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "UserActivitySupervisorClientForm";
            this.Text = "User Activity Monitor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UserActivitySupervisorClientForm_FormClosing);
            this.Shown += new System.EventHandler(this.UserActivitySupervisorClientForm_Shown);
            this.SizeChanged += new System.EventHandler(this.UserActivitySupervisorClientForm_SizeChanged);
            this.cmsNotifyIcon.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer uiTimer;
        private System.Windows.Forms.RichTextBox txtBox;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.Timer logTimer;
        private System.Windows.Forms.ContextMenuStrip cmsNotifyIcon;
        private System.Windows.Forms.ToolStripMenuItem tsmiAbout;
        private System.Windows.Forms.ToolStripSeparator tsSeparator03;
        private System.Windows.Forms.ToolStripMenuItem tsmiLog;
        private System.Windows.Forms.ToolStripSeparator tsSeparator01;
        private System.Windows.Forms.ToolStripMenuItem tsmiExit;
        private System.Windows.Forms.Timer checkServerTimer;
        private System.Windows.Forms.ToolStripMenuItem tsmiClientConfig;
        private System.Windows.Forms.ToolStripSeparator tsSeparator02;
        private System.Windows.Forms.ToolStripMenuItem tsmiServerConfig;
        private System.Windows.Forms.ToolStripSeparator tsSeparator00;
        private System.Windows.Forms.ToolStripMenuItem tsmiEditMonitorConfig;
        private System.Windows.Forms.ToolStripMenuItem tsmiUpdateMonitorConfig;
        private System.Windows.Forms.ToolStripSeparator tsSeparator10;
        private System.Windows.Forms.ToolStripMenuItem tsmiEditSupervisorConfig;
        private System.Windows.Forms.ToolStripSeparator tsSeparator20;
        private System.Windows.Forms.ToolStripMenuItem tsmiUpdateSupervisorConfig;
        private System.ComponentModel.BackgroundWorker bwRequestMessage;
    }
}