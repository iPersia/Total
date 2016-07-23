namespace Nzl.Test.Algorithm
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Nzl.Algorithm.DynamicProgramming;
    using Nzl.Core;
    using Nzl.Core.Interface;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Test_DynamicProgramming : ITest
    {
        public void Test()
        {
            TestLIS();
        }

        private void TestLIS()
        {
            string sequence = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz";
            //string sequence = "56490465406865468760121654979879451";
            //string sequence = "0123456789";
            char[] charSeq = sequence.ToCharArray();            
            //List<int[]> lenSeq = LIS<char>.GetLengthSequence(charSeq);
            //foreach (int[] seq in lenSeq)
            //{
            //    System.Console.WriteLine(seq[0] + "\t" + seq[1] + "\t" + seq[2] + "\t" + charSeq[seq[2]]);
            //}

            for (int i = 1; i <= sequence.Length; i++)
            {
                string tempStr = sequence.Substring(0, i);
                charSeq = tempStr.ToCharArray();
                char[] lis = LIS<char>.GetLIS(charSeq);
                System.Console.WriteLine(tempStr);
                System.Console.WriteLine(new string(lis));
            }            
        }

        private void TestLCS()
        {
            string str1 = @"8qe9fqj32q39radmpfpa39u23jrfmadfiouq2p3mfp0aef89u 2mf0ad89fakeqper90qjemfpadf,algkqp40a,pdfjp9283rjfapdfjp0239jfpasdfkpq390rjmapdfouqp23890t7u789hgnaflmqe";//   
            string str2 = @"adfagadf  apdf qpea ifqp39r-239 8ajefpq 01`=- 093-r9ujwqek p    [1p 90=203rka[wedfk ][- efikqweo  ik23[-0 kadpfi[q0rmadfkjp[2390rfkamdflakp0e9kamdfald9f8-qw3rikfa[dfa0ef9q-03984019923hn4 aljfapw9fmqwer890123p4rjndfpak";//
            char[] charS1 = str1.ToCharArray();
            char[] charS2 = str2.ToCharArray();
            int[,] matrix = LCS<char>.GetLengthMatrix(charS1, charS2);
            char[] charLcs = LCS<char>.GetLCS(charS1, charS2);
            string lcs = new string(charLcs);
            int lcsLength = LCS<char>.GetLCSLength(charS1, charS2);
            bool isInvalidLcs = (lcs.Length == lcsLength && lcs.Length == matrix[charS1.Length, charS2.Length] && Util.IsContains(charS2, charLcs));
            System.Console.WriteLine("Seq 1:" + str1);
            System.Console.WriteLine("Seq 1:" + str2);
            System.Console.WriteLine("\nResult:");
            System.Console.WriteLine("   LCS: " + lcs);
            System.Console.WriteLine("   LCS Length: " + lcs.Length);
            System.Console.WriteLine("   LCS is " + (isInvalidLcs ? "Valid!" : "InValid!"));
            if (isInvalidLcs == false)
            {
                System.Console.WriteLine("Fact is:");
                System.Console.WriteLine("\tLCS Length: " + lcsLength);
                PrintLCSLengthMatrix(str1.ToCharArray(), str2.ToCharArray(), matrix);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        private void PrintLCS(Dictionary<int, object[]> result)
        {
            if (result != null)
            {
                System.Console.WriteLine("The length is " + result.Count);
                foreach (KeyValuePair<int, object[]> kp in result)
                {
                    System.Console.WriteLine("(" + kp.Value[0].ToString() + ", " +
                                            kp.Value[1].ToString() + ")\t" + 
                                            kp.Value[2].ToString());
                }
            }
        }

        /// <summary>
        /// Print the LCS length matrix.
        /// </summary>
        /// <param name="matrix"></param>
        private void PrintLCSLengthMatrix(char[] str1, char[] str2, int[,] matrix)
        {
            int seq1Size = str1.Length;
            int seq2Size = str2.Length;
            System.Console.WriteLine("The LCS Length Matrix");
            string margin = "  ";
            System.Console.Write(margin + margin);
            foreach (char val in str1)
            {
                System.Console.Write(margin + val);
            }

            System.Console.WriteLine();
            for (int j = 0; j < seq2Size + 1; j++)
            {
                if (j == 0)
                {
                    System.Console.Write(" " + margin);
                }
                else
                {
                    System.Console.Write(str2[j - 1] + margin);
                }

                for (int i = 0; i < seq1Size + 1; i++)
                {
                    System.Console.Write(matrix[i, j]);

                    if (i > 0 && j > 0 && str1[i - 1] == str2[j - 1])
                    {
                        System.Console.Write("*" + margin.Substring(1));
                    }
                    else
                    {
                        System.Console.Write(margin);
                    }
                }

                System.Console.WriteLine();
                for (int x = 0; x < margin.Length / 2; x++)
                {
                    System.Console.WriteLine();
                }
            }
        }
    }
}
