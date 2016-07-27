namespace Nzl.Web.Smth.Forms
{
    partial class TestForm
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
            this.tcMail = new System.Windows.Forms.TabControl();
            this.SuspendLayout();
            // 
            // tcMail
            // 
            this.tcMail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcMail.Location = new System.Drawing.Point(0, 0);
            this.tcMail.Name = "tcMail";
            this.tcMail.SelectedIndex = 0;
            this.tcMail.Size = new System.Drawing.Size(797, 565);
            this.tcMail.TabIndex = 0;
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(797, 565);
            this.Controls.Add(this.tcMail);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "TestForm";
            this.Text = "TestForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcMail;
    }
}