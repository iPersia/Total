namespace Nzl.Web.Smth.Forms
{
    partial class LoginForm
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
            this.lcLog = new Nzl.Web.Smth.Containers.LoginControl();
            this.SuspendLayout();
            // 
            // lcLog
            // 
            this.lcLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lcLog.Location = new System.Drawing.Point(0, 0);
            this.lcLog.Name = "lcLog";
            this.lcLog.Size = new System.Drawing.Size(271, 264);
            this.lcLog.TabIndex = 0;
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(271, 264);
            this.Controls.Add(this.lcLog);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginForm";
            this.Text = "Logon";
            this.ResumeLayout(false);

        }

        #endregion

        private Containers.LoginControl lcLog;
    }
}