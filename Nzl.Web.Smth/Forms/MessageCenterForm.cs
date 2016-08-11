namespace Nzl.Web.Smth.Forms
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;
    using Nzl.Web.Smth.Utils;
    using Nzl.Web.Smth.Datas;
    /// <summary>
    /// 
    /// </summary>
    public partial class MessageCenterForm : BaseForm
    {
        /// <summary>
        /// 
        /// </summary>
        public static readonly MessageCenterForm Instance = new MessageCenterForm();

        /// <summary>
        /// 
        /// </summary>
        private Form _parentForm = null;

        /// <summary>
        /// 
        /// </summary>
        private RichTextBox _convertTxtBox = new RichTextBox();

        /// <summary>
        /// 
        /// </summary>
        MessageCenterForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        MessageCenterForm(Form parent)
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

            this.bgwMessager.WorkerReportsProgress = true;
            this.bgwMessager.DoWork +=new DoWorkEventHandler(bgwMessager_DoWork);
            this.bgwMessager.ProgressChanged += new ProgressChangedEventHandler(bgwMessager_ProgressChanged);
            this.bgwMessager.RunWorkerCompleted +=new RunWorkerCompletedEventHandler(bgwMessager_RunWorkerCompleted);
            this.bgwMessager.RunWorkerAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bgwMessager_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                while (true)
                {
                    Datas.Message msg = MessageQueue.Dequeue();
                    if (msg != null)
                    {
                        this.bgwMessager.ReportProgress(1, msg);
                        System.Threading.Thread.Sleep(50);
                    }
                    else
                    {
                        System.Threading.Thread.Sleep(1500);
                    }
                }
            }
            catch (Exception exp)
            {
                if (Program.LoggerEnabled)
                {
                    Program.Logger.Error(exp.Message);
                }

                e.Cancel = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bgwMessager_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Datas.Message msg = e.UserState as Datas.Message;
            if (msg != null)
            {
                this.txtMsg.AppendText(msg.DateTime.TimeOfDay.ToString() + "\t\t" + msg.Source + "\n");
                this.txtMsg.AppendText(msg.Detail + "\n");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bgwMessager_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                this.txtMsg.AppendText(e.Error.Message);
            }
            else if (e.Cancelled)
            {
                this.txtMsg.AppendText("MessageQueue Error!");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MessageCenterForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;            
        }
    }
}
