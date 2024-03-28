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

        var actualAnswer = 1;

        var param1 = inputParameters[1];

        for (int i = 1; i <= inputParameters[0]; i++)
        {
            actualAnswer = actualAnswer * param1;
            param1 = param1 - 1;
        }

        // Return the actual answer
        return actualAnswer;
    }
}