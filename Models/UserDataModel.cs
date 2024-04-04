namespace GroupProject.Models;

public class UserDataModel
{
    public int UserID;
    public string Username;
    public string Email;
    public string Password;
    public UserStatisticData UserStats;
    public UserSettingsModel UserSettings;
    
    public UserDataModel(string username, string email, string password)
    {
        Username = username;
        Email = email;
        Password = password;
        
        UserStats = new UserStatisticData("", "", "", 0, 0);
        UserSettings = new UserSettingsModel();
    }

    public bool IsCreated()
    {
        return Username != null && Email != null && Password != null;
    }
}