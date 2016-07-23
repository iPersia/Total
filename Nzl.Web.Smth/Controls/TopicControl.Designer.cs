namespace Nzl.Web.Smth.Controls
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
            this.panel1.Controls.Add(this.linklblLastID);
            this.panel1.Controls.Add(this.linklblCreateID);
            this.panel1.Controls.Add(this.lblLastDT);
            this.panel1.Controls.Add(this.lblCreateDT);
            this.panel1.Controls.Add(this.linklblTopic);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(599, 43);
            this.panel1.TabIndex = 0;
            // 
            // linklblLastID
            // 
            this.linklblLastID.AutoSize = true;
            this.linklblLastID.Location = new System.Drawing.Point(300, 25);
            this.linklblLastID.Name = "linklblLastID";
            this.linklblLastID.Size = new System.Drawing.Size(41, 12);
            this.linklblLastID.TabIndex = 4;
            this.linklblLastID.TabStop = true;
            this.linklblLastID.Text = "LastID";
            // 
            // linklblCreateID
            // 
            this.linklblCreateID.AutoSize = true;
            this.linklblCreateID.Location = new System.Drawing.Point(100, 25);
            this.linklblCreateID.Name = "linklblCreateID";
            this.linklblCreateID.Size = new System.Drawing.Size(53, 12);
            this.linklblCreateID.TabIndex = 3;
            this.linklblCreateID.TabStop = true;
            this.linklblCreateID.Text = "CreateID";
            // 
            // lblLastDT
            // 
            this.lblLastDT.AutoSize = true;
            this.lblLastDT.Location = new System.Drawing.Point(200, 25);
            this.lblLastDT.Name = "lblLastDT";
            this.lblLastDT.Size = new System.Drawing.Size(65, 12);
            this.lblLastDT.TabIndex = 2;
            this.lblLastDT.Text = "2013-02-01";
            // 
            // lblCreateDT
            // 
            this.lblCreateDT.AutoSize = true;
            this.lblCreateDT.Location = new System.Drawing.Point(19, 25);
            this.lblCreateDT.Name = "lblCreateDT";
            this.lblCreateDT.Size = new System.Drawing.Size(65, 12);
            this.lblCreateDT.TabIndex = 1;
            this.lblCreateDT.Text = "2013-01-01";
            // 
            // linklblTopic
            // 
            this.linklblTopic.AutoSize = true;
            this.linklblTopic.Font = new System.Drawing.Font("SimSun", 9F);
            this.linklblTopic.Location = new System.Drawing.Point(18, 6);
            this.linklblTopic.Name = "linklblTopic";
            this.linklblTopic.Size = new System.Drawing.Size(59, 12);
            this.linklblTopic.TabIndex = 0;
            this.linklblTopic.TabStop = true;
            this.linklblTopic.Text = "Topic (0)";
            // 
            // TopicControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "TopicControl";
            this.Size = new System.Drawing.Size(599, 43);
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
    }
}
