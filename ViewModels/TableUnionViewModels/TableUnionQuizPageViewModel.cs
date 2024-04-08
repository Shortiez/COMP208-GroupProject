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
    public const int answerMAX = 10;

    private UserStatisticData _userStatistics = new UserStatisticData(App.MainWindowViewModel.User.Username, "SQL", "Table Union");

    private UnionQuizGenerator _quizGenerator = new UnionQuizGenerator();
    private QuizQuestion<string> _currentQuestion;
    private Collection<Tuple<string, string>> answer = [];

    // loading bitmaps from file path
    static public Dictionary<String, Bitmap> Images = new Dictionary<String, Bitmap>
    {
        {"Red-Square", ImageHelper.LoadFromResource("/Assets/Red-Square.png")},
        {"Blue-Square", ImageHelper.LoadFromResource("/Assets/Blue-Square.png")},
        {"Pink-Square", ImageHelper.LoadFromResource("/Assets/Pink-Square.png")},
        {"White-Square", ImageHelper.LoadFromResource("/Assets/White-Square.png")},
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
        // clear the tables
        Table1.Clear();
        Table2.Clear();
        ClearButtonClick();

        // update the SQL question
        _currentQuestion = _quizGenerator.NewQuestion();
        Question = _currentQuestion.QuestionTitle;

        //update the names of the tables
        Titles = _currentQuestion.Options[4].Split('+');

        //update the column names of the tables
        Headers = _currentQuestion.Options;


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
        // calculate the correct answer
        answer.Clear();
        for (int x = 0; x < _currentQuestion.QuestionInput.Count; x += 2)
        {
            answer.Add(new Tuple<string, string>(_currentQuestion.QuestionInput[x], _currentQuestion.QuestionInput[x + 1]));
        }

        // check if user's answer = actual answer
        Tuple<string, string> a;
        bool contains = true;
        int count = 0;
        string item1, item2;
        while (contains && count < Answer.Count)
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
                contains = false;
                Explanation = "Items missing!";
            }
            // check if the inputted answer matches an entry in actual answer
            else
            {
                a = new Tuple<string, string>(Answer[count].name, Answer[count + 1].name);
                contains = answer.Contains(a);
                // ensure multiple idenical entries aren't allowed
                if (contains) answer.Remove(a);
            }
            count += 2;
        }

        // change Explanation deending on result
        if (!contains)
        {
            // incorrect
            Explanation = "Something is wrong";
            //_userStatistics.UpdateExistingRecord(0, 1);
        }
        else
        {
            // correct
            Explanation = "Correct! Well done";
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