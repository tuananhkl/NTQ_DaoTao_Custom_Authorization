using Microsoft.AspNetCore.Authorization;

namespace CustomAuthorization.CustomAuthorization;

public class CustomAuthorizeAttribute : AuthorizeAttribute
{
    public CustomAuthorizeAttribute(string permission)
    {
        Policy = permission;
    }
}