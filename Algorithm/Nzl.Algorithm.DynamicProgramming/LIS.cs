namespace Nzl.Algorithm.DynamicProgramming
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// The static longest increasing subsequnce (LIS) class.
    /// </summary>
    public static class LIS<T>
        where T : IComparable<T>
    {
        /// <summary>
        /// Get LIS of seq 1 and seq 2.
        /// </summary>
        /// <typeparam name="T">The IComparable template.</typeparam>
        /// <param name="seq">The sequence.</param>
        /// <returns>The LIS.</returns>
        public static T[] GetLIS(T[] seq)
        {
            List<int[]> listFunc = LIS<T>.GetLengthSequence(seq);
            if (listFunc != null && listFunc.Count > 0)
            {
                int maxLength = listFunc[seq.Length -1][1];
                int maxVIndex = seq.Length - 1;
                for (int i = seq.Length - 1; i >= 0; i--)
                {
                    if (listFunc[i][1] > maxLength)
                    {
                        maxLength = listFunc[i][1];
                        maxVIndex = i;
                    }
                }

                T[] result = new T[maxLength];
                for (int i = maxLength; i > 0; i--)
                {
                    result[i - 1] = seq[listFunc[maxVIndex][0]];
                    maxVIndex = listFunc[maxVIndex][2];
                }

                return result;
            }

            return null;
        }       


        /// <summary>
        /// Get length sequence.
        /// </summary>
        /// <typeparam name="T">The IComparable template.</typeparam>
        /// <param name="seq1">The sequence 1.</param>
        /// <param name="seq2">The sequence 2.</param>
        /// <returns>The length matrix.</returns>
        public static List<int[]> GetLengthSequence(T[] seq)
        {
            if (seq != null)
            {
                int seqSize = seq.Length;
                int[] lenSequence = new int[seqSize];
                List<int[]> listFunc = new List<int[]>();
                listFunc.Add(new int[3] { 0, 1, 0 }); //func index, length, last max value index
                for (int j = 1; j < seqSize; j++)
                {
                    int[] result = GetMaxSubSeq(seq, listFunc, j);
                    if (result != null)
                    {
                        listFunc.Add(result);
                    }
                }               

                return listFunc;
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="seq"></param>
        /// <param name="dic"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        private static int[] GetMaxSubSeq(T[] seq, List<int[]> listFunc, int pos)
        {
            if (listFunc != null)
            {
                int maxLength = 1;
                int secMaxInx = pos;
                for (int i = 0; i < pos; i++)
                {
                    if (seq[i].CompareTo(seq[pos]) < 0)
                    {
                        if (listFunc[i][1] + 1 > maxLength)
                        {
                            maxLength = listFunc[i][1] + 1;
                            secMaxInx = i;
                        }
                    }
                }

                return new int[3] { pos, maxLength, secMaxInx };
            }

            return null;
        }
    }
}
