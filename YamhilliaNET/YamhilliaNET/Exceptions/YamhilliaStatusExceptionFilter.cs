using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace YamhilliaNET.Exceptions
{
    public class YamhilliaStatusExceptionFilter : IActionFilter, IOrderedFilter
    {
        public int Order { get; } = int.MaxValue - 10;
        
        public void OnActionExecuted(ActionExecutedContext context)
        {
            var exception = context.Exception;
            if (exception != null && (exception.GetType() == typeof(YamhilliaStatusException) ||
                                      exception.GetType().IsSubclassOf(typeof(YamhilliaStatusException))))
            {
                YamhilliaStatusException statusException = (YamhilliaStatusException) exception;
                var messageObject = new {message = statusException.Message};
                context.Result = new ObjectResult(statusException.Message)
                {
                    StatusCode = statusException.Status
                };
                context.ExceptionHandled = true;
            }

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            // Carry on
        }

    }
}