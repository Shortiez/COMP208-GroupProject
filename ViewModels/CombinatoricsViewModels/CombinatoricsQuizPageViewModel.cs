using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Avalonia.Controls;
using GroupProject.Scripts.Questions;
using GroupProject.Scripts.Questions.Quizzes.Combinatorics;
using GroupProject.Models;
using System;

namespace GroupProject.ViewModels;

public partial class CombinatoricsQuizPageViewModel : ViewModelBase
{
    [ObservableProperty]
    private TopicContentModel topicContentModel;

    private CombQuizGenerator quizGenerator = new CombQuizGenerator();
    private RadioButton selectedOption;
    private QuizQuestion<int> currentQuestion;

    [ObservableProperty]
    private string _questionTitleBlock = "How many ways are there to select 3 students for a prospectus photograph (order matters) from a group of 5?";
    [ObservableProperty]
    private ObservableCollection<int> _questionOptions = new ObservableCollection<int>();
    [ObservableProperty]
    private string _answerBlock = "Hello!";


    [RelayCommand]
    private void GenerateNewQuestion()
    {
        currentQuestion = quizGenerator.NewQuestion();

        QuestionTitleBlock = currentQuestion.QuestionTitle;
        QuestionOptions = new ObservableCollection<int>(currentQuestion.Options);
    }

    [RelayCommand]
    private void SubmitAnswer()
    {
        
        if (selectedOption == null)
        {
            return;
        }

        var selectedOpt = selectedOption.Content.ToString();
        var selectedOptionInt = int.Parse(selectedOpt);

        if (selectedOptionInt == currentQuestion.Answer)
        {
            // Correct
           // AnswerBlock = "Correct!";
        }
        else
        {
            // Incorrect
            //AnswerBlock = "Incorrect!" + "\n" + "The correct answer was " + currentQuestion.Answer;
        }
        
    }
}