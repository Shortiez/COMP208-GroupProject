using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Avalonia.Controls;
using GroupProject.Scripts.Questions;
using GroupProject.Models;
using System;

namespace GroupProject.ViewModels;

public partial class CombinatoricsLearnPageViewModel : ViewModelBase
{
    [RelayCommand]
    private void ClickBackToHome()
    {
        var topicName = "Combinatorics";
        Console.WriteLine(topicName);

        var topic = new TopicLearnSelectorPageViewModel()
        {
            CurrentTopic = topicName
        };

        App.MainWindowViewModel.ChangeContent(topic);
    }
}