using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GroupProject.Scripts;
using MySql.Data.MySqlClient;
using Avalonia;
using Avalonia.Media.Imaging;
using GroupProject.Services;
using GroupProject.Views;
using Avalonia.Media;
using GroupProject.Models;

namespace GroupProject.ViewModels;


public partial class AccountPageViewModel : ViewModelBase
{
    [Flags]
    enum ErrorType
    {
        InvalidUsername,
        InvalidPassword,
        InvalidEmail,

        NoUser
    }
    private readonly IUserService _userService;
    private readonly IValidationService _validationService;

    private UserStatisticData _userStatistics = new UserStatisticData();

    [ObservableProperty]
    private UserDataModel _userData = App.MainWindowViewModel.User;
    [ObservableProperty]
    private string _email = "";
    [ObservableProperty]
    private string _username = "";
    [ObservableProperty]
    private string _password = "";
        
    [ObservableProperty]
    private string _errorMessage = "Invalid Username or Password\n";
    [ObservableProperty]
    private bool _errorMessageIsVisible = false;

    [ObservableProperty]
    private Thickness _emailBorderThickness = new Thickness(0);
    [ObservableProperty]
    private Thickness _usernameBorderThickness = new Thickness(0);
    [ObservableProperty]
    private Thickness _passwordBorderThickness = new Thickness(0);

    public AccountPageViewModel()
    {
        _userService = new UserService();
        _validationService = new ValidationService();
    }
    public override void Initialize()
    {
        base.Initialize();
        
        Username = UserData.Username;
        Email = UserData.Email;
        Password = UserData.Password;
    }


    private bool IsExistingUser(string signInUsername, string signInEmail)
    {
        return _userService.IsExistingUser(signInUsername, signInEmail);
    }
    
    [RelayCommand]
    private void OnSaveClicked()
    {
        String PasswordHash = Hashes.Sha256(Password);

        if (IsValid(Username) && IsValid(Password))
        {
            if (IsValidEmail(Email))
            {
                if (IsExistingUser(Username, Email))
                {
                    _userService.UpdateUser(Username, Email, PasswordHash);
                    App.MainWindowViewModel.CurrentContent = new MainContentPageViewModel();
                }
                else
                {
                    SetError("No user found", ErrorType.NoUser);
                }
            }
        }
        else
        {
            if (!IsValid(Username) || !IsValid(Password))
            {
                SetError("Invalid Username or Password", ErrorType.InvalidUsername | ErrorType.InvalidPassword);
            }

            if (!IsValidEmail(Email))
            {
                SetError("Invalid Email Address", ErrorType.InvalidEmail);
            }
        }
    }

    private bool IsValidEmail(string email)
    {
        return _validationService.IsValidEmail(email);
    }

    private bool IsValid(string str)
    {
        return _validationService.IsValid(str);
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
            case ErrorType.InvalidEmail:
                EmailBorderThickness = new Thickness(1);
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
}