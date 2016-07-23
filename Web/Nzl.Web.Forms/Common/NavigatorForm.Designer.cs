namespace Nzl.Web.Forms.Common
{
    partial class NavigatorForm
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
            this.wbNav = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // wbNav
            // 
            this.wbNav.AllowNavigation = false;
            this.wbNav.AllowWebBrowserDrop = false;
            this.wbNav.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wbNav.IsWebBrowserContextMenuEnabled = false;
            this.wbNav.Location = new System.Drawing.Point(0, 0);
            this.wbNav.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbNav.Name = "wbNav";
            this.wbNav.ScriptErrorsSuppressed = true;
            this.wbNav.Size = new System.Drawing.Size(1008, 476);
            this.wbNav.TabIndex = 0;
            this.wbNav.TabStop = false;
            this.wbNav.WebBrowserShortcutsEnabled = false;
            // 
            // NavigatorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 476);
            this.Controls.Add(this.wbNav);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "NavigatorForm";
            this.Text = "Navigator";
            this.Load += new System.EventHandler(this.NavigatorForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser wbNav;
    }
}