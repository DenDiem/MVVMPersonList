using System;

namespace WpfSimpleMVVM.Exeptions
{
    class DateExeption : ArgumentException
    {
        public string Value { get; }
        public DateExeption(string message, string val)
            : base(message)
        {
            Value = val;
        }
    }
}
