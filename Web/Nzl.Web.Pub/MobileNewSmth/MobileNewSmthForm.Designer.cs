namespace Nzl.Web.Pub.MobileNewSmth
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
            this.tabctrlPages = new System.Windows.Forms.TabControl();
            this.btnBoardNavi = new System.Windows.Forms.Button();
            this.btnLoadTop = new System.Windows.Forms.Button();
            this.btnFavor = new System.Windows.Forms.Button();
            this.btnMail = new System.Windows.Forms.Button();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.btnSetting = new System.Windows.Forms.Button();
            this.btnLogIn = new System.Windows.Forms.Button();
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
            // tabctrlPages
            // 
            this.tabctrlPages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabctrlPages.Location = new System.Drawing.Point(0, 0);
            this.tabctrlPages.Name = "tabctrlPages";
            this.tabctrlPages.SelectedIndex = 0;
            this.tabctrlPages.Size = new System.Drawing.Size(702, 416);
            this.tabctrlPages.TabIndex = 3;
            // 
            // btnBoardNavi
            // 
            this.btnBoardNavi.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnBoardNavi.Location = new System.Drawing.Point(387, 1);
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
            this.btnLoadTop.Location = new System.Drawing.Point(468, 1);
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
            this.btnFavor.Location = new System.Drawing.Point(306, 1);
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
            this.btnMail.Location = new System.Drawing.Point(225, 1);
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
            this.splitContainer.Size = new System.Drawing.Size(704, 444);
            this.splitContainer.SplitterDistance = 25;
            this.splitContainer.SplitterWidth = 1;
            this.splitContainer.TabIndex = 8;
            this.splitContainer.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblWelcome);
            this.panel1.Controls.Add(this.btnSetting);
            this.panel1.Controls.Add(this.btnLogIn);
            this.panel1.Controls.Add(this.btnMail);
            this.panel1.Controls.Add(this.btnFavor);
            this.panel1.Controls.Add(this.btnLoadTop);
            this.panel1.Controls.Add(this.btnBoardNavi);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(704, 25);
            this.panel1.TabIndex = 0;
            // 
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Location = new System.Drawing.Point(11, 7);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(53, 12);
            this.lblWelcome.TabIndex = 9;
            this.lblWelcome.Text = "Welcome!";
            // 
            // btnSetting
            // 
            this.btnSetting.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnSetting.Location = new System.Drawing.Point(547, 1);
            this.btnSetting.Name = "btnSetting";
            this.btnSetting.Size = new System.Drawing.Size(75, 23);
            this.btnSetting.TabIndex = 8;
            this.btnSetting.Text = "Settings";
            this.btnSetting.UseVisualStyleBackColor = true;
            this.btnSetting.Click += new System.EventHandler(this.btnSetting_Click);
            // 
            // btnLogIn
            // 
            this.btnLogIn.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnLogIn.Location = new System.Drawing.Point(626, 1);
            this.btnLogIn.Name = "btnLogIn";
            this.btnLogIn.Size = new System.Drawing.Size(75, 23);
            this.btnLogIn.TabIndex = 0;
            this.btnLogIn.Text = "Log In";
            this.btnLogIn.UseVisualStyleBackColor = true;
            this.btnLogIn.Click += new System.EventHandler(this.btnLogIn_Click);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.tabctrlPages);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(704, 418);
            this.panel2.TabIndex = 4;
            // 
            // panel
            // 
            this.panel.Controls.Add(this.splitContainer);
            this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel.Location = new System.Drawing.Point(0, 0);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(704, 444);
            this.panel.TabIndex = 9;
            // 
            // MobileNewSmthForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(704, 444);
            this.Controls.Add(this.panel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
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
        private System.Windows.Forms.Button btnSetting;
        private System.Windows.Forms.Button btnLogIn;
        private System.Windows.Forms.Label lblWelcome;
    }
}