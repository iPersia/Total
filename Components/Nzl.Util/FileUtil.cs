namespace Nzl.Utils
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// Util for operating file.
    /// </summary>
    public static class FileUtil
    {
        /// <summary>
        /// Write text to file.
        /// </summary>
        /// <param name="filename">The file name.</param>
        /// <param name="text">The content to be written.</param>
        /// <returns>The operation message.</returns>
        public static string WriteText(string filename, string text)
        {
            try
            {
                FileStream fs = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                sw.BaseStream.Seek(0, SeekOrigin.End);
                sw.Write(text);
                sw.Flush();
                sw.Close();
                fs.Close();
                return "S_Writing text successfully!";
            }
            catch (Exception e)
            {
                return "E_" + e.Message;
            }
        }
    }
}
