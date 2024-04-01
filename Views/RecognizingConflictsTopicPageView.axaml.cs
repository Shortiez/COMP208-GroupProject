using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Media.Immutable;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Input;
//the below was suggested as a solution - todo - is there a way to just change the string reference of the file path?
using Bitmap = Avalonia.Media.Imaging.Bitmap;

namespace GroupProject.Views;

//



public partial class RecognizingConflictsTopicPageView : UserControl
{
    bool interactiveMode = false;
    // private RadioButton _selectedOption => OptionsPanel.Children.OfType<RadioButton>().FirstOrDefault(r => r.IsChecked == true);

    //to-do - eventually I'd like to add a reason why 2 things don't clash so I can prompt the user
    //in the text. Maybe I add an array to each to say why it doesn't clash with each other, which leads to a
    //premade strings ie "It doesn't clash by X and Y are different variables"
    public struct Transaction
    {

        public int transactionNumber;
        public char variable;
        public char operation;
        public int conflictIndex;
    }

   public Transaction[] exampleScheduler = new Transaction[]
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

        {"Transaction", "Uhoh! These transaction numbers are the same! Remember, they must be different for a conflict!" },
        {"Variable", "Uhoh! These variables are different! Remember, it's only a conflict if the variable is the same." },
        {"Operation", "Uhoh! These are both 'Reads'! Remember, at least one of them must be a 'Write', or 'W'" },
   

    };

    public Dictionary<string, string> teachingMaterial = new Dictionary<string, string>
    {

        {"Intro00", "Hey there!" },
        {"Intro01", "Let's get learning how to recognize conflicts in a schedule. We'll go step by step." },
        {"Intro02", "There are 3 things to check, for every transaction against every other. " },
         {"Intro03", "1: They must be a different transaction number. \n" +
                     "2: They must be trying to access the same variable. \n" +
                     "3: At least one of them must be a write, or 'W'"},
          {"Intro04", "First we check R1(x) against the next transacation." },
        {"Intro05", "It's not a conflict! The transaction number (1) is the same in each!" },
         {"Intro06", "Now you try! Click whichever transaction you think conflicts." },
          {"End00", "Fantastic! You found the conflict!" },
          {"End01", "Do you want to try some more questions?" },



    };

    public Dictionary<string, string> genericResponses = new Dictionary<string, string>
    {
        {"TryAgain", "Go ahead and give it another go!" },
    };




    public void NarrativePointer()
    {
        if (TextPrompt.Text == teachingMaterial["Intro00"])
        {

            TextPrompt.Text = teachingMaterial["Intro01"];

            TextFade();

        }

        else if (TextPrompt.Text == teachingMaterial["Intro01"])
        {
            CornerChimp.Source = new Bitmap("C:\\Users\\user\\Documents\\GitHub\\COMP208-GroupProject\\Assets\\Chimpa-corner-idea.png");
            TextPrompt.Text = teachingMaterial["Intro02"];
            TextFade();

        }

        else if (TextPrompt.Text == teachingMaterial["Intro02"])
        {
            CornerChimp.Source = new Bitmap("C:\\Users\\user\\Documents\\GitHub\\COMP208-GroupProject\\Assets\\Chimpa-corner.png");
            TextPrompt.Text = teachingMaterial["Intro03"];

            TextFade();
        }
        else if (TextPrompt.Text == teachingMaterial["Intro03"])
        {
            TextPrompt.Text = teachingMaterial["Intro04"];
            TransactionButton0.Foreground = Brushes.LightGreen;
            TextFade();
        }
        else if (TextPrompt.Text == teachingMaterial["Intro04"])
        {
            TextPrompt.Text = teachingMaterial["Intro05"];
            TransactionButton1.Foreground = Brushes.Red;
            TextFade();
        }
        else if (TextPrompt.Text == teachingMaterial["Intro05"])
        {
            TextPrompt.Text = teachingMaterial["Intro06"];
            TextFade();
            interactiveMode = true;
           

        }

        else if (userErrors.Any(keyValue => keyValue.Value == TextPrompt.Text))
        {
            TextPrompt.Text = genericResponses["TryAgain"];
            TextFade();
            interactiveMode = true;
            CornerChimp.Source = new Bitmap("C:\\Users\\user\\Documents\\GitHub\\COMP208-GroupProject\\Assets\\Chimpa-corner.png");

        }

        else if (TextPrompt.Text == teachingMaterial["End00"])
        {
            TextPrompt.Text = teachingMaterial["End01"];
            TextFade();
            //At this point, 


        }


    }

    public RecognizingConflictsTopicPageView()
    {
       

        InitializeComponent();
        //have to set these as the first points of entry, will change depending on mode 
        TextPrompt.Text = teachingMaterial["Intro00"];
        TextPrompt.Opacity = 1.0;

       //TO-DO make a for loop/function
        TransactionButton0.Content = TransactionFormatter(exampleScheduler[0]);
        TransactionButton1.Content = TransactionFormatter(exampleScheduler[1]);
        TransactionButton2.Content = TransactionFormatter(exampleScheduler[2]);
        TransactionButton3.Content = TransactionFormatter(exampleScheduler[3]);
        TransactionButton4.Content = TransactionFormatter(exampleScheduler[4]);

    }

 
    //a lot of logic, needs cleaning - possibly a switch statement?
    private void Button_OnClick_Submit(object? sender, RoutedEventArgs e)
    {
        String buttonName = "";
        if (sender is Button clickedButton) {
            //System.Diagnostics.Debug.WriteLine(sender.ToString());
            buttonName = clickedButton.Name;
         }

        if (interactiveMode == false)
        {
            if (buttonName == "NextButton")
            {
                NarrativePointer();
            }
        }

        else {

            if (buttonName == "TransactionButton2")
            {
                TransactionButton2.Foreground = Brushes.Red;
                CornerChimp.Source = new Bitmap("C:\\Users\\user\\Documents\\GitHub\\COMP208-GroupProject\\Assets\\Chimpa-fail.png");
                TextPrompt.Text = userErrors["Variable"];
                TextFade();
                interactiveMode = false;
            }

            else if (buttonName == "TransactionButton3")
            {
                TransactionButton3.Foreground = Brushes.LightGreen;
                CornerChimp.Source = new Bitmap("C:\\Users\\user\\Documents\\GitHub\\COMP208-GroupProject\\Assets\\Chimpa-success.png");
                TextPrompt.Text = teachingMaterial["End00"];
                TextFade();
                interactiveMode = false;

            }
            if (buttonName == "TransactionButton4")
            {
                TransactionButton4.Foreground = Brushes.Red;
                CornerChimp.Source = new Bitmap("C:\\Users\\user\\Documents\\GitHub\\COMP208-GroupProject\\Assets\\Chimpa-fail.png");
                TextPrompt.Text = userErrors["Operation"];
                TextFade();
                interactiveMode = false;
            }

        }
        
    }

    //takes a transaction object and returns it as a string - eventually want to do LATEX formatting 
    private String TransactionFormatter(Transaction transaction)
    {
        String transactionString = transaction.operation.ToString() + transaction.transactionNumber.ToString() + "(" + transaction.variable.ToString() + ")";

        return transactionString;


    }
    //text fading, NEEDS DOUBLE TRANSITION CHILD IN ORDER TO WORK in the AXML file 
    public void TextFade()
    {

        TextPrompt.Opacity = 0.2;
        TextPrompt.Opacity = 1.0;
    }

    

}


