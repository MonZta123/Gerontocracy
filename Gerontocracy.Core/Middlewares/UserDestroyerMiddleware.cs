using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Gerontocracy.Core.Middlewares
{
    public class UserDestroyerMiddleware
    {
        private readonly RequestDelegate _next;

        public UserDestroyerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext,
            UserManager<Data.Entities.Account.User> userManager,
            SignInManager<Data.Entities.Account.User> signInManager)
        {
            if (!string.IsNullOrEmpty(httpContext.User.Identity.Name))
            {
                var user = await userManager.FindByNameAsync(httpContext.User.Identity.Name);

                if (user.LockoutEnd > DateTimeOffset.Now)
                {
                    await signInManager.SignOutAsync();
                    httpContext.Response.Redirect("/");
                }
            }
            await _next(httpContext);
        }
    }
}
