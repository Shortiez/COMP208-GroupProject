using System.IO;
using System.Text;
using System.Text.Json;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GroupProject.Models;
using GroupProject.Services;

namespace GroupProject.ViewModels;

public partial class SettingsPageViewModel : ViewModelBase
{
    [ObservableProperty]
    private UserSettingsModel _userSettings;
    
    [ObservableProperty]
    private double _minVolume = 0;
    [ObservableProperty]
    private double _maxVolume = 100;

    [ObservableProperty] 
    private bool _themeSwitchToggled;
    
    public SettingsPageViewModel()
    {
        _userSettings = App.MainWindowViewModel.User.UserSettings;
    }
    
    [RelayCommand]
    private void SaveSettings()
    {
        UserSettings.SaveSettings();
    }

    [RelayCommand]
    private void TriggerTheme()
    {
        var mainWindow = App.MainWindowViewModel;
        var theme = mainWindow.ThemeService.CurrentTheme == "Dark" ? "Light" : "Dark";
        
        mainWindow.ThemeService.CurrentTheme = theme;
    }
    
    [RelayCommand]
    private void TriggerAudioMute()
    {
        _userSettings.IsAudioMuted = !UserSettings.IsAudioMuted;
    }
    
    [RelayCommand]
    private void TriggerResetSettingsToDefault()
    {
        UserSettings = new UserSettingsModel();
        UserSettings.SaveSettings();
    }
}