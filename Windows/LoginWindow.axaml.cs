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

namespace GroupProject.Windows;

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

    private void OnLoginPressed(object? sender, RoutedEventArgs e)
    {
        Console.WriteLine("Login pressed");

        string username = UsernameTextBox.Text;
        string password = PasswordTextBox.Text;

        if (username != null && password != null){
            if (username.Length > 6 && password.Length > 6)
            {
                try
                {
                    using (MySqlConnection conn = connectionDB.connection)
                    {
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
                                    // No rows returned by the query
                                    Console.WriteLine("No user found with the provided credentials.");
                                }
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    // Handle exception
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                if (username.Length <= 6)
                    UsernameTextBox.BorderThickness = new Avalonia.Thickness(1);
                else
                    UsernameTextBox.BorderThickness = new Avalonia.Thickness(0); // Reset to default thickness

                if (password.Length <= 6)
                    PasswordTextBox.BorderThickness = new Avalonia.Thickness(1);
                else
                    PasswordTextBox.BorderThickness = new Avalonia.Thickness(0); // Reset to default thickness

            }
        } else 
        {
            if (username == null)
                UsernameTextBox.BorderThickness = new Avalonia.Thickness(1);
            else
                UsernameTextBox.BorderThickness = new Avalonia.Thickness(0); // Reset to default thickness

            if (password == null)
                PasswordTextBox.BorderThickness = new Avalonia.Thickness(1);
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
}