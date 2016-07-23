namespace Nzl.Algorithm.DynamicProgramming
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// The longest common sequence class.
    /// </summary>
    public static class LCS<T>
        where T : IComparable<T>
    {
        /// <summary>
        /// Get LCS of seq 1 and seq 2.
        /// </summary>
        /// <typeparam name="T">The IComparable template.</typeparam>
        /// <param name="seq1">The sequence 1.</param>
        /// <param name="seq2">The sequence 2.</param>
        /// <returns>The LCS.</returns>
        public static T[] GetLCS(T[] seq1, T[] seq2)
        {
            int[,] matrix = GetLengthMatrix(seq1, seq2);
            if (matrix != null)
            {
                int seq1Length = seq1.Length;
                int seq2Length = seq2.Length;
                int lcsLength = matrix[seq1.Length, seq2.Length];
                int xPos = seq1.Length;
                int yPos = seq2.Length;
                int counter = lcsLength;
                T[] result = new T[lcsLength];
                while (true)
                {
                    if (counter < 0 || xPos < 1 || yPos < 1)
                    {
                        break;
                    }

                    int curLength = matrix[xPos, yPos];
                    int subXLength = matrix[xPos - 1, yPos];
                    int subYLength = matrix[xPos, yPos - 1];
                    int subXYLength = matrix[xPos - 1, yPos - 1];
                    if (curLength == subXYLength + 1 && subXYLength == subXLength && subXYLength == subYLength)
                    {
                        result[--counter] = seq1[xPos - 1];
                        xPos--;
                        yPos--;
                    }
                    else
                    {
                        if (subXLength == subYLength)
                        {
                            xPos--;
                        }
                        else
                        {
                            if (curLength == subXLength)
                            {
                                xPos--;
                            }

                            if (curLength == subYLength)
                            {
                                yPos--;
                            }
                        }
                    }
                }

                return result;
            }

            return null;
        }

        /// <summary>
        /// Get length of LCS.
        /// </summary>
        /// <typeparam name="T">The IComparable template.</typeparam>
        /// <param name="seq1">The sequence 1.</param>
        /// <param name="seq2">The sequence 2.</param>
        /// <returns>The length of LCS.</returns>
        public static int GetLCSLength(T[] seq1, T[] seq2)
        {
            if (seq1 != null || seq2 != null)
            {
                int seq1Size = seq1.Length;
                int seq2Size = seq2.Length;
                if (seq1Size < seq2Size)
                {
                    return GetLCSLength(seq2, seq1);
                }

                int[] lastRow = new int[Math.Min(seq1Size, seq2Size) + 1];
                int lastLength = 0;
                int currLength = 0;
                for (int i = 0; i < seq1Size; i++)
                {
                    lastLength = 0;
                    currLength = 0;
                    for (int j = 0; j < seq2Size; j++)
                    {
                        if (seq1[i].CompareTo(seq2[j]) == 0)
                        {
                            currLength = lastRow[j] + 1;
                            //lastRow[j + 1] = lastLength + 1;
                        }
                        else
                        {
                            currLength = Math.Max(lastLength, lastRow[j + 1]);
                            //lastRow[j + 1] = Math.Max(lastRow[j], lastRow[j + 1]);
                        }

                        lastRow[j] = lastLength;
                        lastLength = currLength;
                    }

                    lastRow[lastRow.Length - 1] = lastLength;
                }

                return lastRow[lastRow.Length - 1];
            }

            return 0;
        }

        /// <summary>
        /// Get length matrix.
        /// </summary>
        /// <typeparam name="T">The IComparable template.</typeparam>
        /// <param name="seq1">The sequence 1.</param>
        /// <param name="seq2">The sequence 2.</param>
        /// <returns>The length matrix.</returns>
        public static int[,] GetLengthMatrix(T[] seq1, T[] seq2)
        {
            if (seq1 != null || seq2 != null)
            {
                int seq1Size = seq1.Length;
                int seq2Size = seq2.Length;
                int[,] matrix = new int[seq1Size + 1, seq2Size + 1];
                for (int i = 0; i < seq1Size; i++)
                {
                    for (int j = 0; j < seq2Size; j++)
                    {
                        if (seq1[i].CompareTo(seq2[j]) == 0)
                        {
                            matrix[i + 1, j + 1] = matrix[i, j] + 1;
                        }
                        else
                        {
                            matrix[i + 1, j + 1] = Math.Max(matrix[i + 1, j], matrix[i, j + 1]);
                        }
                    }
                }

                return matrix;
            }

            return null;
        }        
    }
}
