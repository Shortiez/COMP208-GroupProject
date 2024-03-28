using System;
using System.Text.RegularExpressions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GroupProject.Scripts;
using MySql.Data.MySqlClient;
using Avalonia;
using GroupProject.Services;
using GroupProject.Views;

namespace GroupProject.ViewModels
{
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
        private string _errorMessage = "";
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
            Password = Hashes.Sha256(Password);

            if (IsValid(Username) && IsValid(Password))
            {
                if (IsValidEmail(Email))
                {
                    if (!IsExistingUser(Username, Email))
                    {
                        _userService.RegisterUser(Username, Email, Password);
                        
                        App.MainWindow.CurrentContent = new MainContentPageViewModel();
                    }
                    else
                    {
                        SetError("There already is a user with the same credentials");
                    }
                }
                else
                {
                    SetError("A valid email must be used");
                }
            }
            else
            {
                if (!IsValid(Username))
                {
                    SetError("Username Must be at least 6 characters long");
                }

                if (!IsValid(Password))
                {
                    SetError("Password Must be at least 6 characters long");
                }

                if (!IsValidEmail(Email))
                {
                    SetError("A valid email must be used");
                }
            }
        }

        private void SetError(string message)
        {
            ErrorMessage += message + "\n";
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
            App.MainWindow.CurrentContent = new LoginPageViewModel();
        }
    }
}