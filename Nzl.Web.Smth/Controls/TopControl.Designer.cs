namespace Nzl.Web.Smth.Controls
{
    partial class TopControl
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
            this.panel = new System.Windows.Forms.Panel();
            this.linklblBoard = new System.Windows.Forms.LinkLabel();
            this.lblReplies = new System.Windows.Forms.Label();
            this.linklblTop = new System.Windows.Forms.LinkLabel();
            this.lblIndex = new System.Windows.Forms.Label();
            this.panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Controls.Add(this.linklblBoard);
            this.panel.Controls.Add(this.lblReplies);
            this.panel.Controls.Add(this.linklblTop);
            this.panel.Controls.Add(this.lblIndex);
            this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel.Location = new System.Drawing.Point(0, 0);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(438, 40);
            this.panel.TabIndex = 0;
            // 
            // linklblBoard
            // 
            this.linklblBoard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linklblBoard.Location = new System.Drawing.Point(230, 14);
            this.linklblBoard.Name = "linklblBoard";
            this.linklblBoard.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.linklblBoard.Size = new System.Drawing.Size(200, 12);
            this.linklblBoard.TabIndex = 7;
            this.linklblBoard.TabStop = true;
            this.linklblBoard.Text = "Board";
            // 
            // lblReplies
            // 
            this.lblReplies.AutoSize = true;
            this.lblReplies.Location = new System.Drawing.Point(73, 14);
            this.lblReplies.Name = "lblReplies";
            this.lblReplies.Size = new System.Drawing.Size(23, 12);
            this.lblReplies.TabIndex = 6;
            this.lblReplies.Text = "(0)";
            this.lblReplies.Visible = false;
            // 
            // linklblTop
            // 
            this.linklblTop.AutoSize = true;
            this.linklblTop.Location = new System.Drawing.Point(45, 14);
            this.linklblTop.Name = "linklblTop";
            this.linklblTop.Size = new System.Drawing.Size(23, 12);
            this.linklblTop.TabIndex = 1;
            this.linklblTop.TabStop = true;
            this.linklblTop.Text = "Top";
            // 
            // lblIndex
            // 
            this.lblIndex.AutoSize = true;
            this.lblIndex.Location = new System.Drawing.Point(17, 15);
            this.lblIndex.Name = "lblIndex";
            this.lblIndex.Size = new System.Drawing.Size(17, 12);
            this.lblIndex.TabIndex = 0;
            this.lblIndex.Text = "10";
            // 
            // TopControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel);
            this.Name = "TopControl";
            this.Size = new System.Drawing.Size(438, 40);
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.Label lblIndex;
        private System.Windows.Forms.LinkLabel linklblTop;
        private System.Windows.Forms.Label lblReplies;
        private System.Windows.Forms.LinkLabel linklblBoard;
    }
}
