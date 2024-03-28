namespace GroupProject.Models;

public struct UserDataModel
{
    public int UserID;
    public string Username;
    public string Email;
    public string Password;
    
    public UserDataModel(string username, string email, string password)
    {
        Username = username;
        Email = email;
        Password = password;
    }

    public bool IsCreated()
    {
        return Username != null && Email != null && Password != null;
    }
}