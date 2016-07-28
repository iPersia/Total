namespace Nzl.Web.Smth.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;
    using Nzl.Hook;
    using Nzl.Web.Page;
    using Nzl.Web.Smth.Common;
    using Nzl.Web.Smth.Controls;
    using Nzl.Web.Smth.Datas;
    using Nzl.Web.Smth.Utils;
    using Nzl.Web.Util;

    /// <summary>
    /// 
    /// </summary>
    public partial class TabbedBrowserForm : Form
    {
        #region Singleton
        /// <summary>
        /// 
        /// </summary>
        public static readonly TabbedBrowserForm Instance = new TabbedBrowserForm();
        #endregion

        #region Variable
        /// <summary>
        /// 
        /// </summary>
        private Dictionary<string, object> _dicWindows = new Dictionary<string, object>();

        /// <summary>
        /// 
        /// </summary>
        private Form _parentForm = null;

        /// <summary>
        /// 
        /// </summary>
        private const int CLOSE_SIZE = 16;

        /// <summary>
        /// 
        /// </summary>
        private const int PADDING_SIZE = 4;

        /// <summary>
        /// 
        /// </summary>
        private UserActivityHook _uahKey = new UserActivityHook(false, true);
        #endregion

        #region Ctor
        /// <summary>
        /// 
        /// </summary>
        TabbedBrowserForm()
        {
            InitializeComponent();
            LogStatus.Instance.LoginStatusChanged += Instance_LoginStatusChanged;
            _uahKey.KeyUp += new EventHandler<KeyExEventArgs>(Global_KeyUp);
            _uahKey.Start();

            //(new TestForm()).ShowDialog(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Instance_LoginStatusChanged(object sender, LogStatusEventArgs e)
        {
            SetButtonVisibleByLogInStatus(e.IsLogin);
        }

        /// <summary>
        /// 
        /// </summary>
        TabbedBrowserForm(Form parent)
            : this()
        {
            this._parentForm = parent;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="form"></param>
        public void SetParent(Form parent)
        {
            this._parentForm = parent;
        }

        /// <summary>
        /// Clear all tabpages and windows.
        /// </summary>
        public void Clear()
        {
            try
            {
                foreach (TabPage tp in this.tcTopics.TabPages)
                {
                    tp.Dispose();
                }

                this.tcTopics.TabPages.Clear();
                GC.Collect();
            }
            catch { };

            try
            {
                foreach (Form form in this._dicWindows.Values)
                {
                    form.Dispose();
                }

                this._dicWindows.Clear();
                GC.Collect();
            }
            catch { };
        }
        #endregion

        #region override
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Top10sForm.Instance.OnTopLinkClicked += Instance_OnTopLinkClicked;
            Top10sForm.Instance.OnTopBoardLinkClicked += Instance_OnTopBoardLinkClicked;
        }
        #endregion

        #region Topic
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        public void AddTopic(string url, string subject)
        {
            this.Text = subject;
            string key = "tp" + url;
            ///Exsits
            {
                TabPage tp = GetTabPage(url);
                if (tp != null)
                {
                    this.tcTopics.SelectedTab = tp;
                    return;
                }
            }

            ///NOT Exsits
            {
                TopicBrowserControl tbc = new TopicBrowserControl();
                tbc.Name = "tbc" + url;
                tbc.TopicUrl = url;
                tbc.Dock = DockStyle.Fill;
                tbc.OnThreadDeleteLinkClicked += Tbc_OnThreadDeleteLinkClicked;
                tbc.OnThreadEditLinkClicked += Tbc_OnThreadEditLinkClicked;
                tbc.OnThreadMailLinkClicked += Tbc_OnThreadMailLinkClicked;
                tbc.OnThreadQueryTypeLinkClicked += Tbc_OnThreadQueryTypeLinkClicked;
                tbc.OnThreadReplyLinkClicked += Tbc_OnThreadReplyLinkClicked;
                tbc.OnThreadTransferLinkClicked += Tbc_OnThreadTransferLinkClicked;
                tbc.OnThreadUserLinkClicked += TBF_IDLinkClicked;
                tbc.OnTopicReplyLinkClicked += Tbc_OnTopicReplyLinkClicked;
                tbc.OnBoardLinkClicked += Tbc_OnBoardLinkClicked;

                TabPage tp = new TabPage();
                tp.Name = key;
                tp.Text = subject == null ? "Unknown" : subject.Length > 8 ? subject.Substring(0, 8) + ".." : "" + subject;
                //tp.Text = subject;
                tp.ToolTipText = subject;
                tp.Controls.Add(tbc);
                this.tcTopics.TabPages.Add(tp);
                this.tcTopics.SelectedTab = tp;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tbc_OnBoardLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel linkLabel = sender as LinkLabel;
            if (linkLabel != null)
            {
                this.AddBoard(e.Link.LinkData.ToString(), linkLabel.Text);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tbc_OnTopicReplyLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            NewThreadForm threadForm = new NewThreadForm("回复 - " + this.Text, e.Link.LinkData.ToString(), "Re: " + this.tcTopics.SelectedTab.ToolTipText);
            threadForm.StartPosition = FormStartPosition.CenterParent;
            if (DialogResult.OK == threadForm.ShowDialog(this))
            {
                e.Link.Tag = "Success";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tbc_OnThreadTransferLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ///throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tbc_OnThreadReplyLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel linkLabel = sender as LinkLabel;
            if (linkLabel != null)
            {
                Thread thread = linkLabel.Tag as Thread;
                if (thread != null)
                {
                    NewThreadForm newThreadForm = new NewThreadForm(this.tcTopics.SelectedTab.ToolTipText, thread.ReplyUrl, "Re: " + this.tcTopics.SelectedTab.ToolTipText, SmthUtil.GetReplyContent(thread), true);
                    newThreadForm.StartPosition = FormStartPosition.CenterParent;
                    if (DialogResult.OK == newThreadForm.ShowDialog(this))
                    {
                        e.Link.Tag = "Success";
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tbc_OnThreadQueryTypeLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel linkLabel = sender as LinkLabel;
            if (linkLabel != null)
            {
                string userID = Nzl.Web.Util.CommonUtil.GetMatch(@"au=(?'ID'[a-zA-z][a-zA-Z0-9]{1,11})", e.Link.LinkData.ToString(), 1);
                TopicForm topicForm = new TopicForm(Nzl.Web.Util.CommonUtil.GetUrlBase(e.Link.LinkData.ToString()), userID);
                topicForm.StartPosition = FormStartPosition.CenterParent;
                topicForm.ShowDialog(this);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tbc_OnThreadMailLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel linkLabel = sender as LinkLabel;
            if (linkLabel != null)
            {
                Thread thread = linkLabel.Tag as Thread;
                if (thread != null)
                {
                    NewMailForm newMailForm = new NewMailForm(thread.ID, "Re: " + this.tcTopics.SelectedTab.ToolTipText, SmthUtil.GetReplyContent(thread));
                    newMailForm.StartPosition = FormStartPosition.CenterParent;
                    newMailForm.ShowDialog(this);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tbc_OnThreadEditLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel linkLabel = sender as LinkLabel;
            if (linkLabel != null)
            {
                Thread thread = linkLabel.Tag as Thread;
                if (thread != null)
                {
                    Regex regex = new Regex(@"\s*FROM\s[\d, \., \*]+");
                    string content = regex.Replace(thread.Tag.ToString(), "");
                    content = CommonUtil.ReplaceSpecialChars(content);
                    content = SmthUtil.TrimUrls(content);
                    NewThreadForm newThreadForm = new NewThreadForm(this.tcTopics.SelectedTab.ToolTipText, thread.EditUrl, "Re: " + this.tcTopics.SelectedTab.ToolTipText, content, false);
                    newThreadForm.StartPosition = FormStartPosition.CenterParent;
                    if (DialogResult.OK == newThreadForm.ShowDialog(this))
                    {
                        e.Link.Tag = "Success";
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tbc_OnThreadDeleteLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel linkLabel = sender as LinkLabel;
            if (linkLabel != null)
            {
                Thread thread = linkLabel.Tag as Thread;
                if (thread != null)
                {
                    MessageForm confirmForm = new MessageForm("提示", "确认删除此信息？");
                    confirmForm.StartPosition = FormStartPosition.CenterParent;
                    DialogResult dlgResult = confirmForm.ShowDialog(this);
                    if (dlgResult == DialogResult.OK)
                    {
                        WebPage page = WebPageFactory.CreateWebPage(thread.DeleteUrl);
                        string result = CommonUtil.GetMatch(@"<div id=\Wm_main\W><div class=\Wsp hl f\W>(?'Result'\w+)</div>", page.Html, "Result");
                        if (result != null && result.Contains("成功"))
                        {
                            e.Link.Tag = "Success";
                        }
                    }
                }
            }
        }
        #endregion

        #region Board
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="title"></param>
        public void AddBoard(string url, string title)
        {
            this.Text = title;
            string key = "tp" + url;
            ///Exsits
            {
                TabPage tp = GetTabPage(url);
                if (tp != null)
                {
                    this.tcTopics.SelectedTab = tp;
                    return;
                }
            }

            ///NOT Exsits
            {
                BoardBrowserControl bbc = new BoardBrowserControl(url);
                bbc.OnTopicLinkClicked += new LinkLabelLinkClickedEventHandler(BoardBrowserControl_OnTopicLinkClicked);
                bbc.OnTopicCreateIDLinkClicked += new LinkLabelLinkClickedEventHandler(TBF_IDLinkClicked);
                bbc.OnTopicLastIDLinkClicked += new LinkLabelLinkClickedEventHandler(TBF_IDLinkClicked);
                bbc.Dock = DockStyle.Fill;

                TabPage tp = new TabPage();
                tp.Name = "tp" + url; ;
                tp.Text = "[ " + title + " ]";
                tp.ToolTipText = tp.Text;
                tp.Controls.Add(bbc);
                this.tcTopics.TabPages.Add(tp);
                this.tcTopics.SelectedTab = tp;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoardBrowserControl_OnTopicLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel linkLabel = sender as LinkLabel;
            if (linkLabel != null)
            {
                this.AddTopic(e.Link.LinkData.ToString(), linkLabel.Text);
            }
        }
        #endregion

        #region TabPages
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private TabPage GetTabPage(string url)
        {
            lock (this.tcTopics)
            {
                string key = "tp" + url;
                if (this.tcTopics.TabPages.ContainsKey(key))
                {
                    return this.tcTopics.TabPages[key];
                }

                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tcTopics_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Text = (this.tcTopics.SelectedTab == null) ? "Browser" : this.tcTopics.SelectedTab.ToolTipText;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabbedTopicBrowserForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
            if (this._parentForm != null)
            {
                this._parentForm.Focus();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tcTopics_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.tcTopics.SelectedIndex > -1)
            {
                TabPage tp = this.tcTopics.TabPages[this.tcTopics.SelectedIndex];
                this.tcTopics.TabPages.Remove(tp);
                tp.Dispose();
                GC.Collect();
            }
        }
        #endregion

        #region Event handler
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected void Global_KeyUp(object sender, KeyExEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Global_KeyUp - " + HookUtil.GetForegroundWindowThreadProcessName());
            System.Diagnostics.Debug.WriteLine("Global_KeyUp - " +
                                               "Control.ModifierKeys " + Control.ModifierKeys + "\t" +
                                               "Modifiers " + e.Modifiers + "\t" +
                                               "Alt " + e.Alt + "\t" +
                                               "Control " + e.Control + "\t" +
                                               "Shift " + e.Shift + "\t" +
                                               e.KeyCode + "\t" + e.KeyValue + "\t" + e.KeyData);
            if (HookUtil.GetForegroundWindowThreadProcessName() == "Nzl.Web.Smth")
            {
                if ((e.KeyCode == Keys.LControlKey || e.KeyCode == Keys.RControlKey) && Control.ModifierKeys == Keys.Control)
                {
                    ShowTop10s();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TBF_IDLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel linkLabel = sender as LinkLabel;
            if (linkLabel != null)
            {
                UserForm userForm = new UserForm(e.Link.LinkData.ToString());
                userForm.StartPosition = FormStartPosition.CenterScreen;
                userForm.ShowDialog(this);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogIn_Click(object sender, EventArgs e)
        {
            ShowFormOnCenterParent(LoginForm.Instance);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBoardNavi_Click(object sender, EventArgs e)
        {
            BoardNavigatorForm form = this.GetRegisteredForm<BoardNavigatorForm>();
            if (form != null)
            {
                form.OnBoardLinkLableClicked -= Form_OnBoardLinkLableClicked;
                form.OnBoardLinkLableClicked += Form_OnBoardLinkLableClicked;
                form.StartPosition = FormStartPosition.Manual;
                int centerX = this.Location.X + this.Size.Width / 2;
                int centerY = this.Location.Y + this.Size.Height / 2;
                form.Location = new System.Drawing.Point(centerX - form.Size.Width / 2, centerY - form.Size.Height / 2);
                form.Show();
                form.Focus();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFavor_Click(object sender, EventArgs e)
        {
            FavorForm form = this.GetRegisteredForm<FavorForm>();
            if (form != null)
            {
                form.OnFavorBoardLinkLableClicked -= Form_OnBoardLinkLableClicked;
                form.OnFavorBoardLinkLableClicked += Form_OnBoardLinkLableClicked;
                form.StartPosition = FormStartPosition.Manual;
                int centerX = this.Location.X + this.Size.Width / 2;
                int centerY = this.Location.Y + this.Size.Height / 2;
                form.Location = new System.Drawing.Point(centerX - form.Size.Width / 2, centerY - form.Size.Height / 2);
                form.Show();
                form.Focus();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form_OnBoardLinkLableClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel linkLabel = sender as LinkLabel;
            if (linkLabel != null)
            {
                TabbedBrowserForm.Instance.AddBoard(e.Link.LinkData.ToString(), linkLabel.Text);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMail_Click(object sender, EventArgs e)
        {
            ShowFormOnCenterParent(MailBoxForm.Instance);

            //ShowFormOnCenterParent(this.GetRegisteredForm<MailBoxForm>());

            //MailBoxForm form = this.GetRegisteredForm<MailBoxForm>();
            //if (form != null)
            //{
            //    form.StartPosition = FormStartPosition.Manual;
            //    int centerX = this.Location.X + this.Size.Width / 2;
            //    int centerY = this.Location.Y + this.Size.Height / 2;
            //    form.Location = new System.Drawing.Point(centerX - form.Size.Width / 2, centerY - form.Size.Height / 2);
            //    form.Show();
            //    form.Focus();
            //}
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMessge_Click(object sender, EventArgs e)
        {
            ShowFormOnCenterParent(MessageCenterForm.Instance);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbc_OnTopLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel linklbl = sender as LinkLabel;
            if (linklbl != null)
            {
                TabbedBrowserForm.Instance.AddTopic(e.Link.LinkData.ToString(), linklbl.Text);
                //SmthForm.Browser.AddTopic(e.Link.LinkData.ToString(), linklbl.Text);
                TabbedBrowserForm.Instance.Show();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            foreach (TabPage tp in this.tcTopics.TabPages)
            {
                tp.Dispose();
            }

            this.tcTopics.TabPages.Clear();
            GC.Collect();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnHome_Click(object sender, EventArgs e)
        {
            if (this._parentForm != null)
            {
                this._parentForm.Focus();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLoadTop_Click(object sender, EventArgs e)
        {
            ShowTop10s();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Instance_OnTopBoardLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel linklbl = sender as LinkLabel;
            if (linklbl != null)
            {
                this.AddBoard(@"http://m.newsmth.net/board/" + e.Link.LinkData.ToString(), linklbl.Text);
                this.Show();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Instance_OnTopLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel linklbl = sender as LinkLabel;
            if (linklbl != null)
            {
                this.AddTopic(e.Link.LinkData.ToString(), linklbl.Text);
                this.Show();
            }
        }
        #endregion

        #region Private
        /// <summary>
        /// 
        /// </summary>
        private void SetButtonVisibleByLogInStatus(bool flag)
        {
            if (this.IsDisposed == false)
            {
                lock (this.linklblUserID)
                {
                    if (flag)
                    {
#if (DEBUG)
                        Nzl.Web.Util.CommonUtil.ShowMessage("this.linklblUserID.Links count:" + this.linklblUserID.Links.Count);
#endif

                        string welcomeStr = "Welcome ";
                        this.linklblUserID.Text = welcomeStr + LogStatus.Instance.UserID + "!";
                        this.linklblUserID.Links.Clear();
                        this.linklblUserID.Links.Add(welcomeStr.Length, LogStatus.Instance.UserID.Length, LogStatus.Instance.UserID);

                        this.linklblUserID.LinkClicked -= new LinkLabelLinkClickedEventHandler(TBF_IDLinkClicked);
                        this.linklblUserID.LinkClicked += new LinkLabelLinkClickedEventHandler(TBF_IDLinkClicked);
                        this.btnLogon.Text = "Log Out";
                    }
                    else
                    {
                        this.linklblUserID.Text = "Welcome!";
                        this.linklblUserID.Links.Clear();
                        this.btnLogon.Text = "Log In";
                    }
                }

                this.btnFavor.Visible = flag;
                this.btnMail.Visible = flag;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        private T GetRegisteredForm<T>()
            where T : class, new()
        {
            string key = typeof(T).ToString();
            if (this._dicWindows.ContainsKey(key))
            {
                Form form = this._dicWindows[key] as Form;
                if (form.IsDisposed == false)
                {
                    return this._dicWindows[key] as T;
                }
            }

            T t = new T();
            this._dicWindows.Remove(key);
            this._dicWindows.Add(key, t);
            return t;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        private T GetRegisteredForm<T>(string key)
            where T : class, new()
        {
            if (this._dicWindows.ContainsKey(key))
            {
                Form form = this._dicWindows[key] as Form;
                if (form.IsDisposed == false)
                {
                    return this._dicWindows[key] as T;
                }
            }

            T t = new T();
            this._dicWindows.Remove(key);
            this._dicWindows.Add(key, t);
            return t;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="form"></param>
        private void ShowFormOnCenterParent(Form form)
        {
            if (form != null && form.IsDisposed == false)
            {
                form.StartPosition = FormStartPosition.Manual;
                int centerX = this.Location.X + this.Size.Width / 2;
                int centerY = this.Location.Y + this.Size.Height / 2;
                form.Location = new System.Drawing.Point(centerX - form.Size.Width / 2, centerY - form.Size.Height / 2);
                form.Show();
                form.Focus();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void ShowTop10s()
        {
            Form form = Top10sForm.Instance;
            if (form != null && form.IsDisposed == false)
            {
                form.StartPosition = FormStartPosition.Manual;
                int centerX = this.Location.X + this.Size.Width / 2;
                int centerY = this.Location.Y + this.Size.Height / 2;
                form.Location = new System.Drawing.Point(centerX - form.Size.Width / 2, centerY - form.Size.Height / 2);
                form.Visible = !form.Visible;
            }
        }
        #endregion        
    }
}
