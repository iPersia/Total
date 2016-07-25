namespace Nzl.Web.Smth.Forms
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;
    using Nzl.Web.Util;
    using Nzl.Web.Page;
    using Nzl.Web.Smth.Controls;

    /// <summary>
    /// Class.
    /// </summary>
    public partial class BoardNavigatorForm : BaseForm
    {
        /// <summary>
        /// 
        /// </summary>
        private string _sectionRootUrl = @"http://m.newsmth.net/section";

        /// <summary>
        /// 
        /// </summary>
        private string _currentNaviUrl;

        /// <summary>
        /// 
        /// </summary>
        private string _prevNaviUrl;

        /// <summary>
        /// Ctor.
        /// </summary>
        public BoardNavigatorForm()
        {
            InitializeComponent();
            this._currentNaviUrl = this._sectionRootUrl;
        }

        /// <summary>
        /// Ctor.
        /// </summary>
        public BoardNavigatorForm(string url)
            : this()
        {
            this._currentNaviUrl = url;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnShown(EventArgs e)
        {
            FetchPage(this._currentNaviUrl);
            base.OnShown(e);            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        private void GetPrevUrl(WebPage page)
        {
            this._prevNaviUrl = null;
            if (page != null)
            {
                this._prevNaviUrl = CommonUtil.GetMatch(@"<div class=\Wsec sp\W><a href=\W(?'UpperLayer'/section.*)\W>上一层</a>", page.Html, 1);
                if (string.IsNullOrEmpty(this._prevNaviUrl) == false)
                {
                    this._prevNaviUrl = @"http://m.newsmth.net" + this._prevNaviUrl;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        private void GetPageTitle(WebPage page)
        {
            if (page != null)
            {
                this.Text = CommonUtil.TrimHtml(CommonUtil.GetMatch(@"<div class=\Wmenu sp\W>(?'Title'[\w,\W]+)</div><div id=\Wm_main\W>", page.Html, 1));
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
                string type = e.Link.Tag.ToString();
                string url = e.Link.LinkData.ToString();
                if (type == "Board")
                {
                    TabbedBrowserForm.Instance.AddBoard(url, linkLabel.Text);
                }

                if (type == "Section")
                {
                    FetchPage(url);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void GetSectionsAndBoards(WebPage page)
        {
            if (page != null && page.IsGood)
            {
                if (page != null && page.IsGood)
                {                    
                    MatchCollection mtColSection = CommonUtil.GetMatchCollection(@"(<li>|<li class=\Whl\W>)<font color=\W#f60\W>目录</font>\|<a href=\W(?'SectionUrl'/section/[\w, \., \-, %2E, %5F]+)\W>(?'SectionTitle'[\w, ·]+)</a>", page.Html);
                    MatchCollection mtColBoard = CommonUtil.GetMatchCollection(@"(<li>|<li class=\Whl\W>)版面\|<a href=\W(?'BoardUrl'/board/[\w,  \., \-, %2E, %5F]+)\W>(?'BoardTitle'[\w, ·]+)</a>\|</a></li>", page.Html);
                    int accumulateHeight = 0; 
                    int width = this.panel.Width - 19;
                    bool flag = false;
                    this.panel.Controls.Clear();
                    string stat = string.Empty;
                    if (mtColSection != null)
                    {
                        foreach (Match mt in mtColSection)
                        {
                            string url = @"http://m.newsmth.net" + mt.Groups[2].Value.ToString();
                            string title = mt.Groups[3].Value.ToString();

                            SectionControl sc = new SectionControl(url, title);
                            sc.OnLinkClicked += new LinkLabelLinkClickedEventHandler(BoardControlLink_LinkClicked);
                            sc.Top = accumulateHeight;
                            sc.Width = width;
                            if (flag)
                            {
                                sc.BackColor = Color.White;
                            }

                            flag = !flag;
                            accumulateHeight += sc.Height + 3;
                            this.panel.Controls.Add(sc);                            
                        }

                        stat += mtColSection.Count + "个目录 ";
                    }

                    if (mtColBoard != null)
                    {
                        foreach (Match mt in mtColBoard)
                        {
                            string url = @"http://m.newsmth.net" + mt.Groups[2].Value.ToString();
                            string title = mt.Groups[3].Value.ToString();

                            BoardControl bc = new BoardControl(url, title);
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

                        stat += mtColBoard.Count + "个版面";
                    }

                    this.lblStat.Text = stat;
                }
            }
        }        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this._prevNaviUrl) == false)
            {
                FetchPage(this._prevNaviUrl);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        private void FetchPage(string url)
        {
            this.bwFetchPage = new System.ComponentModel.BackgroundWorker();
            this.bwFetchPage.DoWork += new System.ComponentModel.DoWorkEventHandler(bwFetchPage_DoWork);
            this.bwFetchPage.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwFetchPage_RunWorkerCompleted);
            this.bwFetchPage.RunWorkerAsync(url);
            this.SetBtnEnabled(false);
            this.lblStat.Text = "";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="flag"></param>
        private void SetBtnEnabled(bool flag)
        {
            this.btnPrev.Enabled = flag;
            this.panel.Enabled = flag;
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
                string url = e.Argument as string;
                e.Result = WebPageFactory.CreateWebPage(url);
                this._currentNaviUrl = url;
            }
            catch (Exception exp)
            {
                if (Program.LoggerEnabled)
                {
                    Program.Logger.Error(exp.Message);
                }

#if (DEBUG)
                CommonUtil.ShowMessage(typeof(BoardNavigatorForm), exp.Message);
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
                WebPage page = e.Result as WebPage;
                if (page != null)
                {
                    GetSectionsAndBoards(page);
                    GetPrevUrl(page);
                    GetPageTitle(page);
                }
            }

            SetBtnEnabled(true);
            this.panel.Focus();
        }        
    }
}
