namespace Nzl.Web.Pub.MobileNewSmth
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;
    using Nzl.Web.Util;
    using Nzl.Web.Page;

    /// <summary>
    /// 
    /// </summary>
    public partial class FavorForm : Form
    {
        /// <summary>
        /// 
        /// </summary>
        private string _favorUrl = @"http://m.newsmth.net/favor";

        /// <summary>
        /// 
        /// </summary>
        public FavorForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            LoadFavor();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        private void LoadFavor()
        {
            this.bwFetchPage = new System.ComponentModel.BackgroundWorker();
            this.bwFetchPage.DoWork += new System.ComponentModel.DoWorkEventHandler(bwFetchPage_DoWork);
            this.bwFetchPage.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwFetchPage_RunWorkerCompleted);
            this.bwFetchPage.RunWorkerAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bwFetchPage_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                BackgroundWorker bw = sender as BackgroundWorker;
                e.Result = WebPageFactory.CreateWebPage(this._favorUrl);
            }
            catch (Exception exp)
            {
                if (Program.LoggerEnabled)
                {
                    Program.Logger.Error(exp.Message);
                }

#if (DEBUG)
                Nzl.Web.Util.CommonUtil.ShowMessage(typeof(TopicForm), exp.Message);
#endif
                e.Result = null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bwFetchPage_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }
            else if (e.Cancelled)
            {
                MessageBox.Show("Canceled");
            }
            else
            {
                GetFavors(e.Result as WebPage);
            }

            this.panel.Focus();
        }

        /// <summary>
        /// 
        /// </summary>
        private void GetFavors(WebPage page)
        {
            if (page != null && page.IsGood)
            {
                MatchCollection mtFavor = CommonUtil.GetMatchCollection(@"(<li>|<li class=\Whl\W>)版面\|<a href=\W/board/(?'Url'[\w, %2E, %5F]+)\W>(?'Title'[\w, ·]+)\([\w, %2E, %5F]+\)</a></li>", page.Html);
                if (mtFavor != null)
                {
                    int accumulateHeight = 0;
                    int width = this.panel.Width;
                    bool flag = false;
                    this.panel.Controls.Clear();
                    string stat = string.Empty;
                    int index = 1;
                    foreach (Match mt in mtFavor)
                    {
                        string url = @"http://m.newsmth.net/board/" + mt.Groups["Url"].Value.ToString();
                        string title = mt.Groups["Title"].Value.ToString();

                        BoardControl bc = new BoardControl(index++.ToString("00 - "), url, title);
                        bc.OnLinkClicked += new LinkLabelLinkClickedEventHandler(BoardControlLink_LinkClicked);
                        bc.Top = accumulateHeight;
                        bc.Width = width;
                        if (flag)
                        {
                            bc.BackColor = Color.White;
                        }

                        flag = !flag;
                        accumulateHeight += bc.Height + 3;
                        this.panel.Controls.Add(bc);
                    }

                    this.panel.Height = accumulateHeight;
                    this.Height = accumulateHeight + 28;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoardControlLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel linkLabel = sender as LinkLabel;
            if (linkLabel != null)
            {
                BoardForm boardForm = new BoardForm(e.Link.LinkData.ToString());
                boardForm.StartPosition = FormStartPosition.CenterScreen;
                boardForm.Show();
            }
        }
    }
}
