using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GroupProject.Scripts;
using MySql.Data.MySqlClient;
using Avalonia;
using Avalonia.Controls;
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
    private readonly StatisticsService _statisticsService;

    public AccountPageViewModel()
    {
        _userService = new UserService();
        _validationService = new ValidationService();
        _statisticsService = new StatisticsService();

        _userStatisticData = UserData.UserStats;
    }

    public override void Initialize()
    {
        base.Initialize();

        Username = UserData.Username;
        Email = UserData.Email;
        Password = UserData.Password;
        
        _userStatisticData.UpdateExistingRecord(15, 73, "Binary Arithmetic", "Binary Addition");
        
        LoadUserStatistics();
    }

    #region Account Settings

    [ObservableProperty] private UserDataModel _userData = App.MainWindowViewModel.User;
    [ObservableProperty] private string _email = "";
    [ObservableProperty] private string _username = "";
    [ObservableProperty] private string _password = "";

    [ObservableProperty] private string _errorMessage = "Invalid Username or Password\n";
    [ObservableProperty] private bool _errorMessageIsVisible = false;

    [ObservableProperty] private Thickness _emailBorderThickness = new Thickness(0);
    [ObservableProperty] private Thickness _usernameBorderThickness = new Thickness(0);
    [ObservableProperty] private Thickness _passwordBorderThickness = new Thickness(0);

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

    #endregion

    #region Account Statistics

    private UserStatisticData _userStatisticData;
    
    private DatabaseConnection _connectionDb = new DatabaseConnection();
    
    [ObservableProperty]
    private ObservableCollection<ModuleTopicStatsModel> _moduleTopicStats;
    [ObservableProperty]
    private ObservableCollection<TreeViewItem> _modules;
    [ObservableProperty]
    private ObservableCollection<TreeViewItem> _topics;
    
    private void LoadUserStatistics()
    {
        Modules = _statisticsService.LoadModules();
        Console.WriteLine(Modules.Count);

        foreach (var module in Modules)
        {
            Topics = _statisticsService.LoadTopics(module);
            Console.WriteLine(Topics.Count);
        }
        
        foreach (var topic in Topics)
        {
            Console.WriteLine(topic.Header);
            LoadTopicStats(topic);
        }
    }

    private void LoadTopicStats(TreeViewItem topic)
    {
        var module = (TreeViewItem) topic.Parent;
        string moduleName = module.Header.ToString();
        string topicName = topic.Header.ToString();

        int noCorrect = _userStatisticData.RetrieveNoCorrect(UserData.Username, moduleName, topicName);
        int noWrong = _userStatisticData.RetrieveNoWrong(UserData.Username,  moduleName, topicName);
        
        Console.WriteLine($"{moduleName} - {topicName} - {noCorrect} - {noWrong}");

        var moduleStats = ModuleTopicStats.FirstOrDefault(m => m.ModuleName == moduleName);
        Console.WriteLine(moduleStats);
        
        if (moduleStats == null)
        {
            moduleStats = new ModuleTopicStatsModel(moduleName);
            ModuleTopicStats.Add(moduleStats);
            
            Console.WriteLine("New Module");
        }

        moduleStats.Topics.Add(new TopicStatsModel(topicName, noCorrect, noWrong));
        
        Console.WriteLine("Added Topic");
    }
    
    #endregion
}