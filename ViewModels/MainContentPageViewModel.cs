using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GroupProject.Models;
using GroupProject.Services;
using GroupProject.Views;

namespace GroupProject.ViewModels;

public partial class MainContentPageViewModel : ViewModelBase
{
    [ObservableProperty]
    private bool _isSidebarOpen = true;
    
    [ObservableProperty]
    private ViewModelBase _currentPage = new HomePageViewModel();
    
    [ObservableProperty]
    private SidebarListItemTemplate _selectedListItem;

    [ObservableProperty]
    private bool _themeSwitchToggled;
    
    public ObservableCollection<SidebarListItemTemplate> SidebarListItems { get; } =
    [
        new SidebarListItemTemplate(typeof(HomePageViewModel), "HomeRegular"),
        new SidebarListItemTemplate(typeof(PickATopicPageViewModel), "Study"),
        new SidebarListItemTemplate(typeof(SettingsPageViewModel), "SettingsRegular"),
        new SidebarListItemTemplate(typeof(AccountPageViewModel), "Account")
    ];

    [RelayCommand]
    private void TriggerSidebar()
    {
        IsSidebarOpen = !IsSidebarOpen;
    }
    
    [RelayCommand]
    private void TriggerTheme()
    {
        var mainWindow = App.MainWindowViewModel;
        var theme = mainWindow.ThemeService.CurrentTheme == "Dark" ? "Light" : "Dark";   
        
        mainWindow.ThemeService.CurrentTheme = theme;
    }
    
    partial void OnSelectedListItemChanged(SidebarListItemTemplate? value)
    {
        if (value is null) return;

        var instance = Activator.CreateInstance(value.ModelType);
        
        if(instance is null) return;

        CurrentPage = (ViewModelBase)instance;
        CurrentPage.Initialize();
    }
}