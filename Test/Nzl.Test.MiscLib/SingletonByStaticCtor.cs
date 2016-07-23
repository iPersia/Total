using System;
using System.Collections.Generic;
using System.Text;

namespace Nzl.Test.MiscLib
{
    public class SingletonByStaticCtor
    {
        /// <summary>
        /// 
        /// </summary>
        private static SingletonByStaticCtor _instance = null;

        /// <summary>
        /// 
        /// </summary>
        static SingletonByStaticCtor()
        {
            _instance = new SingletonByStaticCtor();

            System.Console.WriteLine("SingletonByStaticCtor static .ctor.");
        }

        /// <summary>
        /// 
        /// </summary>
        private SingletonByStaticCtor()
        {
            System.Console.WriteLine("SingletonByStaticCtor .ctor.");
        }

        /// <summary>
        /// 
        /// </summary>
        public static SingletonByStaticCtor Instance
        {
            get
            {
                return _instance;
            }
        }
    }
}
