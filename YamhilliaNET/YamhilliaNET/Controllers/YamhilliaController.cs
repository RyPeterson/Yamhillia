using Microsoft.AspNetCore.Mvc;
using YamhilliaNET.Exceptions;

namespace YamhilliaNET.Controllers
{
    public class YamhilliaController : Controller
    {
        protected long GetLoggedInUserId()
        {
            if (!long.TryParse(HttpContext.User.Identity.Name, out var userId))
            {
                throw new YamhilliaForbiddenError("Must be logged in to access this endpoint.");
            }

            return userId;
        }
    }
}