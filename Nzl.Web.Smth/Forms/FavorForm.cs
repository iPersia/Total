namespace Nzl.Web.Smth.Forms
{
    using System.Windows.Forms;

    /// <summary>
    /// 
    /// </summary>
    public partial class FavorForm : BaseForm
    {
        /// <summary>
        /// 
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnFavorBoardLinkLableClicked; 
        
        /// <summary>
        /// 
        /// </summary>
        public FavorForm()
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
    }
}
