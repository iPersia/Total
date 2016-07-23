using System;
using System.Collections;
using System.Collections.Generic;
using Nzl.Core.Interface;
using Nzl.ML;

namespace Nzl.Test.ML
{
    internal class Test_KMeans : ITest
    {
        
        /// <summary>
        /// 
        /// </summary>
        public void Test1()
        {
            int count = 6;
            int dim = 2;
            KMeansPoint[] points = new KMeansPoint[count];
            for (int i = 0; i < count; i++)
            {
                points[i] = new KMeansPoint(2);

                for (int j = 0; j < 2; j++)
                {
                    points[i].Values[0] = i * 1 + j * 1;
                    points[i].Values[1] = i * 1 - j * 1;
                }

                System.Console.WriteLine("Point " + (i + 1) + ": \t" + points[i].ToString());
            }

            {
                KMeansPoint[] centers = KMeans.GetCenters(dim, points);

                for (int i = 0; i < dim; i++)
                {
                    System.Console.WriteLine("Center " + (i + 1) + ": \t" + centers[i].ToString());
                }
            }

            {
                Dictionary<KMeansPoint, List<KMeansPoint>> dic = KMeans.GetClusters(dim, points);
                foreach (KeyValuePair<KMeansPoint, List<KMeansPoint>> kp in dic)
                {
                    System.Console.WriteLine("Start Point:" + kp.Key.ToString());
                    System.Console.WriteLine("Pivot Point:" + kp.Value[0].ToString());
                    System.Console.WriteLine("Cluster Points");
                    for (int i = 1; i < kp.Value.Count; i++ )
                    {
                        System.Console.WriteLine("\t" + kp.Value[i].ToString());
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Test()
        {
            string[] nations = new string[] 
                             { 
                                 "中国",
                                 "日本",
                                 "韩国",
                                 "伊朗",
                                 "沙特",
                                 "伊拉克",
                                 "卡塔尔",
                                 "阿联酋",
                                 "乌兹", //别克斯坦
                                 "泰国",
                                 "越南",
                                 "阿曼",
                                 "巴林",
                                 "朝鲜",
                                 "印尼"
                             };

            double[] values = new double[]
                             {
                                1, 1, 0.5,
                                0.3, 0, 0.19,
                                0, 0.15, 0.13,
                                0.24, 0.76, 0.25, 
                                0.3, 0.76, 0.06, 
                                1, 1, 0, 
                                1, 0.76, 0.5,
                                1, 0.76, 0.5, 
                                0.7, 0.76, 0.25, 
                                1, 1, 0.5, 
                                1, 1, 0.25, 
                                1, 1, 0.5, 
                                0.7, 0.76, 0.5,
                                0.7, 0.68, 1, 
                                1, 1, 0.5
                             };

            KMeansPoint[] points = new KMeansPoint[15];
            for (int i = 0; i < 15; i++)
            {
                points[i] = new KMeansPoint(nations[i], 3, new double[] { values[i * 3], values[i * 3 + 1], values[i * 3 + 2] });
            }

            {
                Dictionary<KMeansPoint, List<KMeansPoint>> dic = KMeans.GetClusters(3, points);
                foreach (KeyValuePair<KMeansPoint, List<KMeansPoint>> kp in dic)
                {
                    System.Console.WriteLine("Start Point:" + kp.Key.ToString());
                    System.Console.WriteLine("Pivot Point:" + kp.Value[0].ToString());
                    System.Console.WriteLine("Cluster Points");
                    for (int i = 1; i < kp.Value.Count; i++)
                    {
                        System.Console.WriteLine("\t" + kp.Value[i].ToString());
                    }
                }
            }
        }
    }
}
