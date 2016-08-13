namespace Nzl.Smth.Controls
{
    partial class MailControl
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.linklblTitle = new System.Windows.Forms.LinkLabel();
            this.lblIndex = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // linklblAuthor
            // 
            this.linklblAuthor.AutoSize = true;
            this.linklblAuthor.Location = new System.Drawing.Point(18, 25);
            this.linklblAuthor.Name = "linklblAuthor";
            this.linklblAuthor.Size = new System.Drawing.Size(41, 12);
            this.linklblAuthor.TabIndex = 3;
            this.linklblAuthor.TabStop = true;
            this.linklblAuthor.Text = "Author";
            // 
            // lblDT
            // 
            this.lblDT.AutoSize = true;
            this.lblDT.Location = new System.Drawing.Point(136, 25);
            this.lblDT.Name = "lblDT";
            this.lblDT.Size = new System.Drawing.Size(119, 12);
            this.lblDT.TabIndex = 2;
            this.lblDT.Text = "2013-02-01 01:01:01";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblIndex);
            this.panel1.Controls.Add(this.linklblAuthor);
            this.panel1.Controls.Add(this.lblDT);
            this.panel1.Controls.Add(this.linklblTitle);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(597, 45);
            this.panel1.TabIndex = 1;
            // 
            // linklblTitle
            // 
            this.linklblTitle.AutoSize = true;
            this.linklblTitle.Font = new System.Drawing.Font("SimSun", 9F);
            this.linklblTitle.Location = new System.Drawing.Point(36, 6);
            this.linklblTitle.Name = "linklblTitle";
            this.linklblTitle.Size = new System.Drawing.Size(65, 12);
            this.linklblTitle.TabIndex = 0;
            this.linklblTitle.TabStop = true;
            this.linklblTitle.Text = "Mail Title";
            // 
            // lblIndex
            // 
            this.lblIndex.AutoSize = true;
            this.lblIndex.Location = new System.Drawing.Point(18, 7);
            this.lblIndex.Name = "lblIndex";
            this.lblIndex.Size = new System.Drawing.Size(35, 12);
            this.lblIndex.TabIndex = 4;
            this.lblIndex.Text = "00 - ";
            // 
            // MailControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "MailControl";
            this.Size = new System.Drawing.Size(597, 45);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.LinkLabel linklblAuthor;
        private System.Windows.Forms.Label lblDT;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.LinkLabel linklblTitle;
        private System.Windows.Forms.Label lblIndex;

    }
}
