namespace Nzl.Smth.Controls.Elements
{
    partial class BoardControl
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
            this.lblType = new System.Windows.Forms.Label();
            this.linklblBoard = new System.Windows.Forms.LinkLabel();
            this.panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Controls.Add(this.lblType);
            this.panel.Controls.Add(this.linklblBoard);
            this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel.Location = new System.Drawing.Point(0, 0);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(475, 24);
            this.panel.TabIndex = 0;
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Font = new System.Drawing.Font("宋体", 9F);
            this.lblType.Location = new System.Drawing.Point(12, 6);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(29, 12);
            this.lblType.TabIndex = 1;
            this.lblType.Text = "版面";
            // 
            // linklblBoard
            // 
            this.linklblBoard.AutoSize = true;
            this.linklblBoard.Font = new System.Drawing.Font("宋体", 9F);
            this.linklblBoard.Location = new System.Drawing.Point(48, 6);
            this.linklblBoard.Name = "linklblBoard";
            this.linklblBoard.Size = new System.Drawing.Size(35, 12);
            this.linklblBoard.TabIndex = 0;
            this.linklblBoard.TabStop = true;
            this.linklblBoard.Text = "Board";
            // 
            // BoardControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel);
            this.Name = "BoardControl";
            this.Size = new System.Drawing.Size(475, 24);
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.LinkLabel linklblBoard;
        private System.Windows.Forms.Label lblType;
    }
}
