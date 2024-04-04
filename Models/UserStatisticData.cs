using System;
using GroupProject.Scripts;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using Avalonia.Controls;

namespace GroupProject.Models
{
    public class UserStatisticData
    {
        private String Username;
        private String ModuleName;
        private String TopicName;
        private int NoCorrect;
        private int NoWrong;

        private DatabaseConnection _connectionDB = new DatabaseConnection();
        private MySqlConnection _connection;
        
        public UserStatisticData(string username, string modulename, string topicname, int nocorrect, int nowrong)
        {
            Username = username;
            ModuleName = modulename;
            TopicName = topicname;
            NoCorrect = nocorrect;
            NoWrong = nowrong;

            _connectionDB = new DatabaseConnection();
            _connectionDB.Connect();
        }

        public bool IsCreated()
        {
            return Username != null && ModuleName != null && TopicName != null;
        }
        // This is the function to be called
        public void UpdateExistingRecord(string username, string modulename, string topicname, int nocorrect, int nowrong)
        {
            try
            {
                _connectionDB.Connect();

                if (_connectionDB.connection.State != ConnectionState.Open)
                {
                    _connectionDB.connection.Open();
                }
                
                if (CheckExistingRecord(username, modulename, topicname))
                {
                    using (MySqlCommand command = _connectionDB.connection.CreateCommand())
                    {

                        nocorrect += RetrieveNoCorrect(username, modulename, topicname);
                        nowrong += RetrieveNoWrong(username, modulename, topicname);

                        command.CommandText = "UPDATE `results` SET `noCorrect` = @noCorrect, `noWrong` = @noWrong WHERE `UserName` = @username AND `ModuleName` = @modulename AND `TopicName` = @topicname";

                        command.Parameters.AddWithValue("@username", username);
                        command.Parameters.AddWithValue("@modulename", modulename);
                        command.Parameters.AddWithValue("@topicname", topicname);
                        command.Parameters.AddWithValue("@noCorrect", nocorrect);
                        command.Parameters.AddWithValue("@noWrong", nowrong);

                        command.ExecuteNonQuery();
                    }
                } 
                else 
                {
                    CreateRecord(username, modulename, topicname, nocorrect, nowrong);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating records: " + ex.ToString());
            }
        }


        public bool CheckExistingRecord(string username, string modulename, string topicname)
        {
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

        public void CreateRecord(string username, string modulename, string topicname, int nocorrect, int nowrong)
        {
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

        public int RetrieveNoCorrect(string username, string modulename, string topicname)
        {
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
        public int RetrieveNoWrong(string username, string modulename, string topicname)
        {
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

        private void LoadTopics(TreeViewItem results, string username, string modulename, string topicname)
        {
            _connectionDB.Connect();
            
            try
            {
                MySqlConnection conn = _connectionDB.connection;
                conn.Open();
                
                using (MySqlCommand command = conn.CreateCommand())
                {
                    string moduleName = results.Header.ToString();
                    
                    command.CommandText = "SELECT * FROM `results` WHERE `UserName` = @username AND `ModuleName` = @modulename AND `TopicName` = @topicname";
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@modulename", modulename);
                    command.Parameters.AddWithValue("@topicname", topicname);
                    
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string? topicName = reader["TopicName"].ToString();
                            var item = new TreeViewItem
                            {
                                Header = topicName
                            };
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
    }
}
