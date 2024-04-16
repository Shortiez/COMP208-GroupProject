using Avalonia.Controls.Primitives;
using GroupProject.VectorGraphicsAndAnimation;
using Mysqlx.Crud;
using System;
using System.Diagnostics;

namespace GroupProject.Scripts.Questions.Quizzes.TableUnion;

/***
 * This class is responsible for generating quiz questions.
 * You should extend this class to generate questions for your specific topic.
 * A quiz question should be a subclass of QuizQuestion.
 * Example:
 * public class BinaryAdditionQuizQuestion : QuizQuestion
 */

public class UnionQuizGenerator : QuizGenerator<string>
{
    protected static Random Random = new Random();

    static private string[] tableNames = [
        "Apes", "Fruits", "Cars", "Cards", "Chairs", "Lamps", "Transactions",
    ];
    // keep number-related headers first
    static private string[] headers = [        
        "Species", "Fruit", "Location", "Time", "Date", "Employee", "Customer",
        "Name", "Address", "Surname", "Forename", "Colour", "Shape", "Day",
    ];
    static private string[] numberHeaders = [
        "Size", "ID", "Price", "Value", "Age",
    ];

    // Various categories
    // MAKE SURE THESE EXIST IN TABLE UNION VIEW MODEL FIRST
    static private string[] squares = [
        "Square-Blue", "Square-Pink", "Square-Red",
    ];
    static private string[] circles = [
        "Circle-Blue", "Circle-Green", "Circle-Orange", "Circle-Pink", "Circle-Purple", "Circle-Red", "Circle-Yellow",
    ];
    static private string[] rhombuses = [
        "Rhombus-Blue", "Rhombus-Green", "Rhombus-Pink", "Rhombus-Purple", "Rhombus-Red", "Rhombus-Yellow",
    ];
    static private string[] triangles = [
        "Triangle-Blue", "Triangle-Green", "Triangle-Pink", "Triangle-Purple", "Triangle-Red", "Triangle-Yellow",
    ];
    static private string[] numbers = [
        "1","102","12","24","26","28","3", "32", "35","38","4","41","56","67","7","8",
    ];
    static private string[] apes = [
        "Baboon", "Gorilla", "Mandril", "Orangutan", "Squirrel",
    ];
    static private string[] fruits = [
        "Apple", "Banana", "Cherry", "Orange", "Pineapple",
    ];
    static private string[][] Shapes = [squares, circles, rhombuses, triangles];

    /***
     * This method generates a new question.
     * @return a new quiz question
     */
    protected override QuizQuestion<string> GenerateQuestion()
    {
        UnionQuizQuestion newQuestion = new UnionQuizQuestion();

        // choose table names
        int a = Random.Next(0, tableNames.Length);
        int b = Random.Next(0, tableNames.Length);
        while (b == a) b = Random.Next(0, tableNames.Length);
        string tableName = $"{tableNames[a]}+{tableNames[b]}";



        // choose unique column names in tables
        int head1 = Random.Next(0, headers.Length);
        int head3 = Random.Next(0, headers.Length);
        while (head3 == head1) head3 = Random.Next(0, headers.Length);

        // determine which collection of shapes will populate each column
        string[] firstColumn, secondColumn, thirdColumn, fourthColumn;
        int g = Random.Next(0, Shapes.Length);
        firstColumn = Shapes[g];
        thirdColumn = Shapes[g];

        // determine if the second columns of ecah tables are number-related or not
        int head2, head4, random;
        int NumberOrNot = Random.Next(0, 2);
        if (NumberOrNot == 0)
        {
            head2 = Random.Next(0, headers.Length);
            while (head2 == head1 || head2 == head3) head2 = Random.Next(0, headers.Length);
            head4 = Random.Next(0, headers.Length);
            while (head4 == head1 || head4 == head2 || head4 == head3) head4 = Random.Next(0, headers.Length);

            random = Random.Next(0, Shapes.Length);
            while (random == g) random = Random.Next(0, Shapes.Length);
            secondColumn = Shapes[random];
            fourthColumn = Shapes[random];
        }
        else
        {
            head2 = Random.Next(0, numberHeaders.Length);
            head4 = Random.Next(0, numberHeaders.Length);
            while (head4 == head2) head4 = Random.Next(0, numberHeaders.Length);

            secondColumn = numbers;
            fourthColumn = numbers;
        }
        newQuestion.SetOptions([headers[head1], headers[head2], headers[head3], headers[head4], tableName]);

        string question = "";
        if (tableNames[a] == "Apes" || tableNames[b] == "Apes" || tableNames[a] == "Fruits" || tableNames[b] == "Fruit")
        {
            newQuestion.SetOptions(["Species", "Age", "Fruit", "Price", "Apes+Fruit"]);
            firstColumn = apes;
            secondColumn = numbers;
            thirdColumn = fruits;
            fourthColumn = numbers;
            question = $"SELECT Species, Age \nFROM Apes \nUNION \nSELECT Fruit, Price \nFROM Fruits";
        }

        // choose how many items (combined) are going to be in both input tables 
        g = Random.Next(3, 7);
        int limit = (g+1)/2;

        // populate the table with randomised images
        int index;
        for (int x = 0; x < limit; x++)
        {
            index = Random.Next(0, firstColumn.Length);
            newQuestion.QuestionInput.Add(firstColumn[index]);
            index = Random.Next(0, secondColumn.Length);
            newQuestion.QuestionInput.Add(secondColumn[index]);
        }
        for (int x = limit; x < g; x++)
        {
            index = Random.Next(0, thirdColumn.Length);
            newQuestion.QuestionInput.Add(thirdColumn[index]);
            index = Random.Next(0, fourthColumn.Length);
            newQuestion.QuestionInput.Add(fourthColumn[index]);
        }

        // if question isn't already formatted
        if (question.Equals(""))
        {
            // form the question using previously chosen parts
            string h = $"{headers[head1]}, ";
            string i = $"{tableNames[a]}";
            string j = $"{headers[head3]}, ";
            string k = $"{tableNames[b]}";
            if (NumberOrNot == 0)
            {
                h += headers[head2];
                j += headers[head4];
            }
            else
            {
                h += numberHeaders[head2];
                j += numberHeaders[head4];
            }
            question = $"SELECT {h} \nFROM {i} \nUNION \nSELECT {j} \nFROM {k}";
        }
        newQuestion.SetTitle(question);

        // calculate the answer to the question
        string answer = new UnionQuizSolver().Solve(newQuestion);
        newQuestion.SetAnswer(answer);

        return newQuestion;
    }

    /***
     * This method should be overridden to generate a new option for a question.
     * @param answer the correct answer to the question
     * @return a new option for the question
     */
    protected override string GenerateOption(string answer)
    {
        return answer;
    }
}