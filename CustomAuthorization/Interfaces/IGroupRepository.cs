using System.Collections;
using CustomAuthorization.Data;

namespace CustomAuthorization.Interfaces;

public interface IGroupRepository
{
    IEnumerable GetAll();
    IOrderedQueryable<Group> GetGroupOrderByName();
}