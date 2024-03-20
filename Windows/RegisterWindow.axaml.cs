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

public class EmailValidator
{
    public static bool IsValidEmail(string email)
    {
        // Regular expression for basic email format validation
        string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        
        // Check if email matches the pattern
        return Regex.IsMatch(email, pattern);
    }
}

private void OnRegisterPressed(object? sender, RoutedEventArgs e)
{
    Console.WriteLine("Register pressed");

    string email = Email.Text;
    string username = Username.Text;
    string password = Password.Text;

    if (username != null && password != null){
        if (EmailValidator.IsValidEmail(email))
        {
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
                    // Handle exception
                    Console.WriteLine(ex.Message);
                }
            } 
            else 
            {
                if (username.Length <= 6)
                    Username.BorderThickness = new Avalonia.Thickness(1);
                else
                    Username.BorderThickness = new Avalonia.Thickness(0); // Reset to default thickness

                if (password.Length <= 6)
                    Password.BorderThickness = new Avalonia.Thickness(1);
                else
                    Password.BorderThickness = new Avalonia.Thickness(0); // Reset to default thickness
        
            }
        } 
        else 
        {
            Email.BorderThickness = new Avalonia.Thickness(1);
        }
    }   else {
            if (username == null)
                Username.BorderThickness = new Avalonia.Thickness(1);
            else
                Username.BorderThickness = new Avalonia.Thickness(0); // Reset to default thickness

            if (password == null)
                Password.BorderThickness = new Avalonia.Thickness(1);
            else
                Password.BorderThickness = new Avalonia.Thickness(0); // Reset to default thickness
       
            if (email == null)
                Email.BorderThickness = new Avalonia.Thickness(1);
            else
                Email.BorderThickness = new Avalonia.Thickness(0); // Reset to default thickness
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