namespace Nzl.Web.Forms.MobileNewSmth.Forms
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
            this.txtMsg = new Nzl.Web.Forms.MobileNewSmth.Controls.RichTextBoxEx();
            this.SuspendLayout();
            // 
            // txtMsg
            // 
            this.txtMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMsg.Location = new System.Drawing.Point(0, 0);
            this.txtMsg.Name = "txtMsg";
            this.txtMsg.Size = new System.Drawing.Size(574, 215);
            this.txtMsg.TabIndex = 0;
            this.txtMsg.Text = "";
            // 
            // MessageCenterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(574, 215);
            this.Controls.Add(this.txtMsg);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "MessageCenterForm";
            this.Text = "Message Center";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MessageCenterForm_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.RichTextBoxEx txtMsg;
        private System.ComponentModel.BackgroundWorker bgwMessager;

    }
}