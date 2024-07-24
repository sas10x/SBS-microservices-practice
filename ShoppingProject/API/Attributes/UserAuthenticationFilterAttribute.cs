using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Core.Interfaces;

namespace API.Attributes
{
    public class UserAuthenticationFilterAttribute : Attribute, IAsyncAuthorizationFilter
    {
        private readonly ILogger _logger;

        public UserAuthenticationFilterAttribute()
        {
            _logger = LoggerFactory.Create(config =>
            {
                config.AddConsole();
            }).CreateLogger<UserAuthenticationFilterAttribute>();
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            IHeaderDictionary headers = context.HttpContext.Request.Headers;

            headers.TryGetValue("Authorization", out StringValues authorizationHeader);

            string? authorization = authorizationHeader.FirstOrDefault();

            if (authorization != null)
            {
                
                try
                {
                    
                    var userApiService = context.HttpContext.RequestServices.GetService(typeof(IAuthService)) as IAuthService;
                    
                    var user = await userApiService.GetUser(authorization);
                   
                    if (user == null)
                    {
                        context.Result = new ObjectResult(new { message = "You need to sign in or sign up before continuing." }) { StatusCode = 401 };
                        _logger.LogWarning("Authentication not valid, invalidating request.");
                        return;
                    }
                }

                catch
                {
                    context.Result = new ObjectResult(new { message = "You need to sign in or sign up before continuing." }) { StatusCode = 401 };
                    _logger.LogWarning("Authentication not valid, invalidating request.");
                    return;
                }
            }

            else
            {
                context.Result = new ObjectResult(new { message = "You need to sign in or sign up before continuing." }) { StatusCode = 401 };
                _logger.LogWarning("Authentication not valid, invalidating request.");
                return;
            }
        }
    }
}