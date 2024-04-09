using System.Collections.Generic;

namespace GroupProject.Scripts.Questions.Quizzes.TableUnion;

public class UnionQuizQuestion : QuizQuestion<string>
{
    // The title of the question
    public override string QuestionTitle { get; protected set; } = "What is the solution to the SQL query";

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
    public UnionQuizQuestion(string question, string answer, string[] headers) : base(question, answer, headers)
    {

    }
}
