using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using GroupProject.Scripts;

namespace GroupProject.ViewModels;

public partial class RecognizingConflictsTopicPageViewModel : ViewModelBase
{
    [ObservableProperty]
    private Task<Bitmap> _backButtonRef;
    
    public RecognizingConflictsTopicPageViewModel()
    {
        BackButtonRef = LMSYImages.BackButtonImage;
    }
}

public static class LMSYImages
{
    public static Task<Bitmap> BackButtonImage = ImageHelper.LoadFromWeb("/Assets/button-return.png");
}