using System;
using System.Data;
using System.Data.Common;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using ExCSS;
using GroupProject.Scripts;
using MySql.Data.MySqlClient;
using Mysqlx;
using Org.BouncyCastle.Asn1.Cmp;
using Tmds.DBus.Protocol;
using System.Text.RegularExpressions;
namespace GroupProject.Windows
{
    public partial class RegisterWindow : Window
    {
        dataBaseConnection connectionDB = new dataBaseConnection();

        public RegisterWindow()
        {
            InitializeComponent();
            connectionDB.Connect();
        }

        public static bool IsValidEmail(string email)
        {
            if (email != null)
            {
                string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
                return Regex.IsMatch(email, pattern);
            }
            return false;
        }
    

        // New functions for validation
        private bool IsValid(string str)
        {
            return str != null && str.Length > 6;
        }

        private bool checkExistingUser(string signInUsername, string signInEmail)
        {
            dataBaseConnection connectionDB = new dataBaseConnection();
            connectionDB.Connect();

            if (signInUsername != null && signInEmail != null){
                try{
                    using (MySqlConnection conn = connectionDB.connection)
                    {
                        if(conn != null) {
                            conn.Open();
                            using (MySqlCommand commandFunc = conn.CreateCommand())
                            {
                                // Using parameterized query to prevent SQL injection
                                commandFunc.CommandText = "SELECT * FROM `user` WHERE `UserName` = @username OR `Email` = @email";
                                commandFunc.Parameters.AddWithValue("@username", signInUsername);
                                commandFunc.Parameters.AddWithValue("@email", signInEmail);
                                using (MySqlDataReader reader = commandFunc.ExecuteReader())
                                {
                                    if (reader.HasRows)
                                    {
                                        Console.WriteLine("Already Existing User");
                                        return false;
                                    }
                                    else
                                    {
                                        return true;
                                    }
                                }
                            }
                        } else {
                            Console.WriteLine("Connection is null");
                            return false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            } 
            else 
            {
                Console.WriteLine("Null Username or Email");
                return false;
            }
    }

        private void OnRegisterPressed(object? sender, RoutedEventArgs e)
        {
            Console.WriteLine("Register pressed");

            string email = Email.Text;
            string username = Username.Text;
            string password = Password.Text;

            if (IsValid(username) && IsValid(password))
            {
                if (IsValidEmail(email))
                {

                    if (checkExistingUser(username, email)){
                        try
                        {
                            using (MySqlConnection conn = connectionDB.connection)
                            {
                                conn.Open();
                                using (MySqlCommand command = conn.CreateCommand())
                                {
                                    command.CommandText = "INSERT INTO `user`(`UserName`, `Email`, `Password`) VALUES (@username, @email, @password)";
                                    command.Parameters.AddWithValue("@username", username);
                                    command.Parameters.AddWithValue("@email", email);
                                    command.Parameters.AddWithValue("@password", password);
                                    command.ExecuteNonQuery();
                                }
                                conn.Close();
                                var loginWindow = new LoginWindow();
                                loginWindow.Show();

                                this.Close();
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    } 
                    else 
                    {
                         //Set TextBlock Text to Already existing user and isVisible = true
                    }

                }  
                else
                {
                    Email.BorderThickness = new Avalonia.Thickness(1);
                }
            }
            else
            {
                if (!IsValid(username))
                    Username.BorderThickness = new Avalonia.Thickness(1);
                else
                    Username.BorderThickness = new Avalonia.Thickness(0);

                if (!IsValid(password))
                    Password.BorderThickness = new Avalonia.Thickness(1);
                else
                    Password.BorderThickness = new Avalonia.Thickness(0);

                if (!IsValidEmail(email))
                    Email.BorderThickness = new Avalonia.Thickness(1);
                else
                    Email.BorderThickness = new Avalonia.Thickness(0);
            }
        }

        private void OnLoginPressed(object? sender, RoutedEventArgs e)
        {
            Console.WriteLine("Login pressed");

            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();

            this.Close();
        }
    }
}
