namespace Nzl.Web.Forms.Rss
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;

    /// <summary>
    /// 
    /// </summary>
    public partial class RssXmlDownloaderForm : Form
    {
        /// <summary>
        /// 
        /// </summary>
        public RssXmlDownloaderForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGetXml_Click(object sender, EventArgs e)
        {
            try
            {
                Uri uri = new Uri(this.txtUrl.Text);
                if (uri != null)
                {
                    SetEnabled(false);
                    this.bgwXmlDownloader.DoWork +=new DoWorkEventHandler(bgwXmlDownloader_DoWork);
                    this.bgwXmlDownloader.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgwXmlDownloader_RunWorkerCompleted);
                    this.bgwXmlDownloader.RunWorkerAsync(uri.AbsoluteUri);
                }
            }
            catch (Exception exp)
            {
                if (Program.LoggerEnabled)
                {
                    Program.Logger.Error(exp.Message);
                }

                this.rtxtRssXml.Text = "";
                this.rtxtRssXml.AppendText(exp.Message + "\n" + exp.StackTrace);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        private System.Text.Encoding GetEncoding(byte[] bytes)
        {
            if (bytes != null)
            {
                string content = System.Text.Encoding.Default.GetString(bytes);
                if (content != null)
                {
                    int index = content.IndexOf("encoding=\"");
                    if (index != -1)
                    {
                        try
                        {
                            content = content.Substring(index + "encoding=\"".Length);
                            string enc = content.Substring(0, content.IndexOf("\""));
                            return System.Text.Encoding.GetEncoding(enc);
                        }
                        catch (Exception exp)
                        {
                            if (Program.LoggerEnabled)
                            {
                                Program.Logger.Error(exp.Message);
                            }
                        }
                    }
                }
            }

            return System.Text.Encoding.UTF8;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="flag"></param>
        private void SetEnabled(bool flag)
        {
            this.txtUrl.Enabled = flag;
            this.btnGetXml.Enabled = flag;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bgwXmlDownloader_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                System.Net.WebClient wc = new System.Net.WebClient();
                byte[] bytes = wc.DownloadData(e.Argument as string);
                System.IO.MemoryStream ms = new System.IO.MemoryStream(bytes, 0, bytes.Length);
                e.Result = new System.IO.StreamReader(ms, this.GetEncoding(bytes)).ReadToEnd();
            }
            catch (Exception exp)
            {
                if (Program.LoggerEnabled)
                {
                    Program.Logger.Error(exp.Message);
                }

                e.Result = "Do work Exception Accured:\n" + exp.Message + "\n" + exp.StackTrace;
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bgwXmlDownloader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (e.Result != null)
                {
                    this.rtxtRssXml.Text = "";
                    this.rtxtRssXml.AppendText(e.Result as string);
                }
            }
            catch (Exception exp)
            {
                if (Program.LoggerEnabled)
                {
                    Program.Logger.Error(exp.Message);
                }

                this.rtxtRssXml.Text = "";
                this.rtxtRssXml.AppendText("Run worker Completed Exception Accured:\n" + exp.Message + "\n" + exp.StackTrace);
            }
            finally
            {
                SetEnabled(true);
            }
        }        
    }
}
