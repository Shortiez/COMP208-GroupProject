namespace GroupProject.Scripts.Questions.Quizzes.binaryAddition;

/**
 * A quiz question that asks the user to add two binary numbers together.
 */
public class binaryAdditionQuizQuestion : QuizQuestion<int>
{
    public override string QuestionTitle { get; protected set; } = "What is {a} + {b}?";

    public binaryAdditionQuizQuestion()
    {
        
    }
    
    public binaryAdditionQuizQuestion(string question, int answer, int[] options) : base(question, answer, options)
    {
        
    }
}