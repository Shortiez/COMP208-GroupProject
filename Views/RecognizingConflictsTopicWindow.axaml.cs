using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace GroupProject.Windows;

public partial class RecognizingConflictsTopicWindow : Window
{
    
    struct Transaction
    {

        public int transactionNumber;
        public char variable;
        public char operation;
    }

    //explicitely declaring an example struct for the teaching, as I want it to be a specific order, based on UI and design doc
    //TODO - algorithm for generating schedules that already have conflicts and ones that don't


    Transaction exampleSchedule[] = {

        [0].transactionNumber = 1,  [0].variable = 'x',  [0].operation = 'R',
        [0].transactionNumber = 1,  [0].variable = 'x',  [0].operation = 'W',
        [0].transactionNumber = 3,  [0].variable = 'y',  [0].operation = 'W',
        [0].transactionNumber = 2,  [0].variable = 'x',  [0].operation = 'W',
        [0].transactionNumber = 1,  [0].variable = 'z',  [0].operation = 'R',

    }














    public RecognizingConflictsTopicWindow()


    {
        InitializeComponent();
    }
}