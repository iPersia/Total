namespace Nzl.Smth.Forms
{
    using System;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;
    using Nzl.Web.Util;    
    using Nzl.Web.Page;
    using Nzl.Smth.Datas;
    
    /// <summary>
    /// Class.
    /// </summary>
    public partial class TestForm : Form
    {
        /// <summary>
        /// Ctor.
        /// </summary>
        public TestForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            System.Drawing.Image image = System.Drawing.Image.FromFile("D:/26b57b720341669a.jpg");
            ///System.Drawing.Image image = System.Drawing.Image.FromFile("D:/stream.gif"); ///

            string rtfCode = null;
            {
                //rtfCode = Nzl.Smth.Utils.RtfUtil.GetRtfCode(image);                
            }

            {
                this.richTextBoxEx1.Clear();
                Clipboard.SetData(DataFormats.Bitmap, image);
                this.richTextBoxEx1.Paste();
                rtfCode = this.richTextBoxEx1.Rtf;
            }

            this.richTextBoxEx1.InsertLink(rtfCode, @"http://m.newsmth.net", this.richTextBoxEx1.Text.Length);
        }
    }
}
