namespace GroupProject.Scripts.Questions.Quizzes.Example;

/**
 * A quiz solver for the addition quiz.
 */
public class AdditionQuizSolver : QuizSolver<int>
{
    public override int Solve(QuizQuestion<int> question)
    {
        // The input parameters are stored in the QuestionInput property of the question
        int[] inputParameters = question.QuestionInput.ToArray();
        
        // The actual answer to the question is the sum of the two input parameters
        var actualAnswer = inputParameters[0] + inputParameters[1];
        
        // Return the actual answer
        return actualAnswer;
    }
}