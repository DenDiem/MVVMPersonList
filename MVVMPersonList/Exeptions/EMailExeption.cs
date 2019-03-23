using System;

namespace WpfSimpleMVVM.Exeptions
{
    class EMailExeption : ArgumentException
    {
        public EMailExeption(string message)
            : base(message)
        {

        }
    }
}
