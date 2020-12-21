#nullable enable
using System;

namespace YamhilliaNET.Exceptions
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