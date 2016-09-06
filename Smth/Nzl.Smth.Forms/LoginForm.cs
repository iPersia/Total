namespace Nzl.Smth.Forms
{
    using System;
    using Nzl.Smth;
    using Nzl.Smth.Controls.Complexes;

    /// <summary>
    /// 
    /// </summary>
    public partial class LoginForm : BaseForm
    {
        #region Singleton
        /// <summary>
        /// 
        /// </summary>
        public static readonly LoginForm Instance = new LoginForm();
        #endregion

        #region event
        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<MessageEventArgs> OnLoginFailed;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<MessageEventArgs> OnLogoutFailed;
        #endregion

        #region Ctor.
        /// <summary>
        /// 
        /// </summary>
        LoginForm()
        {
            InitializeComponent();
            this.lcLog.OnLoginCompleted += LcLog_OnLoginCompleted;
            this.lcLog.OnLoginFailed += LcLog_OnLoginFailed;
            this.lcLog.OnLogoutCompleted += LcLog_OnLogoutCompleted;
            this.lcLog.OnLogoutFailed += LcLog_OnLogoutFailed;
        }
        #endregion

        #region eventhandler
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LcLog_OnLogoutFailed(object sender, MessageEventArgs e)
        {
            if (this.OnLogoutFailed!= null)
            {
                this.Hide();
                this.OnLogoutFailed(sender, e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LcLog_OnLogoutCompleted(object sender, EventArgs e)
        {
            this.Hide();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LcLog_OnLoginFailed(object sender, MessageEventArgs e)
        {
            if (this.OnLoginFailed != null)
            {
                this.Hide();
                this.OnLoginFailed(sender, e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LcLog_OnLoginCompleted(object sender, EventArgs e)
        {
            this.Hide();
        }
        #endregion
    }
}
