using CommunityToolkit.Mvvm.ComponentModel;

namespace GroupProject.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private ViewModelBase _currentContent = new MainContentPageViewModel();
    
    public MainWindowViewModel()
    {
        CurrentContent = new MainContentPageViewModel();
    }
}