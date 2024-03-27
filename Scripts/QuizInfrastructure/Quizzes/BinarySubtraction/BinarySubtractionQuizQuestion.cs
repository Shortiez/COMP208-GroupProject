namespace GroupProject.Scripts.Questions.Quizzes.BinarySubtraction;

/**
 * A quiz question that asks the user to subtract one Binary number from another.
 */
public class BinarySubtractionQuizQuestion : QuizQuestion<int>
{
    public override string QuestionTitle { get; protected set; } = "What is {a} - {b}?";

    public BinarySubtractionQuizQuestion()
    {
        
    }
    
    public BinarySubtractionQuizQuestion(string question, int answer, int[] options) : base(question, answer, options)
    {
        
    }
}