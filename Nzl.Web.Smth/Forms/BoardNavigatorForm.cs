namespace Nzl.Web.Smth.Forms
{
    using System.Windows.Forms;

    /// <summary>
    /// Class.
    /// </summary>
    public partial class BoardNavigatorForm : BaseForm
    {
        /// <summary>
        /// 
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnBoardLinkLableClicked;
        
        /// <summary>
        /// Ctor.
        /// </summary>
        public BoardNavigatorForm()
        {
            InitializeComponent();
            this.sncSection.OnBoardLinkClicked += SncSection_OnBoardLinkClicked;
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
    }
}
