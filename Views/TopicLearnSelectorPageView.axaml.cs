using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace GroupProject.Views;

struct TopicToWindow
{
    public string Topic { get; set; }
    public Window Window { get; set; }
}

public partial class TopicLearnSelectorPageView : Window
{
    public string? TopicToDisplay { get; set; }

    private static readonly TopicToWindow[] Topics = new TopicToWindow[5]
    {
        new TopicToWindow()
        {
            Topic = "Binary Addition",
            Window = new BinaryAdditionTopicPageView()
        },
        new TopicToWindow()
        {
            Topic = "Recognizing Conflicts",
            Window = new RecognizingConflictsTopicPageView()
        },
        new TopicToWindow()
        {
            Topic = "Table Unions",
            Window = new TableUnionTopicPageView()
        },
        new TopicToWindow()
        {
            Topic = "Addition",
            Window = new ExampleTopicPageView()
        },
        new TopicToWindow()
        {
            Topic = "Logic Gates",
            Window = new ExampleTopicPageView()
        }
    };
    
    public TopicLearnSelectorPageView()
    {
        InitializeComponent();
        
        DataContext = this;
    }
    
    public void InitWindow()
    {
        TopicTitle.Text = TopicToDisplay;
    }
    
    private void Button_OnClick_LoadQuestions(object? sender, RoutedEventArgs e)
    {
        Topics.First(t => t.Topic == TopicToDisplay).Window.Show();
    }

    private void Button_OnClick_LoadExamples(object? sender, RoutedEventArgs e)
    {
        
    }

    private void Button_OnClick_BackToHome(object? sender, RoutedEventArgs e)
    {
        new MainContentPageView(" ").Show();
        
        this.Close();
    }
}