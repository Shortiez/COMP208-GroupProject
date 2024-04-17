using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.Styling;
using CommunityToolkit.Mvvm.DependencyInjection;
using GroupProject.ViewModels;
using GroupProject.Views;

namespace GroupProject;

public partial class App : Application
{
    public static MainWindowViewModel? MainWindowViewModel { get; set; }
    public static Window? MainWindow { get; set; }
    
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel()
            };
            
            MainWindow = desktop.MainWindow;
            
            MainWindowViewModel = desktop.MainWindow.DataContext as MainWindowViewModel;
            MainWindowViewModel.ChangeContent(new RegisterPageViewModel());

            MainWindowViewModel.CreateUserData();
        }
        
        base.OnFrameworkInitializationCompleted();
    }
}