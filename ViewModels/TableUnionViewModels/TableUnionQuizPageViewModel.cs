using Avalonia.Controls;
using Avalonia.Controls.Chrome;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GroupProject.Models;
using GroupProject.Scripts.Questions;
using GroupProject.Scripts.Questions.Quizzes.TableUnion;
using GroupProject.Scripts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Diagnostics;
using Microsoft.CodeAnalysis.FlowAnalysis;

namespace GroupProject.ViewModels;
public partial class TableUnionQuizPageViewModel : ViewModelBase
{
    //to do
    // tidy up axaml
    // create and check answers
    // do the Learn stuff
    // move stuff from V to VM
    // add comments + restructure?

    public const string customFormat = "draggable-image-format";
    private DraggableImage currImage = new DraggableImage(null, "Empty", 0);
    static public bool deletingRN = false;
    static private int count = 1;
    public const int answerMAX = 12;
    public bool gotCurrQuestionRight = false;

    Random random = new Random();

    private UserStatisticData _userStatistics = new UserStatisticData(App.MainWindowViewModel.User.Username, "SQL", "Table Union");

    private UnionQuizGenerator _quizGenerator = new UnionQuizGenerator();
    private QuizQuestion<string> _currentQuestion;
    private Collection<Tuple<string, string>> answer = [];

    // loading bitmaps from file path
    static public Dictionary<String, Bitmap> Images = new Dictionary<String, Bitmap>
    {
        {"Circle-Blue", ImageHelper.LoadFromResource("/Assets/SQL Table Icons/Shapes/Circle-Blue.png")},
        {"Circle-Green", ImageHelper.LoadFromResource("/Assets/SQL Table Icons/Shapes/Circle-Green.png")},
        {"Circle-Orange", ImageHelper.LoadFromResource("/Assets/SQL Table Icons/Shapes/Circle-Orange.png")},
        {"Circle-Pink", ImageHelper.LoadFromResource("/Assets/SQL Table Icons/Shapes/Circle-Pink.png")},
        {"Circle-Purple", ImageHelper.LoadFromResource("/Assets/SQL Table Icons/Shapes/Circle-Purple.png")},
        {"Circle-Red", ImageHelper.LoadFromResource("/Assets/SQL Table Icons/Shapes/Circle-Red.png")},
        {"Circle-Yellow", ImageHelper.LoadFromResource("/Assets/SQL Table Icons/Shapes/Circle-Yellow.png")},
        
        {"Rhombus-Blue", ImageHelper.LoadFromResource("/Assets/SQL Table Icons/Shapes/Rhombus-Blue.png")},
        {"Rhombus-Green", ImageHelper.LoadFromResource("/Assets/SQL Table Icons/Shapes/Rhombus-Green.png")},
        {"Rhombus-Pink", ImageHelper.LoadFromResource("/Assets/SQL Table Icons/Shapes/Rhombus-Pink.png")},
        {"Rhombus-Purple", ImageHelper.LoadFromResource("/Assets/SQL Table Icons/Shapes/Rhombus-Purple.png")},
        {"Rhombus-Red", ImageHelper.LoadFromResource("/Assets/SQL Table Icons/Shapes/Rhombus-Red.png")},
        {"Rhombus-Yellow", ImageHelper.LoadFromResource("/Assets/SQL Table Icons/Shapes/Rhombus-Yellow.png")},
        
        {"Square-Blue", ImageHelper.LoadFromResource("/Assets/SQL Table Icons/Shapes/Square-Blue.png")},
        {"Square-Pink", ImageHelper.LoadFromResource("/Assets/SQL Table Icons/Shapes/Square-Pink.png")},
        {"Square-Red", ImageHelper.LoadFromResource("/Assets/SQL Table Icons/Shapes/Square-Red.png")},

        {"Triangle-Blue", ImageHelper.LoadFromResource("/Assets/SQL Table Icons/Shapes/Triangle-Blue.png")},
        {"Triangle-Green", ImageHelper.LoadFromResource("/Assets/SQL Table Icons/Shapes/Triangle-Green.png")},
        {"Triangle-Pink", ImageHelper.LoadFromResource("/Assets/SQL Table Icons/Shapes/Triangle-Pink.png")},
        {"Triangle-Purple", ImageHelper.LoadFromResource("/Assets/SQL Table Icons/Shapes/Triangle-Purple.png")},
        {"Triangle-Red", ImageHelper.LoadFromResource("/Assets/SQL Table Icons/Shapes/Triangle-Red.png")},
        {"Triangle-Yellow", ImageHelper.LoadFromResource("/Assets/SQL Table Icons/Shapes/Triangle-Yellow.png")},

        {"Baboon", ImageHelper.LoadFromResource("/Assets/SQL Table Icons/Apes/Gorilla.png")},
        {"Gorilla", ImageHelper.LoadFromResource("/Assets/SQL Table Icons/Apes/Baboon.png")},
        {"Mandril", ImageHelper.LoadFromResource("/Assets/SQL Table Icons/Apes/Mandril.png")},
        {"Orangutan", ImageHelper.LoadFromResource("/Assets/SQL Table Icons/Apes/Orangutan.png")},
        {"Squirrel", ImageHelper.LoadFromResource("/Assets/SQL Table Icons/Apes/Squirrel.png")},

        {"Apple", ImageHelper.LoadFromResource("/Assets/SQL Table Icons/Fruit/Apple.png")},
        {"Banana", ImageHelper.LoadFromResource("/Assets/SQL Table Icons/Fruit/Banana.png")},
        {"Cherry", ImageHelper.LoadFromResource("/Assets/SQL Table Icons/Fruit/Cherry.png")},
        {"Orange", ImageHelper.LoadFromResource("/Assets/SQL Table Icons/Fruit/Orange.png")},
        {"Pineapple", ImageHelper.LoadFromResource("/Assets/SQL Table Icons/Fruit/Pineapple.png")},

        {"1", ImageHelper.LoadFromResource("/Assets/SQL Table Icons/Numbers/1.png")},
        {"102", ImageHelper.LoadFromResource("/Assets/SQL Table Icons/Numbers/102.png")},
        {"12", ImageHelper.LoadFromResource("/Assets/SQL Table Icons/Numbers/12.png")},
        {"24", ImageHelper.LoadFromResource("/Assets/SQL Table Icons/Numbers/24.png")},
        {"26", ImageHelper.LoadFromResource("/Assets/SQL Table Icons/Numbers/26.png")},
        {"28", ImageHelper.LoadFromResource("/Assets/SQL Table Icons/Numbers/28.png")},
        {"3", ImageHelper.LoadFromResource("/Assets/SQL Table Icons/Numbers/3.png")},
        {"32", ImageHelper.LoadFromResource("/Assets/SQL Table Icons/Numbers/32.png")},
        {"35", ImageHelper.LoadFromResource("/Assets/SQL Table Icons/Numbers/35.png")},
        {"38", ImageHelper.LoadFromResource("/Assets/SQL Table Icons/Numbers/38.png")},
        {"4", ImageHelper.LoadFromResource("/Assets/SQL Table Icons/Numbers/4.png")},
        {"41", ImageHelper.LoadFromResource("/Assets/SQL Table Icons/Numbers/41.png")},
        {"56", ImageHelper.LoadFromResource("/Assets/SQL Table Icons/Numbers/56.png")},
        {"67", ImageHelper.LoadFromResource("/Assets/SQL Table Icons/Numbers/67.png")},
        {"7", ImageHelper.LoadFromResource("/Assets/SQL Table Icons/Numbers/7.png")},
        {"8", ImageHelper.LoadFromResource("/Assets/SQL Table Icons/Numbers/8.png")},

    };

    [ObservableProperty]
    private ObservableCollection<DraggableImage> _table1 = [];

    [ObservableProperty]
    private ObservableCollection<DraggableImage> _table2 = [];

    [ObservableProperty]
    private ObservableCollection<DraggableImage> _answer = [];

    [ObservableProperty]
    private string _question = "";

    [ObservableProperty]
    private string _explanation = "";

    [ObservableProperty]
    private string[] _titles = ["", ""];

    [ObservableProperty]
    private string[] _headers = ["", "", "", "", ""];

    public TableUnionQuizPageViewModel()
    {
        Explanation = "Lorem ipsum";
        GenerateQuestion();
    }

    // clear the answer table
    [RelayCommand]
    private void ClearButtonClick()
    {
        Answer.Clear();
        for (int x = 0; x < answerMAX; x++)
        {
            Answer.Add(new DraggableImage(null, "Empty", count + x));
        }
        count += answerMAX;
    }

    // go back to 'Quiz+Learn' Page
    [RelayCommand]
    private void BackButtonPressed()
    {
        var topicName = "Table Unions";

        var topic = new TopicLearnSelectorPageViewModel()
        {
            CurrentTopic = topicName
        };

        App.MainWindowViewModel.CurrentContent = topic;
    }

    [RelayCommand]
    private void GenerateQuestion()
    {
        // set the boolean back to false
        gotCurrQuestionRight = false;

        // clear the tables
        Table1.Clear();
        Table2.Clear();
        ClearButtonClick();

        // update the SQL question
        _currentQuestion = _quizGenerator.NewQuestion();
        Question = _currentQuestion.QuestionTitle;

        // update the names of the tables
        Titles = _currentQuestion.Options[4].Split('+');

        // update the column names of the tables
        Headers = _currentQuestion.Options;

        // update table names in instruction
        Explanation = $"We want to make our new \ntable, so drag the icons from \nthe {Titles[0]} and {Titles[1]} tables.";

        // calculate the correct answer
        answer.Clear();
        for (int x = 0; x < _currentQuestion.QuestionInput.Count; x += 2)
        {
            answer.Add(new Tuple<string, string>(_currentQuestion.QuestionInput[x], _currentQuestion.QuestionInput[x + 1]));
        }

        Bitmap bitmap;
        string name;
        int limit;
        if (_currentQuestion.QuestionInput.Count % 4 != 0) limit = _currentQuestion.QuestionInput.Count / 2 + 1;
        else limit = _currentQuestion.QuestionInput.Count / 2;
        // populate table 1
        for (int x = 0; x < limit; x++)
        {
            name = _currentQuestion.QuestionInput[x];
            bitmap = Images[name];
            Table1.Add(new DraggableImage(bitmap, name, count));
            count++;
        }
        // populate table 2
        for (int x = limit; x < _currentQuestion.QuestionInput.Count; x++)
        {
            name = _currentQuestion.QuestionInput[x];
            bitmap = Images[name];
            Table2.Add(new DraggableImage(bitmap, name, count));
            count++;
        }
    }

    [RelayCommand]
    private void SubmitAnswer()
    {
        if (gotCurrQuestionRight) return;

        // create a local copy of correct answers to manipulate
        Collection<Tuple<string, string>> localAnswer = new Collection<Tuple<string, string>>();
        foreach (var tuple in answer)
        {
            localAnswer.Add(new Tuple<string, string>(tuple.Item1, tuple.Item2));
        }

        // check if user's answer = actual answer
        Tuple<string, string> a;
        bool gotRight = true;
        int count = 0;
        string item1, item2;
        while (gotRight && count < Answer.Count)
        {
            item1 = Answer[count].name;
            item2 = Answer[count + 1].name;
            // if an entire row is empty it's fine
            if (item1.Equals("Empty") && item2.Equals("Empty"))
            {

            }
            // if a row is partially filled, it's wrong
            else if (item1.Equals("Empty") || item2.Equals("Empty"))
            {
                gotRight = false;
                Explanation = "Oh no! There seem to be some \nmissing items.";
            }
            // check if the inputted answer matches an entry in actual answer
            else
            {
                a = new Tuple<string, string>(item1, item2);
                gotRight = localAnswer.Contains(a);

                // ensure multiple idenical entries aren't allowed
                if (gotRight) localAnswer.Remove(a);
                else Explanation = "Hmm, some rows seem to have \nthe wrong items.";
            }
            count += 2;
        }

        // if the answer is not complete
        if (gotRight && localAnswer.Count > 0) Explanation = "There are a few items \nthat are missing!";
        if (localAnswer.Count > 0) gotRight = false;

        // change Explanation deending on result
        if (gotRight)
        {
            gotCurrQuestionRight = true;
            // correct
            int x = random.Next(0,5);
            if (x == 0) Explanation = "Good thinking! You got the answer.";
            else if (x == 1) Explanation = "That's right! Great work.";
            else if (x == 2) Explanation = "You got it!";
            else if (x == 3) Explanation = "Yes, that's exactly right.";
            else Explanation = "Correct! Well done.";
            //_userStatistics.UpdateExistingRecord(1, 0);
        }
        else
        {
            // incorrect
            //_userStatistics.UpdateExistingRecord(1, 0);
        }
    }

    public void BeginDelete(DraggableImage draggableImage)
    {
        currImage = draggableImage;
        deletingRN = true;
    }
    public void AddInAnswer(DraggableImage draggableImage, int index)
    {
        Answer.RemoveAt(index);
        Answer.Insert(index, new DraggableImage(draggableImage.iImage, draggableImage.name, count));
        count++;
    }
    public void DeleteInAnswer(int index)
    {
        Answer.RemoveAt(index);
        Answer.Insert(index, new DraggableImage(null, "Empty", count));
        count++;
        deletingRN = false;
    }
    public void ReplaceInAnswer(int index1)
    {
        // locate where the image currently being dragged is in the list
        int currImageID = currImage.id;
        int index2 = -1;
        for (int i = 0; i < Answer.Count; i++)
        {
            if (currImageID == Answer[i].id)
            {
                index2 = i;
                i = Answer.Count;
            }
        }

        AddInAnswer(Answer[index1], index2);
        AddInAnswer(currImage, index1);

        deletingRN = false;
    }
    public record DraggableImage(Bitmap iImage, String name, int id)
    {

    }
}