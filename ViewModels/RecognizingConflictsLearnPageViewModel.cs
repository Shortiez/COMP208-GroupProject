using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GroupProject.Models;
using GroupProject.Scripts;

namespace GroupProject.ViewModels;

public partial class RecognizingConflictsLearnPageViewModel : ViewModelBase
{
    public static readonly Bitmap ChimpCornerImage = ImageHelper.LoadFromResource("/Assets/Chimpa-corner.png");
    public static readonly Bitmap ChimpCornerIdeaImage = ImageHelper.LoadFromResource("/Assets/Chimpa-corner-idea.png");
    public static readonly Bitmap ChimpSuccessImage = ImageHelper.LoadFromResource("/Assets/chimpa-success.png");
    public static readonly Bitmap ChimpFailImage = ImageHelper.LoadFromResource("/Assets/Chimpa-fail.png");
    
    enum InteractionMode
    {
        Interactive,
        NonInteractive
    }

    private InteractionMode _interactionMode = InteractionMode.NonInteractive;
    private readonly TransactionModel[] _exampleScheduler = new TransactionModel[]
    {
        new TransactionModel { TransactionNumber = 1, Variable = 'x', Operation = 'R' },
        new TransactionModel { TransactionNumber = 1, Variable = 'x', Operation = 'W' },
        new TransactionModel { TransactionNumber = 3, Variable = 'y', Operation = 'W' },
        new TransactionModel { TransactionNumber = 2, Variable = 'x', Operation = 'W' },
        new TransactionModel { TransactionNumber = 1, Variable = 'z', Operation = 'R' }
    };
    private readonly TransactionResponse[] _transactionUserErrors = new TransactionResponse[]
    {
        new TransactionResponse
        {
            Title = "Transaction",
            ResponseMessage =
                "Uhoh! These transaction numbers are the same! Remember, they must be different for a conflict!"
        },
        new TransactionResponse
        {
            Title = "Variable",
            ResponseMessage =
                "Uhoh! These variables are different! Remember, it's only a conflict if the variable is the same."
        },
        new TransactionResponse
        {
            Title = "Operation",
            ResponseMessage = "Uhoh! These are both 'Reads'! Remember, at least one of them must be a 'Write', or 'W'"
        }
    };
    private readonly TransactionResponse[] _transactionTeachingMaterials = new TransactionResponse[]
    {
        new TransactionResponse
        {
            Title = "Intro00",
            ResponseMessage = "Hey there!"
        },
        new TransactionResponse
        {
            Title = "Intro01",
            ResponseMessage = "Let's get learning how to recognize conflicts in a schedule. We'll go step by step."
        },
        new TransactionResponse
        {
            Title = "Intro02",
            ResponseMessage = "There are 3 things to check, for every transaction against every other."
        },
        new TransactionResponse
        {
            Title = "Intro03",
            ResponseMessage = "1: They must be a different transaction number. \n" +
                              "2: They must be trying to access the same variable. \n" +
                              "3: At least one of them must be a write, or 'W'"
        },
        new TransactionResponse
        {
            Title = "Intro04",
            ResponseMessage = "First we check R1(x) against the next transacation."
        },
        new TransactionResponse
        {
            Title = "Intro05",
            ResponseMessage = "It's not a conflict! The transaction number (1) is the same in each!"
        },
        new TransactionResponse
        {
            Title = "Intro06",
            ResponseMessage = "Now you try! Click whichever transaction you think conflicts with R1(x)."
        },
        new TransactionResponse
        {
            Title = "End00",
            ResponseMessage = "Fantastic! You found the conflict!"
        },
        new TransactionResponse
        {
            Title = "End01",
            ResponseMessage = "Do you want to try some more questions?"
        }
    };
    private readonly TransactionResponse[] _transactionGenericResponses = new TransactionResponse[]
    {
        new TransactionResponse
        {
            Title = "Try Again",
            ResponseMessage = "Try again! Remember, you're looking for a conflict."
        }
    };
    private readonly Dictionary<string, (string, Bitmap)> _narrativeMap = new Dictionary<string, (string, Bitmap)>()
    {
        { "Intro00", ("Intro01", ChimpCornerIdeaImage) },
        { "Intro01", ("Intro02", ChimpCornerImage) },
        { "Intro02", ("Intro03", ChimpCornerImage) },
        { "Intro03", ("Intro04", ChimpCornerImage) },
        { "Intro04", ("Intro05", ChimpCornerImage) },
        { "Intro05", ("Intro06", ChimpCornerImage) },
        { "End00", ("End01", ChimpSuccessImage) }
    };

    [ObservableProperty]
    private TransactionResponse? _currentOutput;
    [ObservableProperty]
    private float _textOutputOpacity = 1.0f;
    [ObservableProperty] 
    private Bitmap _currentChimpImage = ChimpCornerImage;
    [ObservableProperty]
    private float _buttonXPos = 0;
    [ObservableProperty]
    private float _nextBoxOpacity = 1f;
    [ObservableProperty]
    private float _rectangleOpacity = 1f;


    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);
        
        if(e.PropertyName == nameof(CurrentOutput))
        {
            Console.WriteLine($"Current Output: {CurrentOutput?.ResponseMessage}");
        }
        
        // Check chimp image
        if (e.PropertyName == nameof(CurrentChimpImage))
        {
            Console.WriteLine($"Current Chimp Image: {CurrentChimpImage}");
        }
    }

    [ObservableProperty]
    private string _transactionButtonZeroContent;
    [ObservableProperty]
    private string _transactionButtonOneContent;
    [ObservableProperty]
    private string _transactionButtonTwoContent;
    [ObservableProperty]
    private string _transactionButtonThreeContent;
    [ObservableProperty]
    private string _transactionButtonFourContent;

    [ObservableProperty]
    private IBrush _transactionButtonZeroForeground;

    [ObservableProperty]
    private IBrush _transactionButtonTwoForeground;
    [ObservableProperty]
    private IBrush _transactionButtonThreeForeground;
    [ObservableProperty]
    private IBrush _transactionButtonFourForeground;

    public RecognizingConflictsLearnPageViewModel()
    {
        
        CurrentOutput = _transactionTeachingMaterials.
            FirstOrDefault(x => x.Title == "Intro00");
        
        CurrentChimpImage = ChimpCornerImage;
        
        TransactionButtonZeroContent = _exampleScheduler[0].ToString();
        TransactionButtonOneContent = _exampleScheduler[1].ToString();
        TransactionButtonTwoContent = _exampleScheduler[2].ToString();
        TransactionButtonThreeContent = _exampleScheduler[3].ToString();
        TransactionButtonFourContent = _exampleScheduler[4].ToString();


        TransactionButtonZeroForeground = Brushes.White;
        TransactionButtonTwoForeground = Brushes.White;
        TransactionButtonThreeForeground = Brushes.White;
        TransactionButtonFourForeground = Brushes.White;

       
    }
    //public override void Initialize()
    //{
      //  base.Initialize();
      //  IntroAnimations();
   // }
    //right now, this will loop through and index any conflicts. 
    //there are 2 issues right now - if there are more than one conflict, it will use the last one.
    //it will also inefficiently check schedules that already have an an index conflict
    private TransactionModel[] ConflictDetection(TransactionModel[] schedule)
    {
        Console.WriteLine(schedule[0].TransactionNumber);

        for (int i = 0; i < schedule.Length; i++)
        {
            for (int j = 0; j < schedule.Length; j++)
            {
                if (schedule[i].TransactionNumber == schedule[j].TransactionNumber)
                {

                }
                else if (schedule[i].Variable != schedule[j].Variable)
                {

                }
                else if (schedule[i].Operation == 'R' && schedule[j].Operation == 'R')
                {

                }
                else
                {
                    schedule[i].ConflictIndex = j;
                    schedule[j].ConflictIndex = i;

                    Console.WriteLine($"The schedule: {i} is conflicting with schedule: {j}");
                }
            }
        }

        return schedule;
    }

    private void NarrativePointer()
    {
        if (CurrentOutput != null && 
            _narrativeMap.TryGetValue(CurrentOutput.Title, out (string, Bitmap) value))
        {
            var (nextTitle, nextImage) = value;
            Console.WriteLine($"NarrativePointer Next: {nextTitle}");
            
            CurrentOutput = _transactionTeachingMaterials.
                FirstOrDefault(x => x.Title == nextTitle);
            CurrentChimpImage = nextImage;

            if (nextTitle == "Intro06")
            {
                _interactionMode = InteractionMode.Interactive;
            }
        }
        else if (_transactionUserErrors.Any(error => error.Title == CurrentOutput?.Title))
        {
            CurrentOutput = _transactionGenericResponses.FirstOrDefault(x => x.Title == "TryAgain");
            CurrentChimpImage = ChimpFailImage;
            _interactionMode = InteractionMode.Interactive;
        }
        
        FadeText();
    }
    
    [RelayCommand]
    private void OnClickNext()
    {
        IntroAnimations();
        
        SoundHelper.PlaySound("Button");
        if (_interactionMode == InteractionMode.NonInteractive){
            NarrativePointer();
        }
    }
    
    [RelayCommand]
    private void OnClickTransactionTwo()
    {
        if (_interactionMode == InteractionMode.NonInteractive)
        {
            return;
        }

        TransactionButtonTwoForeground = Brushes.Red;
        
        CurrentOutput = _transactionUserErrors.FirstOrDefault
            (x => x.Title == "Variable");
        CurrentChimpImage = ChimpFailImage;
        
        FadeText();
        
        _interactionMode = InteractionMode.NonInteractive;

            
    }
    
    [RelayCommand]
    private void OnClickTransactionThree()
    {
        if (_interactionMode == InteractionMode.NonInteractive)
        {
            return;
        }

        TransactionButtonThreeForeground = Brushes.LightGreen;
        
        CurrentOutput = _transactionTeachingMaterials.FirstOrDefault
            (x => x.Title == "End00");
        CurrentChimpImage = ChimpSuccessImage;
        
        FadeText();
        
        _interactionMode = InteractionMode.NonInteractive;
    }
    
    [RelayCommand]
    private void OnClickTransactionFour()
    {
        if (_interactionMode == InteractionMode.NonInteractive)
        {
            return;
        }

        TransactionButtonFourForeground = Brushes.Red;
        CurrentOutput = _transactionUserErrors.FirstOrDefault
            (x => x.Title == "Operation");
        CurrentChimpImage = ChimpFailImage;
        
        FadeText();
        
        _interactionMode = InteractionMode.NonInteractive;
    }

    [RelayCommand]
    private void BackButtonPressed()
    {
        var topicName = "Recognizing Conflicts";

        var topic = new TopicLearnSelectorPageViewModel()
        {
            CurrentTopic = topicName
        };

        App.MainWindowViewModel.ChangeContent(topic);
    }

    private void FadeText()
    {
        TextOutputOpacity = 0.2f;
        TextOutputOpacity = 1.0f;
    }

    private async void IntroAnimations()
    {
        //soundHelper.PlaySound("Whoosh");

        ButtonXPos = 50f;
        ButtonXPos = 0f;
        NextBoxOpacity = 0f;
        NextBoxOpacity = 1f;
        await Task.Delay(400); // wait for 0.4 seconds
    }

    private async Task GetTextAsync()
    {
        await Task.Delay(1000); // The delay is just for demonstration purpose

    }

    public override void Initialize()
    {

        
    }
}