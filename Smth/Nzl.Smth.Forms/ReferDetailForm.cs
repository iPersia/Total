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
    using Nzl.Smth.Controls.Containers;

    /// <summary>
    /// 
    /// </summary>
    public partial class ReferDetailForm : Form
    {
        /// <summary>
        /// 
        /// </summary>
        ReferDetailForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        public ReferDetailForm(string url)
            : this()
        {
            ReferDetailControlContainer rdcc = new ReferDetailControlContainer();
            rdcc.Url = url;
            rdcc.Dock = DockStyle.Fill;
            rdcc.CreateControl();
            this.panelContainer.Controls.Add(rdcc);
        }
    }
}
