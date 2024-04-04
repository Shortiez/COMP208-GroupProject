using System;
using CommunityToolkit.Mvvm.ComponentModel;
using GroupProject.Models;
using GroupProject.ViewModels;

namespace GroupProject.Services;

public class ThemeService
{
    public event Action<string> ThemeChanged;
    public string CurrentTheme
    {
        get => _userSettings.Theme;
        set
        {
            _userSettings.Theme = value;
            ThemeChanged?.Invoke(value);
            
            Console.WriteLine($"Theme changed to {value}");
        }
    }
    
    private UserSettingsModel _userSettings;
    
    public ThemeService(UserSettingsModel userSettings)
    {
        _userSettings = userSettings;
        CurrentTheme = _userSettings.Theme;

        switch (App.MainWindowViewModel.CurrentContent)
        {
            case SettingsPageViewModel settingsPage:
                settingsPage.ThemeSwitchToggled = CurrentTheme == "Dark";
            
                Console.WriteLine("Settings page theme toggled");
                break;
            case MainContentPageViewModel mainContentPage:
                mainContentPage.ThemeSwitchToggled = CurrentTheme == "Dark";
            
                Console.WriteLine("Main content page theme toggled");
                break;
        }

        ThemeChanged += OnThemeChanged;
    }

    private void OnThemeChanged(string newTheme)
    {
        _userSettings.Theme = newTheme;
        App.MainWindowViewModel.CurrentTheme = newTheme;

        switch (App.MainWindowViewModel.CurrentContent)
        {
            case SettingsPageViewModel settingsPage:
                settingsPage.ThemeSwitchToggled = newTheme == "Dark";
            
                Console.WriteLine("Settings page theme toggled");
                break;
            case MainContentPageViewModel mainContentPage:
                mainContentPage.ThemeSwitchToggled = newTheme == "Dark";
            
                Console.WriteLine("Main content page theme toggled");
                break;
        }

        _userSettings.SaveSettings();
    }
}