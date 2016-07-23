namespace Nzl.Web.ProductClawer.Clawers
{
    using System;
    using System.Drawing;
    using Nzl.Web.Util;

    /// <summary>
    /// The 360buy price image reader.
    /// </summary>
    internal static class The360buyPriceImageReader
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public static decimal GetPrice(Bitmap bitmap)
        {
            if (bitmap != null)
            {
                if (bitmap.Width % 10 != 0)
                {
                    return decimal.MinusOne;
                }

                //数字数量
                int countOfNumber = bitmap.Width / 10 - 3;
                int[] numbers = new int[countOfNumber + 1];
                int counter = 0;
                int startpos, endpos;
                string priceStr = "";

                //整数部分
                for (int i = 0; i < countOfNumber - 2; i++)
                {
                    startpos = 15 + i * 11;
                    endpos = 15 + i * 11 + 11;
                    numbers[counter] = The360buyPriceImageReader.GetNumber(bitmap, startpos, endpos);
                    priceStr += numbers[counter]; 
                    counter++;                    
                }

                //小数点
                startpos = 15 + 11 * (countOfNumber - 2);
                endpos = 15 + 11 * (countOfNumber - 2) + 5;
                priceStr += "."; 
                counter++;

                //小数部分
                for (int i = 0; i < 2; i++)
                {
                    startpos = 15 + 11 * (countOfNumber - 2) + 5 + i * 11;
                    endpos = 15 + 11 * (countOfNumber - 2) + 5 + i * 11 + 11;
                    numbers[counter] = The360buyPriceImageReader.GetNumber(bitmap, startpos, endpos);
                    priceStr += numbers[counter]; 
                    counter++;
                }
                
                try
                {
                    return System.Convert.ToDecimal(priceStr);
                }
                catch (Exception exp)
                {
#if (DEBUG)
                    CommonUtil.ShowMessage(typeof(The360buyPriceImageReader), exp.Message);
#endif
                    return decimal.MinusOne;
                }
            }

            return decimal.MinusOne;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="?"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        private static int GetNumber(Bitmap bitmap, int start, int end)
        {
            int redones = GetRedOnes(bitmap, start, end);
            int[] redonesOfNumber = new int[] { 46, 28, 37, 40, 36, 40, 43, 29, 47, 43 };
            int finalnumber = -1;
            for (int i = 0; i < 10; i++)
            {
                if (redones == redonesOfNumber[i])
                {
                    finalnumber = i;
                    break;
                }
            }

            switch (redones)
            {
                case 40:
                    {
                        int lRedOnes = GetLHalfRedOnes(bitmap, start, end);
                        int rRedOnes = GetRHalfRedOnes(bitmap, start, end);
                        int uRedOnes = GetUHalfRedOnes(bitmap, start, end);
                        int dRedOnes = GetDHalfRedOnes(bitmap, start, end);

                        //3 - 15 25 22 18 
                        //5 - 19 21 22 18
                        if (lRedOnes == 15 && rRedOnes == 25 && uRedOnes == 22 && dRedOnes == 18)
                        {
                            finalnumber = 3;
                        }
                        else if (lRedOnes == 19 && rRedOnes == 21 && uRedOnes == 22 && dRedOnes == 18)
                        {
                            finalnumber = 5;
                        }
                        else
                        {
                            finalnumber = -1;
                        }
                    }
                    break;
                case 43:
                    {
                        int lRedOnes = GetLHalfRedOnes(bitmap, start, end);
                        int rRedOnes = GetRHalfRedOnes(bitmap, start, end);
                        int uRedOnes = GetUHalfRedOnes(bitmap, start, end);
                        int dRedOnes = GetDHalfRedOnes(bitmap, start, end);

                        //6 - 23 20 22 21
                        //9 - 17 26 26 17
                        if (lRedOnes == 23 && rRedOnes == 20 && uRedOnes == 22 && dRedOnes == 21)
                        {
                            finalnumber = 6;
                        }
                        else if (lRedOnes == 17 && rRedOnes == 26 && uRedOnes == 26 && dRedOnes == 17)
                        {
                            finalnumber = 9;
                        }
                        else
                        {
                            finalnumber = -1;
                        }
                    }
                    break;
                default:
                    {

                    }
                    break;
            }

            return finalnumber;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        private static int GetRedOnes(Bitmap bitmap, int start, int end)
        {
            int redOneCount = 0;
            for (int w = start; w < end; w++)
            {
                for (int h = 0; h < bitmap.Height; h++)
                {
                    Color color = bitmap.GetPixel(w, h);
                    if (color.R > color.B && color.R > color.G)
                    {
                        redOneCount++;
                    }
                }
            }

            return redOneCount;
        }

        /// <summary>
        /// Left half.
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        private static int GetLHalfRedOnes(Bitmap bitmap, int start, int end)
        {
            int redOneCount = 0;
            int middleWidth = (start + end) / 2;
            for (int w = start; w < middleWidth; w++)
            {
                for (int h = 0; h < bitmap.Height; h++)
                {
                    Color color = bitmap.GetPixel(w, h);
                    if (color.R > color.B && color.R > color.G)
                    {
                        redOneCount++;
                    }
                }
            }

            return redOneCount;
        }

        /// <summary>
        /// Right half.
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        private static int GetRHalfRedOnes(Bitmap bitmap, int start, int end)
        {
            int redOneCount = 0;
            int middleWidth = (start + end) / 2;
            for (int w = middleWidth; w < end; w++)
            {
                for (int h = 0; h < bitmap.Height; h++)
                {
                    Color color = bitmap.GetPixel(w, h);
                    if (color.R > color.B && color.R > color.G)
                    {
                        redOneCount++;
                    }
                }
            }

            return redOneCount;
        }

        /// <summary>
        /// Up half.
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        private static int GetUHalfRedOnes(Bitmap bitmap, int start, int end)
        {
            int redOneCount = 0;
            int middleHeight = bitmap.Height / 2;
            for (int w = start; w < end; w++)
            {
                for (int h = 0; h < middleHeight; h++)
                {
                    Color color = bitmap.GetPixel(w, h);
                    if (color.R > color.B && color.R > color.G)
                    {
                        redOneCount++;
                    }
                }
            }

            return redOneCount;
        }

        /// <summary>
        /// Down half.
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        private static int GetDHalfRedOnes(Bitmap bitmap, int start, int end)
        {
            int redOneCount = 0;
            int middleHeight = bitmap.Height / 2;
            for (int w = start; w < end; w++)
            {
                for (int h = middleHeight; h < bitmap.Height; h++)
                {
                    Color color = bitmap.GetPixel(w, h);
                    if (color.R > color.B && color.R > color.G)
                    {
                        redOneCount++;
                    }
                }
            }

            return redOneCount;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="picUrl"></param>
        /// <returns></returns>
        public static decimal GetPrice(string picUrl)
        {
            if (string.IsNullOrEmpty(picUrl) == false)
            {
                System.Net.WebClient wc = new System.Net.WebClient();
                byte[] temp = wc.DownloadData(picUrl);
                System.IO.MemoryStream ms = new System.IO.MemoryStream(temp, 0, temp.Length);
                Image image = Image.FromStream(ms);
                return The360buyPriceImageReader.GetPrice(new Bitmap(image));
            }

            return decimal.MinusOne;
        }
    }
}
