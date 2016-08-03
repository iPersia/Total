namespace Nzl.Web.Smth.Forms
{
    using System;
    using SHDocVw;
    using System.Windows.Forms;

    /// <summary>
    /// 
    /// </summary>
    public partial class WebBrowserForm : Form
    {
        /// <summary>
        /// 
        /// </summary>
        private string _url = null;

        /// <summary>
        /// 
        /// </summary>
        WebBrowserForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        public WebBrowserForm(string url)
            : this()
        {
            this._url = url;            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            this.wbBrowser.Url = new Uri(this._url);
            this.wbBrowser.ScriptErrorsSuppressed = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void wbBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            ///Background Color.
            this.wbBrowser.Document.BackColor = System.Drawing.Color.FromArgb(33, 33, 33);

            ///将所有的链接的目标，指向本窗体
            foreach (HtmlElement archor in this.wbBrowser.Document.Links)
            {
                archor.SetAttribute("target", "_self");
            }

            ///将所有的FORM的提交目标，指向本窗体
            foreach (HtmlElement form in this.wbBrowser.Document.Forms)
            {
                form.SetAttribute("target", "_self");
            }

            ///Set window's width.
            if (this.wbBrowser.Document.Images.Count > 0)
            {
                try
                {
                    int width = Convert.ToInt32(this.wbBrowser.Document.Images[0].GetAttribute("width"));
                    if (width > 0)
                    {
                        this.Width = width + 40 + this.Width - this.wbBrowser.Width;
                    }
                }
                catch { };
            }            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void wbBrowser_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            System.Windows.Forms.WebBrowser wb = sender as System.Windows.Forms.WebBrowser;
            if (wb != null)
            {
                wb.Document.GetElementById("wraper").InnerHtml = "";
            }
        }
    }
}
