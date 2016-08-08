namespace Nzl.Web.Smth.Forms
{
    using System.Windows.Forms;

    /// <summary>
    /// 
    /// </summary>
    public partial class FavorForm : BaseForm
    {
        #region Singleton
        /// <summary>
        /// 
        /// </summary>
        public static readonly FavorForm Instance = new FavorForm();
        #endregion

        /// <summary>
        /// 
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnFavorBoardLinkLableClicked;

        /// <summary>
        /// 
        /// </summary>
        private Form _parentForm = null;

        /// <summary>
        /// 
        /// </summary>
        FavorForm()
        {
            InitializeComponent();
            this.fcFavor.OnBoardLinkClicked += FcFavor_OnBoardLinkClicked;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FcFavor_OnBoardLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {  
            if (this.OnFavorBoardLinkLableClicked != null)
            {
                this.OnFavorBoardLinkLableClicked(sender, e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FavorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
            if (this._parentForm != null)
            {
                this._parentForm.Focus();
            }
        }
    }
}
