using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Nzl.Test.MiscLib;
using Nzl.Util;

namespace Nzl.Test.Misc
{
    class Program
    {
        static void Main(string[] args)
        {
            BaseC bc = new BaseC();

            //TestSingletonByStaticCtor();

            //const Int32 iterations = 1000 * 1000 * 1000;
            //PerfTest1(iterations);
            //PerfTest2(iterations);

            {
                // 1. Same as: M(9, "A", default(DateTime), new Guid());
                M();

                // 2. Same as: M(8, "X", default(DateTime), new Guid());
                M(8, "X");

                // 3. Same as: M(5, "A", DateTime.Now, Guid.NewGuid());
                M(5, guid: Guid.NewGuid(), dt: DateTime.Now);

                int s_n = 0;
                // 4. Same as: M(0, "1", default(DateTime), new Guid());
                M(s_n++, s_n++.ToString());
                // 5. Same as: String t1 = "2"; Int32 t2 = 3;
                // M(t2, t1, default(DateTime), new Guid());
                M(s: (s_n++).ToString(), x: s_n++);
            }

            System.Console.ReadLine();
        }

        private static void M(Int32 x = 9, String s = "A",  DateTime dt = default(DateTime), Guid guid = new Guid()) 
        {
            Console.WriteLine("x={0}, s={1}, dt={2}, guid={3}", x, s, dt, guid);
        }

        /// <summary>
        /// 
        /// </summary>
        static void TestSingletonByStaticCtor()
        {
            int threadCount = 1000;
            for (int i = 0; i < threadCount; i++)
            {
                System.Threading.Thread tr = new System.Threading.Thread(NewSingletonByStaticCtor);
                tr.Start(threadCount - i / 2);
            }
        }

        static void NewSingletonByStaticCtor(object state)
        {
            System.Threading.Thread.Sleep((int)state);
            string hashCode = SingletonByStaticCtor.Instance.GetHashCode().ToString();
            Nzl.Util.QueryPerformance qpf = new Util.QueryPerformance();
            qpf.Start();
            System.Console.WriteLine(qpf.BeginTime + "\t" + hashCode);
            qpf.Stop();
        }

        // When this method is JIT compiled, the type constructors for
        // the BeforeFieldInit and Precise classes HAVE NOT executed yet
        // and therefore, calls to these constructors are embedded in
        // this method's code, making it run slower
        private static void PerfTest1(Int32 iterations)
        {
            Stopwatch sw = Stopwatch.StartNew();
            for (Int32 x = 0; x < iterations; x++)
            {
                // The JIT compiler hoists the code to call BeforeFieldInit's
                // type constructor so that it executes before the loop starts
                BeforeFieldInit.s_x = 1;
            }
            Console.WriteLine("PerfTest1: {0} BeforeFieldInit", sw.Elapsed);
            sw = Stopwatch.StartNew();
            for (Int32 x = 0; x < iterations; x++)
            {
                // The JIT compiler emits the code to call Precise's
                // type constructor here so that it checks whether it
                // has to call the constructor with each loop iteration
                Precise.s_x = 1;
            }
            Console.WriteLine("PerfTest1: {0} Precise", sw.Elapsed);
        }
        // When this method is JIT compiled, the type constructors for
        // the BeforeFieldInit and Precise classes HAVE executed
        // and therefore, calls to these constructors are NOT embedded
        // in this method's code, making it run faster
        private static void PerfTest2(Int32 iterations)
        {
            Stopwatch sw = Stopwatch.StartNew();
            for (Int32 x = 0; x < iterations; x++)
            {
                BeforeFieldInit.s_x = 1;
            }
            Console.WriteLine("PerfTest2: {0} BeforeFieldInit", sw.Elapsed);
            sw = Stopwatch.StartNew();
            for (Int32 x = 0; x < iterations; x++)
            {
                Precise.s_x = 1;
            }
            Console.WriteLine("PerfTest2: {0} Precise", sw.Elapsed);
        }
    }

    public class InheritedC : BaseC
    {
        public void Print()
        {
            System.Console.WriteLine(this.protectedInternalX);
            System.Console.WriteLine(this.protectedX);

            NestedProtInterC bnc = new NestedProtInterC();
        }
    }

    // Since this class doesn't explicitly define a type constructor,
    // C# marks the type definition with BeforeFieldInit in the metadata.
    internal sealed class BeforeFieldInit
    {
        public static Int32 s_x = 123;
    }

    // Since this class does explicitly define a type constructor,
    // C# doesn't mark the type definition with BeforeFieldInit in the metadata.
    internal sealed class Precise
    {
        public static Int32 s_x;
        static Precise() { s_x = 123; }
    }
}