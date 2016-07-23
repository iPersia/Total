namespace Nzl.Web.Forms.MobileNewSmth.Forms
{
    partial class WebBrowserForm
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
            this.wbMobileNewSmth = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // wbMobileNewSmth
            // 
            this.wbMobileNewSmth.AllowWebBrowserDrop = false;
            this.wbMobileNewSmth.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wbMobileNewSmth.Location = new System.Drawing.Point(0, 0);
            this.wbMobileNewSmth.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbMobileNewSmth.Name = "wbMobileNewSmth";
            this.wbMobileNewSmth.Size = new System.Drawing.Size(971, 558);
            this.wbMobileNewSmth.TabIndex = 0;
            this.wbMobileNewSmth.Url = new System.Uri("http://m.newsmth.net", System.UriKind.Absolute);
            this.wbMobileNewSmth.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.wbMobileNewSmth_DocumentCompleted);
            this.wbMobileNewSmth.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.wbMobileNewSmth_Navigated);
            // 
            // WebBrowserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(971, 558);
            this.Controls.Add(this.wbMobileNewSmth);
            this.Name = "WebBrowserForm";
            this.Text = "WebBrowser";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser wbMobileNewSmth;
    }
}