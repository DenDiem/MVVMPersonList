using System;

namespace WpfSimpleMVVM.Exeptions
{
    class AgeExeption : ArgumentException
    {
        public int Value { get; }
        public AgeExeption(string message, int val)
            : base(message)
        {
            Value = val;
        }
    }
}
