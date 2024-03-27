using Avalonia.Controls;
using Avalonia.Interactivity;

namespace GroupProject.Views
{
<<<<<<<< Updated upstream:Views/LogicGatesTopicPageView.axaml.cs
    public partial class LogicGatesTopicPageView : Window
    {
        public LogicGatesTopicPageView()
========
    /*
    Note:
    I have changed from logic gates as for the majority of logic questions there are only 2 answers
    0 or 1 obvs :)
    I will of course mention this in design documentation document
    Have changed to combinatorics questions :)
    */

    public partial class CombinatoricsTopicWindow : Window
    {
        public CombinatoricsTopicWindow()
>>>>>>>> Stashed changes:Views/CombinatoricsTopicWindow.axaml.cs
        {
            InitializeComponent();
        }

        private void Button_OnClick_Submit(object? sender, RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void Button_OnClick_GenerateNewQuestion(object? sender, RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }
    }
}
