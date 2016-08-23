namespace Nzl.Smth.Forms
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;
    using Nzl.Smth.Controls.Elements;
    using Nzl.Smth.Datas;

    /// <summary>
    /// 
    /// </summary>
    public partial class ThreadForm : Form
    {
        /// <summary>
        /// 
        /// </summary>
        ThreadForm()
        {
            InitializeComponent();
            this.panel.Width = this.Width - 12;
        }

        /// <summary>
        /// 
        /// </summary>
        public ThreadForm(Thread thread)
            : this()
        {
            ThreadControl tc = CreateThreadControl(thread);
            tc.Top = 1;
            tc.Left = 1;
            this.panel.Controls.Add(tc);
            this.panel.Height = tc.Height + 2;
            this.Height = this.panel.Height + 35;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="thread"></param>
        /// <returns></returns>
        private ThreadControl CreateThreadControl(Thread thread)
        {
            ThreadControl tc = new ThreadControl();
            tc.SetWidth(this.panel.Width - 4);
            tc.Initialize(thread);
            tc.IsPlainView = true;
            return tc;
        }
    }
}
