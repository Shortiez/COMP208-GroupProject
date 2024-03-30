using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using GroupProject.Models;

namespace GroupProject.ViewModels;

public partial class AccountPageViewModel : ViewModelBase
{
    [ObservableProperty]
    private UserDataModel _userData = App.MainWindowViewModel.User;
    [ObservableProperty]
    private string? _username = string.Empty;
    [ObservableProperty]
    private string? _email = string.Empty;
    [ObservableProperty]
    private string? _password = string.Empty;

    public override void Initialize()
    {
        base.Initialize();
        
        Username = UserData.Username;
        Email = UserData.Email;
        Password = UserData.Password;
    }
}