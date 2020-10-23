using System;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
namespace apiCell.Authorization
{
    public class CoolTestHandler : AuthorizationHandler<CoolRequirement>
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        
        public CoolTestHandler(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CoolRequirement requirement)
        {
            var httpContext = httpContextAccessor.HttpContext;
            var cookies = httpContext.Request.Cookies;
            if (cookies.TryGetValue("amIcool", out var result) && result == "true"){
                context.Succeed(requirement);
            };

            return Task.CompletedTask;
        }
    }
}
