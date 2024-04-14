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
using ExCSS;

namespace GroupProject.ViewModels;

public partial class BinaryAdditionLearnPageViewModel : ViewModelBase
{
    // generate a question
    [ObservableProperty]
    private TopicContentModel topicContentModel;
    
    private BinaryAdditionQuizGenerator _quizGenerator = new BinaryAdditionQuizGenerator();
    private QuizQuestion<int> _currentQuestion;

    [ObservableProperty]
    private ObservableCollection<int> _num1Digits = new ObservableCollection<int>(new int[] {0,0,0,0,0,0,0,0});
    [ObservableProperty]
    private ObservableCollection<int> _num2Digits = new ObservableCollection<int>(new int[] {0,0,0,0,0,0,0,0});
    [ObservableProperty]
    private ObservableCollection<int> _answerDigits = new ObservableCollection<int>(new int[] {0,0,0,0,0,0,0,0});


    [ObservableProperty]
    private bool _carry;
    [ObservableProperty]
    private int _index;
    [ObservableProperty]
    String _explanationBlock = "";
    [ObservableProperty]
    String _currentCalculation = "";
    
    public BinaryAdditionLearnPageViewModel()
    {
        _currentQuestion = _quizGenerator.NewQuestion();
        Index = 7;
        Carry = false;
        populateArrays();
        ExplanationBlock = "When adding two binary numbers, we start from the right-most two digits, which in this case would be " + _num1Digits[7] + " + " + _num2Digits[7] + ".";
    }

    public BinaryAdditionLearnPageViewModel(QuizQuestion<int> quizQuestion)
    {
        _currentQuestion = quizQuestion;
        Index = 7;
        Carry = false;
        populateArrays();
        ExplanationBlock = "When adding two binary numbers, we start from the right-most two digits, which in this case would be " + _num1Digits[7] + " + " + _num2Digits[7] + ".";
    }

    private void populateArrays()
    {
        string a = Convert.ToString(_currentQuestion.QuestionInput[0], 2).PadLeft(8, '0');
        string b = Convert.ToString(_currentQuestion.QuestionInput[1], 2).PadLeft(8, '0');

        for(int i = 0; i < a.Length; i++){
            Num1Digits[i] = a[i] - '0';
        }

        for(int i = 0; i < b.Length; i++){
            Num2Digits[i] = b[i] - '0';
        }
    
    }

    private void compareNumbers()
    {
        if(Index < 0)
        {
            CurrentCalculation = "Done :)";
            ExplanationBlock = "The calculation is complete!";
        }
        else{
            if(Carry)
            {
                if(Num1Digits[Index] == 0)
                {
                    if(Num2Digits[Index] == 0)
                    {
                        CurrentCalculation = "1 + 0 + 0 = 1";
                        Carry = false;
                        AnswerDigits[Index] = 1;
                        ExplanationBlock = "We have a carry of 1 and a calculation of 0 + 0. This is equivalent to 1 + 0, resulting in 1.";
                    }
                    else
                    {
                        CurrentCalculation = "1 + 0 + 1 = 10";
                        Carry = true;
                        AnswerDigits[Index] = 0;
                        ExplanationBlock = "We have a carry of 1 and a calculation of 0 + 1. This is equivalent to 1 + 1, resulting in 1 with 1 carried over.";
                    }
                }
                else
                {
                    if(Num2Digits[Index] == 0)
                    {
                        CurrentCalculation = "1 + 1 + 0 = 10";
                        Carry = true;
                        AnswerDigits[Index] = 0;
                        ExplanationBlock = "We have a carry of 1 and a calculation of 1 + 0. This is equivalent to 1 + 1, resulting in 0 with 1 carried over.";
                    }
                    else
                    {   
                        CurrentCalculation = "1 + 1 + 1 = 11";
                        Carry = true;
                        AnswerDigits[Index] = 1;
                        ExplanationBlock = "We have a carry of 1 and a calculation of 1 + 1. This is equivalent to 1 + 1 + 1, resulting in 1 with 1 carried over.";
                    }
                }
            }
            else
            {
                if(Num1Digits[Index] == 0)
                {
                    if(Num2Digits[Index] == 0)
                    {
                        CurrentCalculation = "0 + 0 + 0 = 0";
                        Carry = false;
                        AnswerDigits[Index] = 0;
                        ExplanationBlock = "We have a carry of 0 and a calculation of 0 + 0, which is equal to 0 with no carry-over.";
                    }
                    else
                    {
                        CurrentCalculation = "0 + 0 + 1 = 1";
                        Carry = false;
                        AnswerDigits[Index] = 1;
                        ExplanationBlock = "We have a carry of 0 and a calculation of 0 + 1, which is equal to 1 with no carry-over.";
                    }
                }
                else
                {
                    if(Num2Digits[Index] == 0)
                    {
                        CurrentCalculation = "0 + 1 + 0 = 1";
                        Carry = false;
                        AnswerDigits[Index] = 1;
                        ExplanationBlock = "We have a carry of 0 and a calculation of 1 + 0, which is equal to 1 with no carry-over.";
                    }
                    else
                    {
                        CurrentCalculation = "0 + 1 + 1 = 10";
                        Carry = true;
                        AnswerDigits[Index] = 0;
                        ExplanationBlock = "We have a carry of 0 and a calculation of 1 + 1, which is equal to 0 with 1 carried over..";
                    }
                }
            }
        }
    }

    [RelayCommand]
    private void OnClickNext()
    {
        compareNumbers();
        Index--;
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