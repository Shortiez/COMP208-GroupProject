using CommunityToolkit.Mvvm.Input;

namespace GroupProject.ViewModels;

public partial class FallbackPageViewModel : ViewModelBase
{
    public override void Initialize()
    {
        base.Initialize();
    }
    
    [RelayCommand]
    private void OnClickBackToHome()
    {
        App.MainWindowViewModel.ChangeContent(new MainContentPageViewModel());
    }
}