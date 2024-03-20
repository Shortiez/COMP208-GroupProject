using System;
using System.Data;
using System.Data.Common;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using GroupProject.Scripts;
using MySql.Data.MySqlClient;
using Tmds.DBus.Protocol;

namespace GroupProject.Windows;

public partial class RegisterWindow : Window
{

    dataBaseConnection connectionDB = new dataBaseConnection();
    MySqlCommand command;
    MySqlDataAdapter da;
    DataTable dt;


    public RegisterWindow()
    {
        InitializeComponent();
        connectionDB.Connect();

    }
    
private void OnRegisterPressed(object? sender, RoutedEventArgs e)
{
    Console.WriteLine("Register pressed");

    string email = Email.Text;
    string username = Username.Text;
    string password = Password.Text;

    try
    {
        using (MySqlConnection conn = connectionDB.connection)
        {
            conn.Open();
            using (MySqlCommand command = conn.CreateCommand())
            {
                // Using parameterized query to prevent SQL injection
                command.CommandText = "INSERT INTO `user`(`UserName`, `Email`, `Password`) VALUES (@username, @email, @password)";
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@email", email);
                command.Parameters.AddWithValue("@password", password);
                command.ExecuteNonQuery();
            }
        }

        var homeWindow = new HomeWindow();
        homeWindow.Show();

        this.Close();
    }
    catch (Exception ex)
    {
        // Handle exception
        Console.WriteLine(ex.Message);
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