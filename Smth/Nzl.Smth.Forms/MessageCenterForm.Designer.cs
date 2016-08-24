namespace Nzl.Smth.Forms
{
    partial class MessageCenterForm
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
            this.bgwMessager = new System.ComponentModel.BackgroundWorker();
            this.txtMsg = new Nzl.Controls.RichTextBoxEx();
            this.scContainer = new System.Windows.Forms.SplitContainer();
            this.panelUp = new System.Windows.Forms.Panel();
            this.txtCache = new Nzl.Controls.RichTextBoxEx();
            this.panelDown = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.scContainer)).BeginInit();
            this.scContainer.Panel1.SuspendLayout();
            this.scContainer.Panel2.SuspendLayout();
            this.scContainer.SuspendLayout();
            this.panelUp.SuspendLayout();
            this.panelDown.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtMsg
            // 
            this.txtMsg.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.txtMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMsg.HiglightColor = Nzl.Controls.RtfColor.White;
            this.txtMsg.Location = new System.Drawing.Point(0, 0);
            this.txtMsg.Name = "txtMsg";
            this.txtMsg.ReadOnly = true;
            this.txtMsg.Size = new System.Drawing.Size(782, 318);
            this.txtMsg.TabIndex = 0;
            this.txtMsg.Text = "";
            this.txtMsg.TextColor = Nzl.Controls.RtfColor.Black;
            // 
            // scContainer
            // 
            this.scContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.scContainer.Dock = System.Windows.Forms.DockStyle.Fill;
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
            this.scContainer.Size = new System.Drawing.Size(784, 561);
            this.scContainer.SplitterDistance = 240;
            this.scContainer.SplitterWidth = 1;
            this.scContainer.TabIndex = 1;
            // 
            // panelUp
            // 
            this.panelUp.Controls.Add(this.txtCache);
            this.panelUp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelUp.Location = new System.Drawing.Point(0, 0);
            this.panelUp.Name = "panelUp";
            this.panelUp.Size = new System.Drawing.Size(782, 238);
            this.panelUp.TabIndex = 0;
            // 
            // txtCache
            // 
            this.txtCache.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.txtCache.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCache.HiglightColor = Nzl.Controls.RtfColor.White;
            this.txtCache.Location = new System.Drawing.Point(0, 0);
            this.txtCache.Name = "txtCache";
            this.txtCache.ReadOnly = true;
            this.txtCache.Size = new System.Drawing.Size(782, 238);
            this.txtCache.TabIndex = 1;
            this.txtCache.Text = "";
            this.txtCache.TextColor = Nzl.Controls.RtfColor.Black;
            // 
            // panelDown
            // 
            this.panelDown.Controls.Add(this.txtMsg);
            this.panelDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDown.Location = new System.Drawing.Point(0, 0);
            this.panelDown.Name = "panelDown";
            this.panelDown.Size = new System.Drawing.Size(782, 318);
            this.panelDown.TabIndex = 1;
            // 
            // MessageCenterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.scContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.Name = "MessageCenterForm";
            this.Text = "Message Center";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MessageCenterForm_FormClosing);
            this.scContainer.Panel1.ResumeLayout(false);
            this.scContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scContainer)).EndInit();
            this.scContainer.ResumeLayout(false);
            this.panelUp.ResumeLayout(false);
            this.panelDown.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Nzl.Controls.RichTextBoxEx txtMsg;
        private System.ComponentModel.BackgroundWorker bgwMessager;
        private System.Windows.Forms.SplitContainer scContainer;
        private System.Windows.Forms.Panel panelUp;
        private System.Windows.Forms.Panel panelDown;
        private Nzl.Controls.RichTextBoxEx txtCache;
    }
}