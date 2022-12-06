using Microsoft.AspNetCore.Authorization;

namespace CustomAuthorization.CustomAuthorization.Requirement;

public class CustomPolicyRequirement : IAuthorizationRequirement
{
    public CustomPolicyRequirement() { }
}