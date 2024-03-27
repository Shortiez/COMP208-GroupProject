using System;
using System.Collections.Generic;
using System.Data;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using GroupProject.Scripts;
using MySql.Data.MySqlClient;

namespace GroupProject.Views;

public partial class PickATopicPageView : Window
{
    private readonly DatabaseConnection connectionDB = new DatabaseConnection();
    private MySqlCommand command;
    private MySqlDataAdapter da;
    private DataTable dt;

    protected TreeView ModulesTopics { get; private set; }

    public PickATopicPageView()
    {
        InitializeComponent();

        this.Opened += OnOpened;

        ModulesTopics = this.FindControl<TreeView>("ModulesTopics");
    }

    private void OnOpened(object? sender, EventArgs e)
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
                            //ModulesTopics.Items.Add(moduleItem);
                        }

                        var topicItem = new TreeViewItem { Header = topicName };
                        topicItem.PointerPressed += OnTopicClicked;
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

    private void OnTopicClicked(object? sender, PointerPressedEventArgs pointerPressedEventArgs)
    {
        var item = (TreeViewItem)sender!;
        var topic = item?.Header?.ToString();

        var window = new TopicLearnSelectorPageView()
        {
            TopicToDisplay = topic,
        };

        window.Show();
        window.InitWindow();

        this.Close();
    }
}