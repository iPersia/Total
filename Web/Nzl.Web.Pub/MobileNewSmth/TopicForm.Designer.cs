namespace Nzl.Web.Pub.MobileNewSmth
{
    partial class TopicForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TopicForm));
            this.panel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnOpenInBrowser = new System.Windows.Forms.Button();
            this.btnGrow = new System.Windows.Forms.Button();
            this.btnShrink = new System.Windows.Forms.Button();
            this.btnGo1 = new System.Windows.Forms.Button();
            this.txtGoTo1 = new System.Windows.Forms.TextBox();
            this.lblPage1 = new System.Windows.Forms.Label();
            this.btnLast1 = new System.Windows.Forms.Button();
            this.btnFirst1 = new System.Windows.Forms.Button();
            this.btnAll1 = new System.Windows.Forms.Button();
            this.btnNext1 = new System.Windows.Forms.Button();
            this.btnPrev1 = new System.Windows.Forms.Button();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.panelContainer = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnReply = new System.Windows.Forms.Button();
            this.lblUser = new System.Windows.Forms.Label();
            this.txtGoTo2 = new System.Windows.Forms.TextBox();
            this.btnGo2 = new System.Windows.Forms.Button();
            this.lblPage2 = new System.Windows.Forms.Label();
            this.btnLast2 = new System.Windows.Forms.Button();
            this.btnFirst2 = new System.Windows.Forms.Button();
            this.btnNext2 = new System.Windows.Forms.Button();
            this.btnAll2 = new System.Windows.Forms.Button();
            this.btnPrev2 = new System.Windows.Forms.Button();
            this.bwFetchPage = new System.ComponentModel.BackgroundWorker();
            this.panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.panelContainer.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel.Controls.Add(this.label1);
            this.panel.Location = new System.Drawing.Point(3, 3);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(628, 351);
            this.panel.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(276, 181);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Loading...";
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
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(634, 454);
            this.splitContainer1.SplitterDistance = 25;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 1;
            this.splitContainer1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnOpenInBrowser);
            this.panel1.Controls.Add(this.btnGrow);
            this.panel1.Controls.Add(this.btnShrink);
            this.panel1.Controls.Add(this.btnGo1);
            this.panel1.Controls.Add(this.txtGoTo1);
            this.panel1.Controls.Add(this.lblPage1);
            this.panel1.Controls.Add(this.btnLast1);
            this.panel1.Controls.Add(this.btnFirst1);
            this.panel1.Controls.Add(this.btnAll1);
            this.panel1.Controls.Add(this.btnNext1);
            this.panel1.Controls.Add(this.btnPrev1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(634, 25);
            this.panel1.TabIndex = 0;
            // 
            // btnOpenInBrowser
            // 
            this.btnOpenInBrowser.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnOpenInBrowser.Location = new System.Drawing.Point(403, 2);
            this.btnOpenInBrowser.Name = "btnOpenInBrowser";
            this.btnOpenInBrowser.Size = new System.Drawing.Size(33, 23);
            this.btnOpenInBrowser.TabIndex = 8;
            this.btnOpenInBrowser.TabStop = false;
            this.btnOpenInBrowser.Text = "OiB";
            this.btnOpenInBrowser.UseCompatibleTextRendering = true;
            this.btnOpenInBrowser.UseVisualStyleBackColor = true;
            this.btnOpenInBrowser.Click += new System.EventHandler(this.btnOpenInBrowser_Click);
            // 
            // btnGrow
            // 
            this.btnGrow.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnGrow.Location = new System.Drawing.Point(438, 2);
            this.btnGrow.Name = "btnGrow";
            this.btnGrow.Size = new System.Drawing.Size(30, 23);
            this.btnGrow.TabIndex = 15;
            this.btnGrow.TabStop = false;
            this.btnGrow.Text = "+";
            this.btnGrow.UseCompatibleTextRendering = true;
            this.btnGrow.UseVisualStyleBackColor = true;
            this.btnGrow.Click += new System.EventHandler(this.btnGrow_Click);
            // 
            // btnShrink
            // 
            this.btnShrink.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnShrink.Location = new System.Drawing.Point(470, 2);
            this.btnShrink.Name = "btnShrink";
            this.btnShrink.Size = new System.Drawing.Size(30, 23);
            this.btnShrink.TabIndex = 14;
            this.btnShrink.TabStop = false;
            this.btnShrink.Text = "-";
            this.btnShrink.UseCompatibleTextRendering = true;
            this.btnShrink.UseVisualStyleBackColor = true;
            this.btnShrink.Click += new System.EventHandler(this.btnShrink_Click);
            // 
            // btnGo1
            // 
            this.btnGo1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnGo1.Location = new System.Drawing.Point(535, 2);
            this.btnGo1.Name = "btnGo1";
            this.btnGo1.Size = new System.Drawing.Size(30, 23);
            this.btnGo1.TabIndex = 10;
            this.btnGo1.TabStop = false;
            this.btnGo1.Text = "Go";
            this.btnGo1.UseCompatibleTextRendering = true;
            this.btnGo1.UseVisualStyleBackColor = true;
            this.btnGo1.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // txtGoTo1
            // 
            this.txtGoTo1.Location = new System.Drawing.Point(504, 3);
            this.txtGoTo1.Name = "txtGoTo1";
            this.txtGoTo1.Size = new System.Drawing.Size(25, 21);
            this.txtGoTo1.TabIndex = 9;
            this.txtGoTo1.TabStop = false;
            this.txtGoTo1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblPage1
            // 
            this.lblPage1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPage1.AutoSize = true;
            this.lblPage1.Location = new System.Drawing.Point(182, 8);
            this.lblPage1.Name = "lblPage1";
            this.lblPage1.Size = new System.Drawing.Size(47, 12);
            this.lblPage1.TabIndex = 8;
            this.lblPage1.Text = "001/100";
            this.lblPage1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnLast1
            // 
            this.btnLast1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnLast1.Location = new System.Drawing.Point(324, 2);
            this.btnLast1.Name = "btnLast1";
            this.btnLast1.Size = new System.Drawing.Size(75, 23);
            this.btnLast1.TabIndex = 7;
            this.btnLast1.TabStop = false;
            this.btnLast1.Text = "Last";
            this.btnLast1.UseCompatibleTextRendering = true;
            this.btnLast1.UseVisualStyleBackColor = true;
            this.btnLast1.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // btnFirst1
            // 
            this.btnFirst1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnFirst1.Location = new System.Drawing.Point(11, 2);
            this.btnFirst1.Name = "btnFirst1";
            this.btnFirst1.Size = new System.Drawing.Size(75, 23);
            this.btnFirst1.TabIndex = 6;
            this.btnFirst1.TabStop = false;
            this.btnFirst1.Text = "First";
            this.btnFirst1.UseCompatibleTextRendering = true;
            this.btnFirst1.UseVisualStyleBackColor = true;
            this.btnFirst1.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // btnAll1
            // 
            this.btnAll1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnAll1.Location = new System.Drawing.Point(571, 2);
            this.btnAll1.Name = "btnAll1";
            this.btnAll1.Size = new System.Drawing.Size(50, 23);
            this.btnAll1.TabIndex = 5;
            this.btnAll1.TabStop = false;
            this.btnAll1.Text = "All";
            this.btnAll1.UseCompatibleTextRendering = true;
            this.btnAll1.UseVisualStyleBackColor = true;
            this.btnAll1.Click += new System.EventHandler(this.btnAll_Click);
            // 
            // btnNext1
            // 
            this.btnNext1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnNext1.Location = new System.Drawing.Point(243, 2);
            this.btnNext1.Name = "btnNext1";
            this.btnNext1.Size = new System.Drawing.Size(75, 23);
            this.btnNext1.TabIndex = 3;
            this.btnNext1.TabStop = false;
            this.btnNext1.Text = "Next";
            this.btnNext1.UseCompatibleTextRendering = true;
            this.btnNext1.UseVisualStyleBackColor = true;
            this.btnNext1.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrev1
            // 
            this.btnPrev1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnPrev1.Location = new System.Drawing.Point(92, 2);
            this.btnPrev1.Name = "btnPrev1";
            this.btnPrev1.Size = new System.Drawing.Size(75, 23);
            this.btnPrev1.TabIndex = 0;
            this.btnPrev1.TabStop = false;
            this.btnPrev1.Text = "Prev";
            this.btnPrev1.UseCompatibleTextRendering = true;
            this.btnPrev1.UseVisualStyleBackColor = true;
            this.btnPrev1.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.panelContainer);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.panel2);
            this.splitContainer2.Size = new System.Drawing.Size(634, 428);
            this.splitContainer2.SplitterDistance = 402;
            this.splitContainer2.SplitterWidth = 1;
            this.splitContainer2.TabIndex = 0;
            this.splitContainer2.TabStop = false;
            // 
            // panelContainer
            // 
            this.panelContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelContainer.Controls.Add(this.panel);
            this.panelContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContainer.Location = new System.Drawing.Point(0, 0);
            this.panelContainer.Name = "panelContainer";
            this.panelContainer.Size = new System.Drawing.Size(634, 402);
            this.panelContainer.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnReply);
            this.panel2.Controls.Add(this.lblUser);
            this.panel2.Controls.Add(this.txtGoTo2);
            this.panel2.Controls.Add(this.btnGo2);
            this.panel2.Controls.Add(this.lblPage2);
            this.panel2.Controls.Add(this.btnLast2);
            this.panel2.Controls.Add(this.btnFirst2);
            this.panel2.Controls.Add(this.btnNext2);
            this.panel2.Controls.Add(this.btnAll2);
            this.panel2.Controls.Add(this.btnPrev2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(634, 25);
            this.panel2.TabIndex = 0;
            // 
            // btnReply
            // 
            this.btnReply.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnReply.Location = new System.Drawing.Point(451, -1);
            this.btnReply.Name = "btnReply";
            this.btnReply.Size = new System.Drawing.Size(47, 23);
            this.btnReply.TabIndex = 14;
            this.btnReply.TabStop = false;
            this.btnReply.Text = "Reply";
            this.btnReply.UseCompatibleTextRendering = true;
            this.btnReply.UseVisualStyleBackColor = true;
            this.btnReply.Click += new System.EventHandler(this.btnReply_Click);
            // 
            // lblUser
            // 
            this.lblUser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUser.AutoSize = true;
            this.lblUser.Location = new System.Drawing.Point(406, 5);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(41, 12);
            this.lblUser.TabIndex = 13;
            this.lblUser.Text = "label1";
            this.lblUser.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtGoTo2
            // 
            this.txtGoTo2.Location = new System.Drawing.Point(504, 0);
            this.txtGoTo2.Name = "txtGoTo2";
            this.txtGoTo2.Size = new System.Drawing.Size(25, 21);
            this.txtGoTo2.TabIndex = 12;
            this.txtGoTo2.TabStop = false;
            this.txtGoTo2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnGo2
            // 
            this.btnGo2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnGo2.Location = new System.Drawing.Point(535, -1);
            this.btnGo2.Name = "btnGo2";
            this.btnGo2.Size = new System.Drawing.Size(30, 23);
            this.btnGo2.TabIndex = 11;
            this.btnGo2.TabStop = false;
            this.btnGo2.Text = "Go";
            this.btnGo2.UseCompatibleTextRendering = true;
            this.btnGo2.UseVisualStyleBackColor = true;
            this.btnGo2.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // lblPage2
            // 
            this.lblPage2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPage2.AutoSize = true;
            this.lblPage2.Location = new System.Drawing.Point(183, 5);
            this.lblPage2.Name = "lblPage2";
            this.lblPage2.Size = new System.Drawing.Size(47, 12);
            this.lblPage2.TabIndex = 10;
            this.lblPage2.Text = "001/100";
            this.lblPage2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnLast2
            // 
            this.btnLast2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnLast2.Location = new System.Drawing.Point(324, -1);
            this.btnLast2.Name = "btnLast2";
            this.btnLast2.Size = new System.Drawing.Size(75, 23);
            this.btnLast2.TabIndex = 9;
            this.btnLast2.TabStop = false;
            this.btnLast2.Text = "Last";
            this.btnLast2.UseCompatibleTextRendering = true;
            this.btnLast2.UseVisualStyleBackColor = true;
            this.btnLast2.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // btnFirst2
            // 
            this.btnFirst2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnFirst2.Location = new System.Drawing.Point(11, -1);
            this.btnFirst2.Name = "btnFirst2";
            this.btnFirst2.Size = new System.Drawing.Size(75, 23);
            this.btnFirst2.TabIndex = 8;
            this.btnFirst2.TabStop = false;
            this.btnFirst2.Text = "First";
            this.btnFirst2.UseCompatibleTextRendering = true;
            this.btnFirst2.UseVisualStyleBackColor = true;
            this.btnFirst2.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // btnNext2
            // 
            this.btnNext2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnNext2.Location = new System.Drawing.Point(243, -1);
            this.btnNext2.Name = "btnNext2";
            this.btnNext2.Size = new System.Drawing.Size(75, 23);
            this.btnNext2.TabIndex = 2;
            this.btnNext2.TabStop = false;
            this.btnNext2.Text = "Next";
            this.btnNext2.UseCompatibleTextRendering = true;
            this.btnNext2.UseVisualStyleBackColor = true;
            this.btnNext2.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnAll2
            // 
            this.btnAll2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnAll2.Location = new System.Drawing.Point(571, -1);
            this.btnAll2.Name = "btnAll2";
            this.btnAll2.Size = new System.Drawing.Size(50, 23);
            this.btnAll2.TabIndex = 4;
            this.btnAll2.TabStop = false;
            this.btnAll2.Text = "All";
            this.btnAll2.UseCompatibleTextRendering = true;
            this.btnAll2.UseVisualStyleBackColor = true;
            this.btnAll2.Click += new System.EventHandler(this.btnAll_Click);
            // 
            // btnPrev2
            // 
            this.btnPrev2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnPrev2.Location = new System.Drawing.Point(92, -1);
            this.btnPrev2.Name = "btnPrev2";
            this.btnPrev2.Size = new System.Drawing.Size(75, 23);
            this.btnPrev2.TabIndex = 1;
            this.btnPrev2.TabStop = false;
            this.btnPrev2.Text = "Prev";
            this.btnPrev2.UseCompatibleTextRendering = true;
            this.btnPrev2.UseVisualStyleBackColor = true;
            this.btnPrev2.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // TopicForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 454);
            this.Controls.Add(this.splitContainer1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "TopicForm";
            this.ShowInTaskbar = false;
            this.Text = "Topic";
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.panelContainer.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnPrev1;
        private System.Windows.Forms.Button btnPrev2;
        private System.Windows.Forms.Button btnNext1;
        private System.Windows.Forms.Button btnNext2;
        private System.Windows.Forms.Button btnAll1;
        private System.Windows.Forms.Button btnAll2;
        private System.Windows.Forms.Button btnLast1;
        private System.Windows.Forms.Button btnFirst1;
        private System.Windows.Forms.Button btnLast2;
        private System.Windows.Forms.Button btnFirst2;
        private System.Windows.Forms.Label lblPage1;
        private System.Windows.Forms.Label lblPage2;
        private System.Windows.Forms.TextBox txtGoTo1;
        private System.Windows.Forms.Button btnGo1;
        private System.Windows.Forms.TextBox txtGoTo2;
        private System.Windows.Forms.Button btnGo2;
        private System.ComponentModel.BackgroundWorker bwFetchPage;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.Button btnShrink;
        private System.Windows.Forms.Button btnGrow;
        private System.Windows.Forms.Button btnReply;
        private System.Windows.Forms.Button btnOpenInBrowser;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelContainer;
    }
}