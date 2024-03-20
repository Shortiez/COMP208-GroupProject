using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;

namespace GroupProject.Windows
{
    public partial class HomeWindow : Window
    {
        public string Username { get; private set; }

        // Constructor that takes Username as a parameter
        public HomeWindow(string username)
        {
            InitializeComponent();
            Username = username;
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
    }
}
