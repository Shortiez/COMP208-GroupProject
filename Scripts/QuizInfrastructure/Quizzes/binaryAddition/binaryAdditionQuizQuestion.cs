namespace GroupProject.Scripts.Questions.Quizzes.BinaryAddition;

/**
 * A quiz question that asks the user to add two Binary numbers together.
 */
public class BinaryAdditionQuizQuestion : QuizQuestion<int>
{
    public override string QuestionTitle { get; protected set; } = "What is {a} + {b}?";

    public BinaryAdditionQuizQuestion()
    {
        
    }
    
    public BinaryAdditionQuizQuestion(string question, int answer, int[] options) : base(question, answer, options)
    {
        
    }
}