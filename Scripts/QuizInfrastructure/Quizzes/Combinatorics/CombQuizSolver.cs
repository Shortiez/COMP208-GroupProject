using System;

namespace GroupProject.Scripts.Questions.Quizzes.Combinatorics;

public class CombQuizSolver : QuizSolver<int>
{
    //For questions like:
    //How many ways are there to select 3 students for a prospectus photograph (order matters) from a group of 5?

    public override int Solve(QuizQuestion<int> question)
    {
        // The input parameters are stored in the QuestionInput property of the question
        int[] inputParameters = question.QuestionInput.ToArray();

        var actualAnswer = Factorial(inputParameters[1]) / Factorial(inputParameters[0] - inputParameters[1]);

        // Return the actual answer
        return actualAnswer;
    }

    public int Factorial(int num)
    {
        int fact = 1;

        for (int i = num; i > 0; i = i - 1)
        {
            fact = fact * 1;
        }

        return fact;
    }


}