using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System;
using System.Data;
using System.Collections.Generic;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using MySql.Data.MySqlClient;
using GroupProject.Scripts;

namespace GroupProject.Windows
{
    public partial class HomeWindow : Window
    {
        public string Username { get; private set; }
        dataBaseConnection connectionDB = new dataBaseConnection();
        MySqlCommand command;
        MySqlDataAdapter da;
        DataTable dt;

        // Constructor that takes Username as a parameter
        public HomeWindow(string username)
        {
            InitializeComponent();
            Username = username;
            this.Opened += OnOpened;
            ModulesTopics = this.FindControl<TreeView>("ModulesTopics");
        }

        private void OnOpened(object sender, EventArgs e)
        {
            connectionDB.Connect();
            PopulateTreeView();
        }
        private void PopulateTreeView()
        {
            try 
            {
                MySqlConnection conn = connectionDB.connection;
                conn.Open();
                using (MySqlCommand command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM `topics` WHERE 1";
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        Dictionary<string, TreeViewItem> modules = new Dictionary<string, TreeViewItem>();
                        while (reader.Read())
                        {
                            string moduleName = reader["ModuleName"].ToString();
                            string topicName = reader["TopicName"].ToString();

                            if (!modules.ContainsKey(moduleName))
                            {
                                var moduleItem = new TreeViewItem { Header = moduleName, IsExpanded = true };
                                modules[moduleName] = moduleItem;
                                ModulesTopics.Items.Add(moduleItem);
                                
                            }

                            var topicItem = new TreeViewItem { Header = topicName };
                            topicItem.KeyDown += OnTopicClicked;
                            modules[moduleName].Items.Add(topicItem);
                        }
                    }
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }

        private void OnTopicClicked(object? sender, KeyEventArgs e)
        {
            var item = (TreeViewItem)sender!;
            var topic = item?.Header?.ToString();

            var window = new TopicLearnSelectorWindow()
            {
                TopicToDisplay = topic,
            };

            window.Show();
            window.InitWindow();

            e.Handled = true;

            this.Close();
        }

        private void Button_OnClick_ToggleSidebar(object? sender, RoutedEventArgs e)
        {
            Sidebar.IsPaneOpen = !Sidebar.IsPaneOpen;
        }
    }
}
