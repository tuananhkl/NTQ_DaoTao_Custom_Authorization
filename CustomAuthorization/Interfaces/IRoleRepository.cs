using CustomAuthorization.Data;

namespace CustomAuthorization.Interfaces;

public interface IRoleRepository
{
    Task<IEnumerable<Role>> GetAll();
}