using System;
using GroupProject.Scripts;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;

namespace GroupProject.Services
{
    public class UserService : IUserService
    {
        private DatabaseConnection _connectionDB = new DatabaseConnection();
        private MySqlConnection _connection;

        public UserService()
        {
            _connectionDB = new DatabaseConnection();
            _connectionDB.Connect();  // Initialize the connection
        }

        public bool IsExistingUser(string username)
        {
            try
            {
                _connectionDB.Connect();  // Initialize the connection

                if (_connectionDB.connection.State != ConnectionState.Open)
                {
                    _connectionDB.connection.Open();
                }

                using (MySqlCommand commandFunc = _connectionDB.connection.CreateCommand())
                {
                    commandFunc.CommandText = "SELECT * FROM `user` WHERE `UserName` = @username";
                    commandFunc.Parameters.AddWithValue("@username", username);

                    using (MySqlDataReader reader = commandFunc.ExecuteReader())
                    {
                        return reader.HasRows;
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                throw new Exception("Error checking if user exists: " + ex.ToString());
            }
        }

        public bool IsExistingUser(string username, string email)
        {
            try
            {
                _connectionDB.Connect();  // Initialize the connection

                if (_connectionDB.connection.State != ConnectionState.Open)
                {
                    _connectionDB.connection.Open();
                }

                using (MySqlCommand commandFunc = _connectionDB.connection.CreateCommand())
                {
                    commandFunc.CommandText = "SELECT * FROM `user` WHERE `UserName` = @username OR `Email` = @email";
                    commandFunc.Parameters.AddWithValue("@username", username);
                    commandFunc.Parameters.AddWithValue("@email", email);

                    using (MySqlDataReader reader = commandFunc.ExecuteReader())
                    {
                        return reader.HasRows;
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                throw new Exception("Error checking if user exists: " + ex.ToString());
            }
        }

        public bool LogInUser(string username, string password)
        {
            try
            {
                _connectionDB.Connect();  // Initialize the connection

                if (_connectionDB.connection.State != ConnectionState.Open)
                {
                    _connectionDB.connection.Open();
                }

                using (MySqlCommand commandFunc = _connectionDB.connection.CreateCommand())
                {
                    commandFunc.CommandText = "SELECT * FROM `user` WHERE `UserName` = @username AND `Password` = @password";
                    commandFunc.Parameters.AddWithValue("@username", username);
                    commandFunc.Parameters.AddWithValue("@password", password);

                    using (MySqlDataReader reader = commandFunc.ExecuteReader())
                    {
                        return reader.HasRows;
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                throw new Exception("Error Logging In: " + ex.ToString());
            }
        }


        public void RegisterUser(string username, string email, string password)
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
                    command.CommandText = "INSERT INTO `user`(`UserName`, `Email`, `Password`) VALUES (@username, @email, @password)";
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@password", password);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                throw new Exception("Error registering user: " + ex.ToString());
            }
        }

        public void UpdateUser(string username, string email, string password)
        {
            try
            {
                _connectionDB.Connect();  // Initialize the connection

                if (_connectionDB.connection.State != ConnectionState.Open)
                {
                    _connectionDB.connection.Open();
                }

                using (MySqlCommand command = _connectionDB.connection.CreateCommand())
                {
                    string query = "UPDATE `user` SET ";
                    List<string> updates = new List<string>();

                    if (username != null)
                    {
                        updates.Add("`UserName` = @username");
                        command.Parameters.AddWithValue("@username", username);
                    }

                    if (email != null)
                    {
                        updates.Add("`Email` = @email");
                        command.Parameters.AddWithValue("@email", email);
                    }

                    if (password != null)
                    {
                        updates.Add("`Password` = @password");
                        command.Parameters.AddWithValue("@password", password);
                    }

                    query += String.Join(", ", updates);
                    query += " WHERE `UserName` = @username"; // Assuming UserName is unique

                    command.CommandText = query;
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                throw new Exception("Error updating user: " + ex.ToString());
            }
        }

    }
}