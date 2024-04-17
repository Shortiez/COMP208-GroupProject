using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Avalonia.Controls;
using GroupProject.Scripts.Questions;
using GroupProject.Scripts.Questions.Quizzes.BinaryAddition;
using GroupProject.Models;
using GroupProject.Scripts;
using System;
using System.Collections.Generic;
using Avalonia.Media.Imaging;

namespace GroupProject.ViewModels;

public partial class BinaryAdditionQuizPageViewModel : ViewModelBase
{

    [ObservableProperty]
    private TopicContentModel topicContentModel;
    
    private UserStatisticData _userStatistics = new UserStatisticData(App.MainWindowViewModel.User.Username, "Binary Arithmetic", "Binary Addition");

    private BinaryAdditionQuizGenerator _quizGenerator = new BinaryAdditionQuizGenerator();
    private int _selectedOption;
    private QuizQuestion<int> _currentQuestion;

    static public Dictionary<String, Bitmap> MonkeyImages = new Dictionary<string, Bitmap>
    {
        {"Default", ImageHelper.LoadFromResource("/Assets/Chimpa-corner.png")},
        {"Fail", ImageHelper.LoadFromResource("/Assets/Chimpa-fail.png")},
        {"Success", ImageHelper.LoadFromResource("/Assets/chimpa-success.png")},
    };

    public BinaryAdditionQuizPageViewModel()
    {
        GenerateNewQuestion();
    }

    public override void Initialize()
    {
        base.Initialize();
        
        GenerateNewQuestion();
    }

    [ObservableProperty]
    private string _questionTitleBlock;
    [ObservableProperty]
    private ObservableCollection<int> _questionOptions = new ObservableCollection<int>();
    [ObservableProperty]
    public string _answerBlock;
    [ObservableProperty]
    private String _answerBlockColour = "#283A7B";
    [ObservableProperty]
    private bool _answerNotSubmitted = true;
    [ObservableProperty]
    private Bitmap _cornerImage = MonkeyImages["Default"];

    [ObservableProperty]
    private int _optionOne;
    [ObservableProperty]
    private int _optionTwo;
    [ObservableProperty]
    private int _optionThree;
    [ObservableProperty]
    private int _optionFour;
    [ObservableProperty]
    private int _optionFive;
    
    [RelayCommand]
    private void OneClicked()
    {
        _selectedOption = OptionOne;
    }

    [RelayCommand]
    private void TwoClicked()
    {
        _selectedOption = OptionTwo;
    }

    [RelayCommand]
    private void ThreeClicked()
    {
        _selectedOption = OptionThree;
    }

    [RelayCommand]
    private void FourClicked()
    {
        _selectedOption = OptionFour;
    }

    [RelayCommand]
    private void FiveClicked()
    {
        _selectedOption = OptionFive;
    }

    [RelayCommand]
    private void GenerateNewQuestion()
    {
        // set corner image back to default
        CornerImage = MonkeyImages["Default"];

        AnswerNotSubmitted = true;
        AnswerBlock = "";
        AnswerBlockColour = "#283A7B";

        _currentQuestion = _quizGenerator.NewQuestion();
        QuestionTitleBlock = _currentQuestion.QuestionTitle;
        QuestionOptions = new ObservableCollection<int>(_currentQuestion.Options);

        OptionOne = QuestionOptions[0];
        OptionTwo = QuestionOptions[1];
        OptionThree = QuestionOptions[2];
        OptionFour = QuestionOptions[3];
        OptionFive = QuestionOptions[4];

    }
    
    [RelayCommand]
    private void SubmitAnswer()
    {
        if(_selectedOption == null)
        {
            return;
        }

        if(_selectedOption == _currentQuestion.Answer)
        {
            AnswerBlock = "Correct!";
            AnswerNotSubmitted = false;
            _userStatistics.UpdateExistingRecord(1,0);

            // update corner image
            CornerImage = MonkeyImages["Success"];
        }
        else
        {
            AnswerBlock = "Incorrect!" + "\n"
                        + "The correct answer was " + _currentQuestion.Answer + ".";
            AnswerNotSubmitted = false;
            AnswerBlockColour = "red";

            _userStatistics.UpdateExistingRecord(0,1);

            // update corner image
            CornerImage = MonkeyImages["Fail"];
        }
    }

    [RelayCommand]
    private void OnClickBackToHome()
    {
        var topicName = "Binary Addition";
        Console.WriteLine(topicName);
        
        var topic = new TopicLearnSelectorPageViewModel()
        {
            CurrentTopic = topicName
        };

        App.MainWindowViewModel.ChangeContent(topic);
    }

    [RelayCommand]
    private void OnClickToLearn()
    {
        var topicName = "Binary Addition";
        Console.WriteLine(topicName);

        App.MainWindowViewModel.ChangeContent(new BinaryAdditionLearnPageViewModel(_currentQuestion));
    }
}