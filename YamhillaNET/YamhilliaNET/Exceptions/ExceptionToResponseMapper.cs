using System;
using Microsoft.AspNetCore.Mvc;

namespace YamhillaNET.Exceptions
{
    public static class ExceptionToResponseMapper
    {
        public static IActionResult MapException(this Controller controller, Exception exception)
        {
            if (exception.GetType() == typeof(YamhilliaStatusException) || exception.GetType().IsSubclassOf(typeof(YamhilliaStatusException)))
            {
                YamhilliaStatusException statusException = (YamhilliaStatusException) exception;
                var messageObject = new {message = statusException.Message};
                switch (statusException.Status)
                {
                    case 400:
                        return controller.BadRequest(messageObject);
                    case 404:
                        return controller.NotFound(messageObject);
                    case 403:
                        return controller.Forbid();
                }

            }

            throw exception;
        }
    }
}