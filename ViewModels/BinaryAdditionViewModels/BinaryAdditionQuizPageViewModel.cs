using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Avalonia.Controls;
using GroupProject.Scripts.Questions;
using GroupProject.Scripts.Questions.Quizzes.BinaryAddition;
using GroupProject.Models;
using GroupProject.Scripts;
using System;

namespace GroupProject.ViewModels;

public partial class BinaryAdditionQuizPageViewModel : ViewModelBase
{

    [ObservableProperty]
    private TopicContentModel topicContentModel;
    
    private UserStatisticData _userStatistics = new UserStatisticData(App.MainWindowViewModel.User.Username, "Binary Arithmetic", "Binary Addition");

    private BinaryAdditionQuizGenerator _quizGenerator = new BinaryAdditionQuizGenerator();
    private int _selectedOption;
    private QuizQuestion<int> _currentQuestion;

    public BinaryAdditionQuizPageViewModel()
    {
        GenerateNewQuestion();
    }

    [ObservableProperty]
    private string _questionTitleBlock;
    [ObservableProperty]
    private ObservableCollection<int> _questionOptions = new ObservableCollection<int>();
    [ObservableProperty]
    public string _answerBlock;
    
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
        _currentQuestion = _quizGenerator.NewQuestion();
        QuestionTitleBlock = _currentQuestion.QuestionTitle;
        QuestionOptions = new ObservableCollection<int>(_currentQuestion.Options);

        OptionOne = QuestionOptions[0];
        OptionTwo = QuestionOptions[1];
        OptionThree = QuestionOptions[2];
        OptionFour = QuestionOptions[3];
        OptionFive = QuestionOptions[4];

        AnswerBlock = "";
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
            //_userStatistics.UpdateExistingRecord(1,0);
        }
        else
        {
            AnswerBlock = "Incorrect!" + "\n"
                        + "The correct answer was " + _currentQuestion.Answer + ".";
            //_userStatistics.UpdateExistingRecord(0,1);
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
        
        App.MainWindowViewModel.CurrentContent = topic;
    }

    [RelayCommand]
    private void OnClickToLearn()
    {
        var topicName = "Binary Addition";
        Console.WriteLine(topicName);

        App.MainWindowViewModel.CurrentContent = new BinaryAdditionLearnPageViewModel(_currentQuestion);
    }
}