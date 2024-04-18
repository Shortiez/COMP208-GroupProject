using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using GroupProject.Scripts;
using GroupProject.Scripts.Questions;
using GroupProject.Scripts.Questions.Quizzes.BinarySubtraction;
using GroupProject.Models;
using System;
using ExCSS;

namespace GroupProject.ViewModels;

public partial class BinarySubtractionLearnPageViewModel : ViewModelBase
{
    // generate a question
    [ObservableProperty]
    private TopicContentModel topicContentModel;
    
    int option1 = 01101010;
    int option2 = 00100100;

    [ObservableProperty]
    private ObservableCollection<int> _num1Digits = new ObservableCollection<int>(new int[] {0,1,1,0,1,0,1,0});
    [ObservableProperty]
    private ObservableCollection<int> _num2Digits = new ObservableCollection<int>(new int[] {0,0,1,0,0,1,0,0});
    [ObservableProperty]
    private ObservableCollection<int?> _answerDigits = new ObservableCollection<int?>(new int?[] {null,null,null,null,null,null,null,null});

    [ObservableProperty]
    private ObservableCollection<string> _columnWeights = new ObservableCollection<string>(new string[] {"normal","normal","normal","normal",
                                                          "normal","normal","normal","normal"});
    [ObservableProperty]
    private ObservableCollection<string> _calcWeights = new ObservableCollection<string>(new string[] {"normal", "normal", "normal", "normal"});
    [ObservableProperty]
    private ObservableCollection<char> _columnCarry = new ObservableCollection<char>(new char[] {' ',' ',' ',' ',' ',' ',' ',' '});

    [ObservableProperty]
    private bool _carry;
    [ObservableProperty]
    private int _index;
    [ObservableProperty]
    private String _explanationBlock = "";
    [ObservableProperty]
    private String _currentCalculation = "";

    private Boolean firstRound = true;

    public BinarySubtractionLearnPageViewModel()
    {
        Index = 7;
        ColumnWeights[7] = "bold";
        Carry = false;
        OnClickNext();
    }

    public BinarySubtractionLearnPageViewModel(QuizQuestion<int> quizQuestion)
    {
        Index = 7;
        ColumnWeights[7] = "bold";
        Carry = false;
        OnClickNext();
    }

    public override void Initialize()
    {
        base.Initialize();
        
        Index = 7;
        ColumnWeights[7] = "bold";
        Carry = false;
        OnClickNext();
    }

    private void populateArrays()
    {

        string a = Convert.ToString(option1, 2).PadLeft(8, '0');
        string b = Convert.ToString(option2, 2).PadLeft(8, '0');

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
            CurrentCalculation = "";
            ExplanationBlock = "The calculation is complete!";
        }
        else{
            if(Num1Digits[Index] == 0)
            {
                if(Num2Digits[Index] == 0)
                {
                    CurrentCalculation = "0 - 0 = 0";
                    AnswerDigits[Index] = 0;
                    CalcWeights[0] = "bold";
                    ExplanationBlock = "We have a calculation of 0 - 0, which is equal to 0.";
                }
                else
                {
                    CurrentCalculation = "10 - 0 = 0";
                    AnswerDigits[Index] = 1;
                    CalcWeights[3] = "bold";
                    ColumnCarry[Index] = '1';
                    ExplanationBlock = "We have a calculation of 0 - 1. To avoid working with negative numbers, we \"borrow\" a 1 from the digit to the left. This gives us 10 - 1, which is equal to 1.";
                    Num1Digits[Index-1] = 0;
                    // 10 - 1 == 1
                }
            }
            else
            {
                if(Num2Digits[Index] == 0)
                {
                    CurrentCalculation = "1 - 0 = 1";
                    AnswerDigits[Index] = 1;
                    CalcWeights[2] = "bold";
                    ExplanationBlock = "We have a calculation of 1 - 0, which is equal to 1.";
                    // 1 - 0 = 1
                }
                else
                {
                    CurrentCalculation = "1 - 1 = 0";
                    AnswerDigits[Index] = 0;
                    CalcWeights[1] = "bold";
                    ExplanationBlock = "We have a calculation of 1 - 1, which is equal to 0.";
                }
            }
        }
        if(firstRound){
            ExplanationBlock = "When subtracting two binary numbers, we start from the right-most two digits. " + ExplanationBlock;
            firstRound = false;
        }
    }

    private void borrowChain(int index){
        if(Num1Digits[Index] == 1){
            Num1Digits[Index] = 0;
            ColumnCarry[Index] = '1';
        }
        if(index >= 0){
            borrowChain(index-1);
            ColumnCarry[Index] = '1';
        }
    }

    [RelayCommand]
    private void OnClickNext()
    {
        for(int i = 0; i < CalcWeights.Count; i++){
            CalcWeights[i] = "normal";
        }  
        compareNumbers();
        if(Index >= 0 && Index < 7)
        {
            ColumnWeights[Index] = "bold";
            ColumnWeights[Index+1] = "normal";
        }
        Index--; 
    }

    [RelayCommand]
    private void OnClickBackToHome()
    {
        var topicName = "Binary Subtraction";
        Console.WriteLine(topicName);
        
        var topic = new TopicLearnSelectorPageViewModel()
        {
            CurrentTopic = topicName
        };
        
        App.MainWindowViewModel.ChangeContent(topic);
    }
}