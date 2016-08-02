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
        private Form _prevForm = null;
        /// <summary>
        /// 
        /// </summary>
        public BaseForm()
            : base()
        {
            this.Deactivate += BaseForm_Deactivate;
            this.ShowIcon = false;
            this.ShowInTaskbar = false; ;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="form"></param>
        protected void SetPrevForm(Form form)
        {
            this._prevForm = form;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BaseForm_Deactivate(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(this.ToString() + " - BaseForm_Deactivate");
            this.Hide();
            if (this._prevForm != null)
            {
                this._prevForm.Show();
                this._prevForm.Focus();
            }
        }
    }
}
