namespace Nzl.Web.Forms
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Windows.Forms;
    using Nzl.Log4Net;

    /// <summary>
    /// The main form.
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// 
        /// </summary>
        private Dictionary<string, object> _dicWindows = new Dictionary<string, object>();

        /// <summary>
        /// 
        /// </summary>
        private string _closeFlag = null;        

        /// <summary>
        /// Ctor.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Hide();
            this.ShowInTaskbar = false;
            this.nfiMain.Visible = true;

#if (DEBUG)
            //this.GetRegisteredForm<MobileNewSmth.MobileNewSmthForm>().Show();
            //this.GetRegisteredForm<Rss.RssMonitorForm>().Show();
#endif
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this._closeFlag != "NotifyIcon")
            {
                e.Cancel = true;

                this.ShowInTaskbar = false;
                this.Hide();
                this.nfiMain.Visible = true;
            }
            else
            {
                if (this._dicWindows.Count > 0)
                {
                    e.Cancel = MessageBox.Show(this, 
                                               "There exists some window active, do you want close the form?", 
                                               "Warning", 
                                               MessageBoxButtons.YesNo, 
                                               MessageBoxIcon.Warning) 
                               != System.Windows.Forms.DialogResult.Yes;
                }
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

        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmsMain_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Name.Substring(4))
            {
                case "Exit":
                    {
                        this._closeFlag = "NotifyIcon";
                        this.Close();
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nfiMain_MouseDoubleClick(object sender, MouseEventArgs e)
        {            
            this.Show();
            this.ShowInTaskbar = true;
            this.nfiMain.Visible = false;
        }

        private void tsmiTools_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            Form form = null;
            switch (e.ClickedItem.Name.Substring(4).ToUpper())
            {
                case "SMTH":
                    {
                        //form = this.GetRegisteredForm<MobileNewSmth.Forms.MobileNewSmthForm>();                        
                    }
                    break;
                case "CLAWER":
                    {
                        form = this.GetRegisteredForm<ProductClawer.ProductClawerFom>();                        
                    }
                    break;
                case "RSS":
                    {
                        form = this.GetRegisteredForm<Rss.RssMonitorForm>();                        
                    }
                    break;
                case "RSSXMLDOWNLOADER":
                    {
                        form = this.GetRegisteredForm<Rss.RssXmlDownloaderForm>();
                    }
                    break;
                default:
                    {
                        form = null;
                    }
                    break;
            }

            if (form != null)
            {
                form.Show();
            }
        }
    }
}
