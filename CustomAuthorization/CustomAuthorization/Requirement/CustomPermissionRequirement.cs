using Microsoft.AspNetCore.Authorization;

namespace CustomAuthorization.CustomAuthorization.Requirement;

public class CustomPermissionRequirement : IAuthorizationRequirement
{
    public string Function { get; }
    public string Action { get; }

    public CustomPermissionRequirement(string function, string action)
    {
        Function = function;
        Action = action;
    }
}