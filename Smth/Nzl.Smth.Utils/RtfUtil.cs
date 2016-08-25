namespace Nzl.Smth.Utils
{
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

        /////// <summary>
        /////// 
        /////// </summary>
        /////// <param name="info"></param>
        /////// <returns></returns>
        ////public static string GetRtfCode(System.Drawing.Image image)
        ////{
        ////    RichTextBoxEx txtBox = new RichTextBoxEx(); //Controls.RichTextBoxEx txtBox = new Controls.RichTextBoxEx();
        ////    txtBox.Clear();
        ////    txtBox.InsertImage(image);
        ////    string rtfValue = txtBox.Rtf;
        ////    string startStr = @"\viewkind4\uc1\pard\lang2052";
        ////    rtfValue = rtfValue.Substring(rtfValue.IndexOf(startStr) + startStr.Length);
        ////    return rtfValue = rtfValue.Substring(0, rtfValue.LastIndexOf(@"\par"));
        ////}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static string GetRtfCode(System.Drawing.Image image)
        {
            if (image == null)
                throw new System.Exception("Image is null!");             

            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            ms.Close();
            byte[] bs = ms.ToArray();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("{");

            sb.Append("\\pict");
            sb.Append("\\jpegblip");
            sb.Append("\\picscalex100");
            sb.Append("\\picscaley100");
            sb.Append("\\picwgoal" + System.Convert.ToString(image.Size.Width * 15));
            sb.Append("\\pichgoal" + System.Convert.ToString(image.Size.Height * 15));
            sb.Append(GetImageData(bs));
            sb.Append("}");
            return sb.ToString();
        }

        private static string staticStrIndentString = "   ";
        private static string Hexs = "0123456789abcdef";

        /// <summary>
		/// write binary data
		/// </summary>
		/// <param name="bs">binary data</param>
		private static string GetImageData(byte[] bs)
        {
            if (bs == null || bs.Length == 0)
                return "";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" ");
            for (int iCount = 0; iCount < bs.Length; iCount++)
            {
                if ((iCount % 32) == 0)
                {
                    sb.Append(System.Environment.NewLine);
                    sb.Append(staticStrIndentString);
                }
                else if ((iCount % 8) == 0)
                {
                    sb.Append(" ");
                }

                byte b = bs[iCount];
                int h = (b & 0xf0) >> 4;
                int l = b & 0xf;
                sb.Append(Hexs[h]);
                sb.Append(Hexs[l]);
            }

            return sb.ToString();
        }
    }
}
