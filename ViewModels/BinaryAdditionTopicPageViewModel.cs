using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Avalonia.Controls;
using GroupProject.Scripts.Questions;
using GroupProject.Scripts.Questions.Quizzes.BinaryAddition;
using GroupProject.Models;

namespace GroupProject.ViewModels;

public partial class BinaryAdditionTopicPageViewModel : ViewModelBase
{
    [ObservableProperty]
    private TopicContentModel topicContentModel;
    
    private UserStatisticData _userStatistics = new UserStatisticData(App.MainWindowViewModel.User.Username, "Binary Arithmetic", "Binary Addition");

    private BinaryAdditionQuizGenerator _quizGenerator = new BinaryAdditionQuizGenerator();
    private RadioButton _selectedOption; // => OptionsPanel.Children.OfType<RadioButton>().FirstOrDefault(r => r.IsChecked == true);
    private QuizQuestion<int> _currentQuestion;

    public string QuestionTitleBlock {get; private set;}
    public ObservableCollection<int> QuestionOptions {get; private set;}
    public string AnswerBlock {get; private set;}

    [RelayCommand]
    private void GenerateNewQuestion()
    {
        _currentQuestion = _quizGenerator.NewQuestion();
        
        QuestionTitleBlock = _currentQuestion.QuestionTitle;
        QuestionOptions = new ObservableCollection<int>(_currentQuestion.Options);
    }

    [RelayCommand]
    private void SubmitAnswer(){
        if(_selectedOption == null)
        {
            return;
        }
        
        var selectedOption = _selectedOption.Content.ToString();
        var selectedOptionInt = int.Parse(selectedOption);
        
        if(selectedOptionInt == _currentQuestion.Answer)
        {
            // Correct
            AnswerBlock = "Correct!";
            _userStatistics.UpdateExistingRecord(1,0);
        }
        else
        {
            // Incorrect
            AnswerBlock = "Incorrect!" + "\n"
                        + "The correct answer was " + _currentQuestion.Answer;
            _userStatistics.UpdateExistingRecord(0,1);
        }
    }
}