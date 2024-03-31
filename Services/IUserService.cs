namespace GroupProject.Services;

public interface IUserService
{
    bool IsExistingUser(string username);
    bool IsExistingUser(string username, string email);
    bool LogInUser(string username, string password);
    void RegisterUser(string username, string email, string password);
    void UpdateUser(string username, string email, string password);
}