using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace GroupProject.Windows;

public partial class RegisterWindow : Window
{
    public RegisterWindow()
    {
        InitializeComponent();
    }
    
    private void OnRegisterPressed(object? sender, RoutedEventArgs e)
    {
        Console.WriteLine("Register pressed");
    }

    private void OnLoginPressed(object? sender, RoutedEventArgs e)
    {
        Console.WriteLine("Login pressed");
        
        LoginWindow loginWindow = new LoginWindow();
        loginWindow.Show();
        
        this.Close();
    }
}