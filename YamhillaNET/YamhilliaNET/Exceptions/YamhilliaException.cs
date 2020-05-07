using System;

namespace YamhillaNET.Exceptions
{
    public class YamhilliaException: Exception
    {
        public YamhilliaException(string? message) : base(message)
        {
        }

        public YamhilliaException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}