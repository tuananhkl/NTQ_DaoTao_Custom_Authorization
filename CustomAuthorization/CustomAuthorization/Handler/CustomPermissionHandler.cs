using CustomAuthorization.CustomAuthorization.Requirement;
using CustomAuthorization.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace CustomAuthorization.CustomAuthorization.Handler;

public class CustomPermissionHandler : AuthorizationHandler<CustomPermissionRequirement>
{
    // private readonly AppDbContext _dbContext;
    //
    // public CustomPermissionHandler(AppDbContext dbContext)
    // {
    //     _dbContext = dbContext;
    // }

    private readonly IServiceProvider _serviceProvider;

    public CustomPermissionHandler(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task<Task> HandleRequirementAsync(AuthorizationHandlerContext context, CustomPermissionRequirement requirement)
    {
        // List<string> userPermission = new();
        // var permission = "";
        // var userRoles = await _dbContext.Roles.ToListAsync();
        // foreach (var userRole in userRoles)
        // {
        //     permission = $"{userRole.Controller}.{userRole.Action}";
        //     userPermission.Add(permission);
        // }
        
        
        
        using (var scope = _serviceProvider.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            List<string> userPermission = new();            
            var permission = "";
            var userRoles = await dbContext.Roles.ToListAsync();
            foreach (var userRole in userRoles)
            {
                permission = $"{userRole.Controller}.{userRole.Action}";
                userPermission.Add(permission);
            }
            
            //string[] userPermission = { "Home.Create" };
            var isAllowAccess = userPermission.Contains($"{requirement.Function}.{requirement.Action}");
            if (isAllowAccess)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
            return Task.CompletedTask;
        }
    }
}