using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using GroupProject.VectorGraphicsAndAnimation;
using VectSharp;
using Point = VectSharp.Point;

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
    
    private void Button_OnClick_LoadQuestions(object? sender, RoutedEventArgs e)
    {
        Topics.First(t => t.Topic == TopicToDisplay).Window.Show();
    }

    private void Button_OnClick_LoadExamples(object? sender, RoutedEventArgs e)
    {
        
    }

    private void Button_OnClick_BackToHome(object? sender, RoutedEventArgs e)
    {
        new HomeWindow(" ").Show();
        
        this.Close();
    }
}