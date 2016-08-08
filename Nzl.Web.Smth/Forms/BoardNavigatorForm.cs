namespace Nzl.Web.Smth.Forms
{
    using System.Windows.Forms;

    /// <summary>
    /// Class.
    /// </summary>
    public partial class BoardNavigatorForm : BaseForm
    {
        #region Singleton
        /// <summary>
        /// 
        /// </summary>
        public static readonly BoardNavigatorForm Instance = new BoardNavigatorForm();
        #endregion

        /// <summary>
        /// 
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnBoardLinkLableClicked;

        /// <summary>
        /// 
        /// </summary>
        private Form _parentForm = null;

        /// <summary>
        /// Ctor.
        /// </summary>
        BoardNavigatorForm()
        {
            InitializeComponent();
            this.sncSection.OnBoardLinkClicked += SncSection_OnBoardLinkClicked;
            this.sncSection.SetParent(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SncSection_OnBoardLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.OnBoardLinkLableClicked != null)
            {
                this.OnBoardLinkLableClicked(sender, e);
            }
        }

        private void BoardNavigatorForm_FormClosing(object sender, FormClosingEventArgs e)
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
