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
    private List<TreeViewItem> _moduleListItems = new List<TreeViewItem>();
    [ObservableProperty]
    private List<TreeViewItem> _topicListItems = new List<TreeViewItem>();
    
    public sealed override void Initialize()
    {
        base.Initialize();
        
        LoadModules();

        for (int i = 0; i < ModuleListItems.Count; i++)
        {
            LoadTopics(ModuleListItems[i]);
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
                        ModuleListItems.Add(new TreeViewItem()
                        {
                            Header = moduleName
                        });
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
    
    private void LoadTopics(TreeViewItem module)
    {
        _connectionDb.Connect();
        
        try
        {
            MySqlConnection conn = _connectionDb.connection;
            conn.Open();
            
            using (MySqlCommand command = conn.CreateCommand())
            {
                string moduleName = module.Header.ToString();
                
                command.CommandText = "SELECT * FROM `topics` WHERE ModuleName = @ModuleName";
                command.Parameters.AddWithValue("@ModuleName", moduleName);
                
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string? topicName = reader["TopicName"].ToString();
                        var item = new TreeViewItem
                        {
                            Header = topicName
                        };
                        item.DoubleTapped += TriggerTopicClicked;
                        
                        TopicListItems.Add(item);
                        module.Items.Add(item);
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
        if (sender is not TreeViewItem treeViewItem) return;
        
        var topicName = treeViewItem.Header?.ToString();
        Console.WriteLine(topicName);
        
        var topic = new TopicLearnSelectorPageViewModel()
        {
            CurrentTopic = topicName
        };
        
        App.MainWindow.CurrentContent = topic;
    }
}