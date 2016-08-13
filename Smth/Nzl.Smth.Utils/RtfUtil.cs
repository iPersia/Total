namespace Nzl.Smth.Utils
{
    using Nzl.Controls;

    /// <summary>
    /// 
    /// </summary>
    public class RtfUtil
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static string GetRtfCode(string info)
        {
            System.Windows.Forms.RichTextBox txtBox = new System.Windows.Forms.RichTextBox();
            txtBox.Clear();
            txtBox.AppendText(info);
            string rtfValue = txtBox.Rtf;
            string startStr = @"\viewkind4\uc1\pard\lang2052";
            rtfValue = rtfValue.Substring(rtfValue.IndexOf(startStr) + startStr.Length);
            return rtfValue = rtfValue.Substring(0, rtfValue.LastIndexOf(@"\par"));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static string GetRtfCode(System.Drawing.Image image)
        {
            RichTextBoxEx txtBox = new RichTextBoxEx(); //Controls.RichTextBoxEx txtBox = new Controls.RichTextBoxEx();
            txtBox.Clear();
            txtBox.InsertImage(image);
            string rtfValue = txtBox.Rtf;
            string startStr = @"\viewkind4\uc1\pard\lang2052";
            rtfValue = rtfValue.Substring(rtfValue.IndexOf(startStr) + startStr.Length);
            return rtfValue = rtfValue.Substring(0, rtfValue.LastIndexOf(@"\par"));
        }
    }
}
