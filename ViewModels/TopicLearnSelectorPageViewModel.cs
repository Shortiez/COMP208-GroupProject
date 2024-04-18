using System.Linq;
using CommunityToolkit.Mvvm.Input;
using GroupProject.Models;

namespace GroupProject.ViewModels;

public partial class TopicLearnSelectorPageViewModel : ViewModelBase
{
    public string? CurrentTopic { get; set; }
    public static readonly TopicContentModel[] learnTopics = new TopicContentModel[4]
    {
        new TopicContentModel()
        {
            Topic = "Binary Addition",
            Content = new BinaryAdditionLearnPageViewModel()
        },
        new TopicContentModel()
        {
            Topic = "Binary Subtraction",
            Content = new BinarySubtractionLearnPageViewModel()
        },
        new TopicContentModel()
        {
            Topic = "Recognizing Conflicts",
            Content = new RecognizingConflictsLearnPageViewModel()
        },
        new TopicContentModel()
        {
            Topic = "Combinatorics",
            Content = new CombinatoricsLearnPageViewModel()
        }
    };

    public static readonly TopicContentModel[] quizTopics = new TopicContentModel[5]
    {
        new TopicContentModel()
        {
            Topic = "Binary Addition",
            Content = new BinaryAdditionQuizPageViewModel()
        },
        new TopicContentModel()
        {
            Topic = "Binary Subtraction",
            Content = new BinarySubtractionQuizPageViewModel()
        },
        new TopicContentModel()
        {
            Topic = "Table Unions",
            Content = new TableUnionQuizPageViewModel()
        },
        new TopicContentModel()
        {
            Topic = "Addition",
            Content = new ExampleTopicQuizViewModel()
        },
        new TopicContentModel()
        {
            Topic = "Combinatorics",
            Content = new CombinatoricsQuizPageViewModel()
        }
    };


    public TopicLearnSelectorPageViewModel()
    {
    }
    public TopicLearnSelectorPageViewModel(string? topic)
    {
        CurrentTopic = topic;
    }
    
    private void LoadTopicExamplesPage()
    {
        try
        {
            var content = learnTopics.First(t => t.Topic == CurrentTopic).Content;
            App.MainWindowViewModel.ChangeContent(content);
        }
        catch (System.InvalidOperationException)
        {
            App.MainWindowViewModel.ChangeContent(new FallbackPageViewModel());
        }
    }

    private void LoadTopicQuestionsPage()
    {
        try
        {
            var content = quizTopics.First(t => t.Topic == CurrentTopic).Content;
            App.MainWindowViewModel.ChangeContent(content);
        }
        catch (System.InvalidOperationException)
        {
            App.MainWindowViewModel.ChangeContent(new FallbackPageViewModel());
        }
    }
        
    [RelayCommand]
    private void OnClickLoadQuestions()
    {
        LoadTopicQuestionsPage();
    }
    
    [RelayCommand]
    private void OnClickLoadExamples()
    {
        LoadTopicExamplesPage();
    }
    
    [RelayCommand]
    private void OnClickBackToHome()
    {
        App.MainWindowViewModel.ChangeContent(new MainContentPageViewModel());
    }
}