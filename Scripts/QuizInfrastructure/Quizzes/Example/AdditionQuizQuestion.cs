namespace GroupProject.Scripts.Questions.Quizzes.Example;

/**
 * A quiz question that asks the user to add two numbers together.
 */
public class AdditionQuizQuestion : QuizQuestion<int>
{
    public override string QuestionTitle { get; protected set; } = "What is {a} + {b}?";

    public AdditionQuizQuestion()
    {
        
    }
    
    public AdditionQuizQuestion(string question, int answer, int[] options) : base(question, answer, options)
    {
        
    }
}