using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using GroupProject.Scripts.Questions;
using GroupProject.Scripts.Questions.Quizzes.Example;

namespace GroupProject.Views;

public partial class ExampleTopicQuizView : UserControl
{
    private AdditionQuizGenerator _quizGenerator = new AdditionQuizGenerator();
    private RadioButton _selectedOption => OptionsPanel.Children.OfType<RadioButton>().FirstOrDefault(r => r.IsChecked == true);
    private QuizQuestion<int> _currentQuestion;
    
    public ExampleTopicQuizView()
    {
        InitializeComponent();
    }

    private void ShowQuestion()
    {
        _currentQuestion = _quizGenerator.NewQuestion();
        
        QuestionTitleTextBlock.Text = _currentQuestion.QuestionTitle;
        
        OptionsPanel.Children.Clear();
        
        for (int i = 0; i < _currentQuestion.Options.Length; i++)
        {
            var option = _currentQuestion.Options[i];
            RadioButton optionRadioButton = new RadioButton();
            optionRadioButton.Content = option;
           
            OptionsPanel.Children.Add(optionRadioButton);
        }
    }

    private void Button_OnClick_Submit(object? sender, RoutedEventArgs e)
    {
        if(_selectedOption == null)
        {
            return;
        }
        
        var selectedOption = _selectedOption.Content.ToString();
        var selectedOptionInt = int.Parse(selectedOption);
        
        if(selectedOptionInt == _currentQuestion.Answer)
        {
            // Correct
            AnswerTextBlock.Text = "Correct!";
        }
        else
        {
            // Incorrect
            AnswerTextBlock.Text = "Incorrect!" + "\n"
                                                + "The correct answer was " + _currentQuestion.Answer;
        }
    }

    private void Button_OnClick_GenerateNewQuestion(object? sender, RoutedEventArgs e)
    {
        ShowQuestion();
    }
}