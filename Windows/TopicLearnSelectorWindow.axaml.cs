using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace GroupProject.Windows;

struct TopicToWindow
{
    public string Topic { get; set; }
    public Window Window { get; set; }
}

public partial class TopicLearnSelectorWindow : Window
{
    public string? TopicToDisplay { get; set; }

    private static readonly TopicToWindow[] Topics = new TopicToWindow[4]
    {
        new TopicToWindow()
        {
            Topic = "Binary Addition",
            Window = new BinaryAdditionTopicWindow()
        },
        new TopicToWindow()
        {
            Topic = "Recognizing Conflicts",
            Window = new RecognizingConflictsTopicWindow()
        },
        new TopicToWindow()
        {
            Topic = "Table Unions",
            Window = new TableUnionTopicWindow()
        },
        new TopicToWindow()
        {
            Topic = "Addition",
            Window = new ExampleTopicPage()
        }
    };
    
    public TopicLearnSelectorWindow()
    {
        InitializeComponent();
        
        DataContext = this;
    }
    
    public void InitWindow()
    {
        TopicTitle.Text = TopicToDisplay;
    }
    
    private void GetQuestions_Click(object? sender, RoutedEventArgs e)
    {
        Topics.First(t => t.Topic == TopicToDisplay).Window.Show();
    }

    private void GetExamples_Click(object? sender, RoutedEventArgs e)
    {
        
    }

    private void OnClick_BackToHome(object? sender, RoutedEventArgs e)
    {
        new HomeWindow("").Show();
        
        this.Close();
    }
}