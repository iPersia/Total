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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lbl"></param>
        /// <param name="url"></param>
        protected void InitializeLabel(Label lbl, string text)
        {
            if (lbl != null)
            {
                lbl.Text = text;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lbl"></param>
        /// <param name="url"></param>
        protected void InitializeLinkLabel(LinkLabel lbl, string url)
        {
            this.InitializeLinkLabel(lbl, lbl != null ? lbl.Text : "", url);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lbl"></param>
        /// <param name="url"></param>
        protected void InitializeLinkLabel(LinkLabel lbl, string text, string url)
        {
            if (lbl != null)
            {
                lbl.Visible = false;
                lbl.Text = text;
                lbl.Links.Clear();
                if (string.IsNullOrEmpty(url) == false)
                {
                    lbl.Visible = true;
                    lbl.Links.Add(0, lbl.Text.Length, url);
                }
            }
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
