namespace Nzl.Smth.Forms
{
    using System;
    using System.Windows.Forms;

    /// <summary>
    /// Class.
    /// </summary>
    public partial class MessageForm : Form
    {
        /// <summary>
        /// Ctor.
        /// </summary>
        public MessageForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Ctor.
        /// </summary>
        public MessageForm(string msg)
            : this()
        {
            this.richtxtMessage.AppendText(msg);
            int rowCount = this.richtxtMessage.GetLineFromCharIndex(richtxtMessage.SelectionStart) + 1;
            this.Height = (this.richtxtMessage.Font.Height + 2) * rowCount + (this.Height - this.richtxtMessage.Height);
            this.richtxtMessage.Height = (this.richtxtMessage.Font.Height + 2) * rowCount;
        }

        /// <summary>
        /// Ctor.
        /// </summary>
        public MessageForm(string caption, string msg)
            : this(msg)
        {
            this.Text = caption;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
    }
}
