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

    [ObservableProperty]
    private string _questionTitleBlock = "How many ways are there to select 3 students for a prospectus photograph (order matters) from a group of 5?";
    [ObservableProperty]
    private ObservableCollection<int> _questionOptions = new ObservableCollection<int>();
    [ObservableProperty]
    private string _answerBlock = "";
    [ObservableProperty]
    private string _questionOne = "";
    [ObservableProperty]
    private string _questionTwo = "";
    [ObservableProperty]
    private string _questionThree = "";
    [ObservableProperty]
    private string _questionFour = "";
    [ObservableProperty]
    private string _questionFive = "";

    [RelayCommand]
    private void OneClicked()
    {
        selectedOption = QuestionOne;
    }
    [RelayCommand]
    private void TwoClicked()
    {
        selectedOption = QuestionTwo;
    }
    [RelayCommand]
    private void ThreeClicked()
    {
        selectedOption = QuestionThree;
    }
    [RelayCommand]
    private void FourClicked()
    {
        selectedOption = QuestionFour;
    }
    [RelayCommand]
    private void FiveClicked()
    {
        selectedOption = QuestionFive;
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

        QuestionOne = QuestionOptions[0].ToString();
        QuestionTwo = QuestionOptions[1].ToString();
        QuestionThree = QuestionOptions[2].ToString();
        QuestionFour = QuestionOptions[3].ToString();
        QuestionFive = QuestionOptions[4].ToString();

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