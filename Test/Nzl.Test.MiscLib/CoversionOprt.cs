using System;
using System.Collections.Generic;
using System.Text;

namespace Nzl.Test.MiscLib
{
    public sealed class Rational
    {
        // Constructs a Rational from an Int32
        public Rational(Int32 num) { }

        // Constructs a Rational from a Single
        public Rational(Single num) { }

        // Convert a Rational to an Int32
        public Int32 ToInt32() { return Int32.MinValue; }

        // Convert a Rational to a Single
        public Single ToSingle() { return Single.MinValue; }

        // Implicitly constructs and returns a Rational from an Int32
        public static implicit operator Rational(Int32 num)
        {
            return new Rational(num);
        }

        // Implicitly constructs and returns a Rational from a Single
        public static implicit operator Rational(Single num)
        {
            return new Rational(num);
        }

        // Explicitly returns an Int32 from a Rational
        public static explicit operator Int32(Rational r)
        {
            return r.ToInt32();
        }

        //Explicitly returns a Single from a Rational
        public static explicit operator Single(Rational r)
        {
            return r.ToSingle();
        }

        
    }
}