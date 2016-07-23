namespace Nzl.Web.Smth.Controls
{
    partial class BoardBrowserControl
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
            this.btnOpenInBrower = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtGoTo2 = new System.Windows.Forms.TextBox();
            this.btnGo2 = new System.Windows.Forms.Button();
            this.lblPage2 = new System.Windows.Forms.Label();
            this.btnLast2 = new System.Windows.Forms.Button();
            this.btnFirst2 = new System.Windows.Forms.Button();
            this.btnNext2 = new System.Windows.Forms.Button();
            this.btnPrev2 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnGo1 = new System.Windows.Forms.Button();
            this.txtGoTo1 = new System.Windows.Forms.TextBox();
            this.lblPage1 = new System.Windows.Forms.Label();
            this.btnLast1 = new System.Windows.Forms.Button();
            this.btnFirst1 = new System.Windows.Forms.Button();
            this.btnNext1 = new System.Windows.Forms.Button();
            this.btnPrev1 = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.panelContainer = new System.Windows.Forms.Panel();
            this.panel = new System.Windows.Forms.Panel();
            this.bwFetchPage = new System.ComponentModel.BackgroundWorker();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.panelContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOpenInBrower
            // 
            this.btnOpenInBrower.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenInBrower.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnOpenInBrower.Location = new System.Drawing.Point(479, 0);
            this.btnOpenInBrower.Name = "btnOpenInBrower";
            this.btnOpenInBrower.Size = new System.Drawing.Size(75, 23);
            this.btnOpenInBrower.TabIndex = 16;
            this.btnOpenInBrower.TabStop = false;
            this.btnOpenInBrower.Text = "OiB";
            this.btnOpenInBrower.UseCompatibleTextRendering = true;
            this.btnOpenInBrower.UseVisualStyleBackColor = true;
            this.btnOpenInBrower.Click += new System.EventHandler(this.btnOpenInBrower_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txtGoTo2);
            this.panel2.Controls.Add(this.btnGo2);
            this.panel2.Controls.Add(this.lblPage2);
            this.panel2.Controls.Add(this.btnLast2);
            this.panel2.Controls.Add(this.btnFirst2);
            this.panel2.Controls.Add(this.btnNext2);
            this.panel2.Controls.Add(this.btnPrev2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(635, 25);
            this.panel2.TabIndex = 0;
            // 
            // txtGoTo2
            // 
            this.txtGoTo2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGoTo2.Location = new System.Drawing.Point(563, 1);
            this.txtGoTo2.Name = "txtGoTo2";
            this.txtGoTo2.Size = new System.Drawing.Size(25, 21);
            this.txtGoTo2.TabIndex = 12;
            this.txtGoTo2.TabStop = false;
            this.txtGoTo2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnGo2
            // 
            this.btnGo2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGo2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnGo2.Location = new System.Drawing.Point(594, 0);
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
            this.lblPage2.Location = new System.Drawing.Point(183, 6);
            this.lblPage2.Name = "lblPage2";
            this.lblPage2.Size = new System.Drawing.Size(47, 12);
            this.lblPage2.TabIndex = 10;
            this.lblPage2.Text = "001/100";
            this.lblPage2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnLast2
            // 
            this.btnLast2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnLast2.Location = new System.Drawing.Point(324, 0);
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
            this.btnFirst2.Location = new System.Drawing.Point(11, 0);
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
            this.btnNext2.Location = new System.Drawing.Point(243, 0);
            this.btnNext2.Name = "btnNext2";
            this.btnNext2.Size = new System.Drawing.Size(75, 23);
            this.btnNext2.TabIndex = 2;
            this.btnNext2.TabStop = false;
            this.btnNext2.Text = "Next";
            this.btnNext2.UseCompatibleTextRendering = true;
            this.btnNext2.UseVisualStyleBackColor = true;
            this.btnNext2.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrev2
            // 
            this.btnPrev2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnPrev2.Location = new System.Drawing.Point(92, 0);
            this.btnPrev2.Name = "btnPrev2";
            this.btnPrev2.Size = new System.Drawing.Size(75, 23);
            this.btnPrev2.TabIndex = 1;
            this.btnPrev2.TabStop = false;
            this.btnPrev2.Text = "Prev";
            this.btnPrev2.UseCompatibleTextRendering = true;
            this.btnPrev2.UseVisualStyleBackColor = true;
            this.btnPrev2.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnOpenInBrower);
            this.panel1.Controls.Add(this.btnGo1);
            this.panel1.Controls.Add(this.txtGoTo1);
            this.panel1.Controls.Add(this.lblPage1);
            this.panel1.Controls.Add(this.btnLast1);
            this.panel1.Controls.Add(this.btnFirst1);
            this.panel1.Controls.Add(this.btnNext1);
            this.panel1.Controls.Add(this.btnPrev1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(635, 25);
            this.panel1.TabIndex = 0;
            // 
            // btnGo1
            // 
            this.btnGo1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGo1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnGo1.Location = new System.Drawing.Point(594, 0);
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
            this.txtGoTo1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGoTo1.Location = new System.Drawing.Point(563, 1);
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
            this.lblPage1.Location = new System.Drawing.Point(182, 6);
            this.lblPage1.Name = "lblPage1";
            this.lblPage1.Size = new System.Drawing.Size(47, 12);
            this.lblPage1.TabIndex = 8;
            this.lblPage1.Text = "001/100";
            this.lblPage1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnLast1
            // 
            this.btnLast1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnLast1.Location = new System.Drawing.Point(324, 0);
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
            this.btnFirst1.Location = new System.Drawing.Point(11, 0);
            this.btnFirst1.Name = "btnFirst1";
            this.btnFirst1.Size = new System.Drawing.Size(75, 23);
            this.btnFirst1.TabIndex = 6;
            this.btnFirst1.TabStop = false;
            this.btnFirst1.Text = "First";
            this.btnFirst1.UseCompatibleTextRendering = true;
            this.btnFirst1.UseVisualStyleBackColor = true;
            this.btnFirst1.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // btnNext1
            // 
            this.btnNext1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnNext1.Location = new System.Drawing.Point(243, 0);
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
            this.btnPrev1.Location = new System.Drawing.Point(92, 0);
            this.btnPrev1.Name = "btnPrev1";
            this.btnPrev1.Size = new System.Drawing.Size(75, 23);
            this.btnPrev1.TabIndex = 0;
            this.btnPrev1.TabStop = false;
            this.btnPrev1.Text = "Prev";
            this.btnPrev1.UseCompatibleTextRendering = true;
            this.btnPrev1.UseVisualStyleBackColor = true;
            this.btnPrev1.Click += new System.EventHandler(this.btnPrev_Click);
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
            this.splitContainer1.Size = new System.Drawing.Size(635, 544);
            this.splitContainer1.SplitterDistance = 25;
            this.splitContainer1.TabIndex = 3;
            this.splitContainer1.TabStop = false;
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
            this.splitContainer2.Size = new System.Drawing.Size(635, 515);
            this.splitContainer2.SplitterDistance = 486;
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
            this.panelContainer.Size = new System.Drawing.Size(635, 486);
            this.panelContainer.TabIndex = 1;
            // 
            // panel
            // 
            this.panel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel.Location = new System.Drawing.Point(4, 4);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(626, 100);
            this.panel.TabIndex = 0;
            // 
            // BoardBrowserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "BoardBrowserControl";
            this.Size = new System.Drawing.Size(635, 544);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.panelContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOpenInBrower;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtGoTo2;
        private System.Windows.Forms.Button btnGo2;
        private System.Windows.Forms.Label lblPage2;
        private System.Windows.Forms.Button btnLast2;
        private System.Windows.Forms.Button btnFirst2;
        private System.Windows.Forms.Button btnNext2;
        private System.Windows.Forms.Button btnPrev2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnGo1;
        private System.Windows.Forms.TextBox txtGoTo1;
        private System.Windows.Forms.Label lblPage1;
        private System.Windows.Forms.Button btnLast1;
        private System.Windows.Forms.Button btnFirst1;
        private System.Windows.Forms.Button btnNext1;
        private System.Windows.Forms.Button btnPrev1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Panel panel;
        private System.ComponentModel.BackgroundWorker bwFetchPage;
        private System.Windows.Forms.Panel panelContainer;
    }
}
