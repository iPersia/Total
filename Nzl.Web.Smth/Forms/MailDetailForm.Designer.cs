namespace Nzl.Web.Smth.Forms
{
    partial class MailDetailForm
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
            this.panel = new System.Windows.Forms.Panel();
            this.richtxtContent = new System.Windows.Forms.RichTextBox();
            this.linklblDelete = new System.Windows.Forms.LinkLabel();
            this.linklblTransfer = new System.Windows.Forms.LinkLabel();
            this.linklblReply = new System.Windows.Forms.LinkLabel();
            this.lblDateTime = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.linklblID = new System.Windows.Forms.LinkLabel();
            this.panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Controls.Add(this.richtxtContent);
            this.panel.Controls.Add(this.linklblDelete);
            this.panel.Controls.Add(this.linklblTransfer);
            this.panel.Controls.Add(this.linklblReply);
            this.panel.Controls.Add(this.lblDateTime);
            this.panel.Controls.Add(this.lblTitle);
            this.panel.Controls.Add(this.linklblID);
            this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel.Location = new System.Drawing.Point(0, 0);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(467, 437);
            this.panel.TabIndex = 0;
            // 
            // richtxtContent
            // 
            this.richtxtContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richtxtContent.BackColor = System.Drawing.SystemColors.Control;
            this.richtxtContent.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richtxtContent.Location = new System.Drawing.Point(12, 74);
            this.richtxtContent.Name = "richtxtContent";
            this.richtxtContent.ReadOnly = true;
            this.richtxtContent.Size = new System.Drawing.Size(443, 351);
            this.richtxtContent.TabIndex = 12;
            this.richtxtContent.TabStop = false;
            this.richtxtContent.Text = "";
            // 
            // linklblDelete
            // 
            this.linklblDelete.AutoSize = true;
            this.linklblDelete.Font = new System.Drawing.Font("新宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.linklblDelete.Location = new System.Drawing.Point(134, 42);
            this.linklblDelete.Name = "linklblDelete";
            this.linklblDelete.Size = new System.Drawing.Size(47, 12);
            this.linklblDelete.TabIndex = 11;
            this.linklblDelete.TabStop = true;
            this.linklblDelete.Text = "Delete";
            this.linklblDelete.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.linklblDelete.Visible = false;
            // 
            // linklblTransfer
            // 
            this.linklblTransfer.AutoSize = true;
            this.linklblTransfer.Font = new System.Drawing.Font("新宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.linklblTransfer.Location = new System.Drawing.Point(63, 42);
            this.linklblTransfer.Name = "linklblTransfer";
            this.linklblTransfer.Size = new System.Drawing.Size(61, 12);
            this.linklblTransfer.TabIndex = 10;
            this.linklblTransfer.TabStop = true;
            this.linklblTransfer.Text = "Transfer";
            this.linklblTransfer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.linklblTransfer.Visible = false;
            // 
            // linklblReply
            // 
            this.linklblReply.AutoSize = true;
            this.linklblReply.Font = new System.Drawing.Font("新宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.linklblReply.Location = new System.Drawing.Point(13, 42);
            this.linklblReply.Name = "linklblReply";
            this.linklblReply.Size = new System.Drawing.Size(40, 12);
            this.linklblReply.TabIndex = 9;
            this.linklblReply.TabStop = true;
            this.linklblReply.Text = "Reply";
            this.linklblReply.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.linklblReply.Visible = false;
            // 
            // lblDateTime
            // 
            this.lblDateTime.AutoSize = true;
            this.lblDateTime.Location = new System.Drawing.Point(191, 43);
            this.lblDateTime.Name = "lblDateTime";
            this.lblDateTime.Size = new System.Drawing.Size(119, 12);
            this.lblDateTime.TabIndex = 4;
            this.lblDateTime.Text = "2013-02-22 23:53:35";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(14, 13);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(35, 12);
            this.lblTitle.TabIndex = 3;
            this.lblTitle.Text = "Title";
            // 
            // linklblID
            // 
            this.linklblID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linklblID.AutoSize = true;
            this.linklblID.Font = new System.Drawing.Font("新宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.linklblID.Location = new System.Drawing.Point(350, 42);
            this.linklblID.Name = "linklblID";
            this.linklblID.Size = new System.Drawing.Size(89, 12);
            this.linklblID.TabIndex = 2;
            this.linklblID.TabStop = true;
            this.linklblID.Text = "IDIDIDIDIDID";
            this.linklblID.Visible = false;
            // 
            // MailDetailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(467, 437);
            this.Controls.Add(this.panel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "MailDetailForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Mail Detail";
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.LinkLabel linklblID;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblDateTime;
        private System.Windows.Forms.LinkLabel linklblTransfer;
        private System.Windows.Forms.LinkLabel linklblReply;
        private System.Windows.Forms.LinkLabel linklblDelete;
        private System.Windows.Forms.RichTextBox richtxtContent;
    }
}