namespace GroupProject.Services;

public interface IUserService
{
    bool IsExistingUser(string username);
    bool IsExistingUser(string username, string email);
    void RegisterUser(string username, string email, string password);
}