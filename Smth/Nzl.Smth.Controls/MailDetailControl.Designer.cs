namespace Nzl.Smth.Controls
{
    partial class MailDetailControl
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.scContainer = new System.Windows.Forms.SplitContainer();
            this.panelUp = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.linklblDelete = new System.Windows.Forms.LinkLabel();
            this.linklblID = new System.Windows.Forms.LinkLabel();
            this.linklblTransfer = new System.Windows.Forms.LinkLabel();
            this.lblDateTime = new System.Windows.Forms.Label();
            this.linklblReply = new System.Windows.Forms.LinkLabel();
            this.panelDown = new System.Windows.Forms.Panel();
            this.richtxtContent = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.scContainer)).BeginInit();
            this.scContainer.Panel1.SuspendLayout();
            this.scContainer.Panel2.SuspendLayout();
            this.scContainer.SuspendLayout();
            this.panelUp.SuspendLayout();
            this.panelDown.SuspendLayout();
            this.SuspendLayout();
            // 
            // scContainer
            // 
            this.scContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.scContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.scContainer.IsSplitterFixed = true;
            this.scContainer.Location = new System.Drawing.Point(0, 0);
            this.scContainer.Name = "scContainer";
            this.scContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scContainer.Panel1
            // 
            this.scContainer.Panel1.Controls.Add(this.panelUp);
            // 
            // scContainer.Panel2
            // 
            this.scContainer.Panel2.Controls.Add(this.panelDown);
            this.scContainer.Size = new System.Drawing.Size(590, 468);
            this.scContainer.SplitterDistance = 59;
            this.scContainer.SplitterWidth = 1;
            this.scContainer.TabIndex = 13;
            this.scContainer.TabStop = false;
            // 
            // panelUp
            // 
            this.panelUp.Controls.Add(this.lblTitle);
            this.panelUp.Controls.Add(this.linklblDelete);
            this.panelUp.Controls.Add(this.linklblID);
            this.panelUp.Controls.Add(this.linklblTransfer);
            this.panelUp.Controls.Add(this.lblDateTime);
            this.panelUp.Controls.Add(this.linklblReply);
            this.panelUp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelUp.Location = new System.Drawing.Point(0, 0);
            this.panelUp.Name = "panelUp";
            this.panelUp.Size = new System.Drawing.Size(588, 57);
            this.panelUp.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(12, 13);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(35, 12);
            this.lblTitle.TabIndex = 3;
            this.lblTitle.Text = "Title";
            // 
            // linklblDelete
            // 
            this.linklblDelete.AutoSize = true;
            this.linklblDelete.Font = new System.Drawing.Font("新宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.linklblDelete.Location = new System.Drawing.Point(133, 36);
            this.linklblDelete.Name = "linklblDelete";
            this.linklblDelete.Size = new System.Drawing.Size(47, 12);
            this.linklblDelete.TabIndex = 11;
            this.linklblDelete.TabStop = true;
            this.linklblDelete.Text = "Delete";
            this.linklblDelete.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // linklblID
            // 
            this.linklblID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linklblID.Font = new System.Drawing.Font("新宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.linklblID.Location = new System.Drawing.Point(487, 36);
            this.linklblID.Name = "linklblID";
            this.linklblID.Size = new System.Drawing.Size(89, 12);
            this.linklblID.TabIndex = 2;
            this.linklblID.TabStop = true;
            this.linklblID.Text = "IDIDIDIDIDID";
            this.linklblID.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // linklblTransfer
            // 
            this.linklblTransfer.AutoSize = true;
            this.linklblTransfer.Font = new System.Drawing.Font("新宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.linklblTransfer.Location = new System.Drawing.Point(62, 36);
            this.linklblTransfer.Name = "linklblTransfer";
            this.linklblTransfer.Size = new System.Drawing.Size(61, 12);
            this.linklblTransfer.TabIndex = 10;
            this.linklblTransfer.TabStop = true;
            this.linklblTransfer.Text = "Transfer";
            this.linklblTransfer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDateTime
            // 
            this.lblDateTime.AutoSize = true;
            this.lblDateTime.Location = new System.Drawing.Point(190, 37);
            this.lblDateTime.Name = "lblDateTime";
            this.lblDateTime.Size = new System.Drawing.Size(119, 12);
            this.lblDateTime.TabIndex = 4;
            this.lblDateTime.Text = "2013-02-22 23:53:35";
            // 
            // linklblReply
            // 
            this.linklblReply.AutoSize = true;
            this.linklblReply.Font = new System.Drawing.Font("新宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.linklblReply.Location = new System.Drawing.Point(12, 36);
            this.linklblReply.Name = "linklblReply";
            this.linklblReply.Size = new System.Drawing.Size(40, 12);
            this.linklblReply.TabIndex = 9;
            this.linklblReply.TabStop = true;
            this.linklblReply.Text = "Reply";
            this.linklblReply.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelDown
            // 
            this.panelDown.Controls.Add(this.richtxtContent);
            this.panelDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDown.Location = new System.Drawing.Point(0, 0);
            this.panelDown.Name = "panelDown";
            this.panelDown.Size = new System.Drawing.Size(588, 406);
            this.panelDown.TabIndex = 0;
            // 
            // richtxtContent
            // 
            this.richtxtContent.BackColor = System.Drawing.SystemColors.Control;
            this.richtxtContent.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richtxtContent.Location = new System.Drawing.Point(11, 12);
            this.richtxtContent.Name = "richtxtContent";
            this.richtxtContent.ReadOnly = true;
            this.richtxtContent.Size = new System.Drawing.Size(565, 380);
            this.richtxtContent.TabIndex = 12;
            this.richtxtContent.TabStop = false;
            this.richtxtContent.Text = "";
            // 
            // MailDetailControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.scContainer);
            this.Name = "MailDetailControl";
            this.Size = new System.Drawing.Size(590, 468);
            this.scContainer.Panel1.ResumeLayout(false);
            this.scContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scContainer)).EndInit();
            this.scContainer.ResumeLayout(false);
            this.panelUp.ResumeLayout(false);
            this.panelUp.PerformLayout();
            this.panelDown.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.RichTextBox richtxtContent;
        private System.Windows.Forms.LinkLabel linklblDelete;
        private System.Windows.Forms.LinkLabel linklblTransfer;
        private System.Windows.Forms.LinkLabel linklblReply;
        private System.Windows.Forms.Label lblDateTime;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.LinkLabel linklblID;
        private System.Windows.Forms.SplitContainer scContainer;
        private System.Windows.Forms.Panel panelUp;
        private System.Windows.Forms.Panel panelDown;
    }
}
