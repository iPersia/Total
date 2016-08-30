namespace Nzl.Smth.Controls.Containers
{
    partial class ThreadControlContainer
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel = new System.Windows.Forms.Panel();
            this.panelContainer = new System.Windows.Forms.Panel();
            this.btnOpenInBrowser = new System.Windows.Forms.Button();
            this.scContainer = new System.Windows.Forms.SplitContainer();
            this.panelMenu = new System.Windows.Forms.Panel();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.linklblBoard = new System.Windows.Forms.LinkLabel();
            this.linklblReply = new System.Windows.Forms.LinkLabel();
            this.btnSettings = new System.Windows.Forms.Button();
            this.btnGo = new System.Windows.Forms.Button();
            this.txtGoTo = new System.Windows.Forms.TextBox();
            this.lblPage = new System.Windows.Forms.Label();
            this.btnLast = new System.Windows.Forms.Button();
            this.btnFirst = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrev = new System.Windows.Forms.Button();
            this.panelContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scContainer)).BeginInit();
            this.scContainer.Panel1.SuspendLayout();
            this.scContainer.Panel2.SuspendLayout();
            this.scContainer.SuspendLayout();
            this.panelMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel.Location = new System.Drawing.Point(4, 4);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(841, 351);
            this.panel.TabIndex = 0;
            // 
            // panelContainer
            // 
            this.panelContainer.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panelContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelContainer.Controls.Add(this.panel);
            this.panelContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContainer.Location = new System.Drawing.Point(0, 0);
            this.panelContainer.Name = "panelContainer";
            this.panelContainer.Size = new System.Drawing.Size(850, 388);
            this.panelContainer.TabIndex = 1;
            // 
            // btnOpenInBrowser
            // 
            this.btnOpenInBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenInBrowser.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnOpenInBrowser.Location = new System.Drawing.Point(735, 1);
            this.btnOpenInBrowser.Name = "btnOpenInBrowser";
            this.btnOpenInBrowser.Size = new System.Drawing.Size(33, 23);
            this.btnOpenInBrowser.TabIndex = 8;
            this.btnOpenInBrowser.TabStop = false;
            this.btnOpenInBrowser.Text = "OiB";
            this.btnOpenInBrowser.UseCompatibleTextRendering = true;
            this.btnOpenInBrowser.UseVisualStyleBackColor = true;
            // 
            // scContainer
            // 
            this.scContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.scContainer.IsSplitterFixed = true;
            this.scContainer.Location = new System.Drawing.Point(0, 0);
            this.scContainer.Name = "scContainer";
            this.scContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scContainer.Panel1
            // 
            this.scContainer.Panel1.Controls.Add(this.panelMenu);
            // 
            // scContainer.Panel2
            // 
            this.scContainer.Panel2.Controls.Add(this.panelContainer);
            this.scContainer.Size = new System.Drawing.Size(850, 414);
            this.scContainer.SplitterDistance = 25;
            this.scContainer.SplitterWidth = 1;
            this.scContainer.TabIndex = 2;
            this.scContainer.TabStop = false;
            // 
            // panelMenu
            // 
            this.panelMenu.BackColor = System.Drawing.SystemColors.Window;
            this.panelMenu.Controls.Add(this.btnRefresh);
            this.panelMenu.Controls.Add(this.linklblBoard);
            this.panelMenu.Controls.Add(this.linklblReply);
            this.panelMenu.Controls.Add(this.btnSettings);
            this.panelMenu.Controls.Add(this.btnOpenInBrowser);
            this.panelMenu.Controls.Add(this.btnGo);
            this.panelMenu.Controls.Add(this.txtGoTo);
            this.panelMenu.Controls.Add(this.lblPage);
            this.panelMenu.Controls.Add(this.btnLast);
            this.panelMenu.Controls.Add(this.btnFirst);
            this.panelMenu.Controls.Add(this.btnNext);
            this.panelMenu.Controls.Add(this.btnPrev);
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMenu.Location = new System.Drawing.Point(0, 0);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(850, 25);
            this.panelMenu.TabIndex = 0;
            // 
            // btnRefresh
            // 
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnRefresh.Location = new System.Drawing.Point(273, 1);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(56, 23);
            this.btnRefresh.TabIndex = 18;
            this.btnRefresh.TabStop = false;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseCompatibleTextRendering = true;
            this.btnRefresh.UseVisualStyleBackColor = true;
            // 
            // linklblBoard
            // 
            this.linklblBoard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linklblBoard.Location = new System.Drawing.Point(445, 7);
            this.linklblBoard.Name = "linklblBoard";
            this.linklblBoard.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.linklblBoard.Size = new System.Drawing.Size(200, 12);
            this.linklblBoard.TabIndex = 17;
            this.linklblBoard.TabStop = true;
            this.linklblBoard.Text = "金融产品及个人理财";
            this.linklblBoard.Visible = false;
            // 
            // linklblReply
            // 
            this.linklblReply.AutoSize = true;
            this.linklblReply.Location = new System.Drawing.Point(336, 7);
            this.linklblReply.Name = "linklblReply";
            this.linklblReply.Size = new System.Drawing.Size(35, 12);
            this.linklblReply.TabIndex = 16;
            this.linklblReply.TabStop = true;
            this.linklblReply.Text = "Reply";
            this.linklblReply.Visible = false;
            // 
            // btnSettings
            // 
            this.btnSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSettings.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnSettings.Location = new System.Drawing.Point(651, 1);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(75, 23);
            this.btnSettings.TabIndex = 11;
            this.btnSettings.TabStop = false;
            this.btnSettings.Text = "Settings";
            this.btnSettings.UseCompatibleTextRendering = true;
            this.btnSettings.UseVisualStyleBackColor = true;
            // 
            // btnGo
            // 
            this.btnGo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGo.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnGo.Location = new System.Drawing.Point(806, 1);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(30, 23);
            this.btnGo.TabIndex = 10;
            this.btnGo.TabStop = false;
            this.btnGo.Text = "Go";
            this.btnGo.UseCompatibleTextRendering = true;
            this.btnGo.UseVisualStyleBackColor = true;
            // 
            // txtGoTo
            // 
            this.txtGoTo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGoTo.Location = new System.Drawing.Point(775, 2);
            this.txtGoTo.Name = "txtGoTo";
            this.txtGoTo.Size = new System.Drawing.Size(25, 21);
            this.txtGoTo.TabIndex = 9;
            this.txtGoTo.TabStop = false;
            this.txtGoTo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblPage
            // 
            this.lblPage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPage.AutoSize = true;
            this.lblPage.Location = new System.Drawing.Point(116, 7);
            this.lblPage.Name = "lblPage";
            this.lblPage.Size = new System.Drawing.Size(47, 12);
            this.lblPage.TabIndex = 8;
            this.lblPage.Text = "001/100";
            this.lblPage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnLast
            // 
            this.btnLast.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnLast.Location = new System.Drawing.Point(223, 1);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(45, 23);
            this.btnLast.TabIndex = 7;
            this.btnLast.TabStop = false;
            this.btnLast.Text = "Last";
            this.btnLast.UseCompatibleTextRendering = true;
            this.btnLast.UseVisualStyleBackColor = true;
            // 
            // btnFirst
            // 
            this.btnFirst.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnFirst.Location = new System.Drawing.Point(11, 1);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(45, 23);
            this.btnFirst.TabIndex = 6;
            this.btnFirst.TabStop = false;
            this.btnFirst.Text = "First";
            this.btnFirst.UseCompatibleTextRendering = true;
            this.btnFirst.UseVisualStyleBackColor = true;
            // 
            // btnNext
            // 
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnNext.Location = new System.Drawing.Point(173, 1);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(45, 23);
            this.btnNext.TabIndex = 3;
            this.btnNext.TabStop = false;
            this.btnNext.Text = "Next";
            this.btnNext.UseCompatibleTextRendering = true;
            this.btnNext.UseVisualStyleBackColor = true;
            // 
            // btnPrev
            // 
            this.btnPrev.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnPrev.Location = new System.Drawing.Point(61, 1);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(45, 23);
            this.btnPrev.TabIndex = 0;
            this.btnPrev.TabStop = false;
            this.btnPrev.Text = "Prev";
            this.btnPrev.UseCompatibleTextRendering = true;
            this.btnPrev.UseVisualStyleBackColor = true;
            // 
            // ThreadControlContainer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.scContainer);
            this.DoubleBuffered = true;
            this.Name = "ThreadControlContainer";
            this.Size = new System.Drawing.Size(850, 414);
            this.panelContainer.ResumeLayout(false);
            this.scContainer.Panel1.ResumeLayout(false);
            this.scContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scContainer)).EndInit();
            this.scContainer.ResumeLayout(false);
            this.panelMenu.ResumeLayout(false);
            this.panelMenu.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.Panel panelContainer;
        private System.Windows.Forms.Button btnOpenInBrowser;
        private System.Windows.Forms.SplitContainer scContainer;
        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.TextBox txtGoTo;
        private System.Windows.Forms.Label lblPage;
        private System.Windows.Forms.Button btnLast;
        private System.Windows.Forms.Button btnFirst;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.LinkLabel linklblReply;
        private System.Windows.Forms.LinkLabel linklblBoard;
        private System.Windows.Forms.Button btnRefresh;
    }
}
