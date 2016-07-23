namespace Nzl.Web.Forms.MobileNewSmth.Forms
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;
    using Nzl.Web.Forms.MobileNewSmth.Controls;

    public partial class BrowserForm : Form
    {
        /// <summary>
        /// 
        /// </summary>
        private Form _parentForm = null;

        /// <summary>
        /// url,topic
        /// </summary>
        private Dictionary<string, TopicListItem> _dicTopic = new Dictionary<string, TopicListItem>();

        /// <summary>
        /// 
        /// </summary>
        BrowserForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        public BrowserForm(Form parent)
            : this()
        {
            this._parentForm = parent;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            this.lbTopics.SelectedIndexChanged += new EventHandler(lbTopics_SelectedIndexChanged);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbTopics_SelectedIndexChanged(object sender, EventArgs e)
        {
            lock (this.panelContainer)
            {
                System.Diagnostics.Debug.WriteLine("lbTopics_SelectedIndexChanged");
                this.panelContainer.Controls.Clear();
                TopicListItem tli = this.lbTopics.SelectedItem as TopicListItem;
                if (tli != null)
                {
                    tli.Control.Dock = DockStyle.Fill;
                    this.panelContainer.Controls.Add(tli.Control);
                    this.Text = tli.Subject;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        public void AddTopic(string url, string subject)
        {
            TopicListItem tli = GetTopicListItem(url, subject);
            this.lbTopics.SelectedItem = tli;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private TopicListItem GetTopicListItem(string url, string subject)
        {
            lock (this.lbTopics)
            {
                if (_dicTopic.ContainsKey(url))
                {
                    return _dicTopic[url];
                }

                TopicListItem tli = new TopicListItem();
                TopicBrowserControl tbc = new TopicBrowserControl();
                tbc.TopicUrl = url;
                tli.Url = url;
                tli.Subject = subject;
                tli.Control = tbc;
                _dicTopic.Add(url, tli);
                this.lbTopics.Items.Add(tli);
                return tli;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private class TopicListItem
        {
            /// <summary>
            /// 
            /// </summary>
            public string Url
            {
                get;
                set;
            }

            /// <summary>
            /// 
            /// </summary>
            public string Subject
            {
                get;
                set;
            }

            /// <summary>
            /// 
            /// </summary>
            public TopicBrowserControl Control
            {
                get;
                set;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                return this.Subject == null ? "Unknown" : this.Subject;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BrowserForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
            this._parentForm.Focus();
        }
    }
}
