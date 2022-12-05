namespace CustomAuthorization.Interfaces;

public interface IAuthManager
{
    Task<bool> ValidateUser(string userName, string password);
}