namespace Nzl.Test.Hook
{
    partial class UserActivityLoggerClientForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserActivityLoggerClientForm));
            this.uiTimer = new System.Windows.Forms.Timer(this.components);
            this.txtBox = new System.Windows.Forms.RichTextBox();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.cmsNotifyIcon = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiAutoRun = new System.Windows.Forms.ToolStripMenuItem();
            this.onToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.offToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsSerperator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiLog = new System.Windows.Forms.ToolStripMenuItem();
            this.tsSerperator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiExit = new System.Windows.Forms.ToolStripMenuItem();
            this.logTimer = new System.Windows.Forms.Timer(this.components);
            this.tsmiAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.tsSerperator3 = new System.Windows.Forms.ToolStripSeparator();
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
            this.txtBox.Size = new System.Drawing.Size(444, 67);
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
            this.tsSerperator3,
            this.tsmiLog,
            this.tsSerperator2,
            this.tsmiAutoRun,
            this.tsSerperator1,
            this.tsmiExit});
            this.cmsNotifyIcon.Name = "cmsNotifyIcon";
            this.cmsNotifyIcon.Size = new System.Drawing.Size(212, 110);
            this.cmsNotifyIcon.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.cmsNotifyIcon_ItemClicked);
            // 
            // tsmiAutoRun
            // 
            this.tsmiAutoRun.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.onToolStripMenuItem,
            this.offToolStripMenuItem});
            this.tsmiAutoRun.Name = "tsmiAutoRun";
            this.tsmiAutoRun.Size = new System.Drawing.Size(211, 22);
            this.tsmiAutoRun.Text = "Start on system startup";
            // 
            // onToolStripMenuItem
            // 
            this.onToolStripMenuItem.Name = "onToolStripMenuItem";
            this.onToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.onToolStripMenuItem.Text = "On";
            this.onToolStripMenuItem.Click += new System.EventHandler(this.onToolStripMenuItem_Click);
            // 
            // offToolStripMenuItem
            // 
            this.offToolStripMenuItem.Name = "offToolStripMenuItem";
            this.offToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.offToolStripMenuItem.Text = "Off";
            this.offToolStripMenuItem.Click += new System.EventHandler(this.offToolStripMenuItem_Click);
            // 
            // tsSerperator2
            // 
            this.tsSerperator2.Name = "tsSerperator2";
            this.tsSerperator2.Size = new System.Drawing.Size(208, 6);
            // 
            // tsmiLog
            // 
            this.tsmiLog.Name = "tsmiLog";
            this.tsmiLog.Size = new System.Drawing.Size(211, 22);
            this.tsmiLog.Text = "Log";
            // 
            // tsSerperator1
            // 
            this.tsSerperator1.Name = "tsSerperator1";
            this.tsSerperator1.Size = new System.Drawing.Size(208, 6);
            // 
            // tsmiExit
            // 
            this.tsmiExit.Name = "tsmiExit";
            this.tsmiExit.Size = new System.Drawing.Size(211, 22);
            this.tsmiExit.Text = "Exit";
            // 
            // logTimer
            // 
            this.logTimer.Tick += new System.EventHandler(this.logTimer_Tick);
            // 
            // tsmiAbout
            // 
            this.tsmiAbout.Name = "tsmiAbout";
            this.tsmiAbout.Size = new System.Drawing.Size(211, 22);
            this.tsmiAbout.Text = "About";
            this.tsmiAbout.Click += new System.EventHandler(this.tsmiAbout_Click);
            // 
            // tsSerperator3
            // 
            this.tsSerperator3.Name = "tsSerperator3";
            this.tsSerperator3.Size = new System.Drawing.Size(208, 6);
            // 
            // UserActivityLoggerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 67);
            this.Controls.Add(this.txtBox);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "UserActivityLoggerForm";
            this.Text = "User Activity Logger";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UserActivityLoggerForm_FormClosing);
            this.Shown += new System.EventHandler(this.UserActivityLoggerForm_Shown);
            this.SizeChanged += new System.EventHandler(this.UserActivityLoggerForm_SizeChanged);
            this.cmsNotifyIcon.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer uiTimer;
        private System.Windows.Forms.RichTextBox txtBox;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip cmsNotifyIcon;
        private System.Windows.Forms.ToolStripMenuItem tsmiExit;
        private System.Windows.Forms.ToolStripMenuItem tsmiLog;
        private System.Windows.Forms.ToolStripSeparator tsSerperator1;
        private System.Windows.Forms.Timer logTimer;
        private System.Windows.Forms.ToolStripMenuItem tsmiAutoRun;
        private System.Windows.Forms.ToolStripSeparator tsSerperator2;
        private System.Windows.Forms.ToolStripMenuItem onToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem offToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiAbout;
        private System.Windows.Forms.ToolStripSeparator tsSerperator3;
    }
}