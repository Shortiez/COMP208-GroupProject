using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using GroupProject.Scripts;
using MySql.Data.MySqlClient;
 
using System.Text.RegularExpressions;
using Avalonia;
using GroupProject.ViewModels;

namespace GroupProject.Views
{
    public partial class RegisterPageView : UserControl
    {
        DatabaseConnection connectionDB = new DatabaseConnection();

        public RegisterPageView()
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
            DatabaseConnection connectionDB = new DatabaseConnection();
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
            password = Hashes.Sha256(password);
            ErrorMessage.Text = "";
            ErrorMessage.FontSize = 12;
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
                                
                                OnRegister();
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    } 
                    else 
                    {
                        ErrorMessage.Text = "";
                        ErrorMessage.IsVisible = true;
                        ErrorMessage.Text += "There already is a user with the \nsame credentials";
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
                {
                    Username.BorderThickness = new Avalonia.Thickness(1);
                    ErrorMessage.IsVisible = true;
                    ErrorMessage.Text += "Username Must be at least 6 characters long\n";
                }
                else
                    Username.BorderThickness = new Avalonia.Thickness(0);

                if (!IsValid(password))
                {
                    Password.BorderThickness = new Avalonia.Thickness(1);
                    ErrorMessage.IsVisible = true;
                    ErrorMessage.Text += "Password Must be at least 6 characters long\n";
                }
                else
                    Password.BorderThickness = new Avalonia.Thickness(0);

                if (!IsValidEmail(email))
                {
                    Email.BorderThickness = new Avalonia.Thickness(1);
                    ErrorMessage.IsVisible = true;
                    ErrorMessage.Text += "A valid email must be used\n";
                }
                else
                    Email.BorderThickness = new Avalonia.Thickness(0);
            }
        }

        private void OnRegister()
        {
            var mainWindowViewModel = (MainWindowViewModel)Application.Current?.DataContext!;
            mainWindowViewModel.CurrentContent = new MainContentPageViewModel();
        }

        private void OnLoginPressed(object? sender, RoutedEventArgs e)
        {
            Console.WriteLine("Login pressed");

            ShowLoginPage();
        }

        private void ShowLoginPage()
        {
            var mainWindowViewModel = (MainWindowViewModel)Application.Current?.DataContext!;
            mainWindowViewModel.CurrentContent = new LoginPageViewModel();
        }
    }
}
