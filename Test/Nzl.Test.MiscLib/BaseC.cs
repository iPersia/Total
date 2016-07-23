namespace Nzl.Test.MiscLib
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Use to test the Member Accessiblity
    /// 
    /// Private                     private                 
    /// Family                      protected               
    /// Family and Assembly         (not supported)
    /// Assembly                    internal
    /// Family or Assembly          protected internal
    /// Public                      public
    /// 
    /// </summary>
    public class BaseC
    {
        /// <summary>
        /// 
        /// </summary>
        private int unInitializeInt;

        /// <summary>
        /// 
        /// </summary>
        private string unInitializeString;

        /// <summary>
        /// 
        /// </summary>
        private InternalC unInitializeInternalC;

        /// <summary>
        /// Accessible to the defining type and the nested types.
        /// </summary>
        private int privateX = 4;

        /// <summary>
        /// Accessible to the defining type, the derived types and the nested types.
        /// </summary>
        protected int protectedX = 3;

        /// <summary>
        /// Accessible to all methods in the defining assebly.
        /// </summary>
        internal int internalX = 2;

        /// <summary>
        /// Accessible to the defining assembly and the derived types (regardless of assembly).
        /// </summary>
        protected internal int protectedInternalX = 1;

        /// <summary>
        /// Accessible to all methods in any assembly.
        /// </summary>
        public int publicX = 0;

        /// <summary>
        /// Nested class.
        /// </summary>
        protected class NestedProtInterC
        {
            public void Test()
            {
                BaseC bc = new BaseC();
                bc.protectedInternalX = 65;
                bc.internalX = 65;
                bc.protectedX = 65;
                bc.privateX = 65;
            }
        }

        static BaseC()
        {
            System.Console.WriteLine("BaseC static .ctor.");
        }
    }

    /// <summary>
    /// internal -> Assembly
    /// </summary>
    internal class InternalC
    {
        public void Test()
        {
            BaseC c = new BaseC();
            c.internalX = 5;
            c.protectedInternalX = 5;
        }
    }

    /// <summary>
    /// No default ctor.
    /// </summary>
    public static class StaticC
    {
    }

    /// <summary>
    /// Has default ctor.
    /// </summary>
    public sealed class SealedC
    {
    }

    /// <summary>
    /// Has default ctor.
    /// </summary>
    public abstract class AbstractC
    {
    }
}