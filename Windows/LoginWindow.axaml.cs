using System;
using System.Data;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using MySql.Data.MySqlClient;
using GroupProject.Scripts;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Security.Cryptography;

namespace GroupProject.Windows
{
    public partial class LoginWindow : Window
    {
        dataBaseConnection connectionDB = new dataBaseConnection();
        MySqlCommand command;
        MySqlDataAdapter da;
        DataTable dt;

        protected string Username { get; private set; }
        protected string Password { get; private set; }

        public LoginWindow()
        {
            InitializeComponent();
            connectionDB.Connect();
        }

        private bool IsValid(string str)
        {
            return str != null && str.Length > 6;
        }

        private void OnLoginPressed(object? sender, RoutedEventArgs e)
        {
            Console.WriteLine("Login pressed");

            string username = UsernameTextBox.Text;
            string password = PasswordTextBox.Text;
            password = Hashes.Sha256(password);


            ErrorMessage.Text = "";
            ErrorMessage.FontSize = 12;

            if (IsValid(username) && IsValid(password))
            {
                try
                {
                    MySqlConnection conn = connectionDB.connection;
                    conn.Open();
                    using (MySqlCommand command = conn.CreateCommand())
                    {
                        // Using parameterized query to prevent SQL injection
                        command.CommandText = "SELECT * FROM `user` WHERE `UserName` = @username AND `Password` = @password";
                        command.Parameters.AddWithValue("@username", username);
                        command.Parameters.AddWithValue("@password", password);
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                var homeWindow = new HomeWindow(username);
                                homeWindow.Show();
                                this.Close();
                            }
                            else
                            {
                                ErrorMessage.Text += ("Wrong username or Password");
                                Console.WriteLine("No user found with the provided credentials.");
                            }
                        }
                    }
                    conn.Close();
                }
                catch (Exception ex)
                {
                    // Handle exception
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                if (!IsValid(username))
                {
                    UsernameTextBox.BorderThickness = new Avalonia.Thickness(1);
                    ErrorMessage.IsVisible = true;
                    ErrorMessage.Text += "Username Must be at least 6 characters long\\n";
                }
                else
                    UsernameTextBox.BorderThickness = new Avalonia.Thickness(0); // Reset to default thickness

                if (!IsValid(password))
                {
                    PasswordTextBox.BorderThickness = new Avalonia.Thickness(1);
                    ErrorMessage.IsVisible = true;
                    ErrorMessage.Text += "Password Must be at least 6 characters long\\n";
                }
                else
                    PasswordTextBox.BorderThickness = new Avalonia.Thickness(0); // Reset to default thickness
            }
        }

        private void OnRegisterPressed(object? sender, RoutedEventArgs e)
        {
            Console.WriteLine("Register pressed");

            var registerWindow = new RegisterWindow();
            registerWindow.Show();

            this.Close();
        }

        private void OnLoginAsGuestPressed(object? sender, RoutedEventArgs e)
        {
            new HomeWindow("Guest").Show();
            this.Close();
        }
    }
}