using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System;
using System.Collections.Generic;

namespace GroupProject.Views;

public partial class RecognizingConflictsTopicPageView : Window
{


    //to-do - eventually I'd like to add a reason why 2 things don't clash so I can prompt the user
    //in the text. Maybe I add an array to each to say why it doesn't clash with each other, which leads to a
    //premade strings ie "It doesn't clash by X and Y are different variables"
    struct Transaction
    {

        public int transactionNumber;
        public char variable;
        public char operation;
        public int conflictIndex;
    }

    Transaction[] exampleScheduler = new Transaction[]
       {
            new Transaction { transactionNumber = 1, variable = 'x', operation = 'R' },
            new Transaction { transactionNumber = 1, variable = 'x', operation = 'W' },
            new Transaction { transactionNumber = 3, variable = 'y', operation = 'W' },
            new Transaction { transactionNumber = 2, variable = 'x', operation = 'W' },
            new Transaction { transactionNumber = 1, variable = 'z', operation = 'R' }
       };


    //right now, this will loop through and index any conflicts. 
    //there are 2 issues right now - if there are more than one conflict, it will use the last one.
    //it will also inefficiently check schedules that already have an an index conflict
    Transaction[] ConflictDetection(Transaction[] schedule)
    {
        Console.WriteLine(schedule[0].transactionNumber);

        for (int i = 0; i < schedule.Length; i++)
        {
            for (int j = 0; j < schedule.Length; j++)
            {

                if (schedule[i].transactionNumber == schedule[j].transactionNumber) { }
                else if (schedule[i].variable != schedule[j].variable) { }
                else if (schedule[i].operation == 'R' && schedule[j].operation == 'R') { }
                else
                {

                    schedule[i].conflictIndex = j;
                    schedule[j].conflictIndex = i;

                    //for checking
                    //Console.WriteLine("The schedule: " + i + " is conflicting with schedule: " + j);
                }
            }
        }
        return schedule;
    }

    //errors to relate to user - is there a better way than using string for the key?
    Dictionary<string, string> userErrors = new Dictionary<string, string>
    {

        {"Transaction", "These transaction numbers are the same! Remember, they must be different for a conflict!" },
        {"Variable", "These variables are different! Remember, it's only a conflict if the variable is the same." },
        {"Operation", "These are both 'Reads'! Remember, at least one of them must be a 'Write', or 'W'" },

    };

    public RecognizingConflictsTopicPageView()
    {
        InitializeComponent();
    }
}