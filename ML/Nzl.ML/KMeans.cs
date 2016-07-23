namespace Nzl.ML
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public delegate double DistanceDelegate(KMeansPoint pt1, KMeansPoint pt2);

    /// <summary>
    /// K-Means主要有两个最重大的缺陷——都和初始值有关
    /// 1、K是事先给定的，这个K值的选定是非常难以估计的。
    ///    很多时候，事先并不知道给定的数据集应该分成多少个类别才最合适。
    ///    （ISODATA算法通过类的自动合并和分裂，得到较为合理的类型数目K）
    /// 2、K-Means算法需要用初始随机种子点来搞，这个随机种子点太重要，
    ///    不同的随机种子点会有得到完全不同的结果。
    ///    （K-Means++算法可以用来解决这个问题，其可以有效地选择初始点）
    /// </summary>
    public class KMeans : MLBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pt1"></param>
        /// <param name="pt2"></param>
        /// <returns></returns>
        private static double GetMinkowskiDistance(KMeansPoint pt1, KMeansPoint pt2)
        {
            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pt1"></param>
        /// <param name="pt2"></param>
        /// <returns></returns>
        private static double GetEuclideanDistance(KMeansPoint pt1, KMeansPoint pt2)
        {
            double sum = 0;
            for (int i = 0; i < pt1.Dimension; i++)
            {
                sum += (pt1.Values[i] - pt2.Values[i]) * (pt1.Values[i] - pt2.Values[i]);
            }

            return Math.Sqrt(sum);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pt1"></param>
        /// <param name="pt2"></param>
        /// <returns></returns>
        private static double GetCityBlockDistance(KMeansPoint pt1, KMeansPoint pt2)
        {
            double sum = 0;
            for (int i = 0; i < pt1.Dimension; i++)
            {
                sum += Math.Abs(pt1.Values[i] - pt2.Values[i]);
            }

            return sum;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="k"></param>
        /// <param name="points"></param>
        private static KMeansPoint GetNearestPoint(KMeansPoint kmp, KMeansPoint[] points, DistanceDelegate disDelegate)
        {
            KMeansPoint minKmp = null;
            if (kmp != null && points != null && points.Length > 0)
            {
                double minVal = disDelegate(kmp, points[0]);
                minKmp = points[0];
                for (int i = 1; i < points.Length; i++)
                {
                    double distance = disDelegate(kmp, points[i]);
                    if (minVal > distance)
                    {
                        minVal = distance;
                        minKmp = points[i];
                    }
                }
            }

            return minKmp;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="k"></param>
        /// <param name="points"></param>
        private static KMeansPoint[] Initialize(int k, KMeansPoint[] points)
        {
            if (points != null && k > 0 && points.Length >= k)
            {
                KMeansPoint[] seeds = new KMeansPoint[k];
                for (int i = 0; i < (k + 1) / 2; i++)
                {
                    seeds[i] = new KMeansPoint(points[i]);
                    seeds[k - 1 - i] = new KMeansPoint(points[points.Length - 1 - i]);
                }

                seeds[0] = new KMeansPoint(points[1]);
                seeds[1] = new KMeansPoint(points[12]);
                seeds[2] = new KMeansPoint(points[9]);

                return seeds;
            }

            return null;
        }

        private static void UpdateClusterCenter(List<KMeansPoint> cluster)
        {
            if (cluster != null && cluster.Count > 1)
            {
                cluster[0].CopyValues(cluster[1]);
                for (int i = 2; i < cluster.Count; i++)
                {
                    for (int d = 0; d < cluster[0].Dimension; d++)
                    {
                        cluster[0].Values[d] += cluster[i].Values[d];
                    }
                }

                for (int i = 0; i < cluster[0].Dimension; i++)
                {
                    cluster[0].Values[i] /= (double)(cluster.Count - 1);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static KMeansPoint[] GetCenters(int k, KMeansPoint[] points)
        {
            KMeansPoint[] seeds = Initialize(k, points);
            if (seeds != null)
            {
                Dictionary<KMeansPoint, List<KMeansPoint>> dicCluster = new Dictionary<KMeansPoint, List<KMeansPoint>>();
                for (int i = 0; i < k; i++)
                {
                    dicCluster.Add(seeds[i], new List<KMeansPoint>());
                    dicCluster[seeds[i]].Add(seeds[i]);
                }

                bool isUpdated = true;
                while (isUpdated)
                {
                    isUpdated = false;
                    foreach (KMeansPoint kmp in points)
                    {
                        KMeansPoint seed = GetNearestPoint(kmp, seeds, GetEuclideanDistance);
                        List<KMeansPoint> cluster = dicCluster[seed];
                        if (cluster != null)
                        {
                            if (cluster.Contains(kmp) == false)
                            {
                                cluster.Add(kmp);
                                UpdateClusterCenter(cluster);
                                isUpdated = true;
                            }
                        }
                    }

                    for (int i = 0; i < k; i++)
                    {
                        seeds[i].CopyValues(dicCluster[seeds[i]][0]);
                    }
                }
            }

            return seeds;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static Dictionary<KMeansPoint, List<KMeansPoint>> GetClusters(int k, KMeansPoint[] points)
        {
            KMeansPoint[] seeds = Initialize(k, points);
            Dictionary<KMeansPoint, List<KMeansPoint>> dicCluster = null;
            if (seeds != null)
            {
                dicCluster = new Dictionary<KMeansPoint, List<KMeansPoint>>();
                for (int i = 0; i < k; i++)
                {
                    dicCluster.Add(seeds[i], new List<KMeansPoint>());
                    dicCluster[seeds[i]].Add(new KMeansPoint(seeds[i]));
                }
#if DEBUG
                foreach (KMeansPoint kmp in points)
                {
                    System.Console.Write(kmp.Label + "\t");
                    foreach (double val in kmp.Values)
                    {
                        System.Console.Write(val + "\t");
                    }

                    System.Console.WriteLine("");
                }
#endif
                
#if DEBUG
                int loop = 0;
#endif
                bool isUpdated = true;
                while (isUpdated)
                {
                    for (int i = 0; i < k; i++)
                    {
                        KMeansPoint pivot = dicCluster[seeds[i]][0];
                        dicCluster[seeds[i]].Clear();
                        dicCluster[seeds[i]].Add(pivot);
                    }

#if DEBUG
                    foreach (KMeansPoint seed in seeds)
                    {
                        System.Console.WriteLine(seed);
                    }

                    foreach (KMeansPoint kmp in points)
                    {
                        System.Console.Write(kmp.Label + "\t");
                        foreach (KMeansPoint seed in seeds)
                        {
                            System.Console.Write(GetEuclideanDistance(kmp, seed) + "\t");
                        }

                        System.Console.WriteLine("");
                    }
#endif

                    foreach (KMeansPoint kmp in points)
                    {
                        KMeansPoint seed = GetNearestPoint(kmp, seeds, GetEuclideanDistance);
                        List<KMeansPoint> cluster = dicCluster[seed];
                        if (cluster != null)
                        {
                            if (cluster.Contains(kmp) == false)
                            {
                                cluster.Add(kmp);                                
                            }
                        }
                    }

                    foreach (KeyValuePair<KMeansPoint, List<KMeansPoint>> kp in dicCluster)
                    {
                        UpdateClusterCenter(kp.Value);
                    }

#if DEBUG
                    System.Console.WriteLine("Loop - " + loop++);
                    foreach (KeyValuePair<KMeansPoint, List<KMeansPoint>> kp in dicCluster)
                    {
                        System.Console.WriteLine("\tStart Point:" + kp.Key.ToString());
                        System.Console.WriteLine("\tPivot Point:" + kp.Value[0].ToString());
                        System.Console.WriteLine("\tCluster Points");
                        for (int i = 1; i < kp.Value.Count; i++)
                        {
                            System.Console.WriteLine("\t\t" + kp.Value[i].ToString());
                        }
                    }
#endif

                    isUpdated = false;
                    for (int i = 0; i < k; i++)
                    {
                        if (seeds[i] != dicCluster[seeds[i]][0])
                        {
                            seeds[i].CopyValues(dicCluster[seeds[i]][0]);
                            isUpdated = true;
                        }
                    }
                }
            }

            return dicCluster;
        }

        /// <summary>
        /// 
        /// </summary>
        public override string Name
        {
            get
            {
                return base.Name + " - K-Means";
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class KMeansPoint
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly int _dimension;

        /// <summary>
        /// 
        /// </summary>
        private readonly double[] _values;

        /// <summary>
        /// 
        /// </summary>
        private string _label;

        /// <summary>
        /// 
        /// </summary>
        public int Dimension
        {
            get
            {
                return this._dimension;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public double[] Values
        {
            get
            {
                return this._values;
            }
        }

        public string Label
        {
            get
            {
                return this._label;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        KMeansPoint()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dimension"></param>
        public KMeansPoint(int dimension)
        {
            if (dimension > 0)
            {
                this._label = "null";
                this._dimension = dimension;
                this._values = new double[this._dimension];
            }
            else
            {
                throw new Exception("KMeansPoint.ctor error, dimensions should be greater than 0!");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dimension"></param>
        public KMeansPoint(string label, int dimension)
        {
            if (dimension > 0)
            {
                this._label = label;
                this._dimension = dimension;
                this._values = new double[this._dimension];
            }
            else
            {
                throw new Exception("KMeansPoint.ctor error, dimensions should be greater than 0!");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dimension"></param>
        public KMeansPoint(string label, int dimension, double[] values)
        {
            if (dimension > 0)
            {
                this._label = label;
                this._dimension = dimension;
                this._values = new double[this._dimension];
                for (int i = 0; i < this._dimension; i++)
                {
                    this._values[i] = values[i];
                }
            }
            else
            {
                throw new Exception("KMeansPoint.ctor error, dimensions should be greater than 0!");
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="dimension"></param>
        public KMeansPoint(KMeansPoint kmp)
        {
            if (kmp != null && kmp._dimension > 0)
            {
                this._dimension = kmp._dimension;
                this._values = new double[this._dimension];
                for (int i = 0; i < this._dimension; i++)
                {
                    this._values[i] = kmp._values[i];
                }
            }
            else
            {
                throw new Exception("KMeansPoint Copy.ctor error!");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="kmp"></param>
        public void CopyValues(KMeansPoint kmp)
        {
            if (kmp != null && kmp._dimension == this._dimension)
            {
                for (int i = 0; i < this._dimension; i++)
                {
                    this._values[i] = kmp._values[i];
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pt1"></param>
        /// <param name="pt2"></param>
        /// <returns></returns>
        public static bool operator ==(KMeansPoint pt1, KMeansPoint pt2)
        {
            if ((pt1 as object) != null && (pt2 as object) != null)
            {
                if (pt1._dimension != pt2._dimension)
                {
                    return false;
                }

                for (int i = 0; i < pt1._dimension; i++)
                {
                    if (pt1._values[i] != pt2._values[i])
                    {
                        return false;
                    }
                }

                return true;
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pt1"></param>
        /// <param name="pt2"></param>
        /// <returns></returns>
        public static bool operator !=(KMeansPoint pt1, KMeansPoint pt2)
        {
            return !(pt1 == pt2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string msg = this._label + "\t(";
            for (int i = 0; i < this._dimension; i++)
            {
                msg += string.Format("{0:0.00}", this._values[i]).PadLeft(8) + ", ";
            }

            return msg.Substring(0, msg.LastIndexOf(", ")) + ")";
        }
    }
}
