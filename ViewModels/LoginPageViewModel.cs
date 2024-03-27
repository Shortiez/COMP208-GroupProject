using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GroupProject.Scripts;
using GroupProject.Windows;

namespace GroupProject.ViewModels;

struct LoginPage
{
    private string Username;
    private string Password;
    public Window Window { get; set; }

}

public partial class LoginPageViewModel : ViewModelBase, INotifyPropertyChanged
{
    [ObservableProperty]
    private ViewModelBase _currentPage = new HomePageViewModel();

}