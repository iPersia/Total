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
            this.wbBrowser.Url = new Uri(url);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            this.wbBrowser.ScriptErrorsSuppressed = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void wbBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            //将所有的链接的目标，指向本窗体
            foreach (HtmlElement archor in this.wbBrowser.Document.Links)
            {
                archor.SetAttribute("target", "_self");
            }

            //将所有的FORM的提交目标，指向本窗体
            foreach (HtmlElement form in this.wbBrowser.Document.Forms)
            {
                form.SetAttribute("target", "_self");
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
