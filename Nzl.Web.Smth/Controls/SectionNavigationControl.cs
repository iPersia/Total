namespace Nzl.Web.Smth.Controls
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;
    using Datas;

    /// <summary>
    /// 
    /// </summary>
    public partial class SectionNavigationControl : BaseControl
    {
        #region variable
        #endregion

        #region Ctor.
        /// <summary>
        /// 
        /// </summary>
        public SectionNavigationControl()
        {
            InitializeComponent();
        }
        #endregion

        #region override
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override Panel GetContainer()
        {
            return this.panel;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected override string GetUrl(UrlInfo info)
        {
            return info.BaseUrl;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected override Control CreateControl(BaseItem item)
        {
            return base.CreateControl(item);
        }
        #endregion


    }
}
