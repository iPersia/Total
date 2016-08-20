namespace Nzl.Smth.Forms
{
    using System;
    using System.Windows.Forms;

    /// <summary>
    /// 
    /// </summary>
    public class BaseForm : Form
    {
        #region variable
        /// <summary>
        /// 
        /// </summary>
        private bool _bActive = false;

        /// <summary>
        /// 
        /// </summary>
        private Form _prevForm = null;
        #endregion

        #region Ctor.
        /// <summary>
        /// 
        /// </summary>
        public BaseForm()
            : base()
        {
            this.Deactivate += BaseForm_Deactivate;
            this.Activated += BaseForm_Activated;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.HideWhenDeactivate = true;
        }
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public bool Active
        {
            get
            {
                return this._bActive;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected bool HideWhenDeactivate
        {
            get;
            set;
        }
        #endregion

        #region eventhandler
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BaseForm_Activated(object sender, EventArgs e)
        {
#if (X)
            Nzl.Web.Util.CommonUtil.ShowMessage(this, "\tBaseForm - BaseForm_Activated");
#endif
            this._bActive = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BaseForm_Deactivate(object sender, EventArgs e)
        {
#if (X)
            Nzl.Web.Util.CommonUtil.ShowMessage(this, "\tBaseForm - BaseForm_Deactivate");
#endif
            this._bActive = false;
            if (this.HideWhenDeactivate)
            {
                this.Hide();
                if (this._prevForm != null)
                {
                    this._prevForm.Show();
                    this._prevForm.Focus();
                }
            }            
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="form"></param>
        protected void SetPrevForm(Form form)
        {
            this._prevForm = form;
        }

        #region virtual

        #endregion

        #region static
        /// <summary>
        /// 
        /// </summary>
        /// <param name="form"></param>
        protected void ShowFormOnCenterParent(Form form)
        {
            if (form != null && form.IsDisposed == false)
            {
                ///form.Owner = this;
                form.StartPosition = FormStartPosition.Manual;
                int centerX = this.Location.X + this.Size.Width / 2;
                int centerY = this.Location.Y + this.Size.Height / 2;
                form.Location = new System.Drawing.Point(centerX - form.Size.Width / 2, centerY - form.Size.Height / 2);
                form.Show();
                form.Focus();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="form"></param>
        protected DialogResult ShowFormAsDialog(Form form)
        {
            if (form != null && form.IsDisposed == false)
            {
                form.StartPosition = FormStartPosition.CenterParent;
                return form.ShowDialog(this);
            }

            return DialogResult.None;
        }
        #endregion
    }
}
