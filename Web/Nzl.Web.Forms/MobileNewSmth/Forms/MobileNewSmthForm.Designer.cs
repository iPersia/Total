namespace Nzl.Web.Forms.MobileNewSmth.Forms
{
    partial class MobileNewSmthForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MobileNewSmthForm));
            this.btnLogon = new System.Windows.Forms.Button();
            this.tabctrlPages = new System.Windows.Forms.TabControl();
            this.btnBoardNavi = new System.Windows.Forms.Button();
            this.btnLoadTop = new System.Windows.Forms.Button();
            this.btnFavor = new System.Windows.Forms.Button();
            this.btnMail = new System.Windows.Forms.Button();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnMessge = new System.Windows.Forms.Button();
            this.linklblUserID = new System.Windows.Forms.LinkLabel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel = new System.Windows.Forms.Panel();
            this.bwFetchPage = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnLogon
            // 
            this.btnLogon.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnLogon.Location = new System.Drawing.Point(543, 1);
            this.btnLogon.Name = "btnLogon";
            this.btnLogon.Size = new System.Drawing.Size(79, 23);
            this.btnLogon.TabIndex = 0;
            this.btnLogon.Text = "Login&&Out";
            this.btnLogon.UseVisualStyleBackColor = true;
            this.btnLogon.Click += new System.EventHandler(this.btnLogIn_Click);
            // 
            // tabctrlPages
            // 
            this.tabctrlPages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabctrlPages.Location = new System.Drawing.Point(0, 0);
            this.tabctrlPages.Name = "tabctrlPages";
            this.tabctrlPages.SelectedIndex = 0;
            this.tabctrlPages.Size = new System.Drawing.Size(622, 416);
            this.tabctrlPages.TabIndex = 3;
            // 
            // btnBoardNavi
            // 
            this.btnBoardNavi.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnBoardNavi.Location = new System.Drawing.Point(301, 1);
            this.btnBoardNavi.Name = "btnBoardNavi";
            this.btnBoardNavi.Size = new System.Drawing.Size(75, 23);
            this.btnBoardNavi.TabIndex = 4;
            this.btnBoardNavi.Text = "Boards";
            this.btnBoardNavi.UseVisualStyleBackColor = true;
            this.btnBoardNavi.Click += new System.EventHandler(this.btnBoardNavi_Click);
            // 
            // btnLoadTop
            // 
            this.btnLoadTop.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnLoadTop.Location = new System.Drawing.Point(382, 1);
            this.btnLoadTop.Name = "btnLoadTop";
            this.btnLoadTop.Size = new System.Drawing.Size(75, 23);
            this.btnLoadTop.TabIndex = 5;
            this.btnLoadTop.Text = "Top 10\'s";
            this.btnLoadTop.UseVisualStyleBackColor = true;
            this.btnLoadTop.Click += new System.EventHandler(this.btnLoadTop_Click);
            // 
            // btnFavor
            // 
            this.btnFavor.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnFavor.Location = new System.Drawing.Point(220, 1);
            this.btnFavor.Name = "btnFavor";
            this.btnFavor.Size = new System.Drawing.Size(75, 23);
            this.btnFavor.TabIndex = 6;
            this.btnFavor.Text = "Favors";
            this.btnFavor.UseVisualStyleBackColor = true;
            this.btnFavor.Visible = false;
            this.btnFavor.Click += new System.EventHandler(this.btnFavor_Click);
            // 
            // btnMail
            // 
            this.btnMail.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnMail.Location = new System.Drawing.Point(139, 1);
            this.btnMail.Name = "btnMail";
            this.btnMail.Size = new System.Drawing.Size(75, 23);
            this.btnMail.TabIndex = 7;
            this.btnMail.Text = "Mails";
            this.btnMail.UseVisualStyleBackColor = true;
            this.btnMail.Visible = false;
            this.btnMail.Click += new System.EventHandler(this.btnMail_Click);
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer.IsSplitterFixed = true;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.panel1);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.panel2);
            this.splitContainer.Size = new System.Drawing.Size(624, 444);
            this.splitContainer.SplitterDistance = 25;
            this.splitContainer.SplitterWidth = 1;
            this.splitContainer.TabIndex = 8;
            this.splitContainer.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnMessge);
            this.panel1.Controls.Add(this.linklblUserID);
            this.panel1.Controls.Add(this.btnLogon);
            this.panel1.Controls.Add(this.btnMail);
            this.panel1.Controls.Add(this.btnFavor);
            this.panel1.Controls.Add(this.btnLoadTop);
            this.panel1.Controls.Add(this.btnBoardNavi);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(624, 25);
            this.panel1.TabIndex = 0;
            // 
            // btnMessge
            // 
            this.btnMessge.Location = new System.Drawing.Point(462, 1);
            this.btnMessge.Name = "btnMessge";
            this.btnMessge.Size = new System.Drawing.Size(75, 23);
            this.btnMessge.TabIndex = 9;
            this.btnMessge.TabStop = false;
            this.btnMessge.Text = "Message";
            this.btnMessge.UseVisualStyleBackColor = true;
            this.btnMessge.Click += new System.EventHandler(this.btnMessge_Click);
            // 
            // linklblUserID
            // 
            this.linklblUserID.AutoSize = true;
            this.linklblUserID.Location = new System.Drawing.Point(12, 7);
            this.linklblUserID.Name = "linklblUserID";
            this.linklblUserID.Size = new System.Drawing.Size(53, 12);
            this.linklblUserID.TabIndex = 8;
            this.linklblUserID.TabStop = true;
            this.linklblUserID.Text = "Welcome!";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.tabctrlPages);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(624, 418);
            this.panel2.TabIndex = 4;
            // 
            // panel
            // 
            this.panel.Controls.Add(this.splitContainer);
            this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel.Location = new System.Drawing.Point(0, 0);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(624, 444);
            this.panel.TabIndex = 9;
            // 
            // MobileNewSmthForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(624, 444);
            this.Controls.Add(this.panel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MobileNewSmthForm";
            this.Text = "New Smth";
            this.Activated += new System.EventHandler(this.MobileNewSmthForm_Activated);
            this.Shown += new System.EventHandler(this.MobileNewSmthForm_Shown);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnLogon;
        private System.Windows.Forms.TabControl tabctrlPages;
        private System.Windows.Forms.Button btnBoardNavi;
        private System.Windows.Forms.Button btnLoadTop;
        private System.Windows.Forms.Button btnFavor;
        private System.Windows.Forms.Button btnMail;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.Panel panel2;
        private System.ComponentModel.BackgroundWorker bwFetchPage;
        private System.Windows.Forms.LinkLabel linklblUserID;
        private System.Windows.Forms.Button btnMessge;
    }
}