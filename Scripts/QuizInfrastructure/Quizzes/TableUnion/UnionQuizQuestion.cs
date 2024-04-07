using System.Collections.Generic;

namespace GroupProject.Scripts.Questions.Quizzes.TableUnion;

public class UnionQuizQuestion : QuizQuestion<string>
{
    // The title of the question
    public virtual string QuestionTitle { get; protected set; } = "What is the solution to the SQL query";
    
    // The question itself ie "What is 1 + 1?" would have the question inputs of 1 and 1
    // The question input is the values that are used to generate the question
    // This is for reference only and should be used to verify the questions answer in the solver
    public List<string> QuestionInput { get; } = new List<string>();
    
    // The options for the question
    public string[] Headers { get; private set; } = new string[5];
    
    // The answer to the question
    public string Answer { get; private set; } = "";

    // The names for the tables
    public string[] TableName { get; private set; } = new string[2];

    /**
     * Default constructor for the QuizQuestion class
     */
    public UnionQuizQuestion()
    {

    }
    
    /**
     * Constructor for the QuizQuestion class
     * 
     * @param question The title of the question
     * @param answer The answer to the question
     * @param options The options for the question
     */
    public UnionQuizQuestion(string question, string answer, string[] headers, string[] tableName) : base(question, answer, headers)
    {
        QuestionTitle = question;
        Answer = answer;
        Headers = headers;
        TableName = tableName; 
    }

    public void SetTitle(string title)
    {
        QuestionTitle = title;
    }

    public void SetHeaders(string[] headers)
    {
        Headers = headers;
    }

    public void SetAnswer(string answer)
    {
        Answer = answer;
    }

    public void SetTableName(string[] tableName)
    {
        TableName = tableName;
    }
}
