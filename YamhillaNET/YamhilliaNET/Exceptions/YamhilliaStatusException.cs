using System;

namespace YamhillaNET.Exceptions
{
    public class YamhilliaStatusException: YamhilliaException
    {
        private readonly int _status;
        protected YamhilliaStatusException(string? message, int status) : base(message)
        {
            _status = status;
        }

        public YamhilliaStatusException(string? message, Exception? innerException, int status) : base(message, innerException)
        {
            _status = status;
        }
    }

    public class YamhilliaNotFoundException: YamhilliaStatusException
    {
        public YamhilliaNotFoundException(string? message) : base(message, 404)
        {
            
        }
    }
    
    public class YamhilliaBadRequestException: YamhilliaStatusException
    {
        public YamhilliaBadRequestException(string? message) : base(message,400)
        {
            
        }
    }

    public class YamhilliaForbiddenException : YamhilliaStatusException
    {
        public YamhilliaForbiddenException(string? message) : base(message, 403)
        {
            
        }
    }

    public class YamhilliaServerErrorException : YamhilliaStatusException
    {
        protected YamhilliaServerErrorException(string? message) : base(message, 500)
        {
        }

        public YamhilliaServerErrorException(string? message, Exception? innerException) : base(message, innerException, 500)
        {
        }
    }
}