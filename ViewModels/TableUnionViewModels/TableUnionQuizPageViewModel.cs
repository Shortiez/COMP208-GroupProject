using Avalonia.Controls;
using Avalonia.Controls.Chrome;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GroupProject.Models;
using GroupProject.Scripts.Questions;
using GroupProject.Scripts.Questions.Quizzes.TableUnion;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace GroupProject.ViewModels;
public partial class TableUnionQuizPageViewModel : ViewModelBase
{
    public const string customFormat = "draggable-image-format";
    private DraggableImage currImage;
    static public bool deletingRN;
    static private int count;
    static private int answerMAX = 8;

    private UserStatisticData _userStatistics = new UserStatisticData(App.MainWindowViewModel.User.Username, "SQL", "Table Union");

    private UnionQuizGenerator _quizGenerator = new UnionQuizGenerator();
    private QuizQuestion<string> _currentQuestion;

    // retrieving filepath for current device
    static string currDir = Directory.GetCurrentDirectory().ToString();
    static string truncatedDir = (currDir.Split(new[] { "bin" }, 2, StringSplitOptions.None))[0];
    static string filepath = truncatedDir[0] + "Assets\\";

    // loading bitmaps from calculated file path
    static public Dictionary<String, Bitmap> Images = new Dictionary<String, Bitmap>
    {
        {"Red-Square", new Bitmap(filepath + "Red-Square.png")},
        {"Blue-Square", new Bitmap(filepath + "Blue-Square.png")},
        {"Pink-Square", new Bitmap(filepath + "Pink-Square.png")},
        {"White-Square", new Bitmap(filepath + "White-Square.png")},
    };

    [ObservableProperty]
    private ObservableCollection<DraggableImage> _table1;

    [ObservableProperty]
    private ObservableCollection<DraggableImage> _table2;

    [ObservableProperty]
    private ObservableCollection<DraggableImage> _answer;

    [ObservableProperty]
    private String _question;

    public TableUnionQuizPageViewModel()
    {
        count = 0;
        GenerateQuestion();
        deletingRN = false;
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
    private void GenerateQuestion()
    { 
        // clear the tables
        Table1.Clear();
        Table2.Clear();
        ClearButtonClick();

        // update the SQL question
        _currentQuestion = _quizGenerator.NewQuestion();
        Question = _currentQuestion.QuestionTitle;

        Bitmap bitmap;
        string name;
        // populate table 1
        for (int x = 0; x < _currentQuestion.QuestionInput.Count/2; x++)
        {
            name = _currentQuestion.QuestionInput[x];
            bitmap = Images[name];
            Table1.Add(new DraggableImage(bitmap, name, count));
            count++;
        }
        // populate table 2
        for (int x = _currentQuestion.QuestionInput.Count / 2; x < _currentQuestion.QuestionInput.Count; x++)
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

        if (false)//selectedOptionInt == _currentQuestion.Answer)
        {
            // Correct
            // AnswerBlock = "Correct!";
            //_userStatistics.UpdateExistingRecord(1, 0);
        }
        else
        {
            // Incorrect
            // AnswerBlock = "Incorrect!" + "\n" + "The correct answer was " + _currentQuestion.Answer;
            //_userStatistics.UpdateExistingRecord(0, 1);
        }
    }


    public void BeginDelete(DraggableImage draggableImage)
    {
        currImage = draggableImage;
        deletingRN = true;
    }

    public record DraggableImage(Bitmap iImage, String name, int id)
    {

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
}