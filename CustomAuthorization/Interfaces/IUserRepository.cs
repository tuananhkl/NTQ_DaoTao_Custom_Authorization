using CustomAuthorization.Data;

namespace CustomAuthorization.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAll();

    Task<User> GetById(int? id);
    Task<User> GetByUserName(string userName);

    Task Add(User user);

    Task Update(User user);

    //Task Delete(int id);
    Task Delete(User user);
    Task SafeDelete(User user);
}
