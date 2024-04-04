using System;
using System.Collections.ObjectModel;
using Avalonia.Controls;
using GroupProject.Scripts;
using MySql.Data.MySqlClient;

namespace GroupProject.Services;

public class StatisticsService
{
    private DatabaseConnection _connectionDb = new DatabaseConnection();

    public ObservableCollection<TreeViewItem> LoadModules()
    {
        var modules = new ObservableCollection<TreeViewItem>();

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
                        var moduleTreeViewItem = new TreeViewItem()
                        {
                            Header = moduleName
                        };

                        modules.Add(moduleTreeViewItem);
                    }
                }
            }

            conn.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return modules;
    }

    public ObservableCollection<TreeViewItem> LoadTopics(TreeViewItem module)
    {
        var topics = new ObservableCollection<TreeViewItem>();

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

                        // Topics are added to the corresponding module
                        var item = new TreeViewItem
                        {
                            Header = topicName
                        };

                        module.Items.Add(item);
                        topics.Add(item);
                    }
                }
            }

            conn.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return topics;
    }
}