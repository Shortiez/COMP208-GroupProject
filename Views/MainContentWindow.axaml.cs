using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using System;
using System.Data;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MySql.Data.MySqlClient;
using GroupProject.Scripts;

namespace GroupProject.Windows
{
    struct ContentPage
    {
        public string Title { get; set; }
        public Window Window { get; set; }
    }
    
    public partial class MainContentWindow : Window
    {
        public string Username { get; private set; }
        public Window CurrentContentWindow { get; private set; }
        
        private readonly ContentPage[] _contentPageMapping = new ContentPage[]
        {
            new ContentPage
            {
                Title = "Home", Window = new PickATopicWindow()
            },
            new ContentPage
            {
                Title = "Study", Window = new PickATopicWindow()
            },
            new ContentPage
            {
                Title = "Settings", Window = new SettingsWindow()
            },
            new ContentPage
            {
                Title = "Account", Window = new AccountWindow()
            }
        };

        private ObservableCollection<SidebarListItem> _sidebarListItems { get; } = new()
        { 
            new SidebarListItem(typeof(HomeWindow))
        };

        // Constructor that takes Username as a parameter
        public MainContentWindow(string username)
        {
            InitializeComponent();

            DataContext = this;
            
            Username = username;

            //CurrentContentControl.Content = new HomeWindow();
        }

        private void Button_OnClick_ToggleSidebar(object? sender, RoutedEventArgs e)
        {
            Sidebar.IsPaneOpen = !Sidebar.IsPaneOpen;
        }

        private void SetContentWindow(Window window)
        {
            CurrentContentWindow = window;
        }
    }
}
