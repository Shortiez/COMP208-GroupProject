using Avalonia.Controls;
using Avalonia.Input;
using CommunityToolkit.Mvvm.Input;
using GroupProject.Views;

namespace GroupProject.Models;

public class TopicsListItemTemplate
{
    public string? ModuleName { get; set; }
    public string? TopicName { get; set; }
    
    public TopicsListItemTemplate(string? topicName)
    {
        ModuleName = "";
        TopicName = topicName;
    }
}