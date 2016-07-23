namespace Nzl.Web.Smth.Forms
{
    partial class BoardForm
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
            this.btnShrink = new System.Windows.Forms.Button();
            this.panel = new System.Windows.Forms.Panel();
            this.btnGrow = new System.Windows.Forms.Button();
            this.btnFirst2 = new System.Windows.Forms.Button();
            this.btnLast2 = new System.Windows.Forms.Button();
            this.btnGo1 = new System.Windows.Forms.Button();
            this.lblPage2 = new System.Windows.Forms.Label();
            this.txtGoTo1 = new System.Windows.Forms.TextBox();
            this.lblPage1 = new System.Windows.Forms.Label();
            this.btnNext2 = new System.Windows.Forms.Button();
            this.btnPrev2 = new System.Windows.Forms.Button();
            this.btnLast1 = new System.Windows.Forms.Button();
            this.btnGo2 = new System.Windows.Forms.Button();
            this.btnFirst1 = new System.Windows.Forms.Button();
            this.btnNext1 = new System.Windows.Forms.Button();
            this.txtGoTo2 = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnOpenInBrower = new System.Windows.Forms.Button();
            this.btnPrev1 = new System.Windows.Forms.Button();
            this.lblUser = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.panel2 = new System.Windows.Forms.Panel();
            this.bwFetchPage = new System.ComponentModel.BackgroundWorker();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnShrink
            // 
            this.btnShrink.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnShrink.Location = new System.Drawing.Point(523, 0);
            this.btnShrink.Name = "btnShrink";
            this.btnShrink.Size = new System.Drawing.Size(30, 23);
            this.btnShrink.TabIndex = 14;
            this.btnShrink.TabStop = false;
            this.btnShrink.Text = "-";
            this.btnShrink.UseCompatibleTextRendering = true;
            this.btnShrink.UseVisualStyleBackColor = true;
            this.btnShrink.Click += new System.EventHandler(this.btnShrink_Click);
            // 
            // panel
            // 
            this.panel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel.Location = new System.Drawing.Point(3, 3);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(629, 100);
            this.panel.TabIndex = 0;
            // 
            // btnGrow
            // 
            this.btnGrow.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnGrow.Location = new System.Drawing.Point(487, 0);
            this.btnGrow.Name = "btnGrow";
            this.btnGrow.Size = new System.Drawing.Size(30, 23);
            this.btnGrow.TabIndex = 15;
            this.btnGrow.TabStop = false;
            this.btnGrow.Text = "+";
            this.btnGrow.UseCompatibleTextRendering = true;
            this.btnGrow.UseVisualStyleBackColor = true;
            this.btnGrow.Click += new System.EventHandler(this.btnGrow_Click);
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
            // btnGo1
            // 
            this.btnGo1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnGo1.Location = new System.Drawing.Point(592, 0);
            this.btnGo1.Name = "btnGo1";
            this.btnGo1.Size = new System.Drawing.Size(30, 23);
            this.btnGo1.TabIndex = 10;
            this.btnGo1.TabStop = false;
            this.btnGo1.Text = "Go";
            this.btnGo1.UseCompatibleTextRendering = true;
            this.btnGo1.UseVisualStyleBackColor = true;
            this.btnGo1.Click += new System.EventHandler(this.btnGo_Click);
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
            // txtGoTo1
            // 
            this.txtGoTo1.Location = new System.Drawing.Point(561, 1);
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
            // btnGo2
            // 
            this.btnGo2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnGo2.Location = new System.Drawing.Point(592, 0);
            this.btnGo2.Name = "btnGo2";
            this.btnGo2.Size = new System.Drawing.Size(30, 23);
            this.btnGo2.TabIndex = 11;
            this.btnGo2.TabStop = false;
            this.btnGo2.Text = "Go";
            this.btnGo2.UseCompatibleTextRendering = true;
            this.btnGo2.UseVisualStyleBackColor = true;
            this.btnGo2.Click += new System.EventHandler(this.btnGo_Click);
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
            // txtGoTo2
            // 
            this.txtGoTo2.Location = new System.Drawing.Point(561, 1);
            this.txtGoTo2.Name = "txtGoTo2";
            this.txtGoTo2.Size = new System.Drawing.Size(25, 21);
            this.txtGoTo2.TabIndex = 12;
            this.txtGoTo2.TabStop = false;
            this.txtGoTo2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnOpenInBrower);
            this.panel1.Controls.Add(this.btnGrow);
            this.panel1.Controls.Add(this.btnShrink);
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
            // btnOpenInBrower
            // 
            this.btnOpenInBrower.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnOpenInBrower.Location = new System.Drawing.Point(406, 0);
            this.btnOpenInBrower.Name = "btnOpenInBrower";
            this.btnOpenInBrower.Size = new System.Drawing.Size(75, 23);
            this.btnOpenInBrower.TabIndex = 16;
            this.btnOpenInBrower.TabStop = false;
            this.btnOpenInBrower.Text = "OiB";
            this.btnOpenInBrower.UseCompatibleTextRendering = true;
            this.btnOpenInBrower.UseVisualStyleBackColor = true;
            this.btnOpenInBrower.Click += new System.EventHandler(this.btnOpenInBrower_Click);
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
            // lblUser
            // 
            this.lblUser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUser.AutoSize = true;
            this.lblUser.Location = new System.Drawing.Point(460, 6);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(41, 12);
            this.lblUser.TabIndex = 13;
            this.lblUser.Text = "label1";
            this.lblUser.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.splitContainer1.Size = new System.Drawing.Size(635, 611);
            this.splitContainer1.SplitterDistance = 25;
            this.splitContainer1.TabIndex = 2;
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
            this.splitContainer2.Panel1.Controls.Add(this.panel);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.panel2);
            this.splitContainer2.Size = new System.Drawing.Size(635, 582);
            this.splitContainer2.SplitterDistance = 553;
            this.splitContainer2.TabIndex = 0;
            this.splitContainer2.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.lblUser);
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
            // BoardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(635, 611);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "BoardForm";
            this.Text = "Board";
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
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnShrink;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.Button btnGrow;
        private System.Windows.Forms.Button btnFirst2;
        private System.Windows.Forms.Button btnLast2;
        private System.Windows.Forms.Button btnGo1;
        private System.Windows.Forms.Label lblPage2;
        private System.Windows.Forms.TextBox txtGoTo1;
        private System.Windows.Forms.Label lblPage1;
        private System.Windows.Forms.Button btnNext2;
        private System.Windows.Forms.Button btnPrev2;
        private System.Windows.Forms.Button btnLast1;
        private System.Windows.Forms.Button btnGo2;
        private System.Windows.Forms.Button btnFirst1;
        private System.Windows.Forms.Button btnNext1;
        private System.Windows.Forms.TextBox txtGoTo2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnPrev1;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Panel panel2;
        private System.ComponentModel.BackgroundWorker bwFetchPage;
        private System.Windows.Forms.Button btnOpenInBrower;

    }
}