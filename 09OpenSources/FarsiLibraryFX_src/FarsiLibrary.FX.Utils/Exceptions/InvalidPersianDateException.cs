using System;

namespace FarsiLibrary.FX.Utils.Exceptions
{
    public class InvalidPersianDateException : Exception
    {
        public InvalidPersianDateException() 
        {
        }

        public InvalidPersianDateException(string message) : base(message)
        {
        }
    }
}
