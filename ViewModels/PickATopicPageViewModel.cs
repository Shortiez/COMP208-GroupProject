using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
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

        foreach (var moduleTreeViewItem in ModuleListItems)
        {
            LoadTopics(moduleTreeViewItem);
        }

        if (_connectionDb.connection == null)
        {
            // Hardcoded Topics
            ModuleListItems.Add(new TreeViewItem()
            {
                Header = "No Database"
            });

            TopicListItems.Add(new TreeViewItem()
            {
                Header = "Binary Addition"
            });
            TopicListItems.Add(new TreeViewItem()
            {
                Header = "Binary Subtraction"
            });
            TopicListItems.Add(new TreeViewItem()
            {
                Header = "Recognizing Conflicts"
            });
            TopicListItems.Add(new TreeViewItem()
            {
                Header = "Combinatorics"
            });
            TopicListItems.Add(new TreeViewItem()
            {
                Header = "Table Unions"
            });
            foreach (var item in TopicListItems)
            {
                item.DoubleTapped += TriggerTopicClicked;

                ModuleListItems.FirstOrDefault(x => x.Header.ToString() == "No Database")?.Items.Add(item);
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
        
        App.MainWindowViewModel.CurrentContent = topic;
    }
}