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

namespace GroupProject.ViewModels
{
    [Flags]
    enum ErrorType
    {
        InvalidUsername,
        InvalidPassword,
        InvalidEmail,
        ExistingUser
    }

    public partial class RegisterPageViewModel : ViewModelBase
    {
        private readonly IUserService _userService;
        private readonly IValidationService _validationService;

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
        
        public RegisterPageViewModel()
        {
            _userService = new UserService();
            _validationService = new ValidationService();
        }
        public RegisterPageViewModel(IUserService userService, IValidationService validationService)
        {
            _userService = userService;
            _validationService = validationService;
        }

        private bool IsValidEmail(string email)
        {
            return _validationService.IsValidEmail(email);
        }

        private bool IsValid(string str)
        {
            return _validationService.IsValid(str);
        }
        
        private bool IsExistingUser(string signInUsername, string signInEmail)
        {
            return _userService.IsExistingUser(signInUsername, signInEmail);
        }

        [RelayCommand]
        private void OnRegisterClicked()
        {
            String PasswordHash = Hashes.Sha256(Password);

            if (IsValid(Username) && IsValid(Password))
            {
                if (IsValidEmail(Email))
                {
                    if (!IsExistingUser(Username, Email))
                    {
                        _userService.RegisterUser(Username, Email, PasswordHash);
                        
                        App.MainWindowViewModel.CurrentContent = new LoginPageViewModel();
                    }
                    else
                    {
                        SetError("There already is a user with that username or email", ErrorType.ExistingUser);
                    }
                }
                else
                {
                    SetError("Invalid Email Address", ErrorType.InvalidEmail);
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
                case ErrorType.ExistingUser:
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
        private void OnSwitchToLoginClicked()
        {
            App.MainWindowViewModel.CurrentContent = new LoginPageViewModel();
        }
    }
}