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
    public static MainWindowViewModel? MainWindow { get; set; }
    
    
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
            
            MainWindow = desktop.MainWindow.DataContext as MainWindowViewModel;
            MainWindow.CurrentContent = new RegisterPageViewModel();
            Console.WriteLine($"App Set MainWindow: {MainWindow}");
            Console.WriteLine("App MainWindow DataContext: " + desktop.MainWindow.DataContext);
        }
        
        base.OnFrameworkInitializationCompleted();
    }
}