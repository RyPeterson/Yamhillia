using System;
using System.Runtime.Serialization;

namespace YamhilliaNET.Utils
{
    public class YamhilliaExceptions
    {
        public static void NotFound(string message)
        {
            throw new YamhilliaException(message, 404);
        }

        public static void BadRequest(string message)
        {
            throw new YamhilliaException(message, 400);
        }
    }
}