namespace Nzl.Smth.Controls
{
    partial class TopicControl
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblReplies = new System.Windows.Forms.Label();
            this.linklblLastID = new System.Windows.Forms.LinkLabel();
            this.linklblCreateID = new System.Windows.Forms.LinkLabel();
            this.lblLastDT = new System.Windows.Forms.Label();
            this.lblCreateDT = new System.Windows.Forms.Label();
            this.linklblTopic = new System.Windows.Forms.LinkLabel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblReplies);
            this.panel1.Controls.Add(this.linklblLastID);
            this.panel1.Controls.Add(this.linklblCreateID);
            this.panel1.Controls.Add(this.lblLastDT);
            this.panel1.Controls.Add(this.lblCreateDT);
            this.panel1.Controls.Add(this.linklblTopic);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(599, 36);
            this.panel1.TabIndex = 0;
            // 
            // lblReplies
            // 
            this.lblReplies.AutoSize = true;
            this.lblReplies.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblReplies.Location = new System.Drawing.Point(63, 11);
            this.lblReplies.Name = "lblReplies";
            this.lblReplies.Size = new System.Drawing.Size(28, 14);
            this.lblReplies.TabIndex = 5;
            this.lblReplies.Text = "(0)";
            // 
            // linklblLastID
            // 
            this.linklblLastID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linklblLastID.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.linklblLastID.Location = new System.Drawing.Point(483, 10);
            this.linklblLastID.Name = "linklblLastID";
            this.linklblLastID.Size = new System.Drawing.Size(100, 14);
            this.linklblLastID.TabIndex = 4;
            this.linklblLastID.Text = "LastID";
            this.linklblLastID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // linklblCreateID
            // 
            this.linklblCreateID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linklblCreateID.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.linklblCreateID.Location = new System.Drawing.Point(293, 10);
            this.linklblCreateID.Name = "linklblCreateID";
            this.linklblCreateID.Size = new System.Drawing.Size(100, 14);
            this.linklblCreateID.TabIndex = 3;
            this.linklblCreateID.Text = "ZZZZZZZZZZZZZ";
            this.linklblCreateID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblLastDT
            // 
            this.lblLastDT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLastDT.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblLastDT.Location = new System.Drawing.Point(398, 11);
            this.lblLastDT.Name = "lblLastDT";
            this.lblLastDT.Size = new System.Drawing.Size(80, 12);
            this.lblLastDT.TabIndex = 2;
            this.lblLastDT.Text = "00:00:00";
            this.lblLastDT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCreateDT
            // 
            this.lblCreateDT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCreateDT.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCreateDT.Location = new System.Drawing.Point(208, 11);
            this.lblCreateDT.Name = "lblCreateDT";
            this.lblCreateDT.Size = new System.Drawing.Size(80, 12);
            this.lblCreateDT.TabIndex = 1;
            this.lblCreateDT.Text = "2013-01-01";
            this.lblCreateDT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // linklblTopic
            // 
            this.linklblTopic.AutoSize = true;
            this.linklblTopic.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.linklblTopic.Location = new System.Drawing.Point(14, 11);
            this.linklblTopic.Name = "linklblTopic";
            this.linklblTopic.Size = new System.Drawing.Size(42, 14);
            this.linklblTopic.TabIndex = 0;
            this.linklblTopic.Text = "Topic";
            this.linklblTopic.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TopicControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "TopicControl";
            this.Size = new System.Drawing.Size(599, 36);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.LinkLabel linklblTopic;
        private System.Windows.Forms.Label lblCreateDT;
        private System.Windows.Forms.LinkLabel linklblLastID;
        private System.Windows.Forms.LinkLabel linklblCreateID;
        private System.Windows.Forms.Label lblLastDT;
        private System.Windows.Forms.Label lblReplies;
    }
}
