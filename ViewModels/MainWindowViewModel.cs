using System;
using Avalonia.Controls;
using Avalonia.Styling;
using CommunityToolkit.Mvvm.ComponentModel;
using GroupProject.Models;
using GroupProject.Services;

namespace GroupProject.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private ViewModelBase _currentContent = new MainContentPageViewModel();

    [ObservableProperty] 
    private UserDataModel _user;

    [ObservableProperty]
    private string _currentTheme = "Light";
    
    public MainWindowViewModel()
    {
        ChangeContent(new MainContentPageViewModel());
    }

    public void CreateOrUpdateUserData(string username, string email, string password)
    {
        if(User == null)
        {
            // No user exists, create a new one
            User = new UserDataModel(username, email, password);
            User.UserSettings.LoadSettings();
        }
        else
        {
            // User already exists, update the existing user
            User.Username = username;
            User.Email = email;
            User.Password = password;
        }
    }
    
    public void ChangeContent(ViewModelBase newContent)
    {
        if (newContent == null)
        {
            CurrentContent = new FallbackPageViewModel();
            CurrentContent.Initialize();
            
            return;
        }
        
        CurrentContent = newContent;
        
        CurrentContent.Initialize();
    }
}