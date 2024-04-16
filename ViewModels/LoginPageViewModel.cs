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
    [Flags]
    enum ErrorType
    {
        InvalidUsername,
        InvalidPassword,
        NoUser,
    }
    
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
    
    private bool LogInUser(string signInUsername, string signInPassword)
    {
        return _userService.LogInUser(signInUsername, signInPassword);
    }
    
    [RelayCommand]
    private void OnLoginClicked()
    {
        var mainWindowViewModel = App.MainWindowViewModel;
        if (IsValid(SignInUsername) && IsValid(SignInPassword))
        {
            String signInPasswordHash = Hashes.Sha256(SignInPassword);
            if (LogInUser(SignInUsername, signInPasswordHash))
            {
                if(!App.MainWindowViewModel.User.IsCreated())
                    App.MainWindowViewModel.LoadUserData(_signInUsername, "", signInPasswordHash);
                
                mainWindowViewModel.CurrentContent = new MainContentPageViewModel();
                mainWindowViewModel.CurrentContent.Initialize();
            }
            else
            {
                SetError("No User Found", ErrorType.NoUser);
            }
        } 
        else
        {
            SetError("Username or Password Invalid", ErrorType.InvalidUsername | ErrorType.InvalidPassword);
        }
    }

    private void SetError(string message, ErrorType errorType)
        {
            ClearError();
            
            switch (errorType)
            {
                case ErrorType.InvalidUsername:
                    UsernameBorderThickness = new Thickness(1);
                    break;
                case ErrorType.InvalidPassword:
                    PasswordBorderThickness = new Thickness(1);
                    break;
                case ErrorType.NoUser:
                    
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(errorType), errorType, null);
            }

            ErrorMessage = message;
            ErrorMessageIsVisible = true;
        }
        
        private void ClearError()
        {
            ErrorMessage = "";
            ErrorMessageIsVisible = false;
        }
    
    [RelayCommand]
    private void OnLoginAsGuestClicked()
    {
        var mainWindowViewModel = App.MainWindowViewModel;
        mainWindowViewModel?.LoadUserData("Guest", "", "");
        
        if (mainWindowViewModel != null) mainWindowViewModel.CurrentContent = new MainContentPageViewModel();
    }
    
    [RelayCommand]
    private void OnRegisterClicked()
    {
        App.MainWindowViewModel.CurrentContent = new RegisterPageViewModel();
    }
}