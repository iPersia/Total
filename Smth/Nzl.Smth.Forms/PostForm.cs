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
    public partial class PostForm : Form
    {
        /// <summary>
        /// 
        /// </summary>
        public event LinkLabelLinkClickedEventHandler OnBoardClicked;

        /// <summary>
        /// 
        /// </summary>
        private PostControlContainer _pcContainer = new PostControlContainer();

        /// <summary>
        /// 
        /// </summary>
        public PostForm()
        {
            InitializeComponent();
            this._pcContainer.Dock = DockStyle.Fill;
            this._pcContainer.OnBoardClicked += PostControlContainer_OnBoardClicked;
            this.panelContainer.Controls.Add(this._pcContainer);

            ///First loading.
            this._pcContainer.CreateControl();
            this._pcContainer.SetParentControl(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PostControlContainer_OnBoardClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.OnBoardClicked!= null)
            {
                this.OnBoardClicked(sender, e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Url
        {
            set
            {
                this._pcContainer.Url = value;
            }
        }
    }
}
