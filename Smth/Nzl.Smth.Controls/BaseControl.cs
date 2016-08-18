namespace Nzl.Smth.Controls
{
    using System;
    using System.Windows.Forms;
    using Nzl.Smth.Datas;

    /// <summary>
    /// 
    /// </summary>
    public class BaseControl<TBaseItem> : UserControl
        where TBaseItem : BaseItem
    {
        /// <summary>
        /// 
        /// </summary>
        public BaseControl()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public virtual void Initialize(TBaseItem item)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="width"></param>
        public virtual void SetWidth(int width)
        {
            this.Width = width;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(this.CanBeDisposed);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual bool CanBeDisposed
        {
           get
            {
                return false;
            }
        }
    }
}
