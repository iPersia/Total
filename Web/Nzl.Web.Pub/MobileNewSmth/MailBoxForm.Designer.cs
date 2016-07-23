namespace Nzl.Web.Pub.MobileNewSmth
{
    partial class MailBoxForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MailBoxForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnNew = new System.Windows.Forms.Button();
            this.tcMailBox = new System.Windows.Forms.TabControl();
            this.tpInbox = new System.Windows.Forms.TabPage();
            this.panelInbox = new System.Windows.Forms.Panel();
            this.tpOutbox = new System.Windows.Forms.TabPage();
            this.panelOutbox = new System.Windows.Forms.Panel();
            this.tpTrash = new System.Windows.Forms.TabPage();
            this.panelTrash = new System.Windows.Forms.Panel();
            this.lblPage = new System.Windows.Forms.Label();
            this.btnFirst = new System.Windows.Forms.Button();
            this.btnLast = new System.Windows.Forms.Button();
            this.btnPrev = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.panel = new System.Windows.Forms.Panel();
            this.bwFetchPage = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tcMailBox.SuspendLayout();
            this.tpInbox.SuspendLayout();
            this.tpOutbox.SuspendLayout();
            this.tpTrash.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel);
            this.splitContainer1.Size = new System.Drawing.Size(634, 480);
            this.splitContainer1.SplitterDistance = 25;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 0;
            this.splitContainer1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnNew);
            this.panel2.Controls.Add(this.tcMailBox);
            this.panel2.Controls.Add(this.lblPage);
            this.panel2.Controls.Add(this.btnFirst);
            this.panel2.Controls.Add(this.btnLast);
            this.panel2.Controls.Add(this.btnPrev);
            this.panel2.Controls.Add(this.btnNext);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(634, 25);
            this.panel2.TabIndex = 0;
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(267, 1);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(75, 23);
            this.btnNew.TabIndex = 0;
            this.btnNew.Text = "New";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // tcMailBox
            // 
            this.tcMailBox.Controls.Add(this.tpInbox);
            this.tcMailBox.Controls.Add(this.tpOutbox);
            this.tcMailBox.Controls.Add(this.tpTrash);
            this.tcMailBox.Location = new System.Drawing.Point(5, 5);
            this.tcMailBox.Name = "tcMailBox";
            this.tcMailBox.SelectedIndex = 0;
            this.tcMailBox.Size = new System.Drawing.Size(157, 20);
            this.tcMailBox.TabIndex = 0;
            this.tcMailBox.SelectedIndexChanged += new System.EventHandler(this.tcMailBox_SelectedIndexChanged);
            // 
            // tpInbox
            // 
            this.tpInbox.Controls.Add(this.panelInbox);
            this.tpInbox.Location = new System.Drawing.Point(4, 22);
            this.tpInbox.Name = "tpInbox";
            this.tpInbox.Size = new System.Drawing.Size(149, 0);
            this.tpInbox.TabIndex = 0;
            this.tpInbox.Tag = "http://m.newsmth.net/mail/inbox";
            this.tpInbox.Text = "InBox";
            this.tpInbox.UseVisualStyleBackColor = true;
            // 
            // panelInbox
            // 
            this.panelInbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelInbox.Location = new System.Drawing.Point(0, 0);
            this.panelInbox.Name = "panelInbox";
            this.panelInbox.Size = new System.Drawing.Size(149, 0);
            this.panelInbox.TabIndex = 1;
            // 
            // tpOutbox
            // 
            this.tpOutbox.Controls.Add(this.panelOutbox);
            this.tpOutbox.Location = new System.Drawing.Point(4, 22);
            this.tpOutbox.Name = "tpOutbox";
            this.tpOutbox.Size = new System.Drawing.Size(149, 0);
            this.tpOutbox.TabIndex = 1;
            this.tpOutbox.Tag = "http://m.newsmth.net/mail/outbox";
            this.tpOutbox.Text = "Outbox";
            this.tpOutbox.UseVisualStyleBackColor = true;
            // 
            // panelOutbox
            // 
            this.panelOutbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelOutbox.Location = new System.Drawing.Point(0, 0);
            this.panelOutbox.Name = "panelOutbox";
            this.panelOutbox.Size = new System.Drawing.Size(149, 0);
            this.panelOutbox.TabIndex = 1;
            // 
            // tpTrash
            // 
            this.tpTrash.Controls.Add(this.panelTrash);
            this.tpTrash.Location = new System.Drawing.Point(4, 22);
            this.tpTrash.Name = "tpTrash";
            this.tpTrash.Size = new System.Drawing.Size(149, 0);
            this.tpTrash.TabIndex = 2;
            this.tpTrash.Tag = "http://m.newsmth.net/mail/deleted";
            this.tpTrash.Text = "Trash";
            this.tpTrash.UseVisualStyleBackColor = true;
            // 
            // panelTrash
            // 
            this.panelTrash.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTrash.Location = new System.Drawing.Point(0, 0);
            this.panelTrash.Name = "panelTrash";
            this.panelTrash.Size = new System.Drawing.Size(149, 0);
            this.panelTrash.TabIndex = 1;
            // 
            // lblPage
            // 
            this.lblPage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPage.AutoSize = true;
            this.lblPage.Location = new System.Drawing.Point(477, 7);
            this.lblPage.Name = "lblPage";
            this.lblPage.Size = new System.Drawing.Size(35, 12);
            this.lblPage.TabIndex = 15;
            this.lblPage.Text = "01/10";
            this.lblPage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnFirst
            // 
            this.btnFirst.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnFirst.Location = new System.Drawing.Point(358, 1);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(50, 23);
            this.btnFirst.TabIndex = 13;
            this.btnFirst.TabStop = false;
            this.btnFirst.Text = "First";
            this.btnFirst.UseCompatibleTextRendering = true;
            this.btnFirst.UseVisualStyleBackColor = true;
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // btnLast
            // 
            this.btnLast.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnLast.Location = new System.Drawing.Point(579, 1);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(50, 23);
            this.btnLast.TabIndex = 14;
            this.btnLast.TabStop = false;
            this.btnLast.Text = "Last";
            this.btnLast.UseCompatibleTextRendering = true;
            this.btnLast.UseVisualStyleBackColor = true;
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // btnPrev
            // 
            this.btnPrev.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnPrev.Location = new System.Drawing.Point(414, 1);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(50, 23);
            this.btnPrev.TabIndex = 11;
            this.btnPrev.TabStop = false;
            this.btnPrev.Text = "Prev";
            this.btnPrev.UseCompatibleTextRendering = true;
            this.btnPrev.UseVisualStyleBackColor = true;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // btnNext
            // 
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnNext.Location = new System.Drawing.Point(523, 1);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(50, 23);
            this.btnNext.TabIndex = 12;
            this.btnNext.TabStop = false;
            this.btnNext.Text = "Next";
            this.btnNext.UseCompatibleTextRendering = true;
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // panel
            // 
            this.panel.AutoScroll = true;
            this.panel.AutoSize = true;
            this.panel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel.Location = new System.Drawing.Point(0, 0);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(634, 454);
            this.panel.TabIndex = 0;
            // 
            // MailBoxForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 480);
            this.Controls.Add(this.splitContainer1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MailBoxForm";
            this.ShowInTaskbar = false;
            this.Text = "Mail Box";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tcMailBox.ResumeLayout(false);
            this.tpInbox.ResumeLayout(false);
            this.tpOutbox.ResumeLayout(false);
            this.tpTrash.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.TabControl tcMailBox;
        private System.Windows.Forms.TabPage tpInbox;
        private System.Windows.Forms.TabPage tpTrash;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TabPage tpOutbox;
        private System.Windows.Forms.Panel panelInbox;
        private System.Windows.Forms.Panel panelOutbox;
        private System.Windows.Forms.Panel panelTrash;
        private System.ComponentModel.BackgroundWorker bwFetchPage;
        private System.Windows.Forms.Label lblPage;
        private System.Windows.Forms.Button btnLast;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Button btnFirst;
    }
}