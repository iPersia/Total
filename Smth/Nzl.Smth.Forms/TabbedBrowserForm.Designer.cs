namespace Nzl.Smth.Forms
{
    partial class TabbedBrowserForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TabbedBrowserForm));
            this.tcTopics = new System.Windows.Forms.TabControl();
            this.scBrowser = new System.Windows.Forms.SplitContainer();
            this.panelMenu = new System.Windows.Forms.Panel();
            this.btnRefer = new System.Windows.Forms.Button();
            this.linklblUserID = new System.Windows.Forms.LinkLabel();
            this.btnMessge = new System.Windows.Forms.Button();
            this.btnLoadTop = new System.Windows.Forms.Button();
            this.btnLogon = new System.Windows.Forms.Button();
            this.btnMail = new System.Windows.Forms.Button();
            this.btnFavor = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnBoardNavi = new System.Windows.Forms.Button();
            this.btnSettings = new System.Windows.Forms.Button();
            this.panelContainer = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.scBrowser)).BeginInit();
            this.scBrowser.Panel1.SuspendLayout();
            this.scBrowser.Panel2.SuspendLayout();
            this.scBrowser.SuspendLayout();
            this.panelMenu.SuspendLayout();
            this.panelContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcTopics
            // 
            this.tcTopics.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcTopics.Location = new System.Drawing.Point(0, 0);
            this.tcTopics.Name = "tcTopics";
            this.tcTopics.SelectedIndex = 0;
            this.tcTopics.Size = new System.Drawing.Size(1006, 696);
            this.tcTopics.TabIndex = 0;
            this.tcTopics.SelectedIndexChanged += new System.EventHandler(this.tcTopics_SelectedIndexChanged);
            this.tcTopics.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.tcTopics_MouseDoubleClick);
            // 
            // scBrowser
            // 
            this.scBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scBrowser.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.scBrowser.IsSplitterFixed = true;
            this.scBrowser.Location = new System.Drawing.Point(0, 0);
            this.scBrowser.Name = "scBrowser";
            this.scBrowser.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scBrowser.Panel1
            // 
            this.scBrowser.Panel1.Controls.Add(this.panelMenu);
            // 
            // scBrowser.Panel2
            // 
            this.scBrowser.Panel2.Controls.Add(this.panelContainer);
            this.scBrowser.Size = new System.Drawing.Size(1008, 729);
            this.scBrowser.SplitterDistance = 30;
            this.scBrowser.SplitterWidth = 1;
            this.scBrowser.TabIndex = 1;
            // 
            // panelMenu
            // 
            this.panelMenu.BackColor = System.Drawing.SystemColors.Window;
            this.panelMenu.Controls.Add(this.btnRefer);
            this.panelMenu.Controls.Add(this.linklblUserID);
            this.panelMenu.Controls.Add(this.btnMessge);
            this.panelMenu.Controls.Add(this.btnLoadTop);
            this.panelMenu.Controls.Add(this.btnLogon);
            this.panelMenu.Controls.Add(this.btnMail);
            this.panelMenu.Controls.Add(this.btnFavor);
            this.panelMenu.Controls.Add(this.btnClear);
            this.panelMenu.Controls.Add(this.btnBoardNavi);
            this.panelMenu.Controls.Add(this.btnSettings);
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMenu.Location = new System.Drawing.Point(0, 0);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(1008, 30);
            this.panelMenu.TabIndex = 0;
            // 
            // btnRefer
            // 
            this.btnRefer.Location = new System.Drawing.Point(624, 4);
            this.btnRefer.Name = "btnRefer";
            this.btnRefer.Size = new System.Drawing.Size(75, 23);
            this.btnRefer.TabIndex = 16;
            this.btnRefer.Text = "Refers";
            this.btnRefer.UseVisualStyleBackColor = true;
            this.btnRefer.Visible = false;
            this.btnRefer.Click += new System.EventHandler(this.btnRefer_Click);
            // 
            // linklblUserID
            // 
            this.linklblUserID.AutoSize = true;
            this.linklblUserID.LinkArea = new System.Windows.Forms.LinkArea(0, 0);
            this.linklblUserID.Location = new System.Drawing.Point(12, 9);
            this.linklblUserID.Name = "linklblUserID";
            this.linklblUserID.Size = new System.Drawing.Size(53, 12);
            this.linklblUserID.TabIndex = 15;
            this.linklblUserID.Text = "Welcome!";
            // 
            // btnMessge
            // 
            this.btnMessge.Location = new System.Drawing.Point(381, 4);
            this.btnMessge.Name = "btnMessge";
            this.btnMessge.Size = new System.Drawing.Size(75, 23);
            this.btnMessge.TabIndex = 14;
            this.btnMessge.TabStop = false;
            this.btnMessge.Text = "Message";
            this.btnMessge.UseVisualStyleBackColor = true;
            this.btnMessge.Click += new System.EventHandler(this.btnMessge_Click);
            // 
            // btnLoadTop
            // 
            this.btnLoadTop.Location = new System.Drawing.Point(222, 4);
            this.btnLoadTop.Name = "btnLoadTop";
            this.btnLoadTop.Size = new System.Drawing.Size(75, 23);
            this.btnLoadTop.TabIndex = 6;
            this.btnLoadTop.Text = "Top 10\'s";
            this.btnLoadTop.UseVisualStyleBackColor = true;
            this.btnLoadTop.Click += new System.EventHandler(this.btnLoadTop_Click);
            // 
            // btnLogon
            // 
            this.btnLogon.Location = new System.Drawing.Point(139, 4);
            this.btnLogon.Name = "btnLogon";
            this.btnLogon.Size = new System.Drawing.Size(79, 23);
            this.btnLogon.TabIndex = 10;
            this.btnLogon.Text = "Log in";
            this.btnLogon.UseVisualStyleBackColor = true;
            this.btnLogon.Click += new System.EventHandler(this.btnLogIn_Click);
            // 
            // btnMail
            // 
            this.btnMail.Location = new System.Drawing.Point(543, 4);
            this.btnMail.Name = "btnMail";
            this.btnMail.Size = new System.Drawing.Size(75, 23);
            this.btnMail.TabIndex = 13;
            this.btnMail.Text = "Mails";
            this.btnMail.UseVisualStyleBackColor = true;
            this.btnMail.Visible = false;
            this.btnMail.Click += new System.EventHandler(this.btnMail_Click);
            // 
            // btnFavor
            // 
            this.btnFavor.Location = new System.Drawing.Point(462, 4);
            this.btnFavor.Name = "btnFavor";
            this.btnFavor.Size = new System.Drawing.Size(75, 23);
            this.btnFavor.TabIndex = 12;
            this.btnFavor.Text = "Favors";
            this.btnFavor.UseVisualStyleBackColor = true;
            this.btnFavor.Visible = false;
            this.btnFavor.Click += new System.EventHandler(this.btnFavor_Click);
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.Location = new System.Drawing.Point(925, 4);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 1;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnBoardNavi
            // 
            this.btnBoardNavi.Location = new System.Drawing.Point(301, 4);
            this.btnBoardNavi.Name = "btnBoardNavi";
            this.btnBoardNavi.Size = new System.Drawing.Size(75, 23);
            this.btnBoardNavi.TabIndex = 11;
            this.btnBoardNavi.Text = "Boards";
            this.btnBoardNavi.UseVisualStyleBackColor = true;
            this.btnBoardNavi.Click += new System.EventHandler(this.btnBoardNavi_Click);
            // 
            // btnSettings
            // 
            this.btnSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSettings.Location = new System.Drawing.Point(845, 4);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(75, 23);
            this.btnSettings.TabIndex = 0;
            this.btnSettings.Text = "Setting";
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // panelContainer
            // 
            this.panelContainer.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.panelContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelContainer.Controls.Add(this.tcTopics);
            this.panelContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContainer.Location = new System.Drawing.Point(0, 0);
            this.panelContainer.Name = "panelContainer";
            this.panelContainer.Size = new System.Drawing.Size(1008, 698);
            this.panelContainer.TabIndex = 1;
            // 
            // TabbedBrowserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.scBrowser);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TabbedBrowserForm";
            this.ShowIcon = true;
            this.ShowInTaskbar = true;
            this.Text = "Browser";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TabbedTopicBrowserForm_FormClosing);
            this.scBrowser.Panel1.ResumeLayout(false);
            this.scBrowser.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scBrowser)).EndInit();
            this.scBrowser.ResumeLayout(false);
            this.panelMenu.ResumeLayout(false);
            this.panelMenu.PerformLayout();
            this.panelContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcTopics;
        //private Nzl.Controls.TabControlEx tcTopics;
        private System.Windows.Forms.SplitContainer scBrowser;
        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Panel panelContainer;
        private System.Windows.Forms.Button btnLoadTop;
        private System.Windows.Forms.Button btnMessge;
        private System.Windows.Forms.Button btnLogon;
        private System.Windows.Forms.Button btnMail;
        private System.Windows.Forms.Button btnFavor;
        private System.Windows.Forms.Button btnBoardNavi;
        private System.Windows.Forms.LinkLabel linklblUserID;
        private System.Windows.Forms.Button btnRefer;
    }
}