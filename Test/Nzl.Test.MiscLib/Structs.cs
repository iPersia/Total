using System;
using System.Collections.Generic;
using System.Text;

namespace Nzl.Test.MiscLib
{
    internal struct SomeValType 
    {
        // You cannot do inline instance field initialization in a value type
        //private Int32 m_x = 5;

        private Int32 m_x, m_y;

        //C# doesn’t allow a value type to define a parameterless constructor.
        //public SomeValType() { }

        // C# allows value types to have constructors that take parameters.
        public SomeValType(Int32 x)
        {
            m_x = x;
            // Notice that m_y is not initialized here.
            m_y = 0;
        }

        //Tricky
        public SomeValType(Int32 x, Int32 y)
        {
            // Looks strange but compiles fine and initializes all fields to 0/null           
            this = new SomeValType();
            m_x = x; // Overwrite m_x’s 0 with x
            // Notice that m_y was initialized to 0.
        }

        static SomeValType()
        {
            System.Console.WriteLine("SomeValType static .ctor.");
        }

        public static SomeValType operator+(SomeValType svt1, SomeValType svt2)
        {
            SomeValType svt = new SomeValType();
            svt.m_x = svt1.m_x + svt2.m_x;
            svt.m_y = svt1.m_y + svt2.m_y;
            return svt;
        }
    }
}
