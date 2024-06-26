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
    private ViewModelBase _currentPage = new PickATopicPageViewModel();
    
    [ObservableProperty]
    private SidebarListItemTemplate _selectedListItem;

    [ObservableProperty]
    private bool _themeSwitchToggled;
    
    public ObservableCollection<SidebarListItemTemplate> SidebarListItems { get; } =
    [
        new SidebarListItemTemplate(typeof(PickATopicPageViewModel), "Study", "Home"),
        new SidebarListItemTemplate(typeof(SettingsPageViewModel), "SettingsRegular"),
        new SidebarListItemTemplate(typeof(AccountPageViewModel), "Account")
    ];

    public override void Initialize()
    {
        base.Initialize();
        
        CurrentPage.Initialize();
    }

    [RelayCommand]
    private void TriggerSidebar()
    {
        IsSidebarOpen = !IsSidebarOpen;
    }

    [RelayCommand]
    private void TriggerLogout()
    {
        App.MainWindowViewModel.User = null;
        
        App.MainWindowViewModel.ChangeContent(new LoginPageViewModel());
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