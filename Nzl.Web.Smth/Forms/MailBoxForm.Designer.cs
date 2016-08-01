namespace Nzl.Web.Smth.Forms
{
    partial class MailBoxForm
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
            this.panelContainer = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.mbcMailBox = new Nzl.Web.Smth.Controls.MailBoxControl();
            this.panelContainer.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelContainer
            // 
            this.panelContainer.Controls.Add(this.panel1);
            this.panelContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContainer.Location = new System.Drawing.Point(0, 0);
            this.panelContainer.Name = "panelContainer";
            this.panelContainer.Size = new System.Drawing.Size(643, 553);
            this.panelContainer.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.mbcMailBox);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(643, 553);
            this.panel1.TabIndex = 1;
            // 
            // mbcMailBox
            // 
            this.mbcMailBox.Location = new System.Drawing.Point(0, 0);
            this.mbcMailBox.Name = "mbcMailBox";
            this.mbcMailBox.Size = new System.Drawing.Size(643, 554);
            this.mbcMailBox.TabIndex = 0;
            // 
            // MailBoxForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(643, 553);
            this.Controls.Add(this.panelContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "MailBoxForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "MailBox";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MailBoxForm_FormClosing);
            this.panelContainer.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelContainer;
        private Controls.MailBoxControl mbcMailBox;
        private System.Windows.Forms.Panel panel1;
    }
}