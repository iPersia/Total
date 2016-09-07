namespace Nzl.Smth.Controls.Elements
{
    partial class ReplyControl
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
            this.linklblAuthor = new System.Windows.Forms.LinkLabel();
            this.lblDT = new System.Windows.Forms.Label();
            this.panel = new System.Windows.Forms.Panel();
            this.linklblDelete = new System.Windows.Forms.LinkLabel();
            this.linklblTitle = new System.Windows.Forms.LinkLabel();
            this.panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // linklblAuthor
            // 
            this.linklblAuthor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linklblAuthor.Location = new System.Drawing.Point(381, 11);
            this.linklblAuthor.Name = "linklblAuthor";
            this.linklblAuthor.Size = new System.Drawing.Size(100, 14);
            this.linklblAuthor.TabIndex = 3;
            this.linklblAuthor.TabStop = true;
            this.linklblAuthor.Text = "Author";
            this.linklblAuthor.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblDT
            // 
            this.lblDT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDT.AutoSize = true;
            this.lblDT.Location = new System.Drawing.Point(542, 11);
            this.lblDT.Name = "lblDT";
            this.lblDT.Size = new System.Drawing.Size(140, 14);
            this.lblDT.TabIndex = 2;
            this.lblDT.Text = "2013-02-01 01:01:01";
            // 
            // panel
            // 
            this.panel.Controls.Add(this.linklblDelete);
            this.panel.Controls.Add(this.linklblAuthor);
            this.panel.Controls.Add(this.lblDT);
            this.panel.Controls.Add(this.linklblTitle);
            this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel.Location = new System.Drawing.Point(0, 0);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(696, 36);
            this.panel.TabIndex = 1;
            // 
            // linklblDelete
            // 
            this.linklblDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linklblDelete.AutoSize = true;
            this.linklblDelete.Location = new System.Drawing.Point(487, 11);
            this.linklblDelete.Name = "linklblDelete";
            this.linklblDelete.Size = new System.Drawing.Size(49, 14);
            this.linklblDelete.TabIndex = 4;
            this.linklblDelete.TabStop = true;
            this.linklblDelete.Text = "Delete";
            this.linklblDelete.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.linklblDelete.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linklblDelete_LinkClicked);
            // 
            // linklblTitle
            // 
            this.linklblTitle.AutoSize = true;
            this.linklblTitle.Font = new System.Drawing.Font("宋体", 9F);
            this.linklblTitle.Location = new System.Drawing.Point(17, 13);
            this.linklblTitle.Name = "linklblTitle";
            this.linklblTitle.Size = new System.Drawing.Size(71, 12);
            this.linklblTitle.TabIndex = 0;
            this.linklblTitle.TabStop = true;
            this.linklblTitle.Text = "Refer Title";
            // 
            // ReplyControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel);
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "ReplyControl";
            this.Size = new System.Drawing.Size(696, 36);
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.LinkLabel linklblAuthor;
        private System.Windows.Forms.Label lblDT;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.LinkLabel linklblTitle;
        private System.Windows.Forms.LinkLabel linklblDelete;
    }
}
