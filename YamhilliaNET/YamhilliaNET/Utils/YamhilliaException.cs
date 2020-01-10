using System;
using System.Runtime.Serialization;

namespace YamhilliaNET.Utils
{
    public class YamhilliaException : Exception
    {
        public int Status { get; private set;}

        public YamhilliaException(int status)
        {
            Status = status;
        }

        public YamhilliaException(string message, int status) : base(message)
        {
            Status = status;
        }

        public YamhilliaException(string message, Exception innerException, int status) : base(message, innerException)
        {
            Status = status;
        }

        protected YamhilliaException(SerializationInfo info, StreamingContext context, int status) : base(info, context)
        {
            Status = status;
        }
    }

}