namespace Nzl.Smth.Controls
{
    using System;
    using System.Windows.Forms;
    using Nzl.Smth.Datas;

    /// <summary>
    /// 
    /// </summary>
    public class BaseControl : UserControl
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
        public virtual void Initialize(BaseItem item)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="width"></param>
        public virtual void SetWidth(int width)
        {
        }
    }
}
