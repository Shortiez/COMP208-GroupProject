using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Avalonia;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GroupProject.Models;
using GroupProject.Scripts;
using GroupProject.Services;

namespace GroupProject.ViewModels;

public partial class LoginPageViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _signInUsername = "";
    [ObservableProperty]
    private string _signInPassword = "";
    [ObservableProperty]
    private string _errorMessage = "";
    [ObservableProperty]
    private bool _errorMessageIsVisible = false;
    [ObservableProperty] 
    private Thickness _usernameBorderThickness;
    [ObservableProperty] 
    private Thickness _passwordBorderThickness;
    
    private IUserService _userService;
    private IValidationService _validationService;
    
    public LoginPageViewModel()
    {
        _userService = new UserService();
        _validationService = new ValidationService();
    }
    public LoginPageViewModel(IUserService userService, IValidationService validationService)
    {
        _userService = userService;
        _validationService = validationService;
    }

    private bool IsValid(string str)
    {
        return _validationService.IsValid(str);
    }
    
    private bool IsValidEmail(string email)
    {
        return _validationService.IsValidEmail(email);
    }
    
    private bool IsExistingUser(string signInUsername)
    {
        return _userService.IsExistingUser(signInUsername);
    }
    private bool IsExistingUser(string signInUsername, string signInEmail)
    {
        return _userService.IsExistingUser(signInUsername, signInEmail);
    }
    
    [RelayCommand]
    private void OnLoginClicked()
    {
        if (IsValid(SignInUsername) && IsValidEmail(SignInPassword))
        {
            if (IsExistingUser(SignInUsername))
            {
                if(!App.MainWindowViewModel.User.IsCreated())
                    App.MainWindowViewModel.CreateUser(_signInUsername, "", _signInPassword);
            }
            else
            {
                
            }
        }
    }
    
    [RelayCommand]
    private void OnLoginAsGuestClicked()
    {
        var mainWindowViewModel = App.MainWindowViewModel;
        mainWindowViewModel.CreateUser("Guest", "", "");
        mainWindowViewModel.CurrentContent = new MainContentPageViewModel();
    }
    
    [RelayCommand]
    private void OnRegisterClicked()
    {
        App.MainWindowViewModel.CurrentContent = new RegisterPageViewModel();
    }
}