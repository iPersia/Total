namespace Nzl.Web.Smth.Forms
{
    using System;
    using System.Windows.Forms;

    /// <summary>
    /// 
    /// </summary>
    public class BaseForm : Form
    {        
        /// <summary>
        /// 
        /// </summary>
        public BaseForm()
            : base()
        {
            this.Deactivate += BaseForm_Deactivate;            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BaseForm_Deactivate(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
