namespace Nzl.Smth.Controls.Base
{
    using System.Windows.Forms;
    using Nzl.Recycling;
    using Nzl.Smth.Datas;

    /// <summary>
    /// 
    /// </summary>
    public class BaseControl<TBaseData> : UserControl, IRecycled
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

        #region IRecycled
        /// <summary>
        /// A boolean indicated whether the object is recycled.
        /// </summary>
        public bool IsRecycled
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public RecycledStatus Status
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void Reusing()
        {
            this.Status = RecycledStatus.Using;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void Recycling()
        {
            this.Status = RecycledStatus.Recycled;
        }
        #endregion
    }
}
