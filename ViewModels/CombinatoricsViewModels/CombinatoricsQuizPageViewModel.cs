using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Avalonia.Controls;
using GroupProject.Scripts.Questions;
using GroupProject.Scripts.Questions.Quizzes.Combinatorics;
using GroupProject.Models;
using System;
using System.Windows.Input;

namespace GroupProject.ViewModels;

public partial class CombinatoricsQuizPageViewModel : ViewModelBase
{
    [ObservableProperty]
    private TopicContentModel topicContentModel;

    private CombQuizGenerator quizGenerator = new CombQuizGenerator();
    private string selectedOption = "";
    private QuizQuestion<int> currentQuestion;

    private UserStatisticData _userStatistics = new UserStatisticData(App.MainWindowViewModel.User.Username, "Combinatorics", "Combinatorics");

    [ObservableProperty]
    private string _questionTitleBlock = "How many ways are there to select 3 students for a prospectus photograph (order matters) from a group of 5?";
    [ObservableProperty]
    private ObservableCollection<int> _questionOptions = new ObservableCollection<int>();
    [ObservableProperty]
    private string _answerBlock = "";
    [ObservableProperty]
    private string _optionOne = "";
    [ObservableProperty]
    private string _optionTwo = "";
    [ObservableProperty]
    private string _optionThree = "";
    [ObservableProperty]
    private string _optionFour = "";
    [ObservableProperty]
    private string _optionFive = "";

    [RelayCommand]
    private void OneClicked()
    {
        selectedOption = OptionOne;
    }
    [RelayCommand]
    private void TwoClicked()
    {
        selectedOption = OptionTwo;
    }
    [RelayCommand]
    private void ThreeClicked()
    {
        selectedOption = OptionThree;
    }
    [RelayCommand]
    private void FourClicked()
    {
        selectedOption = OptionFour;
    }
    [RelayCommand]
    private void FiveClicked()
    {
        selectedOption = OptionFive;
    }

    public CombinatoricsQuizPageViewModel()
    {
        GenerateNewQuestion();
    }

    [RelayCommand]
    private void GenerateNewQuestion()
    {
        currentQuestion = quizGenerator.NewQuestion();

        QuestionTitleBlock = currentQuestion.QuestionTitle;
        QuestionOptions = new ObservableCollection<int>(currentQuestion.Options);

        OptionOne = QuestionOptions[0].ToString();
        OptionTwo = QuestionOptions[1].ToString();
        OptionThree = QuestionOptions[2].ToString();
        OptionFour = QuestionOptions[3].ToString();
        OptionFive = QuestionOptions[4].ToString();

        AnswerBlock = "";
    }

    [RelayCommand]
    private void SubmitAnswer()
    {
        if (selectedOption == "")
        {
            return;
        }
;
        var selectedOptionInt = int.Parse(selectedOption);

        if (selectedOptionInt == currentQuestion.Answer)
        {
            // Correct
            AnswerBlock = "Correct!";
        }
        else
        {
            // Incorrect
            AnswerBlock = "Incorrect!" + "\n" + "The correct answer was " + currentQuestion.Answer;
        }
    }



    [RelayCommand]
    private void BackButtonPressed()
    {
        var topicName = "Combinatorics";

        var topic = new TopicLearnSelectorPageViewModel()
        {
            CurrentTopic = topicName
        };

        App.MainWindowViewModel.CurrentContent = topic;
    }
}