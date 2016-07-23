namespace Nzl.Web.Forms.MobileNewSmth.Forms
{
    partial class BrowserForm
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
            this.scContainer = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbTopics = new System.Windows.Forms.ListBox();
            this.panelContainer = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.scContainer)).BeginInit();
            this.scContainer.Panel1.SuspendLayout();
            this.scContainer.Panel2.SuspendLayout();
            this.scContainer.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // scContainer
            // 
            this.scContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.scContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scContainer.IsSplitterFixed = true;
            this.scContainer.Location = new System.Drawing.Point(0, 0);
            this.scContainer.Name = "scContainer";
            // 
            // scContainer.Panel1
            // 
            this.scContainer.Panel1.Controls.Add(this.panel1);
            // 
            // scContainer.Panel2
            // 
            this.scContainer.Panel2.Controls.Add(this.panelContainer);
            this.scContainer.Size = new System.Drawing.Size(963, 605);
            this.scContainer.SplitterDistance = 240;
            this.scContainer.SplitterWidth = 1;
            this.scContainer.TabIndex = 0;
            this.scContainer.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbTopics);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(238, 603);
            this.panel1.TabIndex = 0;
            // 
            // lbTopics
            // 
            this.lbTopics.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbTopics.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbTopics.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbTopics.FormattingEnabled = true;
            this.lbTopics.ItemHeight = 12;
            this.lbTopics.Location = new System.Drawing.Point(0, 0);
            this.lbTopics.Name = "lbTopics";
            this.lbTopics.Size = new System.Drawing.Size(238, 603);
            this.lbTopics.TabIndex = 0;
            this.lbTopics.TabStop = false;
            // 
            // panelContainer
            // 
            this.panelContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContainer.Location = new System.Drawing.Point(0, 0);
            this.panelContainer.Name = "panelContainer";
            this.panelContainer.Size = new System.Drawing.Size(720, 603);
            this.panelContainer.TabIndex = 0;
            // 
            // BrowserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(963, 605);
            this.Controls.Add(this.scContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "BrowserForm";
            this.Text = "Browser";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BrowserForm_FormClosing);
            this.scContainer.Panel1.ResumeLayout(false);
            this.scContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scContainer)).EndInit();
            this.scContainer.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer scContainer;
        private System.Windows.Forms.ListBox lbTopics;
        private System.Windows.Forms.Panel panelContainer;
        private System.Windows.Forms.Panel panel1;
    }
}