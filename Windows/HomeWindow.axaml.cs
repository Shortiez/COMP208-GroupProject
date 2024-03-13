using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;

namespace GroupProject.Windows;

public partial class HomeWindow : Window
{
    public HomeWindow()
    {
        InitializeComponent();
    }

    private void OnTopicClicked(object? sender, KeyEventArgs e)
    {
        var item = (TreeViewItem)sender!;
        var topic = item?.Header?.ToString();
        
        var window = new TopicWindow
        {
            TopicToDisplay = topic
        };

        window.Show();
        
        e.Handled = true;
        
        this.Close();
    }
}