using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;

namespace GroupProject.Windows;

public partial class LoginWindow : Window
{
    protected string Username { get; private set; }
    protected string Password { get; private set; }

    public LoginWindow()
    {
        InitializeComponent();
    }

    private void OnLoginPressed(object? sender, RoutedEventArgs e)
    {
        Console.WriteLine("Login pressed");

        if (UsernameTextBox.Text != null)
            Username = UsernameTextBox.Text;
        if (PasswordTextBox.Text != null) 
            Password = PasswordTextBox.Text;
    }
    
    private void OnRegisterPressed(object? sender, RoutedEventArgs e)
    {
        Console.WriteLine("Register pressed");
        
        var registerWindow = new RegisterWindow();
        registerWindow.Show();
        
        this.Close();
    }
}