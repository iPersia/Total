namespace Nzl.Web.Smth.Forms
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.linklblUserID = new System.Windows.Forms.LinkLabel();
            this.btnMessge = new System.Windows.Forms.Button();
            this.btnLoadTop = new System.Windows.Forms.Button();
            this.btnLogon = new System.Windows.Forms.Button();
            this.btnMail = new System.Windows.Forms.Button();
            this.btnHome = new System.Windows.Forms.Button();
            this.btnFavor = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnBoardNavi = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.scBrowser)).BeginInit();
            this.scBrowser.Panel1.SuspendLayout();
            this.scBrowser.Panel2.SuspendLayout();
            this.scBrowser.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcTopics
            // 
            this.tcTopics.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcTopics.Location = new System.Drawing.Point(0, 0);
            this.tcTopics.Name = "tcTopics";
            this.tcTopics.SelectedIndex = 0;
            this.tcTopics.Size = new System.Drawing.Size(1047, 725);
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
            this.scBrowser.Panel1.Controls.Add(this.panel1);
            // 
            // scBrowser.Panel2
            // 
            this.scBrowser.Panel2.Controls.Add(this.panel2);
            this.scBrowser.Size = new System.Drawing.Size(1049, 758);
            this.scBrowser.SplitterDistance = 30;
            this.scBrowser.SplitterWidth = 1;
            this.scBrowser.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.linklblUserID);
            this.panel1.Controls.Add(this.btnMessge);
            this.panel1.Controls.Add(this.btnLoadTop);
            this.panel1.Controls.Add(this.btnLogon);
            this.panel1.Controls.Add(this.btnMail);
            this.panel1.Controls.Add(this.btnHome);
            this.panel1.Controls.Add(this.btnFavor);
            this.panel1.Controls.Add(this.btnClear);
            this.panel1.Controls.Add(this.btnBoardNavi);
            this.panel1.Controls.Add(this.btnExit);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1049, 30);
            this.panel1.TabIndex = 0;
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
            this.btnMail.Location = new System.Drawing.Point(460, 4);
            this.btnMail.Name = "btnMail";
            this.btnMail.Size = new System.Drawing.Size(75, 23);
            this.btnMail.TabIndex = 13;
            this.btnMail.Text = "Mails";
            this.btnMail.UseVisualStyleBackColor = true;
            this.btnMail.Visible = false;
            this.btnMail.Click += new System.EventHandler(this.btnMail_Click);
            // 
            // btnHome
            // 
            this.btnHome.Location = new System.Drawing.Point(621, 3);
            this.btnHome.Name = "btnHome";
            this.btnHome.Size = new System.Drawing.Size(75, 23);
            this.btnHome.TabIndex = 2;
            this.btnHome.Text = "Home";
            this.btnHome.UseVisualStyleBackColor = true;
            this.btnHome.Visible = false;
            this.btnHome.Click += new System.EventHandler(this.btnHome_Click);
            // 
            // btnFavor
            // 
            this.btnFavor.Location = new System.Drawing.Point(540, 4);
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
            this.btnClear.Location = new System.Drawing.Point(886, 3);
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
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Location = new System.Drawing.Point(966, 3);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 0;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.tcTopics);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1049, 727);
            this.panel2.TabIndex = 1;
            // 
            // TabbedBrowserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1049, 758);
            this.Controls.Add(this.scBrowser);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TabbedBrowserForm";
            this.Text = "Browser";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TabbedTopicBrowserForm_FormClosing);
            this.scBrowser.Panel1.ResumeLayout(false);
            this.scBrowser.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scBrowser)).EndInit();
            this.scBrowser.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcTopics;
        private System.Windows.Forms.SplitContainer scBrowser;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnHome;
        private System.Windows.Forms.Button btnLoadTop;
        private System.Windows.Forms.Button btnMessge;
        private System.Windows.Forms.Button btnLogon;
        private System.Windows.Forms.Button btnMail;
        private System.Windows.Forms.Button btnFavor;
        private System.Windows.Forms.Button btnBoardNavi;
        private System.Windows.Forms.LinkLabel linklblUserID;
    }
}