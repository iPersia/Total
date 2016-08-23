namespace Nzl.Smth.Controls.Base
{
    using System;
    using System.Windows.Forms;
    using Nzl.Smth.Datas;

    /// <summary>
    /// 
    /// </summary>
    public class BaseControl<TBaseData> : UserControl
        where TBaseData : BaseData
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
        public TBaseData Data
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public virtual void Initialize(TBaseData data)
        {
            this.Data = data;
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
