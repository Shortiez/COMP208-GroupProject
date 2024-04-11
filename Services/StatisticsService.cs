using System;
using System.Collections.ObjectModel;
using Avalonia.Controls;
using GroupProject.Models;
using GroupProject.Scripts;
using MySql.Data.MySqlClient;

namespace GroupProject.Services;

public class StatisticsService
{
    private DatabaseConnection _connectionDb = new DatabaseConnection();

    public ObservableCollection<ModuleStatisticsModel> LoadModules()
    {
        var modules = new ObservableCollection<ModuleStatisticsModel>();

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
                        
                        // Modules are added to the list
                        var item = new ModuleStatisticsModel(moduleName);
                        modules.Add(item);
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

    public ObservableCollection<TopicStatisticsModel> LoadTopics(ModuleStatisticsModel module)
    {
        var topics = new ObservableCollection<TopicStatisticsModel>();

        _connectionDb.Connect();

        try
        {
            MySqlConnection conn = _connectionDb.connection;
            conn.Open();

            using (MySqlCommand command = conn.CreateCommand())
            {
                command.CommandText = "SELECT * FROM `topics` WHERE ModuleName = @ModuleName";
                command.Parameters.AddWithValue("@ModuleName", module.ModuleName);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string? topicName = reader["TopicName"].ToString();
                        
                        // Topics are added to the list
                        UserStatisticData userStatisticData = App.MainWindowViewModel.User.UserStats;
                        var username = App.MainWindowViewModel.User.Username;
                        int noCorrect = userStatisticData.RetrieveNoCorrect(username,
                            module.ModuleName, 
                            topicName);
                        int noWrong = userStatisticData.RetrieveNoWrong(username,
                            module.ModuleName, 
                            topicName);
                        
                        var item = new TopicStatisticsModel(topicName, noCorrect, noWrong);
                        topics.Add(item);
                        
                        module.Topics.Add(item);
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