using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GroupProject.Models;
using GroupProject.Scripts;
using GroupProject.Views;
using MySql.Data.MySqlClient;

namespace GroupProject.ViewModels;

public partial class PickATopicPageViewModel : ViewModelBase
{
    private readonly DatabaseConnection _connectionDb = new DatabaseConnection();
    private MySqlCommand _command;
    private MySqlDataAdapter _dataAdapter;
    private DataTable _dataTable;

    [ObservableProperty] 
    private ListBoxItem _selectedItem;

    public PickATopicPageViewModel()
    {
        LoadTopics();
        
        Console.WriteLine($"Topics: {TopicsListItems.Count}");
    }

    protected ObservableCollection<TopicsListItemTemplate> TopicsListItems { get; } = new()
    {
        new TopicsListItemTemplate("Recognizing Conflicts"),   
    };
        
    private void LoadTopics()
    {
        _connectionDb.Connect();
        
        try
        {
            MySqlConnection conn = _connectionDb.connection;
            conn.Open();
            
            using (MySqlCommand command = conn.CreateCommand())
            {
                command.CommandText = "SELECT * FROM `topics` WHERE 1";
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string? moduleName = reader["ModuleName"].ToString();
                        string? topicName = reader["TopicName"].ToString();
                            
                        TopicsListItems.Add(new TopicsListItemTemplate(topicName));
                    }
                }
            }

            conn.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private void TriggerTopicClicked()
    {
        var topicName = SelectedItem.Content?.ToString();
        
        var topic = new TopicLearnSelectorPageViewModel()
        {
            CurrentTopic = topicName
        };
        
        App.MainWindow.CurrentContent = topic;
    }

    partial void OnSelectedItemChanged(ListBoxItem value)
    {
        TriggerTopicClicked();
    }
}