using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Avalonia.Controls;
using GroupProject.Scripts.Questions;
using GroupProject.Scripts.Questions.Quizzes.Combinatorics;
using GroupProject.Models;

namespace GroupProject.ViewModels;

public partial class CombinatoricsTopicPageViewModel : ViewModelBase
{
    [ObservableProperty]
    private TopicContentModel topicContentModel;

    private CombQuizGenerator _quizGenerator = new CombQuizGenerator();
    private RadioButton _selectedOption; // => OptionsPanel.Children.OfType<RadioButton>().FirstOrDefault(r => r.IsChecked == true);
    private QuizQuestion<int> _currentQuestion;

    [ObservableProperty]
    private string _questionTitleBlock = "How many ways are there to select 3 students for a prospectus photograph (order matters) from a group of 5?";
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
    private void SubmitAnswer()
    {
        if (_selectedOption == null)
        {
            return;
        }

        var selectedOption = _selectedOption.Content.ToString();
        var selectedOptionInt = int.Parse(selectedOption);

        if (selectedOptionInt == _currentQuestion.Answer)
        {
            // Correct
            AnswerBlock = "Correct!";
        }
        else
        {
            // Incorrect
            AnswerBlock = "Incorrect!" + "\n"
                        + "The correct answer was " + _currentQuestion.Answer;
        }
    }
}