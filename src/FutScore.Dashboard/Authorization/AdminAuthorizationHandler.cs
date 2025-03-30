using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace FutScore.Dashboard.Authorization
{
    public class AdminAuthorizationHandler : AuthorizationHandler<AdminRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AdminAuthorizationHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            AdminRequirement requirement)
        {
            var user = context.User;

            if (!user.Identity.IsAuthenticated)
            {
                return Task.CompletedTask;
            }

            if (user.IsInRole("Admin"))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }

    public class AdminRequirement : IAuthorizationRequirement
    {
    }
} 