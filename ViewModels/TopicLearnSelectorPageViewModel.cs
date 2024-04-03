using System.Linq;
using CommunityToolkit.Mvvm.Input;
using GroupProject.Models;

namespace GroupProject.ViewModels;

public partial class TopicLearnSelectorPageViewModel : ViewModelBase
{
    public string? CurrentTopic { get; set; }
    public static readonly TopicContentModel[] Topics = new TopicContentModel[5]
    {
        new TopicContentModel()
        {
            Topic = "Binary Addition",
            Content = new BinaryAdditionTopicPageViewModel()
        },
        new TopicContentModel()
        {
            Topic = "Recognizing Conflicts",
            Content = new RecognizingConflictsTopicPageViewModel()
        },
        new TopicContentModel()
        {
            Topic = "Table Unions",
            Content = new TableUnionTopicPageViewModel()
        },
        new TopicContentModel()
        {
            Topic = "Addition",
            Content = new ExampleTopicPageViewModel()
        },
        new TopicContentModel()
        {
            Topic = "Combinatorics",
            Content = new CombinatoricsTopicPageViewModel()
        }
    };

    public TopicLearnSelectorPageViewModel()
    {
    }
    public TopicLearnSelectorPageViewModel(string? topic)
    {
        CurrentTopic = topic;
    }
    
    private void LoadTopicQuestionsPage()
    {
        var content = Topics.First(t => t.Topic == CurrentTopic).Content;
        App.MainWindowViewModel.CurrentContent = content;
    }
    
    private void LoadTopicExamplesPage()
    {
        var content = Topics.First(t => t.Topic == CurrentTopic).Content;
        
        App.MainWindowViewModel.CurrentContent = content;
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
        App.MainWindowViewModel.CurrentContent = new MainContentPageViewModel();
    }
}