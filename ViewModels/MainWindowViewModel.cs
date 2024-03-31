using System;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using GroupProject.Models;

namespace GroupProject.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private ViewModelBase _currentContent = new MainContentPageViewModel();
    [ObservableProperty]
    private string _currentTheme = "Dark";
    
    [ObservableProperty]
    private UserDataModel _user = new UserDataModel();
    
    public MainWindowViewModel()
    {
        CurrentContent = new MainContentPageViewModel();
    }
    
    public UserDataModel CreateUser(string username, string email, string password)
    {
        if(User.IsCreated())
            Console.WriteLine("Already Existing User");
        
        User = new UserDataModel(username, email, password);
        
        return User;
    }
}