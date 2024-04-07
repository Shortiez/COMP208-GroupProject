using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Avalonia.Controls;
using GroupProject.Scripts.Questions;
using GroupProject.Scripts.Questions.Quizzes.BinaryAddition;
using GroupProject.Models;
using System;

namespace GroupProject.ViewModels;

public partial class BinaryAdditionQuizPageViewModel : ViewModelBase
{

    [ObservableProperty]
    private TopicContentModel topicContentModel;
    
    private UserStatisticData _userStatistics = new UserStatisticData(App.MainWindowViewModel.User.Username, "Binary Arithmetic", "Binary Addition");

    private BinaryAdditionQuizGenerator _quizGenerator = new BinaryAdditionQuizGenerator();
    private int _selectedOption;
    public int SelectedOption {
        get => _selectedOption; 
        set {_selectedOption = value; OnPropertyChanged();}}
    private QuizQuestion<int> _currentQuestion;

    [ObservableProperty]
    private string _questionTitleBlock = "What is 1010 + 0101?";
    [ObservableProperty]
    private ObservableCollection<int> _questionOptions = new ObservableCollection<int>();
    [ObservableProperty]
    public string _answerBlock = "";

    [RelayCommand]
    private void GenerateNewQuestion()
    {
        _currentQuestion = _quizGenerator.NewQuestion();
        
        QuestionTitleBlock = _currentQuestion.QuestionTitle;
        QuestionOptions = new ObservableCollection<int>(_currentQuestion.Options);
    }
    
    [RelayCommand]
    private void SubmitAnswer(){
        if(_selectedOption == null){
            return;
        }

        var selectedOptionInt = _selectedOption;
        
        if(selectedOptionInt == _currentQuestion.Answer)
        {
            // Correct
            _answerBlock = "Correct!";
            // _userStatistics.UpdateExistingRecord(1,0);
        }
        else
        {
            // Incorrect
            _answerBlock = "Incorrect!" + "\n"
                        + "The correct answer was " + _currentQuestion.Answer;
            // _userStatistics.UpdateExistingRecord(0,1);
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
}