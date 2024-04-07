using System;

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
        "Cars", "Cards", "Lamps", "Transactions", "Table"
    ];
    static private string[] shapes = [
        "Blue-Square", "Pink-Square", "Red-Square", "White-Square",
    ];
    static private string[] headers = [
        "Size", "ID", "Price", "Value", "Location", "Time", "Date", "Employee", "Customer", 
        "Name", "Address", "Surname", "Forename", "Colour", "Age", "Shape", "Day"
    ];

    /***
     * This method should be overridden to generate a new question.
     * @return a new quiz question
     */
    protected override QuizQuestion<string> GenerateQuestion()
    {
        // need question(sql), tablenames, tableheadings, tablecontent, 
        UnionQuizQuestion newQuestion = new UnionQuizQuestion();

        int a = Random.Next(0, tableNames.Length);
        int b = Random.Next(0, tableNames.Length);
        while (b == a) b = Random.Next(0, tableNames.Length);
        newQuestion.SetTableName([tableNames[a], tableNames[b]]);

        int c = Random.Next(0, headers.Length);
        int d = Random.Next(0, headers.Length);
        while (d == c) d = Random.Next(0, headers.Length);
        int e = Random.Next(0, headers.Length);
        while (e == d || e == c) e = Random.Next(0, headers.Length);
        int f = Random.Next(0, headers.Length);
        while (f == e || f == d || f == c) f = Random.Next(0, headers.Length);
        newQuestion.SetHeaders([headers[c], headers[d], headers[e], headers[f], "null"]);

        int index;
        int g = Random.Next(2, 9);
        for (int x = 0; x < g*2; x++)
        {
            index = Random.Next(0, shapes.Length);
            newQuestion.QuestionInput.Add(shapes[index]);
        }

        string question = "";
        string h = $"{headers[c]}, {headers[d]}";
        string i = $"{tableNames[a]}";
        string j = $"{headers[e]}, {headers[f]}";
        string k = $"{tableNames[b]}";
        question = $"SELECT {h} /nFROM {i} /nUNION /nSELECT {j} /nFROM {k}";

        newQuestion.SetTitle(question);

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