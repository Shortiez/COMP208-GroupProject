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

    public void CreateUserData()
    {
        if(User != null)
        {
            Console.WriteLine("User data already exists");
            return;
        }
        
        User = new UserDataModel("", "", "");
        User.UserSettings.LoadSettings();
    }
    
    public void LoadUserData(string username, string email, string password)
    {
        if(User == null)
        {
            Console.WriteLine("No user exists");
            return;
        }
        
        User = new UserDataModel(username, email, password);
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