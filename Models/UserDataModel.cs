using System;

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
        Console.WriteLine("Username: " + Username);
        Email = email;
        Password = password;
        
        UserStats = new UserStatisticData(Username, "", "");
        UserSettings = new UserSettingsModel();
    }
}