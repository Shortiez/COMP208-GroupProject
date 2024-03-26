namespace GroupProject.Scripts.Questions.Quizzes.binaryAddition;
using System;
using Org.BouncyCastle.Utilities;

/**
 * A quiz solver for the binaryaddition quiz.
 */
public class binaryAdditionQuizSolver : QuizSolver<int>
{
    public override int Solve(QuizQuestion<int> question)
    {
        // The input parameters are stored in the QuestionInput property of the question
        int[] inputParameters = question.QuestionInput.ToArray();
        
        // The actual answer to the question is the sum of the two input parameters
        var actualAnswer = Convert.ToByte(inputParameters[0].ToString(), 2) + Convert.ToByte(inputParameters[1].ToString(), 2);
        
        // Return the actual answer
        return actualAnswer;
    }
}