using System;

namespace FarsiLibrary.FX.Utils.Exceptions
{
    public class InvalidPersianDateFormatException : Exception
    {
        public InvalidPersianDateFormatException(string message) : base(message)
        {
        }

        public InvalidPersianDateFormatException()
        {
        }
    }
}
