namespace Nzl.Web.Pub.MobileNewSmth
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
        public WebBrowserForm()
        {
            InitializeComponent();

            //在新Form中的WebBrowser中打开
            (this.wbMobileNewSmth.ActiveXInstance as SHDocVw.WebBrowser).NewWindow2 += new SHDocVw.DWebBrowserEvents2_NewWindow2EventHandler(Form1_NewWindow2);

            //在主WebBrowser中打开
            (this.wbMobileNewSmth.ActiveXInstance as SHDocVw.WebBrowser).NewWindow3 += new SHDocVw.DWebBrowserEvents2_NewWindow3EventHandler(Form1_NewWindow3);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            this.wbMobileNewSmth.ScriptErrorsSuppressed = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ppDisp"></param>
        /// <param name="Cancel"></param>
        private void Form1_NewWindow2(ref object ppDisp, ref bool Cancel)
        {
            Cancel = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ppDisp"></param>
        /// <param name="Cancel"></param>
        /// <param name="dwFlags"></param>
        /// <param name="bstrUrlContext"></param>
        /// <param name="bstrUrl"></param>
        private void Form1_NewWindow3(ref object ppDisp, ref bool Cancel, uint dwFlags, string bstrUrlContext, string bstrUrl)
        {
            Cancel = true;
            this.wbMobileNewSmth.Navigate(bstrUrl);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void wbMobileNewSmth_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            //将所有的链接的目标，指向本窗体
            foreach (HtmlElement archor in this.wbMobileNewSmth.Document.Links)
            {
                archor.SetAttribute("target", "_self");
            }

            //将所有的FORM的提交目标，指向本窗体
            foreach (HtmlElement form in this.wbMobileNewSmth.Document.Forms)
            {
                form.SetAttribute("target", "_self");
            }
        }

        private void wbMobileNewSmth_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            System.Windows.Forms.WebBrowser wb = sender as System.Windows.Forms.WebBrowser;
            if (wb != null)
            {

                wb.Document.GetElementById("wraper").InnerHtml = "";
            }
        }
    }
}
