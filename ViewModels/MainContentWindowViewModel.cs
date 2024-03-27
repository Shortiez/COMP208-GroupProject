using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GroupProject.Scripts;
using GroupProject.Windows;

namespace GroupProject.ViewModels;

struct ContentPage
{
    public string Title { get; set; }
    public Window Window { get; set; }
}

public partial class MainContentWindowViewModel : ViewModelBase
{
    [ObservableProperty] 
    private bool _isSidebarOpen = true;

    [ObservableProperty]
    private ViewModelBase _currentPage = new HomePageViewModel();
    
    private readonly ContentPage[] _contentPageMapping = new ContentPage[]
    {
        new ContentPage
        {
            Title = "Home", Window = new PickATopicPageView()
        },
        new ContentPage
        {
            Title = "Study", Window = new PickATopicPageView()
        },
        new ContentPage
        {
            Title = "Settings", Window = new SettingsPageView()
        },
        new ContentPage
        {
            Title = "Account", Window = new AccountPageView()
        }
    };

    public ObservableCollection<SidebarListItem> SidebarListItems { get; } = new()
    { 
        new SidebarListItem(typeof(HomePageView))
    };
    
    [RelayCommand]
    private void TriggerSidebar()
    {
        IsSidebarOpen = !IsSidebarOpen;
    }
}

public class SidebarListItemTemplate
{
    public string Label { get; }
    public Type ModelType { get; }
    
    public SidebarListItemTemplate(Type modelType)
    {
        ModelType = modelType;
        Label = modelType.Name.Replace("PageViewModel", "");
    }
}