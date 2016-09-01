namespace Nzl.Smth.Datas
{
    using Nzl.Recycling;

    /// <summary>
    /// 
    /// </summary>
    public class BaseData : IRecycled
    {
        /// <summary>
        /// 
        /// </summary>
        public virtual string ID
        {
            set;
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual bool Updated
        {
            set;
            get;
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
