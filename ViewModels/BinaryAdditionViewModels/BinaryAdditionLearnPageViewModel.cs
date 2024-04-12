using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using GroupProject.Scripts;
using GroupProject.Scripts.Questions;
using GroupProject.Scripts.Questions.Quizzes.BinaryAddition;
using GroupProject.Models;
using System;

namespace GroupProject.ViewModels;

public partial class BinaryAdditionLearnPageViewModel : ViewModelBase
{
    // generate a question
    [ObservableProperty]
    private TopicContentModel topicContentModel;
    
    private BinaryAdditionQuizGenerator _quizGenerator = new BinaryAdditionQuizGenerator();
    private QuizQuestion<int> _currentQuestion;

    [ObservableProperty]
    private int[] _num1digits;
    [ObservableProperty]
    private int[] _num2digits;
    [ObservableProperty]
    private int[] _answerdigits;
    [ObservableProperty]
    private int _remainder;
    [ObservableProperty]
    private int _index;

    
    public BinaryAdditionLearnPageViewModel()
    {

    }

    public BinaryAdditionLearnPageViewModel(QuizQuestion<int> quizQuestion)
    {
        _currentQuestion = quizQuestion;
    }

    private void populateNumbers()
    {
        // loop through option 1, populating ints

        // loop through option 2, populating ints

        // loop through answer, populating ints
    }

    private void compareNumbers()
    {
        if(_index > 7)
        {

        }
        else{
            if(_num1digits[_index] == null || _num2digits[_index] == null)
            {
                // calculate complete. 
            }
            else{
                // compare current index nums
                    // case 0 + 1
                        // answer[i] = 1
                    // case 1 + 1
                        // answer[i] = 0
                        // remainder = 1
                    // case 1 + 0
                        // answer[i] = 1
                    // case 1 + 1 + 1
                    // case 0 + 0
            }
        }
        // if one of them is null, calculation is complete. 
    }

    private void generateQuestion()
    {

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