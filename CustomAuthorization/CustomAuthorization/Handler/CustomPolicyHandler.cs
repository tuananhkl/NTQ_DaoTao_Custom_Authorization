using CustomAuthorization.Common.Constants;
using CustomAuthorization.CustomAuthorization.Requirement;
using Microsoft.AspNetCore.Authorization;

namespace CustomAuthorization.CustomAuthorization.Handler;

public class CustomPolicyHandler : AuthorizationHandler<CustomPolicyRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CustomPolicyRequirement requirement)
    {
        if (context.Resource != null)
        {
            var filterContext = (DefaultHttpContext)context.Resource;
            var httpcontext = filterContext?.HttpContext;
            var userName = httpcontext?.Session.GetString(UserConfigurations.USER_NAME);
            if (!string.IsNullOrEmpty(userName))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }
        }

        context.Fail();
        return Task.CompletedTask;
    }
}