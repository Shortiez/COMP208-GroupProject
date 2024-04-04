using Avalonia.Controls;
using Avalonia.Interactivity;
using GroupProject.ViewModels;

namespace GroupProject.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    protected override void OnLoaded(RoutedEventArgs e)
    {
        base.OnLoaded(e);
    }

    protected override void OnClosing(WindowClosingEventArgs e)
    {
        base.OnClosing(e);
        
        if (DataContext is MainWindowViewModel vm)
        {
            vm.OnClosing(e);
        }
    }
}