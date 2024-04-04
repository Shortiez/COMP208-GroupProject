using System;

namespace GroupProject.Scripts.Questions.Quizzes.Combinatorics;

public class CombQuizSolver : QuizSolver<int>
{
    public override int Solve(QuizQuestion<int> question)
    {
        // The input parameters are stored in the QuestionInput property of the question
        int[] inputParameters = question.QuestionInput.ToArray();

        var actualAnswer = Factorial(inputParameters[1]) / Factorial(inputParameters[1] - inputParameters[0]);

        // Return the actual answer
        return actualAnswer;
    }

    public int Factorial(int num)
    {
        int fact = 1;

        for (int i = num; i > 0; i = i - 1)
        {
            fact = fact * i;
        }

        return fact;
    }


}