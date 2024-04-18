using System;
using GroupProject.Scripts;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using Avalonia.Controls;
using GroupProject.ViewModels;

namespace GroupProject.Models
{
    public class UserStatisticData
    {
        private string Username;
        private string ModuleName;
        private string TopicName;
        private int NoCorrect;
        private int NoWrong;

        private DatabaseConnection _connectionDB = new DatabaseConnection();
        private MySqlConnection _connection;
        /* 
        Create an instance as follows, make sure to replace the "" with respective module and topic names. If you come across any issues resolve them with quick fix.
        private UserStatisticData _userStatistics = new UserStatisticData(App.MainWindowViewModel.User.Username, "", "");
        */
        public UserStatisticData(string username, string modulename, string topicname)
        {

            Username = username;
            ModuleName = modulename;
            TopicName = topicname;

            _connectionDB = new DatabaseConnection();
            _connectionDB.Connect();
            Console.WriteLine(username);
            
        }

        public bool IsCreated()
        {
            return Username != null && ModuleName != null && TopicName != null;
        }
        /* 
        Only call this where you determine the result and do not call any other functions within this file
        */
        public void UpdateExistingRecord(int nocorrect, int nowrong)
        {
            if (IsCreated()){
                try
                {
                    _connectionDB.Connect();

                    if (_connectionDB.connection.State != ConnectionState.Open)
                    {
                        _connectionDB.connection.Open();
                    }
                    
                    if (CheckExistingRecord(Username, ModuleName, TopicName))
                    {
                        using (MySqlCommand command = _connectionDB.connection.CreateCommand())
                        {

                            nocorrect += RetrieveNoCorrect(Username, ModuleName, TopicName);
                            nowrong += RetrieveNoWrong(Username, ModuleName, TopicName);

                            command.CommandText = "UPDATE `results` SET `noCorrect` = @noCorrect, `noWrong` = @noWrong WHERE `UserName` = @username AND `ModuleName` = @modulename AND `TopicName` = @topicname";

                            command.Parameters.AddWithValue("@username", Username);
                            command.Parameters.AddWithValue("@modulename", ModuleName);
                            command.Parameters.AddWithValue("@topicname", TopicName);
                            command.Parameters.AddWithValue("@noCorrect", nocorrect);
                            command.Parameters.AddWithValue("@noWrong", nowrong);

                            command.ExecuteNonQuery();
                        }
                    } 
                    else 
                    {
                        CreateRecord(Username, ModuleName, TopicName, nocorrect, nowrong);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error updating records: " + ex.ToString());
                }
            }
        }

        public void UpdateExistingRecord(int nocorrect, int nowrong, string moduleName, string topicName)
        {
            if (IsCreated()){
                try
                {
                    _connectionDB.Connect();

                    if (_connectionDB.connection.State != ConnectionState.Open)
                    {
                        _connectionDB.connection.Open();
                    }

                    if (CheckExistingRecord(Username, moduleName, topicName))
                    {
                        using (MySqlCommand command = _connectionDB.connection.CreateCommand())
                        {

                            nocorrect += RetrieveNoCorrect(Username, moduleName, topicName);
                            nowrong += RetrieveNoWrong(Username, moduleName, topicName);

                            command.CommandText =
                                "UPDATE `results` SET `noCorrect` = @noCorrect, `noWrong` = @noWrong WHERE `UserName` = @username AND `ModuleName` = @modulename AND `TopicName` = @topicname";

                            command.Parameters.AddWithValue("@username", Username);
                            command.Parameters.AddWithValue("@modulename", moduleName);
                            command.Parameters.AddWithValue("@topicname", topicName);
                            command.Parameters.AddWithValue("@noCorrect", nocorrect);
                            command.Parameters.AddWithValue("@noWrong", nowrong);

                            command.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        CreateRecord(Username, moduleName, topicName, nocorrect, nowrong);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error updating records: " + ex.ToString());
                }
            }
        }
        
        public ObservableCollection<TopicStatsModelTemplate> RetrieveUserStatistics()
        {
            if (IsCreated()){
                try
                {
                    _connectionDB.Connect();

                    if (_connectionDB.connection.State != ConnectionState.Open)
                    {
                        _connectionDB.connection.Open();
                    }

                    ObservableCollection<TopicStatsModelTemplate> topicStats = new ObservableCollection<TopicStatsModelTemplate>();

                    using (MySqlCommand command = _connectionDB.connection.CreateCommand())
                    {
                        command.CommandText = "SELECT * FROM `results` WHERE `UserName` = @username";
                        command.Parameters.AddWithValue("@username", Username);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string topicName = reader.GetString("TopicName");
                                
                                int noCorrect = reader.GetInt32("NoCorrect");
                                int noWrong = reader.GetInt32("NoWrong");
                                
                                int totalQuestions = noCorrect + noWrong;
                                
                                TopicStatsModelTemplate topic = new TopicStatsModelTemplate(topicName, totalQuestions, 
                                    noCorrect, noWrong);

                                topicStats.Add(topic);
                            }
                        }
                    }

                    return topicStats;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error retrieving user statistics: " + ex.ToString());
                }
            }
            return null;
        }
        
        private bool CheckExistingRecord(string username, string modulename, string topicname)
        {
            if (IsCreated()){
                try
                {
                    _connectionDB.Connect();

                    if (_connectionDB.connection.State != ConnectionState.Open)
                    {
                        _connectionDB.connection.Open();
                    }

                    using (MySqlCommand command = _connectionDB.connection.CreateCommand())
                    {
                        command.CommandText = "SELECT * FROM `results` WHERE `UserName` = @username AND `ModuleName` = @modulename AND `TopicName` = @topicname";
                        command.Parameters.AddWithValue("@username", username);
                        command.Parameters.AddWithValue("@modulename", modulename);
                        command.Parameters.AddWithValue("@topicname", topicname);


                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            return reader.HasRows;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error checking if user exists: " + ex.ToString());
                }
            }
            return false;
        }

        private void CreateRecord(string username, string modulename, string topicname, int nocorrect, int nowrong)
        {
            if (IsCreated()){
                try
                {
                    _connectionDB.Connect();

                    if (_connectionDB.connection.State != ConnectionState.Open)
                    {
                        _connectionDB.connection.Open();
                    }

                    using (MySqlCommand command = _connectionDB.connection.CreateCommand())
                    {
                        command.CommandText = "INSERT INTO `results`(`Username`, `ModuleName`, `TopicName`, `noCorrect`, `noWrong`) VALUES (@username, @modulename, @topicname, @nocorrect, @nowrong)";
                        command.Parameters.AddWithValue("@username", username);
                        command.Parameters.AddWithValue("@modulename", modulename);
                        command.Parameters.AddWithValue("@topicname", topicname);
                        command.Parameters.AddWithValue("@nocorrect", nocorrect);
                        command.Parameters.AddWithValue("@nowrong", nowrong);
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error registering user: " + ex.ToString());
                }
            }
        }

        public int RetrieveNoCorrect(string username, string modulename, string topicname)
        {
            if (IsCreated()){
                try
                {
                    _connectionDB.Connect();

                    if (_connectionDB.connection.State != ConnectionState.Open)
                    {
                        _connectionDB.connection.Open();
                    }

                    using (MySqlCommand commandFunc = _connectionDB.connection.CreateCommand())
                    {
                        commandFunc.CommandText = "SELECT `NoCorrect` FROM `results` WHERE `UserName` = @username AND `ModuleName` = @modulename AND `TopicName` = @topicname";
                        commandFunc.Parameters.AddWithValue("@username", username);
                        commandFunc.Parameters.AddWithValue("@modulename", modulename);
                        commandFunc.Parameters.AddWithValue("@topicname", topicname);

                        using (MySqlDataReader reader = commandFunc.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return reader.GetInt32("NoCorrect");
                            }
                            else
                            {
                                return 0;
                                throw new Exception("No record found");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error retrieving NoCorrect: " + ex.ToString());
                }
            }
            return 0;
        }
        public int RetrieveNoWrong(string username, string modulename, string topicname)
        {
            if (IsCreated()){
                try
                {
                    _connectionDB.Connect();

                    if (_connectionDB.connection.State != ConnectionState.Open)
                    {
                        _connectionDB.connection.Open();
                    }

                    using (MySqlCommand commandFunc = _connectionDB.connection.CreateCommand())
                    {
                        commandFunc.CommandText = "SELECT `NoWrong` FROM `results` WHERE `UserName` = @username AND `ModuleName` = @modulename AND `TopicName` = @topicname";
                        commandFunc.Parameters.AddWithValue("@username", username);
                        commandFunc.Parameters.AddWithValue("@modulename", modulename);
                        commandFunc.Parameters.AddWithValue("@topicname", topicname);

                        using (MySqlDataReader reader = commandFunc.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return reader.GetInt32("NoWrong");
                            }
                            else
                            {
                                return 0;
                                throw new Exception("No record found");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error retrieving NoWrong: " + ex.ToString());
                }
            }
            return 0;
        }

    }
}
