namespace Nzl.Web.Forms.Rss
{
    partial class RssItemControl
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
            this.lblVendor = new System.Windows.Forms.Label();
            this.panel = new System.Windows.Forms.Panel();
            this.contentPanel = new System.Windows.Forms.Panel();
            this.richtxtContent = new System.Windows.Forms.RichTextBox();
            this.lblIndex = new System.Windows.Forms.Label();
            this.lnklblTitle = new System.Windows.Forms.LinkLabel();
            this.lblDateTime = new System.Windows.Forms.Label();
            this.panel.SuspendLayout();
            this.contentPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblVendor
            // 
            this.lblVendor.AutoSize = true;
            this.lblVendor.Font = new System.Drawing.Font("NSimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblVendor.Location = new System.Drawing.Point(12, 38);
            this.lblVendor.Name = "lblVendor";
            this.lblVendor.Size = new System.Drawing.Size(57, 12);
            this.lblVendor.TabIndex = 0;
            this.lblVendor.Text = "京东商城";
            // 
            // panel
            // 
            this.panel.AutoSize = true;
            this.panel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel.BackColor = System.Drawing.SystemColors.Control;
            this.panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel.Controls.Add(this.contentPanel);
            this.panel.Controls.Add(this.lblIndex);
            this.panel.Controls.Add(this.lnklblTitle);
            this.panel.Controls.Add(this.lblDateTime);
            this.panel.Controls.Add(this.lblVendor);
            this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel.Location = new System.Drawing.Point(0, 0);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(608, 108);
            this.panel.TabIndex = 4;
            // 
            // contentPanel
            // 
            this.contentPanel.Controls.Add(this.richtxtContent);
            this.contentPanel.Location = new System.Drawing.Point(14, 63);
            this.contentPanel.Name = "contentPanel";
            this.contentPanel.Size = new System.Drawing.Size(577, 27);
            this.contentPanel.TabIndex = 6;
            // 
            // richtxtContent
            // 
            this.richtxtContent.BackColor = System.Drawing.SystemColors.Control;
            this.richtxtContent.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richtxtContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richtxtContent.Location = new System.Drawing.Point(0, 0);
            this.richtxtContent.Name = "richtxtContent";
            this.richtxtContent.Size = new System.Drawing.Size(577, 27);
            this.richtxtContent.TabIndex = 2;
            this.richtxtContent.TabStop = false;
            this.richtxtContent.Text = "";
            this.richtxtContent.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.richtxtContent_LinkClicked);
            this.richtxtContent.Enter += new System.EventHandler(this.richtxtContent_Enter);
            // 
            // lblIndex
            // 
            this.lblIndex.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblIndex.AutoSize = true;
            this.lblIndex.Font = new System.Drawing.Font("NSimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblIndex.Location = new System.Drawing.Point(565, 38);
            this.lblIndex.Name = "lblIndex";
            this.lblIndex.Size = new System.Drawing.Size(26, 12);
            this.lblIndex.TabIndex = 5;
            this.lblIndex.Text = "001";
            this.lblIndex.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lnklblTitle
            // 
            this.lnklblTitle.AutoSize = true;
            this.lnklblTitle.Location = new System.Drawing.Point(12, 12);
            this.lnklblTitle.Name = "lnklblTitle";
            this.lnklblTitle.Size = new System.Drawing.Size(35, 12);
            this.lnklblTitle.TabIndex = 4;
            this.lnklblTitle.TabStop = true;
            this.lnklblTitle.Text = "Title";
            // 
            // lblDateTime
            // 
            this.lblDateTime.AutoSize = true;
            this.lblDateTime.Font = new System.Drawing.Font("NSimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDateTime.Location = new System.Drawing.Point(262, 38);
            this.lblDateTime.Name = "lblDateTime";
            this.lblDateTime.Size = new System.Drawing.Size(82, 12);
            this.lblDateTime.TabIndex = 3;
            this.lblDateTime.Text = "12-25 13:56";
            this.lblDateTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // RssItemControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel);
            this.Name = "RssItemControl";
            this.Size = new System.Drawing.Size(608, 108);
            this.Load += new System.EventHandler(this.RssItemControl_Load);
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            this.contentPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblVendor;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.Label lblDateTime;
        private System.Windows.Forms.RichTextBox richtxtContent;
        private System.Windows.Forms.LinkLabel lnklblTitle;
        private System.Windows.Forms.Label lblIndex;
        private System.Windows.Forms.Panel contentPanel;
    }
}