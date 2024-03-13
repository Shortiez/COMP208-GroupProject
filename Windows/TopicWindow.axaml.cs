using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace GroupProject.Windows;

public partial class TopicWindow : Window
{
    public string? TopicToDisplay { get; set; }

    public TopicWindow()
    {
        InitializeComponent();
    }

}