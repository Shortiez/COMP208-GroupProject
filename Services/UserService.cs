using System;
using GroupProject.Scripts;
using MySql.Data.MySqlClient;

namespace GroupProject.Services
{
    public class UserService : IUserService
    {
        private DatabaseConnection _connectionDB = new DatabaseConnection();
        private MySqlConnection _connection;

        public bool IsExistingUser(string username)
        {
            try
            {
                using (_connection = _connectionDB.connection)
                {
                    _connection.Open();

                    using (MySqlCommand commandFunc = _connection.CreateCommand())
                    {
                        commandFunc.CommandText = "SELECT * FROM `user` WHERE `UserName` = @username";
                        commandFunc.Parameters.AddWithValue("@username", username);

                        using (MySqlDataReader reader = commandFunc.ExecuteReader())
                        {
                            return reader.HasRows;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                throw new Exception("Error checking if user exists: " + ex.Message);
            }
        }
        public bool IsExistingUser(string username, string email)
        {
            try
            {
                using (_connection = _connectionDB.connection)
                {
                    _connection.Open();

                    using (MySqlCommand commandFunc = _connection.CreateCommand())
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
            }
            catch (Exception ex)
            {
                // Handle exception
                throw new Exception("Error checking if user exists: " + ex.Message);
            }
        }

        public void RegisterUser(string username, string email, string password)
        {
            try
            {
                using (_connection = _connectionDB.connection)
                {
                    _connection.Open();

                    using (MySqlCommand command = _connection.CreateCommand())
                    {
                        command.CommandText = "INSERT INTO `user`(`UserName`, `Email`, `Password`) VALUES (@username, @email, @password)";
                        command.Parameters.AddWithValue("@username", username);
                        command.Parameters.AddWithValue("@email", email);
                        command.Parameters.AddWithValue("@password", password);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                throw new Exception("Error registering user: " + ex.Message);
            }
        }
    }
}