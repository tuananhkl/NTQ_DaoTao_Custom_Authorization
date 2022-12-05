using System.Collections;
using CustomAuthorization.Data;
using CustomAuthorization.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CustomAuthorization.Implementations;

public class GroupRepository : IGroupRepository
{
    private readonly AppDbContext _dbContext;

    public GroupRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable GetAll()
    {
        var groups = _dbContext.Groups;

        return groups;
    }

    public IOrderedQueryable<Group> GetGroupOrderByName()
    {
        var groups = from g in _dbContext.Groups
            orderby g.GroupName
            select g;

        return groups;
    }
}