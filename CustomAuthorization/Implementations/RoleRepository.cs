using CustomAuthorization.Data;
using CustomAuthorization.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CustomAuthorization.Implementations;

public class RoleRepository : IRoleRepository
{
    private readonly AppDbContext _dbContext;

    public RoleRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Role>> GetAll()
    {
        var roles = await _dbContext.Roles.ToListAsync();
        return roles;
    }
}