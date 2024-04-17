using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GroupProject.Models;
using GroupProject.Scripts;
using GroupProject.Views;
using MySql.Data.MySqlClient;

namespace GroupProject.ViewModels;

class PickATopicPageItemTemplate
{
    public string TopicName { get; set; }
    public Image? TopicImage { get; set; }
    
    public PickATopicPageItemTemplate(string topicName, Image? topicImage)
    {
        TopicName = topicName;
        TopicImage = topicImage;
    }
}
    
public partial class PickATopicPageViewModel : ViewModelBase
{
    private readonly DatabaseConnection _connectionDb = new DatabaseConnection();
    private MySqlCommand _command;
    private MySqlDataAdapter _dataAdapter;
    private DataTable _dataTable;

    [ObservableProperty]
    private List<string> _moduleListItems = new List<string>();
    [ObservableProperty]
    private List<ListBoxItem> _topicListItems = new List<ListBoxItem>();

    public sealed override void Initialize()
    {
        base.Initialize();

        _connectionDb.Connect();

        LoadModules();

        foreach (var moduleTreeViewItem in ModuleListItems)
        {
            LoadTopics(moduleTreeViewItem);
        }
        
        // Hardcoded Topics
        if (_connectionDb.connection.State != ConnectionState.Open)
        {
            TopicListItems.Add(new ListBoxItem()
            {
                Content = "Binary Addition"
            });
            TopicListItems.Add(new ListBoxItem()
            {
                Content = "Binary Subtraction"
            });
            TopicListItems.Add(new ListBoxItem()
            {
                Content = "Recognizing Conflicts"
            });
            TopicListItems.Add(new ListBoxItem()
            {
                Content = "Combinatorics"
            });
            TopicListItems.Add(new ListBoxItem()
            {
                Content = "Table Unions"
            });
            foreach (var item in TopicListItems)
            {
                item.DoubleTapped += TriggerTopicClicked;
            }
        }
    }

    private void LoadModules()
    {
        _connectionDb.Connect();
        
        try
        {
            MySqlConnection conn = _connectionDb.connection;
            conn.Open();
            
            using (MySqlCommand command = conn.CreateCommand())
            {
                command.CommandText = "SELECT * FROM `modules` WHERE 1";
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string? moduleName = reader["ModuleName"].ToString();
                        
                        ModuleListItems.Add(moduleName);
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
    private void LoadTopics(string module)
    {
        _connectionDb.Connect();
        
        try
        {
            MySqlConnection conn = _connectionDb.connection;
            conn.Open();
            
            using (MySqlCommand command = conn.CreateCommand())
            {
                command.CommandText = "SELECT * FROM `topics` WHERE ModuleName = @ModuleName";
                command.Parameters.AddWithValue("@ModuleName", module);
                
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string? topicName = reader["TopicName"].ToString();
                        var item = new ListBoxItem()
                        {
                            Content = topicName
                        };
                        item.DoubleTapped += TriggerTopicClicked;
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

    private void TriggerTopicClicked(object? sender = null, TappedEventArgs? e = null)
    {
        if (sender is not ListBoxItem listBoxItem) return;
        var topicName = listBoxItem.Content?.ToString();
        Console.WriteLine(topicName);
        
        var topic = new TopicLearnSelectorPageViewModel()
        {
            CurrentTopic = topicName
        };
        
        App.MainWindowViewModel.ChangeContent(topic);
    }
}